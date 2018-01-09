using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
	public class FinOrgForm : Form
	{
		private FinOrgFormState _currentState;
		protected FinOrgFormState CurrentState { get { return _currentState; } }
		protected Dictionary<FinOrgFormState, object[]> FormStateControlVisibility { get; set; }
		protected enum FinOrgFormState
		{
			View,
			Edit,
			New,
			Reset
		}

		public FinOrgForm()
		{
			FormStateControlVisibility = new Dictionary<FinOrgFormState, object[]>();
		}

		protected virtual void SetFormState(FinOrgFormState state)
		{
			_currentState = state;

			Type type;
			PropertyInfo prop;			
			foreach (FinOrgFormState st in FormStateControlVisibility.Keys)
			{
				if (state == st)
					continue;

				foreach (Object o in FormStateControlVisibility[st])
				{
					type = o.GetType();
					prop = type.GetProperty("Visible");
					if (prop != null && prop.CanWrite)
					{
						prop.SetValue(o, false, null);
					}
				}
			}

			foreach (Object o in FormStateControlVisibility[state])
			{
				type = o.GetType();
				prop = type.GetProperty("Visible");
				if (prop != null && prop.CanWrite)
				{
					prop.SetValue(o, true, null);
				}
			}
		}

		// <ControlName, DefaultValue>
		public Dictionary<string, string> ControlDefaultValues { get; set; }

        public virtual void LanguageChanged()
        {

        }

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
            LanguageChanged();
		}

		public static SqlConnection getSqlConnection()
		{
			return new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
		}
	}
}
