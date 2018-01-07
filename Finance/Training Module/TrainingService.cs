using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinOrg.Training_Module
{
	public partial class TrainingService : FinOrgForm
	{

		DataSet _data;

		public TrainingService() : base()
		{
			InitializeComponent();

			init_data();
			init_ui();
		}

		/// <summary>
		/// States
		/// 0.	Reset
		/// 1.	View
		/// 2.	Edit
		/// 3.	New
		/// </summary>
		protected override void SetFormState(FinOrgFormState state)
		{
			bool ReadOnly = false;
			
			switch(state)
			{
				case FinOrgFormState.Reset:
					// clear and set to view mode
					service_code_tb.Text = string.Empty;
					service_ename_tb.Text = string.Empty;
					service_aname_tb.Text = string.Empty;
					searchBox.Text = string.Empty;
					ratesDataGrid.DataSource = null;

					servicesDataGrid.Visible = false;

					SetFormState(FinOrgFormState.View);
					ReadOnly = true;
					break;
				case FinOrgFormState.View:
					// View
					ReadOnly = true;
					break;
				case FinOrgFormState.Edit:
					// Edit
					ReadOnly = false;
					break;
				case FinOrgFormState.New:
					ReadOnly = false;

					// Set DataSource for Rates a new table
					ratesDataGrid.DataSource = _data.Tables["TR_SERVICE_RATES"].Clone().DefaultView;
					break;
			}
			service_ename_tb.ReadOnly = ReadOnly;
			service_aname_tb.ReadOnly = ReadOnly;
			service_code_tb.ReadOnly = ReadOnly || state == FinOrgFormState.Edit;
			ratesDataGrid.ReadOnly = ReadOnly;



			base.SetFormState(state);
		}

		#region INIT
		public void init_data()
		{
			// load service names
			_data = new DataSet();

			using (SqlConnection con = getSqlConnection())
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TR_PRIORITY;", con);
				adapter.Fill(_data);
				_data.Tables[0].TableName = "TR_PRIORITY";
				adapter.SelectCommand = new SqlCommand("SELECT * FROM TR_PERIOD", con);
				adapter.Fill(_data);
				_data.Tables[1].TableName = "TR_PERIOD";

				// for schema, no data
				adapter.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM TR_SERVICE_RATES;", con);
				adapter.Fill(_data);
				_data.Tables[2].TableName = "TR_SERVICE_RATES";
				_data.Tables[2].Clear();
			}
		}

		public void init_ui()
		{

			ratesDataGrid.AutoGenerateColumns = false;
			servicesDataGrid.AutoGenerateColumns = false;
			ratesDataGrid.DataSource = null;
			
			ratesDataGrid.Columns[0].HeaderText = Languages.GetStringTranslation("Training Period");
			ratesDataGrid.Columns[1].HeaderText = Languages.GetStringTranslation("Training Priority");
			ratesDataGrid.Columns[2].HeaderText = Languages.GetStringTranslation("Rate");

			rates_periodCBColumn.DataPropertyName = "TR_PERIOD_CODE";
			rates_periodCBColumn.DataSource = _data.Tables["TR_PERIOD"].DefaultView;
			rates_periodCBColumn.ValueMember = "TR_PERIOD_CODE";
			rates_periodCBColumn.DisplayMember = Languages.currentLanguage == "english" ? "TR_PERIOD_NAME" : "TR_PERIOD_ANAME";

			rates_priorityCBColumn.DataPropertyName = "TR_PRIORITY_CODE";
			rates_priorityCBColumn.DataSource = _data.Tables["TR_PRIORITY"].DefaultView;
			rates_priorityCBColumn.ValueMember = "TR_PRIORITY_CODE";
			rates_priorityCBColumn.DisplayMember = Languages.currentLanguage == "english" ? "TR_PRIORITY_NAME" : "TR_PRIORITY_ANAME";

			rates_rateColumn.DataPropertyName = "TR_SERVICE_RATE";

			servicesDataGrid.Columns[0].HeaderText = Languages.GetStringTranslation("Code");
			servicesDataGrid.Columns[1].HeaderText = Languages.GetStringTranslation("Name");
			servicesDataGrid.Columns[2].HeaderText = Languages.GetStringTranslation("Arabic Name");

			// Add FormVisibility Items
			FormStateControlVisibility.Add(FinOrgFormState.Reset, new object[] { toolStrip_addButton });
			FormStateControlVisibility.Add(FinOrgFormState.View, new object[] { toolStrip_addButton, toolStrip_editButton, toolStrip_deleteButton });
			FormStateControlVisibility.Add(FinOrgFormState.Edit, new object[] { toolStrip_saveButton, toolStrip_cancelButton });
			FormStateControlVisibility.Add(FinOrgFormState.New, new object[] { toolStrip_saveButton, toolStrip_cancelButton });

			SetFormState(FinOrgFormState.Reset);
			servicesDataGrid.Top = searchBox.Top + searchBox.Height;
			servicesDataGrid.Left = searchBox.Left;
		}
		#endregion

		public void LoadServiceDetails(object code)
		{
			if (code == null)
				return;

			try
			{
				using (SqlConnection con = getSqlConnection())
				{
					con.Open();
					DataSet dataset = new DataSet();
					SqlCommand cmd = new SqlCommand("SELECT * FROM TR_SERVICES WHERE TR_SERVICE_CODE = @Code; SELECT * FROM TR_SERVICE_RATES WHERE TR_SERVICE_CODE = @Code;", con);
					cmd.Parameters.Add(new SqlParameter("Code", code));
					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
					adapter.Fill(dataset);

					DataRow main_data = dataset.Tables[0].Rows[0];
					service_code_tb.Text = main_data["TR_SERVICE_CODE"].ToString();
					service_ename_tb.Text = main_data["TR_SERVICE_NAME"].ToString();
					service_aname_tb.Text = main_data["TR_SERVICE_ANAME"].ToString();

					ratesDataGrid.DataSource = dataset.Tables[1].DefaultView;
					SetFormState(FinOrgFormState.View);
				}
			} catch (Exception e)
			{
				SetFormState(FinOrgFormState.Reset);
				MessageBox.Show("Load Failed " + code, "FinOrg TrainingService LoadServiceDetails");
			}
		}

		public void SaveServiceDetails()
		{
			if (string.IsNullOrWhiteSpace(service_code_tb.Text))
			{
				MessageBox.Show("Code is mandatory", "FinOrg TrainingService SaveServiceDetails");
				return;
			}
			int code = -1;
			if (!int.TryParse(service_code_tb.Text.Trim(), out code))
			{
				MessageBox.Show("Please enter number code", "FinOrg TrainingService SaveServiceDetails");
				return;
			}

			SqlTransaction tr = null;
			try
			{
				using (SqlConnection con = getSqlConnection())
				{
					con.Open();

					SqlCommand cmd = null;
					if (CurrentState == FinOrgFormState.New)
					{
						// check for existence
						cmd = new SqlCommand("SELECT COUNT(*) FROM TR_SERVICES WHERE TR_SERVICE_CODE = @Code", con);
						cmd.Parameters.Add(new SqlParameter("Code", code));
						if ((int?)cmd.ExecuteScalar() != 0)
						{
							MessageBox.Show("Code Exists");
							return;
						}
						cmd.Parameters.Clear();
					}

					tr = con.BeginTransaction();
					cmd = new SqlCommand();
					cmd.Connection = con;
					cmd.Transaction = tr;
					if (CurrentState == FinOrgFormState.New)
						cmd.CommandText = "INSERT INTO TR_SERVICES (TR_SERVICE_CODE, TR_SERVICE_NAME, TR_SERVICE_ANAME) VALUES (@Code, @Name, @EName);";
					else
						cmd.CommandText = "UPDATE TR_SERVICES SET TR_SERVICE_NAME = @Name, TR_SERVICE_ANAME = @EName; DELETE FROM TR_SERVICE_RATES WHERE TR_SERVICE_CODE = @Code;";
					cmd.Parameters.Add(new SqlParameter("Code", code));
					cmd.Parameters.Add(new SqlParameter("Name", service_ename_tb.Text.Trim()));
					cmd.Parameters.Add(new SqlParameter("EName", service_aname_tb.Text.Trim()));
					cmd.ExecuteNonQuery();
					cmd.Parameters.Clear();

					cmd.Parameters.Add(new SqlParameter("@Code", code));
					string rates_cmd_text = "INSERT INTO TR_SERVICE_RATES () VALUES ";
					StringBuilder sb = new StringBuilder();
					DataView dv = ratesDataGrid.DataSource as DataView;
					for (int i = 0; i < dv.Count; i++)
					{
						if (i > 0)
							sb.Append(", ");
						sb.Append(string.Format("(@Code, @Period{0}, @Priority{0}, @Rate{0})", i));
						cmd.Parameters.Add(new SqlParameter("Period" + i, dv[i]["TR_PERIOD_CODE"]));
						cmd.Parameters.Add(new SqlParameter("Priority" + i, dv[i]["TR_PRIORITY_CODE"]));
						cmd.Parameters.Add(new SqlParameter("Rate" + i, dv[i]["TR_SERVICE_RATE"]));
					}
					cmd.CommandText = "INSERT INTO TR_SERVICE_RATES (TR_SERVICE_CODE, TR_PERIOD_CODE, TR_PRIORITY_CODE, TR_SERVICE_RATE) VALUES " + sb.ToString();
					cmd.ExecuteNonQuery();

					tr.Commit();
				}
				SetFormState(FinOrgFormState.View);
			} catch (Exception e)
			{
				tr.TryRollback();
				MessageBox.Show("Failed" + "\n" + e.Message, "FinOrg TrainingService SaveServiceDetails");
			}
		}

		public void DeleteService(int code)
		{
			try
			{
				using (SqlConnection con = getSqlConnection())
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM TR_SERVICE_RATES WHERE TR_SERVICE_CODE = @Code; DELETE FROM TR_SERVICES WHERE TR_SERVICE_CODE = @Code;", con);
					cmd.Parameters.Add(new SqlParameter("Code", code));
					cmd.ExecuteNonQuery();
				}
				MessageBox.Show("Success");
				SetFormState(FinOrgFormState.Reset);
			} catch (Exception ef)
			{
				MessageBox.Show("Failed");
			}
		}


		#region UI EVENTS
		private void toolStrip_addButton_Click(object sender, EventArgs e)
		{
			SetFormState(FinOrgFormState.Reset); // Clears
			SetFormState(FinOrgFormState.New);
		}

		private void toolStrip_editButton_Click(object sender, EventArgs e)
		{
			SetFormState(FinOrgFormState.Edit);
		}

		private void toolStrip_saveButton_Click(object sender, EventArgs e)
		{
			SaveServiceDetails();
		}

		private void toolStrip_cancelButton_Click(object sender, EventArgs e)
		{
			SetFormState(FinOrgFormState.Reset);
		}

		private void toolStrip_deleteButton_Click(object sender, EventArgs e)
		{
			if (CurrentState == FinOrgFormState.View)
			{
				int code;
				if (!int.TryParse(service_code_tb.Text, out code))
				{
					MessageBox.Show("Invalid Code");
					return;
				}
				DeleteService(code);
			}
			else
				MessageBox.Show("Invalid Action");
		}

		private void searchBox_TextChanged(object sender, EventArgs e)
		{
			ShowHideServiceDGridView(true);
			try
			{
				string text = searchBox.Text.Trim();
				if (string.IsNullOrWhiteSpace(text))
				{
					ShowHideServiceDGridView(false);
					return;
				}

				SqlConnection con = servicesDataGrid.Tag as SqlConnection;
				SqlCommand cmd = new SqlCommand("SELECT * FROM TR_SERVICES WHERE TR_SERVICE_CODE LIKE Concat('%', @t, '%') OR TR_SERVICE_NAME LIKE Concat('%', @t, '%') OR TR_SERVICE_ANAME LIKE Concat('%', @t, '%');", con);
				cmd.Parameters.Add(new SqlParameter("t", text));
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable tbl = new DataTable();
				adapter.Fill(tbl);
				servicesDataGrid.DataSource = tbl.DefaultView;
			}
			catch (Exception ef)
			{
				MessageBox.Show("");
			}
		}

		private void servicesDataGrid_DoubleClick(object sender, EventArgs e)
		{
			if (servicesDataGrid.SelectedRows.Count == 0 || servicesDataGrid.SelectedRows[0].DataBoundItem == null)
				return;
			ShowHideServiceDGridView(false);
			LoadServiceDetails(((DataRowView)servicesDataGrid.SelectedRows[0].DataBoundItem)["TR_SERVICE_CODE"]);
			searchBox.Text = "";
		}

		private void searchBox_Leave(object sender, EventArgs e)
		{
			
		}

		private void ShowHideServiceDGridView(bool show)
		{
			servicesDataGrid.Visible = show;
			if (!show && servicesDataGrid.Tag != null && servicesDataGrid.Tag.GetType() == typeof(SqlConnection))
			{
				((SqlConnection)servicesDataGrid.Tag).Close();
				servicesDataGrid.Tag = null;
			}
			else if (show && servicesDataGrid.Tag == null)
			{
				SqlConnection con = getSqlConnection();
				con.Open();
				servicesDataGrid.Tag = con;
			}
		}
		#endregion

	}
}
