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
    public partial class FrmPurOrder : FinOrgForm
    {

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView projdv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        Color bg_color;
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();
        DataSet ds2 = new DataSet();
        decimal sal_pur_amt;
        DataTable rec_options = new DataTable();
        string acntrl;
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        bool iserror;
        bool issearch;
        bool isdirty;
        int cur_row;
        int dblclk_row;
        int fcol;
        string last_col;
        string cur_col;
        int last_row;
        Boolean nodata;
        object oldcusno;
        int lastrec = 0;

        public FrmPurOrder()
        {
            try
            {


                InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
                load_form();
               
                
                grdbutton.Rows.Add(1);
                nyear.Text = dt1.Value.Year.ToString();
                load_ini();
            }
            catch
            {

            }

            // ADOconn.Close();

        }

        private void FrmMRNEntry_Load(object sender, EventArgs e)
        {
            try
            {

                if (ADOconn.State == 1) ADOconn.Close();
                //ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                Conn.Close();
                Conn.Open();


                if (txtpriv.Text.Substring(2, 1) == "0")
                    btndelete.Visible = false;

                int trn = Gvar.trntype; 
                txttrn_type.Text = trn.ToString();
              
                sql = "SELECT * FROM options WHERE TRNTYPE=" + Convert.ToInt32(txttrn_type.Text);



                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                rec_options = new DataTable("rec_options");
                adalkp.Fill(rec_options);
                if (rec_options.Rows.Count > 0)
                {
                    object a = rec_options.Rows[0]["auto_next_line"];
                }

 bg_color = System.Drawing.Color.FromName(rec_options.Rows[0]["bg_color"].ToString());  
               
               

                dgv1.Focus();
                dgv1.CurrentCell = dgv1["barcode", 0];
                Conn.Close();
                sql = "sELECT  TRN_CODE,TRN_NAME  froM TRN_TYPE WHERE TRN_CODE IN (" + trn + " ) ORDER BY TRN_CODE";
                Conn.Open();
                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                
                DataTable dt3 = new DataTable("TrnType");
                ada3.Fill(dt3);
                cmbtrntype.DisplayMember = "TRN_NAME";
                cmbtrntype.ValueMember = "TRN_CODE";
              

                cmbtrntype.DataSource = dt3;
                cmbtrntype.SelectedIndex = 0;
                //cmbtrntype.DataSource = ada3;
                lbltrntype.Text = cmbtrntype.Text;
                if (txttrn_type.Text == "2")
                    lbltrntype.Text = "Packing Slip";

                sql = "SELECT  ACC_NO,ACC_NAME FROM ACCOUNTS  WHERE ACC_TYPE_CODE= (SELECT TOP 1 EMP_AC_TYPE FROM AC_OPTIONS WHERE  ac_options.ID =1) union select 0 , 'Direct' order by 1 ";
                SqlDataAdapter sales = new SqlDataAdapter(sql, Conn);
                DataTable dtsales = new DataTable("SaleType");
                sales.Fill(dtsales);
                cmbsalesagent.DisplayMember = "ACC_NAME";
                cmbsalesagent.ValueMember = "ACC_NO";

                cmbsalesagent.DataSource = dtsales;
                cmbsalesagent.SelectedIndex = 0;
                dt1.Value = DateTime.Now;
                nyear.Text = dt1.Value.Year.ToString();
                //string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pos.txt");
                //if(System.IO.File.Exists(myTempFile))
                //{
                //    dgv1.CellLeave -= dgv1_CellLeave;
                //    dgv1.CellEnter -= dgv1_CellEnter;
                //    string text = System.IO.File.ReadAllText(myTempFile);
                //    dgv1.BeginEdit(true);
                //    string [] grid  = text.Split('|');
                //    int i=0;
                //    foreach (string line in grid)
                //    {
                //        string [] rw =line.Split(',');

                //    int j=0;
                //    dgv1.Rows.Add();
                //    foreach (string col in rw)
                //    {
                //        dgv1[j++,i].Value = col.ToString();

                //    }
                //        i++;


                //    }
                //    dgv1.EndEdit();
                //    dgv1.CellLeave += dgv1_CellLeave;
                //    dgv1.CellEnter += dgv1_CellEnter;

                //                }
                sql = "select CURRENCY_code,CURRENCY_code +' : ' +  cast(CURRENCY_RATE as varchar) AS CURRENCY from currency_master ";

                SqlDataAdapter adacurr = new SqlDataAdapter(sql, Conn);
                DataTable dtcurr = new DataTable("currency");
                adacurr.Fill(dtcurr);

                cmbcurrency.DisplayMember = "CURRENCY";
                cmbcurrency.ValueMember = "CURRENCY_code";

                cmbcurrency.DataSource = dtcurr;
                cmbcurrency.SelectedIndex = 0;

                
                if ( trn == 2)
                {
                    lblorderdate.Visible = true;
                    orderdate.Visible = true;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    lblinv.Text="Reciept No.";
                   // lblref.Text="Order No.";
                    cmbmode.Enabled=false;
                    cmbport.Enabled=false;
                    cmbshipterm.Enabled=false;
                    txtotherterms.Enabled = false;
                    cmbterm.Enabled=false;
                    lblorder.Visible = true;
                    txtorderno.Visible = true;
                    dgv1.Columns["cost"].Visible = false;
                    dgv1.Columns["price"].Visible = false;
                    dgv1.Columns["total"].Visible = false;
                    dgv1.Columns["cost"].HeaderCell.Value = "Sale Price";
                    dgv1.Columns["cost"].ReadOnly = false;
                    lblgross.Visible = false;
                    lblnet.Visible = false;
                    txttotal.Visible = false;
                    txtnetamt.Visible = false;
                    lblleftsign.Visible = false;
                    lblrightsign.Visible = false;
                    cmbleftsign.Visible = false;
                    cmbrightsign.Visible = false;
                    cmbaddress.Enabled = false;
                    cmbshipterm.Enabled = false;
                    dtdelivery.Enabled = false;
                    lblremarks.Visible = false;
                    txtremarks.Visible = false;

                }

                populate_options();
                if (bg_color != null)
                    set_bgcolor(bg_color);

                if (txttrn_type.Text == "2")
                {
                    lblref.Text = "Invoice No.";
                    txtorderno.Focus();
                }
                else
                {
                    lblref.Text = "Perfoma Inv.#";
                    txtrefno.Focus();
                }


                get_invno();
                txtcustomer.Focus();

            }
            catch(Exception ex)
            { 
            }

        }

        private void populate_options()
        {
             try
            {
            sql = "sELECT code,description  froM purorder_options WHERE type='ShipTerm' ORDER BY code";
           //Conn.Open();
            SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);

            DataTable dt3 = new DataTable("options");
            ada3.Fill(dt3);
            cmbshipterm.DisplayMember = "description";
            cmbshipterm.ValueMember = "code";


            cmbshipterm.DataSource = dt3;
                 if(cmbshipterm.Items.Count>0)
            cmbshipterm.SelectedIndex = 0;


                 sql = "sELECT code,description  froM purorder_options WHERE type='PayTerm' ORDER BY code";
              
                  ada3 = new SqlDataAdapter(sql, Conn);

                  DataTable term = new DataTable("options");
                  ada3.Fill(term);
                  cmbterm.DisplayMember = "description";
                 cmbterm.ValueMember = "code";


                 cmbterm.DataSource = term;
                 if (cmbterm.Items.Count > 0)
                     cmbterm.SelectedIndex = 0;



                 sql = "sELECT code,description  froM purorder_options WHERE type='Mode' ORDER BY code";

                 ada3 = new SqlDataAdapter(sql, Conn);

                 DataTable mode = new DataTable("options");
                 ada3.Fill(mode);
                 cmbmode.DisplayMember = "description";
                 cmbmode.ValueMember = "code";


                 cmbmode.DataSource = mode;
                 if (cmbmode.Items.Count > 0)
                     cmbmode.SelectedIndex = 0;


                 sql = "sELECT code,description  froM purorder_options WHERE type='Port' ORDER BY code";

                 ada3 = new SqlDataAdapter(sql, Conn);

                 DataTable port = new DataTable("options");
                 ada3.Fill(port);
                 cmbport.DisplayMember = "description";
                 cmbport.ValueMember = "code";


                 cmbport.DataSource = port;
                 if (cmbport.Items.Count > 0)
                     cmbport.SelectedIndex = 0;


                 sql = "sELECT adrs_code,address  froM address_master WHERE adrs_type='Porder' ORDER BY adrs_code";

                 SqlDataAdapter adrs = new SqlDataAdapter(sql, Conn);

                 DataTable dtadrs = new DataTable("address");
                 adrs.Fill(dtadrs);
                 cmbaddress.DisplayMember = "adrs_code";
                 cmbaddress.ValueMember = "adrs_code";
                 cmbaddress.DataSource = dtadrs;
                 if (cmbaddress.Items.Count > 0)
                     cmbaddress.SelectedIndex = 0;

                 sql = "sELECT sno,signatory ,sign_name froM signatory order by sno";

                 SqlDataAdapter sign1 = new SqlDataAdapter(sql, Conn);

                 DataTable dtsign1 = new DataTable("address");
                 sign1.Fill(dtsign1);
                 cmbleftsign.DisplayMember = "signatory";
                 cmbleftsign.ValueMember = "sign_name";
                 cmbleftsign.DataSource = dtsign1       ;

                    

                 SqlDataAdapter sign2 = new SqlDataAdapter(sql, Conn);

                 DataTable dtsign2 = new DataTable("address");
                 
                 sign2.Fill(dtsign2);
                 cmbrightsign.DisplayMember = "signatory";
                 cmbrightsign.ValueMember = "sign_name";

                 cmbleftsign.SelectedIndex = 0;
                 cmbrightsign.DataSource = dtsign2;   
                 cmbrightsign.SelectedIndex = 1;
                 if (txttrn_type.Text != "2")
                     dgv1.Columns["orderno"].Visible = false;

            }
             catch (Exception ex)
             {
             }
        }

        private void load_form()
        {

            try
            {
                Conn.Close();
                Conn.Open();

                //sql = "select Proj_code, Proj_name from proj_master";

                //SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                /////ada.TableMappings.Add("Table", "Leaders");

                //DataSet ds = new DataSet();





                //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                //ds2.AcceptChanges();

                ////set the table as the datasource for the grid in order to show that data in the grid

                //// dgv1.Columns[0].DataPropertyName = "LEADER_NAME";

                //               sql = "select [Col1],[Col2],[Col3] FROM [Grid_Master] where col='1'";
                //               //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";
                //ada = new SqlDataAdapter(sql, Conn);
                //               DataTable dt = new DataTable("Grid_Master");
                //               dt.AcceptChanges();
                //               ada.Fill(ds2, "Grid_Master");
                //               dgv1.Visible = true;
                //               dv.AllowEdit = true;
                //               dv.AllowNew = true;
                //               dv.AllowDelete = true;

                //              ;

                //               dgv1.DataSource = ds2.Tables[0];
                //               // dgv1.Refresh();


                sql = "SELECT VAT_PERCENT FROM AC_OPTIONS WHERE  ac_options.ID =1";
                SqlCommand cmd1 = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd1.ExecuteReader();
                decimal vat_pcnt = 0;
                while (rd.Read())
                {
                    vat_pcnt = Convert.ToDecimal(rd[0].ToString());
                }


                switch (txttrn_type.Text)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        {
                            txtvatpcnt.Text = vat_pcnt.ToString();
                        }
                        break;
                }
                DataGridViewTextBoxColumn Col = new DataGridViewTextBoxColumn();
                Col.HeaderText = "Col";
                Col.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col);

                DataGridViewTextBoxColumn Col1 = new DataGridViewTextBoxColumn();
                Col1.HeaderText = "Col1";
                Col1.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col1);
                DataGridViewTextBoxColumn Col2 = new DataGridViewTextBoxColumn();
                Col2.HeaderText = "Col2";
                Col2.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col2);
                DataGridViewTextBoxColumn Col3 = new DataGridViewTextBoxColumn();
                Col3.HeaderText = "Col2";
                Col3.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col3);


                DataGridViewTextBoxColumn txt7 = new DataGridViewTextBoxColumn();
                txt7.HeaderText = "Qty";
                txt7.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt7);

                DataGridViewTextBoxColumn txt2 = new DataGridViewTextBoxColumn();
                txt2.HeaderText = "Price";
                txt2.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt2);

                DataGridViewTextBoxColumn txtdsc = new DataGridViewTextBoxColumn();
                txtdsc.HeaderText = "disc";
                txtdsc.DefaultCellStyle = dgv1.DefaultCellStyle;
                txtdsc.Visible = false;
                dgv1.Columns.Add(txtdsc);


                DataGridViewTextBoxColumn txtcost = new DataGridViewTextBoxColumn();
                txtcost.HeaderText = "Cost";
                txtcost.Name = "cost";
                txtcost.DefaultCellStyle = dgv1.DefaultCellStyle;
                txtcost.Visible = false;
                dgv1.Columns.Add(txtcost);


                DataGridViewTextBoxColumn txt6 = new DataGridViewTextBoxColumn();
                txt6.HeaderText = "remarks";
                txt6.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt6);


                DataGridViewTextBoxColumn bal = new DataGridViewTextBoxColumn();
                txt6.HeaderText = "Bal.Qty";
                txt6.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(bal);



                DataGridViewTextBoxColumn tot = new DataGridViewTextBoxColumn();
                tot.HeaderText = "Total";
                tot.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(tot);

                DataGridViewTextBoxColumn txt3 = new DataGridViewTextBoxColumn();
                txt3.HeaderText = "Stock";
                txt3.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt3.ReadOnly = true;
                dgv1.Columns.Add(txt3);
                //txt3.Visible = false;


                DataGridViewTextBoxColumn txt4 = new DataGridViewTextBoxColumn();
                txt4.HeaderText = "Fraction";
                txt4.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt4.Visible = false;
                dgv1.Columns.Add(txt4);

                DataGridViewTextBoxColumn txt5 = new DataGridViewTextBoxColumn();
                txt4.HeaderText = "Reorder";
                txt4.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt4.Visible = false;
                dgv1.Columns.Add(txt5);


                //itemcode
                DataGridViewTextBoxColumn txt11 = new DataGridViewTextBoxColumn();

                dgv1.Columns.Add(txt11);

                // proposedprice
                DataGridViewTextBoxColumn txt12 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt12);

                // itemid
                DataGridViewTextBoxColumn txt13 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt13);
                // Hfraction
                DataGridViewTextBoxColumn txt14 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt14);
                // minprofit
                DataGridViewTextBoxColumn txt15 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt15);

                // orgsaleprice fro purchase
                DataGridViewTextBoxColumn txt16 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt16);

                // orgsaleprice fro purchase to update
                DataGridViewTextBoxColumn txt17 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt17);
                DataGridViewTextBoxColumn txt22 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt22);
                DataGridViewTextBoxColumn propose = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn txt23 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt23);
                DataGridViewTextBoxColumn txt24 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt24);
                DataGridViewTextBoxColumn unitid = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(unitid);
                dgv1.Columns.Add(propose);
                dgv1.Refresh();
                dgv1.Columns[0].Name = "orderno";
                dgv1.Columns[1].Name = "barcode";
                dgv1.Columns[2].Name = "Description";

                dgv1.Columns[3].Name = "unit";
                dgv1.Columns[4].Name = "qty";
                dgv1.Columns[5].Name = "Price";
                dgv1.Columns[6].Name = "disc";
                dgv1.Columns[7].Name = "cost";
                dgv1.Columns[9].Name = "balqty";
                dgv1.Columns[8].Name = "remarks";

                dgv1.Columns[10].Name = "Total";
                dgv1.Columns[11].Name = "stock";
                dgv1.Columns[12].Name = "fraction";
                dgv1.Columns[13].Name = "Reorder";


                dgv1.Columns[14].Name = "Itemcode";
                dgv1.Columns[15].Name = "recieved";
                dgv1.Columns[16].Name = "itemid";
                dgv1.Columns[17].Name = "hfraction";
                dgv1.Columns[18].Name = "minprofit";
                dgv1.Columns[19].Name = "orgsaleprice";
                dgv1.Columns[20].Name = "updsale";
                dgv1.Columns[21].Name = "proposed";
                dgv1.Columns[22].Name = "id";
                dgv1.Columns[23].Name = "vat";
                dgv1.Columns[24].Name = "vat%";
                dgv1.Columns[25].Name = "unitid";

                dgv1.Columns["orderno"].HeaderText = "OrderNo";
                dgv1.Columns["barcode"].HeaderText = "Item Code";
                dgv1.Columns["Description"].HeaderText = "Description";
                dgv1.Columns["unit"].HeaderText = "Unit";
                dgv1.Columns["qty"].HeaderText = "Qty";
                dgv1.Columns["Price"].HeaderText = "Price";
                dgv1.Columns["disc"].HeaderText = "Disc.";
                dgv1.Columns["cost"].HeaderText = "Cost";
                dgv1.Columns["balqty"].HeaderText = "Bal.Qty";
                dgv1.Columns["remarks"].HeaderText = "Remarks";

                dgv1.Columns["Total"].HeaderText = "TotalAmt.";
                dgv1.Columns["stock"].HeaderText = "Stock";
                dgv1.Columns["fraction"].HeaderText = "Fraction";
                dgv1.Columns["Reorder"].HeaderText = "Re-order";



                dgv1.Columns["vat"].Visible = false;
                dgv1.Columns["vat%"].Visible = false;

                switch (txttrn_type.Text)
                {
                    case "2":
                        dgv1.Columns["balqty"].HeaderText = "Bal.Qty";
                        dgv1.Columns["unit"].ReadOnly = true;
                     dgv1.Columns["Price"].ReadOnly = true;
                     dgv1.Columns["vat"].Visible = true;
                    

                    break;
                }

                dgv1.Columns["orderno"].ReadOnly = false;
               
                dgv1.Columns["cost"].Visible = false;
                dgv1.Columns["proposed"].Visible = false;
                dgv1.Columns["updsale"].Visible = false;
                dgv1.Columns["fraction"].Visible = false;


                dgv1.Columns["Reorder"].Visible = false;
                dgv1.Columns["Itemcode"].Visible = false;
                dgv1.Columns["recieved"].Visible = false;

                dgv1.Columns["itemid"].Visible = false;
                dgv1.Columns["hfraction"].Visible = false;
                dgv1.Columns["minprofit"].Visible = false;
                dgv1.Columns["id"].Visible = false;
                dgv1.Columns["unitid"].Visible = false;
                dgv1.Columns["orgsaleprice"].Visible = false;

 //               dgv1.Columns[1].ReadOnly = true;
                //dgv1.Columns[3].ReadOnly = true;
                //dgv1.Columns[4].ReadOnly = true;
                dgv1.Columns["cost"].ReadOnly = true;
                dgv1.Columns["balqty"].ReadOnly = true;

                dgv1.Columns["remarks"].ReadOnly = false;
                dgv1.Columns["Total"].ReadOnly = true;
                dgv1.Columns["stock"].ReadOnly = true;
               
                //dgv1.Columns[2].HeaderText = "Qty";
                dgv1.Columns["barcode"].Width = 170;
                dgv1.Columns["Description"].Width = 300;
                dgv1.Columns["qty"].Width = 60;
                dgv1.Columns["Price"].Width = 60;
                dgv1.Columns["disc"].Width = 60;
                dgv1.Columns["remarks"].Width = 200;
                dgv1.Columns["stock"].Width = 80;
                txttrn_type.Text = Gvar.trntype.ToString();


                DataGridViewComboBoxColumn dgvCboColumn = new DataGridViewComboBoxColumn();
               
                Conn.Close();
                Conn.Open();
                sql = "select acc_no,acc_name from accounts inner join ac_options on  acc_type_code=cash_ac_type and   ac_options.ID =1 where acc_no <>  " + Gvar.sale_acno;
                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                ada3 = new SqlDataAdapter(sql, Conn);
                //ada3.Fill(dt);
                DataSet siteds = new DataSet();

                ada3.Fill(siteds, "pay_by");


              

                 switch(txttrn_type.Text)
                 {
                     case "2":
                         {
                             //txtcustomer.Text = Gvar.pur_acno;
                             //txtcusname.Text = "Cash Supplier";
                             lblcustomer.Text = "Supplier";
                            
                             break;
                         }

                     case "22":
                         {
                             //txtcustomer.Text = Gvar.pur_acno;
                             //txtcusname.Text = "Cash Supplier";
                             lblcustomer.Text = "Supplier";
                         }
                         break;
                     case "6":
                         {
                             txtcustomer.Text = Gvar.sale_acno.ToString();
                             txtcusname.Text = "Cash Customer";
                             break;
                         }
                     case "7":
                         {
                             txtcustomer.Text = Gvar.sale_acno.ToString();
                             txtcusname.Text = "Cash Customer";
                             break;
                         }
                         break;
                        
                 }
               


            }




            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:


                    SendKeys.Send("{Tab}");
                    e.Handled = true;
                    break;
                case Keys.Control:
                    dgv1_DoubleClick(sender, null);
                    e.Handled = true;
                    break;
                case Keys.ControlKey:
                    dgv1_DoubleClick(sender, null);
                    e.Handled = true;
                    break;

                default:
                   
                    break;


            }

        }
        private void set_grdlookup()
        {

            try
            {

                if (txttrn_type.Text == "2") return;

                if (dgv1.CurrentCell == null) dgv1.CurrentCell = dgv1["barcode", cur_row];
                if (dgv1.CurrentCell == dgv1["barcode", cur_row])
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    dgv1.EndEdit();
                    string crite = "";
                    object ITM = dgv1["barcode", cur_row].Value;
                    if (ITM == null) ITM = "";
                    if (ITM != "")
                    {
                       // crite = "h.DESCRIPTION like '" + ITM.ToString().Trim() + "%' or h.ITEM_CODE like '" + ITM.ToString().Trim() + "%' OR PART_NO like '" + ITM.ToString().Trim() + "%'";
                        //crite = "(h.DESCRIPTION like '" + ITM.ToString().Trim() + "%' or h.ITEM_CODE like '" + ITM.ToString().Trim() + "%' or h.BARCODE like '" + ITM.ToString().Trim() + "%' )";
                    }

                    //a = InStr(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), " ")

                    //If a > 0 Then
                    //ITM = Left(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), a - 1)
                    //crite = "(DESCRIPTION like '" & Trim(ITM) & "%' or ITEM_CODE like '" & Trim(ITM) & "%' OR PART_NO like '" & Trim(ITM) & "%')"
                    //ITM = Right(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), Len(Trim(myGrid1.TextMatrix(myGrid1.row, 1))) - a)
                    //crite = crite & " AND DESCRIPTION LIKE '%" & ITM & "%'"
                    //End If
                    string EXCLUDE_ITM_cAT;
                    EXCLUDE_ITM_cAT = "0";
                    string sql = "";
                    if (crite != "")
                    {
                        sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM BARCODE as h INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID INNER JOIN OPTIONS ON TRNTYPE= " + txttrn_type.Text + "   where ITM_cAT_CODE NOT IN ( " + EXCLUDE_ITM_cAT + " ) AND " + crite;
                    }
                    else
                    {
                        sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM BARCODE as h INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID INNER JOIN OPTIONS ON TRNTYPE= " + txttrn_type.Text + "   where ITM_cAT_CODE NOT IN ( " + EXCLUDE_ITM_cAT + ") ";
                    }

                    switch (txttrn_type.Text )
                    {
                        case "2":
                            if (txtorderno.Text.Trim() == "")
                            {
                                sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM PUR_ORDER_GRID as h INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID inner join data_entry e on  h.rec_no=e.rec_no    where e.accode ='" + txtcustomer.Text + "'   and h.invoice_no='" + dgv1["orderno", dgv1.CurrentCell.RowIndex].Value + "' and  qty-rqty >0 ";
                                // MessageBox.Show("Invalid Order Number");
                                // return;
                            }

                            else
                            {
                                sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM PUR_ORDER_GRID as h INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID where invoice_no='" + txtorderno.Text + "' and  qty-rqty >0 ";
                            }
                            break;
                    }

                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //GrdLookup.Rows.Clear();
                    GrdLookup.Columns.Clear();
                    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                    DataTable dtlkp = new DataTable("hd_itemmaster");
                    adalkp.Fill(dtlkp);

                    var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                    // dataGrid1.DataContext = dt.DefaultView;
                    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                    GrdLookup.Left = cellRectangle.Left + cellRectangle.Width + dgv1.Left;
                    GrdLookup.Top = textBox1.Top + textBox1.Height;
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    GrdLookup.Tag = "Item_Code";
                    dv.Table = dtlkp;
                    GrdLookup.DataSource = dv;
                    GrdLookup.Columns[0].Width = 170;
                    GrdLookup.Columns[1].Width = 500;
                    GrdLookup.Visible = true;
                    textBox1.Focus();
                    Conn.Close();

                }

                if (cur_col == "Description" && dgv1["barcode", cur_row].Value.ToString() == "999")
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CODE,DESCRIPTION AS DESCR,UNIT FROM OTHER_ITEM ", Conn);

                    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                    DataTable dtlkp = new DataTable("hd_itemmaster");
                    adalkp.Fill(dtlkp);

                    var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                    // dataGrid1.DataContext = dt.DefaultView;
                    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                    GrdLookup.Left = cellRectangle.Left + cellRectangle.Width + dgv1.Left;
                    GrdLookup.Top = textBox1.Top + textBox1.Height;
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    GrdLookup.Tag = "999";
                    dv.Table = dtlkp;
                    GrdLookup.DataSource = dv;
                    GrdLookup.Columns[0].Width = 170;
                    GrdLookup.Columns[1].Width = 300;
                    GrdLookup.Visible = true;
                    textBox1.Focus();
                    Conn.Close();

                    return;
                }
                if (dgv1.CurrentCell == dgv1["unit", cur_row] && dgv1["barcode", cur_row].Value != "" && dgv1["barcode", cur_row].Value != null)
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT unit from barcode WHERE  item_code = '" + dgv1["itemcode", cur_row].Value.ToString() + "' or barcode = '" + dgv1["barcode", cur_row].Value.ToString() + "' ", Conn);

                    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                    DataTable dtlkp = new DataTable("unit");
                    adalkp.Fill(dtlkp);

                    var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                    // dataGrid1.DataContext = dt.DefaultView;
                    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                    GrdLookup.Left = cellRectangle.Left + cellRectangle.Width + dgv1.Left;
                    GrdLookup.Top = textBox1.Top + textBox1.Height;
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    GrdLookup.Tag = "unit";
                    dv.Table = dtlkp;
                    GrdLookup.DataSource = dv;
                    GrdLookup.Columns[0].Width = 170;
                    // GrdLookup.Columns[1].Width = 300;
                    GrdLookup.Visible = true;
                    textBox1.Focus();
                    Conn.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                set_grdlookup();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FrmMRNEntry_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.F1:

                    if (btnnew.Enabled)
                    {
                        btnnew_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F4:

                    if (btnPrint.Enabled)
                    {
                        btnPrint_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F2:

                    if (btnsave.Enabled)
                    {
                        btnsave_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F6:

                    if (btnsearch.Enabled)
                    {
                        btnsearch_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F8:

                    if (btnExit.Enabled)
                    {
                        btnexit_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F9:

                    if (btnoption.Enabled)
                    {
                        btnoption_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;


                case Keys.Enter:
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;

                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    if (GrdLookup.Visible)
                    {
                        GrdLookup.Visible = false;
                        //dgv1.Focus();
                        //dgv1.CurrentCell = dgv1[cur_col, cur_row];
                        //dgv1[cur_col, cur_row].Selected = true;
                        
                    }


                    if (grdcuslookup.Visible)
                    {
                        grdcuslookup.Visible = false;
                        dgv1.Focus();
                    }


                    //e.Handled = true;

                    break;

            }



            if (grdcuslookup.Visible)
            {

                switch (e.KeyCode)
                {



                    case Keys.Up:

                        int crow = grdcuslookup.CurrentRow.Index;
                        int mros = grdcuslookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow > 0)
                            grdcuslookup.CurrentCell = grdcuslookup.Rows[crow - 1].Cells[0];

                        e.Handled = true;

                        break;
                    case Keys.Down:

                        crow = grdcuslookup.CurrentRow.Index;
                        mros = grdcuslookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow < mros - 1)
                            grdcuslookup.CurrentCell = grdcuslookup.Rows[crow + 1].Cells[0];
                        e.Handled = true;

                        break;



                }
            }


            if (GrdLookup.Visible)
            {

                switch (e.KeyCode)
                {



                    case Keys.Up:

                        int crow = GrdLookup.CurrentRow.Index;
                        int mros = GrdLookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow > 0)
                            GrdLookup.CurrentCell = GrdLookup.Rows[crow - 1].Cells[0];

                        e.Handled = true;

                        break;
                    case Keys.Down:

                        crow = GrdLookup.CurrentRow.Index;
                        mros = GrdLookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow < mros - 1)
                            GrdLookup.CurrentCell = GrdLookup.Rows[crow + 1].Cells[0];
                        e.Handled = true;

                        break;



                }







            }


            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GrdLookup.Visible == true && last_col == "barcode")
                {
                    //dgv1.EndEdit();


                    //dgv1.BeginEdit(false);
                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "barCode LIKE  '%" + txt + "%' OR descr LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "barCode <> '0'";


                }

                if (GrdLookup.Visible == true && last_col == "unit")
                {
                    //dgv1.EndEdit();


                    //dgv1.BeginEdit(false);
                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "unit LIKE  '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "unit <> '0'";


                }
            }
            catch
            {

            }



        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Enter:


                        if (GrdLookup.Visible)
                        {

                            if (last_col == "barcode" || last_col == "Description")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;

                                
                                dgv1.BeginEdit(false);
                                dgv1["barcode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                if (last_col == "Description")
                                    dgv1["unit", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[2].Value;

                                dgv1.EndEdit();
                                search_data(dgv1["barcode", dblclk_row].Value.ToString(), "");
                                GrdLookup.Visible = false;
                                dgv1.CurrentCell = dgv1["unit", dgv1.CurrentCell.RowIndex];
                                dgv1.Focus();

                                return;
                                //e.Handled = true;
                                //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }

                            if (last_col == "unit")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;

                                dgv1.CurrentCell = dgv1["unit", last_row];
                                dgv1.BeginEdit(false);
                                dgv1["unit", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                 dgv1["price", dblclk_row].Value = 0;
                               
                                dgv1.EndEdit();
                                search_data(dgv1["barcode", dblclk_row].Value.ToString(), dgv1["unit", dblclk_row].Value.ToString());
                                GrdLookup.Visible = false;
                                dgv1.Focus();
                                dgv1.CurrentCell = dgv1["qty", dgv1.CurrentCell.RowIndex];

                                return;
                                //e.Handled = true;
                                //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }

                        }
                        break;


                    case Keys.ControlKey:
                        dgv1.CurrentCell = dgv1.Rows[0].Cells[dgv1.FirstDisplayedCell.ColumnIndex];
                        dgv1_DoubleClick(sender, null);
                        e.Handled = true;
                        break;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }


        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if (keyData == Keys.Enter)
        //        return base.ProcessDialogKey(Keys.Tab);
        //    else
        //        return base.ProcessDialogKey(keyData);
        //}


        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if (keyData == Keys.Enter)
        //    {
        //        int col = dgv1.CurrentCell.ColumnIndex;
        //        int row = dgv1.CurrentCell.RowIndex;

        //        if (row != dgv1.NewRowIndex)
        //        {
        //            if (col == (dgv1.Columns.Count - 1))
        //            {
        //                col = -1;
        //                row++;
        //            }
        //            dgv1.CurrentCell = dgv1[col + 1, row];
        //        }
        //        return true;
        //    }
        //    return base.ProcessDialogKey(keyData);
        //}

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {
        //        int col = dgv1.CurrentCell.ColumnIndex;
        //        int row = dgv1.CurrentCell.RowIndex;

        //        if (row != dgv1.NewRowIndex)
        //        {
        //            if (col == (dgv1.Columns.Count - 1))
        //            {
        //                col = -1;
        //                row++;
        //            }
        //            dgv1.CurrentCell = dgv1[col + 1, row];
        //        }
        //        e.Handled = true;
        //    }
        //    base.OnKeyDown(e);
        //}




        //ublic class CustomDataGrid : DataGrid
        // {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            try
            {
              

                if (ActiveControl.Name!="")
                acntrl = ActiveControl.Name;
                 if (msg.WParam.ToInt32() == (int)Keys.PageUp && acntrl != "dgv1")
                 {
                     dgv1.Focus();
                     dgv1.CurrentCell = dgv1["barcode", cur_row];
                 }

                

                if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgv1" && !GrdLookup.Visible)
                {
                    // SendKeys.Send("{Tab}");

                    keyData = Keys.Tab;
                    if (dgv1.CurrentCell == dgv1["Remarks", cur_row])
                    {
                        if (dgv1.CurrentCell.RowIndex < dgv1.RowCount - 1)
                        {
                            dgv1.CurrentCell = dgv1[fcol, cur_row + 1];
                        }
                        else
                        {
                            dgv1.CurrentCell = dgv1[fcol, cur_row];
                        }

                    }
                    else
                    {
                        dgv1.EndEdit();
                        SendKeys.Send("{Right}");
                    }
                    return true;
                }



                if (msg.WParam.ToInt32() == (int)Keys.ControlKey && (acntrl == "dgv1" || acntrl == "textBox1") && !GrdLookup.Visible && cur_col == "barcode")
                {
                    // SendKeys.Send("{Tab}");

                    dgv1_DoubleClick(null, null);
                    if (dgv1.CurrentCell.Value != null)
                        textBox1.Text = dgv1.CurrentCell.Value.ToString();
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = textBox1.Text.Length;
                    return true;
                }


                bool nextskip=false;
                string btn;
                btn = this.ActiveControl.GetType().ToString();
                if (this.ActiveControl.GetType().ToString() == "System.Windows.Forms.Button") nextskip = true;

                if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl != "dgv1" && !GrdLookup.Visible && !nextskip)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);

                }
                //if (acntrl == "dgv1" && cur_col=="Description" && dgv1["barcode",cur_row].Value.ToString() !="999")
                //{
                //   switch( msg.WParam.ToInt32())
                //   {
                //       case  (int)Keys.Enter :
                //       case (int)Keys.Right:
                //       case (int)Keys.Up:
                //       case (int)Keys.Down:
                //       case (int)Keys.Left:
                       




                //           break;
                //       default:
                //           return true;

                //    }

                    
                //}

                //return base.ProcessCmdKey(ref msg, Keys.Up);
                //return base.ProcessCmdKey(ref msg, keyData);
                

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);

            }
        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (txtcusname.Text == "")
                {
                    txtcustomer.Enabled = true;
                    txtcustomer.Focus();
                    return;
                }

                if (txttrn_type.Text=="2" && dgv1.CurrentCell.ColumnIndex < dgv1.Columns["qty"].Index)
                {
                    dgv1.CurrentCell = dgv1["qty", e.RowIndex];
                    return;
                }

                acntrl = dgv1.Name;
                cur_row = dgv1.CurrentCell.RowIndex;
                cur_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                if (cur_col == "qty" && (dgv1["description", cur_row].Value == null || dgv1["description", cur_row].Value == ""))
                {
                    dgv1.CurrentCell = dgv1["barcode", cur_row];
                    return;
                }

                if (dgv1.Columns["orderno"].Visible && ( dgv1["orderno", cur_row].Value == null || dgv1["orderno", cur_row].Value == ""))
                {
                    dgv1.CurrentCell = dgv1["orderno", cur_row];
                    return;
                }

                if (GrdLookup.Visible)
                {
                    textBox1.Focus();
                    return;
                }
                 //try
                //{

                //    if (dgv1.CurrentCell == dgv1["barcode", dgv1.CurrentCell.RowIndex] && dgv1.CurrentCell.RowIndex > 0 & dgv1["barcode", dgv1.CurrentCell.RowIndex - 1].Value.ToString() != "")
                //    {
                //        string grid = "";
                //        for (int i = 0; i < dgv1.Rows.Count - 1; i++)
                //        {
                //            for (int col = 0; col < dgv1.Columns.Count; col++)
                //            {
                //                try
                //                {
                //                    grid = grid + dgv1[col, i].Value.ToString() + ",";
                //                }
                //                catch
                //                {
                //                }

                //            }
                //            grid = grid + "|";

                //        }

                //        string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pos.txt");
                //       // using (System.IO.StreamWriter sw = new System.IO.StreamWriter(myTempFile))
                //        {

                //            System.IO.File.WriteAllText(myTempFile, grid);
                //            //sw.WriteLine(grid);
                //        }
                //    }
                //}
                //catch
                //{
                //}

                DataGridView d = (DataGridView)sender;

                if (d[e.ColumnIndex, e.RowIndex].EditType.ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
                    SendKeys.Send("{F4}");



                if (dgv1["barcode", cur_row].Value.ToString() == "999")
                {
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 0;
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "PCS";
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["qty"].Value = "1";
                   
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 0;
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "PCS";
                        if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Value == null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Value == "")
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Value = "OTHER ITEM";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Selected = true;


                    return;
                }



                if (e.ColumnIndex == fcol + 2 && dgv1["barcode", cur_row].Value != "" && dgv1["barcode", cur_row].Value.ToString() != "999")
                {

                    search_data(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["barcode"].Value.ToString(), "");


                   

                }

              






            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void search_data(string Item_Code, string unit)
        {
            try
            {


                Conn.Close();
                Conn.Open();

                if (Item_Code == "999")
                {
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 0;
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "PCS";
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Value = "OTHER ITEM";
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["description"].Selected = true;

                    return;
                }



                string orderno;
                if (txtorderno.Text.ToString().Trim() == "" && dgv1.Columns["orderno"].Visible)
                    orderno= dgv1["orderno", dgv1.CurrentCell.RowIndex].Value.ToString();
                else
                    orderno = txtorderno.Text;


                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = "";
                //  dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["fraction"].Value = "";

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "";

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = "";
                //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = "";

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = "";

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat%"].Value = "";
                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unitid"].Value = "";



                //sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.UNIT,h.FRACTION,s.AVG_PUR_PRICE,s.RE_ORDER,s.stock,u.unit_name from hd_ITEMMASTER h inner join unitmaster u on h.unit=u.unit_id  left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=1 and itm_cat_code=0 and h.Item_Code='" + Item_Code + "'";
                sql = "select BdescrIPTION,stock,avg_PUR_PRICE,LAST_PUR_PRICE,ITEM_CODE,FRACTION,UNIT,stock,wr_code,ITEM_CODE,ITEM_ID,hfraction,barcode,bdescription,r_min_profit,RETAIL_PRICE,vat_percent  from QRY_barcode where  wr_code =" + Gvar.wr_code + " and  flag <> 'C' AND (BARCODE='" + Item_Code + "' OR (item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + Gvar.wr_code;
                sql = "select BdescrIPTION,stock,avg_PUR_PRICE,RETAIL_PRICE,ITEM_CODE,FRACTION,UNIT_" + Gvar.lang_letter + "NAME,stock,wr_code,ITEM_CODE,ITEM_ID,hfraction,barcode,bdescription,r_min_profit,vat_percent,UNIT_NAME,unit_id  from QRY_barcode INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID where  wr_code =" + Gvar.wr_code + " and   flag <> 'C' AND (BARCODE='" + Item_Code + "' OR (item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + Gvar.wr_code;

                switch (txttrn_type.Text)
                {
                        
                    case "2":
                        if (txtorderno.Text.ToString().Trim()== "" && (dgv1.Columns["orderno"].Visible && dgv1["barcode", dgv1.CurrentCell.RowIndex].Value=="" ))
                        {
                            MessageBox.Show("Invalid Order Number ");
                            return;

                        }


                        if (txtorderno.Text.ToString().Trim() == "" && dgv1.Columns["orderno"].Visible)
                            sql = "select BdescrIPTION,stock,avg_PUR_PRICE,LAST_PUR_PRICE,q.ITEM_CODE,p.FRACTION,p.UNIT,stock,Q.wr_code,q.ITEM_CODE,q.ITEM_ID,hfraction,p.barcode,bdescription,r_min_profit,p.rqty,p.qty,p.price,RETAIL_PRICE,unit_id  from QRY_barcode as q INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID inner join   PUR_ORDER_GRID as p on q.barcode = p.barcode INNER JOIN DATA_ENTRY E ON P.REC_NO=E.REC_NO  where E.ACCODE=" + txtcustomer.Text + " AND  P.invoice_no='" + orderno + "' and  qty-rqty >0  and  Q.flag <> 'C' AND (q.BARCODE='" + Item_Code + "' OR (q.item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + Gvar.wr_code;

                        else
                            sql = "select BdescrIPTION,stock,avg_PUR_PRICE,LAST_PUR_PRICE,q.ITEM_CODE,p.FRACTION,p.UNIT,stock,wr_code,q.ITEM_CODE,q.ITEM_ID,hfraction,p.barcode,bdescription,r_min_profit,p.rqty,p.qty,p.price,RETAIL_PRICE,unit_id  from QRY_barcode as q INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID inner join   PUR_ORDER_GRID as p on q.barcode = p.barcode where invoice_no='" + orderno + "' and  qty-rqty >0  and  flag <> 'C' AND (q.BARCODE='" + Item_Code + "' OR (q.item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + Gvar.wr_code;

   

                       
                       // sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT FROM PUR_ORDER_GRID as h where invoice_no='" + txtrefno.Text + "' and  qty-rqty >0 ";

                        break;
                }

                if (unit != "")
                {
                    string sql1 = "";

                    sql1 = sql.Substring(0, sql.IndexOf("where") - 1);
                    sql = sql1 + " where wr_code =" + Gvar.wr_code + " and flag <> 'C' AND (ITeM_code = '" + dgv1["ITEMCODE", dgv1.CurrentCell.RowIndex].Value.ToString() + "' or barcode = '" + dgv1["ITEMCODE", dgv1.CurrentCell.RowIndex].Value.ToString() + "') and unit ='" + unit + "'";


                }

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 0;
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["bARCode"].Value = rd["barcode"].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = rd[0].ToString();
                            //  dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["ItemCode"].Value = rd[4].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["fraction"].Value = rd[5].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = rd["avg_PUR_PRICE"].ToString();
                            if (txttrn_type.Text == "2")
                            {
                                object q = rd["qty"].ToString();
                                object r = rd["rqty"].ToString();
                                int b = Convert.ToInt16(q) - Convert.ToInt16(r);

                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["qty"].Value = b;
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["balqty"].Value = b; // q + "/" + r;
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = rd[17].ToString();
                            }

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = rd[6].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unitid"].Value = rd["unit_id"].ToString();
                            
                             



                            //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["recieved"].Value = rd[3].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = rd[10].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = rd[11].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = rd[14].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = rd["RETAIL_PRICE"].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat%"].Value = rd["vat_percent"].ToString();
                           
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = rd[7].ToString();
                            //btnsave.Enabled = true;
                            //btndelete.Enabled = true;
                            //btnPrint.Enabled = true;
                            nodata = false;
                        }

                        if (rd["RETAIL_PRICE"] == DBNull.Value)
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
                        else
                        {


                            if (Convert.ToDecimal(rd["RETAIL_PRICE"].ToString()) <= 0)
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
                        }

                        if(Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value)>0)
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value =Math.Round(Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value) / Convert.ToDecimal(txtrate.Text),2).ToString();
                        //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["RETAIL_PRICE"].Value = (Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["RETAIL_PRICE"].Value) * Convert.ToDecimal(txtrate.Text)).ToString();
                        if (Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value) > 0)
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value =Math.Round(Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value) / Convert.ToDecimal(txtrate.Text),2).ToString();
                        //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = (Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value) * Convert.ToDecimal(txtrate.Text)).ToString();
                        txtorderno.Enabled = false;
                    }


                }
                else
                {
                    if(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == "")
                    {

                     MessageBox.Show("Invalid Item Found", "Record not Found");
                    
                       
                    

                        //MessageBox.Show("Invalid Item Found, Please check Again", "Invalid Item");

                        nodata = true;


                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = "";
                        //  dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["qty"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["balqty"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["fraction"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = "";
                        //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = "";

                        // dgv1.CurrentCell = dgv1["barcode", cur_row];
                        rd.Close();
                        Conn.Close();
                       // dgv1.CurrentCell = dgv1["orderno", cur_row];
                        isini = false;
                        return;
                    }

                }
               // dgv1.CurrentCell = dgv1["qty", cur_row];
                rd.Close();
                Conn.Close();
                isini = false;

            }


            catch (System.Data.SqlClient.SqlException excep)
            {

                //MessageBox.Show(excep.Message);

            }
        }

        private void dgv1_Leave(object sender, EventArgs e)
        {
            acntrl = "";
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            dgv1_DoubleClick(sender, e);

        }
        private void GrdcusLookup_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                KeyEventArgs e1 = new KeyEventArgs(Keys.Enter);


                txtcustomer_KeyDown(null, e1);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void GrdLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox1_DoubleClick(sender, e);
        }
        private void GrdLookup_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int lkprow = 0;


                lkprow = GrdLookup.CurrentCell.RowIndex;

                switch (GrdLookup.Tag.ToString())
                {


                    case "Item_Code":
                        {
                            dgv1.CurrentCell = dgv1["barCode", dblclk_row];

                            dgv1.BeginEdit(true);
                            dgv1["barCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                            dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                            dgv1.EndEdit();

                            GrdLookup.Visible = false;
                            search_data(GrdLookup.Rows[lkprow].Cells[0].Value.ToString(), "");
                            if (nodata)

                                this.dgv1.CurrentCell = this.dgv1["barCode", dblclk_row];

                            else
                            {
                                //object nextline = rec_options.Rows[0]["Auto_Next_line"];
                                decimal price = 0;
                                if (dgv1["price", cur_row].Value == null) dgv1["price", cur_row].Value = 0;
                                if (!string.IsNullOrEmpty(dgv1["price", cur_row].Value.ToString()))
                                    price = Convert.ToDecimal(dgv1["price", cur_row].Value.ToString());


                                else
                                {

                                   
                                    //this.ActiveControl = dgv1;
                                   dgv1.CurrentCell = dgv1["unit", cur_row];
                                }


                               
                            }

                            dgv1.Focus();
                            break;
                        }

                    case "999":
                        {


                            dgv1.BeginEdit(false);
                            dgv1["barCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                            dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                            dgv1["unit", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[2].Value;
                            dgv1.EndEdit();

                            GrdLookup.Visible = false;

                            this.dgv1.CurrentCell = this.dgv1["qty", dblclk_row];

                            dgv1.Focus();
                            break;
                        }



                    case "unit":

                        dgv1.CurrentCell = dgv1["unit", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["unit", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        dgv1["price", dblclk_row].Value = "0";
                        dgv1.EndEdit();
                        search_data(dgv1["barcode", dblclk_row].Value.ToString(), dgv1["unit", dblclk_row].Value.ToString());
                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["qty", dblclk_row];

                        dgv1.Focus();
                        break;
                    case "inv":
                        if (GrdLookup.Rows.Count < 1) return;
                        txtinvno.Text = GrdLookup[0, GrdLookup.CurrentCell.RowIndex].Value.ToString();
                        GrdLookup.Visible = false;
                        search_mrn();
                        dgv1.Focus();
                        break;

                    case "ord":
                        if (GrdLookup.Rows.Count < 1) return;
                        txtorderno.Text = GrdLookup[0, GrdLookup.CurrentCell.RowIndex].Value.ToString();
                        GrdLookup.Visible = false;
                        txtorderno_Validated(sender, e);
                        dgv1.Focus();
                        break;


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void grdcuslookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtcustomer_KeyDown(null, e);

        }

        private void GrdLookup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            acntrl = "textBox1";
        }

        private void dgv1_Enter(object sender, EventArgs e)
        {
            if(txtcusname.Text=="")
            {
                txtcustomer.Enabled = true;
                txtcustomer.Focus();
                return;
            }
            acntrl = dgv1.Name;
        }

        private void FrmMRNEntry_Activated(object sender, EventArgs e)
        {
            try
            {
                if (txttrn_type.Text == "2")
                    fcol = dgv1.Columns["qty"].Index;
                else

                fcol = dgv1.FirstDisplayedCell.ColumnIndex;
            }

            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }


        }

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgv1["barcode", cur_row].Value == null || dgv1["barcode", cur_row].Value == "") return;
                if (!GrdLookup.Visible)
                {
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    last_row = dgv1.CurrentCell.RowIndex;
                }
                if (cur_row > dgv1.Rows.Count - 1 || cur_col == null) return;
               if(dgv1.IsCurrentCellDirty)
                dgv1.EndEdit();
                //if (dgv1[cur_col, cur_row].Selected && dgv1[cur_col, cur_row].Value !="")
                //dgv1[cur_col, cur_row].Selected = false;
               
                if (dgv1["barcode", cur_row].Value == null) dgv1["barcode", cur_row].Value = "";

                if (cur_col == "barcode" && cur_row > 0 && dgv1["barcode", cur_row].Value.ToString() !="")

                {
                    for (int i = 0; i < cur_row ; i++)
                    {
                        if (txttrn_type.Text == "2" & txtorderno.Text == "")
                        {
                            if (dgv1["barcode", cur_row].Value.ToString() == dgv1["barcode", i].Value.ToString() && dgv1["orderno", cur_row].Value.ToString() == dgv1["orderno", i].Value.ToString())
                            {
                                MessageBox.Show("This Item is already entered , Please Check and Try again");
                                 dgv1["barcode", cur_row].Value = "";
                                 return;
                            }

                        }
                        else
                        {
                            if (dgv1["barcode", cur_row].Value.ToString() == dgv1["barcode", i].Value.ToString())
                            {
                                MessageBox.Show("This Item is already entered , Please Check and Try again");
                                 dgv1["barcode", cur_row].Value = "";
                                 return;
                            }
                        }

                    }

                }

               // dgv1.CurrentCell.InheritedStyle.BackColor = dgv1.CurrentCell.InheritedStyle.BackColor;
               
                //string celval = Convert.ToString(dgv1["Description", cur_row].Value);
                //if (last_col > 6 && celval.Trim() == "") dgv1.CurrentCell = dgv1["Item_Code", cur_row];

                if (cur_col == "qty" && Gvar.trntype == 2)
                {
                    object val =  dgv1["balqty",cur_row].Value;
                    if (val !="" && val != null)
                    {

                    //string [] a = val.ToString().Split('/');
                    //int q = Convert.ToInt16(a[0]);

                        int r = Convert.ToInt16(val);
                    if (dgv1["qty", last_row].Value == "") dgv1["qty", last_row].Value = 1;
                    if (Convert.ToDecimal( dgv1["qty", last_row].Value) > (r) && dgv1["description", last_row].Value != "")
                    {
                        MessageBox.Show("Invalid Receiving quantity");
                        dgv1["qty", last_row].Value = (r).ToString();

                    }
                    }
                }
                else
                {
                    if (last_col == "qty" && dgv1["qty", last_row].Value == null && dgv1["description", last_row].Value != "")
                        dgv1["qty", last_row].Value = 1;
                }

                if (!dgv1["barcode", cur_row].Value.Equals("999"))
                {
                    //if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value == "") dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = 0;
            //    if (last_col == "Price" && Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value) > 0 && Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value) != Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value))
            //        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
            }
                if (e.ColumnIndex == dgv1["disc", e.RowIndex].ColumnIndex || e.ColumnIndex == dgv1["qty", e.RowIndex].ColumnIndex || e.ColumnIndex == dgv1["cost", e.RowIndex].ColumnIndex || e.ColumnIndex == dgv1["price", e.RowIndex].ColumnIndex)
                {
                    find_total();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void cmbproject_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Conn.Close();
        //        Conn.Open();
        //        sql = "select site_code, site_name,proj_code from site_master where proj_code=" + cmbproject.SelectedValue;
        //        SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
        //        ada3 = new SqlDataAdapter(sql, Conn);
        //        //ada3.Fill(dt);
        //        DataSet siteds = new DataSet();

        //        ada3.Fill(siteds, "site_master");

        //        //ada1.Fill(ds, "proj_master");
        //        //projdv.Table = dt;
        //        cmbsite.DisplayMember = "site_name";
        //        cmbsite.ValueMember = "site_code";
        //        cmbsite.DataSource = siteds.Tables[0];
        //    }
        //    catch (SqlException ex)
        //    {
        //        //ADOconn.RollbackTrans();

        //        MessageBox.Show(ex.Message);
        //    }
        //}


        private void get_invno()
        {
            try
            {
                if (ADOconn.State==0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
             

                ADODB.Recordset tmp = new ADODB.Recordset();

                sql = "SELECT max(TRaN_NO)+1 FROM DATA_ENTRY WHERE TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)
                //    txttrn.Text="1";
                //else
                int trn;
                if (Convert.IsDBNull(tmp.Fields[0].Value))
                    trn = 1;
                else
                    trn = (int)tmp.Fields[0].Value;


                txttrn.Text = trn.ToString();

                txtinvno.Text = Gvar.trn_no(Convert.ToInt32(txttrn_type.Text));

            }
            catch (Exception ex)
            {

            }
        }

        private void SAVE_DATAENTRY()
        {
            //DataGridViewCell ccell = dgv1.CurrentCell;
            //dgv1.CurrentCell = dgv1["Item_Code", 0];
            //dgv1.CurrentCell = ccell;

            dgv1.EndEdit();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            int i = 0;
            try
            {
                try
                {


                chk:
                    ADODB.Recordset rec = new ADODB.Recordset();

                    sql = "SELECT ISEDIT FROM ISEDIT";


                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    while (!rec.EOF)
                    {
                        for (i = 0; i < 1000; i++)
                        {
                            Application.DoEvents();
                            if (rec.Fields[0].Value.ToString() == "1")
                            {
                                goto chk;
                            }
                            else
                            {
                                editok();
                                break;
                            }
                        }

                        editno();

                        rec.MoveNext();
                    }
                    editok();





                    if (!isedit)
                    {
                        get_invno();
                    }

                    //ADOconn.BeginTrans();
                    decimal paidcash = 0;
                    decimal paidother = 0;
                    decimal balance = 0;


                   

                    if (Gvar.orgdup == null) Gvar.orgdup = "ORG";

                    rec = new ADODB.Recordset();
                    if (txttrn_type.Text == "2") goto ord;
                   // sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + txtrefno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  TRN_TYPE=22 and  ORG_DUP ='" + Gvar.orgdup + "' and BRN_CODE =" + Gvar.brn_code;
                   
                        sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text) + " AND ORG_DUP ='" + Gvar.orgdup + "' and BRN_CODE =" + Gvar.brn_code;
                    
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    
                    if (rec.RecordCount == 0)
                    {

                        rec.AddNew();
                        rec.Fields["flag"].Value = "N";
                        rec.Fields["user_name"].Value = Gvar.Userid;


                    }





                    rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                    rec.Fields["TRAN_NO"].Value = Convert.ToInt32(txttrn.Text);
                    rec.Fields["CURDATE"].Value = dt1.Value;
                    rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                    rec.Fields["ORG_DUP"].Value = Gvar.orgdup;
                    rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["ENTRY_TYPE"].Value = "Retail";
                    rec.Fields["CRATE"].Value = txtrate.Text;
                    rec.Fields["ACCODE"].Value = txtcustomer.Text;
                    rec.Fields["ename"].Value = txtcusname.Text;
                    //rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["REMARKS"].Value = txtremarks.Text;

                    rec.Fields["Currency"].Value =cmbcurrency.SelectedValue;
                    rec.Fields["REF_NO"].Value = txtrefno.Text;
                    rec.Fields["order_NO"].Value = txtorderno.Text;
                    rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txtrate.Text);
                    rec.Fields["DISC_AMT"].Value = Convert.ToDouble(txtdscamt.Text) * Convert.ToDouble(txtrate.Text);
                    rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text); 
                    rec.Fields["cash_paid"].Value = paidcash * Convert.ToDecimal(txtrate.Text); ;
                    rec.Fields["other_paid"].Value = paidother * Convert.ToDecimal(txtrate.Text);
                    rec.Fields["FRN_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text);
                    rec.Fields["sales_code"].Value = Gvar.Userid;
                    tmp = new ADODB.Recordset();
                    sql = "sELECT  INV_PREFEX  froM TRN_TYPE WHERE TRN_CODE = " + txttrn_type.Text ;
                    tmp .Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (tmp.RecordCount > 0)
                        rec.Fields["INV_PREFEX"].Value = tmp.Fields[0].Value;
                    // rec.Fields["ename"].Value = cmbproject.Text;

                    //rec.Fields["DELIVERY_DATE"].Value = dtdelivery.Text;
                    rec.Fields["wr_code"].Value = Gvar.wr_code;
                    rec.Fields["NYEAR"].Value =nyear.Text;
                    rec.Fields["order_NO"].Value =txtinvno.Text;
                    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                    
                    rec.Update();

                    ord:

                    sql = "SELECT rec_no FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    decimal rec_no = (decimal)rec.Fields["REC_NO"].Value;
                    var a = 0;

                    sql = "update   PUR_ORDER_GRID set ref_no = 1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;
                    ////ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)
                    
                    //SqlCommand cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();
                    object out1;
                    ADOconn.Execute(sql,out out1);
                   
                    


                    // foreach (DataGridViewRow row in this.dgv1.Rows)

                    for (i = 0; i < dgv1.RowCount; i++)
                    {
                        // i=i+1;
                        string celval = Convert.ToString(dgv1["ItemCode", i].Value);


                        //int = dgv1[barcode, i].Value.Equals(null);
                        // MessageBox.Show(celval.ToString());
                        // //string celval =  dgv1[barcode,i].Value.ToString();

                        // if (!Convert.IsDBNull(dgv1[barcode, i].Value) && !Convert.IsDBNull(dgv1["qty", i].Value))
                        if (celval.Trim() != "")
                        {
                            sql = "SELECT * FROM PUR_ORDER_GRID WHERE Item_Code = '" + dgv1["ItemCode", i].Value.ToString().Trim() + "' and BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;
                            rec = new ADODB.Recordset();

                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            if (rec.RecordCount == 0)
                            {
                                rec.AddNew();
                                rec.Fields["REC_NO"].Value = rec_no;
                                rec.Fields["ROWNUM"].Value = lastrec;
                            }
                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                            rec.Fields["Ref_NO"].Value = 0;
                            
                            rec.Fields["Item_Code"].Value = dgv1["ItemCode", i].Value.ToString().Trim(); ;
                            //rec.Fields["PRICE"].Value = dgv1["price", i].Value;
                            rec.Fields["Description"].Value = dgv1["Description", i].Value;
                            rec.Fields["QTY"].Value = dgv1["qty", i].Value;
                            if (dgv1["recieved", i].Value == null) dgv1["recieved", i].Value = "0";
                            if (dgv1["qty", i].Value == null) dgv1["qty", i].Value = "0";

                            if(Convert.ToDecimal(dgv1["qty", i].Value) < Convert.ToDecimal(dgv1["recieved", i].Value))
                            {
                                
                                editno();
                                
                                iserror = true;
                                lblmsg.Text = "Item Request Quantity Less than Recieved Quaintity ( " + dgv1["ItemCode", i].Value.ToString().Trim() +")";
                                return;
                                //MessageBox.Show(lblmsg.Text);
                            }



                            if (txttrn_type.Text != "22")
                                rec.Fields["RQTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) + Convert.ToDecimal(dgv1["recieved", i].Value);

                            rec.Fields["price"].Value = Convert.ToDecimal( dgv1["price", i].Value) * Convert.ToDecimal(txtrate.Text) ;
                            rec.Fields["BARCODE"].Value = dgv1["barcode", i].Value;
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["UNIT"].Value = dgv1["unitid", i].Value;
                            if (dgv1["cost", i].Value == null || dgv1["cost", i].Value == "")
                                dgv1["cost", i].Value = 0;
                            rec.Fields["SALE_PUR_AMT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text); 
                            rec.Fields["ITM_TOTAL"].Value = dgv1["total", i].Value;
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                            //if (dgv1["proposed", i].Value==null)
                            //{
                            //    dgv1["proposed", i].Value = dgv1["price", i].Value;

                            //}

                            //rec.Fields["PROPOSE_PRICE"].Value = dgv1["proposed", i].Value;
                           // object propose = rec.Fields["PROPOSE_PRICE"].Value;
                            //if (propose == "0") rec.Fields["PROPOSE_PRICE"].Value = dgv1["price", i].Value;
                            rec.Fields["ITEM_ID"].Value = dgv1["itemid", i].Value;
                            //rec.Fields["hfraction"].Value = dgv1["hfraction", i].Value;
                            //rec.Fields["wr_code"].Value = Gvar.wr_code;
                            if (dgv1["disc", i].Value == null || dgv1["disc", i].Value == "")
                                dgv1["disc", i].Value = 0;
                            rec.Fields["disc"].Value = Convert.ToDecimal(dgv1["disc", i].Value) * Convert.ToDecimal(txtrate.Text); 
                            rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                            rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                            //rec.Fields["REF_NO"].Value = txtinvno.Text;
                            rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                             rec.Fields["brn_code"].Value = Gvar._brn_code;

                            rec.Fields["UNIT_QTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["UNIT_PRICE"].Value = (Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value))*Convert.ToDecimal(txtrate.Text);
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal( rec.Fields["UNIT_TRN_AMOUNT"].Value) * Convert.ToDecimal(txtrate.Text); 
                            }
                            else
                            {
                                rec.Fields["UNIT_PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) * Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtrate.Text); ; ;
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text); ;

                            }

                            if (rec.Fields["RQTY"].Value == DBNull.Value||rec.Fields["RQTY"].Value ==null) rec.Fields["RQTY"].Value = "0";
                            rec.Update();


                        }

                    }

                    sql = "delete    PUR_ORDER_GRID  WHERE  ref_no = 1 and rqty=0 and  BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;
                    ////ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)
                                        
                    ADOconn.Execute(sql, out out1);


                    if (txttrn_type.Text == "22")
                    {
                        sql = "update   PUR_ORDER_GRID set ref_no = 0 WHERE ref_no = 1 and   BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;
                        ////ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)


                        //SqlCommand cmd = new SqlCommand(sql, Conn);
                        //cmd.ExecuteNonQuery();
                        
                        ADOconn.Execute(sql, out out1);
                       if (Convert.ToInt16( out1) > 0)
                       {
                            editno();

                            iserror = true;
                            lblmsg.Text = "Some Item/s are Deleted while it is already Received!!, Cannot be Delete Such Items";
                            return;
                            //MessageBox.Show(lblmsg.Text);
                        }
                    }



                    rec = new ADODB.Recordset();
                    sql = "SELECT * FROM PurOrder_Terms WHERE Order_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  BRN_CODE =" + Gvar.brn_code;

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {

                        rec.AddNew();
                       
                    }

                    rec.Fields["Order_no"].Value = txtinvno.Text;
                    rec.Fields["BRN_CODE"].Value =  Gvar.brn_code;
                    rec.Fields["NYEAR"].Value = nyear.Text;

                      rec.Fields["Ship_Term"].Value = cmbshipterm.Text;
                      rec.Fields["PAY_TERM"].Value = cmbterm.Text;
                      rec.Fields["SHIPMENT"].Value = cmbmode.Text;

                      rec.Fields["PORT"].Value = cmbport.Text;

                      rec.Fields["address"].Value = cmbaddress.SelectedValue;
                    rec.Fields["OTHER"].Value =  txtotherterms.Text;
                    rec.Fields["left_sign"].Value =cmbleftsign.Text;
                    rec.Fields["right_sign"].Value = cmbrightsign.Text;

                    rec.Fields["left_sign_name"].Value = cmbleftsign.SelectedValue.ToString();
                    rec.Fields["right_sign_name"].Value = cmbrightsign.SelectedValue.ToString();
                    if (!chkdeldate.Checked)
                    rec.Fields["delivery_date"].Value = dtdelivery.Value.Date.ToString("yyyy-MM-dd");
                    rec.Fields["REC_NO"].Value = rec_no;

                    rec.Update();

                    //sql="update data_entry set flag='N' where trn_type=11 and invoice_no='" + txtinvno.Text.Trim() +"'";

                    //tmp = new ADODB.Recordset();



                    //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    //cmd = new SqlCommand(sql, Conn);
                    //  cmd.ExecuteNonQuery();
                    oldcusno = txtcustomer.Text;
                    iserror = false;
                    
                }
                catch (SqlException ex)
                {
                    //ADOconn.RollbackTrans();
                    iserror = true;
                    lblmsg.Text = ex.Message;
                    editno();
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception er)
            {
                editno();
                iserror = true;
                lblmsg.Text = er.Message;
                MessageBox.Show(er.Message);
            }



        }
       

       

     
       




        private
            void TxtmrnNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GrdLookup.Visible == true )
                {
                    //dgv1.EndEdit();


                    //dgv1.BeginEdit(false);
                    string txt = txtinvno.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "Invoice_NO LIKE  '%" + txt + "%' OR ENAME LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "ENAME <> '0'";

                }

                //btnsave.Enabled = false;
               // btndelete.Enabled = false;
               // btnPrint.Enabled = false;

            }
            catch
            { }
        }

        private void Txtinvno_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                txtinvno.Text = "";
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=" + Convert.ToInt16(txttrn_type.Text) + "  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = txtinvno.Left;
                GrdLookup.Top = txtinvno.Top + txtinvno.Height;
                dv.AllowEdit = true;
                dv.Table = dt;
                GrdLookup.Tag = "inv";
                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                GrdLookup.Visible = true;
                //grdIssues.Columns[1].ItemStyle.Width = 100;

                //DataGridTableStyle ts = new DataGridTableStyle;

                //foreach (DataControlField column in dgv1.Columns)
                //{
                //    column.ItemStyle.Width = Unit.Pixel(100);
                //}



            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);


            }
        }


        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            search_mrn();
        }


        private void search_mrn()
        {



            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset rec = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            rec = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            
            try
            {
                if (txtinvno.Text == "") return;
                dgv1.CellEnter -= dgv1_CellEnter;
                dgv1.SelectionChanged -= dgv1_SelectionChanged;


                isedit = false;
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND NYEAR=" + nyear.Text + " AND  TRN_TYPE=" + Convert.ToInt16(txttrn_type.Text) + "  AND INVOICE_NO= '" + txtinvno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)
                load_ini();
                cur_row = 0;
                if (rec.RecordCount > 0)
                {

                  

                    object rec_no = rec.Fields["REC_NO"].Value;

                    cmbcurrency.SelectedValue = rec.Fields["currency"].Value;
                    txtinvno.Text = rec.Fields["INVOICE_NO"].Value.ToString();
                    txttrn.Text = rec.Fields["TRAN_NO"].Value.ToString();
                    txtrate.Text = rec.Fields["CRATE"].Value.ToString();
                    dt1.Value = Convert.ToDateTime(rec.Fields["CURDATE"].Value.ToString());


                    isedit = true;
                    txtcustomer.Text = rec.Fields["ACCODE"].Value.ToString();
                    txtcusname.Text = rec.Fields["ename"].Value.ToString();
                    oldcusno = rec.Fields["ACCODE"].Value;
                    txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                    txtorderno.Text = rec.Fields["order_NO"].Value.ToString();
                    txtorderrecno.Text = rec.Fields["order_recNO"].Value.ToString();
                    txtremarks.Text = rec.Fields["remarks"].Value.ToString();

                    txtvatpcnt.Text = rec.Fields["VAT_PERCENT"].Value.ToString();
                    txtvatamt.Text = rec.Fields["VAT_AMOUNT"].Value.ToString();
                    if(Convert.ToDecimal( rec.Fields["DISC_AMT"].Value) >0)
                    txtdscamt.Text = (Convert.ToDecimal( rec.Fields["DISC_AMT"].Value.ToString()) / Convert.ToDecimal(txtrate.Text)).ToString();
                    //btnsave.Enabled = true;
                    //btndelete.Enabled = true;
                    //btnPrint.Enabled = true;
                    lblinvstatus.Text = "***";

                    lblprefex.Text = rec.Fields["inv_prefex"].Value + "-" + rec.Fields["nyear"].Value + "-" + rec.Fields["ORDER_NO"].Value.ToString();
                    lblmsg.Text = "View / Edit Entry......";
                    lblmsg.BackColor = Color.LightGray;
                    if (rec.Fields["flag"].Value.ToString() == "D")
                    {
                        btnsave.Enabled = false;
                        btndelete.Enabled = false;
                        lblinvstatus.Text = "Invoice Deleted!!!";
                        lblmsg.Text = "Invoice Deleted!!!";
                        lblmsg.BackColor = Color.Red;
                    }

                    var a = 0;
                    string trmsql;
                    rec = new ADODB.Recordset();
                    if (txttrn_type.Text == "2")
                    {
                        sql = "SELECT  e.*,stock,p.qty as pqty,p.rqty,p.id,unit_id,unit_name FROM data_entry_GRID as e inner INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID inner join data_entry as d on e.rec_no=d.rec_no inner join PUR_ORDER_GRID as p on e.item_code=p.item_code and e.ORDEr_recno=p.rec_no and e.trn_type=2  left join stock_master  on e.Item_Code=stock_master.Item_Code WHERE e.REC_NO=" + rec_no;
                        trmsql = "SELECT * FROM PurOrder_Terms WHERE Order_NO = '" + txtorderno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  BRN_CODE =" + Gvar.brn_code;
                        btnsave.Enabled = false;
                    }
                    else
                    {
                        sql = "SELECT  PUR_ORDER_GRID.*,stock,unit_id,unit_name FROM PUR_ORDER_GRID INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID left join stock_master  on PUR_ORDER_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no;
                        trmsql = "SELECT * FROM PurOrder_Terms WHERE Order_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  BRN_CODE =" + Gvar.brn_code;
            
                    }
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //    //dgv1.Rows.Clear();
                    //for (a=0; a< dgv1.RowCount-1;a++)
                    //{
                    //    dgv1.Rows.RemoveAt(a);
                    //    }
                    int i = 0;
                    // dgv1.Rows.Add(rec.RecordCount+1);

                    dgv1.Rows.Clear(); dgv1.Refresh();

                    bool isbalance = false;
                    bool isreceived = false;

                    // foreach (DataGridViewRow row in this.dgv1.Rows)
                    while (!rec.EOF)
                    {
                        //ds2.Tables[0].Rows.Add();
                        dgv1.Rows.Add();
                        dgv1["orderno", i].Value = rec.Fields["order_no"].Value.ToString();
                        dgv1["barcode", i].Value = rec.Fields["barcode"].Value.ToString();
                        dgv1["itemcode", i].Value = rec.Fields["item_code"].Value.ToString();
                        dgv1["price", i].Value = rec.Fields["FPRICE"].Value.ToString();
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();

                        dgv1["unit", i].Value = rec.Fields["Unit_name"].Value.ToString();
                        dgv1["unitid", i].Value = rec.Fields["Unit_id"].Value.ToString();
                        // dgv1["stock", i].Value = rec.Fields["stock"].Value.ToString();
                        // rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                        dgv1.Rows[i].Cells["updsale"].Value = "0";
                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                        dgv1["cost", i].Value = rec.Fields["SALE_PUR_AMT"].Value.ToString();
                        dgv1["total", i].Value = Convert.ToDecimal(rec.Fields["QTY"].Value.ToString()) * Convert.ToDecimal(rec.Fields["FPRICE"].Value.ToString());
                        dgv1["recieved", i].Value = rec.Fields["rqty"].Value.ToString();

                        object qty = rec.Fields["qty"].Value.ToString();
                        if (qty == "0" || qty == "") dgv1["qty", i].Value = "0";
                        qty = rec.Fields["qty"].Value.ToString();
                        if (qty == "0" || qty =="" ) dgv1["recieved", i].Value = "0";
                        if (dgv1["recieved", i].Value==null) dgv1["recieved", i].Value="0";

                         dgv1["itemid", i].Value = rec.Fields["ITEM_ID"].Value.ToString();
                         dgv1["id", i].Value = rec.Fields["id"].Value.ToString();
                         dgv1["BalQty", i].Value = Convert.ToDouble(qty) - Convert.ToDouble(rec.Fields["rQTY"].Value.ToString());

                         if (Convert.ToDouble(qty) - Convert.ToDouble(rec.Fields["rQTY"].Value.ToString()) > 0)
                             isbalance = true;
                         if (Convert.ToDouble(rec.Fields["rQTY"].Value.ToString()) > 0)
                             isreceived = true;

                        if (txttrn_type.Text == "2")
                        {
                            object q = rec.Fields["pqty"].Value.ToString(); ;
                            object r = rec.Fields["rqty"].Value.ToString(); ;
                            decimal b = Convert.ToDecimal(q) - Convert.ToDecimal(r);
                            dgv1["BalQty", i].Value = Convert.ToDecimal(rec.Fields["pqty"].Value.ToString()) - Convert.ToDecimal(dgv1["recieved", i].Value);
                            dgv1["BalQty", i].Value = rec.Fields["rqty"].Value;
                            //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["qty"].Value = b;
                           // dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["balqty"].Value = q + "/" + r;
                        }
                         


                        //dgv1["hfraction", i].Value = rec.Fields["hfraction"].Value.ToString();
                        //dgv1["disc", i].Value = rec.Fields["disc"].Value.ToString();
                        if (lastrec < Convert.ToDecimal(rec.Fields["rownum"].Value.ToString())) lastrec = Convert.ToInt32(rec.Fields["rownum"].Value.ToString());
                        i = i + 1;
                        rec.MoveNext();

                    }
                    lastrec++;
                    find_total();
                    btnsave.Enabled = false;
                    btndelete.Enabled = false;
                    if (isbalance) btnsave.Enabled = true;
                    if (!isreceived) btndelete.Enabled = true;
                    btnPrint.Enabled = true;

                    isedit = true;
                    dgv1.Columns["barcode"].ReadOnly = false;
                    dgv1.Columns["unit"].ReadOnly = false;
                    dgv1.Columns["qty"].ReadOnly = false;
                    dgv1.Columns["Price"].ReadOnly = false;
                    dgv1.Columns["disc"].ReadOnly = false;

                    dgv1.CellEnter += dgv1_CellEnter;
                    dgv1.SelectionChanged += dgv1_SelectionChanged;


                    rec = new ADODB.Recordset();
                    
                    rec.Open(trmsql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {



                    }
                    else
                    {
                        cmbshipterm.Text = rec.Fields["Ship_Term"].Value.ToString();
                        cmbterm.Text = rec.Fields["PAY_TERM"].Value.ToString();
                        cmbmode.Text = rec.Fields["SHIPMENT"].Value.ToString();
                        cmbport.Text = rec.Fields["PORT"].Value.ToString();
                        cmbaddress.Text = rec.Fields["address"].Value.ToString();
                        txtotherterms.Text = rec.Fields["OTHER"].Value.ToString();
                        cmbleftsign.Text = rec.Fields["left_sign"].Value.ToString();
                        cmbrightsign.Text = rec.Fields["right_sign"].Value.ToString();

                        if (rec.Fields["DELIVERY_DATE"].Value != DBNull.Value)
                            dtdelivery.Value = Convert.ToDateTime(rec.Fields["DELIVERY_DATE"].Value.ToString());

                       // dtdelivery.Value = Convert.ToDateTime( rec.Fields["delivery_date"].Value); 

                    }


                    

                }


                else
                {
                    dgv1.CellEnter += dgv1_CellEnter;
                    dgv1.SelectionChanged += dgv1_SelectionChanged;
                    MessageBox.Show("Invalid Invoice Number", "Invalid Invoice Entry");
                }

                cmbcurrency.Enabled = true;
               
                isdirty = false;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                dgv1.CellEnter += dgv1_CellEnter;
                dgv1.SelectionChanged += dgv1_SelectionChanged;
            }

        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

      

        private void load_ini()

        {
            
            btnsave.Enabled = true;
            lastrec = 1;
            cur_col = "barcode";
            cur_row = 0;
            oldcusno = 0;
            last_col = "barcode";
            last_row = 0;
            txtorderrecno.Text = "";
            lblprefex.Text = "";
            txtorderno.Enabled = true;
            dgv1.Rows.Clear();
            cmbcurrency.Enabled = true;
            txtnetamt.Text = "0";
            txtdscamt.Text = "0";
            dtdelivery.Value = DateTime.Now;
            dgv1.Refresh();
            txtinvno.Text = "";
            txtrefno.Text = "";
            txtorderno.Text = "";
            txttrn.Text = "";
            txttotal.Text = "";
            txtvatamt.Text = "0";
            txtvatpcnt.Text = "0";
            cmbmode.SelectedIndex = -1;
            cmbshipterm.SelectedIndex = -1;
            cmbterm.SelectedIndex = -1;
            cmbmode.SelectedIndex = -1;
            cmbaddress.SelectedIndex = -1;
            cmbport.SelectedIndex = -1;
            txtremarks.Text = "";
            txtotherterms.Text = "";
            txtorderno.ReadOnly = false;
            txtcustomer.Text = "";
            txtcusname.Text = "";
            isedit = false;
            dgv1.Rows.Add(2);
            dt1.Value = DateTime.Now;
            lblinvstatus.Text = "***";
            lblmsg.Text = "***";
            txtcustomer.Enabled = true;
            if (txttrn_type.Text == "2")
            {
                lblorderdate.Visible = true;
                orderdate.Visible = true;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                lblinv.Text = "Reciept No.";
                // lblref.Text="Order No.";
                cmbmode.Enabled = false;
                cmbport.Enabled = false;
                cmbshipterm.Enabled = false;
                txtotherterms.Enabled = false;
                cmbterm.Enabled = false;
                lblorder.Visible = true;
                txtorderno.Visible = true;
                dgv1.Columns["cost"].Visible = false;
                dgv1.Columns["price"].Visible = false;
                dgv1.Columns["total"].Visible = false;
                dgv1.Columns["cost"].HeaderCell.Value = "Sale Price";
                dgv1.Columns["cost"].ReadOnly = false;
                lblgross.Visible = false;
                lblnet.Visible = false;
                txttotal.Visible = false;
                txtnetamt.Visible = false;
                lblleftsign.Visible = false;
                lblrightsign.Visible = false;
                cmbleftsign.Visible = false;
                cmbrightsign.Visible = false;
                cmbcurrency.Enabled = false;
                grdbutton.Visible = false;
               

                

            }
            if (txttrn_type.Text == "22")
            {
                dgv1.Columns["orderno"].Visible = false;
            }
           
            
        }
        private void find_total()
        {
            try
            {
                double price;
                double tot;
                price = 0;
                tot = 0;
                dgv1.EndEdit();
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    if (dgv1["price", i].Value == null) dgv1["price", i].Value = 0;
                    if (!Convert.IsDBNull(dgv1["barcode", i].Value)) //dgv1["Item_Code", i].Value = 0;
                    {
                        if (dgv1["barcode", i].Value != null)
                        {

                            if (dgv1["vat%", i].Value == "") dgv1["vat%", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["price", i].Value) || dgv1["price", i].Value == "" || dgv1["price", i].Value == null) dgv1["price", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["qty", i].Value) || dgv1["qty", i].Value == "" || dgv1["qty", i].Value == null) dgv1["qty", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["disc", i].Value) || dgv1["disc", i].Value == "" || dgv1["disc", i].Value == null) dgv1["disc", i].Value = 0;

                            price = Convert.ToDouble(dgv1["price", i].Value) * Convert.ToDouble(dgv1["qty", i].Value) - Convert.ToDouble(dgv1["disc", i].Value);
                            dgv1["total", i].Value = Math.Round(price, 2);
                            dgv1["vat", i].Value = Math.Round(Convert.ToDouble(dgv1["total", i].Value) * Convert.ToDouble(dgv1["vat%", i].Value) / 100,2);
                            tot = tot + price;
                        }
                    }
                }
                isdirty = true;
                txttotal.Text = Math.Round(tot, 2).ToString();
                cmbcurrency.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            print_reciept();
        }

        private void print_reciept()
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


                if (string.IsNullOrEmpty(txtinvno.Text)) return;

                ReportDocument CrRep = new ReportDocument();

                if (Convert.ToInt32(txttrn_type.Text)==22)
                rep_path = Application.StartupPath + "\\reports\\porder.rpt";
                else
                    rep_path = Application.StartupPath + "\\reports\\porder_Reciept.rpt";


                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{DATA_ENTRY.INVOICE_NO} = '" + txtinvno.Text.Trim() + "' and {DATA_ENTRY.TRN_TYPE} =" + Convert.ToInt32(txttrn_type.Text);


                if (crt != "") CrRep.RecordSelectionFormula = crt;

                //CrRep.VerifyDatabase = false;
                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
                //crconnectioninfo.ServerName = "SqlZoomERP";
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
                if (Convert.ToInt32(txttrn_type.Text) == 22)
                {
                    CrRep.DataDefinition.FormulaFields["shipping"].Text = "'" + cmbshipterm.Text + "'";
                    CrRep.DataDefinition.FormulaFields["TermPayment"].Text = "'" + cmbterm.Text + "'";
                    CrRep.DataDefinition.FormulaFields["modedelivery"].Text = "'" + cmbmode.Text + "'";
                    CrRep.DataDefinition.FormulaFields["shipaddress"].Text = "'" + cmbaddress.Text + "'";
                }
                // CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);
                CrRep.ReadRecords();
                //CrRep.Load();
                // CrRep.ReadRecords();

                CrRep.Refresh();

                //if (chkprinter.Checked)
                //{


                //    CrRep.PrintToPrinter(1, true, 0, 0);
                //}
                //else
                //{

                if (chkprintview.Checked)
                {
                    FrmrepView frm = new FrmrepView();
                    frm.MdiParent = this.ParentForm;

                    frm.crv1.ReportSource = CrRep;
                    frm.Show();
                    return;
                }



                CrRep.PrintToPrinter(1, true, 0, 0);
                return;
                //}


            }
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtcustomer_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Conn.Close();
                Conn.Open();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();

                sql = "SELECT CUS_AC_TYPE,SUP_AC_TYPE FROM AC_OPTIONS WHERE  ac_options.ID =1";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                // 
                object sl = 0;
                object pr = 0;
                if (rec.RecordCount > 0)
                {
                    sl = rec.Fields[0].Value;
                    pr = rec.Fields[1].Value;
                }

                sql = "select  cast(Acc_No as Varchar) as Code ,Acc_Name as Name,Acc_aName as Ar_Name from Accounts where acc_type_code=3 ";
                switch (txttrn_type.Text)
                {
                    case "2":
                    case "22":
                        {
                            rec = new ADODB.Recordset();

                            sql = "select  cast(Acc_No as Varchar) as Code ,Acc_Name as Name,Acc_aName as Ar_Name from Accounts where  acc_type_code=3" ;
                            break;
                        }
                   
                }







                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Customer");
                adalkp.Fill(dtlkp);

               // var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                grdcuslookup.Left = txtcustomer.Left;
                grdcuslookup.Top = txtcustomer.Top + txtcustomer.Height;
                acntrl = "txtcus";
                dv.Table = dtlkp;
                grdcuslookup.DataSource = dv;
                grdcuslookup.Columns[1].Width = 300;
                grdcuslookup.Visible = true;
                txtcustomer.Focus();


                ADOconn.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtcustomer_TextChanged(object sender, EventArgs e)
        {
            if (grdcuslookup.Visible == true)
            {
                //dgv1.EndEdit();


                //dgv1.BeginEdit(false);
                string txt = txtcustomer.Text.Trim();
                if (txt != "")
                {
                    dv.RowFilter = "Code LIKE  '%" + txt + "%' OR Name LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Code <> '0'";


            }
        }


        private void find_customer()
        {
            try
            {
                Conn.Close();
                Conn.Open();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();

                if (txtcustomer.Text == "") txtcustomer.Text = "";
                if (string.IsNullOrEmpty( txtcustomer.Text.Trim()))
                {
                    //txtcustomer.Focus();
                    return;
                }

                sql = "select  Acc_No  ,Acc_Name ,Acc_aName,DEF_CURRENCY from Accounts where  acc_no=" + Convert.ToDouble(txtcustomer.Text.ToString());

                txtcusname.Text = "";
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if (rec.RecordCount != 0)
                {
                    txtcustomer.TextChanged -= txtcustomer_TextChanged;
                    txtcustomer.Text = rec.Fields[0].Value.ToString();
                    txtcusname.Text = rec.Fields[1].Value.ToString();
                   
                    cmbcurrency.SelectedValue = rec.Fields[3].Value.ToString();
                    txtcustomer.TextChanged += txtcustomer_TextChanged;
                   if(txttrn_type.Text=="2")
                    txtcustomer.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid Supplier");
                    return;
                }
                //dgv1.Focus();

                // 
            }
            catch (Exception ex)
            {
                txtcustomer.TextChanged -= txtcustomer_TextChanged;
                txtcustomer.Text = "";
                txtcusname.Text = "";
                txtcustomer.TextChanged += txtcustomer_TextChanged;

            }
        }
        private void txtcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {




                switch (e.KeyCode)
                {
                    case Keys.Enter:


                        if (grdcuslookup.Visible)
                        {
                            if (grdcuslookup.Rows.Count < 1) return;


                            int lkprow = 0;


                            lkprow = grdcuslookup.CurrentCell.RowIndex;
                            int rw = grdcuslookup.CurrentCell.RowIndex;

                            txtcustomer.TextChanged -= txtcustomer_TextChanged;
                            txtcustomer.Text = grdcuslookup.Rows[rw].Cells[0].Value.ToString();
                            txtcusname.Text = grdcuslookup.Rows[rw].Cells[1].Value.ToString();
                            txtcustomer.TextChanged += txtcustomer_TextChanged;
                            find_customer();

                            grdcuslookup.Visible = false;
                            find_credit();
                            dgv1.Focus();

                            return;
                            //e.Handled = true;
                            //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        }
                        else
                        {
                            find_customer();
                        }
                        break;

                    case Keys.Control:
                        txtcustomer_DoubleClick(sender, null);
                        e.Handled = true;
                        break;
                    case Keys.ControlKey:
                        txtcustomer_DoubleClick(sender, null);
                        e.Handled = true;
                        break;

                    case Keys.Escape:
                        break;

                    default:
                        if (!grdcuslookup.Visible)
                            txtcustomer_DoubleClick(sender, null);

                        break;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            nyear.Text = dt1.Value.Year.ToString();
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgv1_SelectionChanged(object sender, EventArgs e)
        {

           
            try
            {
                if( cur_row>0)
                {
                if (dgv1["barcode", cur_row-1].Value == null)
                {
                    dgv1.CurrentCell = dgv1["barcode", cur_row-1];
                    return;
                }
            }
                if (dgv1.Rows.Count - 1 <= cur_row && dgv1.CurrentCell == dgv1["barcode",cur_row])
                {
                   // dgv1.Rows.Add();
                    //dgv1.EndEdit();
                  // dgv1.Rowsd(dgv1.Rows.Count-1 , 1);
                    
                }

                if (dgv1.CurrentCell.ColumnIndex > dgv1["remarks",cur_row].ColumnIndex )
                {
                    dgv1.CurrentCell = dgv1["remarks", cur_row];
                    return;
                }

                if (last_col == "barcode" && cur_col == "Description" && dgv1["barcode", cur_row].Value != null && !nodata && !dgv1["barcode", cur_row].Value.Equals("999"))
                {
                    object nextline = rec_options.Rows[0]["Auto_Next_line"];
                    decimal price = 0;
                    if (!string.IsNullOrEmpty(dgv1["price", cur_row].Value.ToString()))
                        price = Convert.ToDecimal(dgv1["price", cur_row].Value.ToString());

                    if (nextline.Equals("1") && price > 0)
                    {
                        if (dgv1["qty", cur_row].Value == null)
                        dgv1["qty", cur_row].Value = 1;
                        find_total();
                       

                        dgv1.CurrentCell = dgv1["barcode", cur_row+1];
                        

                    }
                    else
                    {
                        if(dgv1["barcode", cur_row].Value.ToString()!="999")

                        dgv1.CurrentCell = dgv1["unit", cur_row];
                        return;
                    }
                }
                else
                    if (last_col == "barcode" && cur_col != "barcode" && dgv1["barcode", cur_row].Value != null && !nodata)
                    {

                        dgv1.CurrentCell = dgv1["unit", cur_row];
                    }

                if (dgv1.CurrentCell == dgv1["unit", cur_row] && dgv1.CurrentCell.RowIndex > 0 && dgv1["barcode", cur_row].Value == null)
                {
                    cmbterm.Focus();
                    return;
                }

                if (nodata)
                {
                    nodata = false;
                    if (dgv1.Columns["orderno"].Visible)
                    {
                       
                         dgv1.CurrentCell = dgv1["orderno", cur_row];
                         dgv1["barcode", cur_row].Selected = false;
                    dgv1["orderno", cur_row].Selected = true;
                    dgv1.BeginEdit(true);
                    }
                    else
                        {
                    dgv1.CurrentCell = dgv1["barcode", cur_row];
                    dgv1["barcode", cur_row].Selected = true;
                        dgv1.BeginEdit(true);
                        }

                }

                //if (dgv1.Columns["orderno"].Visible && dgv1["barcode", cur_row].Value == "")
                //    dgv1.CurrentCell = dgv1["orderno", cur_row];
                //else

                //if (dgv1["barcode", cur_row].Value=="")  dgv1.CurrentCell = dgv1["barcode", cur_row];

                switch (last_col)
                {
                    case "Description":
                        if (dgv1["barcode", cur_row].Value.ToString() == "")
                        {
                            dgv1.CurrentCell = dgv1["barcode", cur_row];
                        }
                        else
                        dgv1.CurrentCell = dgv1["unit", cur_row];
                        break;
                }
            }
            catch(Exception ex)
            {
                if (nodata)
                {
                    nodata = false;
                    dgv1.CurrentCell = dgv1["barcode", cur_row];
                }
            }




        }

        private void dgv1_MouseClick(object sender, MouseEventArgs e)
        {
            // last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
            // dgv1.CurrentCell = dgv1[last_col,dgv1.CurrentCell.RowIndex];
        }

        private void calc_total()
        {
            try
            {
                if (txttotal.Text == null || txttotal.Text == "") txttotal.Text = "0";
                if (txtdscamt.Text == null || txtdscamt.Text == "") txtdscamt.Text = "0";
                txtnetamt.Text = Math.Round(Convert.ToDecimal(txttotal.Text.ToString()) - Convert.ToDecimal(txtdscamt.Text.ToString()), 2).ToString();
            }
            catch (Exception ex)
            {

            }

        }

      

        private void txtdscamt_TextChanged(object sender, EventArgs e)
        {
            calc_total();

        }

        private void txttotal_TextChanged(object sender, EventArgs e)
        {
            calc_total();
        }

        private void dgvpaid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
     
     

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnsave.Enabled && !txtnetamt.Text.Equals("0"))
                {
                    DialogResult result = MessageBox.Show("This Changes Not be Saved, Do you want clear the screen ?", "Record not Saved", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {

                        return;
                    }
                }

                load_ini();
                //dgv1.CurrentCell = dgv1["barcode", 0];
                //dgv1.Focus();
                btnsave.Enabled = true;
                lblmsg.Text = "New Entry......";
                lblmsg.BackColor = Color.LightGray;
                
                dgv1.Columns["barcode"].ReadOnly = false;
                dgv1.Columns["unit"].ReadOnly = false;
                dgv1.Columns["qty"].ReadOnly = false;
                dgv1.Columns["Price"].ReadOnly = false;
                dgv1.Columns["disc"].ReadOnly = false;
                lblinvstatus.Text = "***";
                btnsave.Enabled = true;
                btnPrint.Enabled = true;
                isedit = false;
                get_invno();
                if (txttrn_type.Text == "2")
                    txtorderno.Focus();
                else
                    txtcustomer.Focus();




            }
            catch
            {

            }
           
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            search_mrn();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            print_reciept();
        }

        private void tooloption()
        {
            try
            {


                ColorDialog colorDlg = new ColorDialog();
                colorDlg.AllowFullOpen = false;
                colorDlg.AnyColor = true;
                colorDlg.SolidColorOnly = false;
                colorDlg.Color = Color.Red;

                if (colorDlg.ShowDialog() == DialogResult.OK)
                {

                    foreach (Control c in this.Controls)
                    {
                        try
                        {
                            string a = c.Name;

                            if (c.BackColor.ToString() == Gvar._defaultcolor)
                            {
                                c.BackColor = colorDlg.Color;
                                // if (c.GetType() == typeof(TextBox)) {
                                //c.BackgroundColor = colorDlg.Color;
                                c.Refresh();
                            }

                        }
                        catch
                        {

                        }


                    }

                    this.BackColor = colorDlg.Color;
                    dgv1.BackgroundColor = colorDlg.Color;
                   
                    Gvar._defaultcolor = colorDlg.Color.ToString();


                }


            }
            catch
            {

            }
        }

        private void btnoption_Click(object sender, EventArgs e)
        {
            tooloption();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tooldelete_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Program.session_valid(dt1.Value.Date.ToString("yyyy-MM-dd")))
                {
                    MessageBox.Show("There is no valid Finance Session Found, Please check the Entry Date or Contact Admin  ", "Invalid Transaction Date ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }


                if (cmbcurrency.Enabled && isdirty)
                {
                    MessageBox.Show("Please Re-Vaidate The Items ", "Currency Changes not effected ");
                    return;
                }
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
                 

                if (txtcustomer.Text == "" || txtcusname.Text=="")
                {
                    MessageBox.Show("Invalid Supplier Code ", "Invalid Customer Code  ");
                    return;
                }

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

                //dt1.Value =Convert.ToDateTime( "20/09/2016 00:10:10");
                //DateTime updated = dt1.Value.Add(new TimeSpan(0, 0, -1));
                TimeSpan dt2 = dt1.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1));
                if (DateTime.Now.TimeOfDay.Hours < 5 && DateTime.Now.Date == dt1.Value.Date)
                {
                    dt1.Value = dt1.Value.Subtract(dt2);
                }


                if (txttrn_type.Text == "1" || txttrn_type.Text == "2" ) 
                {
                    if(txtrefno.Text=="")
                    {
                         MessageBox.Show("Invalid Supplier Invoice Number ", " Invoice Number Invalid ");
                         txtrefno.Focus();
                        return;

                    }
                }
              
                Application.DoEvents();
               


                find_total();
                Gvar.nyear = dt1.Value.Year;

                bool itemfound = false;
                iserror = true;
                for (int i = 0; i < dgv1.RowCount ; i++)
                {
                    // i=i+1;
                    if (dgv1["barcode", i].Value == null) break;
                    if (dgv1["barcode", i].Value.ToString() == "999")
                    {
                        dgv1["ItemCode", i].Value = "999";
                        dgv1["fraction", i].Value = "1";
                        dgv1["Hfraction", i].Value = "1";
                        dgv1["itemid", i].Value = "999";
                        dgv1["cost", i].Value = dgv1["price", i].Value;


                    }
                    string celval = Convert.ToString(dgv1["ItemCode", i].Value);

                    if (celval.Trim() != "")
                    {
                        itemfound = true;


                        if (dgv1["Description", i].Value == null || dgv1["Qty", i].Value == null || dgv1["price", i].Value.ToString()=="0")
                        {
                            MessageBox.Show("Invalid Entry on Row " + ++i);
                            lblmsg.Text = "Invalid Entry on Row " + ++i;
                            iserror = true;
                            return;
                        }

                        if (dgv1["Description", i].Value.ToString() == "" || dgv1["Qty", i].Value.ToString() == "")
                        {
                            lblmsg.Text = "Invalid Entry on Row " + ++i;
                            iserror = true;
                            MessageBox.Show("Invalid Entry on Row " + ++i);
                            return;
                        }

                    }

                }

                if (itemfound == false)
                {
                    MessageBox.Show("No Item found to Save!", "Invalid Entry");
                    lblmsg.Text = "No Item found to Save!";
                    iserror = true;
                    return;
                }





                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();



                ADODB.Recordset cus = new ADODB.Recordset();

                sql = "select Inv_no from trn_master where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + txtinvno.Text.Trim() + "'  AND NYEAR=" + nyear.Text + " and BRN_CODE =" + Gvar.brn_code;


                cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (isedit)
                {
                    if (cus.RecordCount == 0)
                    {
                        DialogResult result = MessageBox.Show("This Invoice Number not found for Update, Do You want Add It Now?", "Invalid Record", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            iserror = true;

                            //ADOconn.RollbackTrans();
                            return;
                        }
                    }
                }
                else
                {
                    if (cus.RecordCount > 0)
                    {
                        MessageBox.Show("This Invoice Number Already Exist", "Invalid Record");
                        //ADOconn.RollbackTrans();
                        return;
                    }

                }

                if (Gvar.orgdup == null) Gvar.orgdup = "ORG";

                iserror = false;
                ADOconn.BeginTrans();
                lblmsg.Text = "Please Wait Saving......";
                lblmsg.BackColor = Color.LightGray;
                if (txttrn_type.Text == "2")
                {
                    SAVE_DATAENTRY_PUR();
                   
                    if (!iserror)
                    {
                        save_data();
                    }
                    if (!iserror)
                    {
                        updat_accounts();
                    }

                    
                }
                else
                {
                    SAVE_DATAENTRY();
                }
                

                    if (iserror)
                    {
                        ADOconn.RollbackTrans();
                        if(lblmsg.Text.IndexOf("Please Wait")==0)
                        lblmsg.Text = "Error!!!! : Record Not Saved,, Please check and Try again !!";
                        lblmsg.BackColor = Color.Red;
                        btnsave.Enabled = true;
                        editno();
                        return;
                        
                    }
                    isdirty = false;
                    ADOconn.CommitTrans();
                
                    lblmsg.Text = "Record Saved Successfully!!!";
                    lblmsg.BackColor = Color.LightGray;
                    btnsave.Enabled = false;
                   

                    dgv1.Columns["barcode"].ReadOnly = true;
                    dgv1.Columns["unit"].ReadOnly = true;
                    dgv1.Columns["qty"].ReadOnly = true;
                    dgv1.Columns["Price"].ReadOnly = true;
                    dgv1.Columns["disc"].ReadOnly = true;
                    isedit = true;



               
                
                editno();
                string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pos.txt");
                System.IO.File.Delete(myTempFile);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                lblmsg.Text = ex.Message;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            if (isdirty)
            {
                DialogResult result = MessageBox.Show("The Changes may Not Saved , Do  You want to Continue?", "Not Saved", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    lblmsg.Text = "The Changes may Not Saved.....";
                    btnsave.Focus();

                    return;
                }
            }
            this.Dispose();
            this.Close();
        }

        private void editok()
        {

            try
            {
                //  ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();

                ADODB.Recordset rec = new ADODB.Recordset();

                sql = "UPDATE ISEDIT SET ISEDIT='1'";


                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                // ADOconn.Close();
            }
            catch (Exception ex)
            {
                ADOconn.Close();
            }

        }
        private void editno()
        {

            try
            {
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();

                ADODB.Recordset rec = new ADODB.Recordset();

                sql = "UPDATE ISEDIT SET ISEDIT='0'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                ADOconn.Close();
            }
            catch (Exception ex)
            {
                if (ADOconn.State==0)
                ADOconn.Close();
            }

        }

       

        private void txtinvno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Enter:


                        if (GrdLookup.Visible)
                        {
                            if (GrdLookup.Rows.Count < 1) return;


                            int lkprow = 0;


                            lkprow = GrdLookup.CurrentCell.RowIndex;
                            int rw = GrdLookup.CurrentCell.RowIndex;

                            txtinvno.TextChanged -= TxtmrnNo_TextChanged;
                            txtinvno.Text = GrdLookup.Rows[rw].Cells[0].Value.ToString();

                            txtinvno.TextChanged += TxtmrnNo_TextChanged;


                            GrdLookup.Visible = false;
                            //dgv1.Focus();
                            break;
                            return;
                            //e.Handled = true;
                            //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        }
                        break;
                    case Keys.Control:
                        Txtinvno_DoubleClick(sender, null);
                        e.Handled = true;
                        break;
                    case Keys.ControlKey:
                        Txtinvno_DoubleClick(sender, null);
                        e.Handled = true;
                        break;


                }
            }

            catch (Exception ex)
            {


            }
        }

        private void GrdLookup_VisibleChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void add_newitem(int i)
        {

            string sql;
            
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try
            {
                try
                {
                    iserror = false;

                    string SQL="";

                    SQL = "iNSERT INTO [HD_ITEMMASTER]([ITEM_CODE],[DESCRIPTION],[USER],[ITM_CAT_CODE],[UNIT],[FRACTION],[BRN_CODE],[SUB_CAT_CODE],[AR_DESC],[UPD_FLAG],[BARCODE])";
                    SQL = SQL + "  Values ('" + dgv1["barcode",i].Value + "','" + dgv1["description",i].Value + "','" + Gvar.Userid + "','99','" + dgv1["unit",i].Value + "','1'," + Gvar.brn_code + ",'1','" + dgv1["description",i].Value + "','N','" + dgv1["barcode",i].Value + "')";
                    tmp = new Recordset();
                    tmp.Open(SQL, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    SQL = "INSERT INTO [BARCODE] ([BARCODE],[UNIT],[FRACTION],[SALE_PRICE],[RETAIL_PRICE],[ITEM_CODE],[BRN_CODE],[ITEM_ID],[MAIN_ID],[DESCRIPTION],[DESCRIPTION_AR])";
                   // SQL = SQL & "  Values ('" & txtFbarcode & "','" & cmbFunit.Text & "','" & Val(txtFfraction) & "' ,'" & Val(txtFsale) & "','" & Val(txtFsale) & "','" & TxtFItemCode & "', '" & BRN_CODE & "','" & Trim(TxtFItemCode) & cmbFunit.ItemData(cmbFunit.ListIndex) & "','1','" & txtFItemName & "', '" & txtFaname & "')"
                    SQL = SQL + "  Values ('" + dgv1["barcode",i].Value + "','" + dgv1["unit",i].Value + "','1','" + dgv1["price",i].Value + "','" + dgv1["price",i].Value + "','" + dgv1["barcode",i].Value + "','"   + Gvar.brn_code + "','" + dgv1["itemid",i].Value + "', '1','" + dgv1["description",i].Value + "','" + dgv1["description",i].Value + "')";
                    tmp = new Recordset();
                    tmp.Open(SQL, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    SQL = "INSERT INTO [STOCK_MASTER]([ITEM_CODE],[LAST_PUR_PRICE],[SALE_PRICE],[USER],[LAST_PUR_PRICE])";
                //SQL = SQL & "  Values ('" & TxtFItemCode & "','" & Val(txtFcost) & "','" & Val(txtFsale) & "' ,'ADMIN')";
                    SQL = SQL + "  Values ('" + dgv1["barcode", i].Value + "','" + dgv1["price", i].Value + "','" + dgv1["price", i].Value + "','Admin','" + dgv1["price", i].Value + "')";
                    tmp = new Recordset();
                    tmp.Open(SQL, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



            SQL = "INSERT INTO [WR_STOCK_MASTER]([ITEM_CODE],[USER],WR_CODE)";
            //SQL = SQL & "  Values ('" & TxtFItemCode & "','ADMIN'," & WR_CODE & ")"
                SQL = SQL + "  Values ('" + dgv1["barcode",i].Value + "','Admin','" + Gvar.wr_code +"')";
                    tmp = new Recordset();
                    tmp.Open(SQL, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                    iserror = false;





                    //MessageBox.Show("Service Issue Entry Saved Successfully!!!", "Succeed Entry");
                }

                catch (SqlException ex)
                {
                    ////ADOconn.RollbackTrans();
                    //lblmsg.Text = ex.Message;
                    //iserror = true;
                    //MessageBox.Show(ex.Message);
                }
            }
            catch (Exception sq)
            {

                ////ADOconn.RollbackTrans();
                //lblmsg.Text = sq.Message;
                //iserror = true;
                //MessageBox.Show(sq.Message);
            }


        }

        private void cmbtrntype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttrn_type.Text = cmbtrntype.SelectedValue.ToString();
        }

        private void txtcustomer_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!grdcuslookup.Visible)
                {
                    find_customer();
                    
                }
            }
            catch
            { }
        }

        private void find_credit()
        {
            try
            {
                 if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
               
                string sql;
               // if (txtcusname.Text == "") return;
               // sql = "select cr_amount,dr_amount from trn_acc_sum where acc_no =" + Convert.ToDecimal(txtcustomer.Text);
               // decimal dr = 0;
               // decimal cr = 0;

               //ADODB.Recordset tmp = new Recordset();
               // tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               //                 if (!tmp.EOF )
               //                 {
               //                     if (tmp.Fields[0].Value != DBNull.Value) cr = Convert.ToDecimal(tmp.Fields[0].Value);
               //                         if (tmp.Fields[1].Value != DBNull.Value) dr = Convert.ToDecimal( tmp.Fields[1].Value);
                                                
               //                 }

               //                 txtacbalance.Text = ( dr-cr).ToString();


            }
            catch(Exception EX)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
               

                    if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                
                          

                        object TRNBY;
                        object DRCR;
                        object DRCR1;
                        object NARR;
                        object LACC;
                        object PAYBY;
                        object[] ledgerini;
                        string acc_acs;
//string TMP As New ADODB.Recordset
//Set TMP = New ADODB.Recordset
//TMP.Open "SELECT * FROM FINANCE_SESSION WHERE FS_STATUS='OPEN' AND convert(varchar, '" & Format(DTPicker1.Value, "yyyyMMdd") & "',112)  BETWEEN convert(varchar, FS_START_DATE,112) AND convert(varchar, FS_END_DATE,112)", Sqlcon, 3, 3
//If TMP.RecordCount = 0 Then
//MsgBox "There is no Open Finance Session found for This Date " & DTPicker1.Value, vbCritical
//Exit Sub
//End If
//If ISPRINTED = True And Mid(priv, 5, 1) <> "X" And ORGDUP = "ORG" Then

//MsgBox "Insufficient Previlage", vbCritical
//Exit Sub
//End If


DialogResult result = MessageBox.Show("Do you Want to Delete This Invoice?", "Delete Invoice", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes) return;

                    if (ADOconn.State == 1) ADOconn.Close();
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                    Conn.Close();
                    Conn.Open();

                    ADOconn.BeginTrans(); 


    sql = "SELECT ACC_NO FROM TRN_TYPE WHERE TRN_CODE =" + Convert.ToDecimal(txttrn_type.Text);
                

               ADODB.Recordset tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                  double lacc =0;     
                if (tmp.RecordCount>0)
                     {
                          lacc =Convert.ToDouble( tmp.Fields[0].Value);
                     }


   
                        acc_acs = Program.ledger_ini(Convert.ToInt16(txttrn_type.Text), txtinvno.Text );
                        ledgerini = acc_acs.Split('`');
                        TRNBY = Convert.ToInt16((ledgerini[0]));
                        DRCR = ledgerini[1];
                        DRCR1 = ledgerini[2];
                        NARR = ledgerini[3];
                        LACC =   Convert.ToInt64((ledgerini[4]));
                        PAYBY = Convert.ToInt16(ledgerini[5]);

                
ADODB.Recordset rec = new   ADODB.Recordset();
                sql = "select rec_no,tran_no from data_entry  where trn_type =" + Convert.ToInt16(txttrn_type.Text) + "  AND NYEAR=" + Convert.ToInt16(dt1.Value.Year) + " and invoice_no ='" + txtinvno.Text.Trim()  + "' and org_dup='" +  Gvar.orgdup + "'  and BRN_CODE =" +  Gvar.brn_code;
                double rec_no;
                double TxtTrn=0;

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                 if (rec.RecordCount>0)
                     {
                          rec_no =Convert.ToDouble( rec.Fields[0].Value.ToString());
                          TxtTrn = Convert.ToDouble( rec.Fields[1].Value.ToString());
                     }
     
                        object a;
                        sql = "update  data_entry set flag='D' where  trn_type =" + Convert.ToInt16(txttrn_type.Text) + "  AND NYEAR=" + Convert.ToInt16(dt1.Value.Year) + " and invoice_no ='" + txtinvno.Text.Trim()  + "' and org_dup='" +  Gvar.orgdup  + "'  and BRN_CODE =" +  Gvar.brn_code;
                       ADOconn.Execute (sql,out a,-1);

                       sql = "DELETE FROM TMP_ITM_DETAIL WHERE trn_type  =" + Convert.ToInt16(txttrn_type.Text) + "  AND TRN_NO=" + TxtTrn;
                       ADOconn.Execute (sql,out a,-1); 

                         sql = "INSERT INTO TMP_ITM_DETAIL(ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code) SELECT ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code FROM TRN_ITM_DETAIL WHERE  trn_type  =" + Convert.ToInt16(txttrn_type.Text) + "  AND TRN_NO=" + TxtTrn;
                       ADOconn.Execute (sql,out a,-1);
                       rec = new ADODB.Recordset();
                                sql = "select *  FROM TMP_ITM_DETAIL WHERE trn_type  =" + Convert.ToInt16(txttrn_type.Text) + "  AND TRN_NO=" +TxtTrn;
                                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                
                        sql = "DELETE FROM TRN_ITM_DETAIL1  WHERE  trn_type  =" + Convert.ToInt16(txttrn_type.Text) + "  AND TRN_NO=" +TxtTrn;
                       ADOconn.Execute (sql,out a,-1);

                       while (!rec.EOF)
                       {
                           if (rec.Fields["item_code"].Value.ToString() != "" && rec.Fields["item_code"].Value.ToString() != "999")
                           {
                               sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE = " + Gvar.wr_code + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);

                               int ST = 0;
                               if (tmp.RecordCount > 0)
                                   ST = Convert.ToInt16(tmp.Fields[0].Value.ToString());



                               sql = "SELECT * FROM WR_STOCK_MASTER WHERE   WR_CODE = " + Gvar.wr_code + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockPessimistic,-1);
                               if (tmp.RecordCount == 0)
                               {
                                   tmp.AddNew();
                               }


                               tmp.Fields["WR_CODE"].Value = rec.Fields["wr_code"].Value;
                               tmp.Fields["BRN_CODE"].Value = Gvar.brn_code;
                               tmp.Fields["ITEM_CODE"].Value = rec.Fields["ITEM_CODE"].Value;
                               tmp.Fields["User"].Value = Gvar.Userid;
                               tmp.Fields["stock"].Value = ST;
                               tmp.Update();


                               sql = "SELECT sum(STOCK) FROM  WR_STOCK_MASTER WHERE  ITEM_CODE='" + rec.Fields["item_code"].Value + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);
                               ST = 0;
                               if (tmp.RecordCount > 0)
                                   ST = Convert.ToInt16(tmp.Fields[0].Value.ToString());

                              // sql = "Update STOCK_MASTER set stock = '" + ST + "'   WHERE    ITEM_CODE='" + rec.Fields["item_code"].Value + "'";
                                sql = "select * from  STOCK_MASTER   WHERE    ITEM_CODE='" + rec.Fields["item_code"].Value + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockPessimistic, -1);
                               if (tmp.RecordCount != 0)
                               {
                                   tmp.Fields["stock"].Value=ST;
                               }
                               
                               
                               //ADOconn.Execute(sql, out a, -1);


                               rec.MoveNext();
                           }
                       }


                           sql = "delete from trn_master where  trn_type =" + Convert.ToInt16(txttrn_type.Text) + "  AND NYEAR=" + Convert.ToInt16(dt1.Value.Year) + " and inv_no ='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                           ADOconn.Execute(sql, out a, -1);


                           sql = "DELETE FROM tran_acc WHERE trn_by ='" + TRNBY + "' AND year(cur_date)=" + Convert.ToInt16(dt1.Value.Year) + " AND DOC_NO ='" + txtinvno.Text.Trim() + "' and BRN_CODE =" + Gvar.brn_code;
                           ADOconn.Execute(sql, out a, -1);





                           sql = @"INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],
                               [F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) 
                            SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],
                                   [PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]
                           FROM TRN_ACCOUNTS WHERE TRN_by='" + TRNBY + "' AND NYEAR=" + Convert.ToInt16(dt1.Value.Year) + " AND DOC_NO ='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                           ADOconn.Execute(sql, out a, -1);

                           sql = "DELETE FROM TRN_ACCOUNTS WHERE TRN_by='" + TRNBY + "'  AND NYEAR=" + Convert.ToInt16(dt1.Value.Year) + " AND DOC_NO ='" + txtinvno.Text.Trim() + "' and BRN_CODE =" + Gvar.brn_code;
                           ADOconn.Execute(sql, out a, -1);
                           sql = "DELETE FROM TRaN_ACC  WHERE TRN_by='" + TRNBY + "' AND year(cur_date)=" + Convert.ToInt16(dt1.Value.Year) + " AND DOC_NO ='" + txtinvno.Text.Trim() + "' and BRN_CODE =" + Gvar.brn_code;
                           ADOconn.Execute(sql, out a, -1);


                           

                           sql = "DELETE FROM TMP_ITM_DETAIL WHERE trn_type =" + Convert.ToInt16(txttrn_type.Text) + "   and TRN_NO=" + TxtTrn;
                           ADOconn.Execute(sql, out a, -1);
                           btnsave.Enabled = false;
                           btndelete.Enabled = false;
                           lblinvstatus.Text = "Invoice Deleted!!!";
                           ADOconn.CommitTrans();
                           lblmsg.Text = "Invoice Deleted!!!";
                           lblmsg.BackColor = Color.Red;
                           MessageBox.Show("Deleted Invoice Successfully");

                       


            }
            catch(Exception ex)
            {
                
                ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }
        }

        private void nyear_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (nyear.Text.Length == 4)
                {

                    if (isedit==true)
                    {
                        btnsave.Enabled = false;

                        MessageBox.Show("Year Cannot Change Year for a Saved Invoice!!!!!!", "Invalid Year");
                       // search_mrn();
                        isedit = false;
                        btnsave.Enabled = false;
                        btndelete.Enabled=false;
                      

                    }
                    DateTime yeardt = Convert.ToDateTime(nyear.Text + "-" + dt1.Value.Month + "-" + dt1.Value.Day);
                    dt1.Value = yeardt;
                }
            }
            catch
            {
            }

        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form childForm = new FrmBrand();
            //childForm.MdiParent = this;
            Gvar.Gind = 4;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Payment Types";

            childForm.ShowDialog();
            populate_options();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form childForm = new FrmBrand();
           
            Gvar.Gind = 5;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Payment Terms";

            childForm.ShowDialog();
            populate_options();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 6;
            Form childForm = new FrmBrand();
           // childForm.MdiParent = this;
           

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Shipment";

            childForm.ShowDialog();
            populate_options();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form childForm = new FrmBrand();
            //childForm.MdiParent = this;
            Gvar.Gind = 7;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Ports";

            childForm.ShowDialog();
            populate_options();
        }

        private void txtrefno_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void txtrefno_Validated(object sender, EventArgs e)
        {
           
        }

        private void SAVE_DATAENTRY_PUR()
        {
            

            dgv1.EndEdit();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            int i = 0;
            try
            {
                try
                {


            

                chk:
                    ADODB.Recordset rec = new ADODB.Recordset();

                    sql = "SELECT ISEDIT FROM ISEDIT";


                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    while (!rec.EOF)
                    {
                        for (i = 0; i < 1000; i++)
                        {
                            Application.DoEvents();
                            if (rec.Fields[0].Value.ToString() == "1")
                            {
                                goto chk;
                            }
                            else
                            {
                                editok();
                                break;
                            }
                        }

                        editno();

                        rec.MoveNext();
                    }
                    editok();





                    if (!isedit)
                    {
                        get_invno();
                        
                    }





                    //ADOconn.BeginTrans();
                    decimal paidcash = 0;
                    decimal paidother = 0;
                    decimal balance = 0;



                    if (Gvar.orgdup == null) Gvar.orgdup = "ORG";

                    rec = new ADODB.Recordset();
                    sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + nyear.Text + " and  TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text) + " AND ORG_DUP ='" + Gvar.orgdup + "' and BRN_CODE =" + Gvar.brn_code;

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {

                        rec.AddNew();
                        rec.Fields["flag"].Value = "N";
                        rec.Fields["user_name"].Value = Gvar.Userid;

                    }

                    rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                    rec.Fields["TRAN_NO"].Value = Convert.ToInt32(txttrn.Text);
                    rec.Fields["CURDATE"].Value = dt1.Value;
                    rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                    rec.Fields["ORG_DUP"].Value = Gvar.orgdup;
                    rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["ENTRY_TYPE"].Value = "Retail";
                    rec.Fields["CRATE"].Value = txtrate.Text;
                    rec.Fields["ACCODE"].Value = txtcustomer.Text;
                    rec.Fields["ename"].Value = txtcusname.Text;
                    //rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["REMARKS"].Value = txtremarks.Text;
                    rec.Fields["VAT_PERCENT"].Value = Convert.ToDecimal(txtvatpcnt.Text); ;
                    rec.Fields["VAT_AMOUNT"].Value = Convert.ToDecimal(txtvatamt.Text); ; ;
                    rec.Fields["Currency"].Value =cmbcurrency.SelectedValue;
                    rec.Fields["REF_NO"].Value = txtrefno.Text;
                    rec.Fields["ORDER_NO"].Value = txtorderno.Text;
                    rec.Fields["ORDER_RECNO"].Value = txtorderrecno.Text;
                    rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text) * Convert.ToDouble(txtrate.Text);
                    rec.Fields["DISC_AMT"].Value = Convert.ToDouble(txtdscamt.Text) * Convert.ToDouble(txtrate.Text); ; ;
                    rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text); ;
                    rec.Fields["cash_paid"].Value = paidcash * Convert.ToDecimal(txtrate.Text); ;
                    rec.Fields["other_paid"].Value = paidother * Convert.ToDecimal(txtrate.Text); ;
                    rec.Fields["FRN_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) ;
                    rec.Fields["sales_code"].Value = Gvar.Userid;
                    rec.Fields["INV_PREFEX"].Value = rec_options.Rows[0]["INV_PREFEX"]; ;
                    // rec.Fields["ename"].Value = cmbproject.Text;

                    //rec.Fields["order_no"].Value = 0;
                    rec.Fields["wr_code"].Value = Gvar.wr_code;
                    rec.Fields["NYEAR"].Value = nyear.Text;
                    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;

                    rec.Update();

                    sql = "SELECT rec_no FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND  NYEAR=" + nyear.Text + " AND INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    decimal rec_no = (decimal)rec.Fields["REC_NO"].Value;
                    var a = 0;
                    sql = "DELETE FROM DATA_ENTRY_GRID WHERE REC_NO=" + rec_no;
                    //ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)


                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    cmd.ExecuteNonQuery();


                    rec = new ADODB.Recordset();

                    sql = "SELECT * FROM DATA_ENTRY_GRID WHERE REC_NO=1.1";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    string orderno;

                    // foreach (DataGridViewRow row in this.dgv1.Rows)

                    for (i = 0; i < dgv1.RowCount; i++)
                    {
                        // i=i+1;
                        string celval = Convert.ToString(dgv1["ItemCode", i].Value);


                        //int = dgv1[barcode, i].Value.Equals(null);
                        // MessageBox.Show(celval.ToString());
                        // //string celval =  dgv1[barcode,i].Value.ToString();

                        if (dgv1["qty", i].Value == null) dgv1["qty", i].Value = "0";
                        if (celval.Trim() != "" && Convert.ToDecimal(dgv1["qty", i].Value) >0)
                        {

                            if (txtorderno.Text != "")
                                orderno = txtorderno.Text;
                            else
                                orderno = dgv1["orderno", i].Value.ToString();
                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                            rec.AddNew();
                            rec.Fields["REC_NO"].Value = rec_no;
                            rec.Fields["ROWNUM"].Value = i;
                            rec.Fields["Item_Code"].Value = dgv1["ItemCode", i].Value;
                            rec.Fields["PRICE"].Value = dgv1["price", i].Value;
                            rec.Fields["Description"].Value = dgv1["Description", i].Value;
                            rec.Fields["QTY"].Value = dgv1["qty", i].Value;
                            rec.Fields["price"].Value = Convert.ToDecimal( dgv1["price", i].Value)* Convert.ToDecimal(txtrate.Text);
                            rec.Fields["BARCODE"].Value = dgv1["barcode", i].Value;
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["UNIT"].Value = dgv1["unitid", i].Value;
                            if (dgv1["cost", i].Value == null || dgv1["cost", i].Value == "")
                                dgv1["cost", i].Value = 0;
                            rec.Fields["SALE_PUR_AMT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text);
                            rec.Fields["ITM_TOTAL"].Value = Convert.ToDecimal(dgv1["total", i].Value) * Convert.ToDecimal(txtrate.Text);
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                            if (dgv1["proposed", i].Value == null)
                            {
                                dgv1["proposed", i].Value = Convert.ToDecimal(dgv1["price", i].Value) * Convert.ToDecimal(txtrate.Text); 

                            }

                            rec.Fields["PROPOSE_PRICE"].Value = dgv1["proposed", i].Value;
                            object propose = rec.Fields["PROPOSE_PRICE"].Value;
                            if (propose == "0") rec.Fields["PROPOSE_PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) * Convert.ToDecimal(txtrate.Text);
                            rec.Fields["ITEM_ID"].Value = dgv1["itemid", i].Value;
                            rec.Fields["hfraction"].Value = dgv1["hfraction", i].Value;
                            rec.Fields["wr_code"].Value = Gvar.wr_code;
                            if (dgv1["disc", i].Value == null || dgv1["disc", i].Value == "")
                                dgv1["disc", i].Value = 0;
                            rec.Fields["disc"].Value = Convert.ToDecimal( dgv1["disc", i].Value) * Convert.ToDecimal(txtrate.Text); ;
                            rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                            rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                            //rec.Fields["REF_NO"].Value = txtinvno.Text;
                            rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                             rec.Fields["brn_code"].Value = Gvar.brn_code;

                            rec.Fields["UNIT_QTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["UNIT_PRICE"].Value = (Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value)) * Convert.ToDecimal(txtrate.Text); ;
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = ( Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text)) / Convert.ToDecimal(dgv1["FRACTION", i].Value);

                            }
                            else
                            {
                                rec.Fields["UNIT_PRICE"].Value = Convert.ToInt32(dgv1["price", i].Value) * Convert.ToDecimal(txtrate.Text); ;
                                rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text); ;

                            }
                             rec.Fields["order_no"].Value =   orderno;
                             rec.Fields["order_recno"].Value = txtorderrecno.Text;
                            

                             rec.Update();
                             object out1;
                             sql = "Update Pur_order_grid set rqty= (select sum(qty) from data_entry_grid inner join data_entry on data_entry.rec_no=data_entry_grid.rec_no  where item_code='" + dgv1["ItemCode", i].Value + "' and data_entry.trn_type = " + txttrn_type.Text + " and data_entry_grid.ORDER_recno='" + txtorderrecno.Text + "') where item_code='" + dgv1["ItemCode", i].Value + "' and rec_no='" + txtorderrecno.Text + "'";
                             ADOconn.Execute(sql, out out1, -1);

                            

                        }

                    }


                    iserror = false;

                }
                catch (SqlException ex)
                {
                    //ADOconn.RollbackTrans();
                    iserror = true;
                    lblmsg.Text = ex.Message;
                    editno();
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception er)
            {
                editno();
                iserror = true;
                lblmsg.Text = er.Message;
                MessageBox.Show(er.Message);
            }



        }
        private void save_data()
        {

            string sql;

            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try
            {
                try
                {
                    iserror = false;

                    
                    cus = new ADODB.Recordset();
                    //ADOconn.BeginTrans();

                    if (Convert.ToInt32(txttrn.Text) == 0)
                    {
                        sql = "SELECT trno FROM TRN_NO";
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        txttrn.Text = cus.Fields[0].Value.ToString();
                        sql = "SELECT top 1 * from trn_master1";
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        cus.AddNew();

                    }
                    else
                    {
                        sql = "select * from trn_master1 where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + txtinvno.Text.Trim() + "'  AND NYEAR=" + nyear.Text + " and BRN_CODE =" + Gvar.brn_code;

                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (cus.RecordCount == 0) cus.AddNew();

                    }
                    cus.Fields["trn_no"].Value = Convert.ToInt32(txttrn.Text);
                    cus.Fields["INV_NO"].Value = txtinvno.Text.Trim();
                    cus.Fields["REF_NO"].Value = txtrefno.Text.Trim();
                    cus.Fields["DATE_TIME"].Value = dt1.Value;
                    cus.Fields["cus_code"].Value = txtcustomer.Text;
                    cus.Fields["cus_name"].Value = txtcusname.Text;
                    cus.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                    cus.Fields["TOT_AMOUNT"].Value = Convert.ToDouble(txttotal.Text.Trim()) * Convert.ToDouble(txtrate.Text);
                    cus.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text.Trim()) * Convert.ToDouble(txtrate.Text);
                    cus.Fields["FNET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text.Trim()) * Convert.ToDouble(txtrate.Text);

                    cus.Fields["DISCOUNT"].Value = Convert.ToDouble(txtdscamt.Text.Trim()) * Convert.ToDouble(txtrate.Text); ; ;
                    cus.Fields["user_ID"].Value = Gvar._Userid;
                    cus.Fields["SALE_TYPE"].Value = 0; ;
                    cus.Fields["SALES_code"].Value = cmbsalesagent.SelectedValue;
                    cus.Fields["WR_CODE"].Value = Gvar.wr_code;
                    cus.Fields["brn_CODE"].Value = Gvar.brn_code;
                    cus.Fields["NYEAR"].Value = nyear.Text;
                    cus.Fields["REMARKS"].Value = txtremarks.Text.Trim();
                    cus.Fields["CURRENCY"].Value = cmbcurrency.SelectedValue;
                    // cus.Fields["sales_code"].Value = Gvar.Userid;







                    cus.Update();


                    //CRT_TABLE:

                    sql = "INSERT INTO EDT_TRN_MASTER ([TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE) SELECT [TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE FROM TRN_MASTER1 WHERE trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_NO='" + txtinvno.Text.Trim() + "'";

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();


                    sql = "INSERT INTO TMP_ITM_DETAIL(ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code) SELECT ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code FROM TRN_ITM_DETAIL WHERE TRN_NO=" + Convert.ToDouble(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                    sql = "DELETE FROM TRN_ITM_DETAIL1 WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();

                    ADODB.Recordset rec = new ADODB.Recordset();

                    sql = "select * from TRN_ITM_DETAIL1 where trn_no=0";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    int i = 0;

                    sal_pur_amt = 0;

                    // foreach (DataGridViewRow row in this.dgv1.Rows)

                    for (i = 0; i < dgv1.RowCount ; i++)
                    {
                        // i=i+1;
                        string celval = Convert.ToString(dgv1["barcode", i].Value);


                        //int = dgv1["Item_Code", i].Value.Equals(null);
                        // MessageBox.Show(celval.ToString());
                        // //string celval =  dgv1["Item_Code",i].Value.ToString();

                        // if (!Convert.IsDBNull(dgv1["Item_Code", i].Value) && !Convert.IsDBNull(dgv1["qty", i].Value))
                        if (celval.Trim() != "")
                        {


                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                            rec.AddNew();
                            rec.Fields["trn_no"].Value = txttrn.Text.Trim();
                            rec.Fields["Item_Code"].Value = dgv1["ItemCode", i].Value;
                            rec.Fields["barcode"].Value = dgv1["barcode", i].Value;
                            rec.Fields["UNIT_QTY"].Value = dgv1["qty", i].Value;
                            rec.Fields["UNIT_PRICE"].Value = dgv1["price", i].Value;

                            //rec.Fields["RQTY"].Value = 0;
                            //rec.Fields["BARCODE"].Value = dgv1["barcode", i].Value;
                            // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                            // rec.Fields["cost_center"].Value = cmbsite.SelectedValue;
                            rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                            rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                            rec.Fields["trn_no"].Value = Convert.ToInt32(txttrn.Text);
                            rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                           // rec.Fields["crate"].Value = txtrate.Text;
                            //rec.Fields["ORDER_NO"].Value = 0;

                            decimal QTY = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            rec.Fields["QTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["PRICE"].Value = (Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value)) * Convert.ToDecimal(txtrate.Text);
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value)/  Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["SALE_PUR_AMOUNT"].Value =( Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text)) / Convert.ToDecimal(dgv1["FRACTION", i].Value);

                            }
                            else
                            {
                                rec.Fields["PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) *  Convert.ToDecimal(txtrate.Text);
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value);
                                rec.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(txtrate.Text); 

                            }

                            decimal PRICE = Convert.ToDecimal(rec.Fields["PRICE"].Value) ;
                            rec.Fields["discount"].Value = Convert.ToDecimal(dgv1["disc", i].Value) * Convert.ToDecimal(txtrate.Text); ;
                            rec.Fields["WR_CODE"].Value = Gvar.wr_code;
                            rec.Fields["brn_code"].Value = Gvar._brn_code;
                            rec.Update();

                            sal_pur_amt = sal_pur_amt + Convert.ToDecimal(rec.Fields["SALE_PUR_AMOUNT"].Value) * Convert.ToDecimal(txtrate.Text) *Convert.ToDecimal(rec.Fields["QTY"].Value);


                                if (txttrn_type.Text == "1" || txttrn_type.Text == "2")
                                {

                                    if (Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) > 0 && rec_options.Rows[0]["upd_sal_price"].ToString() == "1")
                                    {

                                        sql = "update barcode set retail_price = " + Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) * Convert.ToDecimal(txtrate.Text) +"  WHERE barcode='" + dgv1["barcode", i].Value + "'";


                                        //cmd = new SqlCommand(sql, Conn);
                                        //cmd.ExecuteNonQuery();

                                        tmp = new Recordset();
                                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    }
                                }
                           

                            if ((Convert.ToInt32(txttrn_type.Text) == 1 || Convert.ToInt32(txttrn_type.Text) == 2) && rec_options.Rows[0]["UPD_PUR_PRICE"].ToString() == "1")
                            {
                                object a;

                                sql = "Update STOCK_MASTER set last_pur_price=" + PRICE + ",AVG_PUR_PRICE= round(((AVG_pur_price*stock)+(" + PRICE * QTY + "))/(CASE stock+" + QTY + " WHEN 0 THEN 1 ELSE stock+" + QTY + " END),2) where stock > 0 and Item_Code ='" + rec.Fields["Item_Code"].Value + "'";


                                ADOconn.Execute(sql, out a, -1);

                                if ((int)a < 1)
                                {
                                    sql = "Update STOCK_MASTER set last_pur_price=" + PRICE + ",AVG_pur_price=" + PRICE + " where  stock =0 and Item_Code ='" + rec.Fields["Item_Code"].Value + "'";
                                    ADOconn.Execute(sql, out a, -1);

                                }

                            }

                            double qty = 0;
                            //if (!Convert.IsDBNull(tmp.Fields[0].Value))
                            //{
                            //    sql = "UPDATE DATA_ENTRY_GRID SET RQTY = " + tmp.Fields[0].Value + " WHERE  rownum=" + dgv1["rownum", i].Value + " and Item_Code='" + dgv1["Item_Code", i].Value + "' AND invoice_NO='" + Txtreciept.Text.Trim() + "' And trn_type =2";
                            //    tmp = new Recordset();
                            //    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            //    //tmp.Close();

                            //}
                            tmp = new Recordset();
                            //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            object ST = 0;


                            if (dgv1["barcode", i].Value.ToString() != "999")
                            {

                                sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + Gvar.wr_code + " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";

                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                                ST = 0;
                                // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                                if (!tmp.EOF) ST = tmp.Fields[0].Value;

                                sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + Gvar.wr_code + " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";
                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                                if (tmp.RecordCount == 0) tmp.AddNew();


                                tmp.Fields["WR_CODE"].Value = Gvar.wr_code;
                                tmp.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                tmp.Fields["ITEM_CODE"].Value = dgv1["itemcode", i].Value;
                                tmp.Fields["User"].Value = Gvar.Userid;

                                tmp.Fields["UPD_flag"].Value = "N";
                                tmp.Fields["stock"].Value = ST;
                                tmp.Update();


                                sql = "SELECT SUM(STOCK) FROM  WR_STOCK_MASTER WHERE  ITEM_CODE='" + dgv1["itemcode", i].Value + "'";
                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                                ST = 0;


                                if (!tmp.EOF) ST = tmp.Fields[0].Value;

                                if (!Convert.IsDBNull(tmp.Fields[0].Value))


                                    sql = "Update STOCK_MASTER set stock = " + Math.Round(Convert.ToDecimal(tmp.Fields[0].Value), 2) + ",UPD_FLAG='N' where    ITEM_CODE='" + dgv1["itemcode", i].Value + "'";
                                else
                                    sql = "Update STOCK_MASTER set stock = 0,UPD_FLAG='N' where    ITEM_CODE='" + dgv1["itemcode", i].Value + "'";

                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            }
                        }
                    }



                    sql = "SELECT ITEM_CODE,wr_code,brn_code FROM TMP_ITM_DETAIL WHERE TRN_NO=" + Convert.ToDecimal(txttrn.Text) + " And trn_type = " + Convert.ToInt16(txttrn_type.Text) + " AND ITEM_CODE NOT IN (SELECT ITEM_CODE FROM TRN_ITM_DETAIL WHERE TRN_NO=" + Convert.ToDecimal(txttrn.Text) + " And trn_type = " + Convert.ToInt16(txttrn_type.Text) + ")";

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    while (!tmp.EOF)
                    {

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + Gvar.wr_code + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";

                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object ST1 = 0;
                        //if (rec.RecordCount != 0) ST1 = rec.Fields[0].Value;

                        // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                        if (!rec.EOF) ST1 = rec.Fields[0].Value;




                        sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + Gvar.wr_code + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";
                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (rec.RecordCount == 0) rec.AddNew();
                        rec.Fields["WR_CODE"].Value = Gvar.wr_code;
                        rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                        rec.Fields["ITEM_CODE"].Value = tmp.Fields["item_code"].Value;
                        rec.Fields["User"].Value = Gvar.Userid;

                        rec.Fields["UPD_flag"].Value = "N";
                        rec.Fields["stock"].Value = ST1;
                        rec.Update();

                        sql = "SELECT STOCK FROM wr_STOCK_MASTER WHERE  ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";
                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        if (!Convert.IsDBNull(rec.Fields[0].Value))


                            sql = "Update STOCK_MASTER set stock = " + Math.Round(Convert.ToDecimal(rec.Fields[0].Value), 2) + ",UPD_FLAG='N' where    ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";
                        else
                            sql = "Update STOCK_MASTER set stock = 0,UPD_FLAG='N' where    ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";

                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        tmp.MoveNext();

                    }

                    if (tmp.RecordCount != 0)
                    {

                        sql = "delete FROM Tmp_ITM_DETAIL WHERE TRN_NO=" + Convert.ToDecimal(txttrn.Text) + " And trn_type = " + Convert.ToInt16(txttrn_type.Text);

                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    }




                    //ADOconn.CommitTrans();

                    iserror = false;





                    //MessageBox.Show("Service Issue Entry Saved Successfully!!!", "Succeed Entry");
                }

                catch (SqlException ex)
                {
                    //ADOconn.RollbackTrans();
                    lblmsg.Text = ex.Message;
                    iserror = true;
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception sq)
            {

                //ADOconn.RollbackTrans();
                lblmsg.Text = sq.Message;
                iserror = true;
                MessageBox.Show(sq.Message);
            }


        }
       



        private void updat_accounts()
        {
            try
            {
                try
                {




                    decimal TXTSRAMT = Convert.ToDecimal(txtnetamt.Text) * Gvar._cur_rate;
                    decimal OLD_AMNT = Convert.ToDecimal(txtnetamt.Text);
                    decimal paidamt = 0;
                    


                    if (Convert.ToDecimal(txtnetamt.Text) > 0)
                    {

                        object TRNBY;
                        object DRCR;
                        object DRCR1;
                        object NARR;
                        object LACC;
                        object PAYBY;
                        object[] ledgerini;
                        string acc_acs;
                        object vat_ac ;
                        object exp_ac ;
                        acc_acs = Program.ledger_ini(Convert.ToInt16(txttrn_type.Text), txtinvno.Text);
                        ledgerini = acc_acs.Split('`');
                        TRNBY = Convert.ToInt16((ledgerini[0]));
                        DRCR = ledgerini[1];
                        DRCR1 = ledgerini[2];
                        NARR = ledgerini[3];
                        LACC =   Convert.ToInt64((ledgerini[4]));
                        PAYBY = Convert.ToInt16(ledgerini[05]);
                        vat_ac = ledgerini[06].ToString();
                        exp_ac = ledgerini[07].ToString();

                        if (vat_ac == "") vat_ac = "0";
                        if (exp_ac == "") exp_ac = "0";
                        //NARR = txtremarks.Text.Trim();
                        if (NARR == "")
                            NARR = lbltrntype.Text + " : " + txtinvno.Text;//+ "-" + txtcusname.Text;

                        Recordset TMP = new Recordset();
                        if (isedit)
                        {
                            sql = "INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code] FROM TRN_ACCOUNTS WHERE NYEAR=" + nyear.Text + " and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;

                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        }


                        sql = "DELETE FROM TRN_ACCOUNTS WHERE NYEAR='" + nyear.Text + "' and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        sql = "DELETE FROM TRaN_ACC WHERE YEAR(CUR_DATE) ='" + nyear.Text + "' and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                        sql = "select * from trnno";
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object trno = TMP.Fields[0].Value;
                        object trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                        object accno = 0;



                       
                        Recordset acc = new Recordset();
                       

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
                        if (vat_ac == "0" && Convert.ToDouble(txtvatamt.Text) > 0)
                        {
                            MessageBox.Show("Invalid Initial VAT Account", "Invalid Account");
                            iserror = true;
                            return;
                        }

                        # region Credit account
                        switch (Convert.ToInt16(txttrn_type.Text))
                        {
                            case 2:
                           
                                {
                                    sql = "select * from trnno";
                                    TMP = new Recordset();
                                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    trno = TMP.Fields[0].Value;
                                    trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                    acc.AddNew();
                                    acc.Fields["trn_no"].Value = trno;
                                    acc.Fields["trn_no2"].Value = trno2;
                                    acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                    acc.Fields["DR_CR"].Value = DRCR;
                                    acc.Fields["User_id"].Value = Gvar.Userid;
                                    acc.Fields["acc_no"].Value = Convert.ToDecimal(txtcustomer.Text);
                                    acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);    
                                    acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) * Convert.ToDecimal(txtrate.Text); ;
                                    //acc.Fields["f_pay_amount"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text) / Gvar.Get_Currency_rate(Convert.ToDouble(txtcustomer.Text), cmbcurrency.SelectedValue.ToString());
                                    //acc.Fields["F_RATE"].Value = txtrate.Text;

                                    double frate = Gvar.Get_Currency_rate(Convert.ToDouble(txtcustomer.Text), cmbcurrency.SelectedValue.ToString());
                                    acc.Fields["f_pay_amount"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text) / frate;
                                    acc.Fields["F_RATE"].Value = frate;
                                    
                                    acc.Fields["currency"].Value = cmbcurrency.SelectedValue;
                                    
                                    acc.Fields["pay_date"].Value = dt1.Value;
                                    acc.Fields["NARRATION"].Value = NARR + " (Credit)"; ;
                                    acc.Fields["doc_no"].Value = txtinvno.Text;
                                    acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;
                                    acc.Fields["VOUCHER_NO"].Value = txtinvno.Text;
                                    acc.Fields["TRN_BY"].Value = TRNBY;
                                    acc.Fields["cost_code"].Value = 0;
                                    acc.Fields["dept_code"].Value = 0;
                                    acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    acc.Fields["trn_type"].Value = txttrn_type.Text;

                                   
                                    acc.Update();
                                }
                                break;
                        }

                        #endregion credit acoun



                     


                   

                        #region Inventory ACcount
                        string lnarr = "";
                        switch (Convert.ToInt16(txttrn_type.Text))
                        {
                            
                            case 2:
                            
                                {
                                    LACC = stock_ac;
                                    lnarr = " (StockAC)";
                                }
                                break;
                            default:
                                {
                                    lnarr = " (SaleAC)";
                                    break;
                                }


                        }


                        if (Convert.ToDecimal(txtnetamt.Text) > 0)
                        {
                            sql = "select * from trnno";
                            TMP = new Recordset();
                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            trno2 = TMP.Fields[0].Value;
                           // trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                            acc.AddNew();
                            acc.Fields["trn_no"].Value = trno2;
                            acc.Fields["trn_no2"].Value = trno;
                            acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                            acc.Fields["DR_CR"].Value = DRCR1;
                            acc.Fields["User_id"].Value = Gvar.Userid;
                            acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                            //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            double pay_amt = Math.Round((Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text)) - Convert.ToDouble(txtvatamt.Text), 2);
                            acc.Fields["PAY_AMOUNT"].Value = pay_amt; ;
                            acc.Fields["f_pay_amount"].Value = pay_amt * Convert.ToDouble(txtrate.Text);
                            
                           
                            acc.Fields["F_RATE"].Value = txtrate.Text;
                            acc.Fields["currency"].Value = cmbcurrency.SelectedValue;
                            acc.Fields["VOUCHER_NO"].Value = txtinvno.Text;
                            acc.Fields["pay_date"].Value = dt1.Value;
                            acc.Fields["NARRATION"].Value = NARR + lnarr;
                            acc.Fields["doc_no"].Value = txtinvno.Text;
                            acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;
                            acc.Fields["TRN_BY"].Value = TRNBY;
                            acc.Fields["NYEAR"].Value = dt1.Value.Year;
                            acc.Fields["cost_code"].Value = 0;
                            acc.Fields["dept_code"].Value = 0;
                            acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                            acc.Fields["trn_type"].Value = txttrn_type.Text;

                            acc.Update();
                        }
                        #endregion Inventory ACcount
                        #region vat


                        if (Convert.ToDecimal(txtvatamt.Text) > 0)
                        {



                            if (vat_ac == "0")
                            {
                                MessageBox.Show("Invalid Initial Account[VAT_AC]", "Invalid Account");
                                iserror = true;
                                return;
                            }


                            sql = "SELECT TOP 1 * FROM TRN_ACCOUNTS";
                            ADODB.Recordset rec = new ADODB.Recordset();
                            ADODB.Recordset tmp = new ADODB.Recordset();

                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            long trnno = 0;
                            long trnno2 = 0;

                            rec.AddNew();
                            sql = "SELECT * FROM TRNNO";

                            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                            //if (tmp.Fields[0].Value == DBNull.Value)
                            //{
                            //    trnno = 1;
                            //}
                            //else
                            {
                                trnno = Convert.ToInt64(tmp.Fields[0].Value.ToString());
                                trnno2 = trnno + 1;
                            }
                            rec.Fields["trn_no"].Value = trnno;
                            rec.Fields["trn_no2"].Value = trnno + 1;


                            double amt = Convert.ToDouble(txtvatamt.Text);

                            string docno = txtinvno.Text;


                            rec.Fields["acc_no"].Value = vat_ac;
                            rec.Fields["EntrY_no"].Value = Convert.ToDecimal(txtinvno.Text); ;
                            double rate = 1;
                            rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                            rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                            rec.Fields["F_RATE"].Value = 1;
                            rec.Fields["TRN_BY"].Value = TRNBY;
                            rec.Fields["DR_CR"].Value = DRCR;
                            rec.Fields["user_ID"].Value = Gvar.Userid;
                            rec.Fields["PAYBY"].Value = exp_ac;
                            //rec.Fields["RQTY"].Value = 0;
                            rec.Fields["SNO"].Value = 1;
                            // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["NARRATION"].Value = "Sales Vat Amount for  " + txtinvno.Text + "(" + txttrn_type.Text + ")";
                            rec.Fields["Voucher_No"].Value = 0;
                            rec.Fields["cost_code"].Value = 0;
                            rec.Fields["dept_code"].Value = 0;
                            rec.Fields["pay_date"].Value = dt1.Value;
                            rec.Fields["doc_no"].Value = docno;
                            rec.Fields["NYEAR"].Value = dt1.Value.Year;
                            rec.Fields["brn_code"].Value = Gvar.brn_code;
                            rec.Fields["currency"].Value = "SR";
                            rec.Fields["trn_type"].Value = txttrn_type.Text;

                            rec.Update();



                            ////sql = "select * from TRN_accounts where trn_NO2 =  '" + txtactrnno.Text.Trim() + "' AND TRN_BY = 13 and Sno = 2";
                            ////rec = new ADODB.Recordset();
                            ////rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            ////if (rec.RecordCount == 0)
                            ////{
                            //rec.AddNew();
                            //tmp = new ADODB.Recordset();
                            //sql = "SELECT * FROM TRNNO";

                            //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            ////if (tmp.Fields[0].Value)
                            ////    txttrn.Text="1";
                            ////else
                            //if (tmp.Fields[0].Value == DBNull.Value)
                            //{
                            //    trnno = 1;
                            //}
                            //else
                            //{
                            //    trnno2 = Convert.ToInt64(tmp.Fields[0].Value.ToString());

                            //}
                            //rec.Fields["trn_no"].Value = trnno2;
                            //rec.Fields["trn_no2"].Value = trnno;



                            //rec.Fields["acc_no"].Value = LACC;
                            //rec.Fields["EntrY_no"].Value = Convert.ToDecimal(txtinvno.Text); ;

                            //rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                            //rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                            //rec.Fields["F_RATE"].Value = 1;
                            //rec.Fields["TRN_BY"].Value = TRNBY;
                            //rec.Fields["DR_CR"].Value = DRCR1;
                            //rec.Fields["user_ID"].Value = Gvar.Userid;
                            //rec.Fields["PAYBY"].Value = exp_ac;
                            ////rec.Fields["RQTY"].Value = 0;
                            //rec.Fields["SNO"].Value = 2;
                            //// rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            //rec.Fields["NARRATION"].Value = "Sales Vat Amount for  " + txtinvno.Text + "(" + txttrn_type.Text + ")";

                            //rec.Fields["Voucher_No"].Value = 0;
                            //rec.Fields["cost_code"].Value = 0;
                            //rec.Fields["dept_code"].Value = 0;
                            //rec.Fields["pay_date"].Value = dt1.Value;
                            //rec.Fields["doc_no"].Value = docno;
                            //rec.Fields["NYEAR"].Value = dt1.Value.Year;
                            //rec.Fields["brn_code"].Value = Gvar.brn_code;
                            //rec.Fields["currency"].Value = "SR";

                            //rec.Update();
                        }


                        #endregion

                        #region cost Item account
                        switch (Convert.ToInt16(txttrn_type.Text))
                        {
                            case 3:
                            case 4:
                            case 6:

                            case 7:
                                {
                                    sql = "select * from trnno";
                                    TMP = new Recordset();
                                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    trno = TMP.Fields[0].Value;
                                    trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                    acc.AddNew();
                                    acc.Fields["trn_no"].Value = trno;
                                    acc.Fields["trn_no2"].Value = trno2;
                                    acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                    acc.Fields["DR_CR"].Value = DRCR;
                                    acc.Fields["User_id"].Value = Gvar.Userid;
                                    acc.Fields["acc_no"].Value = stock_ac;
                                    acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                                    acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) * Convert.ToDecimal(txtrate.Text); ;
                                    acc.Fields["f_pay_amount"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text) ;
                                    acc.Fields["F_RATE"].Value = txtrate.Text;
                                    acc.Fields["currency"].Value = cmbcurrency.SelectedValue;
                                    acc.Fields["VOUCHER_NO"].Value = txtinvno.Text;
                                    acc.Fields["pay_date"].Value = dt1.Value;
                                    acc.Fields["NARRATION"].Value = NARR + " (Stock)";
                                    acc.Fields["doc_no"].Value = txtinvno.Text;
                                    acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;

                                    acc.Fields["TRN_BY"].Value = TRNBY;
                                    acc.Fields["cost_code"].Value = 0;
                                    acc.Fields["dept_code"].Value = 0;
                                    acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    acc.Fields["trn_type"].Value = txttrn_type.Text;
                                    acc.Update();
                                }
                                {
                                    sql = "select * from trnno";
                                    TMP = new Recordset();
                                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    trno = TMP.Fields[0].Value;
                                    trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                    acc.AddNew();
                                    acc.Fields["trn_no"].Value = trno;
                                    acc.Fields["trn_no2"].Value = trno2;
                                    acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                    acc.Fields["DR_CR"].Value = DRCR1;
                                    acc.Fields["User_id"].Value = Gvar.Userid;
                                    acc.Fields["acc_no"].Value = cost_ac;
                                    acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                                    acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) * Convert.ToDecimal(txtrate.Text); ;
                                    acc.Fields["f_pay_amount"].Value = Convert.ToDouble(txtnetamt.Text) * Convert.ToDouble(txtrate.Text) ;
                                    acc.Fields["F_RATE"].Value = txtrate.Text;
                                    acc.Fields["currency"].Value = cmbcurrency.SelectedValue;
                                    acc.Fields["VOUCHER_NO"].Value = txtinvno.Text;
                                    acc.Fields["pay_date"].Value = dt1.Value;
                                    acc.Fields["NARRATION"].Value = NARR + " (Cost)";
                                    acc.Fields["doc_no"].Value = txtinvno.Text;
                                    acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;

                                    acc.Fields["TRN_BY"].Value = TRNBY;
                                    acc.Fields["cost_code"].Value = 0;
                                    acc.Fields["dept_code"].Value = 0;
                                    acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    acc.Fields["trn_type"].Value = txttrn_type.Text;
                                    acc.Update();
                                }
                                break;
                        }

                        #endregion Cost Item Account


                        sql = "SELECT TOP 1 * FROM TRAN_ACC";
                        acc = new Recordset();
                        acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        acc.AddNew();
                        // acc.Fields["trn_no"].Value = trno;
                        acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                        acc.Fields["PaidTo_Acc"].Value = 0;
                        //acc.Fields["User"].Value = Gvar.Userid;
                        acc.Fields["acc_no"].Value = txtcustomer.Text.ToString();
                        acc.Fields["AMOUNT"].Value = (Convert.ToDecimal(txtnetamt.Text)*Convert.ToDecimal(txtrate.Text));
                        acc.Fields["currency_rate"].Value = txtrate.Text;

                        acc.Fields["Ledger_acc"].Value = LACC;
                        acc.Fields["CUR_DATE"].Value = dt1.Value;
                        acc.Fields["Description"].Value = NARR;
                        acc.Fields["doc_no"].Value = txtinvno.Text;
                        acc.Fields["currency_code"].Value =cmbcurrency.SelectedValue;
                        acc.Fields["TRN_BY"].Value = TRNBY;
                        // acc.Fields["NYEAR"].Value = dt1.Value.Year;
                        acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                        acc.Update();

                        iserror = false;

                    }
                }







                catch (SqlException er)
                {
                    lblmsg.Text = er.Message;
                    MessageBox.Show(er.Message);
                    iserror = true;
                }
            }




            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                MessageBox.Show(ex.Message);
                iserror = true;
            }
        }

        private void FrmPurOrder_Validated(object sender, EventArgs e)
        {
            //acntrl = (Control)sender;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ADOconn.State == 0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

            sql = "sELECT adrs_code,address  froM address_master WHERE adrs_type='Porder' ORDER BY adrs_code";

            SqlDataAdapter adrs = new SqlDataAdapter(sql, Conn);

            DataTable dtadrs = new DataTable("address");
            adrs.Fill(dtadrs);
            cmbadrscode.DisplayMember = "adrs_code";
            cmbadrscode.ValueMember = "adrs_code";
            cmbadrscode.DataSource = dtadrs;
            if (cmbadrscode.Items.Count > 0)
                cmbadrscode.SelectedIndex = 0;
            pnladdress.Visible = !pnladdress.Visible;

            cmbadrscode.Focus();
        }

        private void btncloseadress_Click(object sender, EventArgs e)
        {

            Conn.Close();

            Conn.Open();
            pnladdress.Visible = false;

            if (ADOconn.State == 0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
              
            sql = "sELECT adrs_code,address  froM address_master WHERE adrs_type='Porder' ORDER BY adrs_code";

            SqlDataAdapter adrs = new SqlDataAdapter(sql, Conn);

            DataTable dtadrs = new DataTable("address");
            adrs.Fill(dtadrs);
            cmbaddress.DisplayMember = "adrs_code";
            cmbaddress.ValueMember = "adrs_code";


            cmbaddress.DataSource = dtadrs;
            if (cmbaddress.Items.Count > 0)
                cmbaddress.SelectedIndex = 0;
        }

        private void btnsaveadress_Click(object sender, EventArgs e)
        {
            try
            {

                if(cmbaddress.Text.Trim()=="")
                {
                    MessageBox.Show("Invalid Address Code");
                    return;
                }
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
              
                Recordset TMP = new Recordset();
                sql = "select * from address_master where adrs_type='Porder' and adrs_code ='" + cmbadrscode.Text.Trim() + "'";
                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (TMP.RecordCount == 0) TMP.AddNew();
                    TMP.Fields["adrs_type"].Value ="Porder";
                    TMP.Fields["adrs_code"].Value = cmbadrscode.Text.Trim();
                    TMP.Fields["address"].Value = txtaddress.Text; 

                    TMP.Update();
                    MessageBox.Show("Successfully Added the address");
                    cmbadrscode.Text = "";
                    txtaddress.Text = "";
                    cmbadrscode.Focus();
               
            }
             catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
               
            }

        }

        private void cmbadrscode_Validated(object sender, EventArgs e)
        {
        }

        private void cmbcurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] currate = cmbcurrency.Text.Split(':');
                txtrate.Text = currate[1];
            }
            catch
            {

            }
        }

        private void grdbutton_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            switch (grdbutton.CurrentCell.ColumnIndex)
            {
                case 0:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("Acccreationtool");

                        Gvar.trntype = 202;
                        Form childForm = new frmAccounts();
                        childForm.MdiParent = MDIParent1.ActiveForm;

                       
                        childForm.Text = "Supplier Entry/Edit Screen";
                        childForm.Show();
                        break;
                    }
                case 1:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("Acccreationtool");

                      
                        Gvar.trntype = 200;
                        Form childForm = new frmAccounts();

                        childForm.MdiParent = MDIParent1.ActiveForm;
                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Account Creation Entry/Edit Screen";
                        childForm.Show();

                        break;
                    }
                case 2:
                    {

                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("mnuItemMaster");

                        Gvar.invno = "0";
                       
                        Gvar._Gind = 1;
                        Form childForm = new FrmAssetMaster();
                        childForm.MdiParent = MDIParent1.ActiveForm;
                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Item Master Screen";

                        childForm.Show();
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

        private void btncurrency_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("mnuItemUnit");
            Form childForm = new FrmItemUnit();
            childForm.MdiParent = MDIParent1.ActiveForm;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Unit Entry Screen";
            childForm.Show();
        }

        private void set_bgcolor(Color color)
        {
            try
            {


                foreach (Control c in this.Controls)
                {
                    try
                    {
                        string a = c.Name;

                        if (c.BackColor.ToString() == Gvar._defaultcolor)
                        {
                            c.BackColor = color;
                            // if (c.GetType() == typeof(TextBox)) {
                            //c.BackgroundColor = colorDlg.Color;
                            c.Refresh();
                        }
                        c.BackColor = color;

                    }
                    catch
                    {

                    }

                    this.BackColor = color;
                    dgv1.BackgroundColor = color;
                   
                    //Gvar._defaultcolor = color;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void cmbadrscode_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                if (cmbaddress.Text.Trim() == "")
                {
                    //MessageBox.Show("Invalid Address Code");
                    return;
                }

                Conn.Close();

                Conn.Open();
                Recordset TMP = new Recordset();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


                sql = "select * from address_master where adrs_type='Porder' and adrs_code ='" + cmbadrscode.Text.Trim() + "'";
                TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (TMP.RecordCount != 0)
                {

                    cmbadrscode.Text = TMP.Fields["adrs_code"].Value.ToString();
                    txtaddress.Text = TMP.Fields["address"].Value.ToString();
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void txtorderno_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtorderno.ReadOnly) return;
                Conn.Close();
                Conn.Open();
                string sql = "";
                switch (Convert.ToInt16(txttrn_type.Text))
                {
                    case 22:
                        sql = "select Ref_NO As ReferenceNo,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=" + Convert.ToInt16(txttrn_type.Text) + "  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC";
                        GrdLookup.Tag = "ref";
                        break;
                    case 2:
                        sql = "select Invoice_NO As OrderNo,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=22  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC";
                        GrdLookup.Tag = "ord";
                        break;
                }


                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = txtorderno.Left;
                GrdLookup.Top = txtorderno.Top + txtorderno.Height;
                dv.AllowEdit = true;
                dv.Table = dt;

                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                GrdLookup.Visible = true;
            }
            catch
            {
            }

        }

        private void txtorderno_Validated(object sender, EventArgs e)
        {
            if (Gvar.trntype != 2 || txtorderno.Text == "") return;
            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset rec = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            rec = new ADODB.Recordset();
            tmp = new ADODB.Recordset();

            try
            {

                txtcusname.Text = "";
                txtcustomer.Text = "";
                orderdate.Text = DateTime.Now.ToString();
                isedit = false;
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                rec = new ADODB.Recordset();

                sql = "SELECT accode,ename,curdate,CURRENCY,remarks,rec_no FROM DATA_ENTRY WHERE NYEAR=" + nyear.Text + " AND  TRN_TYPE=22 AND INVOICE_NO= '" + txtorderno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (rec.RecordCount > 0)
                {
                    cmbcurrency.SelectedValue = rec.Fields["CURRENCY"].Value.ToString();
                    txtcusname.Text = rec.Fields["ename"].Value.ToString();
                    txtcustomer.Text = rec.Fields["accode"].Value.ToString();
                    orderdate.Text = rec.Fields["curdate"].Value.ToString();
                    txtremarks.Text = rec.Fields["remarks"].Value.ToString();
                    txtorderrecno.Text = rec.Fields["rec_no"].Value.ToString();
                    if (txtorderrecno.Text == null) txtorderrecno.Text = "0";
                   
                    //dgv1.Focus();
                }

                cmbterm.SelectedValue = -1;
                cmbshipterm.SelectedValue = -1;
                cmbmode.SelectedValue = -1;
                cmbport.SelectedValue = -1;
                txtotherterms.Text = "";
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM PURORDER_TERMS WHERE NYEAR=" + nyear.Text + " AND  ORDER_NO= '" + txtorderno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (rec.RecordCount > 0)
                {
                    cmbterm.Text = rec.Fields["PAY_TERM"].Value.ToString();
                    cmbshipterm.Text = rec.Fields["Ship_Term"].Value.ToString();
                    cmbmode.Text = rec.Fields["SHIPMENT"].Value.ToString();
                    cmbport.Text = rec.Fields["PORT"].Value.ToString();
                    txtotherterms.Text = rec.Fields["OTHER"].Value.ToString();
                    cmbaddress.Text = rec.Fields["address"].Value.ToString();

                }
                dgv1.CellEnter -= dgv1_CellEnter;
                dgv1.SelectionChanged -= dgv1_SelectionChanged;


                int i = 0;
                // dgv1.Rows.Add(rec.RecordCount+1);

                dgv1.Rows.Clear(); dgv1.Refresh();
                sql = "select BdescrIPTION,stock,avg_PUR_PRICE,LAST_PUR_PRICE,q.ITEM_CODE,p.FRACTION,p.UNIT,stock,e.wr_code,q.ITEM_CODE,q.ITEM_ID,hfraction,p.barcode,bdescription,r_min_profit,p.rqty,p.qty,p.price,RETAIL_PRICE  from QRY_barcode as q inner join   PUR_ORDER_GRID as p on q.barcode = p.barcode inner join data_entry e on p.rec_no=e.rec_no  and q.wr_code=e.wr_code where e.rec_no='" + txtorderrecno.Text + "' and  qty-rqty >0 "; // AND WR_CODE=" + Gvar.wr_code;
                rec = new ADODB.Recordset();

                dgv1.AllowUserToAddRows = false;
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if (rec.RecordCount>0)
                dgv1.Rows.Add(rec.RecordCount);

                // foreach (DataGridViewRow row in this.dgv1.Rows)
                 while (!rec.EOF)
                    {
                        if (!string.IsNullOrEmpty(rec.Fields[0].Value.ToString()))
                        {
                           

                            dgv1.Rows[i].Cells["bARCode"].Value = rec.Fields["barcode"].Value.ToString();
                            dgv1.Rows[i].Cells["Description"].Value = rec.Fields[0].Value.ToString();
                            //  dgv1.Rows[i].Cells["Fraction"].Value = rd[3].Value.ToString();

                            dgv1.Rows[i].Cells["ItemCode"].Value = rec.Fields[4].Value.ToString();

                            dgv1.Rows[i].Cells["fraction"].Value = rec.Fields[5].Value.ToString();
                            dgv1.Rows[i].Cells["price"].Value = rec.Fields["avg_PUR_PRICE"].Value.ToString();
                            if (txttrn_type.Text == "2")
                            {
                                object q = rec.Fields["qty"].Value.ToString();
                                object r = rec.Fields["rqty"].Value.ToString();
                                decimal b = Convert.ToDecimal(q) - Convert.ToDecimal(r);

                                dgv1.Rows[i].Cells["qty"].Value = b;
                                dgv1.Rows[i].Cells["balqty"].Value = b; // q + "/" + r;
                                dgv1.Rows[i].Cells["price"].Value = rec.Fields[17].Value;
                            }

                            dgv1.Rows[i].Cells["unit"].Value = rec.Fields[6].Value.ToString();

                          
                            
                             



                            //dgv1.Rows[i].Cells["recieved"].Value = rd[3].Value.ToString();

                            dgv1.Rows[i].Cells["Itemid"].Value = rec.Fields[10].Value.ToString();

                            dgv1.Rows[i].Cells["hfraction"].Value = rec.Fields[11].Value.ToString();
                            dgv1.Rows[i].Cells["minprofit"].Value = rec.Fields[14].Value.ToString();
                            dgv1.Rows[i].Cells["cost"].Value = rec.Fields["RETAIL_PRICE"].Value.ToString();
                            dgv1.Rows[i].Cells["stock"].Value = rec.Fields[7].Value.ToString();
                            //btnsave.Enabled = true;
                            //btndelete.Enabled = true;
                            //btnPrint.Enabled = true;
                            nodata = false;
                        }

                        txtorderno.ReadOnly = true;

                        if(Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value)>0)
                        dgv1.Rows[i].Cells["cost"].Value =Math.Round(Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) / Convert.ToDecimal(txtrate.Text),2).ToString();
                        //dgv1.Rows[i].Cells["RETAIL_PRICE"].Value = (Convert.ToDecimal(dgv1.Rows[i].Cells["RETAIL_PRICE"].Value) * Convert.ToDecimal(txtrate.Text)).ToString();
                        if (Convert.ToDecimal(dgv1.Rows[i].Cells["price"].Value) > 0)
                        dgv1.Rows[i].Cells["price"].Value =Math.Round(Convert.ToDecimal(dgv1.Rows[i].Cells["price"].Value) / Convert.ToDecimal(txtrate.Text),2).ToString();
                        //dgv1.Rows[i].Cells["cost"].Value = (Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) * Convert.ToDecimal(txtrate.Text)).ToString();
                        txtorderno.Enabled = false;
                        rec.MoveNext();
                        i++;
                    }


               

                find_total();


                dgv1.Columns["barcode"].ReadOnly = true;
                dgv1.Columns["unit"].ReadOnly = true;
                dgv1.Columns["qty"].ReadOnly = false;
                dgv1.Columns["Price"].ReadOnly = true;
                dgv1.Columns["disc"].ReadOnly = true;
                dgv1.Columns[0].Frozen = true;
                dgv1.Columns[1].Frozen = true;
                dgv1.Columns[2].Frozen = true;
                dgv1.Columns[0].Frozen = true;
              
               if (dgv1.Rows.Count > 0)
               {
                   dgv1.FirstDisplayedCell = dgv1[3, 0];
                   dgv1.CurrentCell = dgv1["qty", 0];
               }
                dgv1.CellEnter += dgv1_CellEnter;
                dgv1.SelectionChanged += dgv1_SelectionChanged;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtorderno_TextChanged(object sender, EventArgs e)
        {
            if (txtorderno.Text.Trim() == "")
                dgv1.Columns["orderno"].Visible = true;
            else
            {
                dgv1.Columns["orderno"].Visible = false;
            }

        }

        private void txtcustomer_Leave(object sender, EventArgs e)
        {
            if(txtcusname.Text=="" && txtcustomer.Text!="")
                txtcustomer_Validated(sender,e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form childForm = new FrmBrand();
            //childForm.MdiParent = this;
            Gvar.Gind = 8;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Signatory Detail";

            childForm.ShowDialog();
            populate_options();
        }

        private void dgv1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void FrmPurOrder_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            if (dgv1.CurrentCell.IsInEditMode && cur_col=="barcode" &&  !GrdLookup.Visible && acntrl=="dgv1")
            {
                set_grdlookup();
                dgv1.EndEdit();
                dgv1.BeginEdit(false);
                if (dgv1.CurrentCell.Value!=null)
                textBox1.Text = dgv1.CurrentCell.Value.ToString();
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.Text.Length;

            }
        }

       
            private void txtinvno_Leave(object sender, EventArgs e)
        {
            if (!btnsave.Enabled)
            search_mrn();
        }

            private void dgv1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
            {
                if (Convert.ToInt64(dgv1["balqty", e.Row.Index].Value) !=Convert.ToInt64(dgv1["qty", e.Row.Index].Value) && txttrn_type.Text =="22" && isedit )
                {
                    // Do not allow the user to delete the Starting Balance row.
                    MessageBox.Show("Cannot delete Already Recieved Item!");

                    // Cancel the deletion if the Starting Balance row is included.
                    e.Cancel = true;
                }
            }

            private void label14_DoubleClick(object sender, EventArgs e)
            {
                
            }

            private void lblinvstatus_DoubleClick(object sender, EventArgs e)
            {
                btnsave_Click(sender, e);
            }



            private void find_vat()
            {
                if (txttotal.Text == "") txttotal.Text = "0";
                if (txtdscamt.Text == "") txtdscamt.Text = "0";
                if (txtnetamt.Text == "") txtnetamt.Text = "0";
                if (txtvatpcnt.Text == "") txtvatpcnt.Text = "0";

                txtvatamt.Text = Math.Round(((Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdscamt.Text)) * Convert.ToDecimal(txtvatpcnt.Text) / 100), 2).ToString();
                txtnetamt.Text = Math.Round(((Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdscamt.Text)) + Convert.ToDecimal(txtvatamt.Text)), 2).ToString();

                // txtvatamt.Text = Math.Round((Convert.ToDecimal(txtnetamt.Text) * Convert.ToDecimal(txtvatpcnt.Text) / 100), 2).ToString();
            }

            private void txtvatamt_TextChanged(object sender, EventArgs e)
            {
                find_vat();
            }





      
   
        

    }
}








    

