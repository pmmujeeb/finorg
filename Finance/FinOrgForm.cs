using System;
using System.Collections.Generic;
using System.ComponentModel;
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
			if (!DesignMode)
			{ // Necessary
				Languages.InitFormLanguage(this);
			}
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (!DesignMode)
			{
				Languages.onLanguageChanged -= this.onLanguageChanged;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!DesignMode)
			{
				Languages.onLanguageChanged += this.onLanguageChanged;
			}
		}

		private void onLanguageChanged(object sender, EventArgs e)
		{
			Languages.LazyLoadTranslations(this);
		}

		public static SqlConnection getSqlConnection()
		{
			return new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
		}
	}
}
