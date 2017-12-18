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
namespace FinOrg
{

    public partial class FrmSRVCMaster : Form
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


        DataTable itemdt = new DataTable();
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        bool issearch;

        public FrmSRVCMaster()
        {
            InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
            ClearTextBoxes(this);
            isini = true;
            Load_data();
            isini = false;

        }

         private void Load_data()
        {
            try
            {
                Conn.Close();
                Conn.Open();
                saveToolStripButton.Enabled = true;
                toolRefund.Enabled = true;
                sql = "sELECT  Item_Code,DESCRIPTION,ITM_CAT_CODE,LAST_PUR_PRICE,STOCK from ITEMMASTER WHERE ITM_CAT_CODE=0";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

             aditem = new SqlDataAdapter(cmd);
            
                 itemdt = new DataTable("ITEMMASTER");
                aditem.Fill(itemdt);
                grditem.Visible = true;
                dv.Table = itemdt;

                grditem.DataSource = dv;
                grditem.Columns[1].Width = 300;
                //MessageBox.Show(rd["isrefund"].ToString());
                if (Gvar._SuperUserid != 1)
                {
                    saveToolStripButton.Enabled = false;


                }

                
                
                sql = "sELECT  Unit_id,unit_name froM Unitmaster WHERE UNIT_TYPE='S'";

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("Unitmaster");
                ada3.Fill(dt3);
                cmbunit.DataSource = dt3;

                Gvar.ComboboxItem cbmitm= new Gvar.ComboboxItem() ;
                cbmitm.Text = "Service";
                cbmitm.Value = 0;

                cmbcat.Items.Add(cbmitm);
                //cmbcatcode.
                cmbcat.SelectedValue = 0;
                Conn.Close();
            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }


        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save_data();
        }


        
        private void print_reciept()
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


                if (string.IsNullOrEmpty(Txtitem.Text)) return;

                ReportDocument CrRep = new ReportDocument();





                //crconnectioninfo.ServerName = "Mujeeb";
                //crconnectioninfo.DatabaseName = "Printex";
                //crconnectioninfo.UserID = "sa";
                //crconnectioninfo.Password = "sa0101";

                crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                rep_path = Application.StartupPath + "\\reports\\Receipt.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;





                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
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

                if (Txtitem.Text.Length != 16)
                {
                    MessageBox.Show("Invalid Length of Service Code, Must Be 16 Digit!!");
                    return;
                }
                Conn.Close();
                Conn.Open();

                bool isempty;
                isempty = false;
                txtfraction.Text = "1";
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
                                        MessageBox.Show(tb.Name);
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



                if (isedit == false)
                {
                  

                    sql = "SELECT Item_Code FROM HD_ITEMMASTER where Item_Code ='" + Txtitem.Text.Trim() + "'";

                    cmd = new SqlCommand(sql, Conn);
                    
                    rd = cmd.ExecuteReader();



                    if (rd.HasRows)
                    {
                       
                       
                       DialogResult result = MessageBox.Show("This Service Already Existing!!, Do You want to Update?","Service Found", MessageBoxButtons.YesNoCancel);

                       if (result == DialogResult.Yes) isedit = true; else return;

                    }
                }

                              

                if (string.IsNullOrEmpty(Txtitem.Text.ToString()))
                {
                    return;
                }
                Conn.Close();
                Conn.Open();
                //Conn.BeginTransaction();
                fnd = false;

                if (isedit == false)
                {

                    sql = "INSERT INTO [HD_ITEMMASTER]([Item_Code],[DESCRIPTION],[USER],[ITM_CAT_CODE],[UNIT],[FRACTION],[ALIAS_NAME],[BRN_CODE])";
                    sql = sql + " VALUES ('" + Txtitem.Text + "','" + txtname.Text + "','" + Gvar._Userid + "','" + cmbcat.SelectedValue + "','" + cmbunit.SelectedValue + "','" + txtfraction.Text + "','" + txtalias.Text + "'," + Gvar._brn_code + " )";

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                    sql = " INSERT INTO [STOCK_MASTER]([Item_Code],[LAST_PUR_PRICE],[USER1],[BRN_CODE],[OP_STOCK],";
                    sql = sql + "[AVG_PUR_PRICE]) VALUES ('" + Txtitem.Text + "','" + txtcost.Text + "','" + Gvar._Userid + "'," + Gvar._brn_code + " ,0,'" + txtcost.Text + "')";

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                }
                else
                {
                    sql = "update  [HD_ITEMMASTER]  set [Item_Code]='" + Txtitem.Text + "',[DESCRIPTION]='" + txtname.Text + "',[ITM_CAT_CODE]='" + cmbcat.SelectedValue + "',[UNIT]='" + cmbunit.SelectedValue + "',[FRACTION]='" + txtfraction.Text + "',[ALIAS_NAME]='" + txtalias.Text + "'  where Item_Code ='" + Txtitem.Text.Trim() + "'";
                    

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();



                    sql = " update  [STOCK_MASTER] set [Item_Code]='" + Txtitem.Text + "',[LAST_PUR_PRICE]='" + txtcost.Text + "',[AVG_PUR_PRICE]='" + txtcost.Text + "' where Item_Code ='" + Txtitem.Text.Trim() + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                }
                string v1;
                string v2;



                isedit = true;




                Conn.Close();

               
              

               
                MessageBox.Show("Successfully Updated Service", "Successfull");
                return;

               
               // MessageBox.Show("Successfully Inserted New reciept", "Successfull");
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

                if (double.TryParse(Txtitem.Text, out val1))
                {
                    if (Convert.ToDouble(Txtitem.Text) < 1)
                    {
                        Txtitem.Text = "";
                        MessageBox.Show("Invalid Service Number, PLease Try Again", "Plese Enter Correct Value");
                        Conn.Close();
                        return;
                    }
                }
                else
                {
                    Txtitem.Text = "";
                    MessageBox.Show("Invalid Service Number, Please Try Again", "Plese Enter Correct Value");
                    Conn.Close();
                    return;

                }

                DialogResult result = MessageBox.Show("Do You want to Delete The Service Number " + Txtitem.Text + "?", "Delete Service", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    Conn.Close();
                    Conn.Open();
                   
                    sql = "Delete from  Reciepts where reciept_no =" + Convert.ToDouble(Txtitem.Text);
                    Conn.Close();
                    Conn.Open();
                    cmd = new SqlCommand(sql, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Reciept Completed Successfully!!");

                }


            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);


            }
        }

        private void ini_form()
        {
            ClearTextBoxes(this);
            if (cmbcat.Items.Count > 0 )             cmbcat.SelectedIndex = 0;
            if (cmbunit.Items.Count > 0) cmbunit.SelectedIndex = 0;

            ClearTextBoxes(this);
            toolRefund.Enabled = true;
            dt1.Value = DateTime.Now;

            Gvar.ArCalendar(dt1.Value);
            saveToolStripButton.Enabled = true;
            if (Gvar._SuperUserid != 1)
            {
                toolRefund.Enabled = false;
                tooldelete.Enabled = false;

            }
            //_load_stock();
            
            isedit = false;
            isini = true;
            
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
        }
        
        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            
            issearch = false;
            if (textBox1.Text.Trim() != "")
            {
                search_data(textBox1.Text.Trim());

            }
            else
            {
                if (grditem.CurrentCell != null)
                {

                    int row = grditem.CurrentCell.RowIndex;
                    if (!issearch && row >= 0) search_data(grditem["Item_Code", row].Value.ToString());
                }
            }
           
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
        }


       

        

       
        

        private void search_data(string Item_Code)
        {
            try
            {

                ini_form();
                Txtitem.Text = Item_Code;
                Conn.Close();
                Conn.Open();
                //textBox1.Text = Item_Code;
                saveToolStripButton.Enabled = true;
                toolRefund.Enabled = true;

                sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.ITM_CAT_CODE,h.UNIT,h.FRACTION,h.ALIAS_NAME,s.LAST_PUR_PRICE,s.RE_ORDER from hd_ITEMMASTER h  left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=1 and H.ITM_CAT_CODE=0 and h.Item_Code='" + Item_Code + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();



               

                rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {

                            Txtitem.Text = rd[0].ToString();
                            string catval;
                            txtname.Text = rd[1].ToString();
                            string ctcode=rd[2].ToString();

                            cmbcat.SelectedValue = ctcode;

                          ctcode = rd[3].ToString();


                          cmbunit.SelectedValue = ctcode;


                          txtcost.Text = rd[6].ToString();
                            
                            txtalias.Text = rd[5].ToString();
                            txtfraction.Text = rd[4].ToString();
                            
                            isedit = true;
                        }

                    }




                    //_load_stock();



                if (Gvar._SuperUserid != 1)
                {
                    saveToolStripButton.Enabled = false;


                }







               

                rd.Close();
                Conn.Close();
                isini = false;
            }
            }

            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }
        }



           
            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                string txt = textBox1.Text.Trim();
                if (txt != "")
                {
                    dv.RowFilter = "Item_Code LIKE  '%" + txt + "%' OR description LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Item_Code <> '0'";

                
               
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
                        textBox1.Text = grditem.CurrentRow.Cells[0].Value.ToString();
                        
                        search_data(textBox1.Text.Trim());
                      

                        break;
                    case 38:
                        
                            int crow = grditem.CurrentRow.Index;
                            int mros = grditem.Rows.Count;
                            // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                            //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                            if (crow > 0)
                                grditem.CurrentCell = grditem.Rows[crow - 1].Cells[0];

                       

                        break;
                    case 40:
                        
                             crow = grditem.CurrentRow.Index;
                            mros = grditem.Rows.Count;
                            // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                            //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                            if (crow < mros - 1)
                                grditem.CurrentCell = grditem.Rows[crow + 1].Cells[0];


                        break;

                }
            }

            

            private void Txtitem_Validated(object sender, EventArgs e)
            {
                search_data(Txtitem.Text);
            }

            private void grditem_RowEnter(object sender, DataGridViewCellEventArgs e)
            {
                if (grditem.CurrentCell != null)
                {

                    int row = e.RowIndex;
                    if (!issearch && row >= 0) search_data(grditem["Item_Code", row].Value.ToString());
                }
            }

            private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {

            }

            private void grdstock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
            {

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

                    crt = "";
                    switch (idx)
                    {
                        case 1:
                            {

                                //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                                rep_path = Gvar.report_path + "\\reports\\ItemMasterS.rpt";
                                CrRep.Load(rep_path);

                                CrRep.SummaryInfo.ReportTitle = "Service Report for all";
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

                    crconnectioninfo.ServerName = decoder.DataSource;

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

    }
}



