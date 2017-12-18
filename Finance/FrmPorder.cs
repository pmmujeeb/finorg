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
using System.Threading;

namespace FinOrg
{
    public partial class Frmporder : Form
    {

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

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
        bool issearch;
        int cur_row;
        int dblclk_row;
        int fcol;
        string last_col;


        public Frmporder()
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
                Txtporder.Text = Gvar.invno;
                search_order();
            }
        }

        private void load_form()
        {

            try
            {
                int trn = Gvar._trntype;
                txttrn_type.Text = trn.ToString();
                Conn.Close();
                Conn.Open();

                sql = "select Proj_code, Proj_name from proj_master";

                SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");

                DataSet ds = new DataSet();



                ada1.Fill(ds, "proj_master");
                cmbproject.DisplayMember = "Proj_name";
                cmbproject.ValueMember = "proj_code";
                cmbproject.DataSource = ds.Tables[0];


                sql = "select acc_no, acc_name from accounts where acc_type_code=6";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet ds1 = new DataSet();
                this.ada.Fill(ds1, "accounts");
                cmbrequestor.DisplayMember = "Acc_name";
                cmbrequestor.ValueMember = "acc_no";
                cmbrequestor.DataSource = ds1.Tables[0];

                sql = "select acc_no, acc_name from accounts where acc_type_code=2";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet dssup = new DataSet();
                this.ada.Fill(dssup, "accounts");
                cmbsup.DisplayMember = "Acc_name";
                cmbsup.ValueMember = "acc_no";
                cmbsup.DataSource = dssup.Tables[0];


                sql = "select wr_code, wr_name from wrhouse_master";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet dswh = new DataSet();
                this.ada.Fill(dswh, "wrhouse_master");
                cmbwh.DisplayMember = "wr_name";
                //cmbsup.ValueMember = "WR_CODE";
                cmbwh.DataSource = dswh.Tables[0];


                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                ds2.AcceptChanges();
                //set the table as the datasource for the grid in order to show that data in the grid

                // dgv1.Columns[0].DataPropertyName = "LEADER_NAME";

                sql = "select [Col1],[Col2],convert(bit,1) as Selected,[Col3] FROM [Grid_Master] where col='1'";
                //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";

                SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                DataTable dt = new DataTable("Grid_Master");
                dt.AcceptChanges();
                ada2.Fill(ds2, "Grid_Master");
                dgv1.Visible = true;
                dv.AllowEdit = true;
                // dv.AllowNew = true;
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
                dgv1.Columns.Add(Col2);



                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                //chk.DefaultCellStyle = lst1.DefaultCellStyle;
               
                chk.ReadOnly = false;
                dgv1.Columns.Add(chk);
                DataGridViewTextBoxColumn Col3 = new DataGridViewTextBoxColumn();
                Col3.HeaderText = "Col3";
                Col3.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(Col3);

                //sql = "select site_code, site_name,proj_code from site_master";
                //SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                //ada3 = new SqlDataAdapter(sql, Conn);
                //ada3.Fill(dt);
                //projdv.Table = dt;
                DataGridViewTextBoxColumn txt2 = new DataGridViewTextBoxColumn();
                txt2.HeaderText = "Price";
                txt2.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt2);

               

                DataGridViewTextBoxColumn txt1 = new DataGridViewTextBoxColumn();
                txt1.HeaderText = "Plot";
                txt1.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt1);

                DataGridViewTextBoxColumn txt6 = new DataGridViewTextBoxColumn();
                txt6.HeaderText = "remarks";
                txt6.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt6);

                DataGridViewTextBoxColumn tot = new DataGridViewTextBoxColumn();
                tot.HeaderText = "Total";
                tot.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(tot);

                DataGridViewTextBoxColumn txt7 = new DataGridViewTextBoxColumn();
                txt7.HeaderText = "Unit";
                txt7.DefaultCellStyle = dgv1.DefaultCellStyle;
                dgv1.Columns.Add(txt7);


                DataGridViewTextBoxColumn txt3 = new DataGridViewTextBoxColumn();
                txt3.HeaderText = "Stock";
                txt3.DefaultCellStyle = dgv1.DefaultCellStyle;
                txt3.ReadOnly = true;
                dgv1.Columns.Add(txt3);

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




                dgv1.Refresh();
                dgv1.Columns[0].HeaderText = "Item Code";
                dgv1.Columns[1].HeaderText = "Description";
                dgv1.Columns[2].HeaderText = "Selected";
                dgv1.Columns[3].HeaderText = "Qty";
                dgv1.Columns[4].HeaderText = "Price";

                dgv1.Columns[6].HeaderText = "Remarks";

                dgv1.Columns[5].HeaderText = "Site";
                dgv1.Columns[7].HeaderText = "Total";
                dgv1.Columns[8].HeaderText = "Unit";


                dgv1.Columns[9].HeaderText = "Stock";
                dgv1.Columns[10].HeaderText = "Fraction";
                dgv1.Columns[11].HeaderText = "Re-order";


                dgv1.Columns[0].Name = "Item_Code";
                dgv1.Columns[1].Name = "Description";
                dgv1.Columns[2].Name = "Selected";
                dgv1.Columns[3].Name = "Qty";
                dgv1.Columns[4].Name = "Price";
                dgv1.Columns[6].Name = "remarks";
                dgv1.Columns[5].Name = "Site";
                dgv1.Columns[7].Name = "Total";

                dgv1.Columns[8].Name = "Unit";

                dgv1.Columns[9].Name = "stock";
                dgv1.Columns[10].Name = "fraction";
                dgv1.Columns[11].Name = "Reorder";

                dgv1.Columns[10].Visible = false;
                //dgv1.Columns[10].Visible = false;
               // DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dgv1.Columns[11].Visible = false;
                dgv1.Columns[11].Visible = false;
                //dgv1.Columns[0].ReadOnly = true;
                dgv1.Columns[1].ReadOnly = true;
                //dgv1.Columns[6].ReadOnly = true;
                dgv1.Columns[7].ReadOnly = true;
                dgv1.Columns[8].ReadOnly = true;
                dgv1.Columns[9].ReadOnly = true;
                dgv1.Columns[10].Visible = false;
                dgv1.Columns[10].ReadOnly = true;
                //dgv1.Columns[2].HeaderText = "Qty";
                dgv1.Columns[1].Width = 250;
                dgv1.Columns[0].Width = 170;
                dgv1.Columns[2].Width = 75;
                dgv1.Columns[3].Width = 75;
                dgv1.Columns[5].Width = 75;
                dgv1.Columns[6].Width = 75;
                dgv1.Columns[7].Width = 75;
                dgv1.Columns[6].Width = 250;
                 dgv1.Columns[8].Width = 75;
                 dgv1.Columns[9].Width = 75;
                //dgv1.AllowUserToAddRows = false;
                // dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //dgv1.Columns["leader_no"].Visible = false;
                //if (Gvar._SuperUserid != 1) dgv1.Columns["Approved"].ReadOnly = true;
                 this.toolpost.Visible = false;
                 if (this.txtpriv.Text.Substring(this.txtpriv.Text.Length - 1, 1) == "9")
                 {
                     this.toolpost.Visible = true;
                     checkBox1.Checked = true;
                 }

                Thread thread = new Thread(new ThreadStart(load_crt));
                 thread.Start();

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


                rep_path = Application.StartupPath + "\\reports\\Reciept_ORD.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;

                CrRep.Close();

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dgv1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GrdLookup.Visible) return;

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
                if (dgv1.CurrentCell == null) dgv1.CurrentCell = dgv1["Item_Code", cur_row];
                if (dgv1.CurrentCell == dgv1["Item_Code", cur_row])
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,stock from hd_itemmaster  left join stock_master on (hd_itemmaster.Item_Code=stock_master.Item_Code)", Conn);

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

                }


                if (dgv1.CurrentCell == dgv1["site", cur_row])
                {
                    dblclk_row = dgv1.CurrentCell.RowIndex;
                    Conn.Close();
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand("select site_code,site_name from site_master where proj_code=" + cmbproject.SelectedValue, Conn);

                    SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                    DataTable dtlkp = new DataTable("site_master");
                    adalkp.Fill(dtlkp);
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                    //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                    // dataGrid1.DataContext = dt.DefaultView;
                    //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                    //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                    GrdLookup.Left = textBox1.Left;
                    GrdLookup.Top = textBox1.Top + textBox1.Height;

                    GrdLookup.Tag = "site";
                    dv.Table = dtlkp;
                    GrdLookup.DataSource = dv;
                    GrdLookup.Columns[0].Width = 170;
                    GrdLookup.Columns[1].Width = 300;
                    GrdLookup.Visible = true;
                    textBox1.Focus();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FrmMRNEntry_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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



                if (GrdLookup.Visible == true && last_col == "site")
                {
                    //dgv1.EndEdit();


                    //dgv1.BeginEdit(false);
                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "site_code LIKE  '" + txt + "%' OR site_name LIKE '" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "site_code <> '0'";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        

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

                            if (last_col == "Item_Code" || last_col==null)
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;
                                dgv1.CurrentCell = dgv1.Rows[dblclk_row].Cells[dgv1.FirstDisplayedCell.ColumnIndex];

                                dgv1.BeginEdit(false);
                                dgv1["Item_Code", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                dgv1.EndEdit();
                                //dgv1.CurrentCell = dgv1.Rows[dblclk_row].Cells[dgv1.FirstDisplayedCell.ColumnIndex];
                                search_data(dgv1["Item_Code", dblclk_row].Value.ToString());
                                GrdLookup.Visible = false;
                                dgv1.Focus();
                                return;
                                //e.Handled = true;
                                //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }

                            if (last_col == "Site")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;
                                dgv1.CurrentCell = dgv1["site", cur_row];

                                dgv1.BeginEdit(false);
                                dgv1["site", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                dgv1.EndEdit();

                                GrdLookup.Visible = false;
                                dgv1.Focus();
                                return;
                                //e.Handled = true;
                                //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }


                        }
                        break;


                    case Keys.ShiftKey:
                       // dgv1.CurrentCell = dgv1.Rows[0].Cells[dgv1.FirstDisplayedCell.ColumnIndex];
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
            if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgv1" && !GrdLookup.Visible)
            {
                // SendKeys.Send("{Tab}");

                keyData = Keys.Tab;
                if (dgv1.CurrentCell == dgv1["Remarks", cur_row])
                {
                    if (dgv1.CurrentCell.RowIndex+1 < dgv1.RowCount)
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
                dgv1.EndEdit();
                acntrl = dgv1.Name;
                cur_row = dgv1.CurrentCell.RowIndex;
                
               



                if (e.ColumnIndex == dgv1["Description",e.RowIndex].ColumnIndex )
                {

                    search_data(dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());


                }



                
            }
            catch
            {
            }
        }

        private void search_data(string Item_Code)
        {
            try
            {


                Conn.Close();
                Conn.Open();



                sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.UNIT,h.FRACTION,s.AVG_PUR_PRICE,s.RE_ORDER,s.stock ,u.unit_name from hd_ITEMMASTER h inner join unitmaster u on h.unit=u.unit_id  left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=1  and h.Item_Code='" + Item_Code + "'";
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
                            if (dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value == null || dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value.ToString().Trim()=="")
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Description"].Value = rd[1].ToString();
                            dgv1.Rows[dgv1.CurrentCell.RowIndex].Cells["Fraction"].Value = rd[3].ToString();
                            //dgv1.Columns["Unit"].


                            //DataGridViewComboBoxColumn cmbunit = (DataGridViewComboBoxColumn)this.dgv1.Columns["Unit"];
                            //cmbunit.Items.Clear();
                            //cmbunit.Items.Add(rd[2].ToString());
                            //cmbunit.Items.Add("Single");

                            dgv1.Rows[cur_row].Cells["Unit"].Value = rd[7].ToString();
                            dgv1.Rows[cur_row].Cells["Price"].Value = rd[4].ToString();
                            dgv1.Rows[cur_row].Cells["Reorder"].Value = rd[5].ToString();
                            dgv1.Rows[cur_row].Cells["Stock"].Value = rd["Stock"].ToString();
                            if (string.IsNullOrWhiteSpace(dgv1["Stock", cur_row].Value.ToString())) dgv1["Stock", cur_row].Value = 0;
                            dgv1.Rows[cur_row].Cells["Stock"].Value = Math.Round(Convert.ToDouble(dgv1["Stock", cur_row].Value), 2);

                            

                            dgv1.Rows[cur_row].Cells["Selected"].Value = 1;
                            dgv1.CurrentCell = dgv1["qty", dblclk_row];
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
            try
            {

                int lkprow = 0;


                lkprow = GrdLookup.CurrentCell.RowIndex;

                switch (GrdLookup.Tag.ToString())
                {

                    case "ORD":

                        Txtporder.Text = GrdLookup.Rows[lkprow].Cells[0].Value.ToString();
                        GrdLookup.Visible = false;
                        search_order();
                        // item_select();



                        break;

                    case "MRN":

                        TxtmrnNo.Text = GrdLookup.Rows[lkprow].Cells[0].Value.ToString();
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
                        GrdLookup.Visible = false;
                        search_data(dgv1["Item_Code", dblclk_row].Value.ToString());
                         //this.dgv1.CurrentCell = this.dgv1["description", dblclk_row];

                        //dgv1.Focus();
                        break;
                    case "site":

                        dgv1.CurrentCell = dgv1["site", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["site", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        //dgv1["Description", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["remarks", dblclk_row];

                        dgv1.Focus();
                        break;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void item_select()
        {

            try
            {
                sql = " SELECT convert(bit,1) as Selected,[Item_Code] ,[DESCRIPTION] ,[QTY] ,[UNIT]  FROM [DATA_ENTRY_GRID] where trn_type=11 and Invoice_no='" + TxtmrnNo.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                // //SqlDataReader rd = cmd.ExecuteReader();

                //SqlDataReader rd = cmd.ExecuteReader();


                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY_GRID");
                ada.Fill(dt);


                //// dataGrid1.DataContext = dt.DefaultView;
                //grditems.Left = TxtmrnNo.Left;
                ////GrdLookup.Top = TxtmrnNo.Top + TxtmrnNo.Height + groupBox1.Top;
                //dv.AllowEdit = true;
                //dv.Table = dt;
                //grditems.Tag = "MRN";
                //grditems.DataSource = dv;
                //grditems.Columns[2].Width = 300;
                //grditems.Columns[0].ReadOnly = false;
                //grditems.Columns[1].ReadOnly = true;
                //grditems.Columns[2].ReadOnly = true;
                //grpterm.Visible = true;

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
            catch
            {
            }

            finally
            {

            }


        }

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!GrdLookup.Visible)
                    last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                //string celval = Convert.ToString(dgv1["Description", cur_row].Value);
                //if (last_col > 6 && celval.Trim() == "") dgv1.CurrentCell = dgv1["Item_Code", cur_row];
                
                dgv1.EndEdit();

                if (e.ColumnIndex == dgv1["qty", e.RowIndex].ColumnIndex || e.ColumnIndex == dgv1["price", e.RowIndex].ColumnIndex)
                {
                    find_total();
                }

                if (dgv1.CurrentCell == dgv1["site", dgv1.CurrentCell.RowIndex] && !GrdLookup.Visible)
                {
                    ADODB.Connection ADOconn = new ADODB.Connection();
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                    Conn.Close();
                    Conn.Open();
                    ADODB.Recordset tmp = new ADODB.Recordset();
                    dgv1.EndEdit();
                    var plot = dgv1["site", e.RowIndex].Value;
                    if (plot == "" || plot == null) plot = "0";
                    sql = "SELECT SITE_NAME  FROM SITE_MASTER WHERE PROJ_CODE=" + cmbproject.SelectedValue + " and site_code=" + Convert.ToInt32(plot);

                    tmp = new Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (tmp.RecordCount == 0)
                    {
                        //dgv1["site", e.RowIndex].Value = "";
                        
                        MessageBox.Show("Invalid Site");
                        // dgv1.CurrentCell = dgv1["site", dgv1.CurrentCell.RowIndex];
                        dgv1["site", e.RowIndex].Value = "";
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
        }




        private void SAVE_DATAENTRY()
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            // SqlTransaction tran = Conn.BeginTransaction();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try
            {
                find_total();  
                int i;
                bool itemfound = false;
                for (i = 0; i < dgv1.RowCount; i++)
                {
                    // i=i+1;
                    string celval = Convert.ToString(dgv1["Item_Code", i].Value);

                    if (celval.Trim() != "")
                    {
                        itemfound = true;


                        if (dgv1["Description", i].Value == null || dgv1["Qty", i].Value == null || dgv1["site", i].Value == null)
                        {
                            MessageBox.Show("Invalid Entry on Row " + ++i);
                            return;
                        }

                        if (dgv1["Description", i].Value.ToString() == "" || dgv1["Qty", i].Value.ToString() == "" || dgv1["site", i].Value.ToString() == "")
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




                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                //tran = Conn.BeginTransaction();
                tmp = new ADODB.Recordset();
                if (!isedit)
                {
                    sql = "SELECT max(TRaN_NO)+1 FROM DATA_ENTRY WHERE TRN_TYPE=12";

                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //if (tmp.Fields[0].Value)
                    //    txttrn.Text="1";
                    //else

                    int trn;
                    if (Convert.IsDBNull(tmp.Fields[0].Value))
                        trn = 1;
                    else
                        trn =(int)  tmp.Fields[0].Value;
                    txttrn.Text = trn.ToString();
                    Txtporder.Text = string.Format( Gvar.trn_no(Convert.ToInt32(txttrn_type.Text)),"0000000");
                }



                ADOconn.BeginTrans();
                ADODB.Recordset rec = new ADODB.Recordset();
                sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + Txtporder.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (rec.RecordCount == 0)
                {

                    rec.AddNew();
                    rec.Fields["flag"].Value = "N";

                }

                rec.Fields["INVOICE_NO"].Value = Txtporder.Text;
                rec.Fields["TRAN_NO"].Value = Convert.ToInt32(txttrn.Text);
                rec.Fields["CURDATE"].Value = dt1.Value;
                rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                rec.Fields["ORG_DUP"].Value = "P";
                rec.Fields["ACCODE"].Value = cmbsup.SelectedValue.ToString(); 
                rec.Fields["REF_NO"].Value = txtrefno.Text;
                rec.Fields["ename"].Value = cmbsup.Text;
                rec.Fields["sales_code"].Value = Convert.ToInt32(cmbrequestor.SelectedValue.ToString());
                rec.Fields["order_no"].Value = TxtmrnNo.Text;
                rec.Fields["QOUT_NO"].Value = txtqout.Text;
                rec.Fields["WHCODE"].Value = Convert.ToInt32(cmbwh.SelectedValue.ToString());
                rec.Fields["G_TOTAL"].Value = Convert.ToDouble(txttotal.Text);
                rec.Fields["DISC_AMT"].Value = Convert.ToDouble(Txtdisc.Text);
                rec.Fields["NET_AMOUNT"].Value = Convert.ToDouble(Txtnet.Text);
                // ENTRY TYPE USING FOR SUPPLIER
                rec.Fields["ENTRY_TYPE"].Value = cmbproject.Text;
                rec.Fields["BRN_code"].Value = Gvar._brn_code;
                rec.Fields["Remarks"].Value = txtmrnrefno.Text;




                // FOR ORDER MRN_NO USED FOR  THE ORDER NUMBER COLUMNS
                //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;
                //rec.Fields["INVOICE_NO"].Value =TxtmrnNo.Text;

                rec.Update();

                sql = "SELECT rec_no FROM DATA_ENTRY WHERE INVOICE_NO = '" + Txtporder.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                rec = new ADODB.Recordset();
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                decimal rec_no = (decimal) rec.Fields["REC_NO"].Value;
                var a = 0;
                sql = "DELETE FROM DATA_ENTRY_GRID WHERE REC_NO=" + rec_no;
                //ADOconn.Execute("DELETE FROM DATA_ENTRY_GRID",a,1)

                tmp = new ADODB.Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                //SqlCommand cmd = new SqlCommand(sql, Conn);

                //cmd.ExecuteNonQuery();


                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY_GRID WHERE REC_NO=1.1";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

             

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
                        rec.Fields["REC_NO"].Value = rec_no;
                        rec.Fields["ROWNUM"].Value = i + 1;
                        rec.Fields["Item_Code"].Value = dgv1["Item_Code", i].Value;
                        rec.Fields["PRICE"].Value = dgv1["price", i].Value;
                        rec.Fields["Description"].Value = dgv1["Description", i].Value;
                        rec.Fields["QTY"].Value = dgv1["qty", i].Value;
                        rec.Fields["RQTY"].Value = 0;
                        rec.Fields["BARCODE"].Value = dgv1["Item_Code", i].Value;
                        if (string.IsNullOrEmpty(dgv1["fraction", i].Value.ToString())) dgv1["fraction", i].Value=1;
                        rec.Fields["FRACTION"].Value = Convert.ToInt32(dgv1["fraction", i].Value);
                        rec.Fields["UNIT"].Value = dgv1["unit", i].Value;

                        rec.Fields["plot"].Value = dgv1["site", i].Value;
                        rec.Fields["brn_code"].Value = Gvar._brn_code;

                        rec.Fields["REMARKS"].Value = dgv1["remarks", i].Value;
                        rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
                        rec.Fields["REF_NO"].Value = TxtmrnNo.Text;
                        rec.Fields["INVOICE_NO"].Value = Txtporder.Text;
                        rec.Update();

                        tmp = new Recordset();

                        if (!string.IsNullOrEmpty(TxtmrnNo.Text))
                        {

                            tmp.Open("select SUM(QTY) from DATA_ENTRY_GRID  WHERE trn_type =" + Convert.ToInt32(txttrn_type.Text) + " and Item_Code='" + dgv1["Item_Code", i].Value + "' AND ref_NO='" + TxtmrnNo.Text + "'", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            double qty = 0;
                            if (tmp.RecordCount != 0) qty = (double)tmp.Fields[0].Value;
                            tmp.Close();

                            sql = "UPDATE DATA_ENTRY_GRID SET RQTY = " + qty + " WHERE TRN_TYPE = 11 AND  Item_Code='" + dgv1["Item_Code", i].Value + "' AND ref_NO='" + TxtmrnNo.Text.Trim() + "'";

                            ADODB.Recordset tmp1 = new ADODB.Recordset();
                            tmp1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                            // cmd = new SqlCommand(sql, Conn);
                            // cmd.Transaction = tran;
                            //cmd.ExecuteNonQuery();


                        }

                    }

                }

               

                if (!string.IsNullOrEmpty(TxtmrnNo.Text))
                {
                    sql = "select Invoice_no from DATA_ENTRY_GRID   WHERE rqty<qty and TRN_TYPE = 11 AND ref_NO='" + TxtmrnNo.Text.Trim() + "'";

                    tmp = new ADODB.Recordset();
                    tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (tmp.RecordCount == 0)
                    {
                        sql = "update data_entry set flag='U' where trn_type=11 and invoice_no='" + TxtmrnNo.Text.Trim() + "'";
                        tmp = new ADODB.Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    }
                    else
                    {
                        sql = "update data_entry set flag='P' where trn_type=11 and invoice_no='" + TxtmrnNo.Text.Trim() + "'";
                        tmp = new ADODB.Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    }
                }
                //cmd = new SqlCommand(sql, Conn);
                //cmd.Transaction = tran;
                //cmd.ExecuteNonQuery();
                //tran.Commit();
                isedit = true;


            sql = "INSERT INTO [DATA_ENTRY_GRID_LOG]([REC_NO],[ROWNUM],[ITEM_CODE],[DESCRIPTION],[QTY],[PRICE],[DISC],[SALE_PUR_AMT],[ITM_TOTAL],[BARCODE],[FRACTION],[UNIT],[REMARKS],[RQTY],[REF_NO],[TRN_TYPE],[BRN_CODE],[plot],[SIZES],[invoice_no],[Budg_Code],USER_ID) select [REC_NO],[ROWNUM],[ITEM_CODE],[DESCRIPTION],[QTY],[PRICE],[DISC],[SALE_PUR_AMT],[ITM_TOTAL],[BARCODE],[FRACTION],[UNIT],[REMARKS],[RQTY],[REF_NO],[TRN_TYPE],[BRN_CODE],[plot],[SIZES],[invoice_no],[Budg_Code],'" + Gvar.Userid + "' from data_entry_grid  where trn_type=12 and invoice_no='" + Txtporder.Text.Trim() + "'";
                tmp = new ADODB.Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                ADOconn.CommitTrans();
                cmdterm.Enabled = true;
                MessageBox.Show("Successfully Saved Purchase Order!!", "Save Succeeded");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ADOconn.RollbackTrans();
                //tran.Rollback();
            }


        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {


            //DataGridViewCell ccell = dgv1.CurrentCell;
            //dgv1.CurrentCell = dgv1["Item_Code", 0];
            //dgv1.CurrentCell = ccell;

            dgv1.EndEdit();
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
            SAVE_DATAENTRY();
        }



        private void TxtmrnNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select Invoice_NO as MRN_No,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=11 AND FLAG <> 'U' ORDER BY   CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = TxtmrnNo.Left;
                GrdLookup.Top = TxtmrnNo.Top + TxtmrnNo.Height + groupBox1.Top;
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
            if (Txtporder.Text != "")
                search_order();
            else

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






                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY WHERE TRN_TYPE=11 AND FLAG <> 'U' AND INVOICE_NO= '" + TxtmrnNo.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)


                isedit = false;
                if (rec.RecordCount > 0)
                {


                    txttrn.Text = rec.Fields["TRAN_NO"].Value.ToString();
                    dt1.Value = (DateTime) rec.Fields["CURDATE"].Value;

                    //rec.Fields["ACCODE"].Value =cmbproject.SelectedValue;
                    txtmrnrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                    //cmbproject.FindString(rec.Fields["ACCODE"].Value.ToString());
                    //cmbrequestor.FindString(rec.Fields["sales_code"].Value.ToString());

                    cmbproject.SelectedValue = rec.Fields["ACCODE"].Value;
                    cmbrequestor.Text = rec.Fields["ename"].Value.ToString();
                    //cmbcost.SelectedValue = rec.Fields["cost_code"].Value;
                    decimal rec_no = (decimal) rec.Fields["REC_NO"].Value;

                    //sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + TxtmrnNo.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    //rec = new ADODB.Recordset();
                    //rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    var a = 0;

                    rec = new ADODB.Recordset();

                    sql = "SELECT DATA_ENTRY_GRID.*,stock  FROM DATA_ENTRY_GRID LEFT join stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no;

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //    //dgv1.Rows.Clear();
                    //for (a=0; a< dgv1.RowCount-1;a++)
                    //{
                    //    dgv1.Rows.RemoveAt(a);
                    //    }
                    int i = 0;
                    // dgv1.Rows.Add(rec.RecordCount+1);

                      dgv1.Rows.Clear()  ;    dgv1.Refresh();



                    // foreach (DataGridViewRow row in this.dgv1.Rows)
                    while (!rec.EOF)
                    {
                        //ds2.Tables[0].Rows.Add();
                        dgv1.Rows.Add();
                        dgv1["Item_Code", i].Value = rec.Fields["Item_Code"].Value.ToString();
                        dgv1["price", i].Value = rec.Fields["PRICE"].Value.ToString();
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();
                        dgv1["site", i].Value = rec.Fields["plot"].Value.ToString();
                        dgv1["Unit", i].Value = rec.Fields["Unit"].Value.ToString();
                        dgv1["stock", i].Value = rec.Fields["stock"].Value.ToString();
                        dgv1["Selected", i].Value = 1;

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



                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                        i = i + 1;
                        rec.MoveNext();

                    }

                    find_total();
                    cmdterm.Enabled = true;
                }


                else
                {
                    MessageBox.Show("Invalid MRN Number OR No More Item Remaining", "Invalid Entry");
                }



            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void search_order()
        {



            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset rec = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            rec = new ADODB.Recordset();
            tmp = new ADODB.Recordset();

            try
            {
                toolpost.Enabled = true;
                lblpost.Text = "New Order";
                cmdterm.Enabled = false;


                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();

                sql = "SELECT * FROM DATA_ENTRY WHERE TRN_TYPE=12 AND INVOICE_NO= '" + Txtporder.Text.Trim() + "'";
                rec = new ADODB.Recordset();
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //if (tmp.Fields[0].Value)


                isedit = false;
                if (rec.RecordCount > 0)
                {
                    isedit = true;
                    cmdterm.Enabled = true;
                    txttrn.Text = rec.Fields["TRAN_NO"].Value.ToString();
                    dt1.Value = (DateTime) rec.Fields["CURDATE"].Value;
                    TxtmrnNo.Text = rec.Fields["order_no"].Value.ToString();
                    //rec.Fields["ACCODE"].Value =cmbproject.SelectedValue;
                    txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();
                    txtqout.Text = rec.Fields["QOUT_NO"].Value.ToString();
                    txtmrnrefno.Text = rec.Fields["remarks"].Value.ToString();
                    //cmbproject.FindString(rec.Fields["ACCODE"].Value.ToString());
                    //cmbrequestor.FindString(rec.Fields["sales_code"].Value.ToString());

                    cmbwh.SelectedValue = rec.Fields["WHCODE"].Value;

                    cmbproject.Text = rec.Fields["entry_type"].Value.ToString();
                    cmbrequestor.SelectedValue = rec.Fields["sales_code"].Value;
                    // ENTRY TYPE USING FOR SUPPLIER

                    cmbsup.SelectedValue = rec.Fields["ACCODE"].Value;
                    toolpost.Enabled = true;
                    lblpost.Text = "Order Not Posted";
                    
                    if (rec.Fields["POSTED"].Value.ToString() == "P")
                    {
                            toolpost.Enabled = false;
                            lblpost.Text="Order Posted";
                            tooldelete.Enabled = false;
                           // saveToolStripButton.Enabled = false;
                        

                             if (Gvar._SuperUserid != 1)
                         {
                            saveToolStripButton.Enabled = false;
                         }
                    }
                    //cmbcost.SelectedValue = rec.Fields["cost_code"].Value;
                    decimal rec_no = (decimal) rec.Fields["REC_NO"].Value;
                    txtqout.Text = rec.Fields["QOUT_NO"].Value.ToString();
                    Txtdisc.Text = rec.Fields["DISC_AMT"].Value.ToString();
                    //sql = "SELECT * FROM DATA_ENTRY WHERE INVOICE_NO = '" + TxtmrnNo.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    //rec = new ADODB.Recordset();
                    //rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    sql = "SELECT * FROM ORDER_SET WHERE ORDER_NO = '" + Txtporder.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);
                    rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount != 0)
                    {



                          Txtporder.Text =rec.Fields["ORDER_NO"].Value.ToString();
                          txturref.Text = rec.Fields["UREF"].Value.ToString();
                           txtdtime.Text= rec.Fields["DTIME"].Value.ToString();
                           txtdplace.Text = rec.Fields["DPLACE"].Value.ToString();
                           txttpayment.Text = rec.Fields["tpayment"].Value.ToString();
                         txtother.Text=rec.Fields["other"].Value.ToString();

                    }

                    var a = 0;

                    rec = new ADODB.Recordset();

                    sql = "SELECT DATA_ENTRY_GRID.*,stock  FROM DATA_ENTRY_GRID left join stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no;

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    //    //dgv1.Rows.Clear();
                    //for (a=0; a< dgv1.RowCount-1;a++)
                    //{
                    //    dgv1.Rows.RemoveAt(a);
                    //    }
                    int i = 0;
                    // dgv1.Rows.Add(rec.RecordCount+1);

                      dgv1.Rows.Clear()  ;    dgv1.Refresh();



                    // foreach (DataGridViewRow row in this.dgv1.Rows)
                    while (!rec.EOF)
                    {
                        //ds2.Tables[0].Rows.Add();
                        dgv1.Rows.Add();
                        dgv1["Item_Code", i].Value = rec.Fields["Item_Code"].Value.ToString();
                        dgv1["price", i].Value = rec.Fields["PRICE"].Value.ToString();
                        dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                        dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                        dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();
                        dgv1["site", i].Value = rec.Fields["plot"].Value.ToString();
                        dgv1["Unit", i].Value = rec.Fields["Unit"].Value.ToString();
                        dgv1["stock", i].Value = rec.Fields["stock"].Value.ToString();
                        dgv1["Selected", i].Value = 1;






                        dgv1["remarks", i].Value = rec.Fields["REMARKS"].Value.ToString();
                        i = i + 1;
                        rec.MoveNext();

                    }

                    find_total();
                }


                else
                {
                    MessageBox.Show("Invalid Purchase Order Number", "Invalid Entry");
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
            isedit = false;
              dgv1.Rows.Clear()  ;    dgv1.Refresh();
              dgv1.Refresh();
            TxtmrnNo.Text = "";
            txtrefno.Text = "";
            txttrn.Text = "";
            Txtporder.Text = "";
            txtdtime.Text = "";
            txturref.Text = "";
            txttpayment.Text = "";
            txtother.Text = "";
            txtdtime.Text = "";
            txtdplace.Text = "";
            cmdterm.Enabled = false;
            lblmsg.Text = "....";
            txtqout.Text = "";
            txttotal.Text = "0";
            Txtdisc.Text = "0";
            Txtnet.Text = "0";
            toolpost.Enabled = true;
            lblpost.Text = "New Order";
            tooldelete.Enabled = true;
            saveToolStripButton.Enabled = true;

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            grpterm.Visible = false;

        }

        //private void cmdok_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < grditems.Rows.Count; i++)
        //        {
        //            var a = grditems["selected", i].Value;

        //            if ((bool)a)
        //            {
        //                // MessageBox.Show(a);
        //                a = 1;
        //            }
        //        }

        //    }
        //    catch
        //    {

        //    }
        //}

        private void Txtporder_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Conn.Close();
                Conn.Open();
                if (checkBox1.Checked)
                    sql = "select Invoice_NO  as Order_No,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=12 and posted <> 'P' ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC";
                else
                    sql = "select Invoice_NO  as Order_No,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=12 ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = Txtporder.Left;
                GrdLookup.Top = Txtporder.Top + Txtporder.Height + groupBox1.Top;
                dv.AllowEdit = true;
                dv.Table = dt;
                GrdLookup.Tag = "ORD";
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
                lblmsg.Text = "Please Wait Report Generating.......";
                

                if (string.IsNullOrEmpty(Txtporder.Text)) return;

                ReportDocument CrRep = new ReportDocument();


                rep_path = Application.StartupPath + "\\reports\\Reciept_ORD.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{DATA_ENTRY.INVOICE_NO} = '" + Txtporder.Text.Trim() + "' and {DATA_ENTRY.TRN_TYPE} =" +Convert.ToInt32(txttrn_type.Text);


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
                lblmsg.Text = "....";

            }
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

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
                    if (dgv1["qty", i].Value == null) dgv1["qty", i].Value = 0;
                    if (string.IsNullOrEmpty(dgv1["price", i].Value.ToString())) dgv1["price", i].Value = 0;
                    if (Convert.IsDBNull(dgv1["qty", i].Value)) dgv1["qty", i].Value = 0;
                    price = Convert.ToDouble(dgv1["price", i].Value) * Convert.ToDouble(dgv1["qty", i].Value);
                    dgv1["total", i].Value = price;
                    tot = tot + price;
                }
                if (string.IsNullOrEmpty(Txtdisc.Text.ToString())) Txtdisc.Text = "0";
                double disc = Convert.ToDouble(Txtdisc.Text) ;
                double net = 0;

                net = tot - disc;
                txttotal.Text = tot.ToString();
                Txtnet.Text = net.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdterm_Click(object sender, EventArgs e)
        {
            grpterm.Left = cmbwh.Left;
            grpterm.Visible = !grpterm.Visible;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            // SqlTransaction tran = Conn.BeginTransaction();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try{

            ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
            Conn.Close();
            Conn.Open();
            //tran = Conn.BeginTransaction();
            tmp = new ADODB.Recordset();
            
           
            ADODB.Recordset rec = new ADODB.Recordset();
            sql = "SELECT * FROM ORDER_SET WHERE ORDER_NO = '" + Txtporder.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

            if (rec.RecordCount == 0)
            {

                rec.AddNew();
                

            }

            rec.Fields["ORDER_NO"].Value = Txtporder.Text;
            rec.Fields["UREF"].Value = txturref.Text.Trim();
                            
            rec.Fields["trn_type"].Value = Convert.ToInt32(txttrn_type.Text);
            rec.Fields["PROJECT"].Value = cmbproject.Text;
            rec.Fields["DTIME"].Value = txtdtime.Text.Trim();
            rec.Fields["DPLACE"].Value = txtdplace.Text.Trim();
            rec.Fields["tpayment"].Value = txttpayment.Text.Trim();
            rec.Fields["other"].Value = txtother.Text.Trim();
           
            rec.Fields["BRN_code"].Value = Gvar._brn_code;
                       
            rec.Update();
            MessageBox.Show("Successfully Added Terms");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Txtdisc_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttotal.Text.ToString())) txttotal.Text = "0";
            if (string.IsNullOrEmpty(Txtdisc.Text.ToString())) Txtdisc.Text = "0";
            Txtnet.Text = (Convert.ToDouble(txttotal.Text) - Convert.ToDouble(Txtdisc.Text)).ToString();
        }

        private void chkdesc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdesc.Checked==true)
                dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            else
                dgv1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void toolpost_Click(object sender, EventArgs e)
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
            string sql;
            ADODB.Recordset cus = new ADODB.Recordset();
            ADODB.Recordset tmp = new ADODB.Recordset();
            // SqlTransaction tran = Conn.BeginTransaction();
            cus = new ADODB.Recordset();
            tmp = new ADODB.Recordset();
            double trn_no;
            try
            {

                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                //tran = Conn.BeginTransaction();
                tmp = new ADODB.Recordset();


                ADODB.Recordset rec = new ADODB.Recordset();
                sql = "UPDATE DATA_ENTRY SET POSTED='P' WHERE  INVOICE_NO = '" + Txtporder.Text.Trim() + "' AND TRN_TYPE=" + Convert.ToInt32(txttrn_type.Text);

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                toolpost.Enabled = false;
                lblpost.Text = "Order Posted";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        // }










    }
}










