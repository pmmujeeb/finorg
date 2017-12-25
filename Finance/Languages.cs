using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FinOrg
{
	public static class Languages
	{
		public static bool LANG_DEBUG_MODE = true;


		public static event EventHandler onLanguageChanged;
		// Set column name in TRANSLATIONS Table
		public static string currentLanguage = "English";

		// <DefaultValue, Translation>
		public static Dictionary<string, string> Translations;

		public static List<string> TranslationLoadedForms;

		//
		public static Thread Init()
		{
			Thread t = new Thread(() =>
			{
				SqlConnection con = FinOrgForm.getSqlConnection();
				try
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TRANSLATIONS';", con);
					if ((int)cmd.ExecuteScalar() == 0)
					{
						// create translations table
						cmd.CommandText = TRANSLATIONS_TABLE_SQL;
						cmd.ExecuteNonQuery();
					}
					if (LANG_DEBUG_MODE)
						LoadAllTranslations();
					else
						Translations = new Dictionary<string, string>();
					con.Close();
				}
				catch (Exception e)
				{
					con.Close();
					MessageBox.Show(e.Message, "FinOrg Languages Init");
				}
			});
			t.Start();
			return t;
		}


		private static void LoadAllTranslations()
		{
			SqlConnection con = FinOrgForm.getSqlConnection();
			con.Open();
			SqlCommand cmd = new SqlCommand(string.Format("SELECT text, {0} FROM TRANSLATIONS", currentLanguage), con);
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				Translations = new Dictionary<string, string>();
				while (reader.Read())
					Translations.Add(reader["text"].ToString(), reader[currentLanguage].ToString());
			}
			con.Close();
		}

		public static void ChangeLanguage(string newLanguage)
		{
			currentLanguage = newLanguage;
			if (LANG_DEBUG_MODE)
				LoadAllTranslations();
			else
				Translations = new Dictionary<string, string>();
			onLanguageChanged?.Invoke(null, null);
		}

		public static void ApplyTranslation(FinOrgForm f) {
			f.RightToLeftLayout = currentLanguage == "arabic";
			f.RightToLeft = currentLanguage == "arabic" ? RightToLeft.Yes : RightToLeft.No;
			foreach (Control c in f.GetAllControlChildren())
			{
				try
				{
					if (c.IsTranslatableControl())
						c.Text = Translations[f.ControlDefaultValues[c.Name]];

					// Apply on ToolStrips
					if (c.GetType().IsSubclassOf(typeof(ToolStrip)) || c.GetType() == typeof(ToolStrip))
					{
						foreach (ToolStripItem toolStripItem in ((ToolStrip)c).GetAllToolStripItems())
						{
							if (!string.IsNullOrEmpty(toolStripItem.Name))
								toolStripItem.Text = Translations[f.ControlDefaultValues[toolStripItem.Name]];
						}
					}

					// if Control is a type of DataGridView, iterate for DataGridViewColumn
					if (c.GetType() == typeof(DataGridView))
					{
						foreach (DataGridViewColumn col in ((DataGridView)c).Columns)
						{
							if (!string.IsNullOrEmpty(col.Name))
								col.HeaderText = Translations[f.ControlDefaultValues[col.Name]];
						}
					}

					// Apply RTL on the following Controls
					if (c.GetType() == typeof(Panel))
					{
						if (c.RightToLeft != f.RightToLeft)
							// panel type, rearrage children
							foreach (Control panel_child in c.Controls)
								panel_child.Location = new System.Drawing.Point(c.Size.Width - panel_child.Size.Width - panel_child.Location.X, panel_child.Location.Y);
						c.RightToLeft = f.RightToLeft;
					}
				} catch (Exception e)
				{
					MessageBox.Show(string.Format("{0}({4}) : {1} ({2})\nKey: {5}\nMessage: {3}", f.Name, c.Name, c.GetType(), e.Message, f.GetType(), "f.ControlDefaultValues[c.Name]"), "FinOrg Langauges ApplyTranslation");
				}
			}
		}

		/// <summary>
		/// Initialize Languaging Process for a form
		/// </summary>
		/// <param name="f"></param>
		public static void InitFormLanguage(FinOrgForm f)
		{
			f.ControlDefaultValues = new Dictionary<string, string>();

			foreach (Control c in f.GetAllControlChildren())
			{

				if (c.IsTranslatableControl() && !string.IsNullOrEmpty(c.Name))
				{
					f.ControlDefaultValues.Add(c.Name, c.Text.Simplified(true));
					c.AutoSize = false;
				}

				// if Control is a type of ToolStrip, iterate for ToolStripItem
				if (c.GetType().IsSubclassOf(typeof(ToolStrip)) || c.GetType() == typeof(ToolStrip)) {
					foreach (ToolStripItem toolStripItem in ((ToolStrip)c).GetAllToolStripItems())
					{
						if (!string.IsNullOrEmpty(toolStripItem.Name))
							f.ControlDefaultValues.Add(toolStripItem.Name, toolStripItem.Text.Simplified(true));
					}
				}

				// if Control is a type of DataGridView, iterate for DataGridViewColumn
				if (c.GetType() == typeof(DataGridView))
				{
					foreach (DataGridViewColumn col in ((DataGridView)c).Columns)
					{
						if (!string.IsNullOrEmpty(col.Name))
							f.ControlDefaultValues.Add(col.Name, col.HeaderText.Simplified(true));
					}
				}
			}

			// ControlDefaultValues loaded
			// LazyLoad these in Languages
			Languages.LazyLoadTranslations(f);
		}

		public static string GetStringTranslation(string s)
		{
			if (!Translations.ContainsKey(s))
			{
				SqlConnection con = FinOrgForm.getSqlConnection();
				SqlCommand cmd = new SqlCommand("", con);
				try
				{
					con.Open();
					// Insert to DB
					if (LANG_DEBUG_MODE)
					{
						cmd.CommandText = "INSERT INTO TRANSLATIONS (text, english) VALUES (@v);";
						cmd.Parameters.Add(new SqlParameter("v", s));
						cmd.ExecuteNonQuery();
						cmd.Parameters.Clear();
					}
					// fetch from DB
					cmd.CommandText = string.Format("SELECT {0} FROM TRANSLATIONS WHERE text = @v", currentLanguage);
					cmd.Parameters.Add(new SqlParameter("v", s));
					object data = cmd.ExecuteScalar();
					con.Close();
					if (data != null)
						return data.ToString();
					else
						return "";
				} catch (Exception ef)
				{
					MessageBox.Show(ef.Message + "\nSQL: " + cmd.CommandText, "FinOrg Languages GetTranslation");
					return "";
				}
			}
			else
				return Translations[s];
		}

		/// <summary>
		/// Lazy Loads the text for each Control in form
		/// </summary>
		/// <param name="ControlDefaultValues">A dictionary with ControlName-Text pairs</param>
		public static Thread LazyLoadTranslations(FinOrgForm f)
		{
			Thread t = new Thread(() =>
			{
				// Get a list of items to fetch
				IEnumerable<string> items = f.ControlDefaultValues.Values;
				if (Translations != null)
					items = items.Except(Translations.Keys).ToArray();
				SqlConnection con = FinOrgForm.getSqlConnection();
				try
				{
					con.Open();
					SqlCommand cmd = new SqlCommand(string.Format("SELECT text, {0} FROM TRANSLATIONS WHERE text IN ({{keys}});", currentLanguage), con);
					cmd.AddArrayParameters(items, "keys");
					if (cmd.Parameters.Count > 0)
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while(reader.Read())
							{
								// Only Keys NOT Present in Translations Dictionary is fetched from Database
								Translations.Add(reader["text"].ToString(), reader[currentLanguage].ToString());
							}
						}
					con.Close();
				}
				catch (Exception e)
				{
					con.Close();
					System.Windows.MessageBox.Show(e.Message, "FinOrg Languages LazyLoadTranslations");
				}
				if (LANG_DEBUG_MODE) // Copies the value from form to database currentLanguage field
					InsertFormTranslations(f.ControlDefaultValues);

				// Apply Translation
				f.BeginInvoke(new Action(() =>
				{
					ApplyTranslation(f);
				}));
			});
			// finish  this fast
			t.Priority = ThreadPriority.Highest;
			t.Start();
			return t;
		}


		// Returns true if Control is translatable
		public static bool IsTranslatableControl(this Control ctrl)
		{
			return new Type[] {
				typeof(Label),
				typeof(Button),
				typeof(CheckBox),
				typeof(RadioButton),
				typeof(ToolStripMenuItem),
				typeof(ToolStripButton),
				typeof(ToolStrip),
				typeof(DataGridView)
			}.Contains(ctrl.GetType());
		}


		/// <summary>
		/// Copies values
		/// Call from a thread, not from UI
		/// </summary>
		/// <param name="ControlDefaultValues"></param>
		public static void InsertFormTranslations(Dictionary<string, string> ControlDefaultValues)
		{
			if (ControlDefaultValues.Count == 0)
				return;
			SqlConnection con = FinOrgForm.getSqlConnection();
			SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO TRANSLATIONS (text, {0}) VALUES ", currentLanguage), con);
			try
			{
				con.Open();
				int i = 0; // dictionary doesnt have proper index to loop with forloop
				foreach (KeyValuePair<string, string> e in ControlDefaultValues)
				{
					// check for duplications
					if (Translations.ContainsKey(e.Value.Simplified()))
						continue;
					if (i > 0)
						cmd.CommandText += ", ";
					cmd.CommandText += string.Format("(@text{0}, @value{0})", i);
					cmd.Parameters.Add(new SqlParameter("@text" + i, e.Value.Simplified()));
					cmd.Parameters.Add(new SqlParameter("@value" + i, e.Value.Simplified()));
					Translations.Add(e.Value.Simplified(), e.Value.Simplified());
					i++;
				}
				if (cmd.Parameters.Count > 0)
					cmd.ExecuteNonQuery();
				con.Close();
			} catch (Exception e)
			{
				con.Close();
				MessageBox.Show(e.Message + "\n" + cmd.CommandText, "FinOrg: Languages InsertFormTranslations");
			}
		}

		public static string TRANSLATIONS_TABLE_SQL = @"USE [Finance]
														GO

														/****** Object:  Table [dbo].[TRANSLATIONS]    Script Date: 20-Dec-17 3:06:26 PM ******/
														SET ANSI_NULLS ON
														GO

														SET QUOTED_IDENTIFIER ON
														GO

														CREATE TABLE [dbo].[TRANSLATIONS](
															[text] [nvarchar](200) NOT NULL,
															[english] [nvarchar](200) NULL,
															[arabic] [nvarchar](200) NULL,
														 CONSTRAINT [IX_TRANSLATIONS] UNIQUE NONCLUSTERED
														(
															[text] ASC
														)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
														) ON [PRIMARY]

														GO";
	}
}
