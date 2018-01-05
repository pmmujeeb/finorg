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
	public partial class TrainingPeriod : FinOrgForm
	{

		public TrainingPeriod()
		{
			InitializeComponent();
		}

		public void SaveData()
		{
			if (dgv1.DataSource == null)
				return;

			dgv1.EndEdit();
			using (SqlConnection con = getSqlConnection())
			{
				SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TR_PERIOD;", con);
				SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
				dataAdapter.Update(((DataView)dgv1.DataSource).Table);
				MessageBox.Show(Languages.GetStringTranslation("Success"), "FinOrg Training Module");
			}
		}

		public void LoadData()
		{
			using (SqlConnection con = getSqlConnection()) {
				DataTable tbl = new DataTable("TR_PERIOD");
				SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TR_PERIOD;", con);
				dataAdapter.Fill(tbl);

				dgv1.DataSource = tbl.DefaultView;

				dgv1.Columns[0].HeaderText = Languages.GetStringTranslation("Code");				
				dgv1.Columns[1].HeaderText = Languages.GetStringTranslation("English Name");
				dgv1.Columns[2].HeaderText = Languages.GetStringTranslation("Arabic Name");
				dgv1.Columns[3].HeaderText = Languages.GetStringTranslation("Days");
			}
		}

		/// <summary>
		/// On Load Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TrainingPeriod_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			SaveData();
		}
	}
}
