using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.VisualBasic;
using System.Threading;
using CrystalDecisions.CrystalReports.Engine;

namespace FinOrg
{
    public partial class Frmlogin : FinOrgForm
    {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        string sql;
        public Frmlogin()
        {
            InitializeComponent();
            panel1.Left = panel2.Left; ;
        }

        private void cmdcancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void cmdok_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    Conn.Close();
                    Conn.Open();



                    if (txtpass.Text.Trim() == "")
                    {
                        txtpass.Focus();
                        return;
                    }


                    sql = "sELECT  * from Userinfo where userName='" + txtuser.Text.Trim() + "' and Password='" + txtpass.Text.Trim() + "'";


                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader rd = cmd.ExecuteReader();
                    bool fnd;
                    fnd = false;
                    while (rd.Read())
                    {
                        fnd = true;
                       


                        if (!string.IsNullOrEmpty(rd["superuser"].ToString()))
                        {
                            Gvar._SuperUserid = (Convert.ToInt32(rd["superuser"]));
                        }

                        if (!string.IsNullOrEmpty(rd["userid"].ToString()))
                        {
                           // Gvar._Userid =Convert.ToInt32( rd["userid"].ToString());
                        }
                        Gvar.wr_code = Convert.ToInt32(rd["wr_code"]);
                        Gvar.menu_dock = rd["Menu_dock"].ToString(); ;

                        Gvar.brn_code = Convert.ToInt32(rd["brn_code"]);
                        //Gvar.wr_code = 1;

                       
                       
                        Gvar.pos_user = 0;
                        Gvar.orgdup = "ORG";
                        Gvar._currency = "SR";
                        Gvar._cur_rate = 1;
                        if (Gvar._SuperUserid != 1)
                        {



                            //saveToolStripButton.Enabled = false;


                        }
                        Gvar.Userid = Convert.ToInt32(rd["userid"].ToString());
                       Gvar. report_path = Application.StartupPath;
                    }

                 Gvar.sale_acno = "1";
                 Gvar.pur_acno = "1000";

                 sql = "select acc_no,acc_name,cash_sale_ac,cash_pur_ac from accounts inner join ac_options on acc_no=cash_sale_ac and   ac_options.ID =1";

                   rd.Close();
                     cmd = new SqlCommand(sql, Conn);
                     rd = cmd.ExecuteReader();
                   
                    while (rd.Read())
                    {
                        
                            Gvar.sale_acno = rd[2].ToString();
                            Gvar.pur_acno = rd[3].ToString();
                        
                    }
                        
                    Conn.Close();

                    if (fnd == false)
                    {
                        MessageBox.Show("Invalid User Name Or Password!!!!", "Wrong Authentication");
                        return;
                    }

                   


                    Form childForm = new MDIParent1();
            //childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Financial Management System- FinOrg";
            this.Hide();      
            childForm.Show();

            Thread thread = new Thread(new ThreadStart(load_crt));
            thread.Start();

                    
                    ///Application.EnableVisualStyles();
                    /////Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new MDIParent1());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void load_crt()
        {
            try
            {

            
            string rep_path;
          


                

                ReportDocument CrRep = new ReportDocument();


                rep_path = Application.StartupPath + "\\reports\\RptTest.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;

                CrRep.Close();
            
            }

                
          
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmdchange_Click(object sender, EventArgs e)
        {
            try
            {
                
                    Conn.Close();
                    Conn.Open();

                    



                    sql = "sELECT  * from Userinfo where userName='" + txtuser.Text.Trim() + "' and Password='" + txtpass.Text.Trim() + "'";


                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader rd = cmd.ExecuteReader();
                    bool fnd;
                    fnd = false;
                    while (rd.Read())
                    {
                        fnd = true;
                       


                        if (!string.IsNullOrEmpty(rd["superuser"].ToString()))
                        {
                            Gvar._SuperUserid = (Convert.ToInt32(rd["superuser"]));
                        }

                        if (!string.IsNullOrEmpty(rd["userid"].ToString()))
                        {
                           // Gvar._Userid =Convert.ToInt32( rd["userid"].ToString());
                        }

                        Gvar.brn_code = 1;
                        if (Gvar._SuperUserid != 1)
                        {



                            //saveToolStripButton.Enabled = false;


                        }
                        Gvar.Userid = Convert.ToInt32(rd["userid"].ToString());
                       Gvar. report_path = Application.StartupPath;
                    }

                    Conn.Close();

                    if (fnd == false)
                    {
                        MessageBox.Show("Invalid User Name Or Password!!!!", "Wrong Authentication");
                        return;
                    }

                    panel3.Visible = true;
                    panel2.Visible = false;
                }
                catch
            {
                }

        }

        private void cmdpasscancel_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = true;
        }

        private void cmdpass_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtnewpass1.Text != txtnewpass2.Text)
                {
                    MessageBox.Show("NEW Password does not Match !!!", "Invalid Password");
                    return;
                }
                
                Conn.Close();
                Conn.Open();
                
                sql = "update  Userinfo set Password='" + txtnewpass1.Text.Trim() + "' where userName='" + txtuser.Text.Trim() + "'";


                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("NEW Password Changed Successfully !!!", "New Password Changed");
                panel3.Visible = false;
                panel2.Visible = true;
                txtpass.Text = "";
            }
            catch
            {
                MessageBox.Show("NEW Password does not Changed, Error Occured ", "Error Password Changed");
            }

        }

        private void btnlang_Click(object sender, EventArgs e)
        {
            try
            {
				if (Languages.currentLanguage.Simplified(true) == "english")
				{
					Languages.ChangeLanguage("arabic");
				}
				else
				{
					Languages.ChangeLanguage("english");
				}
			}
            catch
            {

            }
        }

    }
}
