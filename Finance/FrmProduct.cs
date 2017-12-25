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
    public partial class FrmProduct : Form
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
        int cur_row;
        int dblclk_row;
        int fcol;
        string last_col;
        string cur_col;
        int last_row;
        Boolean nodata;


        public FrmProduct()
        {
            try
            {


                InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
                load_form();


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
                object a ;

                rec_options = new DataTable("rec_options");
                adalkp.Fill(rec_options);
                bg_color = System.Drawing.Color.FromName(rec_options.Rows[0]["bg_color"].ToString());  
                if (rec_options.Rows.Count<1)
                {
                    MessageBox.Show("Invalid Transaction Type, Please Register The Transaction Type!!!");

                }
                if (rec_options.Rows.Count>0)
                 a = rec_options.Rows[0]["auto_next_line"];


                isini = true;
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


                sql = "SELECT  ACC_NO,ACC_NAME FROM ACCOUNTS  WHERE ACC_TYPE_CODE= (SELECT  EMP_AC_TYPE FROM AC_OPTIONS WHERE  ac_options.ID =1) union select 0 , 'Direct' order by 1 ";
                SqlDataAdapter sales = new SqlDataAdapter(sql, Conn);
                DataTable dtsales = new DataTable("SaleType");
                sales.Fill(dtsales);
                cmbsalesagent.DisplayMember = "ACC_NAME";
                cmbsalesagent.ValueMember = "ACC_NO";

                cmbsalesagent.DataSource = dtsales;
                cmbsalesagent.SelectedIndex = 0;
                dt1.Value = DateTime.Now;

                switch (trn)
                {
                    case -1:
                        {
                            sql = "SELECT  ACC_NO,ACC_NAME FROM ACCOUNTS  WHERE ACC_NO= (SELECT TOP 1 PROD_AC_NO FROM AC_OPTIONS WHERE  ac_options.ID =1) ";
               
                        }
                        break;
                            case 11:
                        {
                            sql = "SELECT  ACC_NO,ACC_NAME FROM ACCOUNTS  WHERE ACC_NO= (SELECT TOP 1 DAMAGE_AC_NO FROM AC_OPTIONS WHERE  ac_options.ID =1) ";
               
                        }
                        break;

                }
               
               
                SqlDataAdapter CUS = new SqlDataAdapter(sql, Conn);
                DataTable DTCUS = new DataTable("SaleType");
                CUS.Fill(DTCUS);
                if (DTCUS.Rows.Count > 0)
                    txtcustomer.Text = DTCUS.Rows[0][0].ToString();


                sql = "SELECT  wr_CODE,WR_NAME  FROM WRHOUSE_MASTER";
                SqlDataAdapter wr = new SqlDataAdapter(sql, Conn);
                DataTable dtwr1 = new DataTable("WR1");
               
                wr.Fill(dtwr1);

                cmbwhouse.DisplayMember = "WR_NAME";
                cmbwhouse.ValueMember = "wr_CODE";

                cmbwhouse.DataSource = dtwr1;
                cmbwhouse.SelectedIndex = 0;
                

                if (trn == -1)
                {
                    dgv1.Columns["cost"].Visible = true;
                    dgv1.Columns["cost"].HeaderCell.Value = "Sale Price";
                    dgv1.Columns["cost"].ReadOnly = false;
                }

                if (bg_color != null)
                    set_bgcolor(bg_color);
            }
            catch(Exception ex)
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


                dgv1.Refresh();
                dgv1.Columns[0].HeaderText = "Item Code";
                dgv1.Columns[1].HeaderText = "Description";
                dgv1.Columns[2].HeaderText = "Unit";
                dgv1.Columns[3].HeaderText = "Qty";
                dgv1.Columns[4].HeaderText = "Price";
                dgv1.Columns[5].HeaderText = "Disc.";
                dgv1.Columns[6].HeaderText = "Cost";
                dgv1.Columns[7].HeaderText = "Remarks";


                dgv1.Columns[8].HeaderText = "Total";
                dgv1.Columns[9].HeaderText = "Stock";
                dgv1.Columns[10].HeaderText = "Fraction";
                dgv1.Columns[11].HeaderText = "Re-order";


                dgv1.Columns[0].Name = "barcode";
                dgv1.Columns[1].Name = "Description";

                dgv1.Columns[2].Name = "unit";
                dgv1.Columns[3].Name = "qty";
                dgv1.Columns[4].Name = "Price";
                dgv1.Columns[5].Name = "disc";
                dgv1.Columns[6].Name = "cost";
                dgv1.Columns[7].Name = "remarks";



                dgv1.Columns[8].Name = "Total";
                dgv1.Columns[9].Name = "stock";
                dgv1.Columns[10].Name = "fraction";
                dgv1.Columns[11].Name = "Reorder";


                dgv1.Columns[12].Name = "Itemcode";
                dgv1.Columns[13].Name = "proposed";
                dgv1.Columns[14].Name = "itemid";
                dgv1.Columns[15].Name = "hfraction";
                dgv1.Columns[16].Name = "minprofit";
                dgv1.Columns[17].Name = "orgsaleprice";
                dgv1.Columns[18].Name = "updsale";

                dgv1.Columns[6].Visible = false;
                dgv1.Columns[10].Visible = false;
                // dgv1.Columns[9].Visible = false;
                dgv1.Columns[11].Visible = false;


                dgv1.Columns[12].Visible = false;
                dgv1.Columns[13].Visible = false;
                dgv1.Columns[14].Visible = false;

                dgv1.Columns[15].Visible = false;
                dgv1.Columns[16].Visible = false;
                dgv1.Columns[17].Visible = false;
                dgv1.Columns[18].Visible = false;

 //               dgv1.Columns[1].ReadOnly = true;
                //dgv1.Columns[3].ReadOnly = true;
                //dgv1.Columns[4].ReadOnly = true;
                dgv1.Columns[6].ReadOnly = true;
                dgv1.Columns[8].ReadOnly = true;

                dgv1.Columns[7].ReadOnly = false;
                dgv1.Columns[9].ReadOnly = true;
                dgv1.Columns[10].ReadOnly = true;
               
                //dgv1.Columns[2].HeaderText = "Qty";
                dgv1.Columns[0].Width = 170;
                dgv1.Columns[1].Width = 300;
                dgv1.Columns[3].Width = 60;
                dgv1.Columns[4].Width = 60;
                dgv1.Columns[5].Width = 60;
                dgv1.Columns[7].Width = 200;
                dgv1.Columns[9].Width = 80;
                txttrn_type.Text = Gvar.trntype.ToString();


                DataGridViewComboBoxColumn dgvCboColumn = new DataGridViewComboBoxColumn();


                Conn.Close();
                Conn.Open();
                sql = "select acc_no,acc_name from accounts inner join ac_options on  acc_type_code=cash_ac_type AND   ac_options.ID =1 where acc_no <>  " + Gvar.sale_acno;
                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                ada3 = new SqlDataAdapter(sql, Conn);
                //ada3.Fill(dt);
                DataSet siteds = new DataSet();

                ada3.Fill(siteds, "pay_by");


                dgvpaid.CellValueChanged -= dgvpaid_CellValueChanged;

                DataGridViewTextBoxColumn txt20 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn txt21 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn txt22 = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn txt23 = new DataGridViewTextBoxColumn();
                dgvpaid.Columns.Add(txt20);
                dgvpaid.Columns.Add(txt21);
                dgvpaid.Columns.Add(txt22);
                dgvpaid.Columns.Add(txt23);
                dgvpaid.Columns[0].Name = "amount";
                dgvpaid.Columns[1].Name = "paid";
                dgvpaid.Columns[2].Name = "paidby";
                dgvpaid.Columns[3].Name = "paidac";
                dgvpaid.Columns[0].HeaderText = "Amoumt";
                dgvpaid.Columns[1].HeaderText = "Paid";
                dgvpaid.Columns[2].HeaderText = "Paid By";
                dgvpaid.Columns[2].Width = 300;
                dgvpaid.Columns[3].Visible = false;
                lstpaid.Width = 300;


                dgvpaid.Columns[0].Frozen = true;
                dgvpaid.Columns[0].ReadOnly = true;


                ////ada1.Fill(ds, "proj_master");
                ////projdv.Table = dt;
                lstpaid.DisplayMember = "acc_name";
                lstpaid.ValueMember = "acc_no";
                lstpaid.DataSource = siteds.Tables[0];
                //lstpaid.Left=dgvpaid.Colum

                //       var cellRectangle = dgvpaid.GetCellDisplayRectangle(dgvpaid.CurrentCell.ColumnIndex, dgvpaid.CurrentCell.RowIndex, true);
                 switch(txttrn_type.Text)
                 {
                     case "1":
                         {
                             txtcustomer.Text = Gvar.pur_acno;
                             txtcusname.Text = "Cash Supplier";
                             lblcustomer.Text = "Supplier";
                            
                             break;
                         }

                     case "2":
                         {
                             txtcustomer.Text = Gvar.pur_acno;
                             txtcusname.Text = "Cash Supplier";
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
               

                //lstpaid.Top = dgvpaid.Top + cellRectangle.Top;
                //dgvpaid.Columns.Add(dgvCboColumn);
                //dgvpaid.Columns[2].Width = 300;
                //dgvpaid.Columns[2].HeaderText = "Paid As";

                dgvpaid.CellValueChanged += dgvpaid_CellValueChanged;

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


            }

        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.CurrentCell == null) dgv1.CurrentCell = dgv1["barcode", cur_row];
                if (dgv1.CurrentCell == dgv1["barcode", cur_row])
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    dgv1.EndEdit();
                    string crite="";
                    object ITM = dgv1["barcode", cur_row].Value;
                    if(ITM==null) ITM="";
                            if( ITM != "")
                            {
                            crite = "h.DESCRIPTION like '" + ITM.ToString().Trim() + "%' or h.ITEM_CODE like '"+ ITM.ToString().Trim() +  "%' OR PART_NO like '" + ITM.ToString().Trim() +  "%'";
                            crite = "(h.DESCRIPTION like '" + ITM.ToString().Trim() +  "%' or h.ITEM_CODE like '"+ ITM.ToString().Trim() +  "%' or h.BARCODE like '"+ ITM.ToString().Trim() +  "%' )";
                            }
                            
                            //a = InStr(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), " ")

                            //If a > 0 Then
                            //ITM = Left(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), a - 1)
                            //crite = "(DESCRIPTION like '" & Trim(ITM) & "%' or ITEM_CODE like '" & Trim(ITM) & "%' OR PART_NO like '" & Trim(ITM) & "%')"
                            //ITM = Right(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), Len(Trim(myGrid1.TextMatrix(myGrid1.row, 1))) - a)
                            //crite = crite & " AND DESCRIPTION LIKE '%" & ITM & "%'"
                            //End If

                            string sql = "";
                    if (crite !="")
                    {
                       sql= "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT FROM BARCODE as h where " + crite;
                    }
                    else
                    {
                        sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT FROM BARCODE as h ";
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
                    GrdLookup.Left = textBox1.Left;
                    GrdLookup.Top = textBox1.Top + textBox1.Height;
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    GrdLookup.Tag = "Item_Code";
                    dv.Table = dtlkp;
                    GrdLookup.DataSource = dv;
                    GrdLookup.Columns[0].Width = 170;
                    GrdLookup.Columns[1].Width = 300;
                    GrdLookup.Visible = true;
                    textBox1.Focus();
                    Conn.Close();

                }

                if ( cur_col == "Description" && dgv1["barcode", cur_row].Value.ToString() == "999" )
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
                    GrdLookup.Left = textBox1.Left;
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
                if (dgv1.CurrentCell == dgv1["unit", cur_row] && dgv1["barcode", cur_row].Value!="" && dgv1["barcode", cur_row].Value!=null) 
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
                    GrdLookup.Left = textBox1.Left;
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
                        dgv1.Focus();
                        dgv1.CurrentCell = dgv1[cur_col, cur_row];
                        dgv1[cur_col, cur_row].Selected = true;
                        
                    }


                    if (grdcuslookup.Visible)
                    {
                        grdcuslookup.Visible = false;
                        dgv1.Focus();
                    }


                    if (lstpaid.Visible)
                    {
                        lstpaid.Visible = false;
                        dgvpaid.Focus();
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

            if (lstpaid.Visible)
            {

                switch (e.KeyCode)
                {



                    case Keys.Up:

                        int crow = lstpaid.SelectedIndex;
                        int mros = lstpaid.Items.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow > 0)
                            lstpaid.SelectedIndex = crow - 1;

                        e.Handled = false;

                        break;
                    case Keys.Down:

                        crow = lstpaid.SelectedIndex;
                        mros = lstpaid.Items.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow < mros - 1)
                            lstpaid.SelectedIndex = crow + 1;
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
                            dgv1.CurrentCell = dgv1["barCode", cur_row + 1];
                        }
                        else
                        {
                            dgv1.CurrentCell = dgv1["barCode", cur_row];
                        }

                    }
                    else
                    {
                        SendKeys.Send("{Right}");
                    }
                    return true;
                }


                if (msg.WParam.ToInt32() == (int)Keys.ControlKey && acntrl == "dgv1" && !GrdLookup.Visible && cur_col == "barcode")
                {
                    // SendKeys.Send("{Tab}");

                    dgv1_DoubleClick(null, null);
                    return true;
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

                if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgvpaid" && !lstpaid.Visible)
                {
                    // SendKeys.Send("{Tab}");

                    keyData = Keys.Tab;
                    if (dgvpaid.CurrentCell == dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex])
                    {
                        if (dgvpaid.CurrentCell.RowIndex < dgvpaid.RowCount - 1)
                        {
                            dgvpaid.CurrentCell = dgvpaid["amount", dgvpaid.CurrentCell.RowIndex + 1];
                        }
                        else
                        {
                            if (dgvpaid.CurrentCell.ColumnIndex == 2 && dgvpaid.CurrentCell.RowIndex == dgvpaid.RowCount - 1)
                            {
                                txtcash.Focus();
                                return true;
                            }

                            dgvpaid.CurrentCell = dgv1["amount", dgvpaid.CurrentCell.RowIndex];
                        }

                    }
                    else
                    {

                        SendKeys.Send("{Right}");
                    }
                    return true;
                }
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
                acntrl = dgv1.Name;
                cur_row = dgv1.CurrentCell.RowIndex;
                cur_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;

                if (dgv1["barcode", cur_row].Value == null) return;
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



                if (e.ColumnIndex == fcol + 1 && dgv1["barcode", cur_row].Value != null && dgv1["barcode", cur_row].Value.ToString() != "999")
                {

                    search_data(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), "");
                    

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


                if ((txttrn_type.Text == "6" || txttrn_type.Text == "7") && Item_Code.Substring(0, 2) == "00" && Item_Code.Length > 10)

                {
                    dgv1["barcode", dgv1.CurrentCell.RowIndex].Value = Item_Code.Substring(0, 6);
                    dgv1["price", dgv1.CurrentCell.RowIndex].Value = Convert.ToDecimal( Item_Code.Substring(6, 5))/100;
                    Item_Code = dgv1["barcode", dgv1.CurrentCell.RowIndex].Value.ToString();


                }




                //sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.UNIT,h.FRACTION,s.AVG_PUR_PRICE,s.RE_ORDER,s.stock,u.unit_name from hd_ITEMMASTER h inner join unitmaster u on h.unit=u.unit_id  left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=1 and itm_cat_code=0 and h.Item_Code='" + Item_Code + "'";
                sql = "select BdescrIPTION,stock,avg_PUR_PRICE,RETAIL_PRICE,ITEM_CODE,FRACTION,UNIT,stock,wr_code,ITEM_CODE,ITEM_ID,hfraction,barcode,bdescription,r_min_profit  from QRY_barcode where wr_code =" + Gvar.wr_code + " and   flag <> 'C' AND (BARCODE='" + Item_Code + "' OR (item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + cmbwhouse.SelectedValue;

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

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = rd[6].ToString();

                           
                                if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value==null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value.ToString()=="0" )
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = rd[2].ToString();
                            

                            
                             



                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = rd[3].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = rd[10].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = rd[11].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = rd[14].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = rd[3].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = rd[7].ToString();





                            nodata = false;
                        }

                        if (rd[3] == DBNull.Value)
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
                        else
                        {


                            if (Convert.ToDecimal(rd[3].ToString()) <= 0)
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
                        }
                        if (txttrn_type.Text == "0")
                        {
                            rd.Close();
                            sql = "select unit_qty from data_entry_grid where wr_code=" + cmbwhouse.SelectedValue + " and item_code ='" + Item_Code + "' and trn_type=0 ";
                              cmd = new SqlCommand(sql, Conn);
                            rd = cmd.ExecuteReader();
                            if (rd.HasRows)
                            {
                                while (rd.Read())
                                {
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["qty"].Value = rd[0].ToString();
                                }
                            }
                        }
                    }


                }
                else
                {
                    if(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == "")
                    {

                    DialogResult result = MessageBox.Show("Invalid Item Found, Do You want to add it Now?", "Record not Found", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == "")
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["barcode"].Value;

                        //  dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["ItemCode"].Value = dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["barcode"].Value;

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["fraction"].Value = 1;

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "PCS";

                        //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = "0";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["barcode"].Value + "01";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = "1";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = "0";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = "2";

                    }
                    else
                    {

                        //MessageBox.Show("Invalid Item Found, Please check Again", "Invalid Item");

                        nodata = true;


                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = "";
                        //  dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["fraction"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unit"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = "";

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = "";
                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = "";

                        // dgv1.CurrentCell = dgv1["barcode", cur_row];
                    }
                    }

                }
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
                                object nextline=1 ;
                                if (rec_options.Rows.Count >0)
                                nextline = rec_options.Rows[0]["Auto_Next_line"];
                                decimal price = 0;
                                if (dgv1["price", cur_row].Value == null) dgv1["price", cur_row].Value = 0;
                                if (!string.IsNullOrEmpty(dgv1["price", cur_row].Value.ToString()))
                                    price = Convert.ToDecimal(dgv1["price", cur_row].Value.ToString());

                                if (nextline.Equals("1") && price > 0)
                                {
                                    if (dgv1["qty", cur_row].Value == null)
                                        dgv1["qty", cur_row].Value = 1;
                                    find_total();

                                    if (dgv1.Rows.Count> cur_row+1)
                                    dgv1.CurrentCell = dgv1["barcode", cur_row + 1];


                                }
                                else

                                if ((nextline.Equals("0") && price > 0) ||  price==0)
                                {
                                   


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
            acntrl = dgv1.Name;
        }

        private void FrmMRNEntry_Activated(object sender, EventArgs e)
        {
            try
            {
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
                dgv1.EndEdit();
                dgv1[cur_col, cur_row].Selected = false;
               // dgv1.CurrentCell.InheritedStyle.BackColor = dgv1.CurrentCell.InheritedStyle.BackColor;
                if (dgv1["barcode", cur_row].Value == null) return;
                if (!GrdLookup.Visible)
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                last_row = dgv1.CurrentCell.RowIndex;
                //string celval = Convert.ToString(dgv1["Description", cur_row].Value);
                //if (last_col > 6 && celval.Trim() == "") dgv1.CurrentCell = dgv1["Item_Code", cur_row];


                if (last_col == "qty" && dgv1["qty", last_row].Value == null && dgv1["description", last_row].Value != "")
                    dgv1["qty", last_row].Value = 1;

                if (!dgv1["barcode", cur_row].Value.Equals("999"))
                {
                    if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value == "") dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = 0;
                if (last_col == "Price" && Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value) > 0 && Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value) != Convert.ToDecimal(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["price"].Value))
                    dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
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

                        tmp = new ADODB.Recordset();

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
                        if(txttrn_type.Text=="0")
                        {
                            txttrn.Text = cmbwhouse.SelectedValue.ToString();
                           

                            txtinvno.Text = cmbwhouse.SelectedValue.ToString();

                        }

                    }

                    //ADOconn.BeginTrans();
                    decimal paidcash = 0;
                    decimal paidother = 0;
                    decimal balance = 0;


                    
                    if (Gvar.orgdup == null) Gvar.orgdup = "ORG";

                    rec = new ADODB.Recordset();
                    sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + Gvar.nyear + " and  TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text) + " AND ORG_DUP ='" + Gvar.orgdup + "' and BRN_CODE =" + Gvar.brn_code;

                    if (txttrn_type.Text == "0")
                    {
                        sql = "SELECT * FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND  INVOICE_NO = '" + txtinvno.Text.Trim() + "' and  TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

                    }


                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {

                        rec.AddNew();
                        rec.Fields["flag"].Value = "N";
                        rec.Fields["user_name"].Value = Gvar.Userid;
                        rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                        rec.Fields["TRAN_NO"].Value = Convert.ToInt32(txttrn.Text);
                        rec.Fields["CURDATE"].Value = dt1.Value;

                    }





                    
                    rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                    rec.Fields["ORG_DUP"].Value = Gvar.orgdup;
                    rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["ENTRY_TYPE"].Value = "Retail";
                    rec.Fields["CRATE"].Value = Gvar._cur_rate;
                    rec.Fields["ACCODE"].Value = txtcustomer.Text;
                    rec.Fields["ename"].Value = txtcusname.Text;
                    //rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["REMARKS"].Value = txtremarks.Text;

                    rec.Fields["Currency"].Value = Gvar._currency;
                    rec.Fields["REF_NO"].Value = txtrefno.Text;
                    rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text);
                    rec.Fields["DISC_AMT"].Value = Convert.ToDouble(txtdscamt.Text); ;
                    rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text);
                    rec.Fields["cash_paid"].Value = paidcash;
                    rec.Fields["other_paid"].Value = paidother;
                    rec.Fields["FRN_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) * Gvar._cur_rate;
                    rec.Fields["sales_code"].Value = Gvar.Userid;

                    // rec.Fields["ename"].Value = cmbproject.Text;

                    //rec.Fields["order_no"].Value = 0;
                    rec.Fields["wr_code"].Value = cmbwhouse.SelectedValue;
                    rec.Fields["NYEAR"].Value = Gvar.nyear;
                    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                    rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text); ;

                    rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text); ;

                    rec.Fields["FRN_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) * Gvar._cur_rate;
                    //txtnetamt.Text = rec.Fields["G_TOTAL"].Value.ToString();
                    rec.Update();
                    //rec.Update();

                    sql = "SELECT rec_no FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    tmp = new ADODB.Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    decimal rec_no = (decimal)tmp.Fields["REC_NO"].Value;
                    var a = 0;
                    if (txttrn_type.Text != "0")
                    {
                        sql = "DELETE FROM DATA_ENTRY_GRID WHERE  BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;
                        object ot;
                        ADOconn.Execute(sql, out ot, 1);
                    }

                    


                    //SqlCommand cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();




                    ADODB.Recordset rec1 = new ADODB.Recordset();

                    sql = "SELECT * FROM DATA_ENTRY_GRID WHERE REC_NO=1.1";
                   
                    rec1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);



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
                            if (txttrn_type.Text == "0")
                            {
                                rec1 = new ADODB.Recordset();

                                sql = "SELECT * FROM DATA_ENTRY_GRID WHERE  BRN_CODE = " + Gvar.brn_code + " AND  wr_code ='" + cmbwhouse.SelectedValue + "' and  Item_Code = '" + dgv1["ItemCode", i].Value + "' and trn_type =0";

                                rec1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                                if (rec1.RecordCount == 0)
                                {
                                    rec1.AddNew();
                                    rec1.Fields["ROWNUM"].Value = i;
                                }

                            }
                            else
                            {
                                rec1.AddNew();
                                rec1.Fields["ROWNUM"].Value = i;
                            }

                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                            
                            rec1.Fields["REC_NO"].Value = rec_no;
                           
                            rec1.Fields["Item_Code"].Value = dgv1["ItemCode", i].Value;
                            rec1.Fields["PRICE"].Value = dgv1["price", i].Value;
                            rec1.Fields["Description"].Value = dgv1["Description", i].Value;
                            rec1.Fields["QTY"].Value = dgv1["qty", i].Value;
                            rec1.Fields["price"].Value = dgv1["price", i].Value;
                            rec1.Fields["BARCODE"].Value = dgv1["barcode", i].Value;
                            rec1.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec1.Fields["UNIT"].Value = dgv1["unit", i].Value;
                            if (dgv1["cost", i].Value == null || dgv1["cost", i].Value == "")
                                dgv1["cost", i].Value = 0;
                            rec1.Fields["SALE_PUR_AMT"].Value = dgv1["cost", i].Value;
                            rec1.Fields["ITM_TOTAL"].Value = dgv1["total", i].Value;
                            rec1.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec1.Fields["BRN_CODE"].Value = Gvar.brn_code;
                            if (dgv1["proposed", i].Value==null||dgv1["proposed", i].Value=="")
                            {
                                dgv1["proposed", i].Value = dgv1["price", i].Value;

                            }

                            rec1.Fields["PROPOSE_PRICE"].Value = dgv1["proposed", i].Value;
                            object propose = rec1.Fields["PROPOSE_PRICE"].Value;
                            if (propose == "0") rec1.Fields["PROPOSE_PRICE"].Value = dgv1["price", i].Value;
                            rec1.Fields["ITEM_ID"].Value = dgv1["itemid", i].Value;
                            rec1.Fields["hfraction"].Value = dgv1["hfraction", i].Value;
                            rec1.Fields["wr_code"].Value = cmbwhouse.SelectedValue;
                            if (dgv1["disc", i].Value == null || dgv1["disc", i].Value == "")
                                dgv1["disc", i].Value = 0;
                            rec1.Fields["disc"].Value = dgv1["disc", i].Value;
                            rec1.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                            rec1.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                            //rec1.Fields["REF_NO"].Value = txtinvno.Text;
                            rec1.Fields["INVOICE_NO"].Value = txtinvno.Text;
                            // rec1.Fields["brn_code"].Value = Gvar._brn_code;

                            rec1.Fields["UNIT_QTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec1.Fields["UNIT_PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec1.Fields["FPRICE"].Value = rec1.Fields["PRICE"].Value;
                                rec1.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);

                            }
                            else
                            {
                                rec1.Fields["UNIT_PRICE"].Value = Convert.ToInt32(dgv1["price", i].Value);
                                rec1.Fields["FPRICE"].Value = rec1.Fields["PRICE"].Value;
                                rec1.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value);

                            }
                            
                            rec1.Update();


                        }

                    }

                    //if (txttrn_type.Text=="0")
                    //{
                    //    sql="select sum(qty*price) as total from  data_entry_grid  where trn_type=0 and invoice_no='" + txtinvno.Text.Trim() +"'";

                    //    tmp = new ADODB.Recordset();



                    //    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);
                    //    if(tmp.RecordCount!=0)
                    //    {
                    

                    //    }

                    //}

                    //sql="update data_entry set flag='N' where trn_type=11 and invoice_no='" + txtinvno.Text.Trim() +"'";

                    //tmp = new ADODB.Recordset();



                    //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    //cmd = new SqlCommand(sql, Conn);
                    //  cmd.ExecuteNonQuery();

                    iserror = false;

                }
                catch (SqlException ex)
                {
                    //ADOconn.RollbackTrans();
                    iserror = true;
                    lblmsg.Text = ex.Message;
                    editno();
                    btnsave.Enabled = true;
                    MessageBox.Show(ex.Message);
                }
            }
            
            catch (Exception er)
            {
                editno();
                iserror = true;
                lblmsg.Text = er.Message;
                btnsave.Enabled = true;
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

                    //If Val(options(13)) = "1" Then
                    //If Trim(txtpassword) = "" Then txtpassword = InputBox("Please Enter The Password", "Login Password")

                    //Set TMP = New ADODB.Recordset
                    //TMP.Open "SELECT userid FROM Userinfo WHERE password='" & txtpassword & "'", Sqlcon, 3, 3
                    //If TMP.RecordCount = 0 Then
                    //MsgBox "Invalid Password!", vbCritical
                    //txtpassword.SetFocus
                    //Exit Sub
                    //Else
                    //usrid = TMP(0)
                    //End If
                    //End If


                    //                If DTPicker1.Value < Date Then
                    //a = MsgBox("The selected Date is on Previous Date, Do you want to Continue?", vbYesNoCancel)
                    //If a <> 6 Then Exit Sub
                    //End If



                    //ADOconn.BeginTrans();
                    cus = new ADODB.Recordset();
                    //ADOconn.BeginTrans();

                    if (Convert.ToInt32(txttrn.Text) == 0)
                    {
                        sql = "SELECT trno FROM TRN_NO";
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        txttrn.Text = cus.Fields[0].Value.ToString();
                        sql = "SELECT top 1 * from trn_master";
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        cus.AddNew();

                    }
                    else
                    {
                        sql = "select * from trn_master1 where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + txtinvno.Text.Trim() + "'  AND NYEAR=" + Gvar.nyear + " and BRN_CODE =" + Gvar.brn_code;

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
                    cus.Fields["TOT_AMOUNT"].Value = Convert.ToDouble(txttotal.Text.Trim());
                    cus.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text.Trim());
                    cus.Fields["FNET_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text.Trim()) * Gvar._cur_rate;
                    cus.Fields["DISCOUNT"].Value = Convert.ToDouble(txtdscamt.Text.Trim()); ;
                    cus.Fields["user_ID"].Value = Gvar._Userid;
                    cus.Fields["SALE_TYPE"].Value = 0; ;
                    cus.Fields["SALES_code"].Value = cmbsalesagent.SelectedValue ;
                    cus.Fields["WR_CODE"].Value = cmbwhouse.SelectedValue;
                    cus.Fields["brn_CODE"].Value = Gvar.brn_code;
                    cus.Fields["NYEAR"].Value = Gvar.nyear;
                    cus.Fields["REMARKS"].Value = txtremarks.Text.Trim();

                   // cus.Fields["sales_code"].Value = Gvar.Userid;







                    cus.Update();


                    //CRT_TABLE:

                    sql = "INSERT INTO EDT_TRN_MASTER ([TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE) SELECT [TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE FROM TRN_MASTER1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND  trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_NO='" + txtinvno.Text.Trim() + "'";

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();


                    sql = "INSERT INTO TMP_ITM_DETAIL(ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code) SELECT ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code FROM TRN_ITM_DETAIL1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND TRN_NO=" + Convert.ToDouble(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                    if (txttrn_type.Text != "0")
                    {
                        sql = "DELETE FROM TRN_ITM_DETAIL1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                        object ot;
                        ADOconn.Execute(sql, out ot, 1);
                        //tmp = new Recordset();
                        //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    }
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
                            if (txttrn_type.Text == "0")
                            {
                                rec = new ADODB.Recordset();

                                sql = "select * from TRN_ITM_DETAIL1 where  BRN_CODE = " + Gvar.brn_code + " AND  INVOICE_NO='" + txtinvno.Text + "' and item_code='" + dgv1["ItemCode", i].Value + "'";

                                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                if (rec.RecordCount == 0) rec.AddNew();
                            }
                            else
                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());
                            {
                                rec.AddNew();
                            }
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
                            //rec.Fields["ORDER_NO"].Value = 0;

                            decimal QTY = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            rec.Fields["QTY"].Value = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                                rec.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);

                            }
                            else
                            {
                                rec.Fields["PRICE"].Value = Convert.ToDecimal(dgv1["price", i].Value);
                                rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                                rec.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value);

                            }

                            decimal PRICE = Convert.ToDecimal(rec.Fields["PRICE"].Value); ;
                            rec.Fields["discount"].Value = Convert.ToDecimal(dgv1["disc", i].Value);
                            rec.Fields["WR_CODE"].Value = cmbwhouse.SelectedValue;
                            rec.Fields["brn_code"].Value = Gvar._brn_code;
                            rec.Update();

                            sal_pur_amt = sal_pur_amt + Convert.ToDecimal(rec.Fields["SALE_PUR_AMOUNT"].Value) * Convert.ToDecimal(rec.Fields["QTY"].Value);


                            if (Convert.ToDecimal(dgv1.Rows[i].Cells["price"].Value) > 0 && dgv1.Rows[i].Cells["updsale"].Value.ToString() == "2")
                            {

                                add_newitem(i);
                            }

                            else
                            {
                                if (txttrn_type.Text == "-1" || txttrn_type.Text == "0")
                                {

                                    if (Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) > 0 && rec_options.Rows[0]["upd_sal_price"].ToString() == "1")
                                    {

                                        sql = "update barcode set retail_price = " + Convert.ToDecimal(dgv1.Rows[i].Cells["cost"].Value) + "  WHERE barcode='" + dgv1["barcode", i].Value + "'";


                                        //cmd = new SqlCommand(sql, Conn);
                                        //cmd.ExecuteNonQuery();

                                        tmp = new Recordset();
                                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    }
                                }
                            }

                           if( rec_options.Rows[0]["UPD_PUR_PRICE"].ToString()=="1")
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

                            if (txttrn_type.Text == "0")
                            {
                                decimal amt = Convert.ToDecimal(dgv1["qty", i].Value) * Convert.ToDecimal(dgv1["price", i].Value);
                                updat_accounts(dgv1["ItemCode", i].Value.ToString(), amt);
                                if (iserror)
                                {
                                    return;

                                }

                            }
                            tmp = new Recordset();
                            //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            object ST = 0;


                            if (dgv1["barcode", i].Value.ToString() != "999")
                            {

                                sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwhouse.SelectedValue + " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";

                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                                ST = 0;
                               // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                                if (!tmp.EOF) ST = tmp.Fields[0].Value;

                                sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwhouse.SelectedValue+ " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";
                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                                if (tmp.RecordCount == 0) tmp.AddNew();


                                tmp.Fields["WR_CODE"].Value = cmbwhouse.SelectedValue;
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



                    sql = "SELECT ITEM_CODE,wr_code,brn_code FROM TMP_ITM_DETAIL WHERE TRN_NO=" + Convert.ToDecimal(txttrn.Text) + " And trn_type = " + Convert.ToInt16(txttrn_type.Text) + " AND ITEM_CODE NOT IN (SELECT ITEM_CODE FROM TRN_ITM_DETAIL1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND TRN_NO=" + Convert.ToDecimal(txttrn.Text) + " And trn_type = " + Convert.ToInt16(txttrn_type.Text) + ")";

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    while (!tmp.EOF)
                    {

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwhouse.SelectedValue + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";

                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object ST1 = 0;
                        //if (rec.RecordCount != 0) ST1 = rec.Fields[0].Value;
                        
                        // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                        if (!rec.EOF) ST1 = rec.Fields[0].Value;




                        sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwhouse.SelectedValue + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";
                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (rec.RecordCount == 0) rec.AddNew();
                        rec.Fields["WR_CODE"].Value = cmbwhouse.SelectedValue;
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
                    btnsave.Enabled = true;
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception sq)
            {

                //ADOconn.RollbackTrans();
                lblmsg.Text = sq.Message;
                iserror = true;
                btnsave.Enabled = true;
                MessageBox.Show(sq.Message);
            }


        }
       



        private void updat_accounts(string docno, decimal TXTSRAMT)
        {
            try
            {
                try
                {

                   


                   // decimal TXTSRAMT = Convert.ToDecimal(txtnetamt.Text) * Gvar._cur_rate;
                    decimal OLD_AMNT = TXTSRAMT;
                    decimal paidamt = 0;




                    if (TXTSRAMT > 0)
                    {

                        object TRNBY;
                        object DRCR;
                        object DRCR1;
                        object NARR;
                        object LACC;
                        object PAYBY;
                        object[] ledgerini;
                        string acc_acs;
                        acc_acs = Program.ledger_ini(Convert.ToInt16(txttrn_type.Text), docno);
                        ledgerini = acc_acs.Split('`');
                        TRNBY = Convert.ToInt16((ledgerini[0]));
                        DRCR = ledgerini[1];
                        DRCR1 = ledgerini[2];
                        NARR = ledgerini[3];
                        LACC =   Convert.ToInt64((ledgerini[4]));
                        PAYBY = Convert.ToInt16(ledgerini[05]);

                        NARR = txtremarks.Text.Trim();
                        if (NARR == "")
                            NARR = lbltrntype.Text + " : " + docno;//+ "-" + txtcusname.Text;

                        Recordset TMP = new Recordset();
                        if (isedit)
                        {
                            sql = "INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code] FROM TRN_ACCOUNTS WHERE  BRN_CODE = " + Gvar.brn_code + " AND NYEAR=" + Gvar.nyear + " and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'";

                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        }


                        sql = "DELETE FROM TRN_ACCOUNTS WHERE  BRN_CODE = " + Gvar.brn_code + " AND NYEAR='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'";
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        sql = "DELETE FROM TRaN_ACC WHERE  BRN_CODE = " + Gvar.brn_code + " AND YEAR(CUR_DATE) ='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + docno + "'";
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

                        if (cost_ac==0||pur_ac==0||sale_ac==0||cash_ac==0||stock_ac==0)
                        {
                            MessageBox.Show("Invalid Initial Account[cost_item_ac,[CASH_SALE_AC],[CASH_PUR_AC],DEF_CASH_AC,STOCK_AC]","Invalid Account");
                        iserror=true;
                            return;
                        }


                  

                       

                        #region Inventory ACcount
                        string lnarr = "";
                        switch (Convert.ToInt16(txttrn_type.Text))
                        {
                            case 0:
                            
                                {
                                    //LACC = stock_ac;
                                     lnarr = "Opening Stock";
                                }
                                break;
                            default:
                                {
                                 lnarr = " (Product AC)";
                                break;
                                }


                        }


                         if (Convert.ToDecimal(TXTSRAMT) > 0)
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
                            acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                            //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                            acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                            acc.Fields["f_pay_amount"].Value = TXTSRAMT*Gvar._cur_rate;

                            acc.Fields["F_RATE"].Value = Gvar._cur_rate;
                            acc.Fields["pay_date"].Value = dt1.Value;
                            acc.Fields["NARRATION"].Value = NARR + lnarr;
                            acc.Fields["doc_no"].Value = docno;
                            acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;
                            acc.Fields["TRN_BY"].Value = TRNBY;
                            acc.Fields["NYEAR"].Value = dt1.Value.Year;
                            acc.Fields["cost_code"].Value = 0;
                            acc.Fields["dept_code"].Value = 0;
                            acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);

                            acc.Update();
                        }
                        #endregion Inventory ACcount


                        #region cost Item account
                        switch (Convert.ToInt16(txttrn_type.Text))
                        {
                            case 0:
                            case -1:
                            
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
                                    acc.Fields["acc_no"].Value = stock_ac;
                                    acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                                    acc.Fields["PAY_AMOUNT"].Value = TXTSRAMT;
                                    acc.Fields["f_pay_amount"].Value = TXTSRAMT * Gvar._cur_rate;
                                    acc.Fields["F_RATE"].Value = Gvar._cur_rate;
                                    acc.Fields["pay_date"].Value = dt1.Value;
                                    acc.Fields["NARRATION"].Value = NARR + " (Stock)";
                                    acc.Fields["doc_no"].Value = docno;
                                    acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;

                                    acc.Fields["TRN_BY"].Value = TRNBY;
                                    acc.Fields["cost_code"].Value = 0;
                                    acc.Fields["dept_code"].Value = 0;
                                    acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    
                                    acc.Update();

                                }
                                {
                                    //sql = "select * from trnno";
                                    //TMP = new Recordset();
                                    //TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    //trno = TMP.Fields[0].Value;
                                    //trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                    //acc.AddNew();
                                    //acc.Fields["trn_no"].Value = trno;
                                    //acc.Fields["trn_no2"].Value = trno2;
                                    //acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                    //acc.Fields["DR_CR"].Value = DRCR1;
                                    //acc.Fields["User_id"].Value = Gvar.Userid;
                                    //acc.Fields["acc_no"].Value = cost_ac;
                                    //acc.Fields["entry_no"].Value = Convert.ToDecimal(docno);
                                    //acc.Fields["PAY_AMOUNT"].Value = sal_pur_amt;
                                    //acc.Fields["f_pay_amount"].Value = sal_pur_amt * Gvar._cur_rate;
                                    //acc.Fields["F_RATE"].Value = Gvar._cur_rate;
                                    //acc.Fields["pay_date"].Value = dt1.Value;
                                    //acc.Fields["NARRATION"].Value = NARR + " (Cost)"; 
                                    //acc.Fields["doc_no"].Value = docno;
                                    //acc.Fields["PAYBY"].Value = cmbsalesagent.SelectedValue;

                                    //acc.Fields["TRN_BY"].Value = TRNBY;
                                    //acc.Fields["cost_code"].Value = 0;
                                    //acc.Fields["dept_code"].Value = 0;
                                    //acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    //acc.Update();
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
                                acc.Fields["acc_no"].Value = LACC;
                                acc.Fields["AMOUNT"].Value = TXTSRAMT;
                              
                                 acc.Fields["CURRENCY_CODE"].Value =Gvar._currency;
                                 acc.Fields["CURRENCY_RATE"].Value = Gvar._cur_rate;
                                acc.Fields["Ledger_acc"].Value = LACC;
                                acc.Fields["CUR_DATE"].Value = dt1.Value;
                                acc.Fields["Description"].Value = NARR;
                                acc.Fields["doc_no"].Value = docno;
                                acc.Fields["currency_code"].Value = Gvar._currency;
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

                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnPrint.Enabled = false;


            }
            catch
            { }
        }

        private void Txtinvno_DoubleClick(object sender, EventArgs e)
        {

            try
            {
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

                dgv1.CellEnter -= dgv1_CellEnter;
                dgv1.SelectionChanged -= dgv1_SelectionChanged;


                isedit = false;
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND NYEAR=" + dt1.Value.Year + " AND  TRN_TYPE=" + Convert.ToInt16(txttrn_type.Text) + "  AND INVOICE_NO= '" + txtinvno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)
                load_ini();
                if (rec.RecordCount > 0)
                {

                  

                    object rec_no = rec.Fields["REC_NO"].Value;


                    txtinvno.Text = rec.Fields["INVOICE_NO"].Value.ToString();
                    txttrn.Text = rec.Fields["TRAN_NO"].Value.ToString();
                    dt1.Value = Convert.ToDateTime(rec.Fields["CURDATE"].Value.ToString());

                    txtcustomer.Text = rec.Fields["ACCODE"].Value.ToString();
                    txtcusname.Text = rec.Fields["ename"].Value.ToString();

                    txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                    txtremarks.Text = rec.Fields["remarks"].Value.ToString();



                    txtdscamt.Text = rec.Fields["DISC_AMT"].Value.ToString();
                    btnsave.Enabled = true;
                    btndelete.Enabled = true;
                    btnPrint.Enabled = true;
                    lblinvstatus.Text = "***";

                     btnsave.Enabled = true;
                    lblmsg.Text = "Edit Entry......";
                    lblmsg.BackColor = Color.Green;
                    if (rec.Fields["flag"].Value.ToString() == "D")
                    {
                        btnsave.Enabled = false;
                        btndelete.Enabled = false;

                        lblinvstatus.Text = "Invoice Deleted!!!";
                        lblmsg.Text = "Invoice Deleted!!!";
                        lblmsg.BackColor = Color.Red;
                    }

                    var a = 0;

                    rec = new ADODB.Recordset();

                    sql = "SELECT  DATA_ENTRY_GRID.*,stock FROM DATA_ENTRY_GRID left join stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE  DATA_ENTRY_GRID.BRN_CODE = " + Gvar.brn_code + " AND REC_NO=" + rec_no;

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //    //dgv1.Rows.Clear();
                    //for (a=0; a< dgv1.RowCount-1;a++)
                    //{
                    //    dgv1.Rows.RemoveAt(a);
                    //    }
                    int i = 0;
                    // dgv1.Rows.Add(rec.RecordCount+1);

                    dgv1.Rows.Clear(); dgv1.Refresh();
                    

                    // foreach (DataGridViewRow row in this.dgv1.Rows)
                    while (!rec.EOF)
                    {
                        //ds2.Tables[0].Rows.Add();
                        dgv1.Rows.Add();
                        dgv1["barcode", i].Value = rec.Fields["barcode"].Value.ToString();
                        dgv1["itemcode", i].Value = rec.Fields["item_code"].Value.ToString();
                        dgv1["price", i].Value = rec.Fields["PRICE"].Value.ToString();
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();

                        dgv1["unit", i].Value = rec.Fields["Unit"].Value.ToString();
                        // dgv1["stock", i].Value = rec.Fields["stock"].Value.ToString();
                        // rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                        dgv1.Rows[i].Cells["updsale"].Value = "0";
                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                        dgv1["cost", i].Value = rec.Fields["SALE_PUR_AMT"].Value.ToString();
                        dgv1["total", i].Value = rec.Fields["ITM_TOTAL"].Value.ToString();
                        dgv1["proposed", i].Value = rec.Fields["PROPOSE_PRICE"].Value.ToString();
                        object propose = rec.Fields["PROPOSE_PRICE"].Value.ToString();
                        if (propose == "0") dgv1["proposed", i].Value = dgv1["price", i].Value;
                        dgv1["itemid", i].Value = rec.Fields["ITEM_ID"].Value.ToString();
                        dgv1["hfraction", i].Value = rec.Fields["hfraction"].Value.ToString();
                        dgv1["disc", i].Value = rec.Fields["disc"].Value.ToString();
                        i = i + 1;
                        rec.MoveNext();

                    }

                    find_total();

                    isedit = true;
                    dgv1.Columns[0].ReadOnly = false;
                    dgv1.Columns[2].ReadOnly = false;
                    dgv1.Columns[3].ReadOnly = false;
                    dgv1.Columns[4].ReadOnly = false;
                    dgv1.Columns[5].ReadOnly = false;

                    dgv1.CellEnter += dgv1_CellEnter;
                    dgv1.SelectionChanged += dgv1_SelectionChanged;
                }


                else
                {
                    dgv1.CellEnter += dgv1_CellEnter;
                    dgv1.SelectionChanged += dgv1_SelectionChanged;
                    MessageBox.Show("Invalid Invoice Number", "Invalid Invoice Entry");
                }



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

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            load_ini();
            txtcustomer.Focus();

        }

        private void load_ini()
        {
            dgv1.Rows.Clear();
            dgvpaid.Rows.Clear();
            dgvpaid.Refresh();
            txtnetamt.Text = "0";
            txtdscamt.Text = "0";
            txtbalance.Text = "0";
            dgv1.Refresh();
            txtinvno.Text = "";
            txtrefno.Text = "";
            txttrn.Text = "";
            txttotal.Text = "";
            txtcash.Text = "";
            txtacbalance.Text = "";
            isedit = false;
            dgv1.Rows.Add(2);
            dt1.Value = DateTime.Now;
            lblinvstatus.Text = "***";
            lblmsg.Text = "***";
            switch (txttrn_type.Text)
            {
                case "1":
                    {
                        txtcustomer.Text = Gvar.pur_acno;
                        txtcusname.Text = "Cash Supplier";
                        lblcustomer.Text = "Supplier";

                        break;
                    }

                case "2":
                    {
                        txtcustomer.Text = Gvar.pur_acno;
                        txtcusname.Text = "Cash Supplier";
                        lblcustomer.Text = "Supplier";

                        break;
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
                            if (Convert.IsDBNull(dgv1["price", i].Value) || dgv1["price", i].Value == "" || dgv1["price", i].Value == null) dgv1["price", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["qty", i].Value) || dgv1["qty", i].Value == "" || dgv1["qty", i].Value == null) dgv1["qty", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["disc", i].Value) || dgv1["disc", i].Value == "" || dgv1["disc", i].Value == null) dgv1["disc", i].Value = 0;

                            price = Convert.ToDouble(dgv1["price", i].Value) * Convert.ToDouble(dgv1["qty", i].Value) - Convert.ToDouble(dgv1["disc", i].Value);
                            dgv1["total", i].Value = Math.Round(price, 2);
                            tot = tot + price;
                        }
                    }
                }
                txttotal.Text = Math.Round(tot, 2).ToString();
                pay_calc_total();
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


                rep_path = Application.StartupPath + "\\reports\\Reciept_cash.rpt";

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


                // CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);
                CrRep.ReadRecords();
                //CrRep.Load();
                // CrRep.ReadRecords();

                //CrRep.Refresh();

                //if (chkprinter.Checked)
                //{


                //    CrRep.PrintToPrinter(1, true, 0, 0);
                //}
                //else
                //{
                CrRep.PrintToPrinter(1, true, 0, 0);
                return;
                FrmrepView frm = new FrmrepView();
                frm.MdiParent = this.ParentForm;

                frm.crv1.ReportSource = CrRep;
                frm.Show();
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

                sql = "select  cast(Acc_No as Varchar) as Code ,Acc_Name as Name,Acc_aName as Ar_Name from Accounts ";
                switch (txttrn_type.Text)
                {
                    case "1":
                    case "2":
                        {
                            rec = new ADODB.Recordset();

                            sql = "select  cast(Acc_No as Varchar) as Code ,Acc_Name as Name,Acc_aName as Ar_Name from Accounts where  acc_type_code=" + pr;
                            break;
                        }
                    case "6":
                    case "7":
                        {
                            rec = new ADODB.Recordset();

                            sql = "select  cast(Acc_No as Varchar) as Code ,Acc_Name as Name,Acc_aName as Ar_Name from Accounts where  acc_type_code=" + sl;
                            break;
                        }

                }







                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Customer");
                adalkp.Fill(dtlkp);

                var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
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
                if (string.IsNullOrEmpty(txtcustomer.Text.Trim()))
                {
                    //txtcustomer.Focus();
                    return;
                }
                sql = "select  Acc_No  ,Acc_Name ,Acc_aName from Accounts where  acc_no=" + Convert.ToDouble(txtcustomer.Text.ToString());

                txtcusname.Text = "";
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if (rec.RecordCount != 0)
                {
                    txtcustomer.TextChanged -= txtcustomer_TextChanged;
                    txtcustomer.Text = rec.Fields[0].Value.ToString();
                    txtcusname.Text = rec.Fields[1].Value.ToString();
                    txtcustomer.TextChanged += txtcustomer_TextChanged;
                }
                else
                {
                    MessageBox.Show("Invalid Customer");
                    return;
                }
                dgv1.Focus();

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
                    txtdscamt.Focus();
                    return;
                }

                if (nodata)
                {
                    nodata = false;
                    dgv1.CurrentCell = dgv1["barcode", cur_row];
                    dgv1["barcode", cur_row].Selected = true;
                        dgv1.BeginEdit(true);

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
                dgvpaid.Rows[0].Cells["amount"].Value = txtnetamt.Text;
                pay_calc_total();
            }
            catch (Exception ex)
            {

            }

        }

        private void pay_calc_total()
        {
            try
            {
                dgvpaid.CellValueChanged -= dgvpaid_CellValueChanged;


                dgvpaid.EndEdit();
                decimal tot = 0;
                if (txtnetamt.Text == null || txtnetamt.Text == "")
                {
                    txtnetamt.Text = "0";
                    dgvpaid.CellValueChanged += dgvpaid_CellValueChanged;
                    return;
                }

                if (txtnetamt.Text == "0")
                {
                    dgvpaid.CellValueChanged += dgvpaid_CellValueChanged;
                    return;
                }

                for (int i = 0; i <= dgvpaid.Rows.Count - 1; i++)
                {

                    if (dgvpaid["paid", i].Value == null) dgvpaid["paid", i].Value = 0;
                    if (dgvpaid["amount", i].Value == null) dgvpaid["amount", i].Value = 0;


                    tot = tot + Convert.ToDecimal(dgvpaid["paid", i].Value);
                    txtbalance.Text = Math.Round((Convert.ToDecimal(txtnetamt.Text) - tot), 2).ToString();
                    if (i < dgvpaid.Rows.Count - 1)
                        dgvpaid.Rows[i + 1].Cells["amount"].Value = 0;
                    if (Convert.ToDecimal(dgvpaid["amount", i].Value) - (Convert.ToDecimal(dgvpaid["paid", i].Value)) > 0)
                    {
                        if (i < dgvpaid.Rows.Count - 1)

                            dgvpaid.Rows[i + 1].Cells["amount"].Value = (Convert.ToDecimal(dgvpaid["amount", i].Value)) - (Convert.ToDecimal(dgvpaid["paid", i].Value));

                    }


                }
                if (txtcash.Text == "") txtcash.Text = "0";
                txtbalance.Text = Math.Round(Convert.ToDecimal(txtnetamt.Text) - tot, 2).ToString();
                dgvpaid.CellValueChanged += dgvpaid_CellValueChanged;

            }
            catch (Exception ex)
            {
                dgvpaid.CellValueChanged += dgvpaid_CellValueChanged;
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

        private void dgvpaid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvpaid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvpaid.CurrentCell.ColumnIndex != 1) return;
            pay_calc_total();
        }

        private void dgvpaid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if(lstpaid.Visible)
                //{
                //    dgvpaid.CurrentCell = dgvpaid["paidby",dgvpaid.CurrentCell.RowIndex];
                //    return;
                //}




                if (dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value == null) dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value = "";
                if (dgvpaid.CurrentCell.ColumnIndex == dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].ColumnIndex && Convert.ToDecimal(dgvpaid["paid", dgvpaid.CurrentCell.RowIndex].Value) > 0 && dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value == "")
                {

                    var cellRectangle = dgvpaid.GetCellDisplayRectangle(dgvpaid.CurrentCell.ColumnIndex, dgvpaid.CurrentCell.RowIndex, true);



                    lstpaid.Top = dgvpaid.Top + cellRectangle.Top + cellRectangle.Height;

                    if (dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value.ToString() != "")
                        lstpaid.SelectedItem = dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value.ToString(); ;
                    lstpaid.Width = dgvpaid.Columns[2].Width;
                    lstpaid.Width = cellRectangle.Left;
                    lstpaid.Visible = true;

                }
            }

            catch (Exception ex)
            {
            }

        }

        private void dgvpaid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Enter:


                        if (lstpaid.Visible)
                        {

                            if (dgvpaid.CurrentCell.ColumnIndex == 2)
                            {
                                int lkprow = 0;




                                set_dgvpaid_cell();

                                return;
                                //e.Handled = true;
                                //this.dgvpaid.CurrentCell = this.dgvpaid[dgvpaid.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }


                        }
                        break;
                    case Keys.Right:
                        if (dgvpaid.CurrentCell.ColumnIndex == 3)
                            txtcash.Focus();

                        break;

                    case Keys.ControlKey:
                        var cellRectangle = dgvpaid.GetCellDisplayRectangle(dgvpaid.CurrentCell.ColumnIndex, dgvpaid.CurrentCell.RowIndex, true);



                        lstpaid.Top = dgvpaid.Top + cellRectangle.Top + cellRectangle.Height;
                        if (dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value.ToString() != "")
                            lstpaid.SelectedItem = dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value.ToString(); ;
                        lstpaid.Width = dgvpaid.Columns[2].Width;
                        lstpaid.Width = cellRectangle.Left;
                        lstpaid.Visible = true;

                        e.Handled = true;
                        break;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvpaid_Enter(object sender, EventArgs e)
        {
            try
            {
                acntrl = "dgvpaid";

                dgvpaid.CurrentCell = dgvpaid["paid", dgvpaid.CurrentCell.RowIndex];
            }
            catch
            { }
        }

        private void lstpaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)

                try
                {
                    set_dgvpaid_cell();

                    return;
                }
                catch
                {

                }
        }

        private void lstpaid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                set_dgvpaid_cell();
                return;
            }
            catch
            {

            }
        }


        private void set_dgvpaid_cell()
        {
            try
            {
                dgvpaid.BeginEdit(false);
                dgvpaid["paidby", dgvpaid.CurrentCell.RowIndex].Value = lstpaid.Text;
                dgvpaid["paidac", dgvpaid.CurrentCell.RowIndex].Value = lstpaid.SelectedValue.ToString();
                dgvpaid.EndEdit();

                lstpaid.Visible = false;
                dgvpaid.CurrentCell = dgvpaid["amount", dgvpaid.CurrentCell.RowIndex + 1];
                dgvpaid.Focus();
            }
            catch
            {
            }
        }

        private void txtcash_Enter(object sender, EventArgs e)
        {
            acntrl = "txtcash";
            txtcash.SelectAll();
            txtcash.BackColor = Color.LightYellow;
        }

        private void txtdscamt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
                txtcash.Focus();
        }

        private void txtcash_TextChanged(object sender, EventArgs e)
        {

            if (txtcash.Text == "") txtcash.Text = "0";
            if (txtbalance.Text == "") txtbalance.Text = "0";

            txtchange.Text = (Convert.ToDecimal(txtcash.Text) - Convert.ToDecimal(txtbalance.Text)).ToString();
        }

        private void txtcash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                dgvpaid.Focus();

            if (e.KeyValue == 13)
            {
                btnsave.Focus();



            }


        }

        private void dgvpaid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtdscamt_Leave(object sender, EventArgs e)
        {
            txtdscamt.BackColor = Color.DeepSkyBlue;
        }

        private void txtdscamt_Enter(object sender, EventArgs e)
        {
            if (txtnetamt.Text == "")
            {
                dgv1.Focus();
                return;
            }
            txtdscamt.BackColor = Color.LightYellow;
        }

        private void txtcash_Leave(object sender, EventArgs e)
        {
            txtcash.BackColor = Color.DeepSkyBlue;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip_Enter(object sender, EventArgs e)
        {

        }

        private void toolStrip_Validated(object sender, EventArgs e)
        {

        }

        private void toolStrip_RegionChanged(object sender, EventArgs e)
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
                dgv1.CurrentCell = dgv1["barcode", 0];
                dgv1.Focus();
                btnsave.Enabled = true;
                lblmsg.Text = "New Entry......";
                lblmsg.BackColor = Color.Green;
                dgv1.Columns[0].ReadOnly = false;
                dgv1.Columns[2].ReadOnly = false;
                dgv1.Columns[3].ReadOnly = false;
                dgv1.Columns[4].ReadOnly = false;
                dgv1.Columns[5].ReadOnly = false;
                lblinvstatus.Text = "***";
                btnsave.Enabled = true;
                btnPrint.Enabled = true;
                isedit = false;




            }
            catch
            {

            }
            //string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pos.txt");
            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(myTempFile))
            //{
            //    sw.WriteLine("Your error message");
            //}
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
                    dgvpaid.BackgroundColor = colorDlg.Color;
                    dgvpaid.AlternatingRowsDefaultCellStyle.BackColor = colorDlg.Color;
                    dgvpaid.DefaultCellStyle.BackColor = colorDlg.Color;
                    dgvpaid.Refresh();
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
                 

                if (txtcustomer.Text == "")
                {
                    MessageBox.Show("Invalid Product Account Code ", "Invalid Product Account Code  ");
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

                

              
                Application.DoEvents();
                
                lblmsg.Text = "Please Wait.....";
               



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

                sql = "select Inv_no from trn_master where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + txtinvno.Text.Trim() + "'  AND NYEAR=" + Gvar.nyear + " and BRN_CODE =" + Gvar.brn_code;


                cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (isedit)
                {
                    if (cus.RecordCount == 0)
                    {
                        DialogResult result = MessageBox.Show("This Invoice Number not found for Update, Do You want Add It Now?", "Invalid Record", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            iserror = true;
                            btnsave.Enabled = true;
                           // ADOconn.RollbackTrans();
                            return;
                        }
                    }
                }
                else
                {
                    if (cus.RecordCount > 0 && txttrn_type.Text!="0")
                    {
                        MessageBox.Show("This Invoice Number Already Exist", "Invalid Record");
                        //ADOconn.RollbackTrans();
                        btnsave.Enabled = true;
                        return;
                    }

                }

                if (Gvar.orgdup == null) Gvar.orgdup = "ORG";

                iserror = false;
                ADOconn.BeginTrans();
                lblmsg.Text = "Please Wait Saving......";
                lblmsg.BackColor = Color.Green;
                SAVE_DATAENTRY();
                if (!iserror)
                {
                    save_data();

                    if (!iserror )
                    {
                       if( txttrn_type.Text != "0")
                        updat_accounts(txtinvno.Text,Convert.ToDecimal(txtnetamt.Text));
                    }
                    else
                    {
                        btnsave.Enabled = true;
                        ADOconn.RollbackTrans();
                        return;

                    }

                    if (iserror)
                    {
                        btnsave.Enabled = true;
                        ADOconn.RollbackTrans();
                        lblmsg.Text = "Error!!!! : Record Not Saved,, Please check and Try again !!";
                        lblmsg.BackColor = Color.Red;
                        return;
                    }

                    ADOconn.CommitTrans();
                    lblmsg.Text = "Record Saved Successfully!!!";
                    lblmsg.BackColor = Color.Green;
                    btnsave.Enabled = false;
                    dgv1.Columns[0].ReadOnly = true;
                    dgv1.Columns[2].ReadOnly = true;
                    dgv1.Columns[3].ReadOnly = true;
                    dgv1.Columns[4].ReadOnly = true;
                    dgv1.Columns[5].ReadOnly = true;
                    isedit = false;



                }
                else
                {
                    btnsave.Enabled = true;
                    ADOconn.RollbackTrans();
                    lblmsg.Text = "Error!!!! : Record Not Saved,, Please check and Try again !!";
                    lblmsg.BackColor = Color.Red;
                }
                editno();
                string myTempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pos.txt");
                System.IO.File.Delete(myTempFile);
            }
            catch (SqlException ex)
            {
                btnsave.Enabled = true;
                MessageBox.Show(ex.Message);
                lblmsg.Text = ex.Message;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
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

        private void txtbalance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtchange.Text = Math.Round(Convert.ToDecimal(txtcash.Text) - Convert.ToDecimal(txtbalance.Text), 2).ToString();
                // txtchange.Text = (Convert.ToDecimal(txtcash.Text) - Convert.ToDecimal(txtbalance.Text)).ToString();
            }


            catch
            {

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
                SQL = SQL + "  Values ('" + dgv1["barcode",i].Value + "','Admin','" + cmbwhouse.SelectedValue +"')";
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
                    find_credit();
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
                txtacbalance.Text = "";
                string sql;
                if (txtcusname.Text == "") return;
                sql = "select cr_amount,dr_amount from trn_acc_sum where acc_no =" + Convert.ToDecimal(txtcustomer.Text);
                decimal dr = 0;
                decimal cr = 0;

               ADODB.Recordset tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                if (!tmp.EOF )
                                {
                                    if (tmp.Fields[0].Value != DBNull.Value) cr = Convert.ToDecimal(tmp.Fields[0].Value);
                                        if (tmp.Fields[1].Value != DBNull.Value) dr = Convert.ToDecimal( tmp.Fields[1].Value);
                                                
                                }

                                txtacbalance.Text = ( dr-cr).ToString();


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
                               sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE = " + cmbwhouse.SelectedValue + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);

                               int ST = 0;
                               if (tmp.RecordCount > 0)
                                   ST = Convert.ToInt16(tmp.Fields[0].Value.ToString());



                               sql = "SELECT * FROM WR_STOCK_MASTER WHERE   WR_CODE = " + cmbwhouse.SelectedValue + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

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
                    dgvpaid.BackgroundColor = color;
                    dgvpaid.AlternatingRowsDefaultCellStyle.BackColor = color;
                    dgvpaid.DefaultCellStyle.BackColor = color;
                    dgvpaid.Refresh();
                    //Gvar._defaultcolor = color;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void txtinvno_Leave(object sender, EventArgs e)
        {
            if (!btnsave.Enabled)
            search_mrn();
        }
    }
}








    

