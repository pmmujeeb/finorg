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

    public partial class FrmItemMaster : Form
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
        int entrytype = 0;
        public FrmItemMaster()
        {
            InitializeComponent();

            txtpriv.Text = Gvar.frm_priv.ToString();
            ClearTextBoxes(this);
            isini = true;
            entrytype = Gvar.Gind;
            txttrn_type.Text = Gvar.trntype.ToString();
            Load_data();
            Load_grid();
            grditem.Visible = false;
            grdbutton.Rows.Add(1);
            grdmain.Focus();
            first_grdrow = 0;
            last_grdrow = 0;
            for (int i = 0; i < grdmain.Rows.Count; i++)
            {
                if (grdmain.Rows[i].Visible && first_grdrow == 0) first_grdrow = i;

                if (grdmain.Rows[i].Visible) last_grdrow = i;

            }

            if (grdmain.Rows[first_grdrow].Visible)
            {
                grdmain.CurrentCell = grdmain["colvalue", first_grdrow];
                grdmain.BeginEdit(false);
            }

            if (cmbcat.Items.Count > 1)
                cmbcat.SelectedIndex = 1;
            cmbcat.SelectedIndex = 0;
            isini = false;



        }



        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save_data();
        }


        private void load_sup_list(int ACC_NO)
        {
            try
            {
                Conn.Close();
                Conn.Open();

                sql = " SELECT ACCOUNTS.ACC_NO  ,ACC_NAME,ACC_MOBILE_NO,CONTACT_PERSON  ";
                sql = sql + " FROM ACCOUNTS INNER JOIN ACCOUNTS_INFO ON   ACCOUNTS.ACC_NO=ACCOUNTS_INFO.ACC_NO WHERE ACCOUNTS.ACC_NO  =" + ACC_NO;
                cmd = new SqlCommand(sql, Conn);
                SqlDataAdapter ada2 = new SqlDataAdapter(cmd);



                grdsup.Visible = true;

                SqlDataReader rd = cmd.ExecuteReader();
                grdsup.Rows.Clear();


                while (rd.Read())
                {
                    grdsup.Rows.Add();
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[0].Value = rd[0];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[1].Value = rd[1];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[2].Value = rd[2];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[3].Value = rd[3];


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = true;

                }
                rd.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }


        private void _load_stock()
        {
            // if (string.IsNullOrEmpty(Txtitem.Text)) return;
            Conn.Close();
            Conn.Open();
            try
            {
                sql = " SELECT WRHOUSE_MASTER.WR_NAME, WR_STOCK_MASTER.STOCK, WR_STOCK_MASTER.OP_STOCK, WRHOUSE_MASTER.WR_CODE FROM WR_STOCK_MASTER RIGHT JOIN WRHOUSE_MASTER ON (WR_STOCK_MASTER.WR_CODE = WRHOUSE_MASTER.WR_CODE AND WR_STOCK_MASTER.Item_Code='" + Txtitem.Text + "')";

                cmd = new SqlCommand(sql, Conn);

                SqlDataReader rd = cmd.ExecuteReader();
                grdstock.ReadOnly = false;
                //grdstock.BeginEdit(true);
                grdstock.Rows.Clear();
                isini = true;
                while (rd.Read())
                {

                    grdstock.Rows.Add();
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[0].Value = rd[0];
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[1].Value = rd[1];
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[2].Value = rd[2];
                    grdstock.Rows[grdstock.Rows.Count - 1].Cells[3].Value = rd[3];

                    grdstock.Columns[0].ReadOnly = true;
                    grdstock.Columns[1].ReadOnly = true;
                    //grdstock.Columns[2].ReadOnly = true;
                    //dt1.Text = rd["Cur_date"].ToString();
                    double v1;
                    double v2;
                    v1 = 0;
                    v2 = 0;
                    foreach (DataGridViewRow row in this.grdstock.Rows)
                    {



                        if (!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                        {
                            v1 += Convert.ToDouble(row.Cells[1].Value);


                        }


                        if (!string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            v2 += Convert.ToDouble(row.Cells[2].Value);


                        }

                    }
                    txtclstock.Text = v1.ToString();
                    txtopstock.Text = v2.ToString();
                    fnd = true;

                }
                rd.Close();
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


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
                sql = "sELECT  DISTINCT Item_Code,DESCRIPTION,ITM_CAT_CODE,AVG_PUR_PRICE ,BSTOCK from ITEMMASTER INNER JOIN AC_OPTIONS ON ITM_CAT_CODE <> RAW_ITM_CAT AND   ac_options.ID =1  where itm_cat_code <> 0";
                sqlcat = "sELECT  itm_cat_code,ITM_CAT_name froM ITEM_CAT where itm_cat_code<>0 ";
                if (entrytype == 2)
                {
                    sql = "sELECT  DISTINCT Item_Code,DESCRIPTION,ITM_CAT_CODE,AVG_PUR_PRICE ,BSTOCK from ITEMMASTER INNER JOIN AC_OPTIONS ON ITM_CAT_CODE = RAW_ITM_CAT AND     ac_options.ID =1  where itm_cat_code <> 0";
                    sqlcat = "sELECT  itm_cat_code,ITM_CAT_name  froM ITEM_CAT INNER JOIN AC_OPTIONS ON ITM_CAT_CODE = RAW_ITM_CAT AND   ac_options.ID =1 where itm_cat_code<>0 ";

                }
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();



                aditem = new SqlDataAdapter(cmd);

                itemdt = new DataTable("ITEMMASTER");
                aditem.Fill(itemdt);
                //grditem.Visible = true;
                dv.Table = itemdt;

                grditem.DataSource = dv;
                grditem.Columns[1].Width = 300;
                grditem.Columns[0].Width = 175;
                //MessageBox.Show(rd["isrefund"].ToString());
                //if (Gvar._SuperUserid != 1)
                //{
                //    saveToolStripButton.Enabled = false;


                //}
                sql = "select Branch_code,Branch_Name from BRANCHES ";
                SqlDataAdapter adabrn = new SqlDataAdapter(sql, Conn);
                DataTable dtbrn = new DataTable("branches");
                adabrn.Fill(dtbrn);

                cmbbranch.DisplayMember = "Branch_Name";
                cmbbranch.ValueMember = "Branch_code";

                cmbbranch.DataSource = dtbrn;
                cmbbranch.SelectedIndex = 0;

                if (cmbbranch.Items.Count < 2)
                {
                    cmbbranch.Visible = false;
                    lblbranch.Visible = false;
                }
               

                load_sup_list(-1);
                _load_stock();
                //rd.Close();


                //DataGridViewRow row = (DataGridViewRow)grdsup.Rows[0].Clone();
                //row.Cells["Column2"].Value = "XYZ";
                //row.Cells["Column6"].Value = 50.2;


                // grdsup.DataSource = dt2;
                //dgv1.Columns[1].Width = 300;

                sql = "sELECT  acc_no,acc_name froM accounts inner join ac_options on accounts.ACC_TYPE_CODE=ac_options.sup_ac_type AND   ac_options.ID =1 ";

                SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                DataTable dt1 = new DataTable("accounts");
                ada1.Fill(dt1);
                CMBSUP.DataSource = dt1;

                //sql = "sELECT  itm_cat_code,ITM_CAT_name froM ITEM_CAT where itm_cat_code<>0 ";

                SqlDataAdapter ada2 = new SqlDataAdapter(sqlcat, Conn);
                DataTable dt2 = new DataTable("ITEM_CAT");
                ada2.Fill(dt2);

                cmbcat.DataSource = dt2;
                cmbcatcode.DataSource = dt2;

                sqlunt = "sELECT  Unit_id,unit_name froM Unitmaster WHERE UNIT_TYPE='I'";

                SqlDataAdapter ada3 = new SqlDataAdapter(sqlunt, Conn);
                DataTable dt3 = new DataTable("Unitmaster");
                ada3.Fill(dt3);
                cmbunit.DataSource = dt3;



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

                crt = "";
                switch (idx)
                {
                    case 1:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\Rptitemmaster.rpt";
                            CrRep.Load(rep_path);
                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report for all";
                            //CrRep.SummaryInfo.ReportTitle = "Item Stock Report for all";
                        }


                        break;


                    case 2:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterbywh.rpt";
                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report By WareHouse for all";
                        }


                        break;

                    case 3:
                        {

                            crt = "{ITEMMASTER.ITM_CAT_CODE}  =" + cmbcat.SelectedValue;
                            // Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\Rptitemmaster.rpt";

                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report for Category " + cmbcat.Text;
                        }


                        break;


                    case 4:
                        {

                            crt = "{HD_ITEMMASTER.STOCK} <= {HD_ITEMMASTER.RE_ORDER} ";
                            // Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\Rptitemmaster.rpt";

                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report for Re-Order ";
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
                CrRep.OpenSubreport("HEADER.rpt").DataDefinition.FormulaFields["RPTHEAD"].Text = "'" + CrRep.SummaryInfo.ReportTitle + "'";

                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
                crconnectioninfo.DatabaseName = decoder.InitialCatalog;
                crconnectioninfo.UserID = decoder.UserID;
                crconnectioninfo.Password = decoder.Password;


                //crconnectioninfo.ServerName = "Mujeeb-pc";
                //crconnectioninfo.DatabaseName = "sqlStockex";
                //crconnectioninfo.UserID = "sa";
                //crconnectioninfo.Password = "sa0101";
                CrTables = CrRep.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtablelogoninfo = CrTable.LogOnInfo;
                    crtablelogoninfo.ConnectionInfo = crconnectioninfo;
                    CrTable.ApplyLogOnInfo(crtablelogoninfo);
                }







                CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);

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
                //if (Txtitem.Text.Trim().Length != 16)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 16 Digit!!");
                //    return;
                //}

                Conn.Close();
                Conn.Open();

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

                for (int i = 0; i < grdmain.Rows.Count; i++)
                {
                    if (grdmain["ismandatory", i].Value == "*" && grdmain["fieldval", i].Value == "")
                    {
                        MessageBox.Show(grdmain["caption", i].Value + " is Not Allowed Blank, Please Enter a Valid value!!", "Invalid Entry");
                        return;
                    }
                }
                if (txtfraction.Text == "") txtfraction.Text = "1";

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



                if (isedit == false)
                {


                    sql = "SELECT Item_Code FROM HD_ITEMMASTER where Item_Code ='" + Txtitem.Text.Trim() + "'";

                    cmd = new SqlCommand(sql, Conn);

                    rd = cmd.ExecuteReader();



                    if (rd.HasRows)
                    {


                        DialogResult result = MessageBox.Show("This Item Already Existing!!, Do You want to Update?", "Item Found", MessageBoxButtons.YesNoCancel);

                        if (result == DialogResult.Yes) isedit = true; else return;

                    }
                }


                if (cmbcat.SelectedIndex < 0)
                {
                    MessageBox.Show("Invalid Item Category", "Wrong Item Category");

                    return;
                }

                if (cmbunit.Text.Trim()=="")
                {
                    MessageBox.Show("Invalid Item Unit", "Wrong Item Unit");

                    return;
                }


                if (string.IsNullOrEmpty(Txtitem.Text.ToString()))
                {
                    return;
                }
                Conn.Close();
                Conn.Open();
                //Conn.BeginTransaction();
                fnd = false;
                double lst_price;
                double avg_price;
                double net_cost;

                if (txtcost.Text.Trim() == "")
                    lst_price = 0;
                else
                    lst_price = Convert.ToDouble(txtcost.Text); // / Convert.ToDouble(txtfraction.Text);

                if (txtlastprice.Text.Trim() == "")
                    avg_price = 0;
                else
                    avg_price = Convert.ToDouble(txtlastprice.Text); /// Convert.ToDouble(txtfraction.Text);

                if (avg_price == 0) avg_price = lst_price;

                if (txtlastexp.Text.Trim() == "")
                    txtlastexp.Text = "0";
                net_cost = avg_price + Convert.ToDouble(txtlastexp.Text);
                DataGridViewRow row;
                row = grdmain.Rows
                         .Cast<DataGridViewRow>()
                         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                         .First();
                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "";
                string txtarabic = row.Cells["colvalue"].Value.ToString();
                string txtbarcode = "";
                if (grdbarcode.Rows.Count > 0)
                {
                    if (grdbarcode["barcode", 0].Value != null)
                        txtbarcode = grdbarcode["barcode", 0].Value.ToString();
                    else
                    {
                        row = grdmain.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("BARCODE"))
                        .First();

                        if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "";
                        if (row.Cells["colvalue"].Value == "") row.Cells["colvalue"].Value = Txtitem.Text;
                        txtbarcode = row.Cells["colvalue"].Value.ToString();

                    }



                }

                if (txtbarcode.Trim() == "") txtbarcode = Txtitem.Text;

                row = grdmain.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("SALE_PRICE"))
                    .First();
                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";

                object txtsaleprice = row.Cells["colvalue"].Value;
                if (txtsaleprice == "") txtsaleprice = "0";

                row = grdmain.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RE_ORDER"))
                    .First();
                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";
                object txtorder = row.Cells["colvalue"].Value;
                if (txtorder == "") txtorder = "0";
                row = grdmain.Rows
                   .Cast<DataGridViewRow>()
                   .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RETAIL_PRICE"))
                   .First();
                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";

                object txtretailprice = row.Cells["colvalue"].Value;
                if (txtretailprice == "") txtretailprice = "0";
                if (txtcost.Text == "") txtcost.Text = "0";

                if (Convert.ToDecimal(txtcost.Text) > Convert.ToDecimal(txtsaleprice) || Convert.ToDecimal(txtcost.Text) > Convert.ToDecimal(txtretailprice))
                {
                    DialogResult result = MessageBox.Show("Either Wholesale Price Or Retail Price Less Than Cost Price!!!, Do you want to Continue??", "Invalid Sale Prices", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        Conn.Close();
                        return;

                    }
                }





                if (txtminprofit.Text.Trim() == "")
                    txtminprofit.Text = "0";
                if (txtlastexp.Text.Trim() == "")
                    txtlastexp.Text = "0";
                if (txtsaleprofit.Text.Trim() == "")
                    txtsaleprofit.Text = "0";

                if (txtminstock.Text.Trim() == "")
                    txtminstock.Text = "0";
                if (txtvat.Text.Trim() == "")
                    txtvat.Text = "0";
                iserror = false;

                ADODB.Connection ADOconn = new ADODB.Connection();
                if(ADOconn.State==0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


                if (isedit == false)
                {

                    sql = "INSERT INTO [HD_ITEMMASTER]([Item_Code],[DESCRIPTION],AR_DESC,[USER],[ITM_CAT_CODE],[UNIT],[FRACTION],[ALIAS_NAME],[BRN_CODE],BARCODE,[VAT_PERCENT])";
                    sql = sql + " VALUES ('" + Txtitem.Text.Trim() + "','" + txtname.Text.Trim() + "','" + txtname.Text + "','" + Gvar._Userid + "','" + cmbcat.SelectedValue + "','" + cmbunit.Text + "','" + txtfraction.Text + "','" + txtarabic + "'," + cmbbranch.SelectedValue + ",'" + txtbarcode + "',"+ txtvat.Text + ")";

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                    Recordset rec = new ADODB.Recordset();
                    rec.Open("SELECT @@IDENTITY", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //rec.GetRows();

                    txtitemno.Text = rec.Fields[0].Value.ToString();

                    sql = " INSERT INTO [STOCK_MASTER]([Item_Code],[STOCK],[LAST_PUR_PRICE],[AVG_PUR_PRICE],[USER1],[RE_ORDER],[BRN_CODE],[OP_STOCK],sale_price,retail_price,profit,expense_amt,net_cost,SALES_PROFIT_PCNT,[VAT_PERCENT])";
                    sql = sql + " VALUES ('" + Txtitem.Text.Trim() + "','" + txtclstock.Text + "','" + lst_price + "','" + avg_price + "','" + Gvar._Userid + "','" + txtorder + "'," +cmbbranch.SelectedValue + ",'" + txtopstock.Text + "','" + txtsaleprice + "','" + txtretailprice + "','" + txtminprofit.Text + "','" + txtlastexp.Text + "','" + net_cost + "','" + txtsaleprofit.Text + "',"+  txtvat.Text + " )";

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                }
                else
                {

                    if (Txtitem.Text.Trim() != txtolditm.Text.Trim() && txtolditm.Text.Trim() != "")
                    {

                        DialogResult result = MessageBox.Show("Do You want to Update The Item Code from " + Txtitem.Text.Trim() + " To " + txtolditm.Text.Trim() + "?", "Confirm Item Update", MessageBoxButtons.YesNoCancel);

                        if (result != DialogResult.Yes)
                        {
                            Conn.Close();
                            return;

                        }
                    }
                   
                    row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("ALIAS_NAME"))
                      .First();
                    if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "";

                    // object txtarabic = row.Cells["colvalue"].Value;


                    object txtalias = row.Cells["colvalue"].Value;


                    sql = "update  [HD_ITEMMASTER]  set [Item_Code]='" + Txtitem.Text.Trim() + "',[DESCRIPTION]='" + txtname.Text + "',[ar_desc]='" + txtarabic + "',[ITM_CAT_CODE]='" + cmbcat.SelectedValue + "',[UNIT]='" + cmbunit.Text + "',[FRACTION]='" + txtfraction.Text + "',[ALIAS_NAME]='" + txtalias + "',BARCODE ='" + txtbarcode + "',VAT_PERCENT=" + txtvat.Text + "  where Item_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();




                    sql = " update  [STOCK_MASTER] set [Item_Code]='" + Txtitem.Text.Trim() + "',[STOCK]='" + txtclstock.Text + "',[AVG_PUR_PRICE]='" + avg_price + "',[LAST_PUR_PRICE]='" + lst_price + "',[RE_ORDER]='" + txtorder + "',[OP_STOCK]='" + txtopstock.Text + "',sale_price='" + txtsaleprice + "',retail_price='" + txtretailprice + "', [profit]='" + txtminprofit.Text.Trim() + "',[expense_amt]='" + txtlastexp.Text.Trim() + "',[net_cost]='" + net_cost + "',SALES_PROFIT_PCNT='" + txtsaleprofit.Text + "' where Item_Code ='" + txtolditm.Text + "'";



                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                    sql = " update  [wr_STOCK_MASTER] set [Item_Code]='" + Txtitem.Text.Trim() + "' where Item_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                    sql = " update  [data_entry_grid] set [Item_Code]='" + Txtitem.Text.Trim() + "' where Item_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                    sql = " update  [trn_itm_detail] set [Item_Code]='" + Txtitem.Text.Trim() + "' where Item_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();



                }


                


                sql = "SELECT Item_Code FROM STOCK_MASTER where Item_Code ='" + Txtitem.Text.Trim() + "'";

                cmd = new SqlCommand(sql, Conn);

                rd = cmd.ExecuteReader();



                if (!rd.HasRows)
                {

                    rd.Close();
                    sql = " INSERT INTO [STOCK_MASTER]([Item_Code],[STOCK],[LAST_PUR_PRICE],[AVG_PUR_PRICE],[USER1],[RE_ORDER],[BRN_CODE],[OP_STOCK],sale_price,retail_price)";
                    sql = sql + " VALUES ('" + Txtitem.Text.Trim() + "','" + txtclstock.Text + "','" + lst_price + "','" + avg_price + "','" + Gvar._Userid + "','" + txtorder + "',"+ cmbbranch.SelectedValue + ",'" + txtopstock.Text + "','" + txtsaleprice + "','" + txtretailprice + "')";

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                }
                rd.Close();
                string v1;
                string v2;



                sql = " delete from item_supplier where Item_Code ='" + Txtitem.Text.Trim() + "'";


                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                foreach (DataGridViewRow row1 in this.grdsup.Rows)
                {
                    //foreach (DataGridViewCell cell in row.Cells)
                    if (!string.IsNullOrEmpty(row1.Cells[0].Value.ToString()))
                    {
                        v1 = row1.Cells[0].Value.ToString();
                        v2 = row1.Cells[1].Value.ToString();
                        sql = " INSERT INTO [ITEM_SUPPLIER] ([Item_Code],[SUPLIER_CODE]) VALUES('" + Txtitem.Text.Trim() + "','" + v1 + "')";

                        cmd = new SqlCommand(sql, Conn);

                        cmd.ExecuteNonQuery();
                    }

                    //if (dgv1.Rows[row.  cell.Size.IsEmpty)
                    //{

                    // }
                    //MessageBox.Show(cell.Value.ToString());

                }
                grdbarcode.EndEdit();

                
                    add_barcode();

                  


                ADODB.Recordset bcode = new ADODB.Recordset();
                string barcode = "'-9'";
                for (int i = 0; i <= grdbarcode.Rows.Count - 1; i++)
                {
                    if (grdbarcode["barcode", i].Value != null && grdbarcode["barcode", i].Value != "")
                    {

                        sql = "SELECT * FROM barcode WHERE barcode ='" + grdbarcode["barcode", i].Value + "'";

                        bcode = new ADODB.Recordset();


                        bcode.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (grdbarcode["saleprice1", i].Value == "") grdbarcode["saleprice1", i].Value = 0;
                        if (grdbarcode["saleprice2", i].Value == "") grdbarcode["saleprice2", i].Value = 0;
                        if (bcode.RecordCount == 0) bcode.AddNew();
                        barcode = barcode + ",'" + grdbarcode["barcode", i].Value + "'";
                        bcode.Fields["barcode"].Value = grdbarcode["barcode", i].Value;
                        bcode.Fields["unit"].Value = grdbarcode["unit", i].Value;
                        bcode.Fields["description"].Value = grdbarcode["description", i].Value;
                        bcode.Fields["fraction"].Value = grdbarcode["fraction", i].Value;
                        bcode.Fields["retail_price"].Value = grdbarcode["saleprice2", i].Value;
                        bcode.Fields["sale_price"].Value = grdbarcode["saleprice1", i].Value;
                        if (bcode.Fields["retail_price"].Value == "") bcode.Fields["retail_price"].Value = 0;
                        if (bcode.Fields["sale_price"].Value == "") bcode.Fields["sale_price"].Value = 0;

                        bcode.Fields["item_code"].Value = Txtitem.Text;
                        bcode.Fields["ITM_CAT_CODE"].Value = cmbcat.SelectedValue;
                        bcode.Fields["brn_code"].Value = cmbbranch.SelectedValue;
                        bcode.Fields["item_id"].Value = Txtitem.Text + "-" + grdbarcode["unitid", i].Value;
                        bcode.Fields["description_ar"].Value = grdbarcode["descriptionAr", i].Value;
                        if (i == 0) bcode.Fields["main_id"].Value = 1; else bcode.Fields["main_id"].Value = 0;
                        bcode.Update();


                    }


                }


                if (barcode == "'-9'")
                {

                    bcode = new ADODB.Recordset();
                    //sql = "SELECT * FROM barcode WHERE barcode ='" + txtbarcode.Text.Trim() + "'";

                    //bcode.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //if (bcode.RecordCount == 0) bcode.AddNew();
                    //barcode = barcode + ",'" + txtbarcode.Text.Trim() + "'";
                    //bcode.Fields["barcode"].Value = txtbarcode.Text.Trim();
                    //bcode.Fields["unit"].Value = cmbunit.Text;
                    //bcode.Fields["description"].Value = txtname.Text.Trim();
                    //bcode.Fields["fraction"].Value = txtfraction.Text;
                    //bcode.Fields["retail_price"].Value = txtretailprice.Text;
                    //bcode.Fields["sale_price"].Value = txtsaleprice.Text;
                    //if (bcode.Fields["retail_price"].Value == "") bcode.Fields["retail_price"].Value = 0;
                    //if (bcode.Fields["sale_price"].Value == "") bcode.Fields["sale_price"].Value = 0;
                    //bcode.Fields["item_code"].Value = Txtitem.Text;
                    //bcode.Fields["brn_code"].Value = cmbbranch.SelectedValue;
                    //bcode.Fields["item_id"].Value = Txtitem.Text + "-00";
                    //bcode.Fields["description_ar"].Value = txtarabic.Text;
                    //if (bcode.Fields["description_ar"].Value == "") bcode.Fields["description_ar"].Value = txtname.Text.Trim(); ;
                    // bcode.Fields["main_id"].Value = 1; 
                    //bcode.Update();

                }

                sql = "delete from barcode where item_code='" + Txtitem.Text + "' and barcode not in (" + barcode + ")";
                if (Conn.State == 0) Conn.Open();
                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();
                update_stock();

                isedit = true;




                Conn.Close();

                if (pictureBox1.Image != null)
                {
                    save_foto(isedit);
                }

                isini = false;


                Load_data();
                MessageBox.Show("Successfully Added/Updated Item", "Successfull");

                return;


                // MessageBox.Show("Successfully Inserted New reciept", "Successfull");
            }

            catch (Exception excep)
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
                        MessageBox.Show("Invalid Item Code, PLease Try Again", "Plese Enter Correct Value");
                        Conn.Close();
                        return;
                    }
                }
                else
                {
                    Txtitem.Text = "";
                    MessageBox.Show("Invalid Item Code, Please Try Again", "Plese Enter Correct Value");
                    Conn.Close();
                    return;

                }

                DialogResult result = MessageBox.Show("Do You want to Delete The Item Code " + Txtitem.Text + "?", "Delete Item", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    Conn.Close();
                    Conn.Open();


                    sql = "delete from   [HD_ITEMMASTER]  where Item_Code ='" + txtolditm.Text + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();



                    sql = "delete from   [STOCK_MASTER]  where Item_Code ='" + txtolditm.Text + "'";



                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                    sql = "delete from  [wr_STOCK_MASTER]  where Item_Code ='" + txtolditm.Text + "'";

                    //sql = "insert into RecieptsLog(Reciept_no , Cur_date, Reg_date, Employee_no, Master_sheet_no, Purpose, No_Participants, Amount, Remarks, user_id, isRefund, Entry_Type)";
                    //sql = sql + " SELECT Reciept_no , Cur_date, Reg_date, Employee_no, Master_sheet_no, Purpose, No_Participants, Amount, Remarks, " + Gvar._Userid + ", isRefund, Entry_Type FROM Reciepts where reciept_no=" + Convert.ToDouble(Txtitem.Text);
                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();
                    //sql = "Delete from  Reciepts where reciept_no =" + Convert.ToDouble(Txtitem.Text);
                    //Conn.Close();
                    //Conn.Open();
                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();
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
            if (cmbunit.Items.Count > 0) cmbunit.SelectedIndex = 0;


            ClearTextBoxes(this);
            txtsearch.Text = "";
            toolRefund.Enabled = true;
            dt1.Value = DateTime.Now;

            Gvar.ArCalendar(dt1.Value);
            saveToolStripButton.Enabled = true;
            if (Gvar._SuperUserid != 1)
            {
                toolRefund.Enabled = false;
                tooldelete.Enabled = false;

            }


            foreach (DataGridViewRow row1 in this.grdmain.Rows)
            {
                //foreach (DataGridViewCell cell in row.Cells)

                row1.Cells["colvalue"].Value = "";
                row1.Cells["fieldval"].Value = "";
            }



            grdsup.Rows.Clear();
            grdbarcode.Rows.Clear();
            grdstock.Rows.Clear();
            isedit = false;
            isini = true;
            pictureBox1.Image = null;
            _load_stock();
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
            object trntype = txttrn_type.Text;
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
            txttrn_type.Text = trntype.ToString();
            //lblrefund.Text = "";


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
            txtsearch.Focus();
        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {

            issearch = false;
            //if (textBox1.Text.Trim() != "")
            //{
            //    search_data(textBox1.Text.Trim());

            //}
            //else
            //{
            //if (grditem.CurrentCell != null)
            //{

            //    int row = grditem.CurrentCell.RowIndex;
            //    if (!issearch && row >= 0) search_data(grditem["Item_Code", row].Value.ToString());
            //}

            if (Txtitem.Text.Trim() != "")
                search_data(Txtitem.Text);
            //}

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
                txtsearch.Text = Txtitem.Text;
                search_data(txtsearch.Text.Trim());
            }
            grdmain.ClearSelection();
            grdbarcode.ClearSelection();
            grdstock.ClearSelection();
            acntrl = "";
        }


        private void btnaddsup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Txtitem.Text)) return;
                Conn.Close();


                foreach (DataGridViewRow row in this.grdsup.Rows)
                {
                    //foreach (DataGridViewCell cell in row.Cells)
                    if (row.Cells[0].Value.ToString() == CMBSUP.SelectedValue.ToString())
                    {
                        return;
                    }

                    //if (dgv1.Rows[row.  cell.Size.IsEmpty)
                    //{

                    // }
                    //MessageBox.Show(cell.Value.ToString());

                }


                Conn.Open();


                sql = " SELECT accounts.ACC_NO  ,ACC_NAME,ACC_MOBILE_NO,CONTACT_PERSON  ";
                sql = sql + " FROM ACCOUNTS left join accounts_info on accounts.acc_no=accounts_info.acc_no WHERE accounts.ACC_NO  =" + CMBSUP.SelectedValue;

                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();


                while (rd.Read())
                {
                    grdsup.Rows.Add();
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[0].Value = rd[0];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[1].Value = rd[1];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[2].Value = rd[2];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[3].Value = rd[3];


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = true;

                }



                Conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void grdstock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (isini) return;
            double v1;
            double v2;
            v1 = 0;
            v2 = 0;

            foreach (DataGridViewRow row in this.grdstock.Rows)
            {



                if (!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                {
                    v1 += Convert.ToDouble(row.Cells[1].Value);


                }


                if (!string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                {
                    v2 += Convert.ToDouble(row.Cells[2].Value);


                }

            }
            txtclstock.Text = v1.ToString();
            txtopstock.Text = v2.ToString();



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void update_stock()
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            rd.Close();
            int k= 0;
            decimal rec_no = 0;
            string v1;
            string v2;
            try
            {
                // string strConn
                //    "Provider=SQLOLEDB;Initial Catalog=[database];Data Source=[server];";
                //conn.Open(strConn, [user], [pwd], 0);
                //ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                grdstock.EndEdit();
                trn_no = 1;

                foreach (DataGridViewRow row in this.grdstock.Rows)
                {
                    
                    //foreach (DataGridViewCell cell in row.Cells)
                    if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    //if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()) && !string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(row.Cells[2].Value.ToString())) row.Cells[2].Value = 0;
                        v1 = row.Cells[3].Value.ToString();
                        v2 = row.Cells[2].Value.ToString();
                        k++;
                        if (k == 1)
                        {
                            cus = new ADODB.Recordset();

                            sql = "SELECT * FROM TRN_MASTER1 WHERE TRN_TYPE =" + txttrn_type.Text + " AND  INV_NO ='" + txtitemno.Text + "' and  WR_CODE =" + v1;
                            cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            if (cus.RecordCount == 0)
                            {

                                trn_no = 1;

                                tmp = new ADODB.Recordset();


                                sql = "SELECT TRNNO FROM TRN_NO";

                                cmd = new SqlCommand(sql, Conn);

                                rd = cmd.ExecuteReader();



                                if (rd.HasRows)
                                {
                                    while (rd.Read())
                                    {
                                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                                        {

                                            trn_no = Convert.ToDouble(rd[0]);

                                            fnd = true;
                                        }

                                    }
                                }

                                //cus.Fields["trn_no"].Value = trn_no;




                                rd.Close();




                                cus.AddNew();
                                cus.Fields["DATE_TIME"].Value = DateTime.Now;
                                cus.Fields["trn_no"].Value = trn_no;
                                cus.Fields["brn_code"].Value = cmbbranch.SelectedValue;
                            }

                            //With CUS
                            if (txtcost.Text == "") txtcost.Text = "0";
                            trn_no = Convert.ToDouble(cus.Fields["trn_no"].Value);
                            cus.Fields["WR_CODE"].Value = v1;


                            cus.Fields["INV_NO"].Value = txtitemno.Text;

                            cus.Fields["cus_code"].Value = 5016;
                            cus.Fields["cus_name"].Value = "OPENING BALANCE";
                            cus.Fields["trn_type"].Value = 0;
                            if (txtopstock.Text == "'") txtopstock.Text = "0";
                            cus.Fields["TOT_AMOUNT"].Value = Convert.ToDouble(txtopstock.Text) * Convert.ToDouble(txtcost.Text);
                            cus.Fields["NET_AMOUNT"].Value = cus.Fields["TOT_AMOUNT"].Value;
                            cus.Fields["FNET_AMOUNT"].Value = cus.Fields["TOT_AMOUNT"].Value;
                            cus.Fields["DISCOUNT"].Value = 0;
                            cus.Fields["user_ID"].Value = Gvar.Userid;
                            cus.Fields["SALE_TYPE"].Value = entrytype;
                            cus.Update();

                        }



                        cus = new ADODB.Recordset();


                        sql = " SELECT * FROM TRN_ITM_DETAIL1 WHERE    WR_CODE ='" + v1 + "' AND TRN_TYPE = 0 AND Item_Code ='" + Txtitem.Text.Trim() + "'";

                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (cus.RecordCount < 1)
                        {
                            cus.AddNew();
                        }

                        cus.Fields["trn_no"].Value = trn_no;
                        if (v1 == "") v1 = "0";
                        if (txtcost.Text.Trim() == "") txtcost.Text = "0";
                        //if (txtcost.Text.Trim() == "") txtcost.Text = "0";
                        if (txtlastprice.Text.Trim() == "") txtlastprice.Text = "0";

                        cus.Fields["WR_CODE"].Value = v1;
                        cus.Fields["brn_code"].Value =cmbbranch.SelectedValue;

                        cus.Fields["Item_Code"].Value = Txtitem.Text.Trim();
                        cus.Fields["Invoice_no"].Value = v1;
                        cus.Fields["QTY"].Value = Convert.ToDouble(v2);
                        cus.Fields["UNIT"].Value = cmbunit.Text;
                        cus.Fields["UNIT_QTY"].Value = Convert.ToDecimal(v2) * Convert.ToDecimal(txtfraction.Text);
                        cus.Fields["PRICE"].Value = Convert.ToDouble(txtcost.Text);
                        cus.Fields["fPRICE"].Value = Convert.ToDecimal(txtcost.Text) * Gvar._cur_rate;
                        cus.Fields["DISCOUNT"].Value = 0;
                        cus.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDouble(txtcost.Text);
                        cus.Fields["trn_type"].Value = 0;
                        cus.Update();


                        //End With



                        ADODB.Recordset rec = new ADODB.Recordset();


                        // SAVE TO DATA_GRID
                        if (k == 1)
                        {
                           
                            sql = "SELECT * FROM DATA_ENTRY WHERE   INVOICE_NO = '" + txtitemno.Text + "' and wr_code ='" + v1 + "' AND TRN_TYPE=0";

                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            if (rec.RecordCount == 0)
                            {

                                rec.AddNew();
                                rec.Fields["flag"].Value = "N";

                                rec.Fields["CURDATE"].Value = DateTime.Now;
                            }

                            rec.Fields["INVOICE_NO"].Value = txtitemno.Text;
                            rec.Fields["TRAN_NO"].Value = trn_no;
                            //rec.Fields["CURDATE"].Value = dt1.Value;
                            rec.Fields["trn_type"].Value = 0;
                            if (rec.Fields["ORG_DUP"].Value == null)
                                rec.Fields["ORG_DUP"].Value = "O";
                            rec.Fields["ACCODE"].Value = 5016;
                            rec.Fields["REF_NO"].Value = entrytype;
                            rec.Fields["ename"].Value = "OPENING BALANCE";
                            rec.Fields["sales_code"].Value = entrytype;
                            //rec.Fields["ordr_no"].Value = 0;
                            //rec.Fields["QOUT_NO"].Value =0;

                            rec.Fields["WR_CODE"].Value = v1;
                            rec.Fields["BRN_code"].Value = cmbbranch.SelectedValue;
                            // rec.Fields["COST_CODE"].Value = 0;
                            rec.Fields["ENTRY_TYPE"].Value = "OPSTOCK";
                            rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txtopstock.Text) * Convert.ToDouble(txtcost.Text); ;
                            rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtopstock.Text) * Convert.ToDouble(txtcost.Text); ;
                            rec.Fields["remarks"].Value = "";

                            rec.Update();
                            sql = "SELECT rec_no FROM DATA_ENTRY WHERE    INVOICE_NO = '" + txtitemno.Text + "' and  wr_code ='" + v1 + "' AND TRN_TYPE=0";
                            rec = new ADODB.Recordset();
                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                             rec_no = (decimal)rec.Fields["REC_NO"].Value;
                        }

                        
                        var a = 0;
                        //sql = "DELETE FROM DATA_ENTRY_GRID WHERE REC_NO=" + rec_no;
                        ////ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)
                        //tmp = new Recordset();
                        //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        rec = new ADODB.Recordset();

                        sql = "SELECT * FROM DATA_ENTRY_GRID WHERE  BRN_CODE = " + cmbbranch.SelectedValue + " AND  wr_code ='" + v1 + "' and Item_Code = '" + Txtitem.Text.Trim() + "' and trn_type =0";


                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        int i = 0;



                        if (rec.RecordCount == 0)
                        {
                            rec.AddNew();
                            rec.Fields["REC_NO"].Value = Convert.ToInt64(rec_no);
                            tmp = new ADODB.Recordset();

                            sql = "SELECT max(ROWNUM) FROM DATA_ENTRY_GRID WHERE REC_NO ='" + rec_no + "'";

                            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            if (tmp.Fields[0].Value != DBNull.Value)

                                rec.Fields["ROWNUM"].Value = Convert.ToInt32(tmp.Fields[0].Value) + 1;
                            else
                                rec.Fields["ROWNUM"].Value = "1";
                        }


                        rec.Fields["Item_Code"].Value = Txtitem.Text.Trim();
                        rec.Fields["PRICE"].Value = Convert.ToDouble(txtcost.Text);
                        rec.Fields["Description"].Value = txtname.Text.Trim();
                        rec.Fields["QTY"].Value = Convert.ToDouble(v2);
                        //rec.Fields["RQTY"].Value = 0;
                        rec.Fields["BARCODE"].Value = Txtitem.Text.Trim();
                        rec.Fields["FRACTION"].Value = Convert.ToDouble(txtfraction.Text);
                        rec.Fields["UNIT"].Value = cmbunit.Text;

                        //rec.Fields["plot"].Value = "";
                        rec.Fields["UNIT_QTY"].Value = Convert.ToDecimal(v2) * Convert.ToDecimal(txtfraction.Text);
                        rec.Fields["brn_code"].Value = cmbbranch.SelectedValue;
                        rec.Fields["REMARKS"].Value = "OP_STOCK";
                        rec.Fields["trn_type"].Value = 0;
                        rec.Fields["wr_code"].Value = v1;
                        rec.Fields["INVOICE_NO"].Value = txtitemno.Text; 

                        if (Convert.ToDecimal(txtfraction.Text) > 0)
                        {
                            rec.Fields["UNIT_PRICE"].Value = Convert.ToDecimal(txtcost.Text) / Convert.ToDecimal(txtfraction.Text);
                            rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                            rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(txtcost.Text) / Convert.ToDecimal(txtfraction.Text);

                        }
                        else
                        {
                            rec.Fields["UNIT_PRICE"].Value = Convert.ToInt32(txtcost.Text);
                            rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                            rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(txtcost.Text);

                        }
                        rec.Update();



                        // END SAVE TO DATAGRID



                        tmp = new ADODB.Recordset();

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + v1 + " AND Item_Code='" + Txtitem.Text.Trim() + "'";
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, 1);




                        double ST = 0;
                        if (tmp.Fields[0].Value != DBNull.Value) ST = (double)tmp.Fields[0].Value;
                        cus = new ADODB.Recordset();

                        sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + v1 + " AND Item_Code='" + Txtitem.Text.Trim() + "'";
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (cus.RecordCount == 0) cus.AddNew();


                        //With CUS
                        cus.Fields["WR_CODE"].Value = v1;
                        cus.Fields["brn_code"].Value = cmbbranch.SelectedValue;
                        cus.Fields["Item_Code"].Value = Txtitem.Text.Trim();
                        cus.Fields["User"].Value = Gvar.Userid;
                        //cus.Fields["LOCATION"].Value 
                        cus.Fields["stock"].Value = ST;
                        //Grid12.TextMatrix(i, 2) = ST
                        cus.Fields["OP_stock"].Value = v2;
                        cus.Update();
                        //.Update
                        //End With

                        //End If
                        //Next i


                        decimal amount;
                        amount = Convert.ToDecimal(v2) * Convert.ToDecimal(txtcost.Text); ;
                        updat_accounts(Txtitem.Text.Trim(), amount);



                    }

                }

                sql = "SELECT sum(STOCK) FROM  WR_STOCK_MASTER WHERE  Item_Code='" + Txtitem.Text.Trim() + "'";

                cmd = new SqlCommand(sql, Conn);

                rd = cmd.ExecuteReader();
                sql = "";
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {

                            trn_no = Convert.ToDouble(rd[0]);
                            sql = "Update STOCK_MASTER set stock =" + trn_no + " where    Item_Code='" + Txtitem.Text.Trim() + "' AND BRN_CODE=" + cmbbranch.SelectedValue;

                            fnd = true;
                        }

                    }
                }
                rd.Close();

                if (sql != "")
                {

                    cmd = new SqlCommand(sql, Conn);
                    cmd.ExecuteNonQuery();
                }

                //Set TMP = New ADODB.Recordset
                //TMP.Open "SELECT STOCK FROM  WR_STOCK_MASTER WHERE  Item_Code='" & Trim(Text1(0)) & "' AND BRN_CODE=" & brn_code, con, 2, 3

                //If TMP.RecordCount > 0 Then
                //con.Execute "Update STOCK_MASTER set stock = " & Round(TMP(0), 2) & " where   Item_Code ='" & Trim(Text1(0)) & "' AND BRN_CODE=" & brn_code, a
                //TXTSTOCK(0) = TMP(0)
                //End If

            }


            catch (Exception e)
            {
                MessageBox.Show(this, "Error" + e.Message);
            }
            finally
            {
                Conn.Close();
            }
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

                sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.ITM_CAT_CODE,h.UNIT,h.FRACTION,h.ALIAS_NAME,s.AVG_PUR_PRICE,s.RE_ORDER,S.AVG_PUR_PRICE,H.BARCODE,H.AR_DESC,SALE_PRICE,RETAIL_PRICE,PART_NO,BRAND,SUB_CAT_CODE,h.item_no,H.VAT_PERCENT from hd_ITEMMASTER h left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=" + cmbbranch.SelectedValue + "  and h.Item_Code='" + Item_Code + "'";


                
                
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                //SqlDataReader rd = cmd.ExecuteReader();
                try
                {
                  //  rd1 = cmd.ExecuteReader();



                    if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {

                            Txtitem.Text = rd[0].ToString();
                            //txtarabic.Text = rd["AR_DESC"].ToString();
                            //txtalias.Text = rd["alias_name"].ToString();
                            txtolditm.Text = rd[0].ToString();
                            string catval;
                            txtname.Text = rd[1].ToString();
                            string ctcode = rd[2].ToString();

                            

                            cmbcat.SelectedValue = ctcode;
                            // txtbarcode.Text = rd["BARCODE"].ToString();

                            ctcode = rd[3].ToString();
                            cmbunit.Text = ctcode;

                            txtitemno.Text = rd["item_no"].ToString();
                            txtvat.Text = rd["VAT_PERCENT"].ToString();





                            if (!string.IsNullOrEmpty(rd[0].ToString()))
                            {


                                for (int i = 0; i <= grdmain.Rows.Count - 1; i++)
                                {
                                    int a = grdmain["tablename", i].Value.ToString().IndexOf("Hd_Itemmaster");
                                    if (grdmain["tablename", i].Value.ToString() != "" && grdmain["fieldname", i].Value.ToString() != "")
                                    {
                                        if (grdmain["fieldname", i].Value != null)
                                        {


                                            // rec.Fields[grdmain["fieldname", i].Value.ToString()].Value =
                                            grdmain["fieldval", i].Value = rd[grdmain["fieldname", i].Value.ToString()].ToString();
                                            grdmain["colvalue", i].Value = rd[grdmain["fieldname", i].Value.ToString()].ToString();


                                        }

                                    }

                                }



                                isedit = true;
                            }



                        }
                   
                    
                
                    



                        //txtarabic.Text = rd[5].ToString();
                        txtfraction.Text = rd[4].ToString();
                        txtlastprice.Text = rd[6].ToString();

                        //txtorder.Text = rd[7].ToString();
                        txtcost.Text = rd[8].ToString();
                        isedit = true;
                    }
                        //Txtitem.Focus();
                    }


                
                else
                {
                    return;
                }
            
                }
                        catch (SqlException sq)
                {

                    }




                _load_stock();

                search_suplier();
                search_barcode();
                search_foto();
                rd.Close();
                Conn.Close();
                // grdmain.CurrentCell = grdmain[2, 5];
                grdmain.CurrentCell = grdmain[2, first_grdrow];

                isini = false;
                grdmain.ClearSelection();
                grdbarcode.ClearSelection();
                grdstock.ClearSelection();
            }
            //}

            catch (Exception excep)
            {

                MessageBox.Show(excep.Message);

            }
        }



        private void search_suplier()
        {
            try
            {
                if (string.IsNullOrEmpty(Txtitem.Text)) return;
                Conn.Close();




                Conn.Open();


                sql = "SELECT ACCOUNTS.ACC_NO  ,ACC_NAME,ACC_MOBILE_NO,CONTACT_PERSON  ";
                sql = sql + " FROM ACCOUNTS left join accounts_info on accounts.acc_no=accounTs_info.acc_no  left join item_supplier on accounts.acc_no=item_supplier.suplier_code  WHERE Item_Code  ='" + Txtitem.Text.Trim() + "'";


                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                grdsup.Rows.Clear();
                isini = true;
                while (rd.Read())
                {
                    grdsup.Rows.Add();
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[0].Value = rd[0];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[1].Value = rd[1];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[2].Value = rd[2];
                    grdsup.Rows[grdsup.Rows.Count - 1].Cells[3].Value = rd[3];


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = true;

                }

                isini = false;

                Conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void search_barcode()
        {
            try
            {
                if (string.IsNullOrEmpty(Txtitem.Text)) return;
                Conn.Close();




                Conn.Open();


                sql = "SELECT *  from barcode where item_code='" + Txtitem.Text.Trim() + "'";



                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                grdbarcode.Rows.Clear();
                isini = true;
                while (rd.Read())
                {
                    grdbarcode.Rows.Add();



                    grdbarcode["barcode", grdbarcode.Rows.Count - 1].Value = rd["barcode"];
                    grdbarcode["description", grdbarcode.Rows.Count - 1].Value = rd["description"];
                    grdbarcode["unit", grdbarcode.Rows.Count - 1].Value = rd["unit"];
                    grdbarcode["fraction", grdbarcode.Rows.Count - 1].Value = rd["fraction"];
                    grdbarcode["itemcode", grdbarcode.Rows.Count - 1].Value = rd["item_code"]; ;
                    grdbarcode["saleprice1", grdbarcode.Rows.Count - 1].Value = rd["sale_price"];
                    grdbarcode["saleprice2", grdbarcode.Rows.Count - 1].Value = rd["retail_price"];
                    grdbarcode["descriptionAr", grdbarcode.Rows.Count - 1].Value = rd["description_ar"];


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = true;

                }

                isini = false;

                Conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
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
                        showdata((byte[])rd1["photo"]);

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
            try
            {
                string txt = txtsearch.Text.Trim();
                if (txt != "")
                {
                    isini = true;

                    if (Txtitem.Text.Trim() != "")
                    {
                        ini_form();
                        txtsearch.Text = txt;
                    }
                    dv.RowFilter = "Item_Code LIKE  '%" + txt + "%' OR description LIKE '%" + txt + "%'";
                    isini = false;
                    // if (!issearch && dv.Count >0 ) search_data(grditem["Item_Code", 0].Value.ToString());
                }
                else
                    dv.RowFilter = "Item_Code <> '0'";

                grditem.Visible = true;
                grditem.Top = txtsearch.Top + txtsearch.Height;
                grditem.Left = txtsearch.Left;
                isini = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                    if (grditem.Visible && grditem.Rows.Count > 0)
                    {
                        txtsearch.Text = grditem.CurrentRow.Cells[0].Value.ToString();
                        if (txtsearch.Text.Trim() != "" && txtolditm.Text != txtsearch.Text)

                            search_data(txtsearch.Text.Trim());
                        grditem.Visible = false;
                    }
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

            if (Txtitem.Text.Trim() != "" && txtolditm.Text != Txtitem.Text)
                search_data(Txtitem.Text);
        }

        private void grditem_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isini) return;


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



        private void txtclstock_TextChanged(object sender, EventArgs e)
        {
            txtstock.Text = txtclstock.Text;
        }

        private void btnaddbarcode_Click(object sender, EventArgs e)
        {
            tab1.SelectedTab = tab1.TabPages[0];
            tab1.TabPages[0].Show();
            try
            {
                if (string.IsNullOrEmpty(Txtitem.Text))
                {
                    MessageBox.Show("Invalid Itemcode");
                    return;
                }

                //if (string.IsNullOrEmpty(txtbarcode.Text))
                //{
                //    MessageBox.Show("Invalid Barcode");
                //    return;
                //}

                Conn.Close();


                foreach (DataGridViewRow row in this.grdbarcode.Rows)
                {
                    //foreach (DataGridViewCell cell in row.Cells)
                    if (row.Cells[2].Value.ToString() == cmbunit.Text)
                    {
                        return;
                    }

                    //if (row.Cells[0].Value.ToString() == txtbarcode.Text)
                    //{
                    //    MessageBox.Show("Invalid Barcode");
                    //    return;
                    //}

                    //if (dgv1.Rows[row.  cell.Size.IsEmpty)
                    //{

                    // }
                    //MessageBox.Show(cell.Value.ToString());

                }


                //Conn.Open();


                //sql = " SELECT ACC_NO  ,ACC_NAME,ACC_MOBILE_NO,CONTACT_PERSON  ";
                //sql = sql + " FROM ACCOUNTS WHERE ACC_NO  =" + CMBSUP.SelectedValue;

                //SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

                fnd = true;
                while (fnd)
                {
                    grdbarcode.Rows.Add();
                    // grdbarcode["barcode",grdbarcode.Rows.Count - 1].Value = txtbarcode.Text;
                    grdbarcode["description", grdbarcode.Rows.Count - 1].Value = txtname.Text;
                    grdbarcode["unit", grdbarcode.Rows.Count - 1].Value = cmbunit.Text;
                    grdbarcode["unitid", grdbarcode.Rows.Count - 1].Value = cmbunit.SelectedValue.ToString();
                    grdbarcode["fraction", grdbarcode.Rows.Count - 1].Value = txtfraction.Text;
                    grdbarcode["itemcode", grdbarcode.Rows.Count - 1].Value = Txtitem.Text;
                    grdbarcode["description", grdbarcode.Rows.Count - 1].Value = txtname.Text;
                    //* grdbarcode["descriptionAr", grdbarcode.Rows.Count - 1].Value = txtarabic.Text;
                    //*  grdbarcode["saleprice1", grdbarcode.Rows.Count - 1].Value = txtsaleprice.Text;
                    //*grdbarcode["saleprice2", grdbarcode.Rows.Count - 1].Value = txtretailprice.Text;


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = false;

                }

                grdbarcode.ReadOnly = false;

                Conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Load_grid()
        {
            try
            {
                Conn.Close();
                Conn.Open();



                string sql = "";

                if (entrytype == 2)
                    sql = "sELECT *  from form_caption  where form_code=1 and  flag <> 'X' order by Order_by";
                else
                    sql = "sELECT *  from form_caption  where form_code=2 and  flag <> 'X' order by Order_by";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();
                System.Drawing.Image image1;
                int i = 0;
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            grdmain.Rows.Add();


                            if (Convert.ToBoolean(rd["Lookup"]))
                            {
                                grdmain[3, i].Style.BackColor = Color.Red;

                                grdmain["Lookupsql", i].Value = rd["lookup_sql"].ToString();
                                //image1 = Image.FromFile(
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\lookup.jpg");
                                //grdmain[3, i].Value = image1;
                                grdmain.Rows[i].Cells[3].Value = image1;
                                if (rd["default_val"].ToString() != "")
                                    if (rd["default_val"].ToString().Contains("="))
                                    {
                                        string[] ary = rd["default_val"].ToString().Split('=');
                                        grdmain["colvalue", i].Value = ary[0].ToString();
                                        grdmain["fieldval", i].Value = ary[1].ToString();

                                    }
                                    else
                                    {
                                        grdmain["colvalue", i].Value = rd["default_val"].ToString();
                                        grdmain["fieldval", i].Value = rd["default_val"].ToString();
                                    }

                            }
                            else
                            {
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\white.jpg");
                                grdmain.Rows[i].Cells[3].Value = image1;
                                grdmain["Lookupsql", i].Value = "";
                            }
                            if (Convert.ToBoolean(rd["Is_Mandatory"]))
                                grdmain[1, i].Value = "*";

                            grdmain["fieldname", i].Value = rd["field_name"].ToString();
                            grdmain["tablename", i].Value = rd["table_name"].ToString();
                            grdmain.Rows[i].HeaderCell.Value = rd["field_name"].ToString();

                            if (rd["default_val"].ToString() != "")
                            {
                                if (rd["default_val"].ToString().Contains("="))
                                {
                                    string[] ary = rd["default_val"].ToString().Split('=');
                                    grdmain["colvalue", i].Value = ary[0].ToString();
                                    grdmain["fieldval", i].Value = ary[1].ToString();

                                }
                                else
                                {
                                    grdmain["colvalue", i].Value = rd["default_val"].ToString();
                                    grdmain["fieldval", i].Value = rd["default_val"].ToString();
                                }
                            }
                            grdmain["flag", i].Value = rd["flag"].ToString();


                            if (rd["flag"].ToString() == "R")
                            {
                                grdmain.Rows[i].ReadOnly = true;
                                //grdmain.Rows[i].Visible = false;
                                for (int c = 1; c < grdmain.ColumnCount; c++)
                                {
                                    grdmain[c, i].Style.BackColor = Color.LightGray;
                                    // grdmain[c, i ].Style.ForeColor = Color.Beige;
                                }

                            }
                            grdmain[0, i++].Value = rd[0].ToString();


                            if (rd["rlposition"].ToString() == "H")
                            {
                                MergeCellsInRow(i - 1, 1, 2);
                                for (int c = 0; c < grdmain.ColumnCount; c++)
                                {
                                    grdmain[c, i - 1].Style.BackColor = Color.Honeydew;
                                    grdmain[c, i - 1].Style.ForeColor = Color.Red;
                                }
                            }
                            if (rd["flag"].ToString() == "H")
                            {
                                grdmain.Rows[i - 1].Visible = false;
                                if (rd["col_name"].ToString() == "WHprice")
                                {
                                    grdbarcode.Columns["bwhprice"].Visible = false;
                                    grdbarcode.Columns["bretprice"].Visible = false;

                                }

                            }

                        }
                       
                    }
                }
                rd.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        private void MergeCellsInColumn(int col, int row1, int row2)
        {
            Graphics g = grdmain.CreateGraphics();
            Pen p = new Pen(grdmain.GridColor);
            Rectangle r1 = grdmain.GetCellDisplayRectangle(col, row1, true);
            Rectangle r2 = grdmain.GetCellDisplayRectangle(col, row2, true);
            int recHeight = 0;
            string recValue = string.Empty;
            for (int i = row1; i <= row2; i++)
            {
                recHeight += grdmain.GetCellDisplayRectangle(col, i, true).Height;
                if (grdmain[col, i].Value != null)
                    recValue += grdmain[col, i].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, r1.Width, recHeight);
            g.FillRectangle(new SolidBrush(grdmain.DefaultCellStyle.BackColor), newCell); g.DrawRectangle(p, newCell); g.DrawString(recValue, grdmain.DefaultCellStyle.Font, new SolidBrush(grdmain.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }

        private void MergeCellsInRow(int row, int col1, int col2)
        {
            Graphics g = grdmain.CreateGraphics();
            Pen p = new Pen(grdmain.GridColor);
            Rectangle r1 = grdmain.GetCellDisplayRectangle(col1, row, true);
            Rectangle r2 = grdmain.GetCellDisplayRectangle(col2, row, true);
            int recWidth = 0; string recValue = string.Empty;
            for (int i = col1; i <= col2; i++)
            {
                recWidth += grdmain.GetCellDisplayRectangle(i, row, true).Width;
                if (grdmain[i, row].Value != null)
                    recValue += grdmain[i, row].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, recWidth, r1.Height);
            g.FillRectangle(new SolidBrush(grdmain.DefaultCellStyle.BackColor), newCell);
            g.DrawRectangle(p, newCell);
            g.DrawString(recValue, grdmain.DefaultCellStyle.Font, new SolidBrush(grdmain.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }

        private void poplookup(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                grdlookup.DataSource = null;
                dblclk_row = grd.CurrentCell.RowIndex;

                Conn.Close();
                Conn.Open();
                string sql = grd["Lookupsql", e.RowIndex].Value.ToString();
                if (sql.Contains("?up"))
                {
                    string up = "'" + grd["fieldval", e.RowIndex - 1].Value.ToString() + "'";
                    sql = sql.Replace("?up", up);
                }

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,W.stock AS WStock,S.STOCK AS TStock from hd_itemmaster  left join wr_stock_master as w on (hd_itemmaster.Item_Code=w.Item_Code) left join stock_master as s on (hd_itemmaster.Item_Code=s.Item_Code)", Conn);
                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Lookupsql");
                adalkp.Fill(dtlkp);
                // last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                var cellRectangle = grd.GetCellDisplayRectangle(grd.CurrentCell.ColumnIndex, grd.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                grdlookup.Parent = grd;
                grdlookup.Tag = "Item_Code";
                dv.Table = dtlkp;
                grdlookup.DataSource = dv;
                grdlookup.Width = grd.Columns["colvalue"].Width + grd.Columns["collookup"].Width;
                grdlookup.Columns[0].Width = 150;
                grdlookup.Columns[1].Width = 300;
                grdlookup.Refresh();
                grdlookup.Left = cellRectangle.Left;
                grdlookup.Top = cellRectangle.Top + grd.Rows[0].Height;
                object txt = txtgrd.Text.ToString();
                if (txt != "")
                {
                    dv.RowFilter = "Code LIKE  '%" + txt + "%' OR desc1 LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Code <> '0'";


                grdlookup.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // grditem.Visible = false;
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
                        if (cmbunit.SelectedIndex < 0)
                        {
                            MessageBox.Show("Invalid Unit Selection,Please check and confirm!!");
                            return;
                        }
                        add_barcode();
                        break;
                    }
                case 1:
                    {

                        if (string.IsNullOrEmpty(Txtitem.Text))
                        {
                            MessageBox.Show("Invalid Itemcode");
                            return;
                        }

                        if (cmbunit.SelectedIndex < 0)
                        {
                            MessageBox.Show("Invalid Unit Selection,Please check and confirm!!");
                            return;
                        }
                        saveToolStripButton_Click(sender, e);
                        break;
                    }
                case 2:
                    {

                        newToolStripButton_Click(sender, e);
                        break;
                    }

                case 3:
                    {

                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("mnuItemcat");

                        Form childForm = new FrmItemCat();
                        childForm.MdiParent = MDIParent1.ActiveForm;

                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Item Category Entry Screen";
                        childForm.Show();

                    }
                    break;

                case 4:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("mnuItemUnit");
                        Form childForm = new FrmItemUnit();
                        childForm.MdiParent = MDIParent1.ActiveForm;

                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Item Unit Entry Screen";
                        childForm.Show();
                    }
                    break;
            }








        }

        private void add_barcode()
        {


            DataGridViewRow row;
            int i = 0;
            bool fnd = false;
            try
            {
                if (txtfraction.Text == null || txtfraction.Text == "") txtfraction.Text = "1";
                for (i = 0; i < grdbarcode.Rows.Count; i++)
                {
                    if (grdbarcode["unit", i].Value.ToString() == cmbunit.Text)
                    {
                        fnd = true;
                        break;
                    }
                }


                row = grdmain.Rows
                               .Cast<DataGridViewRow>()
                               .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("BARCODE"))
                               .First();


                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "";

                if (!fnd)
                {

                    for (int b = 0; b < grdbarcode.Rows.Count; b++)
                    {
                        if (grdbarcode["barcode", b].Value.ToString() == row.Cells["colvalue"].Value.ToString() && row.Cells["colvalue"].Value.ToString() != "")
                        {
                            MessageBox.Show("Duplicate Barcode Found , Please Fix It and Try again");
                            return;
                        }
                    }

                    grdbarcode.Rows.Add(1);
                }



                tab1.SelectedTab = tab1.TabPages[0];
                tab1.TabPages[0].Show();




                Conn.Close();


                //foreach (DataGridViewRow row1 in this.grdbarcode.Rows)
                //{
                //    //foreach (DataGridViewCell cell in row.Cells)
                //    if (row.Cells[2].Value.ToString() == cmbunit.Text)
                //    {
                //        return;
                //    }


                fnd = true;
                while (fnd)
                {
                    string unitcode = "";
                    grdbarcode["unit", i].Value = cmbunit.Text;

                    unitcode = cmbunit.SelectedValue.ToString();



                    // row.Cells["colvalue"].Value = "";

                    if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "";
                    grdbarcode["barcode", i].Value = row.Cells["colvalue"].Value;

                    //grdmain.CurrentCell = grdmain["colvalue", row.Index];


                    if (grdbarcode["barcode", i].Value.ToString() == "") grdbarcode["barcode", i].Value = Txtitem.Text;


                    if (i > 0 && row.Cells["colvalue"].Value.ToString() == "")

                        grdbarcode["barcode", i].Value = grdbarcode["barcode", i].Value.ToString() + unitcode;


                    grdbarcode["description", grdbarcode.Rows.Count - 1].Value = txtname.Text;
                    grdbarcode["unit", grdbarcode.Rows.Count - 1].Value = cmbunit.Text;
                    grdbarcode["unitid", grdbarcode.Rows.Count - 1].Value = cmbunit.SelectedValue.ToString();
                    grdbarcode["fraction", grdbarcode.Rows.Count - 1].Value = txtfraction.Text;
                    grdbarcode["itemcode", grdbarcode.Rows.Count - 1].Value = Txtitem.Text;
                    row = grdmain.Rows
                     .Cast<DataGridViewRow>()
                     .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                     .First();

                    grdbarcode["descriptionAr", grdbarcode.Rows.Count - 1].Value = row.Cells["colvalue"].Value;

                    row = grdmain.Rows
                     .Cast<DataGridViewRow>()
                     .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RETAIL_PRICE"))
                     .First();
                    if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";
                    grdbarcode["saleprice2", grdbarcode.Rows.Count - 1].Value = row.Cells["colvalue"].Value;

                    row = grdmain.Rows
                     .Cast<DataGridViewRow>()
                     .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("SALE_PRICE"))
                     .First();
                    if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";
                    grdbarcode["saleprice1", grdbarcode.Rows.Count - 1].Value = row.Cells["colvalue"].Value; ;


                    //dt1.Text = rd["Cur_date"].ToString();


                    fnd = false;

                }

                grdbarcode.ReadOnly = false;

                Conn.Close();
            }
            catch (Exception ex)
            {

            }

        }

        private void grdbutton_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdmain_Enter(object sender, EventArgs e)
        {
            acntrl = "grdmain";
            try
            {
                grdmain.CurrentCell = grdmain[2, 0];
                grdmain.CurrentCell = grdmain[2, 5];
                grdmain.CurrentCell = grdmain[2, 0];
            }
            catch
            { }
            grdmain.BeginEdit(true);
        }

        private void grdstock_Enter(object sender, EventArgs e)
        {
            acntrl = "grdstock";
            grdstock.CurrentCell = grdstock[2, 0];
            grdstock.BeginEdit(false);
        }

        private void grdmain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Tab:
                    grditem.Focus();
                    break;
                case Keys.Escape:

                    break;
                case Keys.Enter:
                    int i;
                    for (i = grdmain.CurrentCell.RowIndex + 1; i < grdmain.Rows.Count - 1; i++)
                    {
                        if (i == grdmain.Rows.Count) break;
                        if (grdmain[0, i].Visible) break;
                    }
                    if (i == grdmain.Rows.Count - 1)
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);



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
            if (msg.WParam.ToInt32() == (int)Keys.Enter && !grditem.Visible)
            {
                // SendKeys.Send("{Tab}");

                switch (acntrl)
                {
                    case "grdmain":

                    case "grditem":
                    case "grdlookup":
                    case "grdstock":
                    case "grdsup":
                    case "grdbutton":
                    case "grdbarcode":
                        {
                            return (false);
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

        private void grdmain_Leave(object sender, EventArgs e)
        {
            add_barcode();
            acntrl = "";
            grdmain.ClearSelection();
            grdstock.Focus();
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

        private void grdbarcode_Leave(object sender, EventArgs e)
        {
            acntrl = "";
            grdbarcode.ClearSelection();
        }

        private void grdstock_Leave(object sender, EventArgs e)
        {
            acntrl = "";
            grdstock.ClearSelection();

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            grditem.Visible = !grditem.Visible;
            grditem.Top = txtsearch.Top + txtsearch.Height;
            grditem.Left = txtsearch.Left;
            isini = false;
        }

        private void grditem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (grditem.Rows.Count > 0)
                {
                    txtsearch.Text = grditem[0, grditem.CurrentCell.RowIndex].Value.ToString();
                    search_data(txtsearch.Text.Trim());
                }
                grditem.Visible = false;
                txtsearch.Focus();

            }
            catch (Exception ex)
            {
                grditem.Visible = false;
            }
        }

        private void grdstock_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {

                case Keys.Enter:
                    int i;
                    for (i = grdstock.CurrentCell.RowIndex + 1; i < grdstock.Rows.Count - 1; i++)
                    {
                        if (i == grdstock.Rows.Count) break;
                        if (grdstock[0, i].Visible) break;
                    }
                    if (i == grdstock.Rows.Count)
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);



                    break;
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

        private void cmbunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdmain.Rows.Count < 1) return;

                DataGridViewRow row;

                row = grdmain.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("BARCODE"))
                    .First();
                row.Cells["colvalue"].Value = "";


                //    row = grdmain.Rows
                //.Cast<DataGridViewRow>()
                //.Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                //.First();


                //    row.Cells["colvalue"].Value = "";

                row = grdmain.Rows
                 .Cast<DataGridViewRow>()
                 .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RETAIL_PRICE"))
                 .First();

                row.Cells["colvalue"].Value = "";

                row = grdmain.Rows
                 .Cast<DataGridViewRow>()
                 .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("SALE_PRICE"))
                 .First();
                if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";
                row.Cells["colvalue"].Value = "";




                for (int i = 0; i < grdbarcode.Rows.Count; i++)
                {
                    if (cmbunit.Text == grdbarcode[2, i].Value.ToString())
                    {

                        row = grdmain.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("BARCODE"))
                    .First();
                        row.Cells["colvalue"].Value = grdbarcode["barcode", i].Value;


                        row = grdmain.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                    .First();


                        row.Cells["colvalue"].Value = grdbarcode["descriptionAr", i].Value;

                        row = grdmain.Rows
                         .Cast<DataGridViewRow>()
                         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RETAIL_PRICE"))
                         .First();

                        row.Cells["colvalue"].Value = grdbarcode["saleprice2", i].Value;

                        row = grdmain.Rows
                         .Cast<DataGridViewRow>()
                         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("SALE_PRICE"))
                         .First();
                        if (row.Cells["colvalue"].Value == null) row.Cells["colvalue"].Value = "0";
                        row.Cells["colvalue"].Value = grdbarcode["saleprice1", i].Value;

                        txtfraction.Text = grdbarcode["fraction", i].Value.ToString();

                    }

                }

            }
            catch (Exception ex)
            {

            }
        }


        private void updat_accounts(string docno, decimal TXTSRAMT)
        {
            try
            {
                try
                {




                    //decimal TXTSRAMT = Convert.ToDecimal(txtstock.Text) * Convert.ToDecimal(txtcost.Text) * Gvar._cur_rate;

                    decimal paidamt = 0;




                    if (Convert.ToDecimal(TXTSRAMT) > 0)
                    {
                        ADODB.Connection ADOconn = new ADODB.Connection();

                        ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                        object TRNBY;
                        object DRCR;
                        object DRCR1;
                        object NARR;
                        object LACC;
                        object PAYBY;
                        object[] ledgerini;
                        string acc_acs;
                        acc_acs = Program.ledger_ini(Convert.ToInt16("0"), docno);
                        ledgerini = acc_acs.Split('`');
                        TRNBY = Convert.ToInt16((ledgerini[0]));
                        DRCR = ledgerini[1];
                        DRCR1 = ledgerini[2];
                        NARR = ledgerini[3];
                        LACC =   Convert.ToInt64((ledgerini[4]));
                        PAYBY = Convert.ToInt16(ledgerini[05]);

                        NARR = "Opening Stock";

                        Recordset TMP = new Recordset();
                        if (isedit)
                        {
                            sql = "INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code] FROM TRN_ACCOUNTS WHERE NYEAR=" + Gvar.nyear + " and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + cmbbranch.SelectedValue;

                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        }


                        sql = "DELETE FROM TRN_ACCOUNTS WHERE NYEAR='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + cmbbranch.SelectedValue;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        sql = "DELETE FROM TRaN_ACC WHERE YEAR(CUR_DATE) ='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + cmbbranch.SelectedValue;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                        sql = "select * from trnno";
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object trno = TMP.Fields[0].Value;
                        object trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                        object accno = 0;



                        sql = "DELETE FROM TRN_PAYMENT_DET WHERE NYEAR=" + Gvar.nyear + " and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'  and BRN_CODE =" + cmbbranch.SelectedValue;
                        Recordset acc = new Recordset();
                        acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        sql = "SELECT TOP 1 * FROM TRN_ACCOUNTS";
                        acc = new Recordset();
                        acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);






                        long cost_ac = 0;
                        long pur_ac = 0;
                        long sale_ac = 0;
                        long cash_ac = 0;
                        long stock_ac = 0;

                        sql = "select cost_item_ac,[CASH_SALE_AC],[CASH_PUR_AC],DEF_CASH_AC,STOCK_AC from ac_options WHERE  ac_options.ID =1";
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (TMP.RecordCount > 0)
                        {
                            if (TMP.Fields[0].Value != DBNull.Value)
                                cost_ac = Convert.ToInt64(TMP.Fields[0].Value);

                            if (TMP.Fields[1].Value != DBNull.Value)
                                sale_ac = Convert.ToInt64(TMP.Fields[1].Value);
                            if (TMP.Fields[2].Value != DBNull.Value)
                                pur_ac = Convert.ToInt64(TMP.Fields[2].Value);
                            if (TMP.Fields[3].Value != DBNull.Value)
                                cash_ac = Convert.ToInt64(TMP.Fields[3].Value);
                            if (TMP.Fields[4].Value != DBNull.Value)
                                stock_ac = Convert.ToInt64(TMP.Fields[4].Value);

                        }

                        if (cost_ac == 0 || pur_ac == 0 || sale_ac == 0 || cash_ac == 0 || stock_ac == 0)
                        {
                            MessageBox.Show("Invalid Initial Account[cost_item_ac,[CASH_SALE_AC],[CASH_PUR_AC],DEF_CASH_AC,STOCK_AC]", "Invalid Account");
                            iserror = true;
                            return;
                        }









                        #region Inventory ACcount
                        string lnarr = "";



                        if (TXTSRAMT > 0)
                        {
                            sql = "select * from trnno";
                            TMP = new Recordset();
                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            trno = TMP.Fields[0].Value;
                            trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                            acc.AddNew();
                            acc.Fields["trn_no"].Value = trno;
                            acc.Fields["trn_no2"].Value = trno2;
                            acc.Fields["BRN_CODE"].Value = cmbbranch.SelectedValue;
                            acc.Fields["DR_CR"].Value = DRCR;
                            acc.Fields["User_id"].Value = Gvar.Userid;
                            acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                            //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);


                            acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                            acc.Fields["f_pay_amount"].Value = TXTSRAMT;
                            acc.Fields["F_RATE"].Value = "1";
                            acc.Fields["currency"].Value = Gvar._currency;

                            acc.Fields["pay_date"].Value = dt1.Value;
                            acc.Fields["NARRATION"].Value = NARR + lnarr;
                            acc.Fields["doc_no"].Value = docno;
                            acc.Fields["PAYBY"].Value = "0";
                            acc.Fields["TRN_BY"].Value = TRNBY;
                            acc.Fields["NYEAR"].Value = dt1.Value.Year;
                            acc.Fields["cost_code"].Value = 0;
                            acc.Fields["dept_code"].Value = 0;
                            acc.Fields["entry_no"].Value = 0;// Convert.ToDecimal(docno);
                            acc.Fields["trn_type"].Value = 0;
                            acc.Update();

                            sql = "select * from trnno";
                            TMP = new Recordset();
                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            trno2 = TMP.Fields[0].Value;
                            //trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                            acc.AddNew();
                            acc.Fields["trn_no"].Value = trno2;
                            acc.Fields["trn_no2"].Value = trno;
                            acc.Fields["BRN_CODE"].Value = cmbbranch.SelectedValue;
                            acc.Fields["DR_CR"].Value = DRCR1;
                            acc.Fields["User_id"].Value = Gvar.Userid;
                            acc.Fields["acc_no"].Value = stock_ac; // IF SALES THEN LACC ELSE STOCK_AC
                            //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);


                            acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                            acc.Fields["f_pay_amount"].Value = TXTSRAMT;
                            acc.Fields["F_RATE"].Value = "1";
                            acc.Fields["currency"].Value = Gvar._currency;

                            acc.Fields["pay_date"].Value = dt1.Value;
                            acc.Fields["NARRATION"].Value = NARR + lnarr;
                            acc.Fields["doc_no"].Value = docno;
                            acc.Fields["PAYBY"].Value = "0";
                            acc.Fields["TRN_BY"].Value = TRNBY;
                            acc.Fields["NYEAR"].Value = dt1.Value.Year;
                            acc.Fields["cost_code"].Value = 0;
                            acc.Fields["dept_code"].Value = 0;
                            acc.Fields["entry_no"].Value = 0;// Convert.ToDecimal(docno);
                            acc.Fields["trn_type"].Value = 0;
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
                        //acc.Fields["User"].Value = Gvar.Userid;
                        acc.Fields["acc_no"].Value = LACC;
                        acc.Fields["AMOUNT"].Value = TXTSRAMT;
                        acc.Fields["currency_rate"].Value = Gvar._cur_rate;

                        acc.Fields["currency_CODE"].Value = Gvar._currency;
                        acc.Fields["Ledger_acc"].Value = LACC;
                        acc.Fields["CUR_DATE"].Value = dt1.Value;
                        acc.Fields["Description"].Value = NARR;
                        acc.Fields["doc_no"].Value = docno;
                        acc.Fields["currency_code"].Value = Gvar._currency;
                        acc.Fields["TRN_BY"].Value = TRNBY;
                        // acc.Fields["NYEAR"].Value = dt1.Value.Year;
                        acc.Fields["entry_no"].Value = 0;// Convert.ToDecimal(docno);
                        acc.Update();

                        iserror = false;

                    }
                }







                catch (SqlException er)
                {

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
                    cmd = new SqlCommand("Update  Item_Image set barcode='" + Txtitem.Text + "',photo=@photo where barcode='" + txtolditm.Text + "'", con);
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
                if (n == 0)
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
        private void showdata(byte[] photo_aray)
        {


            pictureBox1.Image = null;

            // photo_aray = (byte[])ds.Tables[0].Rows[rno][4];
            System.IO.MemoryStream ms = new System.IO.MemoryStream(photo_aray);
            pictureBox1.Image = Image.FromStream(ms);


        }

        private void grdlookup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // DataGridView grd = (DataGridView)sender;

                Type obj = sender.GetType();

                var c2 = sender.GetType();


                object oc2 = c2;

                //nder.GetType() obj  = (sender.GetType()) sender;
                DataGridView grd = (DataGridView)((DataGridView)sender).Parent;

                if (grdlookup.Visible && grdlookup.Rows.Count > 0)
                {
                    grd["fieldval", grd.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                    grd["colvalue", grd.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;

                    grdlookup.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void grdlookup_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        grdlookup.Visible = false;
                        break;
                    }
                    break;


            }
        }

        private void txtminprofit_Leave(object sender, EventArgs e)
        {
            grdmain.Focus();
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            if (grdbarcode.Rows.Count < 1) return;
            if (grdbarcode["description",0].Value!=null )
    {
        grdbarcode["description",0].Value = txtname.Text;
    }
        }

        private void cmbcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbcat.SelectedIndex < 0 ) return;
            
            try
            {

                sql = " SELECT vat_percent from item_cat where itm_cat_code =" + cmbcat.SelectedValue ;
                
                SqlCommand cmd1 = new SqlCommand(sql, Conn);
                cmd.Cancel();
                SqlDataReader rd = cmd1.ExecuteReader();


                while (rd.Read())
                {
                    txtvat.Text = rd[0].ToString();

                }
                rd.Close();
                cmd1.Cancel();
            }
                catch(Exception ex)
            {

                
            }
        }

        private void txtvat_TextChanged(object sender, EventArgs e)
        {

        }
    }
 }




