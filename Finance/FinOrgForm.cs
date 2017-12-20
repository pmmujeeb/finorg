using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
	public class FinOrgForm : Form
	{

		// <ControlName, DefaultValue>
		public Dictionary<string, string> ControlDefaultValues { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!DesignMode) // Necessary
				Languages.InitFormLanguage(this);
		}

		public static SqlConnection getSqlConnection()
		{
			return new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
		}
	}
}
