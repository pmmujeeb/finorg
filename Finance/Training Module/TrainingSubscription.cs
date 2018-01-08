using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace FinOrg.Training_Module
{
	public partial class TrainingSubscription : FinOrgForm
	{

		private DataSet _data;

		public TrainingSubscription() : base()
		{
			InitializeComponent();

			init_data();
			init_ui();
		}


		protected override void SetFormState(FinOrgFormState state)
		{
			bool ReadOnly = false;

			switch (state)
			{
				case FinOrgFormState.Reset:
					// clear and set to view mode
					customer_tb.Text = string.Empty;
					code_tb.Text = string.Empty;
					trans_date.Value = DateTime.Today;
					start_date.Value = DateTime.Today;
					items.DataSource = null;

					ShowHideDGridView(false, subscription_datagrid);
					ShowHideDGridView(false, customer_datagrid);

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
					items.DataSource = _data.Tables["TR_SUBSCRIPTION_ITEM"].Clone().DefaultView;
					break;
			}
			code_tb.ReadOnly = ReadOnly || state == FinOrgFormState.Edit;
			customer_tb.ReadOnly = ReadOnly;
			trans_date.Enabled = !ReadOnly;
			start_date.Enabled = !ReadOnly;
			items.ReadOnly = ReadOnly;
			discount_tb.ReadOnly = ReadOnly;
			advance_tb.ReadOnly = ReadOnly;
			payment_period_cb.Enabled = !ReadOnly;

			base.SetFormState(state);
		}

		public void init_data()
		{
			// load service names
			_data = new DataSet();

			using (SqlConnection con = getSqlConnection())
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TR_PRIORITY;", con);
				adapter.Fill(_data);
				_data.Tables[0].TableName = "TR_PRIORITY";

				// TR_PERIOD
				adapter.SelectCommand = new SqlCommand("SELECT * FROM TR_PERIOD", con);
				adapter.Fill(_data);
				_data.Tables[1].TableName = "TR_PERIOD";


				//TR SERVICES
				adapter.SelectCommand = new SqlCommand("SELECT * FROM TR_SERVICES;", con);
				adapter.Fill(_data);
				_data.Tables[2].TableName = "TR_SERVICES";
				adapter.SelectCommand = new SqlCommand("SELECT * FROM TR_SERVICE_RATES;", con);
				adapter.Fill(_data);
				_data.Tables[3].TableName = "TR_SERVICE_RATES";


				// for schema
				adapter.SelectCommand = new SqlCommand("SELECT TOP 1 * FROM TR_SUBSCRIPTION_ITEM;", con);
				adapter.Fill(_data);
				_data.Tables[4].TableName = "TR_SUBSCRIPTION_ITEM";
				_data.Tables[4].Clear();
			}
		}

		public void init_ui()
		{
			items.AutoGenerateColumns = false;
			customer_datagrid.AutoGenerateColumns = false;
			subscription_datagrid.AutoGenerateColumns = false;

			period_CBColumn.HeaderText = Languages.GetStringTranslation("Period");
			period_CBColumn.DataSource = _data.Tables["TR_PERIOD"].DefaultView;
			period_CBColumn.ValueMember = "TR_PERIOD_CODE";
			period_CBColumn.DisplayMember = Languages.currentLanguage == "english" ? "TR_PERIOD_NAME" : "TR_PERIOD_ANAME";

			priority_CBColumn.HeaderText = Languages.GetStringTranslation("Priority");
			priority_CBColumn.DataSource = _data.Tables["TR_PRIORITY"].DefaultView;
			priority_CBColumn.ValueMember = "TR_PRIORITY_CODE";
			priority_CBColumn.DisplayMember = Languages.currentLanguage == "english" ? "TR_PRIORITY_NAME" : "TR_PRIORITY_ANAME";

			service_CBColumn.HeaderText = Languages.GetStringTranslation("Service");
			service_CBColumn.DataSource = _data.Tables["TR_SERVICES"];
			service_CBColumn.ValueMember = "TR_SERVICE_CODE";
			service_CBColumn.DisplayMember = Languages.currentLanguage == "english" ? "TR_SERVICE_NAME" : "TR_SERVICE_ANAME";

			payment_period_cb.DataSource = _data.Tables["TR_PERIOD"];
			payment_period_cb.ValueMember = "TR_PERIOD_CODE";
			payment_period_cb.DisplayMember = Languages.currentLanguage == "english" ? "TR_PERIOD_NAME" : "TR_PERIOD_ANAME";

			// Add FormVisibility Items
			FormStateControlVisibility.Add(FinOrgFormState.Reset, new object[] { toolStrip_addButton });
			FormStateControlVisibility.Add(FinOrgFormState.View, new object[] { toolStrip_addButton, toolStrip_editButton, toolStrip_deleteButton });
			FormStateControlVisibility.Add(FinOrgFormState.Edit, new object[] { toolStrip_saveButton, toolStrip_cancelButton });
			FormStateControlVisibility.Add(FinOrgFormState.New, new object[] { toolStrip_saveButton, toolStrip_cancelButton });


			subscription_datagrid.Top = search_tb.Top + search_tb.Height;
			subscription_datagrid.Left = search_tb.Left;

			customer_codeColumn.HeaderText = Languages.GetStringTranslation("Code");
			customer_nameColumn.HeaderText = Languages.GetStringTranslation("Name");
			customer_anameColumn.HeaderText = Languages.GetStringTranslation("Arabic Name");
			customer_datagrid.Top = customer_tb.Top + customer_tb.Height;
			customer_datagrid.Left = customer_tb.Left;

			SetFormState(FinOrgFormState.Reset);
		}

		private void LoadSubscription(object code)
		{
			if (code == null)
				return;

			try
			{
				using (SqlConnection con = getSqlConnection())
				{
					con.Open();
					DataSet dataset = new DataSet();
					SqlCommand cmd = new SqlCommand("SELECT * FROM TR_SUBSCRIPTION WHERE TR_SUBSCRIPTION_CODE = @Code; SELECT * FROM TR_SUBSCRIPTION_ITEM WHERE TR_SUBSCRIPTION_CODE = @Code;", con);
					cmd.Parameters.Add(new SqlParameter("Code", code));
					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
					adapter.Fill(dataset);

					DataRow main_data = dataset.Tables[0].Rows[0];
					code_tb.Text = main_data["TR_SUBSCRIPTION_CODE"].ToString();
					trans_date.Value = DateTime.Parse(main_data["TR_SUBSCRIPTION_DATE"].ToString());
					start_date.Value = DateTime.Parse(main_data["TR_SUBSCRIPTION_START_DATE"].ToString());
					customer_tb.Text = main_data["CUSTOMER_ACC_NO"].ToString();
					total_tb.Text = main_data["TR_SUBSCRIPTION_TOTAL"].ToString();
					discount_tb.Text = main_data["TR_SUBSCRIPTION_DISCOUNT"].ToString();
					advance_tb.Text = main_data["TR_SUBSCRIPTION_ADVANCE"].ToString();

					items.DataSource = dataset.Tables[1].DefaultView;

					CalculateTotals();
					SetFormState(FinOrgFormState.View);
				}
			} catch (Exception ef)
			{
				SetFormState(FinOrgFormState.Reset);
				MessageBox.Show("Load Failed " + code, "FinOrg TrainingSubscription LoadSubscription");
			}
		}

		private void SaveForm()
		{
			if (string.IsNullOrWhiteSpace(code_tb.Text))
			{
				MessageBox.Show("Code is mandatory", "FinOrg TrainingSubscription SaveForm");
				return;
			}
			int code = -1;
			if (!int.TryParse(code_tb.Text, out code))
			{
				MessageBox.Show("Please enter number code", "FinOrg TrainingSubscription SaveForm");
				return;
			}

			SqlTransaction tr = null;
			try
			{
				CalculateTotals();
				using (SqlConnection con  = getSqlConnection())
				{
					con.Open();

					SqlCommand cmd = new SqlCommand();
					cmd.Connection = con;
					if (CurrentState == FinOrgFormState.New)
					{
						// check for existence
						cmd = new SqlCommand("SELECT COUNT(*) FROM TR_SUBSCRIPTION WHERE TR_SUBSCRIPTION_CODE = @Code", con);
						cmd.Parameters.Add(new SqlParameter("Code", code));
						if ((int?)cmd.ExecuteScalar() != 0)
						{
							MessageBox.Show("Code Exists");
							return;
						}
						cmd.Parameters.Clear();
					}

					tr = con.BeginTransaction();
					cmd.Transaction = tr;
					if (CurrentState == FinOrgFormState.New)
						cmd.CommandText = "INSERT INTO TR_SUBSCRIPTION (TR_SUBSCRIPTION_CODE, CUSTOMER_ACC_NO, TR_SUBSCRIPTION_DATE, TR_SUBSCRIPTION_START_DATE, TR_SUBSCRIPTION_TOTAL, TR_SUBSCRIPTION_DISCOUNT, TR_SUBSCRIPTION_ADVANCE) VALUES (@Code, @CUS_ACC_NO, @Date, @StartDate, @Total, @Discount, @Advance)";
					else
						cmd.CommandText = "UPDATE TR_SUBSCRIPTION SET CUSTOMER_ACC_NO = @CUS_ACC_NO, @TR_SUBSCRIPTION_DATE = @Date, @TR_SUBSCRIPTION_START_DATE = @StartDate, TR_SUBSCRIPTION_TOTAL = @Total, TR_SUBSCRIPTION_DISCOUNT = @Discount, @TR_SUBSCRIPTION_ADVANCE = @Advance WHERE TR_SUBSCRIPTION_CODE = @Code; DELETE FROM TR_SUBSCRIPTION_ITEM WHERE TR_SUBSCRIPTION_CODE = @Code";
					cmd.Parameters.AddWithValue("Code", code);
					cmd.Parameters.AddWithValue("CUS_ACC_NO", int.Parse(customer_tb.Text));
					cmd.Parameters.AddWithValue("Date", trans_date.Value);
					cmd.Parameters.AddWithValue("StartDate", start_date.Value);
					cmd.Parameters.AddWithValue("Total", float.Parse(total_tb.Text));
					cmd.Parameters.AddWithValue("Discount", float.Parse(discount_tb.Text));
					cmd.Parameters.AddWithValue("Advance", float.Parse(advance_tb.Text));
					cmd.ExecuteScalar();
					cmd.Parameters.Clear();

					cmd.Parameters.AddWithValue("Code", code);
					StringBuilder sb = new StringBuilder();
					DataView dv = items.DataSource as DataView;
					for (int i = 0; i < dv.Count; i++)
					{
						DataRowView v = dv[i];
						if (i > 0)
							sb.Append(", ");
						sb.Append(string.Format("(@Code, @Service{0}, @Period{0}, @Priority{0}, @Rate{0})", i));
						cmd.Parameters.AddWithValue("Service" + i, v["TR_SUBITEM_SERVICE_CODE"]);
						cmd.Parameters.AddWithValue("Period" + i, v["TR_SUBITEM_PERIOD_CODE"]);
						cmd.Parameters.AddWithValue("Priority" + i, v["TR_SUBITEM_PRIORITY_CODE"]);
						cmd.Parameters.AddWithValue("Rate" + i, v["TR_SUBITEM_RATE"]);
					}
					cmd.CommandText = "INSERT INTO TR_SUBSCRIPTION_ITEM (TR_SUBSCRIPTION_CODE, TR_SUBITEM_SERVICE_CODE, TR_SUBITEM_PERIOD_CODE, TR_SUBITEM_PRIORITY_CODE, TR_SUBITEM_RATE) VALUES " + sb.ToString();
					cmd.ExecuteNonQuery();

					tr.Commit();
				}
				MessageBox.Show("Success");
				SetFormState(FinOrgFormState.View);
			} catch (Exception ef)
			{
				tr.TryRollback();
				MessageBox.Show("Failed" + "\n" + ef.Message, "F");
			}
		}

		public void DeleteSubscription(int code)
		{
			try
			{
				using (SqlConnection con = getSqlConnection())
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM TR_SUBSCRIPTION WHERE TR_SUBSCRIPTION_CODE = @Code; DELETE FROM TR_SUBSCRIPTION_ITEM WHERE TR_SUBSCRIPTION_CODE = @Code;", con);
					cmd.Parameters.Add(new SqlParameter("Code", code));
					cmd.ExecuteNonQuery();
				}
				MessageBox.Show("Success");
				SetFormState(FinOrgFormState.Reset);
			}
			catch (Exception ef)
			{
				MessageBox.Show("Failed");
			}
		}

		private void CalculateTotals()
		{
			if (items.DataSource == null)
				return;

			double total = 0;
			foreach (DataRowView v in (DataView)items.DataSource)
			{
				if (v.Row.RowState != DataRowState.Detached)
					total += v["TR_SUBITEM_RATE"] != null ? (double)v["TR_SUBITEM_RATE"] : 0;
			}
			total_tb.Text = total.ToString("N2");
			float discount = 0;
			float.TryParse(discount_tb.Text, out discount);
			discount_tb.Text = discount.ToString("N2");
			grandtotal_tb.Text = (total - discount).ToString("N2");
			float advance = 0;
			float.TryParse(advance_tb.Text, out advance);
			advance_tb.Text = advance.ToString("N2");
			balance_tb.Text = (total - discount - advance).ToString("N2");
		}

		private double getServiceRate(int service, int period, int priority)
		{
			DataRow[] items = _data.Tables["TR_SERVICE_RATES"].Select(string.Format("TR_SERVICE_CODE = {0} AND TR_PERIOD_CODE = {1} AND TR_PRIORITY_CODE = {2}", service, period, priority));
			if (items.Length > 0)
				return (double)items[0]["TR_SERVICE_RATE"];

			// conflict: priority unit rate
			// no matching row-- get unit rate and multiply
			double min_unit_price_for_priority = 9999;
			object days_required = _data.Tables["TR_PERIOD"].Compute("SUM(TR_PERIOD_DAYS)", string.Format("TR_PERIOD_CODE = {0}", period));
			items = _data.Tables["TR_SERVICE_RATES"].Select(string.Format("TR_SERVICE_CODE = {0}", service));
			foreach (DataRow r in items)
			{
				// get number of days for the period in this particular row
				object days = _data.Tables["TR_PERIOD"].Compute("SUM(TR_PERIOD_DAYS)", string.Format("TR_PERIOD_CODE = {0}", r["TR_PERIOD_CODE"]));
				double rate = r["TR_SERVICE_RATE"] != null ? (double)r["TR_SERVICE_RATE"] : 0;
				if (days.GetType() != typeof(Int64))
				{
					MessageBox.Show("Type Invalid: TrainingSubscription.getServiceRate");
					throw new Exception("Invalid Type");
				}
				if (min_unit_price_for_priority > rate / (Int64)days)
				{
					min_unit_price_for_priority = rate / (Int64)days;
				}
			}

			if (days_required.GetType() != typeof(Int64))
			{
				MessageBox.Show("Type Invalid: TrainingSubscription.getServiceRate");
				throw new Exception("Invalid Type");
			}

			return min_unit_price_for_priority * (Int64)days_required;
		}

		private void search_tb_TextChanged(object sender, EventArgs e)
		{
			ShowHideDGridView(true, subscription_datagrid);
			try
			{
				string text = search_tb.Text.Trim();
				if (string.IsNullOrWhiteSpace(text))
				{
					ShowHideDGridView(false, subscription_datagrid);
					return;
				}

				SqlConnection con = subscription_datagrid.Tag as SqlConnection;
				SqlCommand cmd = new SqlCommand("SELECT ACC.ACC_" + (Languages.currentLanguage == "english" ? "" : "A") + "NAME AS CUSTOMER_NAME, SUB.TR_SUBSCRIPTION_CODE, (SUB.TR_SUBSCRIPTION_TOTAL - SUB.TR_SUBSCRIPTION_DISCOUNT) AS GRAND_TOTAL FROM TR_SUBSCRIPTION as SUB LEFT JOIN ACCOUNTS as ACC ON SUB.CUSTOMER_ACC_NO = ACC.ACC_NO WHERE TR_SUBSCRIPTION_CODE LIKE Concat('%', @t, '%') OR CUSTOMER_ACC_NO LIKE Concat('%', @t, '%') OR ACC.ACC_NAME LIKE Concat('%', @t, '%') OR ACC.ACC_ANAME LIKE Concat('%', @t, '%');", con);
				cmd.Parameters.Add(new SqlParameter("t", text));
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable tbl = new DataTable();
				adapter.Fill(tbl);
				subscription_datagrid.DataSource = tbl.DefaultView;
			}
			catch (Exception ef)
			{
				MessageBox.Show("");
			}
		}

		private void ShowHideDGridView(bool show, DataGridView dg)
		{
			dg.Visible = show;
			if (!show && dg.Tag != null && dg.Tag.GetType() == typeof(SqlConnection))
			{
				((SqlConnection)dg.Tag).Close();
				dg.Tag = null;
			}
			else if (show && dg.Tag == null)
			{
				SqlConnection con = getSqlConnection();
				con.Open();
				dg.Tag = con;
			}
		}

		private void subscription_datagrid_DoubleClick(object sender, EventArgs e)
		{
			if (subscription_datagrid.SelectedRows.Count == 0 || subscription_datagrid.SelectedRows[0].DataBoundItem == null)
				return;
			search_tb.Text = "";
			LoadSubscription(((DataRowView)subscription_datagrid.SelectedRows[0].DataBoundItem)["TR_SUBSCRIPTION_CODE"]);
			ShowHideDGridView(false, subscription_datagrid);
		}

		private void customer_tb_TextChanged(object sender, EventArgs e)
		{
			ShowHideDGridView(true, customer_datagrid);
			customer_name.Text = "";
			try
			{
				string text = customer_tb.Text.Trim();
				if (string.IsNullOrWhiteSpace(text))
				{
					ShowHideDGridView(false, customer_datagrid);
					return;
				}

				SqlConnection con = customer_datagrid.Tag as SqlConnection;
				SqlCommand cmd = new SqlCommand("SELECT ACC_NO, ACC_NAME, ACC_ANAME, DEF_CURRENCY FROM ACCOUNTS WHERE ACC_NO LIKE Concat('%', @t, '%') OR ACC_NAME LIKE Concat('%', @t, '%') OR ACC_ANAME LIKE Concat('%', @t, '%');", con);
				cmd.Parameters.Add(new SqlParameter("t", text));
				SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				DataTable tbl = new DataTable();
				adapter.Fill(tbl);
				customer_datagrid.DataSource = tbl.DefaultView;
			}
			catch (Exception ef)
			{
				MessageBox.Show("");
			}
		}

		private void customer_datagrid_DoubleClick(object sender, EventArgs e)
		{
			if (customer_datagrid.SelectedRows.Count == 0 || customer_datagrid.SelectedRows[0].DataBoundItem == null)
				return;
			DataRowView v = ((DataRowView)customer_datagrid.SelectedRows[0].DataBoundItem);
			customer_tb.Text = v["ACC_NO"].ToString();
			customer_name.Text = v[Languages.currentLanguage.ToLower() == "english" ? "ACC_NAME" : "ACC_ANAME"].ToString();
			ShowHideDGridView(false, customer_datagrid);
		}

		private void toolStrip_addButton_Click(object sender, EventArgs e)
		{
			SetFormState(FinOrgFormState.Reset);
			SetFormState(FinOrgFormState.New);
		}

		private void toolStrip_editButton_Click(object sender, EventArgs e)
		{
			SetFormState(FinOrgFormState.Edit);
		}

		private void toolStrip_saveButton_Click(object sender, EventArgs e)
		{
			SaveForm();
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
				if (!int.TryParse(code_tb.Text, out code))
				{
					MessageBox.Show("Invalid Code");
					return;
				}
				DeleteSubscription(code);
			}
			else
				MessageBox.Show("Invalid Action");
		}

		private void items_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != rate_DGColumn.Index) // not custom rate, fetch calculated rate
			{
				DataRowView rv = items.Rows[e.RowIndex].DataBoundItem as DataRowView;
				double rate = 0;
				try
				{
					rate = getServiceRate((int)rv["TR_SUBITEM_SERVICE_CODE"], (int)rv["TR_SUBITEM_PERIOD_CODE"], (int)rv["TR_SUBITEM_PRIORITY_CODE"]);
				}
				catch (Exception ef)
				{
					Console.WriteLine("Exception");
				}
				rv["TR_SUBITEM_RATE"] = rate;				
				items.UpdateCellValue(rate_DGColumn.Index, e.RowIndex);
			}
			CalculateTotals();
		}

		private void discount_advance_tb_Leave(object sender, EventArgs e)
		{
			CalculateTotals();
		}
	}
}
