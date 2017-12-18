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
    public partial class Frmadjust : Form
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

        DataTable itemdt = new DataTable();
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
        int last_row;
        int adj_type;
        int adj_trntype;
        public Frmadjust()
        {
            InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
            load_form();
            
        }

        private void FrmMRNEntry_Load(object sender, EventArgs e)
        {
            int trn = Gvar._trntype;
            txttrn_type.Text = trn.ToString();

            if (Gvar.invno != "0")
            {
                Txtadjustno.Text = Gvar.invno;
                search_mrn();
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



                //ada1.Fill(ds, "proj_master");
                //cmbproject.DisplayMember = "Proj_name";
                //cmbproject.ValueMember = "proj_code";
                //cmbproject.DataSource = ds.Tables[0];


                //sql = "select Cost_code, Cost_name from Cost_master";

                //SqlDataAdapter cost = new SqlDataAdapter(sql, Conn);
                /////ada.TableMappings.Add("Table", "Leaders");

                //DataSet dscost = new DataSet();



                //cost.Fill(dscost, "Cost_master");
                //cmbcost.DisplayMember = "Cost_name";
                //cmbcost.ValueMember = "Cost_code";
                //cmbcost.DataSource = dscost.Tables[0];


                sql = "select wr_code, wr_name from wrhouse_master";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet dswh = new DataSet();
                this.ada.Fill(dswh, "wrhouse_master");
                cmbwh.DisplayMember = "wr_name";
                cmbwh.ValueMember = "WR_CODE";
                cmbwh.DataSource = dswh.Tables[0];

                

                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                ds2.AcceptChanges();

                //set the table as the datasource for the grid in order to show that data in the grid

                // dgv1.Columns[0].DataPropertyName = "LEADER_NAME";

                sql = "select [Col1],[Col2] FROM [Grid_Master] where col='1'";
                //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";

                SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                DataTable dt = new DataTable("Grid_Master");
                dt.AcceptChanges();
                ada2.Fill(ds2, "Grid_Master");
                dgv1.Visible = true;
                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;


                //dgv1.DataSource = ds2.Tables[0];
                // dgv1.Refresh();

                DataGridViewTextBoxColumn Col1 = new DataGridViewTextBoxColumn();
                Col1.HeaderText = "Col1";
                Col1.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col1);
                DataGridViewTextBoxColumn Col2 = new DataGridViewTextBoxColumn();
                Col2.HeaderText = "Col2";
                Col2.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col2); ;

                DataGridViewTextBoxColumn qty = new DataGridViewTextBoxColumn();
                qty.HeaderText = "ActualStock";
                qty.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(qty);


                DataGridViewTextBoxColumn unt = new DataGridViewTextBoxColumn();
                unt.HeaderText = "Unit";
                unt.ReadOnly = true;
                unt.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(unt);

               // DataGridViewComboBoxColumn cmbbox = new DataGridViewComboBoxColumn();
               // //cmbbox.DataSource = projdv;
               // //cmbbox.DataPropertyName = "Unit";

               //// cmbbox.DefaultCellStyle = dgv1.DefaultCellStyle;

               // cmbbox.HeaderText = "Unit";
               // cmbbox.ValueMember = "Unit_code";
               // cmbbox.DisplayMember = "Unit_name";
               // cmbbox.AutoComplete = true;
               // //cmbbox.AutoCompleteMode.SuggestAppend;



               // dgv1.Columns.Add(cmbbox);
                DataGridViewTextBoxColumn txt6 = new DataGridViewTextBoxColumn();
                txt6.HeaderText = "remarks";
                txt6.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt6);

                DataGridViewTextBoxColumn txt1 = new DataGridViewTextBoxColumn();
                txt1.HeaderText = "NewStock";
                txt1.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt1.Visible = true;
                dgv1.Columns.Add(txt1);

                DataGridViewTextBoxColumn bcode = new DataGridViewTextBoxColumn();
                bcode.HeaderText = "Bud.Code";
                bcode.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(bcode);
                         

                



                DataGridViewTextBoxColumn txt2 = new DataGridViewTextBoxColumn();
                txt2.HeaderText = "Price";
                txt2.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt2);

                DataGridViewTextBoxColumn tot = new DataGridViewTextBoxColumn();
                tot.HeaderText = "Total";
                tot.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(tot);

                DataGridViewTextBoxColumn txt3 = new DataGridViewTextBoxColumn();
                txt3.HeaderText = "Stock";
                txt3.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt3.ReadOnly = true;
                dgv1.Columns.Add(txt3);

                DataGridViewTextBoxColumn frac = new DataGridViewTextBoxColumn();
                frac.HeaderText = "Fraction";
                frac.DefaultCellStyle = dgv1.DefaultCellStyle;
                frac.Visible = false;
                dgv1.Columns.Add(frac);

                DataGridViewTextBoxColumn txt5 = new DataGridViewTextBoxColumn();
                txt5.HeaderText = "Reorder";
                txt5.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt5.Visible = false;
                dgv1.Columns.Add(txt5);

                DataGridViewTextBoxColumn unt1 = new DataGridViewTextBoxColumn();
                unt1.HeaderText = "OUnit";
                unt1.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(unt1);
                unt1.Visible = false;


                DataGridViewTextBoxColumn tstk = new DataGridViewTextBoxColumn();
                tstk.HeaderText = "TStock";
                tstk.DefaultCellStyle = dgv1.DefaultCellStyle;
                tstk.ReadOnly = true;
                dgv1.Columns.Add(tstk);

                dgv1.Refresh();
                dgv1.Columns[0].HeaderText = "Item Code";
                dgv1.Columns[1].HeaderText = "Description";
                dgv1.Columns[2].HeaderText = "ActualStock";
                dgv1.Columns[3].HeaderText = "Unit";
                
                dgv1.Columns[4].HeaderText = "Remarks";
                dgv1.Columns[5].HeaderText = "Change(+/-)";
                dgv1.Columns[6].HeaderText = "Bdg.Code";


               
                dgv1.Columns[7].HeaderText = "Price";
                dgv1.Columns[8].HeaderText = "Total";
                dgv1.Columns[9].HeaderText = "WStock";
                dgv1.Columns[10].HeaderText = "Fraction";
                dgv1.Columns[11].HeaderText = "Re-order";
                dgv1.Columns[13].HeaderText = "TStock";


                dgv1.Columns[0].Name = "Item_Code";
                dgv1.Columns[1].Name = "Description";

                dgv1.Columns[2].Name = "Actualstock";
                dgv1.Columns[3].Name = "Unit";
                
                dgv1.Columns[4].Name = "remarks";
                dgv1.Columns[5].Name = "Change";
                dgv1.Columns[6].Name = "Bdg.code";
                
                dgv1.Columns[7].Name = "Price";
               // dgv1.Columns[7].Name = "Unit";
               
                dgv1.Columns[8].Name = "Total";
                dgv1.Columns[9].Name = "stock";
                dgv1.Columns[10].Name = "fraction";
                dgv1.Columns[11].Name = "Reorder";
                dgv1.Columns[12].Name = "OUnit";
                dgv1.Columns[13].Name = "TStock";

                dgv1.Columns[10].Visible = false;
               dgv1.Columns[6].Visible = false;
               dgv1.Columns[8].Visible = false;
                dgv1.Columns[11].Visible = false;
                dgv1.Columns[1].ReadOnly = true;
                dgv1.Columns[2].ReadOnly = false;
                //dgv1.Columns[4].ReadOnly = true;
                dgv1.Columns[5].ReadOnly = true;
                dgv1.Columns[9].ReadOnly = true;
                dgv1.Columns[7].ReadOnly = true;
                dgv1.Columns[8].ReadOnly = true;
                dgv1.Columns[10].ReadOnly = true;

                int xcol = 85;
                dgv1.Columns[0].Width = 170;
                dgv1.Columns[1].Width = 250;
                dgv1.Columns[2].Width = 120;
                dgv1.Columns[3].Width = xcol;
                //dgv1.Columns[4].Width = xcol;
                dgv1.Columns[4].Width = 250;

                dgv1.Columns[5].Width = xcol+30;
                dgv1.Columns[7].Width = xcol;
                dgv1.Columns[8].Width = xcol;

                //dgv1.Columns["leader_no"].Visible = false;
                //if (Gvar._SuperUserid != 1) dgv1.Columns["Approved"].ReadOnly = true;

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
                case Keys.ShiftKey:
                    dgv1_DoubleClick(sender, null);
                    e.Handled = true;
                    break;


            }

        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.CurrentCell == null) dgv1.CurrentCell = dgv1["Item_Code",cur_row];
                if (dgv1.CurrentCell == dgv1["Item_Code",cur_row])
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,round(W.stock,2) AS WStock,round(S.STOCK,2) AS TStock from hd_itemmaster  left join wr_stock_master as w on (hd_itemmaster.Item_Code=w.Item_Code and  w.wr_code= " + cmbwh.SelectedValue + ") left join stock_master as s on (hd_itemmaster.Item_Code=s.Item_Code)"  , Conn);
                    //SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,W.stock AS WStock,S.STOCK AS TStock from hd_itemmaster  left join wr_stock_master as w on (hd_itemmaster.Item_Code=w.Item_Code) left join stock_master as s on (hd_itemmaster.Item_Code=s.Item_Code)", Conn);
                    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                    DataTable dtlkp = new DataTable("hd_itemmaster");
                    adalkp.Fill(dtlkp);
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
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
                    
                }


                //if (dgv1.CurrentCell == dgv1["site", cur_row])
                //{
                //    dblclk_row = dgv1.CurrentCell.RowIndex;
                //    Conn.Close();
                //    Conn.Open();
                //    SqlCommand cmd = new SqlCommand("select site_code,site_name from site_master where proj_code=" +cmbproject.SelectedValue, Conn);

                //    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                //    DataTable dtlkp = new DataTable("site_master");
                //    adalkp.Fill(dtlkp);

                //    //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                //    // dataGrid1.DataContext = dt.DefaultView;
                //    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                //    GrdLookup.Left = textBox1.Left;
                //    GrdLookup.Top = textBox1.Top + textBox1.Height;

                //    GrdLookup.Tag = "site";
                //    dv.Table = dtlkp;
                //    GrdLookup.DataSource = dv;
                //    GrdLookup.Columns[0].Width = 170;
                //    GrdLookup.Columns[1].Width = 300;
                //    GrdLookup.Visible = true;
                //    textBox1.Focus();


                //}


                //if (dgv1.CurrentCell == dgv1["Bdg.Code", cur_row])
                //{
                //    dblclk_row = dgv1.CurrentCell.RowIndex;
                //    Conn.Close();
                //    Conn.Open();
                //    SqlCommand cmd = new SqlCommand("select Budg_code,Description from Budg_master", Conn);

                //    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                //    DataTable dtlkp = new DataTable("Budg_master");
                //    adalkp.Fill(dtlkp);

                //    //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                //    // dataGrid1.DataContext = dt.DefaultView;
                //    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                //    GrdLookup.Left = textBox1.Left;
                //    GrdLookup.Top = textBox1.Top + textBox1.Height;

                //    GrdLookup.Tag = "Bdg.Code";
                //    dv.Table = dtlkp;
                //    GrdLookup.DataSource = dv;
                //    GrdLookup.Columns[0].Width = 170;
                //    GrdLookup.Columns[1].Width = 300;
                //    GrdLookup.Visible = true;
                //    textBox1.Focus();


                //}



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
                    }

                    //e.Handled = true;

                    break;

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

                if (GrdLookup.Visible == true && last_col == "Item_Code")
                {
                    //dgv1.EndEdit();


                    //dgv1.BeginEdit(false);
                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "Item_Code LIKE  '%" + txt + "%' OR description LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "Item_Code <> '0'";


                }



                //if (GrdLookup.Visible == true && last_col == "site")
                //{
                //    //dgv1.EndEdit();


                //    //dgv1.BeginEdit(false);
                //    string txt = textBox1.Text.Trim();
                //    if (txt != "")
                //    {
                //        dv.RowFilter = "site_code LIKE  '" + txt + "%' OR site_name LIKE '" + txt + "%'";
                //    }
                //    else
                //        dv.RowFilter = "site_code <> '0'";


                //}


                //if (GrdLookup.Visible == true && last_col == "Bdg.Code")
                //{
                //    //dgv1.EndEdit();


                //    //dgv1.BeginEdit(false);
                //    string txt = textBox1.Text.Trim();
                //    if (txt != "")
                //    {
                //        dv.RowFilter = "Budg_code LIKE  '" + txt + "%' OR Description LIKE '" + txt + "%'";
                //    }
                //    else
                //        dv.RowFilter = "Budg_code <> '0'";


                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:


                    if (GrdLookup.Visible)
                    {

                        if (last_col == "Item_Code")
                        {
                            int lkprow = 0;


                            lkprow = GrdLookup.CurrentCell.RowIndex;

                            dgv1.CurrentCell = dgv1["actualstock", last_row];
                            dgv1.BeginEdit(false);
                            dgv1["Item_Code", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                            dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                            dgv1.EndEdit();
                            search_data(dgv1["Item_Code", dblclk_row].Value.ToString());
                            GrdLookup.Visible = false;
                            dgv1.Focus();
                            
                            return;
                            //e.Handled = true;
                            //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        }

                        //if (last_col == "Site")
                        //{
                        //    int lkprow = 0;


                        //    lkprow = GrdLookup.CurrentCell.RowIndex;
                        //    dgv1.CurrentCell = dgv1["site",cur_row];

                        //    dgv1.BeginEdit(false);
                        //    dgv1["site", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        //    //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        //    dgv1.EndEdit();

                        //    GrdLookup.Visible = false;
                        //    dgv1.Focus();
                        //    return;
                        //    //e.Handled = true;
                        //    //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        //}

                        //if (last_col == "Bdg.code")
                        //{
                        //    int lkprow = 0;


                        //    lkprow = GrdLookup.CurrentCell.RowIndex;
                        //    dgv1.CurrentCell = dgv1["Bdg.Code", cur_row];

                        //    dgv1.BeginEdit(false);
                        //    dgv1["Bdg.Code", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        //    //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        //    dgv1.EndEdit();

                        //    GrdLookup.Visible = false;
                        //    dgv1.Focus();
                        //    return;
                        //    //e.Handled = true;
                        //    //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        //}

                    }
                    break;


                case Keys.ShiftKey:
                    dgv1.CurrentCell = dgv1.Rows[0].Cells[dgv1.FirstDisplayedCell.ColumnIndex];
                    dgv1_DoubleClick(sender, null);
                    e.Handled = true;
                    break;


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
            if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgv1" && !GrdLookup.Visible)
            {
                // SendKeys.Send("{Tab}");

                keyData = Keys.Tab;
                if (dgv1.CurrentCell == dgv1["Unit", cur_row] && lstunit.Visible)
                {
                    dgv1["Unit", cur_row].Value = lstunit.Items[lstunit.SelectedIndex].ToString();
                }
                   


                if (dgv1.CurrentCell == dgv1["Remarks", cur_row])
                {
                    if (dgv1.CurrentCell.RowIndex < dgv1.RowCount-1)
                    {
                        dgv1.CurrentCell = dgv1["Item_Code", cur_row + 1];
                    }
                    else
                    {
                        dgv1.CurrentCell = dgv1["Item_Code", cur_row];
                    }

                }
                else
                {
                    SendKeys.Send("{Right}");
                }

                //return base.ProcessCmdKey(ref msg, Keys.Up);
                //return base.ProcessCmdKey(ref msg, keyData);
                return true;
            }

            if (msg.WParam.ToInt32() == (int)Keys.Up && lstunit.Visible)
            {
                if (lstunit.SelectedIndex == 0) lstunit.SelectedIndex = 1; else lstunit.SelectedIndex = 0;
                return true;
            }
            if (msg.WParam.ToInt32() == (int)Keys.Down && lstunit.Visible)
            {
                if (lstunit.SelectedIndex == 0) lstunit.SelectedIndex = 1; else lstunit.SelectedIndex = 0;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdLookup.Visible)
                {
                    textBox1.Focus();
                    return;
                }
                acntrl = dgv1.Name;
                cur_row = dgv1.CurrentCell.RowIndex;
                DataGridView d = (DataGridView)sender;

                if (d[e.ColumnIndex, e.RowIndex].EditType.ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
                    SendKeys.Send("{F4}");
                if (e.ColumnIndex != dgv1["Unit", e.RowIndex].ColumnIndex)
                    lstunit.Visible = false;


                if (e.ColumnIndex == dgv1["Description", e.RowIndex].ColumnIndex)
                {

                    search_data(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                    //if (last_col =="Item_Code")
                    //dgv1.CurrentCell = dgv1["qty", dblclk_row];

                }


                if (e.ColumnIndex == dgv1["Unit", e.RowIndex].ColumnIndex)
                {
                    if (Convert.ToDouble(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value) > 1)
                    {
                        lstunit.Items.Clear();

                        lstunit.Items.Add(dgv1.Rows[cur_row].Cells["OUnit"].Value.ToString());
                        lstunit.Items.Add("Each");
                        lstunit.SelectedIndex = 0;
                    }

                   
                    lstunit.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;
                    lstunit.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top + dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                    if (lstunit.Items.Count>1)
                    lstunit.Visible = true;
                    lstunit.SelectedIndex = 0;
                }

               


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }



        }

        private void search_data(string Item_Code)
        {
            try
            {


                Conn.Close();
                Conn.Open();



                sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.UNIT,h.FRACTION,s.AVG_PUR_PRICE,s.RE_ORDER,W.stock,u.unit_name,S.STOCK AS TSTOCK from hd_ITEMMASTER h inner join unitmaster u on h.unit=u.unit_id left join stock_master s on h.Item_Code=s.Item_Code left JOIN WR_STOCK_MASTER W ON  (h.Item_Code=W.Item_Code   AND W.WR_CODE=" + cmbwh.SelectedValue + " ) where h.brn_code=1  and  h.Item_Code='" + Item_Code + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            

                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Item_Code"].Value = rd[0].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = rd[1].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();
                            //dgv1.Columns["Unit"].


                            //DataGridViewComboBoxColumn cmbunit = (DataGridViewComboBoxColumn)this.dgv1.Columns["Unit"];
                            //cmbunit.Items.Clear();
                            //cmbunit.Items.Add(rd[7].ToString());
                            //if(Convert.ToDouble(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value)>1)
                            //                                cmbunit.Items.Add("Each");



                            //DataGridViewComboBoxColumn cmbunit = (DataGridViewComboBoxColumn)this.dgv1.Columns["Unit"];
                            //cmbunit.Items.Clear();
                            //cmbunit.Items.Add(rd[7].ToString());
                            if (Convert.ToDouble(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value) > 1)
                            {
                                lstunit.Items.Clear();
                                lstunit.Items.Add(rd[7].ToString());
                                lstunit.Items.Add("Each");
                            }

                            dgv1.Rows[cur_row].Cells["Unit"].Value = rd[7].ToString();
                            dgv1.Rows[cur_row].Cells["OUnit"].Value = rd[7].ToString();

                            dgv1.Rows[cur_row].Cells["Price"].Value = rd[4].ToString();
                            dgv1.Rows[cur_row].Cells["Reorder"].Value = rd[5].ToString();
                            //dgv1.Rows[cur_row].Cells["Stock"].Value = rd[6].ToString();
                            dgv1.Rows[cur_row].Cells["Stock"].Value = rd["Stock"].ToString();
                            dgv1.Rows[cur_row].Cells["TStock"].Value = rd["TStock"].ToString();
                            if (string.IsNullOrWhiteSpace(dgv1["Stock", cur_row].Value.ToString())) dgv1["Stock", cur_row].Value = 0;
                            dgv1.Rows[cur_row].Cells["Stock"].Value = Math.Round(Convert.ToDouble(dgv1["Stock", cur_row].Value), 2);

                            if (string.IsNullOrWhiteSpace(dgv1["TStock", cur_row].Value.ToString())) dgv1["TStock", cur_row].Value = 0;
                            dgv1.Rows[cur_row].Cells["TStock"].Value = Math.Round(Convert.ToDouble(dgv1["TStock", cur_row].Value), 2);
                            if (Convert.IsDBNull(dgv1["Reorder", cur_row].Value)) dgv1["Reorder", cur_row].Value = 0;
                           

                            dgv1["Stock", cur_row].Style.BackColor=Color.White;
                            if (Convert.ToDouble(dgv1["Stock", cur_row].Value) <= Convert.ToDouble( dgv1["Reorder", cur_row].Value) &&  Convert.ToDouble( dgv1["Reorder", cur_row].Value) > 0)
                            {
                              dgv1["Stock", cur_row].Style.BackColor=Color.Red;
                            }

                            
                            

                            //dgv1.BeginEdit(true);
                        }

                    }
                    

                    }
                else
                {
                    MessageBox.Show("Invalid Item Found, Please check Again", "Invalid Item");
                    
                    return;

                }
                rd.Close();
                Conn.Close();
                isini = false;

            }


            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

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

        private void GrdLookup_DoubleClick(object sender, EventArgs e)
        {
            int lkprow = 0;


            lkprow = GrdLookup.CurrentCell.RowIndex;
           
                     switch (GrdLookup.Tag.ToString())
            {
                case "MRN":

                    Txtadjustno.Text = GrdLookup.Rows[lkprow].Cells[0].Value.ToString();
                    GrdLookup.Visible = false;
                    // item_select();
                    search_mrn();


                    break;

                case "Item_Code":
                    
                        dgv1.CurrentCell = dgv1["Item_Code", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["Item_Code", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();
                        search_data(dgv1["Item_Code", dblclk_row].Value.ToString());
                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["description", dblclk_row];

                        dgv1.Focus();
                             break;
                case "site":
                    
                        dgv1.CurrentCell = dgv1["site", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["site", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["Bdg.Code", dblclk_row];

                        dgv1.Focus();
                             break;

                case  "Bdg.Code":
                    
                    
                        dgv1.CurrentCell = dgv1["Bdg.Code", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["Bdg.Code", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["remarks", dblclk_row];

                        dgv1.Focus();
                             break;

                               }


            

        }

        private void GrdLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox1_DoubleClick(sender, e);
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
            if (dgv1.CurrentCell == null) return;
            fcol = dgv1.FirstDisplayedCell.ColumnIndex;

        }

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try

               
            {
                dgv1.EndEdit();
                if (!GrdLookup.Visible)
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                last_row = dgv1.CurrentCell.RowIndex;
                //string celval = Convert.ToString(dgv1["Description", cur_row].Value);
                //if (last_col > 6 && celval.Trim() == "") dgv1.CurrentCell = dgv1["Item_Code", cur_row];

                if (e.ColumnIndex != dgv1["Unit", e.RowIndex].ColumnIndex)
                lstunit.Visible = false;

                if ( e.ColumnIndex == dgv1["actualstock", e.RowIndex].ColumnIndex)
            {
                if (dgv1["actualstock", e.RowIndex].Value == null) dgv1["actualstock", e.RowIndex].Value = 0;
                if (dgv1["stock", e.RowIndex].Value == null) dgv1["stock", e.RowIndex].Value = 0;
                if (e.RowIndex == 0)
                {
                    if (Convert.ToDouble(dgv1["actualstock", e.RowIndex].Value) - Convert.ToDouble(dgv1["stock", e.RowIndex].Value) > 0)
                    {
                        adj_type = 1;
                        adj_trntype = 4;

                    }

                    else
                    {
                        adj_type = 0;
                        adj_trntype = 9;
                    }

                }
                else
                {
                    int radj_type;
                    if (Convert.ToDouble(dgv1["actualstock", e.RowIndex].Value) - Convert.ToDouble(dgv1["stock", e.RowIndex].Value) > 0)
                        radj_type = 1;
                        
                    else
                        radj_type = 0;

                    if (radj_type != adj_type)
                    {
                        MessageBox.Show("It is not Possible to Adjust Diffrent Type(Minus Stock/PlusStock) Stock in the same Voucher!!");
                        dgv1["actualstock", e.RowIndex].Value=0;
                    }

                }
                    find_total();
            }

                if (dgv1.Columns[e.ColumnIndex].ReadOnly)
                {
                    //if (last_col > e.ColumnIndex) SendKeys.Send("{Left}"); ;
                    //if (last_col < e.ColumnIndex) SendKeys.Send("{Right}");
                }


                //if (dgv1.CurrentCell == dgv1["site", dgv1.CurrentCell.RowIndex] && !GrdLookup.Visible)
                //{
                //    ADODB.Connection ADOconn = new ADODB.Connection();
                //    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                //    Conn.Close();
                //    Conn.Open();
                //    ADODB.Recordset tmp = new ADODB.Recordset();
                //    var plot = dgv1["site", dgv1.CurrentCell.RowIndex].Value;
                //    if (plot == "" || plot == null) plot = "0";
                //    sql = "SELECT SITE_NAME  FROM SITE_MASTER WHERE PROJ_CODE=" + cmbproject.SelectedValue + " and site_code=" + Convert.ToInt32(plot);

                //    tmp = new Recordset();
                //    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //    if (tmp.RecordCount == 0)
                //    {
                //        MessageBox.Show("Invalid Site");
                //        // dgv1.CurrentCell = dgv1["site", dgv1.CurrentCell.RowIndex];
                //        dgv1["site", dgv1.CurrentCell.RowIndex].Value = "";
                //    }
                //    else
                //    {
                //        // dgv1["site", dgv1.CurrentCell.RowIndex]
                //    }
                //}


                if (dgv1.CurrentCell == dgv1["Bdg.Code", dgv1.CurrentCell.RowIndex] && !GrdLookup.Visible && !string.IsNullOrEmpty( dgv1["Item_Code", cur_row].Value.ToString()))
                {
                    ADODB.Connection ADOconn = new ADODB.Connection();
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                    Conn.Close();
                    Conn.Open();
                    ADODB.Recordset tmp = new ADODB.Recordset();
                    var plot = dgv1["Bdg.Code", dgv1.CurrentCell.RowIndex].Value;
                    if (plot == "" || plot == null) plot = "0";
                    sql = "SELECT Description  FROM Budg_MASTER WHERE  Budg_code=" + Convert.ToInt32(plot);

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (tmp.RecordCount == 0)
                    {
                        MessageBox.Show("Invalid Budget Code");
                        // dgv1.CurrentCell = dgv1["site", dgv1.CurrentCell.RowIndex];
                        dgv1["Bdg.Code", dgv1.CurrentCell.RowIndex].Value = "";
                    }
                    else
                    {
                        // dgv1["site", dgv1.CurrentCell.RowIndex]
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbproject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //projdv.RowFilter = "Proj_Code=" + cmbproject.SelectedValue;
            try {
                 Conn.Close();
                Conn.Open();
            sql = "select site_code, site_name,proj_code from site_master where proj_code=" + cmbproject.SelectedValue ;
            SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
            ada3 = new SqlDataAdapter(sql, Conn);
            //ada3.Fill(dt);
            DataSet siteds = new DataSet();

            ada3.Fill(siteds, "site_master");

            //ada1.Fill(ds, "proj_master");
            //projdv.Table = dt;
            cmbsite.DisplayMember = "site_name";
            cmbsite.ValueMember = "site_code";
            cmbsite.DataSource = siteds.Tables[0];
            }
            catch (SqlException ex)
            {
                //ADOconn.RollbackTrans();
                
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
try
{
    txttrn_type.Text =Convert.ToString(adj_trntype);
    find_total();  


    
    int i;
    bool itemfound = false;
    iserror = true;
    for ( i = 0; i < dgv1.RowCount; i++)
    {
        // i=i+1;
        string celval = Convert.ToString(dgv1["Item_Code",i].Value);

        if (celval.Trim() != "")
        {
            itemfound = true;

            if (dgv1["Bdg.code", i].Value == null )
            {
                dgv1["Bdg.code", i].Value = 0;
            }

            if ( dgv1["Description", i].Value == null || dgv1["actualstock", i].Value == null )
            {
                MessageBox.Show("Invalid Entry on Row " + ++i);
                return;
            }

            if (dgv1["Description", i].Value.ToString() == "" || dgv1["actualstock", i].Value.ToString() == "" )
            {
                MessageBox.Show("Invalid Entry on Row " + ++i);
                return;
            }

        }

    }

    if (itemfound == false)
    {
        MessageBox.Show("No Item found to Save!", "Invalid Entry");
        return;
    }

                       
    

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
          trn = (int) tmp.Fields[0].Value;   
       

         txttrn.Text = trn.ToString();

         Txtadjustno.Text = Gvar.trn_no(Convert.ToInt32(txttrn_type.Text));
     }

     //ADOconn.BeginTrans();

    ADODB.Recordset rec =  new ADODB.Recordset();
    sql ="SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + Txtadjustno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

    if (rec.RecordCount==0)
    {
        
    rec.AddNew();
        rec.Fields["flag"].Value ="N";

    }


   
    rec.Fields["ENTRY_TYPE"].Value = cmbproject.Text;


     rec.Fields["INVOICE_NO"].Value =Txtadjustno.Text;
    rec.Fields["TRAN_NO"].Value = Convert.ToInt32(txttrn.Text);
    rec.Fields["CURDATE"].Value =dt1.Value;
    rec.Fields["trn_type"].Value =Convert.ToInt32(txttrn_type.Text);
   // rec.Fields["trn_type"].Value = adj_trntype;
    rec.Fields["ORG_DUP"].Value ="R";
    rec.Fields["ACCODE"].Value =cmbproject.SelectedValue;
    rec.Fields["REF_NO"].Value =txtrefno.Text;
    rec.Fields["BRN_code"].Value = Gvar._brn_code;
    rec.Fields["WHCODE"].Value = Convert.ToInt32(cmbwh.SelectedValue.ToString());
    rec.Fields["COST_CODE"].Value = cmbcost.SelectedValue;
    rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text);
    rec.Fields["DISC_AMT"].Value = 0;
    rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txttotal.Text);
    rec.Fields["ename"].Value = cmbproject.Text;
    string scode = cmbwh.SelectedValue.ToString();
    rec.Fields["sales_code"].Value = 0;// Convert.ToInt32(cmbsite.SelectedValue.ToString());
    rec.Fields["qout_no"].Value = txtmrifno.Text;
    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
    //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
    
   rec.Update();

   sql = "SELECT rec_no FROM DATA_ENTRY WHERE INVOICE_NO = '" + Txtadjustno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);


   rec = new ADODB.Recordset();
   rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
    decimal rec_no= (decimal) rec.Fields["REC_NO"].Value;
    var a=0;
    sql="DELETE FROM DATA_ENTRY_GRID WHERE REC_NO=" + rec_no;
//ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)
    
                
    SqlCommand cmd = new SqlCommand(sql, Conn);
    cmd.ExecuteNonQuery();


 rec = new ADODB.Recordset();

    sql ="SELECT * FROM DATA_ENTRY_GRID WHERE REC_NO=1.1";

    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

    
    
                           // foreach (DataGridViewRow row in this.dgv1.Rows)

                                for(i=0;i<dgv1.RowCount;i++)
                            {
                               // i=i+1;
                                string celval = Convert.ToString(dgv1["Item_Code",i].Value);


                               //int = dgv1["Item_Code", i].Value.Equals(null);
                               // MessageBox.Show(celval.ToString());
                               // //string celval =  dgv1["Item_Code",i].Value.ToString();

                               // if (!Convert.IsDBNull(dgv1["Item_Code", i].Value) && !Convert.IsDBNull(dgv1["qty", i].Value))
                                if (celval.Trim() != "")
                                {


                                    //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                                    rec.AddNew();
                                    rec.Fields["REC_NO"].Value = rec_no;
                                    rec.Fields["ROWNUM"].Value = i;
                                    rec.Fields["Item_Code"].Value = dgv1["Item_Code", i].Value;
                                    
                                    rec.Fields["Description"].Value = dgv1["Description", i].Value;
                                    
                                    rec.Fields["Budg_Code"].Value = dgv1["Bdg.Code", i].Value;
                                    rec.Fields["RQTY"].Value = Convert.ToDouble(dgv1["Change", i].Value);
                                    rec.Fields["BARCODE"].Value = dgv1["Item_Code", i].Value;
                                    rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                                    rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                                    //rec.Fields["PRICE"].Value = dgv1["price", i].Value;
                                   // rec.Fields["QTY"].Value = dgv1["qty", i].Value;
                                    rec.Fields["plot"].Value = cmbsite.SelectedValue;
                                    rec.Fields["brn_code"].Value = Gvar._brn_code;

                                    rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                                    rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                                    rec.Fields["REF_NO"].Value = Txtadjustno.Text;
                                    rec.Fields["INVOICE_NO"].Value = Txtadjustno.Text;

                                    if (rec.Fields["UNIT"].Value.ToString() == "Each")
                                    {
                                        rec.Fields["QTY"].Value = Convert.ToDouble(dgv1["actualstock", i].Value) / Convert.ToDouble(dgv1["FRACTION", i].Value);
                                        rec.Fields["PRICE"].Value = Convert.ToDouble(dgv1["price", i].Value) / Convert.ToDouble(dgv1["FRACTION", i].Value);
                                        
                                    }
                                    else
                                    {
                                        rec.Fields["QTY"].Value = dgv1["actualstock", i].Value;
                                        rec.Fields["PRICE"].Value = Convert.ToDouble(dgv1["price", i].Value); /// Convert.ToDouble(dgv1["FRACTION", i].Value);
                                        
                                    }              




                                    rec.Update();
                                }

                            }

    //sql="update data_entry set flag='N' where trn_type=11 and invoice_no='" + Txtadjustno.Text.Trim() +"'";

    //tmp = new ADODB.Recordset();

    

    //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                
  //cmd = new SqlCommand(sql, Conn);
  //  cmd.ExecuteNonQuery();

    iserror = false;
    //ADOconn.CommitTrans();
    
   // Conn.Close();
    //ADOconn.Close();
    //MessageBox.Show("MRN Created Successfully!!","Succeed Entry");

//.AddNew
//!REC_NO = REC_NO
//!ROWNUM = i
//!Item_Code = Trim(Mygrid1.TextMatrix(i, 11))
//!PRICE = Trim(Mygrid1.TextMatrix(i, 7))
//!Description = Trim(Mygrid1.TextMatrix(i, 2))
//!QTY = Val(Mygrid1.TextMatrix(i, 3))
//!RQTY = 0
//!BARCODE = Mygrid1.TextMatrix(i, 1)
//!FRACTION = Val(Mygrid1.TextMatrix(i, 8))
//!UNIT = Mygrid1.TextMatrix(i, 6)
//!plot = Trim(Mygrid1.TextMatrix(i, 4))
//If !plot = "" Then
//MsgBox "Invalid Plot for " & Mygrid1.TextMatrix(i, 1), vbInformation
//con.RollbackTrans
//qry = 999
//Exit Sub
//End If
//!REMARKS = Trim(Mygrid1.TextMatrix(i, 5))
//!trn_type = trntype
//!REF_NO = Trim(INVNO)
//!INVOICE_NO = Trim(INVNO)
//.Update
//End With
//End If
//Next i
//'con.Execute "update data_entry set flag='N' where trn_type=11 and invoice_no='" & Trim(INVNO) & "'"
//con.CommitTrans
//Exit Sub
//Resume Next

//ER:
//qry = 999
//con.RollbackTrans
//MsgBox Err.Description
//End Sub
    }
    catch (SqlException ex)
        {
            //ADOconn.RollbackTrans();
            iserror = true;
            MessageBox.Show(ex.Message);
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
                iserror = false;


                cus = new ADODB.Recordset();

                sql = "select Inv_no from trn_master where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + Txtadjustno.Text.Trim() + "'";
                cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (isedit)
                {
                    //if (cus.RecordCount == 0)
                    //{
                    //    DialogResult result = MessageBox.Show("This Invoice Number not found for Update, Do You want Add It Now?", "Invalid Record", MessageBoxButtons.YesNoCancel);
                    //    if (result != DialogResult.Yes)
                    //    {
                    //        ADOconn.RollbackTrans();
                    //        return;
                    //    }
                    //}
                }
                else
                {
                    if (cus.RecordCount > 0)
                    {
                        MessageBox.Show("This Invoice Number Already Exist", "Invalid Record");
                        ADOconn.RollbackTrans();
                        return;
                    }

                }

                //ADOconn.BeginTrans();
                cus = new ADODB.Recordset();
                //ADOconn.BeginTrans();

                if (Convert.ToInt32(txttrn.Text) == 0)
                {
                    sql = "SELECT TRNNO FROM TRN_NO";
                    cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    txttrn.Text = cus.Fields[0].Value.ToString();
                    sql = "SELECT top 1 * from trn_master";
                    cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    cus.AddNew();

                }
                else
                {
                    sql = "select * from trn_master where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + Txtadjustno.Text.Trim() + "'";

                    cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (cus.RecordCount == 0) cus.AddNew();

                }
                cus.Fields["trn_no"].Value = Convert.ToInt32(txttrn.Text);
                cus.Fields["INV_NO"].Value = Txtadjustno.Text.Trim();
                cus.Fields["DATE_TIME"].Value = dt1.Value;
                cus.Fields["cus_code"].Value = cmbproject.SelectedValue;
                cus.Fields["cus_name"].Value = cmbsite.Text;
                cus.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                cus.Fields["TOT_AMOUNT"].Value = Convert.ToDouble(txttotal.Text.Trim());
                cus.Fields["NET_AMOUNT"].Value = Convert.ToDouble(txttotal.Text.Trim());
                cus.Fields["FNET_AMOUNT"].Value = Convert.ToDouble(txttotal.Text.Trim());
                cus.Fields["DISCOUNT"].Value = 0;
                cus.Fields["user_ID"].Value = Gvar._Userid;
                cus.Fields["SALE_TYPE"].Value = cmbsite.SelectedValue;
                cus.Fields["WR_CODE"].Value = cmbwh.SelectedValue;
                cus.Fields["COST_CODE"].Value = cmbcost.SelectedValue;
    
                //cus.Fields["sales_code"].Value = Convert.ToInt32(cmbrequestor.SelectedValue.ToString());
                cus.Update();


                //CRT_TABLE:

                sql = "INSERT INTO EDT_TRN_MASTER([TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE])  SELECT [TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE] FROM TRN_MASTER WHERE trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_NO='" + Txtadjustno.Text.Trim() + "'";

                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();


                sql = "INSERT INTO TMP_ITM_DETAIL(rec_no,Item_Code,TRN_TYPE,TRN_NO,ORDER_NO,INVOICE_NO,BRN_CODE,WR_CODE) SELECT REC_NO,Item_Code,TRN_TYPE,TRN_NO,ORDER_NO,INVOICE_NO,BRN_CODE,WR_CODE FROM TRN_ITM_DETAIL WHERE WR_CODE=" + cmbwh.SelectedValue + " AND BRN_CODE=" + Gvar._brn_code + " AND TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();


                sql = "DELETE FROM TRN_ITM_DETAIL WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();

                ADODB.Recordset rec = new ADODB.Recordset();

                sql = "select * from TRN_ITM_DETAIL where trn_no=0";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                int i = 0;

                // foreach (DataGridViewRow row in this.dgv1.Rows)

                for (i = 0; i < dgv1.RowCount; i++)
                {
                    // i=i+1;
                    string celval = Convert.ToString(dgv1["Item_Code", i].Value);


                    //int = dgv1["Item_Code", i].Value.Equals(null);
                    // MessageBox.Show(celval.ToString());
                    // //string celval =  dgv1["Item_Code",i].Value.ToString();

                    // if (!Convert.IsDBNull(dgv1["Item_Code", i].Value) && !Convert.IsDBNull(dgv1["qty", i].Value))
                    if (celval.Trim() != "")
                    {


                        //MessageBox.Show(Convert.IsDBNull(row.Cells["qty"].Value.ToString());

                        rec.AddNew();
                        rec.Fields["trn_no"].Value = txttrn.Text.Trim();
                        rec.Fields["Item_Code"].Value = dgv1["Item_Code", i].Value;
                        rec.Fields["barcode"].Value = dgv1["Item_Code", i].Value;
                        rec.Fields["UNIT_QTY"].Value = Math.Abs(Convert.ToDouble( dgv1["Change", i].Value));
                        rec.Fields["UNIT_PRICE"].Value = dgv1["price", i].Value;
                        
                        rec.Fields["Budg_Code"].Value = dgv1["Bdg.Code", i].Value;
                        //rec.Fields["RQTY"].Value = 0;
                        rec.Fields["BARCODE"].Value = dgv1["Item_Code", i].Value;
                        // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                        rec.Fields["UNIT"].Value = dgv1["unit", i].Value;
                        rec.Fields["cost_center"].Value = cmbsite.SelectedValue;
                        rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                     if (Convert.ToDouble( dgv1["Change", i].Value) < 0 )
                        rec.Fields["trn_type"].Value = 9;
                     else
                         rec.Fields["trn_type"].Value = 4;
                     rec.Fields["trn_type"].Value =Convert.ToInt32(txttrn_type.Text);
                        rec.Fields["trn_no"].Value = Convert.ToInt32(txttrn.Text);
                        rec.Fields["INVOICE_NO"].Value = Txtadjustno.Text;
                        rec.Fields["ORDER_NO"].Value = 0;
                        if (rec.Fields["UNIT"].Value.ToString() == "Each")
                        {
                            rec.Fields["QTY"].Value = Math.Round(Math.Abs(Convert.ToDouble(dgv1["Change", i].Value)) / Convert.ToDouble(dgv1["FRACTION", i].Value), 2);
                            rec.Fields["PRICE"].Value = Math.Round(Convert.ToDouble(dgv1["price", i].Value) / Convert.ToDouble(dgv1["FRACTION", i].Value),2);
                            rec.Fields["FPRICE"].Value = Math.Round(Convert.ToDouble(rec.Fields["PRICE"].Value) / Convert.ToDouble(dgv1["FRACTION", i].Value),2); 
                        }
                        else
                        {
                            rec.Fields["QTY"].Value = Math.Abs(Convert.ToDouble(dgv1["Change", i].Value));
                            rec.Fields["PRICE"].Value = Convert.ToDouble(dgv1["price", i].Value); /// Convert.ToDouble(dgv1["FRACTION", i].Value);
                            rec.Fields["FPRICE"].Value = rec.Fields["PRICE"].Value;
                        }
                        
                        rec.Fields["SALE_PUR_AMOUNT"].Value = rec.Fields["PRICE"].Value;
                        rec.Fields["WR_CODE"].Value = cmbwh.SelectedValue;
                        rec.Fields["brn_code"].Value = Gvar._brn_code;
                        rec.Update();
                        double sal_pur_amt;
                        sal_pur_amt = 0;
                        sal_pur_amt = sal_pur_amt + ((double)rec.Fields["SALE_PUR_AMOUNT"].Value * (double)rec.Fields["QTY"].Value);



                        //sql = "select SUM(QTY) from DATA_ENTRY_GRID  WHERE rownum=" + dgv1["rownum", i].Value + " and Item_Code='" + dgv1["Item_Code", i].Value + "' AND ref_NO='" + Txtreciept.Text.Trim() + "' And trn_type =" + Convert.ToInt32(txttrn_type.Text);


                        ////cmd = new SqlCommand(sql, Conn);
                        ////cmd.ExecuteNonQuery();

                        //tmp = new Recordset();
                        //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        double qty = 0;
                        //if (!Convert.IsDBNull(tmp.Fields[0].Value))
                        //{
                        //    sql = "UPDATE DATA_ENTRY_GRID SET RQTY = " + tmp.Fields[0].Value + " WHERE  rownum=" + dgv1["rownum", i].Value + " and Item_Code='" + dgv1["Item_Code", i].Value + "' AND invoice_NO='" + Txtreciept.Text.Trim() + "' And trn_type =2";
                        //    tmp = new Recordset();
                        //    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        //    //tmp.Close();

                        //}

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwh.SelectedValue + " AND  Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;



                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        qty = 0;
                        if (tmp.RecordCount != 0)
                        {
                            qty = (double)  tmp.Fields[0].Value;
                        }
                        sql = "SELECT *  FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwh.SelectedValue + " AND   Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;

                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (tmp.RecordCount == 0)
                        {
                            tmp.AddNew();
                        }
                        tmp.Fields["WR_CODE"].Value = cmbwh.SelectedValue;
                        tmp.Fields["brn_CODE"].Value = Gvar._brn_code;
                        tmp.Fields["Item_Code"].Value = rec.Fields["Item_Code"].Value;
                        tmp.Fields["User"].Value = Gvar._Userid;
                        tmp.Fields["stock"].Value = qty;
                        tmp.Update();


                        sql = "SELECT sum(STOCK) FROM STOCK_ITEM WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;



                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        qty = 0;
                        if (tmp.RecordCount != 0)
                        {
                            qty = (double)  tmp.Fields[0].Value;
                        }

                        tmp.Close();

                        sql = "Update STOCK_MASTER set stock = " + Math.Round(qty,2) + " WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;
                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        // ADOconn.Execute(sql, out a, -1);

                        tmp = new Recordset();
                        //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    }
                }


                sql = "SELECT Item_Code FROM TMP_ITM_DETAIL  WHERE WR_CODE=" + cmbwh.SelectedValue + " AND BRN_CODE=" + Gvar._brn_code + " AND TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text) + " AND Item_Code NOT IN (SELECT Item_Code FROM TRN_ITM_DETAIL WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text) + ")";

                rec = new Recordset();
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                while
                (!rec.EOF)
                {
                    sql = "SELECT stock from stock_item WHERE WR_CODE=" + cmbwh.SelectedValue + " AND   Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (tmp.RecordCount != 0)
                    {
                        object a;
                        double qty = (double)tmp.Fields[0].Value;
                        sql = "Update wr_STOCK_MASTER set stock = " + Math.Round(qty,2) + " WHERE WR_CODE=" + cmbwh.SelectedValue + " and Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;
                        ADOconn.Execute(sql, out a, -1);
                    }

                    sql = "SELECT sum(stock) from stock_item WHERE  Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (tmp.RecordCount != 0)
                    {
                        double qty = (double)tmp.Fields[0].Value;
                        sql = "Update STOCK_MASTER set stock = " + Math.Round(qty, 2) + " WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;
                        object a;
                        ADOconn.Execute(sql, out a, -1);

                    }

               }
                saveToolStripButton.Enabled = false;
                ADOconn.CommitTrans();

                tooldelete.Enabled = true;
                cmbwh.Enabled = false;

                isedit = true;

                MessageBox.Show("Material Adjust Entry Saved Successfully!!!", "Succeed Entry");
            }

            catch (Exception ex)
            {
                ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }
        }
        private void delete_data()
        {

            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try
            {
                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                iserror = false;
                ADOconn.BeginTrans();
              
                        DialogResult result = MessageBox.Show("Are You sure to delete This entry?", "Delete Record", MessageBoxButtons.YesNoCancel);
                        if (result != DialogResult.Yes)
                        {
                            ADOconn.RollbackTrans();
                            return;
                        }
               
                
                sql = "INSERT INTO EDT_TRN_MASTER([TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE])  SELECT [TRN_NO],[INV_NO],[DATE_TIME],[CUS_CODE],[CUS_NAME],[TRN_TYPE],[TOT_AMOUNT],[DISCOUNT],[USER_ID],[SALE_TYPE],[FNET_AMOUNT],[NET_AMOUNT],[BRN_CODE],[WR_CODE] FROM TRN_MASTER WHERE trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_NO='" + Txtadjustno.Text.Trim() + "'";

                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    sql = "delete from trn_master where trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and inv_no = '" + Txtadjustno.Text.Trim() + "'";

                    cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                   

                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();



                    sql = "update data_entry set flag='D'  WHERE INVOICE_NO = '" + Txtadjustno.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);


                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);




                sql = "DELETE FROM TMP_ITM_DETAIL WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                sql = "INSERT INTO TMP_ITM_DETAIL(rec_no,Item_Code,TRN_TYPE,TRN_NO,ORDER_NO,INVOICE_NO,BRN_CODE,WR_CODE) SELECT REC_NO,Item_Code,TRN_TYPE,TRN_NO,ORDER_NO,INVOICE_NO,BRN_CODE,WR_CODE FROM TRN_ITM_DETAIL WHERE WR_CODE=" + cmbwh.SelectedValue + " AND BRN_CODE=" + Gvar._brn_code + " AND TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);
                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();


                sql = "DELETE FROM TRN_ITM_DETAIL WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

                tmp = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                //cmd = new SqlCommand(sql, Conn);
                //cmd.ExecuteNonQuery();

                ADODB.Recordset rec = new ADODB.Recordset();
                //ADOconn.CommitTrans();
                //ADOconn.BeginTrans();
                sql = "select * FROM TMP_ITM_DETAIL WHERE TRN_NO=" + Convert.ToInt32(txttrn.Text) + " And trn_type = " + Convert.ToInt32(txttrn_type.Text);

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                  double qty = 0;

                while (!rec.EOF)

                {                       

                        sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwh.SelectedValue + " AND  Item_Code ='" + rec.Fields["Item_Code"].Value.ToString() + "' AND BRN_CODE=" + Gvar._brn_code;
                    

                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);
                        qty = 0;
                        if (tmp.RecordCount != 0)
                        {
                            qty = (double)tmp.Fields[0].Value;
                        }
                        sql = "SELECT *  FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwh.SelectedValue + " AND   Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;

                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                        if (tmp.RecordCount == 0)
                        {
                            tmp.AddNew();
                        }
                        tmp.Fields["WR_CODE"].Value = cmbwh.SelectedValue;
                        tmp.Fields["brn_CODE"].Value = Gvar._brn_code;
                        tmp.Fields["Item_Code"].Value = rec.Fields["Item_Code"].Value;
                        tmp.Fields["User"].Value = Gvar._Userid;
                        tmp.Fields["stock"].Value = qty;
                        tmp.Update();


                        sql = "SELECT sum(STOCK) FROM STOCK_ITEM WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;



                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly, -1);
                        qty = 0;
                        if (tmp.RecordCount != 0)
                        {
                            qty = (double)tmp.Fields[0].Value;
                        }

                        tmp.Close();

                        sql = "Update STOCK_MASTER set stock = " + Math.Round(qty, 2) + " WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;
                        tmp = new Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        // ADOconn.Execute(sql, out a, -1);

                        tmp = new Recordset();
                        //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        rec.MoveNext();

                }






                tooldelete.Enabled = false;
                cmbwh.Enabled = true;
                 ADOconn.CommitTrans();
                 try
                 {

                     for (int i = 0; i < dgv1.RowCount; i++)
                     {
                         if (dgv1[0, i].Value!=null)
                         {
                             search_data(dgv1[0,i].Value.ToString());
                             //dgv1.CurrentCell = dgv1[i, 1];
                             //dgv1.CurrentCell = dgv1[i, 2];
                         }

                     }
                 }
                 catch
                 {
                 }




                 isedit = true;

                MessageBox.Show("Material Adjust Entry Deleted Successfully!!!", "Deleted Entry");

        }
        

                

               



           

            catch (Exception ex)
            {
                ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
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
                    else
                    {

                        if (tooldelete.Enabled)
                        {
                            MessageBox.Show("Please Press Delete Button and Confirm the actual stock!!! and Press Save Button again  ", "Re-Assure the Actual stock ");
                            return;
                        }

                       
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
                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                iserror = false;
                ADOconn.BeginTrans();
                SAVE_DATAENTRY();
                if (!iserror) save_data();
                else
                    ADOconn.RollbackTrans();
            }
            catch (SqlException ex)
            {
            }
        }

        

        private void TxtmrnNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtmrnNo_DoubleClick(object sender, EventArgs e)
        {
          
            try
            {
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE = " + Convert.ToDouble(txttrn_type.Text.ToString()) + " ORDER BY   CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = Txtadjustno.Left;
                GrdLookup.Top = Txtadjustno.Top + Txtadjustno.Height + groupBox1.Top;
                dv.AllowEdit = true;
                dv.Table = dt;
                GrdLookup.Tag = "MRN";
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





                isedit = false;
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY WHERE TRN_TYPE =" +  Convert.ToInt32(txttrn_type.Text) + " AND INVOICE_NO= '" + Txtadjustno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)

                if (rec.RecordCount > 0)
                {
                    txttrn_type.Text = rec.Fields["TRN_TYPE"].Value.ToString();
                    saveToolStripButton.Enabled = false;
                    cmbwh.Enabled = false;
                    Txtadjustno.Enabled = false;
                    isedit = true;
                        txttrn.Text=  rec.Fields["TRAN_NO"].Value.ToString();
                        dt1.Value = (DateTime) rec.Fields["CURDATE"].Value;
                        if (rec.Fields["flag"].Value.ToString() == "D")
                            lblflag.Text = "Deleted";
                        else
                            lblflag.Text = "";
                        //rec.Fields["ACCODE"].Value =cmbproject.SelectedValue;
                        txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                        //txtmrifno.Text = rec.Fields["ORDER_NO"].Value.ToString();
                        txtmrifno.Text = rec.Fields["qout_NO"].Value.ToString();
                        cmbcost.SelectedValue = rec.Fields["cost_code"].Value;
                        cmbproject.SelectedValue = rec.Fields["ACCODE"].Value;
                        cmbwh.SelectedValue = rec.Fields["WHCODE"].Value;
                        //cmbrequestor.SelectedValue = rec.Fields["sales_code"].Value;
                        cmbsite.SelectedValue = rec.Fields["sales_code"].Value;
                        decimal rec_no = (decimal) rec.Fields["REC_NO"].Value;

                        //sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + TxtmrnNo.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                        //rec = new ADODB.Recordset();
                        //rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
    
                        var a=0;
   
                            rec = new ADODB.Recordset();

                            //sql = "SELECT  DATA_ENTRY_GRID.*,stock FROM DATA_ENTRY_GRID left join wr_stock_master as stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no + " and wr_code=" + cmbwh.SelectedValue;
                            sql = "SELECT  DATA_ENTRY_GRID.*,ROUND(W.STOCK,2) AS WSTOCK,ROUND(S.STOCK,2) AS TSTOCK FROM DATA_ENTRY_GRID left join wr_stock_master as w on ( DATA_ENTRY_GRID.Item_Code=w.Item_Code and w.wr_code=" + cmbwh.SelectedValue + ") left join stock_master as s on ( DATA_ENTRY_GRID.Item_Code=s.Item_Code)  WHERE REC_NO=" + rec_no;
                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //    //dgv1.Rows.Clear();
                    //for (a=0; a< dgv1.RowCount-1;a++)
                    //{
                    //    dgv1.Rows.RemoveAt(a);
                    //    }
                                int i=0;
                       // dgv1.Rows.Add(rec.RecordCount+1);

                          dgv1.Rows.Clear()  ;    dgv1.Refresh();
                       

    
                           // foreach (DataGridViewRow row in this.dgv1.Rows)
                   while  (!rec.EOF)

                    {
                        GrdLookup.Visible = false;
                        //ds2.Tables[0].Rows.Add();
                        dgv1.Rows.Add();
                        dgv1["Item_Code", i].Value = rec.Fields["Item_Code"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();
                        //DataGridViewComboBoxColumn cmbunit = (DataGridViewComboBoxColumn)this.dgv1.Columns["Unit"];
                        //cmbunit.Items.Clear();
                        //cmbunit.Items.Add(rec.Fields["Unit"].Value.ToString());
                       
                        //if (Convert.ToDouble(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value) > 1)
                        //    cmbunit.Items.Add("Each");

                        dgv1["Unit", i].Value = rec.Fields["Unit"].Value.ToString();
                        dgv1["OUnit", i].Value = rec.Fields["Unit"].Value.ToString();

                        if (rec.Fields["UNIT"].Value.ToString() == "Each")
                        {
                            dgv1["actualstock", i].Value = Convert.ToDouble(rec.Fields["QTY"].Value) * Convert.ToDouble(dgv1["FRACTION", i].Value);
                            dgv1["price", i].Value = Convert.ToDouble(rec.Fields["PRICE"].Value) * Convert.ToDouble(dgv1["FRACTION", i].Value);
                            
                        }
                        else
                        {
                            dgv1["actualstock", i].Value = rec.Fields["QTY"].Value.ToString();
                            dgv1["price", i].Value = rec.Fields["PRICE"].Value.ToString();
                          
                        }
                       
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        
                        dgv1["Bdg.Code", i].Value = rec.Fields["Budg_Code"].Value.ToString();
                        
                        //dgv1["site", i].Value = rec.Fields["plot"].Value.ToString();
                       double stock = Convert.ToDouble(rec.Fields["QTY"].Value) - Convert.ToDouble(rec.Fields["rqty"].Value);
                       // CHNAGED LATELY 
                       //dgv1["stock", i].Value = stock;
                       dgv1["stock", i].Value = rec.Fields["WSTOCK"].Value.ToString(); ;
                       dgv1["Tstock", i].Value = rec.Fields["TSTOCK"].Value.ToString(); ;

                                   // rec.Fields["UNIT"].Value = dgv1["unit", i].Value;


                        //dgv1["site", i].Value = rec.Fields["site_name"].Value;

                        //DataGridViewComboBoxColumn cmbsite = (DataGridViewComboBoxColumn) this.dgv1["Site",i];
                         
                       //DataGridViewComboBoxCell cmbsite =(DataGridViewComboBoxCell) this.dgv1["Site",i];
                       // for (int r = 0; r < cmbsite.Items.Count; r++)
                       // {
                       //     DataRowView drw = cmbsite.Items[r] as DataRowView;
                       //     string site = drw["site_name"].ToString();
                       //     if( site.ToString() == rec.Fields["site_name"].Value.ToString())
                       //    // i = cmbsite.Items.IndexOf(rec.Fields["site_name"].Value.ToString());
                       //     {
                       //         //cmbsite.DisplayIndex = i;
                       //         cmbsite.;
                       //         cmbsite.Value =rec.Fields["site_name"].Value.ToString();
                       //         dgv1["plot",i].
                       //         break;
                       //     }
                       // }



                        //for (i = 0; i < cmbsite.Items.Count; i++)
                        //{


                        //    DataRowView drw = cmbsite.Items[i] as DataRowView;
                        //    //catval = drw["plot"].ToString();
                        //    if ( dr .ToString() == rec.Fields["plot"].Value)
                        //    {
                        //        cmbsite.DisplayIndex = i;
                        //        break;
                        //    }

                        //}


                       //cmbunit.Items.Clear();
                        //cmbunit.Items.Add(rd[2].ToString());
                        //cmbunit.Items.Add("Single")


                        dgv1["change", i].Value = rec.Fields["rqty"].Value.ToString();
                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                                i=i+1;
                                rec.MoveNext();
                                dgv1.EndEdit();
                           
                    
                    }

                   //find_total();
                }


                else
                {
                    MessageBox.Show("Invalid Adjust Number", "Invalid Entry");
                }



            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
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

        }

        private void load_ini()
        {
              dgv1.Rows.Clear()  ;    dgv1.Refresh();
            Txtadjustno.Text="";
            txtrefno.Text = "";
            txttrn.Text = "";
            txttotal.Text = "";
            isedit = false;
            tooldelete.Enabled = true;
            Txtadjustno.Enabled = true;
            lblflag.Text = "";
            saveToolStripButton.Enabled = true;
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
                    if (dgv1["Item_Code", i].Value != null)
                    {
                        if (dgv1["price", i].Value == null) dgv1["price", i].Value = 0;
                        if (dgv1["stock", i].Value == null) dgv1["stock", i].Value = 0;
                        if (dgv1["actualstock", i].Value == null) dgv1["actualstock", i].Value = 0;
                        if (Convert.IsDBNull(dgv1["price", i].Value) || string.IsNullOrEmpty(dgv1["price", i].Value.ToString())) dgv1["price", i].Value = 0;
                        if (Convert.IsDBNull(dgv1["actualstock", i].Value)) dgv1["actualstock", i].Value = 0;
                        if (Convert.IsDBNull(dgv1["stock", i].Value)) dgv1["stock", i].Value = 0;
                        //dgv1["Change", i].Value = Convert.ToDouble(dgv1["actualstock", i].Value) - Convert.ToDouble(dgv1["stock", i].Value);
                        price = Convert.ToDouble(dgv1["price", i].Value) * Math.Abs( Convert.ToDouble(dgv1["change", i].Value));


                        if (dgv1["UNIT",i].Value.ToString() == "Each")
                        {
                            
                            dgv1["Change", i].Value = Convert.ToDouble(dgv1["actualstock", i].Value) - (Convert.ToDouble(dgv1["stock", i].Value) * Convert.ToDouble(dgv1["FRACTION", i].Value));
                        }
                        else
                        {
                            dgv1["Change", i].Value = Convert.ToDouble(dgv1["actualstock", i].Value) - Convert.ToDouble(dgv1["stock", i].Value);
                        }


                        if (Convert.ToInt32(txttrn_type.Text) == 4 && Convert.ToInt32(dgv1["Change", i].Value) < 0)
                        {
                            MessageBox.Show("Invalid Adjustment Please Use Minus Adjustment screen");
                            dgv1["actualstock", i].Value = 0;
                            dgv1["Change", i].Value = 0;
                        }

                        if (Convert.ToInt32(txttrn_type.Text) == 9 && Convert.ToInt32(dgv1["Change", i].Value) > 0)
                        {
                            MessageBox.Show("Invalid Adjustment Please Use Plus Adjustment screen");
                            dgv1["actualstock", i].Value = 0;
                            dgv1["Change", i].Value = 0;

                        }
                            ;





                        dgv1["total", i].Value = price;
                        tot = tot + price;
                        
                    }
                }

                txttotal.Text = tot.ToString();
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
        // }



        private void print_reciept()
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


                if (string.IsNullOrEmpty(Txtadjustno.Text)) return;

                ReportDocument CrRep = new ReportDocument();


                rep_path = Application.StartupPath + "\\reports\\Reciept_ADJ.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{DATA_ENTRY.INVOICE_NO} = '" + Txtadjustno.Text.Trim() + "' and {DATA_ENTRY.TRN_TYPE} =" +Convert.ToInt32(txttrn_type.Text);


                if (crt != "") CrRep.RecordSelectionFormula = crt;

                //CrRep.VerifyDatabase = false;
                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
                //crconnectioninfo.ServerName = "SqlStockex";
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

        

        private void lstunit_DoubleClick(object sender, EventArgs e)
        {
            dgv1.Rows[cur_row].Cells["Unit"].Value = lstunit.Items[lstunit.SelectedIndex].ToString();
            lstunit.Visible = false;
            dgv1.CurrentCell = dgv1["Price", cur_row];

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tooldelete_Click(object sender, EventArgs e)
        {
            delete_data();
        }
       






    }
}








    

