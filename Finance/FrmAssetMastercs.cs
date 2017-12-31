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
using CrystalDecisions.CrystalReports.Engine;
using ADODB;
using System.Drawing.Imaging;
namespace FinOrg
{

    public partial class FrmAssetMaster : FinOrgForm
    {


        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlDataReader rd;
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();

         int dblclk_row;
        int last_row;
        int last_col;
        DataTable itemdt = new DataTable();
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        int first_grdrow;
        int last_grdrow;
        bool iserror;
        bool issearch;
        string acntrl = "";
        public FrmAssetMaster()
        {
            InitializeComponent();
            
            txtpriv.Text = Gvar.frm_priv.ToString();
            ClearTextBoxes(this);
            isini = true;
            
            Load_data();
           
            grditem.Visible = false;
            grdbutton.Rows.Add(1);
           
            first_grdrow=0;
            last_grdrow=0;
           
            isini = false;
            
           

        }



        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (!Program.session_valid(dtpayment.Value.Date.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("There is no valid Finance Session Found, Please check the Entry Date or Contact Admin  ", "Invalid Transaction Date ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            save_data();
        }


    

      




        private void Load_data()
        {
            try
            {
                Conn.Close();
                Conn.Open();
                saveToolStripButton.Enabled = true;
                toolRefund.Enabled = true;
                string sqlunt;
                string sqlcat;
                sql = "sELECT   Asset_Code,DESCRIPTION,AST_CAT_CODE,ASSET_COST from ASSET_MASTER";
                sqlcat = "sELECT  AST_cat_code,AST_CAT_name froM ASSET_CAT where AST_cat_code<>0 ";
               
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

             aditem = new SqlDataAdapter(cmd);
            
                 itemdt = new DataTable("ASETMASTER");
                aditem.Fill(itemdt);
                //grditem.Visible = true;
                dv.Table = itemdt;

                grditem.DataSource = dv;
                grditem.Columns[1].Width = 300;
                grditem.Columns[0].Width = 175;

                dteffectdate.Value = DateTime.Now.Date;
                dtpayment.Value = DateTime.Now.Date;


                sql = "sELECT  acc_no,acc_name froM accounts inner join ac_options on accounts.LEVEL3_NO=ac_options.CASH_ac_type  and   ac_options.ID =1 and acc_level=4";

                SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                DataTable dt1 = new DataTable("accounts");
                ada1.Fill(dt1);
                cmbpaidac.DataSource = dt1;
                cmbpaidac.DisplayMember = "acc_name";
                cmbpaidac.ValueMember = "acc_no";
                
               


                sql = "sELECT  AST_cat_code,AST_CAT_name froM ASSET_CAT where AST_cat_code<>0 ";
               
                SqlDataAdapter ada2 = new SqlDataAdapter(sqlcat, Conn);
                DataTable dt2 = new DataTable("ASSET_CAT");
                ada2.Fill(dt2);
                cmbcat.DisplayMember = "AST_CAT_name";
                cmbcat.ValueMember="AST_cat_code";
                cmbcat.DataSource = dt2;

                sql = "select Branch_code,Branch_Name from BRANCHES ";

                SqlDataAdapter adabrn = new SqlDataAdapter(sql, Conn);
                DataTable dtbrn = new DataTable("branches");
                adabrn.Fill(dtbrn);

                cmbbranch.DisplayMember = "Branch_Name";
                cmbbranch.ValueMember = "Branch_code";

                cmbbranch.DataSource = dtbrn;
                cmbbranch.SelectedIndex = 0;

                if (cmbbranch.Items.Count<2)
                {
                    cmbbranch.Visible = false;
                    lblbranch.Visible = false;
                }
               


                Conn.Close();
            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }


        }

        private void print_Report(int idx)
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


              


                ReportDocument CrRep = new ReportDocument();
                rep_path = "";
                
                crt="";
                switch (idx)
                {
                    case 1:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";
                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report for all";
                        }


                        break;


                    case 2:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterbywh.rpt";
                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report By WareHouse for all";
                        }


                        break;

                    case 3:
                        {

                            crt = "{HD_ITEMMASTER.ITM_CAT_CODE}  =" + cmbcat.SelectedValue; 
                            // Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";
                              
                            CrRep.Load(rep_path);
                           
                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Category " + cmbcat.Text ;
                        }


                        break;


                    case 4:
                        {

                            crt = "{STOCK_MASTER.STOCK} <= {STOCK_MASTER.RE_ORDER} ";
                            // Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";

                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Re-Order Items" ;
                        }


                        break;
                }

                //crconnectioninfo.ServerName = "Mujeeb";
                //crconnectioninfo.DatabaseName = "Printex";
                //crconnectioninfo.UserID = "sa";
                //crconnectioninfo.Password = "sa0101";

                

                

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                if (crt != "")
                    {
                        CrRep.RecordSelectionFormula = crt;
                    }


                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName =   decoder.DataSource;
               
                crconnectioninfo.DatabaseName = decoder.InitialCatalog;
                crconnectioninfo.UserID = decoder.UserID;
                crconnectioninfo.Password = decoder.Password;



                CrTables = CrRep.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtablelogoninfo = CrTable.LogOnInfo;
                    crtablelogoninfo.ConnectionInfo = crconnectioninfo;
                    CrTable.ApplyLogOnInfo(crtablelogoninfo);
                }



                //CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);

                //CrRep.Load();
                //CrRep.ReadRecords();

                //CrRep.Refresh();

                //if (chkprinter.Checked)
                //{


                //    CrRep.PrintToPrinter(1, true, 0, 0);
                //}
                //else
                //{
                    FrmrepView frm = new FrmrepView();
                    frm.MdiParent = this.ParentForm;

                    frm.crv1.ReportSource = CrRep;
                    frm.Show();
                //}










                //ConnectionInfo connInfo = new ConnectionInfo();
                //connInfo.ServerName = "dbservername";
                //connInfo.DatabaseName = "dbname";
                //connInfo.UserID = "dbusername";
                //connInfo.Password = "dbpassword";
                //reportViewer.ReportSource = GetReportSource(connInfo);






                CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, "Mujeeb", decoder.InitialCatalog);

                //CrRep.Load();
                // CrRep.ReadRecords();

                //CrRep.Refresh();


            }
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }


        }
        private void save_data()
        {

            try
            {

                bool isempty;
                isempty = false;
                // foreach (Control tx in this.Controls)
                //foreach (TextBox tx in this.Controls.OfType<TextBox>())
                //{
                //    //if (tx.GetType == System.Windows.Forms.TextBox) 
                //   //{
                //        if (tx.Tag == "1")
                //        {

                //            tx.BackColor = System.Drawing.Color.White;
                //            if (string.IsNullOrEmpty(tx.Text.Trim()))
                //            {
                //                tx.BackColor = System.Drawing.Color.Yellow;
                //                isempty = true;
                //            }
                //        }

                //   //}
                //}

                if (isedit)
                {
                    if (txtpriv.Text.Substring(1, 1) == "0")
                    {
                        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
                        return;
                    }
                }
                else
                {

                    if (txtpriv.Text.Substring(0, 1) == "0")
                    {
                        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
                        return;
                    }
                }




                foreach (Control gb in this.Controls)
                {
                    if (gb is GroupBox)
                    {
                        foreach (Control tb in gb.Controls)
                        {
                            if (tb is TextBox)
                            {
                                if (tb.Tag == "1")
                                {

                                    tb.BackColor = System.Drawing.Color.White;
                                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                                    {
                                        tb.BackColor = System.Drawing.Color.Yellow;
                                        isempty = true;
                                    }
                                }
                            }
                        }
                    }
                }



                if (isempty)
                {
                    MessageBox.Show("Entry Not Completed, Please fill all Yellow Marked fileds!!", "Invalid Entry");
                    return;

                }





                if (cmbcat.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Asset Item Category", "Wrong Asset Category");

                    return;
                }


                if (cmbpaidac.SelectedIndex < 0 && !chkOb.Checked)
                {
                    MessageBox.Show("Invalid Asset Cost Paid from Account", "Invalid Unknown Paid selection");

                    return;
                }

                if (cmbpaidac.SelectedIndex > 0 && chkOb.Checked)
                {
                    MessageBox.Show("Invalid Asset Cost Paid from Account", "Invalid Unknown Paid selection");

                    return;
                }


                if (string.IsNullOrEmpty(Txtitem.Text.ToString()))
                {
                    MessageBox.Show("Invalid Asset Item name", "Wrong Asset Name");
                    return;
                }

                fnd = false;


                if (txtcost.Text.Trim() == "") txtcost.Text = "0";


                if (txtbarcode.Text.Trim() == "") txtbarcode.Text = Txtitem.Text;

                if (txtaname.Text.Trim() == "") txtaname.Text = txtname.Text;
                if (txtdeppcnt.Text.Trim() == "") txtdeppcnt.Text = "0";

                if (txtdeppcnt.Text.Trim() == "")
                    txtdeppcnt.Text = "0";

                iserror = false;

                try
                {
                    ADODB.Connection ADOconn = new ADODB.Connection();

                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                    ADODB.Recordset TMP = new ADODB.Recordset();

                    sql = "select * from asset_master where asset_code='" + Txtitem.Text + "'";
                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (TMP.RecordCount == 0)
                    {
                        TMP.AddNew();

                    }

                    TMP.Fields["ASSET_CODE"].Value = Txtitem.Text;
                    TMP.Fields["DESCRIPTION"].Value = txtname.Text;
                    TMP.Fields["USER_ID"].Value = Gvar.Userid;
                    TMP.Fields["UNIT"].Value = "PCS";
                    TMP.Fields["FLAG"].Value = "N";
                    TMP.Fields["AST_cAT_CODE"].Value = cmbcat.SelectedValue;
                    TMP.Fields["PART_NO"].Value = txtpartno.Text;
                    //TMP.Fields["BRAND"].Value = TXTB;
                    //TMP.Fields["ALIAS_NAME"].Value = Gvar.brn_code;
                    TMP.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    TMP.Fields["AR_DESC"].Value = txtaname.Text;
                    TMP.Fields["BARCODE"].Value = txtbarcode.Text;
                    TMP.Fields["ASSET_COST"].Value = txtcost.Text;
                    TMP.Fields["YEAR_DEPRECATION"].Value = txtdeppcnt.Text;
                    TMP.Fields["SUPLIER_NAME"].Value = txtsuplier.Text;
                    TMP.Fields["WARRENTY_EXPIRE_DATE"].Value = dtwarrentyDate.Value.Date.ToString("yyyy-MM-dd");
                    TMP.Fields["Asset_Effect_Date"].Value = dteffectdate.Value.Date.ToString("yyyy-MM-dd");
                    TMP.Fields["REMARKS"].Value = txtremarks.Text;
                    if (!chkOb.Checked)
                        TMP.Fields["PAID_AC"].Value = cmbpaidac.SelectedValue;
                    else
                        TMP.Fields["PAID_AC"].Value = 0;

                    TMP.Update();

                    updat_accounts(Txtitem.Text, 0);
                    isedit = true;


                  
                    MessageBox.Show("Successfully Added/Updated Asset", "Successfull");
                    Load_data();
                    return;
                }
                catch (Exception ex)
                {

                     MessageBox.Show("There is Some Error!! detected while saving , Please check and Try Again", "UnSuccessfull");
                }
            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }

        }

        
        private void delete_Item()
        {

            try
            {

                double val1;

                //if (double.TryParse(Txtitem.Text, out val1))
                //{
                //    if (Convert.ToDouble(Txtitem.Text) < 1)
                //    {
                //        Txtitem.Text = "";
                //        MessageBox.Show("Invalid Asset Code, PLease Try Again", "Plese Enter Correct Value");
                //        Conn.Close();
                //        return;
                //    }
                //}
                //else
                //{
                //    Txtitem.Text = "";
                //    MessageBox.Show("Invalid Item Code, Please Try Again", "Plese Enter Correct Value");
                //    Conn.Close();
                //    return;

                //}

                DialogResult result = MessageBox.Show("Do You want to Delete The Asset Item " + txtolditm.Text + "?", "Delete Item", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    Conn.Close();
                    Conn.Open();


                    sql = "delete from   [ASSET_MASTER]  where ASSET_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();



                    sql = "delete from   [ASSET_MASTER]  where ASSET_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Delete Item Completed Successfully!!");
                    grditem.Refresh();

                }


            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);


            }
        }

        private void ini_form()
        {
           
            if (cmbcat.Items.Count > 0) cmbcat.SelectedIndex = 0;
            

            
            ClearTextBoxes(this);
            textBox1.Text = "";
            toolRefund.Enabled = true;
            dtwarrentyDate.Value = DateTime.Now;

            Gvar.ArCalendar(dtwarrentyDate.Value);
            saveToolStripButton.Enabled = true;
            if (Gvar._SuperUserid != 1)
            {
                toolRefund.Enabled = false;
                tooldelete.Enabled = false;

            }

            chkOb.Checked = false;
            cmbpaidac.SelectedIndex = -1;
            
               
            isedit = false;
            isini = true;
            pictureBox1.Image = null;
         
            isini = false;
            

        }
        private void Select_grid()
        {
            try
            {
                switch (grditem.Tag.ToString())
                {
                    case "id":
                        {


                        }


                        break;
                    case "sno":
                        {



                        }

                        break;
                    case "rno":
                        {
                            grditem.Visible = false;
                            Txtitem.Text = grditem.CurrentRow.Cells[0].Value.ToString();


                            // txtid.Text = dgv1.Rows[dgv1.SelectedRows[1].Index].Cells[1].Value.ToString();


                            Load_data();

                        }

                        break;


                }
            }

            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }
        }

        public void ClearTextBoxes(Control parent)
        {
           
            foreach (Control child in parent.Controls)
            {
                TextBox textBox = child as TextBox;
                if (textBox == null)
                    ClearTextBoxes(child);
                else
                    if (textBox.Name == "txtpriv")
                    { 
                    }

                    else
                        if (textBox.Name != "textBox1")
                        textBox.Text = string.Empty;
            }
          
          
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            issearch = true;
            Load_data();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            issearch = true;
            ini_form();
            textBox1.Focus();
        }
        
        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            
            issearch = false;
            
                if (Txtitem.Text.Trim() != "" )
                    search_data(Txtitem.Text);
            
           
        }

       
        private void tooldelete_Click(object sender, EventArgs e)
        {
            delete_Item();
            ini_form();
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();

            this.Close();
        }

        
       

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            print_Report(1);

        }

        private void Frmentry_Load_1(object sender, EventArgs e)
        {
            ini_form();
            if (Gvar.invno != "0")
            {
                Txtitem.Text = Gvar.invno;
                textBox1.Text = Txtitem.Text;
                search_data(textBox1.Text.Trim());   
            }
           
            acntrl = "";
        }



        private void search_data(string Item_Code)
        {
            try
            {

                ini_form();
                Txtitem.Text = Item_Code;
                
                saveToolStripButton.Enabled = true;
                ADODB.Connection ADOconn = new ADODB.Connection();
                if(ADOconn.State==0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
           
                 ADODB.Recordset TMP = new ADODB.Recordset();
                
                sql = "select * from asset_master where asset_code='" + Txtitem.Text + "'";
                TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if(TMP.RecordCount>0)
                {
                   txtolditm.Text=Txtitem.Text; 

               
                txtname.Text= TMP.Fields["DESCRIPTION"].Value.ToString() ;
                 txtpartno.Text= TMP.Fields["PART_NO"].Value.ToString() ;
                
               
                 txtaname.Text=TMP.Fields["AR_DESC"].Value.ToString() ;
                txtbarcode.Text =TMP.Fields["BARCODE"].Value.ToString()  ;
                txtcost.Text=TMP.Fields["ASSET_COST"].Value.ToString() ;
                txtdeppcnt.Text=TMP.Fields["YEAR_DEPRECATION"].Value.ToString() ;
                txtsuplier.Text=TMP.Fields["SUPLIER_NAME"].Value.ToString() ;
                dtwarrentyDate.Value = Convert.ToDateTime( TMP.Fields["WARRENTY_EXPIRE_DATE"].Value);
                 dteffectdate.Value=Convert.ToDateTime( TMP.Fields["Asset_Effect_Date"].Value) ;
                 dtpayment.Value = DateTime.Now.Date;
                  txtremarks.Text =TMP.Fields["REMARKS"].Value.ToString();
                 if (!chkOb.Checked)
                     TMP.Fields["PAID_AC"].Value = cmbpaidac.SelectedValue;
                 else
                   if( TMP.Fields["PAID_AC"].Value == "0")
                   {
                       chkOb.Checked=true;
                       cmbpaidac.SelectedIndex=-1;

                   }

                   else
                   {
                       cmbpaidac.SelectedValue = TMP.Fields["PAID_AC"].Value;
                       chkOb.Checked = false;
                   }


                 find_currevalue();

                 TMP = new ADODB.Recordset();

                 sql = "select Pay_Date, Dr_CR,Pay_Amount, Narration,Acc_no from Trn_accounts  WHERE  DOC_NO='" + Txtitem.Text.Trim() + "' AND VOUCHER_NO='ASSET'";
                 //TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                 SqlDataAdapter adac = new SqlDataAdapter(sql, Conn);
                 //dt = new DataTable("Accounts");
                 DataTable acdv = new DataTable();

                 adac.Fill(acdv);
                 //acdv.Table = dt;

                 dgac.DataSource = acdv;
                    if (dgac.Rows.Count>0)
                    {
                        dgac.Columns[1].Width=50;
                        dgac.Columns[3].Width = 200;


                    }
                
                 dgac.Visible = true;

                isedit = true;


                        isedit = true;
                        //Txtitem.Focus();
                    }

                else
                {
                    return;
                }




                search_foto();
                
            }
            //}

            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }
        }



         

          
            private void search_foto()
            {
                try
                {
                    if (string.IsNullOrEmpty(Txtitem.Text)) return;



                    SqlCommand cmd1;
                   
                    System.IO.MemoryStream ms;
                    
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConImage"].ConnectionString);

                    con.Open();


                    sql = "SELECT *  from Item_image where barcode='" + Txtitem.Text.Trim() + "'";



                     cmd1 = new SqlCommand(sql, con);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                  
                  
                    while (rd1.Read())
                    {
                       if (rd1["photo"] != DBNull.Value)
                       {
                           showdata((byte[]) rd1["photo"]);

                       }

                    }
                    rd1.Close();
                    
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                string txt = textBox1.Text.Trim();
                if (txt != "")
                {
                    isini = true;

                    if (Txtitem.Text.Trim() != "")
                    {
                        ini_form();
                        textBox1.Text = txt;
                    }
                    dv.RowFilter = "asset_Code LIKE  '%" + txt + "%' OR description LIKE '%" + txt + "%'";
                    isini = false;
                    // if (!issearch && dv.Count >0 ) search_data(grditem["Item_Code", 0].Value.ToString());
                }
                else
                    dv.RowFilter = "asset_Code <> '0'";

                grditem.Visible = true;
                grditem.Top = textBox1.Top + textBox1.Height;
                grditem.Left = textBox1.Left;
                isini = false;
            }

            private void textBox1_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyValue)
                {
                    case 17:
                     
                        break;
                    case 27:
                        //dgv1.Visible = false;
                        break;
                    case 13:
                        if (grditem.Visible)
                        textBox1.Text = grditem.CurrentRow.Cells[0].Value.ToString();
                        if (textBox1.Text.Trim() != "" && txtolditm.Text != textBox1.Text)
                          
                        search_data(textBox1.Text.Trim());
                        grditem.Visible = false;

                        break;
                    case 38:
                        if (!grditem.Visible) return;
                            int crow = grditem.CurrentRow.Index;
                            int mros = grditem.Rows.Count;
                            // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                            //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                            //if (Txtitem.Text.Trim() == "" && mros > 0)
                            //{
                            //    search_data(grditem[0, crow].Value.ToString());
                            //    return;
                            //}
                            if (crow > 0)
                                grditem.CurrentCell = grditem.Rows[crow - 1].Cells[0];

                       

                        break;
                    case 40:
                        if (!grditem.Visible) return;
                             crow = grditem.CurrentRow.Index;
                            mros = grditem.Rows.Count;
                            // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                            //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;

                            //if (Txtitem.Text.Trim() == "" && mros > 0)
                            //{
                            //    search_data(grditem[0, crow].Value.ToString());
                            //        return;
                            //}
                           
                            {
                                if (crow < mros - 1)
                                    grditem.CurrentCell = grditem.Rows[crow + 1].Cells[0];
                            }

                        break;

                }
            }

            

            private void Txtitem_Validated(object sender, EventArgs e)
            {
                //if (Txtitem.Text.Length==16)

                if (Txtitem.Text.Trim()!="" && txtolditm.Text != Txtitem.Text)
                search_data(Txtitem.Text);
            }

            private void grditem_RowEnter(object sender, DataGridViewCellEventArgs e)
            {
               if (isini)  return;

                
                if (grditem.CurrentCell != null)
                {

                    //int row = e.RowIndex;
                    //string txt = textBox1.Text.Trim();
                    //if (!issearch && row >= 0) search_data(grditem["Item_Code", row].Value.ToString());
                    // textBox1.Text =  txt ;
                }
            }

            private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {

            }

            private void grditem_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
            {
                print_Report(1);
            }

            private void stockReprtByWHouseToolStripMenuItem_Click(object sender, EventArgs e)
            {
                print_Report(2);
            }

            private void stockReportForCategoryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                print_Report(3);
            }

            private void stockReportForReorderToolStripMenuItem_Click(object sender, EventArgs e)
            {
                print_Report(4);
            }

            private void label11_Click(object sender, EventArgs e)
            {

            }

          

            

       
       


                private void merge()
    {




















          //  {
    //If Header item
    
    //If (e.Item.ItemType = = ListItemType.Header)
    //{
    //    e.Item.Cells.RemoveAt(2);
    //    e.Item.Cells(1).ColumnSpan = 2;
    //    //Insert the table shown in the diagram 3
    //    // to the Text property of the Cell
    //    e.Item.Cells(1).Text = "<table style='FONT-WEIGHT: bold; WIDTH:" + 
    //          " 100%; COLOR: white; TEXT-ALIGN: center'><tr align" + 
    //          " =center><td colspan = 2 style='BORDER-BOTTOM:" + 
    //          " cccccc 1pt solid'>Name</td></tr>" + 
    //          "<tr align =center ><td style ='BORDER-RIGHT:" + 
    //          " cccccc 1pt solid'>F Name</td><td>L" + 
    //          " Name</td></tr></table>";
    //}  


//            public class JobTreeNode : TreeNode {

//    private int intField1;

//    public int Field1 {
//        get {
//            return intField1;
//        }
//        set {
//            intField1 = value;
//        }
//    }
//}
//Usage (added after comments)

//// Add the node
//JobTreeNode CustomNode = new JobTreeNode();
//CustomNode.Text = "Test";
//CustomNode.Field1 = 10
//treeView1.Nodes.add(CustomNode);


//// SelectedNode 
//((CustomNode)(treeView1.SelectedNode)).Field1;

    }


      
     
          private void FrmItemMaster_KeyDown(object sender, KeyEventArgs e)
          {
              switch (e.KeyData & Keys.KeyCode)
              {
                  case Keys.Up:
                  case Keys.Right:
                  case Keys.Down:
                  case Keys.Left:

                      if (grdlookup.Visible)
                      {
                          e.Handled = true;
                          e.SuppressKeyPress = true;
                      }
                      break;
                  case Keys.Escape:
                      grditem.Visible = false;
                      break;
                  case Keys.Tab:
                     // grditem.Visible = false;
                      break;

              }
          }
          private void grdbutton_CellClick(object sender, DataGridViewCellEventArgs e)
          {



              switch (grdbutton.CurrentCell.ColumnIndex)
              {
                  
                  case 0:
                      {

                          if (string.IsNullOrEmpty(Txtitem.Text))
                          {
                              MessageBox.Show("Invalid Asset Code");
                              return;
                          }

                         
                          saveToolStripButton_Click(sender,e);
                          break;
                      }
                  case 1:
                      {

                          newToolStripButton_Click(sender, e);
                          break;
                      }

                  case 2:
                      {
                          Gvar.Gind = 3;
                          if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("assetCategoryToolStripMenuItem");

                          Form childForm = new FrmItemCat();
                          childForm.MdiParent = MDIParent1.ActiveForm;

                          //childForm.Text = "Window " + childFormNumber++;
                          childForm.Text = "Asset Category Entry Screen";
                          childForm.Show();
                          
                      }
                      break;

                 
              }
                         

               
                       

                         

                         
          }




    
          private void grdbutton_Enter(object sender, EventArgs e)
          {
              grdbutton.CurrentCell = grdbutton[0, 0];
             
          }

          private void grdmain_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {

          }

          protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
          {
              if (msg.WParam.ToInt32() == (int)Keys.Enter &&  !grditem.Visible )
              {
                  // SendKeys.Send("{Tab}");

                  switch(acntrl)
                  {
                      case "grdmain":

                      case "grditem":
                      case "grdlookup":
                      case "grdstock":
                      case "grdsup":
                      case "grdbutton":
                      case "grdbarcode":
                          {
                              return(false);
                          }
                          break;

                  }
                  bool nextskip = false;
                  string btn;
                  btn = this.ActiveControl.GetType().ToString();
                  if (this.ActiveControl.GetType().ToString() == "System.Windows.Forms.Button") nextskip = true;
                  if (!nextskip)
                  this.SelectNextControl(this.ActiveControl, true, true, true, true);

                  keyData = Keys.Tab;


               //   SendKeys.Send("{Tab}");
                  return true;
                  return base.ProcessCmdKey(ref msg, keyData);
                 
                  
                  //return true;
              }
              return base.ProcessCmdKey(ref msg, keyData);
          }

        

          private void grdsup_Enter(object sender, EventArgs e)
          {
              acntrl = "grdsup";
          }

          private void grdsup_Leave(object sender, EventArgs e)
          {
              acntrl = "";
          }

          private void grdbarcode_Enter(object sender, EventArgs e)
          {
              acntrl = "grdbarcode";
          }

        


          private void textBox1_DoubleClick(object sender, EventArgs e)
          {
              grditem.Visible = !grditem.Visible;
              grditem.Top = textBox1.Top + textBox1.Height;
              grditem.Left = textBox1.Left;
              isini = false;
          }

          private void grditem_DoubleClick(object sender, EventArgs e)
          {
              try
              {
                  textBox1.Text = grditem[0, grditem.CurrentCell.RowIndex].Value.ToString();
                  search_data(textBox1.Text.Trim());
                  grditem.Visible = false;
                  textBox1.Focus();

              }
              catch(Exception ex)
              {
                  grditem.Visible = false;
              }
          }

        

          private void grdbutton_CellEnter(object sender, DataGridViewCellEventArgs e)
          {
              acntrl = "grdbutton";
          }

          private void grdbutton_Leave(object sender, EventArgs e)
          {
              acntrl = "";
          }

          private void grdbutton_KeyDown(object sender, KeyEventArgs e)
          {
              switch (e.KeyData & Keys.KeyCode)
              {

                  case Keys.Enter:
                      int i;
                      //for (i = grdbutton.CurrentCell.RowIndex + 1; i < grdbutton.Rows.Count - 1; i++)
                      //{
                      //    if (i == grdstock.Rows.Count) break;
                      //    if (grdstock[0, i].Visible) break;
                      //}
                      //if (i == grdstock.Rows.Count)
                      //    this.SelectNextControl(this.ActiveControl, true, true, true, true);

                      grdbutton_CellClick(sender, null);

                      break;
              }

          }

      
          private void updat_accounts(string docno, decimal TXTSRAMT)
          {
              ADODB.Connection ADOconn = new ADODB.Connection();

              try
              {
                  try
                  {






                      docno = Txtitem.Text.Trim();

                      if (Convert.ToDecimal(txtcost.Text) > 0)
                      {

                          ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                          object TRNBY;
                          object DRCR;
                          object DRCR1;
                          object NARR;
                          object LACC;
                          object PAYBY;
                          object[] ledgerini;
                          string acc_acs;
                          acc_acs = Program.ledger_ini(Convert.ToInt16("300"), docno);
                          ledgerini = acc_acs.Split('`');
                          TRNBY = Convert.ToInt16((ledgerini[0]));
                          DRCR = ledgerini[1];
                          DRCR1 = ledgerini[2];
                          NARR = ledgerini[3];
                          LACC =   Convert.ToInt64((ledgerini[4]));
                     
                          PAYBY = Convert.ToInt16(ledgerini[05]);

                          NARR = "Asset Entry of " + txtname.Text;
                          ADOconn.BeginTrans();
                          Recordset TMP = new Recordset();

                          if (isedit)
                          {
                              sql = "INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code] FROM TRN_ACCOUNTS WHERE NYEAR=" + Gvar.nyear + " and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + Gvar.brn_code;

                              TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          }

                          sql = "SELECT AC_NO FROM ASSET_CAT WHERE AST_CAT_CODE =" +cmbcat.SelectedValue;
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          if(TMP.RecordCount>0)
                          {
                              LACC =   Convert.ToInt64(TMP.Fields[0].Value);
                          }


                          sql = "DELETE FROM TRaN_ACC WHERE YEAR(CUR_DATE) ='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + Gvar.brn_code;
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                          sql = "select * from trnno";
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          object trno = TMP.Fields[0].Value;
                          object trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                          object accno = 0;



                          Recordset acc = new Recordset();





                          long asset_ac = 0;
                          long asset_ob = 0;


                          sql = "select ASSET_OB from ac_options WHERE  ac_options.ID =1";
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                          if (TMP.RecordCount > 0)
                          {
                              //if (TMP.Fields[0].Value != DBNull.Value)
                              //    asset_ac = Convert.ToInt64(TMP.Fields[0].Value);

                              if (TMP.Fields[0].Value != DBNull.Value)
                                  asset_ob = Convert.ToInt64(TMP.Fields[0].Value);
                          }

                          //if (asset_ac == 0 )
                          //{
                          //    MessageBox.Show("Invalid Initial Account FOR ASSET", "Invalid Account");
                          //    iserror = true;
                          //    return;
                          //}

                          if (asset_ob == 0 && chkOb.Checked)
                          {
                              ADOconn.RollbackTrans();
                              MessageBox.Show("Invalid Initial Account FOR ASSET Ob Account", "Invalid Account");
                              iserror = true;
                              return;
                          }






                          #region aSSET ACcount
                          string lnarr = "";

                          sql = "SELECT *  FROM TRN_ACCOUNTS WHERE DR_CR='" + DRCR + "' AND DOC_NO='" + Txtitem.Text.Trim() + "' AND VOUCHER_NO='ASSET' AND ENTRY_NO=1";
                          acc = new Recordset();
                          acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                          TXTSRAMT = Convert.ToDecimal(txtcost.Text);

                          if (TXTSRAMT > 0)
                          {
                              sql = "select * from trnno";
                              TMP = new Recordset();
                              TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                              trno = TMP.Fields[0].Value;
                              trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                              if (acc.RecordCount == 0)
                              {
                                  acc.AddNew();
                                  acc.Fields["trn_no"].Value = trno;
                                  acc.Fields["trn_no2"].Value = trno2;
                                 
                                  acc.Fields["DR_CR"].Value = DRCR;
                                  acc.Fields["User_id"].Value = Gvar.Userid;
                              }
                              acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                              //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                              //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                              acc.Fields["SNO"].Value = 1;
                              lnarr = " Fixed Asset A/C " ;
                              acc.Fields["PAY_AMOUNT"].Value = txtcost.Text;
                              acc.Fields["f_pay_amount"].Value = txtcost.Text;
                              acc.Fields["F_RATE"].Value = "1";
                              acc.Fields["currency"].Value = Gvar._currency;

                              acc.Fields["pay_date"].Value = dteffectdate.Value;
                              acc.Fields["NARRATION"].Value = NARR + lnarr;
                              acc.Fields["doc_no"].Value = docno;
                              acc.Fields["PAYBY"].Value = cmbcat.SelectedValue;
                              acc.Fields["TRN_BY"].Value = TRNBY;
                              acc.Fields["NYEAR"].Value = dteffectdate.Value.Year;
                              acc.Fields["cost_code"].Value = 0;
                              acc.Fields["dept_code"].Value = 0;
                              acc.Fields["entry_no"].Value = 1;// Convert.ToDecimal(docno);
                              acc.Fields["voucher_no"].Value = "ASSET";
                              acc.Fields["trn_type"].Value = 300;
                             
                                  acc.Fields["BRN_CODE"].Value = cmbbranch.SelectedValue; 
                              acc.Update();

                              sql = "select * from trnno";
                              TMP = new Recordset();
                              TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                              trno2 = TMP.Fields[0].Value;
                              // trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;


                              sql = "SELECT  * FROM TRN_ACCOUNTS WHERE DR_CR='" + DRCR1 + "' AND DOC_NO='" + Txtitem.Text.Trim() + "' AND VOUCHER_NO='ASSET' AND ENTRY_NO=1";
                              acc = new Recordset();
                              acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                              if (acc.RecordCount == 0)
                              {
                                  acc.AddNew();
                                  acc.Fields["trn_no"].Value = trno2;
                                  acc.Fields["trn_no2"].Value = trno;
                                 
                                  acc.Fields["DR_CR"].Value = DRCR1;
                                  acc.Fields["User_id"].Value = Gvar.Userid;
                              }
                              if (chkOb.Checked)
                              {
                                  acc.Fields["acc_no"].Value = asset_ob;
                                  lnarr = "Capital AC.";
                              }
                              else
                              {
                                  acc.Fields["acc_no"].Value = cmbpaidac.SelectedValue;
                                  lnarr = "Paid by " + cmbpaidac.Text;
                              }
                              acc.Fields["PAY_AMOUNT"].Value = txtcost.Text;
                              acc.Fields["f_pay_amount"].Value = txtcost.Text; ;
                              acc.Fields["F_RATE"].Value = "1";
                              acc.Fields["currency"].Value = Gvar._currency;
                              acc.Fields["SNO"].Value = 2;
                              acc.Fields["pay_date"].Value = dteffectdate.Value;
                              acc.Fields["NARRATION"].Value = NARR + lnarr;
                              acc.Fields["doc_no"].Value = docno;
                              acc.Fields["PAYBY"].Value = cmbcat.SelectedValue;
                              acc.Fields["TRN_BY"].Value = TRNBY;
                              acc.Fields["NYEAR"].Value = dteffectdate.Value.Year;
                              acc.Fields["cost_code"].Value = 0;
                              acc.Fields["dept_code"].Value = 0;
                              acc.Fields["entry_no"].Value = 1;// Convert.ToDecimal(docno);
                              acc.Fields["voucher_no"].Value = "ASSET";
                              acc.Fields["trn_type"].Value = 300;

                              acc.Fields["BRN_CODE"].Value = cmbbranch.SelectedValue; 
                              acc.Update();
                          }
                          #endregion Inventory ACcount





                          sql = "SELECT TOP 1 * FROM TRAN_ACC";
                          acc = new Recordset();
                          acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                          acc.AddNew();
                          // acc.Fields["trn_no"].Value = trno;
                          acc.Fields["BRN_CODE"].Value = cmbbranch.SelectedValue;
                          acc.Fields["PaidTo_Acc"].Value = 0;

                          if (chkOb.Checked)
                              acc.Fields["PaidTo_Acc"].Value = asset_ob;
                          else
                              acc.Fields["PaidTo_Acc"].Value = cmbpaidac.SelectedValue;
                          //acc.Fields["User"].Value = Gvar.Userid;
                          acc.Fields["acc_no"].Value = LACC;
                          acc.Fields["AMOUNT"].Value = txtcost.Text;
                          acc.Fields["currency_rate"].Value = Gvar._cur_rate;

                          acc.Fields["currency_CODE"].Value = Gvar._currency;
                          acc.Fields["Ledger_acc"].Value = LACC;
                          acc.Fields["CUR_DATE"].Value = dteffectdate.Value;
                          acc.Fields["Description"].Value = NARR;
                          acc.Fields["doc_no"].Value = docno;
                          acc.Fields["currency_code"].Value = Gvar._currency;
                          acc.Fields["TRN_BY"].Value = TRNBY;
                          // acc.Fields["NYEAR"].Value = dt1.Value.Year;
                          acc.Fields["entry_no"].Value = 1;// Convert.ToDecimal(docno);
                          acc.Update();

                          iserror = false;
                          ADOconn.CommitTrans();
                      }
                  }


                  catch (SqlException er)
                  {
                      ADOconn.RollbackTrans();
                      MessageBox.Show(er.Message);
                      iserror = true;
                  }
              }


              catch (Exception ex)
              {
                 
                  MessageBox.Show(ex.Message);
                  iserror = true;
              }



          }

          private void btnbrowse_Click(object sender, EventArgs e)
          {
              OpenFileDialog openFileDialog = new OpenFileDialog();
              openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
              //openFileDialog.Filter = "Image Files (*.jpeg)|*.jpg|All Files (*.png)|*.bmp";
              openFileDialog.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
              if (openFileDialog.ShowDialog(this) == DialogResult.OK)
              {
                  txtfilename.Text = openFileDialog.FileName;
                  pictureBox1.Load(txtfilename.Text);
              }
          }
          private void save_foto(bool update)
          {
              try
              {
                  //SqlConnection con;
                  SqlCommand cmd;
                
                  System.IO.MemoryStream ms;
                  
                  SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConImage"].ConnectionString);
     
                  //con = new SqlConnection("user id=sa;password=123;database=adil");
                  if (update)
                  cmd = new SqlCommand("Update  Item_Image set barcode='" + Txtitem.Text + "',photo=@photo where barcode='"  + txtolditm.Text + "'", con);
                  else
                      cmd = new SqlCommand("insert into Item_Image(barcode,photo) values('" + Txtitem.Text + "',@photo)", con);

                  if (pictureBox1.Image != null)
                  {
                      //using FileStream:(will not work while updating, if image is not changed)
                      //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                      //byte[] photo_aray = new byte[fs.Length];
                      //fs.Read(photo_aray, 0, photo_aray.Length);  

                      //using MemoryStream:
                      ms = new System.IO.MemoryStream();
                      pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                      byte[] photo_aray = new byte[ms.Length];
                      ms.Position = 0;
                      ms.Read(photo_aray, 0, photo_aray.Length);
                      cmd.Parameters.AddWithValue("@photo", photo_aray);
                  }
                  con.Open();
                  int n = cmd.ExecuteNonQuery();
                  if (n==0)
                  {
                      cmd = new SqlCommand("insert into Item_Image(barcode,photo) values('" + Txtitem.Text + "',@photo)", con);
                      if (pictureBox1.Image != null)
                      {
                         
                          ms = new System.IO.MemoryStream();
                          pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                          byte[] photo_aray = new byte[ms.Length];
                          ms.Position = 0;
                          ms.Read(photo_aray, 0, photo_aray.Length);
                          cmd.Parameters.AddWithValue("@photo", photo_aray);
                      }
                      n = cmd.ExecuteNonQuery();

                  }
                  rd.Close();
                  con.Close();
              }
              catch (Exception ex)
              {

              }

          }

          void conv_photo()
          {
              //converting photo to binary data
              if (pictureBox1.Image != null)
              {
                  //using FileStream:(will not work while updating, if image is not changed)
                  //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                  //byte[] photo_aray = new byte[fs.Length];
                  //fs.Read(photo_aray, 0, photo_aray.Length);  

                  //using MemoryStream:
                  System.IO.MemoryStream ms = new System.IO.MemoryStream();
                  pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                  byte[] photo_aray = new byte[ms.Length];
                  ms.Position = 0;
                  ms.Read(photo_aray, 0, photo_aray.Length);
                  cmd.Parameters.AddWithValue("@photo", photo_aray);
              }
          }
             private void showdata( byte[] photo_aray)
    {
        
           
            pictureBox1.Image = null;
           
               // photo_aray = (byte[])ds.Tables[0].Rows[rno][4];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(photo_aray);
                pictureBox1.Image = Image.FromStream(ms);
           
       
    }

             private void label1_Click(object sender, EventArgs e)
             {

             }

             private void label9_Click(object sender, EventArgs e)
             {

             }

             private void label12_Click(object sender, EventArgs e)
             {

             }

             private void grdbutton_CellContentClick(object sender, DataGridViewCellEventArgs e)
             {

             }

        private void find_currevalue()
             {
            try
            {
                double days = (dtpayment.Value.Date - dteffectdate.Value.Date).TotalDays;

                if (txtcost.Text == "") txtcost.Text = "0";
                if (txtdeppcnt.Text == "'") txtdeppcnt.Text = "0";
                double price = (Convert.ToDouble(txtcost.Text) * Convert.ToDouble(txtdeppcnt.Text)/100)/365 * days;
                txtdepamt.Text = "0";
                if (price > 0)
                {
                   
                    txtcurval.Text =  Math.Round((Convert.ToDouble(txtcost.Text) - price),2).ToString();
                    txtdepamt.Text = Math.Round(price,2).ToString();
                }
                else
                {
                    txtdepamt.Text = "0";
                    txtcurval.Text = txtcost.Text;
                }


            }
            catch(Exception ex)
            {

            }

             }

        private void txtdeppcnt_TextChanged(object sender, EventArgs e)
        {
            find_currevalue();
        }

        private void txtcost_TextChanged(object sender, EventArgs e)
        {
            find_currevalue();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
              try
              {
                  try
                  {



                     


                      string docno = Txtitem.Text.Trim();

                      if (Convert.ToDecimal(txtdepamt.Text) > 0)
                      {
                          
                          if(ADOconn.State==0)
                          ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
           
                          object TRNBY;
                          object DRCR;
                          object DRCR1;
                          object NARR;
                          object LACC;
                          object PAYBY;
                          object[] ledgerini;
                          string acc_acs;
                          acc_acs = Program.ledger_ini(Convert.ToInt16("300"), docno);
                          ledgerini = acc_acs.Split('`');
                          TRNBY = Convert.ToInt16((ledgerini[0]));
                          DRCR = ledgerini[1];
                          DRCR1 = ledgerini[2];
                          NARR = ledgerini[3];
                          LACC =   Convert.ToInt64((ledgerini[4]));
                          PAYBY = Convert.ToInt16(ledgerini[05]);

                          NARR = "Deperciation of " + txtname.Text;
                         
                          Recordset TMP = new Recordset();

                          object asset_dep_ac = "0";
                          object out1;
                          //sql = "select ASSET_DEP_AC from ac_options WHERE  ac_options.ID =1";
                          //TMP = new Recordset();
                          //TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                          //if (TMP.RecordCount > 0)
                          //{
                              

                          //    if (TMP.Fields[0].Value != DBNull.Value)
                          //        asset_dep_ac = Convert.ToInt64(TMP.Fields[0].Value);
                          //}



                          sql = "SELECT AC_NO,DEP_ACNO FROM ASSET_CAT WHERE AST_CAT_CODE =" + cmbcat.SelectedValue;
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          if (TMP.RecordCount > 0)
                          {
                              LACC = Convert.ToInt64(TMP.Fields[0].Value);
                              asset_dep_ac = Convert.ToInt64(TMP.Fields[1].Value);
                          }

                          if ((long)asset_dep_ac < 1 || (long)LACC < 1)
                          {
                              MessageBox.Show("Invalid  Account FOR ASSET Depreceation ", "Invalid Account");
                              iserror = true;
                              return;
                          }

                          sql = "SELECT sum(case when dr_cr='C' THEN pay_amount ELSE PAY_AMOUNT *-1 END) FROM trn_accounts WHERE convert(varchar,PAY_DATE,112) <= '" + dtpayment.Value.Date.ToString("yyyy-MM-dd") + "' and  TRN_by=" + TRNBY + "  and DOC_NO='" + docno + "'  AND VOUCHER_NO='ASSET' and acc_no =" + LACC;
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);
                          double CURVAL = 0;
                          if    (txtdepamt.Text=="") txtdepamt.Text="0";
                          if (TMP.Fields[0].Value != DBNull.Value)
                          {
                              CURVAL = Convert.ToDouble(TMP.Fields[0].Value) - Convert.ToDouble(txtdepamt.Text) ;
                              if (Convert.ToDouble(txtcurval.Text) > CURVAL)
                              {
                                  MessageBox.Show("Mismached Current asset Value found at the accounts Record :" + CURVAL + ", Please check , Exiting update!!!");
                                  return;

                              }
                          }


                          ADOconn.BeginTrans();

                          if (chckdepupd.Checked)
                          {
                              sql = "delete from  TRN_ACCOUNTS WHERE  TRN_by=" + TRNBY + " AND ENTRY_NO=2 and DOC_NO='" + docno + "'  acc_no =" + asset_dep_ac + " and voucher_no = (select max(voucher_no) from TRN_ACCOUNTS WHERE  TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and  acc_no =" + asset_dep_ac + ")";

                              ADOconn.Execute(sql, out out1);
                              sql = "delete from  TRaN_ACC WHERE  TRN_by=" + TRNBY + " AND  DOC_NO='" + docno + "'  acc_no =" + asset_dep_ac + " and entry_no = (select max(voucher_no) from TRN_ACCOUNTS WHERE  TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and  acc_no =" + asset_dep_ac + ")";

                              ADOconn.Execute(sql, out out1);
                          }


                         
                          //sql = "DELETE FROM TRaN_ACC WHERE YEAR(CUR_DATE) ='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + Gvar.brn_code;
                          //TMP = new Recordset();
                          //TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                          sql = "select * from trnno";
                          TMP = new Recordset();
                          TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          object trno = TMP.Fields[0].Value;
                          object trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                          object accno = 0;



                            Recordset acc = new Recordset();
                         

                         


                          long asset_ac = 0;
                          long asset_ob = 0;
                         


                          #region aSSET ACcount
                          string lnarr = "";

                          sql = "SELECT *  FROM TRN_ACCOUNTS WHERE DR_CR='" + DRCR + "' AND DOC_NO='" + Txtitem.Text.Trim() + "' AND VOUCHER_NO='ASSET' AND ENTRY_NO=2";
                          acc = new Recordset();
                          acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                          double TXTSRAMT = Convert.ToDouble(txtdepamt.Text);

                          if (TXTSRAMT > 0)
                          {
                              sql = "select * from trnno";
                              TMP = new Recordset();
                              TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                              trno = TMP.Fields[0].Value;
                              trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                              //if (acc.RecordCount == 0)
                              //{
                                  acc.AddNew();
                                  acc.Fields["trn_no"].Value = trno;
                                  acc.Fields["trn_no2"].Value = trno2;
                                  acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                  acc.Fields["DR_CR"].Value = DRCR1;
                                  acc.Fields["User_id"].Value = Gvar.Userid;
                              ///}
                              acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                              //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                              //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);

                              lnarr = " Fixed Asset A/C";
                              acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                              acc.Fields["f_pay_amount"].Value = TXTSRAMT;
                              acc.Fields["F_RATE"].Value = "1";
                              acc.Fields["SNO"].Value = 1;
                              acc.Fields["currency"].Value = Gvar._currency;

                              acc.Fields["pay_date"].Value = DateTime.Now.Date;
                              acc.Fields["NARRATION"].Value = NARR + lnarr;
                              acc.Fields["doc_no"].Value = docno;
                              acc.Fields["PAYBY"].Value = cmbcat.SelectedValue;
                              acc.Fields["TRN_BY"].Value = TRNBY;
                              acc.Fields["NYEAR"].Value = DateTime.Now.Date.Year;
                              acc.Fields["cost_code"].Value = 0;
                              acc.Fields["dept_code"].Value = 0;
                              acc.Fields["entry_no"].Value = 2;// Convert.ToDecimal(docno);
                              acc.Fields["voucher_no"].Value = "ASSET";
                              acc.Update();

                              sql = "select * from trnno";
                              TMP = new Recordset();
                              TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                              trno2 = TMP.Fields[0].Value;
                             // trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;


                              sql = "SELECT  * FROM TRN_ACCOUNTS WHERE DR_CR='" + DRCR1 + "' AND DOC_NO='" + Txtitem.Text.Trim() + "' AND VOUCHER_NO='ASSET' AND ENTRY_NO=2";
                              acc = new Recordset();
                              acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                              //if (acc.RecordCount == 0)
                              //{
                                  acc.AddNew();
                                  acc.Fields["trn_no"].Value = trno2;
                                  acc.Fields["trn_no2"].Value = trno;
                                  acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                  acc.Fields["DR_CR"].Value = DRCR;
                                  acc.Fields["User_id"].Value = Gvar.Userid;
                                  acc.Fields["SNO"].Value = 2;
                              //}
                                  lnarr = " Fixed Asset Dep. A/C";
                                  if (chkOb.Checked)
                                  {
                                      acc.Fields["acc_no"].Value = asset_dep_ac;
                                  }
                                  else
                                      acc.Fields["acc_no"].Value = asset_dep_ac; 
                              acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                              acc.Fields["f_pay_amount"].Value = TXTSRAMT;
                              acc.Fields["F_RATE"].Value = "1";
                              acc.Fields["currency"].Value = Gvar._currency;

                              acc.Fields["pay_date"].Value = DateTime.Now.Date;
                              acc.Fields["NARRATION"].Value = NARR + lnarr;
                              acc.Fields["doc_no"].Value = docno;
                              acc.Fields["PAYBY"].Value = cmbcat.SelectedValue;
                              acc.Fields["TRN_BY"].Value = TRNBY;
                              acc.Fields["NYEAR"].Value = dteffectdate.Value.Year;
                              acc.Fields["cost_code"].Value = 0;
                              acc.Fields["dept_code"].Value = 0;
                              acc.Fields["entry_no"].Value = 2;// Convert.ToDecimal(docno);
                              acc.Fields["voucher_no"].Value = "ASSET";
                              acc.Update();
                          }
                          #endregion Inventory ACcount


                   


                          sql = "SELECT TOP 1 * FROM TRAN_ACC";
                          acc = new Recordset();
                          acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                          acc.AddNew();
                          // acc.Fields["trn_no"].Value = trno;
                          acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                          acc.Fields["PaidTo_Acc"].Value = 0;

                          if (chkOb.Checked)
                              acc.Fields["PaidTo_Acc"].Value = asset_dep_ac;
                          else
                              acc.Fields["PaidTo_Acc"].Value = asset_dep_ac; 
                          //acc.Fields["User"].Value = Gvar.Userid;
                          acc.Fields["acc_no"].Value = LACC;
                          acc.Fields["AMOUNT"].Value = TXTSRAMT;
                          acc.Fields["currency_rate"].Value = Gvar._cur_rate;

                          acc.Fields["currency_CODE"].Value = Gvar._currency;
                          acc.Fields["Ledger_acc"].Value = LACC;
                          acc.Fields["CUR_DATE"].Value = DateTime.Now.Date;
                          acc.Fields["Description"].Value = NARR;
                          acc.Fields["doc_no"].Value = docno;
                          acc.Fields["currency_code"].Value = Gvar._currency;
                          acc.Fields["TRN_BY"].Value = TRNBY;
                          // acc.Fields["NYEAR"].Value = dt1.Value.Year;
                          acc.Fields["entry_no"].Value = trno;// Convert.ToDecimal(docno);
                          acc.Update();

                          iserror = false;

                      }
                      ADOconn.CommitTrans();
                      MessageBox.Show("Successfully Updated Asset Depreciation", "Successfull");
                  }

                  catch (SqlException er)
                  {
                      ADOconn.RollbackTrans();
                      MessageBox.Show(er.Message);
                      iserror = true;
                  }
              }


              catch (Exception ex)
              {
                 
                  MessageBox.Show(ex.Message);
                  iserror = true;
              }



         

        }

        private void dteffectdate_ValueChanged(object sender, EventArgs e)
        {
            dtpayment.Value = dteffectdate.Value;
        }

        private void dtpayment_ValueChanged(object sender, EventArgs e)
        {
            find_currevalue();
        }
    
    }    

 }




