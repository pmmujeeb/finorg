using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FinOrg
{
    public static class Languages
    {
        public static bool COPY_KEY_VALUES = true;

        // Set column name in TRANSLATIONS Table
        public static string currentLanguage = "english";        

        // <DefaultValue, Translation>
        public static Dictionary<string, string> Translations;

        public static List<string> TranslationLoadedForms;

        // 
        public static void Init()
        {
            new Thread(() => {
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

                    con.Close();
                } catch (Exception e)
                {                    
                    con.Close();
                    System.Windows.MessageBox.Show(e.Message);
                    System.Windows.Application.Current.Shutdown();
                } 
            });   
        }

        /// <summary>
        /// Lazy Loads the text for each form
        /// </summary>
        /// <param name="ControlDefaultValues">A dictionary with ControlName-Text pairs</param>
        public static Thread LazyLoadTranslations(Dictionary<string, string> ControlDefaultValues)
        {            
            Thread t = new Thread(() => 
            {
                // Get a list of items to fetch
                string[] items = ControlDefaultValues.Values.Except(Translations.Keys).ToArray();
                SqlConnection con = FinOrgForm.getSqlConnection();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("SELECT text, {0} FROM TRANSLATIONS WHERE text IN ({keys});", currentLanguage), con);
                    cmd.AddArrayParameters(items, "keys");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Translations.Add(reader["text"].ToString(), reader[currentLanguage].ToString());
                        }
                    }
                    con.Close();
                }
                catch (Exception e)
                {
                    con.Close();
                    System.Windows.MessageBox.Show(e.Message);                                        
                }
            });
            // finish  this fast
            t.Priority = ThreadPriority.Highest;
            t.Start();
            return t;
        }


        // Returns true if Control is translatable
        public static bool IsTranslatableControl(Control ctrl)
        {
            return new Type[] {
                typeof(Label),
                typeof(Button),
                typeof(CheckBox),
                typeof(RadioButton),
                typeof(MenuItem)
            }.Contains(ctrl.GetType());            
        }


        public static string TRANSLATIONS_TABLE_SQL = @"USE [Finance]
                                                        GO

                                                        /****** Object:  Table [dbo].[TRANSLATIONS]    Script Date: 20-Dec-17 7:39:29 AM ******/
                                                        SET ANSI_NULLS ON
                                                        GO

                                                        SET QUOTED_IDENTIFIER ON
                                                        GO

                                                        CREATE TABLE [dbo].[TRANSLATIONS](
	                                                        [text] [nvarchar](200) NOT NULL,
	                                                        [english] [nvarchar](200) NULL,
	                                                        [arabic] [nvarchar](200) NULL,
                                                         CONSTRAINT [PK_TRANSLATIONS] PRIMARY KEY CLUSTERED 
                                                        (
	                                                        [text] ASC
                                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                                        ) ON [PRIMARY]

                                                        GO";
    }
}
