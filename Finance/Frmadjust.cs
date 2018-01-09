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
    public partial class Frmadjust : FinOrgForm
    {

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView projdv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();

        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();
        DataSet ds2 = new DataSet();
        decimal sal_pur_amt;
        Color bg_color;
        DataTable rec_options = new DataTable();
        string acntrl;
        bool isini;
        bool isedit;
        bool isdirty;
        string sql;
        bool fnd;
        bool iserror;
        bool issearch;
        int cur_row;
        int dblclk_row;
        int fcol;
        string last_col;
        string cur_col;
        string EXCLUDE_ITM_cAT;
        int last_row;
        decimal vat_pcnt;
        Boolean nodata;


        public Frmadjust()
        {
            try
            {


                InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
                load_form();
                load_ini();
                cmbwarehouse.Focus();
                


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

                if (Gvar.SuperUserid != 1) chkprintview.Visible = false;

                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                rec_options = new DataTable("rec_options");
                adalkp.Fill(rec_options);
                object a = rec_options.Rows[0]["auto_next_line"];

               


                EXCLUDE_ITM_cAT = "-0";
                EXCLUDE_ITM_cAT = rec_options.Rows[0]["exclude_itm_cat"].ToString();

                if (string.IsNullOrEmpty(rec_options.Rows[0]["exclude_itm_cat"].ToString()))
                     EXCLUDE_ITM_cAT = "-0";

               //color = (Color) rec_options.Rows[0]["bg_color"].ToString()
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


                sql = "SELECT  WR_CODE,WR_NAME FROM WRHOUSE_MASTER ORDER BY WR_CODE";
                SqlDataAdapter sales = new SqlDataAdapter(sql, Conn);
                DataTable dtsales = new DataTable("WHOUSE");
                sales.Fill(dtsales);
                cmbwarehouse.DisplayMember = "WR_NAME";
                cmbwarehouse.ValueMember = "WR_CODE";

                cmbwarehouse.DataSource = dtsales;
                cmbwarehouse.SelectedIndex = 0;
                dt1.Value = DateTime.Now;

               

                if(bg_color!=null)
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
                txt2.HeaderText = "changes";
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

                DataGridViewTextBoxColumn txt18 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt18);
                DataGridViewTextBoxColumn txt19 = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(txt19);
                DataGridViewTextBoxColumn unitid = new DataGridViewTextBoxColumn();
                dgv1.Columns.Add(unitid);
                isini = true;
                dgv1.Refresh();
                dgv1.Columns[0].HeaderText = "Item Code";
                dgv1.Columns[1].HeaderText = "Description";
                dgv1.Columns[2].HeaderText = "Unit";
                dgv1.Columns[3].HeaderText = "Actual Stock";
                dgv1.Columns[6].HeaderText = "Changes";
                dgv1.Columns[5].HeaderText = "Disc.";
                dgv1.Columns[7].HeaderText = "Cost";
                dgv1.Columns[4].HeaderText = "Remarks";


                dgv1.Columns[8].HeaderText = "Total";
                dgv1.Columns[9].HeaderText = "Stock";
                dgv1.Columns[10].HeaderText = "Fraction";
                dgv1.Columns[11].HeaderText = "Re-order";
                dgv1.Columns[19].HeaderText = "VAT";
                dgv1.Columns[21].HeaderText = "unitid";

                dgv1.Columns[0].Name = "barcode";
                dgv1.Columns[1].Name = "Description";

                dgv1.Columns[2].Name = "unit";
                dgv1.Columns[3].Name = "qty";
                dgv1.Columns[7].Name = "cost";
                dgv1.Columns[5].Name = "disc";
                dgv1.Columns[6].Name = "changes";
                dgv1.Columns[4].Name = "remarks";



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
                dgv1.Columns[19].Name = "vat";
                dgv1.Columns[20].Name = "vat%";
                dgv1.Columns[21].Name = "unitid";
                dgv1.Columns[5].Visible = false;
                //dgv1.Columns[4].Visible = false;
                dgv1.Columns[6].Visible = true;
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
                dgv1.Columns[19].Visible = false;
                dgv1.Columns[20].Visible = false;
                dgv1.Columns[21].Visible = false;
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
                dgv1.Columns[6].Width = 80;
                dgv1.Columns[5].Width = 60;
                //dgv1.Columns[6].Width = 60;
                dgv1.Columns[4].Width = 200;
                dgv1.Columns[9].Width = 80;
                dgv1.Columns[9].Width = 100;
                txttrn_type.Text = Gvar.trntype.ToString();
                //dgv1.Columns[7].DefaultCellStyle.BackColor = Color.Beige;
               // dgv1.Columns[7].InheritedStyle.BackColor = Color.Beige;
               // Style.ApplyStyle(this.dataGridView1.Columns[1].DefaultCellStyle);

                dgv1.RowHeadersWidth = 60;


                Conn.Close();
                Conn.Open();
               

               
                
                
            }




            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void get_invno()
        {
            try
            {

                if (ADOconn.State == 0)
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

                set_grdlookup();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void set_grdlookup()
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
                Conn.Close();





                string crite = "";
                object ITM = dgv1["barcode", cur_row].Value;
                if (ITM == null) ITM = "";
                if (ITM != "")
                {
                   // crite = "h.DESCRIPTION like '" + ITM.ToString().Trim() + "%' or h.ITEM_CODE like '" + ITM.ToString().Trim() + "%' OR PART_NO like '" + ITM.ToString().Trim() + "%'";
                   // crite = "(h.DESCRIPTION like '" + ITM.ToString().Trim() + "%' or h.ITEM_CODE like '" + ITM.ToString().Trim() + "%' or h.BARCODE like '" + ITM.ToString().Trim() + "%' )";
                }

                //a = InStr(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), " ")

                //If a > 0 Then
                //ITM = Left(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), a - 1)
                //crite = "(DESCRIPTION like '" & Trim(ITM) & "%' or ITEM_CODE like '" & Trim(ITM) & "%' OR PART_NO like '" & Trim(ITM) & "%')"
                //ITM = Right(Trim(myGrid1.TextMatrix(myGrid1.row, 1)), Len(Trim(myGrid1.TextMatrix(myGrid1.row, 1))) - a)
                //crite = crite & " AND DESCRIPTION LIKE '%" & ITM & "%'"
                //End If

                string sql = "";
                if (crite != "")
                {
                    sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM BARCODE as h  INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID  INNER JOIN OPTIONS ON TRNTYPE= " + txttrn_type.Text + "   where ITM_cAT_CODE NOT IN (" + EXCLUDE_ITM_cAT + " ) AND " + crite;
                    //sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT FROM BARCODE as h INNER JOIN OPTIONS ON TRNTYPE= " + txttrn_type.Text + "   where ITM_cAT_CODE NOT IN (" + EXCLUDE_ITM_cAT + " ) ";
                }
                else
                {
                    sql = "SELECT BARCODE,DESCRIPTION AS DESCR,UNIT_NAME FROM BARCODE as h INNER JOIN UNITMASTER AS U ON H.UNIT=U.UNIT_ID INNER JOIN OPTIONS ON TRNTYPE= " + txttrn_type.Text + "   where ITM_cAT_CODE NOT IN (" + EXCLUDE_ITM_cAT + ") ";
                }


                int ttype = 0;
                switch (txttrn_type.Text)
                {
                    case "3":

                    case "4":

                    case "8":

                    case "9":
                        {
                            if (txttrn_type.Text == "3") ttype = 6;
                            if (txttrn_type.Text == "4") ttype = 7;
                            if (txttrn_type.Text == "8") ttype = 1;
                            if (txttrn_type.Text == "9") ttype = 2;

                            if (txtrefno.Text.Trim() != "")
                            {

                                //sql = "select BARCODE,DESCRIPTION AS DESCR,UNIT,unit_price from data_entry_grid inner join data_entry as e  where   Invoice_no='" + txtrefno.Text.Trim() + "' and trn_type = " + ttype;
                            }
                            else
                            {
                              //  sql = "select BARCODE,DESCRIPTION AS DESCR,UNIT,unit_price from data_entry_grid where  trn_type = " + ttype;

                            }



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
                                 dgv1["changes", dblclk_row].Value = 0;
                               
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
                    
                 }


                if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgv1" && !GrdLookup.Visible)
                {
                    // SendKeys.Send("{Tab}");
                    //string a = dgv1["remarks", cur_row].Value.ToString();
                    keyData = Keys.Tab;
                    if (dgv1.CurrentCell == dgv1["remarks", cur_row])
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


                if (msg.WParam.ToInt32() == (int)Keys.ControlKey && (acntrl == "dgv1" ||  acntrl=="textBox1") && !GrdLookup.Visible && cur_col == "barcode")
                {
                    // SendKeys.Send("{Tab}");

                    dgv1_DoubleClick(null, null);
                    if(dgv1.CurrentCell.Value!=null)
                    textBox1.Text = dgv1.CurrentCell.Value.ToString();
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = textBox1.Text.Length;
                    return true;
                }


               

              
                    
                //}

                

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

               




                //sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.UNIT,h.FRACTION,s.AVG_PUR_PRICE,s.RE_ORDER,s.stock,u.unit_name from hd_ITEMMASTER h inner join unitmaster u on h.unit=u.unit_id  left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=1 and itm_cat_code=0 and h.Item_Code='" + Item_Code + "'";
                
               // sql = "select BdescrIPTION,stock,avg_PUR_PRICE,RETAIL_PRICE,ITEM_CODE,FRACTION,UNIT,stock,wr_code,ITEM_CODE,ITEM_ID,hfraction,barcode,bdescription,r_min_profit,vat_percent  from QRY_barcode where  wr_code =" +  cmbwarehouse.SelectedValue + " and   flag <> 'C' AND (BARCODE='" + Item_Code + "' OR (item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + cmbwarehouse.SelectedValue;
                sql = "select BdescrIPTION,stock,avg_PUR_PRICE,RETAIL_PRICE,ITEM_CODE,FRACTION,UNIT_" + Gvar.lang_letter + "NAME,stock,wr_code,ITEM_CODE,ITEM_ID,hfraction,barcode,bdescription,r_min_profit,vat_percent,UNIT_NAME,unit_id   from QRY_barcode INNER JOIN UNITMASTER AS U ON UNIT=U.UNIT_ID where  wr_code =" + cmbwarehouse.SelectedValue + " and   flag <> 'C' AND (BARCODE='" + Item_Code + "' OR (item_CODE='" + Item_Code + "' and MAIN_ID=1) OR ALIAS_NAME='" + Item_Code + "')"; // AND WR_CODE=" + Gvar.wr_code;

                if (unit != "")
                {
                    string sql1 = "";

                    sql1 = sql.Substring(0, sql.IndexOf("where") - 1);
                    sql = sql1 + " where wr_code =" + cmbwarehouse.SelectedValue + " and flag <> 'C' AND (ITeM_code = '" + dgv1["ITEMCODE", dgv1.CurrentCell.RowIndex].Value.ToString() + "' or barcode = '" + dgv1["ITEMCODE", dgv1.CurrentCell.RowIndex].Value.ToString() + "') and unit ='" + unit + "'";


                }

                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 0;
                //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat"].Value = "";
                //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat%"].Value = "";
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

                            //if ((txttrn_type.Text == "6" || txttrn_type.Text == "7") && Item_Code.Substring(0, 2) == "00" )
                             
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["cost"].Value = rd[2].ToString();

                            
                            
                             



                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["proposed"].Value = rd[3].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Itemid"].Value = rd[10].ToString();

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["hfraction"].Value = rd[11].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["minprofit"].Value = rd[14].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["vat%"].Value = rd[15].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["unitid"].Value = rd["unit_id"].ToString();
                           
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["stock"].Value = rd[7].ToString();

                        
                               // btnsave.Enabled = true;
                            btndelete.Enabled = true;
                            btnPrint.Enabled = true;
                           
                            nodata = false;
                        }

                        if (rd[3] == DBNull.Value)
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
                        else
                        {


                            if (Convert.ToDecimal(rd[3].ToString()) <= 0)
                                dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["updsale"].Value = 1;
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

                        //dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["changes"].Value = "";
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

                        dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["changes"].Value = "";
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
                cmd.Cancel();
                int ttype=0;


              

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
                                object nextline = rec_options.Rows[0]["Auto_Next_line"];
                                decimal price = 0;
                                if (dgv1["changes", cur_row].Value == null) dgv1["changes", cur_row].Value = 0;
                                if (!string.IsNullOrEmpty(dgv1["changes", cur_row].Value.ToString()))
                                    price = Convert.ToDecimal(dgv1["changes", cur_row].Value.ToString());

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

                            switch (txttrn_type.Text)
                            {
                                case "3":

                                case "4":

                                case "8":

                                case "9":
                                    {
                                        dgv1.Columns["unit"].ReadOnly = true;
                                        dgv1.CurrentCell = dgv1["qty", cur_row];
                                    }
                                    break;
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
                        dgv1["changes", dblclk_row].Value = "0";
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


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


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

                if ( dgv1["stock", last_row].Value == null && dgv1["description", last_row].Value != "")
                    dgv1["stock", last_row].Value = 0;

                if ( e.ColumnIndex == dgv1["qty", e.RowIndex].ColumnIndex )
                {
                    if (Convert.ToDouble( dgv1["qty", e.RowIndex].Value) <  Convert.ToDouble( dgv1["stock", e.RowIndex].Value) && txttrn_type.Text=="-2")
                    {
                        MessageBox.Show("Invaldi Quantuty Entry, Please correct The qty");
                         dgv1["qty", last_row].Value = 0;
                         //dgv1.CurrentCell = dgv1["qty", last_row];
                            return;
                    }
                    if (Convert.ToDouble( dgv1["qty", e.RowIndex].Value) >  Convert.ToDouble( dgv1["stock", e.RowIndex].Value) && txttrn_type.Text=="17")
                    {
                        MessageBox.Show("Invaldi Quantuty Entry, Please correct The qty");
                        dgv1["qty", last_row].Value = 0;
                        //dgv1.CurrentCell = dgv1["qty", last_row];
                            return;
                    }

                    if(txttrn_type.Text=="-2")
                    dgv1["changes", last_row].Value = Convert.ToDouble(dgv1["qty", e.RowIndex].Value) - Convert.ToDouble(dgv1["stock", e.RowIndex].Value);
                    else
                        dgv1["changes", last_row].Value = Convert.ToDouble(dgv1["stock", e.RowIndex].Value) - Convert.ToDouble(dgv1["qty", e.RowIndex].Value);


                    find_total();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND  NYEAR=" + Gvar.nyear + " and  TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text) + " and BRN_CODE =" + Gvar.brn_code;

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
                    rec.Fields["ENTRY_TYPE"].Value = "Adjust";
                    rec.Fields["CRATE"].Value = 1;
                    rec.Fields["ACCODE"].Value = 0;
                    rec.Fields["ename"].Value = lbltrntype.Text ;
                    rec.Fields["REMARKS"].Value = txtremarks.Text;

                    tmp = new ADODB.Recordset();
                    sql = "sELECT  INV_PREFEX  froM TRN_TYPE WHERE TRN_CODE = " + txttrn_type.Text;
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (tmp.RecordCount > 0)
                        rec.Fields["INV_PREFEX"].Value = tmp.Fields[0].Value;

                    rec.Fields["Currency"].Value = Gvar._currency ;
                    rec.Fields["REF_NO"].Value = txtrefno.Text;
                    
                    rec.Fields["sales_code"].Value = Gvar.Userid;


                    rec.Fields["G_TOTAL"].Value = Convert.ToDecimal(txtnetamt.Text);
                    rec.Fields["DISC_AMT"].Value = 0;
                    rec.Fields["NET_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text);
                    rec.Fields["cash_paid"].Value =0;
                    rec.Fields["other_paid"].Value = Convert.ToDecimal(txtnetamt.Text);
                    rec.Fields["FRN_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text);
                    // rec.Fields["ename"].Value = cmbproject.Text;

                    //rec.Fields["order_no"].Value = 0;
                    rec.Fields["wr_code"].Value =cmbwarehouse.SelectedValue;
                    rec.Fields["NYEAR"].Value = dt1.Value.Year;

                    rec.Fields["VAT_PERCENT"].Value = 0 ;
                    rec.Fields["VAT_AMOUNT"].Value = 0 ; ;
                   

                    rec.Update();

                    sql = "SELECT rec_no FROM DATA_ENTRY WHERE  BRN_CODE = " + Gvar.brn_code + " AND INVOICE_NO = '" + txtinvno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
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


                            //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                            rec.AddNew();
                            rec.Fields["REC_NO"].Value = rec_no;
                            rec.Fields["ROWNUM"].Value = i;
                            rec.Fields["Item_Code"].Value = dgv1["ItemCode", i].Value;
                           
                            rec.Fields["Description"].Value = dgv1["Description", i].Value;
                            rec.Fields["QTY"].Value = dgv1["changes", i].Value;
                            
                            
                                dgv1["proposed", i].Value =0;



                                rec.Fields["price"].Value = Convert.ToDecimal(dgv1["cost", i].Value);
                            rec.Fields["BARCODE"].Value = dgv1["barcode", i].Value;
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["UNIT"].Value = dgv1["unitid", i].Value;
                            if (dgv1["cost", i].Value == null || dgv1["cost", i].Value == "")
                                dgv1["cost", i].Value = 0;
                            rec.Fields["SALE_PUR_AMT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) * Convert.ToDecimal(1);
                            rec.Fields["ITM_TOTAL"].Value = dgv1["total", i].Value;
                            rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["BRN_CODE"].Value = Gvar.brn_code;


                            rec.Fields["PROPOSE_PRICE"].Value = dgv1["proposed", i].Value;
                            object propose = rec.Fields["PROPOSE_PRICE"].Value;
                            if (propose == "0") rec.Fields["PROPOSE_PRICE"].Value = dgv1["cost", i].Value;
                            rec.Fields["ITEM_ID"].Value = dgv1["itemid", i].Value;
                            rec.Fields["hfraction"].Value = dgv1["hfraction", i].Value;
                            rec.Fields["wr_code"].Value = cmbwarehouse.SelectedValue;
                           
                                dgv1["disc", i].Value = 0;
                                rec.Fields["disc"].Value = 0; 
                            rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                            rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                            //rec.Fields["REF_NO"].Value = txtinvno.Text;
                            rec.Fields["INVOICE_NO"].Value = txtinvno.Text;
                            // rec.Fields["brn_code"].Value = Gvar._brn_code;

                            rec.Fields["UNIT_QTY"].Value = Convert.ToDecimal(dgv1["changes", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                           


                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["UNIT_PRICE"].Value = (Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value)) ;
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["cost", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(rec.Fields["UNIT_TRN_AMOUNT"].Value) * Convert.ToDecimal(1);
                            }
                            else
                            {
                                rec.Fields["UNIT_PRICE"].Value = Convert.ToDecimal(dgv1["cost", i].Value);
                                rec.Fields["FPRICE"].Value = Convert.ToDecimal(dgv1["cost", i].Value);
                                rec.Fields["UNIT_TRN_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) ;

                            }

                             rec.Fields["vat_amount"].Value = 0;
                             rec.Fields["vat_percent"].Value = 0;

                            
                            rec.Update();


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
                        sql = "select * from trn_master1 where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + txtinvno.Text.Trim() + "'  AND NYEAR=" + Gvar.nyear + " and BRN_CODE =" + Gvar.brn_code;

                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (cus.RecordCount == 0) cus.AddNew();

                    }
                    cus.Fields["trn_no"].Value = Convert.ToInt32(txttrn.Text);
                    cus.Fields["INV_NO"].Value = txtinvno.Text.Trim();
                    cus.Fields["REF_NO"].Value = txtrefno.Text.Trim();
                    cus.Fields["DATE_TIME"].Value = dt1.Value;
                    cus.Fields["cus_code"].Value = 0;
                    cus.Fields["cus_name"].Value = lbltrntype.Text;
                    cus.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                    cus.Fields["TOT_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text); ;
                    cus.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txtnetamt.Text.Trim());
                    cus.Fields["FNET_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text.Trim()) ;
                    cus.Fields["DISCOUNT"].Value = 0 ;
                    cus.Fields["user_ID"].Value = Gvar._Userid;
                    cus.Fields["SALE_TYPE"].Value = 0; ;
                    cus.Fields["SALES_code"].Value = cmbwarehouse.SelectedValue ;
                    cus.Fields["WR_CODE"].Value = cmbwarehouse.SelectedValue;
                    cus.Fields["brn_CODE"].Value = Gvar.brn_code;
                    cus.Fields["NYEAR"].Value = Gvar.nyear;
                    cus.Fields["REMARKS"].Value = txtremarks.Text.Trim();
                    cus.Fields["CURRENCY"].Value = Gvar._currency;






                    cus.Update();


                    //CRT_TABLE:

                    sql = "INSERT INTO EDT_TRN_MASTER ([TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE) SELECT [TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE],SALES_CODE FROM TRN_MASTER1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_NO='" + txtinvno.Text.Trim() + "'";

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //cmd = new SqlCommand(sql, Conn);
                    //cmd.ExecuteNonQuery();


                    sql = "INSERT INTO TMP_ITM_DETAIL(ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code) SELECT ITEM_CODE,TRN_TYPE,TRN_NO,wr_code,brn_code FROM TRN_ITM_DETAIL1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND TRN_NO=" + Convert.ToDouble(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                    sql = "DELETE FROM TRN_ITM_DETAIL1 WHERE  BRN_CODE = " + Gvar.brn_code + " AND TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

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

                    for (i = 0; i < dgv1.RowCount; i++)
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
                            rec.Fields["UNIT_QTY"].Value = dgv1["changes", i].Value;
                            rec.Fields["UNIT_PRICE"].Value = dgv1["cost", i].Value;
                           
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

                            decimal QTY = Convert.ToDecimal(dgv1["changes", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            rec.Fields["QTY"].Value = Convert.ToDecimal(dgv1["changes", i].Value) * Convert.ToDecimal(dgv1["fraction", i].Value);
                            if (Convert.ToDecimal(dgv1["fraction", i].Value) > 0)
                            {
                                rec.Fields["price"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);
                                rec.Fields["FPRICE"].Value = rec.Fields["price"].Value;
                               
                                    rec.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) / Convert.ToDecimal(dgv1["FRACTION", i].Value);


                                
                               

                            }
                            else
                            {
                                rec.Fields["price"].Value = Convert.ToDecimal(dgv1["cost", i].Value);
                                rec.Fields["FPRICE"].Value = rec.Fields["cost"].Value;
                               
                                    rec.Fields["SALE_PUR_AMOUNT"].Value = Convert.ToDecimal(dgv1["cost", i].Value) ;


                              

                            }

                            decimal PRICE = Convert.ToDecimal(rec.Fields["price"].Value); ;
                            rec.Fields["discount"].Value = Convert.ToDecimal(dgv1["disc", i].Value);
                            rec.Fields["WR_CODE"].Value = cmbwarehouse.SelectedValue;
                            rec.Fields["brn_code"].Value = Gvar.brn_code;
                            rec.Update();

                            sal_pur_amt = sal_pur_amt + Convert.ToDecimal(rec.Fields["SALE_PUR_AMOUNT"].Value) * Convert.ToDecimal(rec.Fields["QTY"].Value);


                         
                               


                           
                            double qty = 0;
                           
                            tmp = new Recordset();
                            //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            object ST = 0;


                            if (dgv1["barcode", i].Value.ToString() != "999")
                            {

                                sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwarehouse.SelectedValue + " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";

                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                                ST = 0;
                               // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                                if (!tmp.EOF) ST = tmp.Fields[0].Value;

                                sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwarehouse.SelectedValue + " AND ITEM_CODE='" + dgv1["itemcode", i].Value + "'";
                                tmp = new Recordset();
                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                                if (tmp.RecordCount == 0) tmp.AddNew();


                                tmp.Fields["WR_CODE"].Value = cmbwarehouse.SelectedValue;
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

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwarehouse.SelectedValue + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";

                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object ST1 = 0;
                        //if (rec.RecordCount != 0) ST1 = rec.Fields[0].Value;
                        
                        // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                        if (!rec.EOF) ST1 = rec.Fields[0].Value;




                        sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwarehouse.SelectedValue + " AND ITEM_CODE='" + tmp.Fields["item_code"].Value + "'";
                        rec = new Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (rec.RecordCount == 0) rec.AddNew();
                        rec.Fields["WR_CODE"].Value = cmbwarehouse.SelectedValue;
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
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

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
                        object vat_ac = 0;
                        object exp_ac = 0;
                        acc_acs = Program.ledger_ini(Convert.ToInt16(txttrn_type.Text), txtinvno.Text);
                        ledgerini = acc_acs.Split('`');
                        TRNBY = Convert.ToInt16((ledgerini[0]));
                        DRCR = ledgerini[1];
                        DRCR1 = ledgerini[2];
                        NARR = ledgerini[3];
                        LACC = Convert.ToInt64((ledgerini[4]));
                        PAYBY = Convert.ToInt64(ledgerini[05]);
                        vat_ac = 0;
                        exp_ac = 0;

                        NARR = txtremarks.Text.Trim();
                        if (NARR == "")
                            NARR = lbltrntype.Text + " : " + txtinvno.Text;//+ "-" + txtcusname.Text;

                        Recordset TMP = new Recordset();
                        if (isedit)
                        {
                            sql = "INSERT INTO TRN_ACCOUNTS_UPD([TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code]) SELECT [TRN_NO],[DR_CR],[ACC_NO],[PAY_AMOUNT],[PAY_DATE],[NARRATION],[DOC_NO],[PAYBY],[TRN_BY],[F_PAY_AMOUNT],[F_RATE],[user_id],[sno],[BRN_CODE],[Cost_Code],[Dept_Code] FROM TRN_ACCOUNTS WHERE NYEAR=" + Gvar.nyear + " and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;

                            TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        }


                        sql = "DELETE FROM TRN_ACCOUNTS WHERE NYEAR='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        sql = "DELETE FROM TRaN_ACC WHERE YEAR(CUR_DATE) ='" + Gvar.nyear + "' and TRN_by=" + TRNBY + " and DOC_NO='" + txtinvno.Text.Trim() + "'  and BRN_CODE =" + Gvar.brn_code;
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                        sql = "select * from trnno";
                        TMP = new Recordset();
                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        object trno = TMP.Fields[0].Value;
                        object trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                        object accno = 0;



                       
                        Recordset acc = new Recordset();
                        
                        long cost_ac = 0;
                        long pur_ac = 0;
                        long sale_ac = 0;
                        long cash_ac = 0;
                        long stock_ac = 0;
                        


                        sql = "select cost_item_ac,[CASH_SALE_AC],[CASH_PUR_AC],DEF_CASH_AC,STOCK_AC,EXP_ACC from ac_options WHERE  ac_options.ID =1";
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
                            //if (TMP.Fields[5].Value != DBNull.Value)
                            //    vat_ac = Convert.ToInt64(TMP.Fields[5].Value);
                            //if (TMP.Fields[6].Value != DBNull.Value)
                            //    exp_ac = Convert.ToInt64(TMP.Fields[6].Value);

                        }

                        if (cost_ac==0||pur_ac==0||sale_ac==0||cash_ac==0||stock_ac==0)
                        {
                            MessageBox.Show("Invalid Initial Account[cost_item_ac,[CASH_SALE_AC],[CASH_PUR_AC],DEF_CASH_AC,STOCK_AC]","Invalid Account");
                        iserror=true;
                            return;
                        }


                        # region Credit account
                        

                            

                        #endregion credit acoun
                                    #region Inventory ACcount
                                    string lnarr = "";
                                    switch (Convert.ToInt16(txttrn_type.Text))
                                    {
                                        

                                        case -2:
                                            {
                                                //LACC = stock_ac;
                                                lnarr = " Stock Adjustment Addition " ;
                                            }
                                            break;
                                        default:
                                            {
                                                lnarr = " Stock Adjustment Deletion ";
                                                break;
                                            }


                                    }


                                    if (Convert.ToDecimal(txtnetamt.Text) > 0)
                                    {
                                         sql = "SELECT TOP 1 * FROM TRN_ACCOUNTS";
                        acc = new Recordset();
                        acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                                        sql = "select * from trnno";
                                        TMP = new Recordset();
                                        TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                        trno2 = TMP.Fields[0].Value;
                                        //trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                        acc.AddNew();
                                        acc.Fields["trn_no"].Value = trno2;
                                        acc.Fields["trn_no2"].Value = trno;
                                        acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                        acc.Fields["DR_CR"].Value = DRCR;
                                        acc.Fields["User_id"].Value = Gvar.Userid;
                                        acc.Fields["acc_no"].Value = LACC; // IF SALES THEN LACC ELSE STOCK_AC
                                        //acc.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);
                                        //acc.Fields["f_pay_amount"].Value = Convert.ToDecimal(txtnetamt.Text) - (Convert.ToDecimal(txtnetamt.Text) - sal_pur_amt);

                                        double pay_amt = Math.Round(Convert.ToDouble(txtnetamt.Text),2) ;
                                        acc.Fields["PAY_AMOUNT"].Value = pay_amt; ;
                                        acc.Fields["f_pay_amount"].Value = pay_amt * Convert.ToDouble(1);
                                        acc.Fields["F_RATE"].Value = 1;
                                        acc.Fields["currency"].Value = Gvar._currency;

                                        acc.Fields["pay_date"].Value = dt1.Value;
                                        acc.Fields["NARRATION"].Value = NARR + lnarr;
                                        acc.Fields["doc_no"].Value = txtinvno.Text;
                                        acc.Fields["PAYBY"].Value = cmbwarehouse.SelectedValue;
                                        acc.Fields["TRN_BY"].Value = TRNBY;
                                        acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                        acc.Fields["cost_code"].Value = cmbwarehouse.SelectedValue;
                                        acc.Fields["dept_code"].Value = 0;
                                        acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                                        acc.Fields["trn_type"].Value = txttrn_type.Text;
                                        acc.Update();
                                    }
                                    #endregion Inventory ACcount
                                 

                    

                    

                    


                        #region Inventory account
                      
                                    sql = "select * from trnno";
                                    TMP = new Recordset();
                                    TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                    trno2 = TMP.Fields[0].Value;
                                    //trno2 = Convert.ToDouble(TMP.Fields[0].Value) + 1;
                                    acc.AddNew();
                                    acc.Fields["trn_no"].Value = trno2;
                                    acc.Fields["trn_no2"].Value = trno;
                                    acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                    //if (Convert.ToInt16(txttrn_type.Text) == 3 || Convert.ToInt16(txttrn_type.Text) == 4)
                                    acc.Fields["DR_CR"].Value = DRCR1;
                                    //else
                                    //{
                                    //    acc.Fields["DR_CR"].Value = DRCR;

                                    //}
                                    acc.Fields["User_id"].Value = Gvar.Userid;
                                    acc.Fields["acc_no"].Value = stock_ac;
                                    acc.Fields["entry_no"].Value = Convert.ToDecimal(txtinvno.Text);
                                   
                                    
                                    acc.Fields["PAY_AMOUNT"].Value = sal_pur_amt * Convert.ToDecimal(1); ;
                                    acc.Fields["f_pay_amount"].Value = sal_pur_amt * Convert.ToDecimal(1);
                                    acc.Fields["F_RATE"].Value = 1;
                                    acc.Fields["currency"].Value = Gvar._currency;




                                    acc.Fields["pay_date"].Value = dt1.Value;
                                    acc.Fields["NARRATION"].Value = NARR + " (Stock)";
                                    acc.Fields["doc_no"].Value = txtinvno.Text;
                                    acc.Fields["PAYBY"].Value = cmbwarehouse.SelectedValue;

                                    acc.Fields["TRN_BY"].Value = TRNBY;
                                    acc.Fields["cost_code"].Value = cmbwarehouse.SelectedValue;
                                    acc.Fields["dept_code"].Value = 0;
                                    acc.Fields["NYEAR"].Value = dt1.Value.Year;
                                    acc.Fields["trn_type"].Value = txttrn_type.Text;
                                    acc.Update();
                               
                               
                        #endregion Cost Item Account

                        
                                sql = "SELECT TOP 1 * FROM TRAN_ACC";
                                acc = new Recordset();
                                acc.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                                acc.AddNew();
                                // acc.Fields["trn_no"].Value = trno;
                                acc.Fields["BRN_CODE"].Value = Gvar.brn_code;
                                acc.Fields["PaidTo_Acc"].Value = 0;
                                //acc.Fields["User"].Value = Gvar.Userid;
                                acc.Fields["acc_no"].Value = 0;
                                acc.Fields["AMOUNT"].Value = (Convert.ToDecimal(txtnetamt.Text)*Convert.ToDecimal(1));
                                acc.Fields["currency_rate"].Value = 1;

                                acc.Fields["currency_CODE"].Value = Gvar._currency;
                                acc.Fields["Ledger_acc"].Value = LACC;
                                acc.Fields["CUR_DATE"].Value = dt1.Value;
                                acc.Fields["Description"].Value = NARR;
                                acc.Fields["doc_no"].Value = txtinvno.Text;
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
                txtinvno.Text = "";
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME,   CURDATE from  DATA_ENTRY WHERE  brn_code = " + Gvar.brn_code + " and  TRN_TYPE=" + Convert.ToInt16(txttrn_type.Text) + "  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

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


            if (txtinvno.Text == "") return;
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
                    

                    txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                    txtremarks.Text = rec.Fields["remarks"].Value.ToString();



                    btnsave.Enabled = false;
                    btndelete.Enabled = true;
                    btnPrint.Enabled = true;
                    lblinvstatus.Text = "***";

                    
                    lblmsg.Text = "Edit Entry......";
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

                    rec = new ADODB.Recordset();

                    sql = "SELECT  DATA_ENTRY_GRID.*,stock,unit_id FROM DATA_ENTRY_GRID inner join unitmaster as u on DATA_ENTRY_GRID.unit=unit_id  left join stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no;

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
                        dgv1["changes", i].Value = rec.Fields["FPRICE"].Value.ToString();
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();

                        dgv1["unit", i].Value = rec.Fields["Unit"].Value.ToString();
                        dgv1["unitid", i].Value = rec.Fields["Unit_id"].Value.ToString();
                        // dgv1["stock", i].Value = rec.Fields["stock"].Value.ToString();
                        // rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                        dgv1.Rows[i].Cells["updsale"].Value = "0";
                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                        dgv1["cost", i].Value = rec.Fields["SALE_PUR_AMT"].Value.ToString();
                        dgv1["total", i].Value = Convert.ToDecimal(rec.Fields["QTY"].Value.ToString()) * Convert.ToDecimal(rec.Fields["FPRICE"].Value.ToString());
                       

                      
                      
                        dgv1["proposed", i].Value = rec.Fields["PROPOSE_PRICE"].Value.ToString();
                        object propose = rec.Fields["PROPOSE_PRICE"].Value.ToString();
                        if (propose == "0") dgv1["proposed", i].Value = dgv1["changes", i].Value;
                        dgv1["itemid", i].Value = rec.Fields["ITEM_ID"].Value.ToString();
                        dgv1["hfraction", i].Value = rec.Fields["hfraction"].Value.ToString();
                        dgv1["disc", i].Value = rec.Fields["disc"].Value.ToString();
                        i = i + 1;
                        rec.MoveNext();

                    }
                    if (txttrn_type.Text == "22")
                        dgv1.Columns["orderno"].Visible = false;

                    find_total();



                   


                   

                    pay_calc_total();

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

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            load_ini();
            txtcustomer.Focus();

        }

        private void load_ini()
        {
            cur_col = "barcode";
            cur_row = 0;
            last_col = "barcode";
            last_row = 0;
           
            dgv1.Rows.Clear();
          
            txtnetamt.Text = "0";
           
            dgv1.Refresh();
            txtinvno.Text = "";
            txtrefno.Text = "";
            txttrn.Text = "";
            
            txtcustomer.Text = "";
            
            isedit = false;
            dgv1.Rows.Add(2);
            dt1.Value = DateTime.Now;
            lblinvstatus.Text = "***";
            lblmsg.Text = "***";
            sql = "SELECT VAT_PERCENT FROM AC_OPTIONS WHERE  ac_options.ID =1";
            SqlCommand cmd1 = new SqlCommand(sql, Conn);
            SqlDataReader rd = cmd1.ExecuteReader();
            vat_pcnt = 0;
            while (rd.Read())
            {
                vat_pcnt = Convert.ToDecimal(rd[0].ToString());
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
                    if (dgv1["changes", i].Value == null) dgv1["changes", i].Value = 0;
                    if (!Convert.IsDBNull(dgv1["barcode", i].Value)) //dgv1["Item_Code", i].Value = 0;
                    {
                        if (dgv1["barcode", i].Value != null)
                        {
                            
                            if (Convert.IsDBNull(dgv1["changes", i].Value) || dgv1["changes", i].Value == "" || dgv1["changes", i].Value == null) dgv1["changes", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["qty", i].Value) || dgv1["qty", i].Value == "" || dgv1["qty", i].Value == null) dgv1["qty", i].Value = 0;
                            if (Convert.IsDBNull(dgv1["disc", i].Value) || dgv1["disc", i].Value == "" || dgv1["disc", i].Value == null) dgv1["disc", i].Value = 0;
                            
                            price = Convert.ToDouble(dgv1["changes", i].Value) * Convert.ToDouble(dgv1["cost", i].Value);
                            dgv1["total", i].Value = Math.Round(price, 2);
                            
                            tot = tot + price;
                        }
                    }
                }
                isdirty = true;
                txtnetamt.Text = Math.Round(tot, 2).ToString();

                switch (txttrn_type.Text)
                {
                    case "3":
                    case "4":
                    case "8":
                    case "9":
                        {
                           // txtcash.Text = txttotal.Text;
                        }
                        break;
                }

                
               
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


                rep_path = Application.StartupPath + "\\reports\\Reciept.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = " {DATA_ENTRY.BRN_CODE} = " + Gvar.brn_code + " AND {DATA_ENTRY.INVOICE_NO} = '" + txtinvno.Text.Trim() + "' and {DATA_ENTRY.TRN_TYPE} =" + Convert.ToInt32(txttrn_type.Text);


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

                if (dgv1.Columns[cur_col].ReadOnly)
                {


                    //dgv1.CellEnter -= dgv1_CellEnter;
                    //dgv1.SelectionChanged -= dgv1_SelectionChanged;
                    //dgv1.CellLeave -= dgv1_CellLeave;
                    dgv1.CurrentCell = dgv1[dgv1.CurrentCell.ColumnIndex+1, cur_row];
                   // dgv1.CellEnter += dgv1_CellEnter;
                   // dgv1.SelectionChanged += dgv1_SelectionChanged;
                }


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
                    if (!string.IsNullOrEmpty(dgv1["changes", cur_row].Value.ToString()))
                        price = Convert.ToDecimal(dgv1["changes", cur_row].Value.ToString());

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
                
                
                pay_calc_total();
                find_vat();
            }
            catch (Exception ex)
            {

            }

        }

        private void pay_calc_total()
        {
            try
            {
                
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

        private void dgvpaid_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }


        

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnsave.Enabled && txtnetamt.Text.Trim() != "" && txtnetamt.Text.Trim() != "0")
                {
                    DialogResult result = MessageBox.Show("This Changes Not be Saved, Do you want clear the screen ?", "Record not Saved", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {

                        return;
                    }
                }

                load_ini();
                dgv1.CurrentCell = dgv1["barcode", 0];
               txtcustomer.Focus();
                btnsave.Enabled = true;
                lblmsg.Text = "New Entry......";
                lblmsg.BackColor = Color.LightGray;
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
                            c.BackColor = colorDlg.Color;

                        }
                        catch
                        {

                        }


                    }

                    this.BackColor = colorDlg.Color;
                    dgv1.BackgroundColor = colorDlg.Color;
                    ;
                    Gvar._defaultcolor = colorDlg.Color.ToString();

                    Color color = colorDlg.Color;
                    string colorName = color.Name;

                    sql = "update options set bg_color='" + colorName + "' where trntype ='" + txttrn_type.Text + "'";
                    object a1;
                    //ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                    ADOconn.Execute(sql, out a1, -1);

                }


            }
            catch(Exception ex)
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
                 

                //if (txtcustomer.Text == "" )
                //{
                //    MessageBox.Show("Invalid GL Account Code ", "Invalid Account Code  ");
                //    txtcustomer.Focus();
                //    return;
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
                  
                    string celval = Convert.ToString(dgv1["ItemCode", i].Value);

                    if (celval.Trim() != "")
                    {
                        itemfound = true;


                        if (dgv1["Description", i].Value == null || dgv1["Qty", i].Value == null || dgv1["changes", i].Value.ToString()=="0")
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

                           // ADOconn.RollbackTrans();
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
                SAVE_DATAENTRY();
                if (!iserror)
                {
                    save_data();

                    if (!iserror)
                    {
                        updat_accounts();
                    }
                    else
                    {
                        ADOconn.RollbackTrans();
                        return;

                    }

                    if (iserror)
                    {
                        ADOconn.RollbackTrans();
                        lblmsg.Text = "Error!!!! : Record Not Saved,, Please check and Try again !!";
                        lblmsg.BackColor = Color.Red;
                        isdirty = false;
                        btnsave.Enabled = true;
                        return;
                    }
                    isdirty = false;
                    ADOconn.CommitTrans();
                    lblmsg.Text = "Record Saved Successfully!!!";
                    lblmsg.BackColor = Color.LightGray;
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
                    ADOconn.RollbackTrans();
                    btnsave.Enabled = true;
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
            if(isdirty)
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
                        search_mrn();
                        txtrefno.Focus();
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

     

        private void cmbtrntype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txttrn_type.Text = cmbtrntype.SelectedValue.ToString();
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
                               sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE = " + cmbwarehouse.SelectedValue + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

                               tmp = new Recordset();
                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);

                               int ST = 0;
                               if (tmp.RecordCount > 0)
                                   ST = Convert.ToInt16(tmp.Fields[0].Value.ToString());



                               sql = "SELECT * FROM WR_STOCK_MASTER WHERE   WR_CODE = " + cmbwarehouse.SelectedValue + " and  item_code ='" + rec.Fields["item_code"].Value.ToString() + "'";

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

                
            }
            this.BackColor = color;
            dgv1.BackgroundColor = color;
        }
        catch (Exception ex)
        {

        }

    }

   
    private void txtinvno_Enter(object sender, EventArgs e)
    {
        acntrl = "txtinvno";
    }

    private void txtcustomer_Enter(object sender, EventArgs e)
    {
        acntrl = "txtcustomer";
    }

    private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgv1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
        switch (dgv1.Columns[e.ColumnIndex].Name)
        {
            case "barcode":
               // dgv1_DoubleClick(null, null);
                dgv1.EndEdit();
                break;

        }

    }

    private void FrmService_KeyUp(object sender, KeyEventArgs e)
    {
        if (dgv1.CurrentCell == null) return;
        if (dgv1.CurrentCell.IsInEditMode && cur_col == "barcode" && !GrdLookup.Visible && acntrl == "dgv1")
        {
            set_grdlookup();
            dgv1.EndEdit();
            dgv1.BeginEdit(false);
            if (dgv1.CurrentCell.Value != null)
                textBox1.Text = dgv1.CurrentCell.Value.ToString();
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;

        }
    }

    

    private void txtvatpcnt_TextChanged(object sender, EventArgs e)
    {
        if (isini) return;
       find_vat();

    }

   private void find_vat()
   {
             // txtvatamt.Text = Math.Round((Convert.ToDecimal(txtnetamt.Text) * Convert.ToDecimal(txtvatpcnt.Text) / 100), 2).ToString();
   }

   private void txtvatamt_TextChanged(object sender, EventArgs e)
   {
       find_vat();
   }

    
        
    
      
    
    }
}








    

