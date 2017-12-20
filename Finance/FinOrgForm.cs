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
				InitLanguage();
		}

		protected void InitLanguage()
		{
			ControlDefaultValues = new Dictionary<string, string>();

			foreach (Control c in this.GetAllChildren())
				if (c.IsTranslatableControl())
					ControlDefaultValues.Add(c.Name, c.Text);

			// ControlDefaultValues loaded
			// LazyLoad these in Languages
			Languages.LazyLoadTranslations(this);
		}

		public static SqlConnection getSqlConnection()
		{
			return new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
		}
	}
}
