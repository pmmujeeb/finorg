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
            InitLanguage();
        }

        protected void InitLanguage()
        {
            ControlDefaultValues = new Dictionary<string, string>();

			var controls = new Queue<Control>();
			controls.Enqueue(this);
			do {
				Control t = controls.Dequeue();
				if (Languages.IsTranslatableControl(t))
					ControlDefaultValues.Add(t.Name, t.Text);

				foreach (Control c in t.Controls)
					controls.Enqueue(c);

			} while (controls.Count > 0);

            // ControlDefaultValues loaded
            // LazyLoad these in Languages
            Languages.LazyLoadTranslations(ControlDefaultValues);
        }

        public static SqlConnection getSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        }
    }
}
