using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.VisualBasic;
using CrystalDecisions.CrystalReports.Engine;
using ADODB;
namespace FinOrg
{
    public partial class frmGLTran : Form
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
        string cur_col;
        int dblclk_row;
        int fcol;
        string last_col;
        int last_row;
        int lascolno;


        public frmGLTran()
        {
            InitializeComponent();
            txtpriv.Text = Gvar.frm_priv.ToString();
            load_form();

        }

        private void FrmMRNEntry_Load(object sender, EventArgs e)
        {
            int trn = Gvar.trntype;
            txttrn_type.Text = trn.ToString();
        }

        private void load_form()
        {

            try
            {
                Conn.Close();
                Conn.Open();



               
                    if (txtpriv.Text.Substring(2, 1) == "0")
                    {
                        tooldelete.Visible = false;
                        return;
                    }
               


                if (Gvar.trntype == 100)
                {
                    txtdrcr.Text = "C";
                    cmbproject.SelectedIndex = 0;
                    chksms.Visible = true;

                }

                if (Gvar.trntype == 200)
                {
                    txtdrcr.Text = "D";
                    cmbproject.SelectedIndex = 1;

                }




                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                ds2.AcceptChanges();

                //set the table as the datasource for the grid in order to show that data in the grid

                // dgv1.Columns[0].DataPropertyName = "LEADER_NAME";

                sql = "select [Col1],[Col2],[Col3] FROM [Grid_Master] where doc_no='1'";
                //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";

                //SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                //DataTable dt = new DataTable("Grid_Master");
                dt.AcceptChanges();
                //ada2.Fill(ds2, "Grid_Master");
                dgv1.Visible = true;
                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;

                sql = "select Trn_code,Abrv from Trn_Type ";

                SqlDataAdapter adatrn = new SqlDataAdapter(sql, Conn);
                DataTable dttrn = new DataTable("Trn_Type");
                adatrn.Fill(dttrn);

                //DataGridViewComboBoxColumn trn = new DataGridViewComboBoxColumn();
                //trn.DisplayMember = "Abrv";
                //trn.ValueMember = "Trn_code";
                //trn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                //trn.DataSource = dttrn;
                //dgv1.Columns.Add(trn);
                //dgv1.Columns[dgv1.Columns.Count-1].Width = 200;
                //dgv1.Columns[dgv1.Columns.Count - 1].Name = "trntype";
                //dgv1.Columns[dgv1.Columns.Count - 1].HeaderText = "Document Type";
                //dgv1.Rows.Add(10);
                

                for (int i =0;i<dgv1.ColumnCount-1;i++)

                {
                    if (dgv1.Columns[i].Visible)
                    {
                        lstgridcols.Items.Add(dgv1.Columns[i].HeaderText);
                        lstgridcols.SetItemChecked(lstgridcols.Items.Count - 1, true);
                    }
                }

                sql = "select CURRENCY_code,CURRENCY_code +' : ' +  cast(CURRENCY_RATE as varchar) AS CURRENCY from currency_master ";

                SqlDataAdapter adacurr = new SqlDataAdapter(sql, Conn);
                DataTable dtcurr = new DataTable("currency");
                adacurr.Fill(dtcurr);

                cmbcurrency.DisplayMember = "CURRENCY";
                cmbcurrency.ValueMember = "CURRENCY_code";

                cmbcurrency.DataSource = dtcurr;
                cmbcurrency.SelectedIndex = 0;

                sql = "select Branch_code,Branch_Name from BRANCHES ";

                SqlDataAdapter adabrn = new SqlDataAdapter(sql, Conn);
                DataTable dtbrn = new DataTable("branches");
                adabrn.Fill(dtbrn);

                cmbbranch.DisplayMember = "Branch_Name";
                cmbbranch.ValueMember = "Branch_code";

                cmbbranch.DataSource = dtbrn;
                cmbbranch.SelectedIndex = 0;

                
            
                            sql = "select *  from ACC_TRN_OPTION where id=" +Gvar.Gind;
                        
                


                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd["SH_COSTCODE"].ToString()))
                        {
                            dgv1.Columns["CostCode"].Visible = Convert.ToBoolean(rd["SH_COSTCODE"].ToString());
                          



                        }

                        if (!string.IsNullOrEmpty(rd["SH_division"].ToString()))
                        {
                            dgv1.Columns["DeptCode"].Visible = Convert.ToBoolean(rd["SH_division"].ToString());




                        }


                        if (!string.IsNullOrEmpty(rd["sh_branch"].ToString()))
                        {
                            dgv1.Columns["branch"].Visible = Convert.ToBoolean(rd["sh_branch"].ToString());




                        }

                        if (!string.IsNullOrEmpty(rd["SH_trntype"].ToString()))
                        {
                            dgv1.Columns["trntype"].Visible = Convert.ToBoolean(rd["SH_trntype"].ToString());




                        }
                        if (!string.IsNullOrEmpty(rd["SH_docno"].ToString()))
                        {
                            dgv1.Columns["DocNo"].Visible = Convert.ToBoolean(rd["SH_docno"].ToString());




                        }

                    }


                }
                if (cmbbranch.Items.Count < 2)
                {
                    cmbbranch.Visible = false;
                    dgv1.Columns["branch"].Visible = false;
                }
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
                    if (dgv1.CurrentCell.RowIndex < dgv1.RowCount - 1 && dgv1.CurrentCell.ColumnIndex == dgv1.Columns["narration"].Index )
                    {
                       // dgv1.CurrentCell = dgv1["accno1", cur_row + 1];
                        SendKeys.Send("{Right}");
                    }
                    else
                    {

                       //dgv1.CurrentCell = dgv1[dgv1.CurrentCell.ColumnIndex+1, cur_row];
                        SendKeys.Send("{Right}");
                    }

                   
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


                dgv1_CellDoubleClick(null, null);

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
            if (!GrdLookup.Visible)
            {
                dgv1_DoubleClick(sender, e);
            }

            if (GrdLookup.Visible == true)
            {
                //dgv1.EndEdit();


                //dgv1.BeginEdit(false);

                if (last_col == "CostCode")
                {

                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "Cost_Code LIKE  '%" + txt + "%' OR Cost_name LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "Cost_Code <> '0'";

                }
                else if (last_col == "DeptCode")
                {

                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "Dept_Code LIKE  '%" + txt + "%' OR Dept_name LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "Dept_Code <> '0'";

                }
                else
                {



                    string txt = textBox1.Text.Trim();
                    if (txt != "")
                    {
                        dv.RowFilter = "acc_no LIKE  '%" + txt + "%' OR acc_name LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "acc_no <> '0'";

                }
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

                            if (last_col == "accno1")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;

                               // dgv1.CurrentCell = dgv1["accamt", last_row];
                                dgv1.BeginEdit(false);
                                dgv1["accno1", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                dgv1["Accname1", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                dgv1.EndEdit();
                                //search_data(dgv1["accno1", dblclk_row].Value.ToString());
                               // this.dgv1.CurrentCell = this.dgv1["accamt", dblclk_row];
                                GrdLookup.Visible = false;
                                dgv1.Focus();

                                return;
                                //e.Handled = true;
                                //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                            }


                            
                            if (last_col == "CostCode")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;

                                dgv1.CurrentCell = dgv1["CostCode", dblclk_row];

                                dgv1.BeginEdit(false);
                                dgv1["CostCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                dgv1["CostName", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                // dgv1["accname2", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                dgv1.EndEdit();

                                GrdLookup.Visible = false;

                                this.dgv1.CurrentCell = this.dgv1["CostCode", dblclk_row];

                                dgv1.Focus();
                            }

                            if (last_col == "DeptCode")
                            {
                                int lkprow = 0;


                                lkprow = GrdLookup.CurrentCell.RowIndex;

                                dgv1.CurrentCell = dgv1["DeptCode", dblclk_row];

                                dgv1.BeginEdit(false);
                                dgv1["DeptCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                                dgv1["DeptName", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                // dgv1["accname2", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                                dgv1.EndEdit();

                                GrdLookup.Visible = false;

                                this.dgv1.CurrentCell = this.dgv1["DeptCode", dblclk_row];

                                dgv1.Focus();
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
            
            if (msg.WParam.ToInt32() == (int)Keys.Enter && acntrl == "dgv1" && !GrdLookup.Visible)
            {
                // SendKeys.Send("{Tab}");

                //keyData = Keys.Tab;
                if (dgv1.CurrentCell == dgv1["Narration", cur_row])
                {
                    if (dgv1.CurrentCell.RowIndex < dgv1.RowCount - 1)
                    {
                        dgv1.CurrentCell = dgv1["drcr", cur_row + 1];
                    }
                    else
                    {
                        dgv1.CurrentCell = dgv1["drcr", cur_row];
                    }

                }
                else
                {
                    SendKeys.Send("{Right}");
                    return true;

                }
                
                {
                    dgv1.EndEdit();
                    
                   
                   

                    //dgv1_KeyDown(null, new KeyEventArgs(Keys.Enter));
                 
                   // return true;
                    return base.ProcessCmdKey(ref msg, keyData);
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
                if(dgv1.CurrentCell.ColumnIndex==1 && dgv1["drcr",e.RowIndex].Value==null )
                {
                    dgv1["drcr", e.RowIndex].Value = "Debit";
                }
                    

                acntrl = dgv1.Name;
                cur_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                cur_row = dgv1.CurrentCell.RowIndex;
                if (GrdLookup.Visible)
                {
                    textBox1.Focus();
                    return;
                }
               
                DataGridView d = (DataGridView)sender;

                if (d[e.ColumnIndex, e.RowIndex].EditType.ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
                    SendKeys.Send("{F4}");




               



                

                if (dgv1.Columns[e.ColumnIndex].ReadOnly)
                {
                    if (lascolno > e.ColumnIndex) SendKeys.Send("{Left}"); ;
                    if (dgv1.ColumnCount - 1 == e.ColumnIndex && dgv1.RowCount - 1 != e.RowIndex)
                    {
                        // dgv1.CurrentCell = dgv1[0, e.RowIndex + 1];
                        //dgv1.CurrentCell.ColumnIndex = 0;
                        // SendKeys.Send("{Down}");
                        Application.Idle += new EventHandler(Application_Idle);


                    }
                    else



                        if (lascolno < e.ColumnIndex) SendKeys.Send("{Right}");

                    if (dgv1["Accname1", cur_row].Value == "")

                        dgv1.CurrentCell = dgv1["accno1", cur_row];


                }
            }
            catch (Exception ex)
            {
            }




        }




        void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(Application_Idle);
            dgv1.CurrentCell = dgv1[0, last_row + 1];
        }

        private void search_data(string Code,string colname,int colno)
        {
            try
            {


                Conn.Close();
                Conn.Open();


                switch (colname)
                {
                    case  "Accname1":
                    case  "accname2":
  
                {
                    sql = "sELECT  ACC_NAME FROM ACCOUNTS WHERE ACC_NO='" + Code + "'";
                }
                        break;


                    case "CostName":
                   
                        {
                            sql = "sELECT  Cost_NAME FROM Cost_Master WHERE Cost_code='" + Code + "'";
                        }
                        break;

                    case "DeptName":
                        {
                            sql = "sELECT  Dept_NAME FROM Dept_Master WHERE Dept_code='" + Code + "'";
                        }
                        break;
                }


               
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {

                            dgv1[colname, last_row].Value = rd[0].ToString();



                        }

                    }


                }
                else
                {
                    MessageBox.Show("Invalid Code Found, Please check Again", "Invalid Item");
                    //dgv1[colno, last_row].Value = "";
                    dgv1[colname, last_row].Value = "";
                   
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

                    case "TRN":

                     
                     txttrn.Text = GrdLookup.Rows[lkprow].Cells[0].Value.ToString();
                       

                        GrdLookup.Visible = false;

                        search_entry();

                        txttrn.Focus();
                        break;
                    case "accno1":

                        dgv1.CurrentCell = dgv1["accno1", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["accno1", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        dgv1["Accname1", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        //this.dgv1.CurrentCell = this.dgv1["accamt", dblclk_row];

                        dgv1.Focus();
                        break;
                    

                    case "CostCode":

                        dgv1.CurrentCell = dgv1["CostCode", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["CostCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        dgv1["CostName", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                       // dgv1["accname2", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["CostCode", dblclk_row];

                        dgv1.Focus();
                        break;
                    case "DeptCode":

                        dgv1.CurrentCell = dgv1["DeptCode", dblclk_row];

                        dgv1.BeginEdit(false);
                        dgv1["DeptCode", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[0].Value;
                        dgv1["DeptName", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        // dgv1["accname2", dblclk_row].Value = GrdLookup.Rows[lkprow].Cells[1].Value;
                        dgv1.EndEdit();

                        GrdLookup.Visible = false;

                        this.dgv1.CurrentCell = this.dgv1["DeptCode", dblclk_row];

                        dgv1.Focus();
                        break;

                }


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
            fcol = dgv1.FirstDisplayedCell.ColumnIndex;

        }

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                lascolno = e.ColumnIndex;
                last_row = dgv1.CurrentCell.RowIndex;
                if (dgv1[0, e.RowIndex].Value == null ) return;
                
                if (!GrdLookup.Visible)
                {
                   
                    //string celval = Convert.ToString(dgv1["Description", cur_row].Value);
                    //if (last_col > 6 && celval.Trim() == "") dgv1.CurrentCell = dgv1["Item_Code", cur_row];
                    dgv1.EndEdit();

                    if ((dgv1.Columns[e.ColumnIndex].Name == "accno1" ) & dgv1[e.ColumnIndex, e.RowIndex].Value != null)
                    {
                        if (dgv1[e.ColumnIndex, e.RowIndex].Value == null)
                            dgv1[e.ColumnIndex, e.RowIndex].Value = 0;

                        search_data(dgv1[e.ColumnIndex, e.RowIndex].Value.ToString(), dgv1.Columns[e.ColumnIndex + 1].Name, e.ColumnIndex);


                    }

                    if (dgv1.Columns[e.ColumnIndex].Name == "CostCode")
                    {
                        if (dgv1[e.ColumnIndex, e.RowIndex].Value == null)
                            dgv1[e.ColumnIndex, e.RowIndex].Value = 0;

                        search_data(dgv1[e.ColumnIndex, e.RowIndex].Value.ToString(), "CostName", e.ColumnIndex);


                    }


                    if (dgv1.Columns[e.ColumnIndex].Name == "DeptCode")
                    {

                        if (dgv1[e.ColumnIndex, e.RowIndex].Value == null)
                            dgv1[e.ColumnIndex, e.RowIndex].Value = 0;
                        search_data(dgv1[e.ColumnIndex, e.RowIndex].Value.ToString(), "DeptName", e.ColumnIndex);


                    }



                    if (e.ColumnIndex == dgv1["accamt", e.RowIndex].ColumnIndex)
                    {

                        find_total();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }






        private void saveToolStripButton_Click(object sender, EventArgs e)
        {


            try
            {
                save_payment();
            }
            catch (SqlException ex)
            {
            }
        }



        private void TxtmrnNo_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                Conn.Close();
                Conn.Open();

                SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME,   CURDATE from  DATA_ENTRY WHERE  TRN_TYPE=90  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("DATA_ENTRY");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = txttrn.Left;
                GrdLookup.Top = txttrn.Top + txttrn.Height + btnsavetofile.Top;
                dv.AllowEdit = true;
                dv.Table = dt;
                GrdLookup.Tag = "MRN";
                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                textBox1.Text = "";
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
            dgv1.Rows.Clear(); dgv1.Refresh();
            dgv1.Rows.Add(25);
           
            txtrefno.Text = "";
            txttrn.Text = "";
            txtdrtotal.Text = "";
            txtremark.Text = "";
            isedit = false;
            saveToolStripButton.Enabled = true;
        }
        private void find_total()
        {
            try
            {
                double price;
                double totdr;
                double totcr;
                price = 0;
                totdr = 0;
                totcr = 0;
                dgv1.EndEdit();
                for (int i = 0; i < dgv1.RowCount; i++)
                {
                    if (!Convert.IsDBNull(dgv1["accno1", i].Value)) //dgv1["Item_Code", i].Value = 0;
                    {
                        if (dgv1["accno1", i].Value != null)
                        {

                            if (dgv1["accamt", i].Value == null) dgv1["accamt", i].Value = 0;
                            price = Convert.ToDouble(dgv1["accamt", i].Value);
                            if (dgv1["drcr", i].Value.ToString().Substring(0,1)=="D")
                            {
                                totdr = totdr + price;

                            }
                            else
                            {
                                totcr = totcr + price;


                            }

                                

                        }
                    }
                }
                txtdrtotal.Text = totdr.ToString();
                txtcrtotal.Text = totcr.ToString();
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


                if (string.IsNullOrEmpty(txttrn.Text)) return;

                ReportDocument CrRep = new ReportDocument();


                rep_path = Application.StartupPath + "\\reports\\Reciept_SRVC.rpt";

                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{DATA_ENTRY.INVOICE_NO} = '" + txttrn.Text.Trim() + "' and {DATA_ENTRY.TRN_TYPE} =" + Convert.ToInt32(txttrn_type.Text);


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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv1.CurrentCell == null) dgv1.CurrentCell = dgv1["accno1", cur_row];
            if (dgv1.CurrentCell == dgv1["accno1", cur_row] )
                //SqlCommand cmd =  SqlCommand;
            {
                dblclk_row = dgv1.CurrentCell.RowIndex;
                Conn.Close();
                Conn.Open();

                if (dgv1.CurrentCell.ColumnIndex == dgv1["accno1", 0].ColumnIndex)
                {
                    switch (Gvar.Gind)
                    {
                        case 1:
                            {
                                sql = "select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS WHERE ACC_TYPE_CODE =2  ORDER BY ACC_NAME";

                                break;
                            }

                        case 2:
                            {
                                sql = "select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS WHERE ACC_TYPE_CODE =3 ORDER BY ACC_NAME";

                                break;
                            }

                        case 3:
                        case 5:
                            {
                                sql = "select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS WHERE ACC_TYPE_CODE =1 and acc_level=4 ORDER BY ACC_NAME";

                                break;
                            }
                    }
                    

                }
                else
                {
                    sql = "select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS inner join ac_options on level3_no=cash_ac_type AND ID =1 and  ACC_TYPE_CODE =1 and acc_level=4   ORDER BY ACC_NAME";

                   

                }

                //if (chkcustomer.Checked)
                //{
                //    sql ="select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS WHERE ACC_TYPE_CODE =1 ORDER BY ACC_NAME";

                    
                //}
                //else
                //{
                //    sql = "select CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME FROM ACCOUNTS WHERE ACC_TYPE_CODE NOT IN (99,1) ORDER BY ACC_NAME";
                //}

                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("ACCOUNTS");
                adalkp.Fill(dtlkp);

                var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                GrdLookup.Left = cellRectangle.Left + cellRectangle.Width+dgv1.Left;
                GrdLookup.Top = textBox1.Top + textBox1.Height;
                last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                if (dgv1.CurrentCell == dgv1["accno1", cur_row])
                    GrdLookup.Tag = "accno1";
               
                dv.Table = dtlkp;
                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                textBox1.Text = "";
                GrdLookup.Visible = true;
                textBox1.Text = "";
                textBox1.Focus();

            }


            if (dgv1.CurrentCell == dgv1["CostCode", cur_row])
            {
                dblclk_row = dgv1.CurrentCell.RowIndex;
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select CAST(Cost_Code AS VARCHAR) AS Cost_Code,Cost_Name FROM Cost_master", Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("cost_master");
                adalkp.Fill(dtlkp);

                var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                GrdLookup.Left = cellRectangle.Left + cellRectangle.Width + dgv1.Left;
                GrdLookup.Top = textBox1.Top + textBox1.Height;
                last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                if (dgv1.CurrentCell == dgv1["CostCode", cur_row])
                    GrdLookup.Tag = "CostCode";
                

                dv.Table = dtlkp;
                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                GrdLookup.Visible = true;
                textBox1.Text = "";
                
                textBox1.Focus();

            }

            if (dgv1.CurrentCell == dgv1["DeptCode", cur_row])
            {
                dblclk_row = dgv1.CurrentCell.RowIndex;
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand("select CAST(Dept_Code AS VARCHAR) AS Dept_Code,Dept_Name FROM Dept_master", Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Dept_master");
                adalkp.Fill(dtlkp);

                var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                GrdLookup.Left = cellRectangle.Left + cellRectangle.Width + dgv1.Left;
                GrdLookup.Top = textBox1.Top + textBox1.Height;
                last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                if (dgv1.CurrentCell == dgv1["DeptCode", cur_row])
                    GrdLookup.Tag = "DeptCode";


                dv.Table = dtlkp;
                GrdLookup.DataSource = dv;
                GrdLookup.Columns[0].Width = 170;
                GrdLookup.Columns[1].Width = 300;
                textBox1.Text = "";
                GrdLookup.Visible = true;
                textBox1.Focus();

            }

        }

        private void save_payment()
        {
            try
            {
                ADODB.Recordset tmp = new ADODB.Recordset();
                ADODB.Recordset rec = new ADODB.Recordset();
                ADODB.Recordset rec1 = new ADODB.Recordset();
                string sql = "";
                string DR_CR = "D";
                string DR_CR1 = "C";
                long doc_no = 0;
                int TRN_BY = 100;

                long trnno = 0;
                long trnno2 = 0;
                string trnlist="-0";
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

                if (txtdrtotal.Text == "") txtdrtotal.Text = "0";
                if (txtcrtotal.Text == "") txtcrtotal.Text = "0";

                if(Convert.ToDouble(txtdrtotal.Text)!= Convert.ToDouble(txtcrtotal.Text))
                {
                    MessageBox.Show("Debit Amount not Match with Credit Amount, Pleas check ", "Invalid Amount Entry ");
                    return;

                }

                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                iserror = false;



                find_total();

                int i;
                bool itemfound = false;
                iserror = true;
                for (i = 0; i < dgv1.RowCount; i++)
                {
                    // i=i+1;
                    string celval = Convert.ToString(dgv1["accamt", i].Value);

                    if (celval.Trim() != "")
                    {
                        itemfound = true;
                        if (dgv1["accno1", i].Value == "" || dgv1["accno1", i].Value == null)
                        {
                            dgv1.CurrentCell = dgv1["accno1", i];
                            MessageBox.Show("Invalid Entry on Row " + ++i);
                            return;
                        }





                        if (dgv1["accno1", i].Value != null && dgv1["accamt", i].Value == null)
                        {
                            MessageBox.Show("Invalid Amount Entry on Row " + ++i);
                            dgv1.CurrentCell = dgv1["accamt", i];
                            return;
                        }


                    }



                    if (itemfound == false)
                    {
                        MessageBox.Show("No Item found to Save!", "Invalid Entry");
                        return;
                    }


                    object a;

                    if (!isedit)
                    {
                        tmp = new ADODB.Recordset();

                        sql = "SELECT MAX(ENTRY_NO) FROM TRAN_ACC";

                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        //if (tmp.Fields[0].Value)
                        //    txttrn.Text="1";
                        //else

                        if (tmp.Fields[0].Value == DBNull.Value)
                            trnno = 1;
                        else
                            trnno = Convert.ToInt64(tmp.Fields[0].Value) + 1;


                        txttrn.Text = trnno.ToString();

                        //TxtissueNo.Text = Gvar.trn_no(Convert.ToInt32(txttrn_type.Text));
                    }

                    ADOconn.BeginTrans();
                    tmp = new ADODB.Recordset();
                    rec = new ADODB.Recordset();




                    TRN_BY = 250;




                    sql = "select * from TRAN_ACC where  ENTRY_no = " + Convert.ToDouble(txttrn.Text);

                    rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();
                    }


                    rec.Fields["EntrY_no"].Value = txttrn.Text.Trim();
                    rec.Fields["Reference_No"].Value = txtrefno.Text;
                    rec.Fields["TRN_by"].Value = TRN_BY;
                    rec.Fields["DR_CR"].Value = "D";
                    rec.Fields["user_ID"].Value = Gvar.Userid;
                    rec.Fields["NARRATION"].Value = txtremark.Text;
                    rec.Fields["pay_date"].Value = dt1.Value;
                    rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                    rec.Fields["AMOUNT"].Value = Convert.ToDouble(txtdrtotal.Text) * Convert.ToDouble(txtrate.Text);
                    rec.Fields["CURRENCY_CODE"].Value = cmbcurrency.SelectedValue;
                    rec.Fields["CURRENCY_RATE"].Value = txtrate.Text;
                    rec.Fields["FAMOUNT"].Value = Convert.ToDouble(txtdrtotal.Text);
                    rec.Update();


                    
                    sql = "DELETE from GRID_MASTER where  TRN_BY = " + TRN_BY + " AND doc_no = '" + txttrn.Text.ToString() + "'";
                    ADOconn.Execute(sql, out a, -1);
                    sql = "select top 1 * from GRID_MASTER";
                    ADODB.Recordset gridrec = new ADODB.Recordset();

                    gridrec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    string Nar1 = "";
                    string Nar2 = " GL Voucher ";

                   
                    for (i = 0; i < dgv1.RowCount; i++)
                    {
                        // i=i+1;
                         celval = Convert.ToString(dgv1["accamt", i].Value);

                        if (celval.Trim() != "")
                        {

                           // DataGridViewComboBoxCell trntype = (DataGridViewComboBoxCell)dgv1["trntype", i];

                            if (dgv1["drcr", i].Value.ToString() == "Debit")
                                DR_CR = "D";
                            else
                            {
                                DR_CR = "C";
                            }
                            if (dgv1["trnno1", i].Value == null || dgv1["trnno1", i].Value == "") dgv1["trnno1", i].Value = 0;

                            if (dgv1["CostCode", i].Value == null || dgv1["CostCode", i].Value == "") dgv1["CostCode", i].Value = 0;
                            if (dgv1["DeptCode", i].Value == null || dgv1["DeptCode", i].Value == "") dgv1["DeptCode", i].Value = 0;


                            sql = "select * from TRN_accounts where  trn_no = " + Convert.ToInt32(dgv1["trnno1", i].Value);
                            rec = new ADODB.Recordset();
                            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            trnno = Convert.ToInt32(dgv1["trnno1", i].Value);

                            if (dgv1["accamt", i].Value == null || dgv1["accamt", i].Value == "") dgv1["accamt", i].Value = "0";
                            if (rec.RecordCount > 0 && Convert.ToDecimal(dgv1["accamt", i].Value) < 1)
                            {

                                sql = "delete  from TRN_accounts where trn_by = " + TRN_BY + " and   trn_no in (" + trnno + "," + trnno2 + ")";


                                ADOconn.Execute(sql, out a, -1);

                                goto nextline;
                            }
                            if (rec.RecordCount == 0)
                            {
                                rec.AddNew();
                                tmp = new ADODB.Recordset();
                                sql = "SELECT * FROM TRNNO";

                                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                                //if (tmp.Fields[0].Value)
                                //    txttrn.Text="1";
                                //else

                                if (tmp.Fields[0].Value == DBNull.Value)
                                {
                                    trnno = 1;
                                }
                                else
                                {
                                    trnno = Convert.ToInt64(tmp.Fields[0].Value.ToString());
                                    trnno2 = trnno + 1;
                                }
                            }
                            trnlist = trnlist + "," + trnno.ToString() + "," + trnno2.ToString();
                            dgv1["trnno1", i].Value = trnno;

                            if(i==0)
                            doc_no = trnno * -1;

                            dgv1["trnno1", i].Value = trnno;

                            rec.Fields["acc_no"].Value = dgv1["ACCNO1", i].Value;
                            rec.Fields["EntrY_no"].Value = txttrn.Text.Trim();
                            rec.Fields["trn_no"].Value = trnno;
                            rec.Fields["PAY_AMOUNT"].Value = dgv1["accamt", i].Value;

                            rec.Fields["PAY_AMOUNT"].Value = Convert.ToDecimal(dgv1["accamt", i].Value) * Convert.ToDecimal(txtrate.Text); ;
                            double frate = Gvar.Get_Currency_rate(Convert.ToDouble(dgv1["ACCNO1", i].Value), cmbcurrency.SelectedValue.ToString());
                            rec.Fields["f_pay_amount"].Value = Convert.ToDouble(dgv1["accamt", i].Value) * Convert.ToDouble(txtrate.Text) / frate;
                            rec.Fields["F_RATE"].Value = frate;
                            rec.Fields["currency"].Value = cmbcurrency.SelectedValue;




                            rec.Fields["TRN_BY"].Value = TRN_BY;
                            rec.Fields["DR_CR"].Value = DR_CR;
                            rec.Fields["user_ID"].Value = Gvar.Userid;
                            rec.Fields["PAYBY"].Value = 0;
                            //rec.Fields["RQTY"].Value = 0;
                            rec.Fields["SNO"].Value = i;
                            // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                            rec.Fields["NARRATION"].Value = Nar2 + dgv1["NARRATION", i].Value;
                            rec.Fields["Voucher_No"].Value = dgv1["DocNo", i].Value;
                            //object v = trntype.Items[0].ToString();
                            rec.Fields["trn_type"].Value = dgv1["trntype", i].Value; ;
                            rec.Fields["cost_code"].Value = dgv1["CostCode", i].Value;
                            rec.Fields["dept_code"].Value = dgv1["DeptCode", i].Value;
                            rec.Fields["pay_date"].Value = dt1.Value;
                            rec.Fields["doc_no"].Value = doc_no;
                            if (dgv1["CostCode", i].Value == "") dgv1["CostCode", i].Value = 0;
                            // rec.Fields["CostCode"].Value = dgv1["CostCode", i].Value;
                            rec.Fields["Voucher_No"].Value = dgv1["DocNo", i].Value;
                            rec.Fields["trn_no2"].Value = 0;
                            rec.Fields["NYEAR"].Value = dt1.Value.Year;
                            rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                            rec.Update();




                            gridrec.AddNew();
                            gridrec.Fields["doc_no"].Value = txttrn.Text.ToString();
                            gridrec.Fields["TRN_BY"].Value = TRN_BY;

                            gridrec.Fields["row"].Value = i + 1;
                            for (int j = 0; j < dgv1.ColumnCount; j++)
                            {
                                int coli = j + 1;
                                string fld = "col" + coli;
                                if (dgv1[j, i].Value == null) dgv1[j, i].Value = "";
                                if (dgv1[j, i].Value == null || dgv1[j, i].Value == "" && (j == 5 || j == 6)) dgv1[j, i].Value = "0";
                                gridrec.Fields[fld].Value = dgv1[j, i].Value.ToString();
                            }
                            gridrec.Update();

                        nextline: ;


                        }
                    }
                }

                object a1;
                sql = "DELETE from trn_accounts where  TRN_BY = " + TRN_BY + " AND entry_no = '" + txttrn.Text.ToString() + "' and trn_no not in (" +  trnlist  + ")";
                ADOconn.Execute(sql, out a1, -1);

                ADOconn.CommitTrans();

                //sql = "SELECT     A.ACC_NO, A.ACC_NAME, I.ACC_MOBILE_NO, S.DR_AMOUNT, S.CR_AMOUNT,S.DR_AMOUNT-S.CR_AMOUNT AS balamt,T1.PAY_AMOUNT as payamt FROM ACCOUNTS AS A INNER JOIN  ACCOUNTS_INFO AS I  ON A.ACC_NO = I.ACC_NO " +
                //" INNER JOIN   TRN_ACC_SUM AS S ON I.ACC_NO = s.ACC_NO inner join tran_acc as t on  t.trn_by=100 and entry_no='" + txttrn.Text.Trim() + "' "+
                //" inner join trn_accounts as t1 on t1.trn_by=t.trn_by and t1.entry_no=t.entry_no and t1.dr_cr=t.dr_cr   where i.send_sms = 1 and len(ACC_MOBILE_NO) > 9";
               
                
                //if (chksms.Checked && chksms.Visible)
                //{
                //    rec1 = new ADODB.Recordset();
                //    rec1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                //    if (rec1.RecordCount != 0)
                //    {
                //        try
                //        {
                //            Gvar.build_sms(rec1);
                //        }
                //        catch (Exception ex)
                //        {

                //        }
                //    }
                //}
                      
                MessageBox.Show("Record Added/Updated Successfully");
            }
            catch (SqlException ex)
            {
                saveToolStripButton.Enabled = true;
                MessageBox.Show(ex.Message);
                ADOconn.RollbackTrans();
            }

            catch (Exception ex1)
            {
                saveToolStripButton.Enabled = true;
                MessageBox.Show(ex1.Message);
                ADOconn.RollbackTrans();
            }
        }

       

        private void btnfilesave_Click_1(object sender, EventArgs e)
        {


        }

        private void BtnSavefile_Click(object sender, EventArgs e)
        {
            gpfile.Text = "Save To File";
            lstgridcols.Visible = true;
            btnsaveload.Text = "SaveFile";

            gpfile.Visible = !gpfile.Visible;

        }

        private void button1_Click()
        {
            try
            {
               


                if (txtfilename.Text.Trim() != "")
                    sf1.FileName = txtfilename.Text;

                sf1.InitialDirectory = Application.StartupPath;
                sf1.Filter = "CSV File|*.Csv";

                
                DialogResult res = sf1.ShowDialog();
        if (res == DialogResult.OK)
        {                
         
               
                txtfilename.Text = sf1.FileName;

                txtfilename.Text = txtfilename.Text.Replace(".Csv", "");
                txtfilename.Text = txtfilename.Text + ".Csv";
                //File.Create(Application.StartupPath + "\\" + txtfilename.Text) ;
                //StreamWriter sw = new StreamWriter(Application.StartupPath + "\\" + txtfilename.Text);
                StreamWriter sw = new StreamWriter(txtfilename.Text);
                string txt;
                string str1;
                str1 = "";

                bool[] ary = new bool[lstgridcols.Items.Count - 1];

                for (int i = 0; i < lstgridcols.Items.Count - 1; i++)
                {
                    ary[i] = lstgridcols.GetItemChecked(i);


                }

                for (int r = 0; r < dgv1.Rows.Count - 1; r++)
                {

                    if (dgv1[0, r].Value != null && dgv1[0, r].Value != "")
                    {

                        for (int i = 0; i < dgv1.ColumnCount ; i++)
                        {

                            if (i < lstgridcols.Items.Count-1 )
                            {

                                if (ary[i] && dgv1[i, r].Value!=null)
                                    txt = dgv1[i, r].Value.ToString();
                                else
                                    txt = "";
                            }
                            else
                            {
                                if (dgv1[i, r].Value != null)
                                    txt = dgv1[i, r].Value.ToString();
                                else
                                    txt = "";
                            }


                            str1 = str1 + txt + ",";

                        }
                        sw.WriteLine(str1);
                    }
                }




                sw.Close();
               //returns a string for the directory
          
        }
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            

        }

        private void btnopen_Click()
        {
            try
            {
                if (txtfilename.Text.Trim() != "")
                    op1.FileName = txtfilename.Text;

                else


                    op1.FileName = "";

                op1.InitialDirectory = Application.StartupPath;
                op1.Filter = "CSV File|*.Csv";

               
                DialogResult res = op1.ShowDialog();
                if (res == DialogResult.OK)
                {


                    txtfilename.Text = op1.FileName;

                    int i = 0;
                         int r = 0;
                    string line;

                    // Read the file and display it line by line.
                    System.IO.StreamReader file =
                       new System.IO.StreamReader(txtfilename.Text);
                    while ((line = file.ReadLine()) != null)
                    {
                       // Console.WriteLine(line);
                       
                 
                        string[] words = line.Split(',');
                        i=0;
	                        foreach (string txt in words)
	                        {
                                try
                                {
                                    dgv1[i, r].Value = txt;
                                    i++;
                                }
                                catch
                                { }
	                        }
                            r++;
                       }






                    file.Close();
                    //returns a string for the directory

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            gpfile.Visible = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            gpfile.Text = "OpenFromFile";
            lstgridcols.Visible = false;
            btnsaveload.Text = "OpenFile";
            gpfile.Visible = !gpfile.Visible;

            
                btnopen_Click();
                gpfile.Visible = false;

        }

        private void btnsaveload_Click(object sender, EventArgs e)
        {
            button1_Click();
        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            search_entry();
        }

        private void txttrn_TextChanged(object sender, EventArgs e)
        {
            saveToolStripButton.Enabled = false;
        }

        private void txttrn_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Conn.Close();
                Conn.Open();

                int TRN_BY = 100;
                if (txtdrcr.Text.ToString().Trim() == "C")
                {
                   
                    TRN_BY = 100;
                }
                else
                {
                    
                    TRN_BY = 200;
                }

                SqlCommand cmd = new SqlCommand("select Entry_no,pay_date,   pay_AMOUNT,acc_name,accounts.acc_no from  TRN_ACCOUNTS left join accounts on TRN_ACCOUNTS.acc_no=accounts.acc_no   WHERE   TRN_BY= " + TRN_BY + "  ORDER BY ENTRY_NO  DESC", Conn);

                SqlDataAdapter ada = new SqlDataAdapter(cmd);


                DataTable dt = new DataTable("TRAN_ACC");
                ada.Fill(dt);


                // dataGrid1.DataContext = dt.DefaultView;
                GrdLookup.Left = txttrn.Left;
                GrdLookup.Top = txttrn.Top + txttrn.Height + btnsavetofile.Top;
                dv.AllowEdit = true;
                dv.Table = dt;
                GrdLookup.Tag = "TRN";
                GrdLookup.DataSource = dv;
               // GrdLookup.Columns[0].Width = 170;
               // GrdLookup.Columns[1].Width = 300;
                textBox1.Text = "";
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
            private void search_entry()
            {
             
                try
                {
                    if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                iserror = false;
              
              
              
                        string DR_CR = "D";
                    string DR_CR1 = "C";
                    int TRN_BY = 200;

                if (txtdrcr.Text.ToString().Trim() == "C")
                {
                    DR_CR = "D";
                    DR_CR1 = "C";
                    TRN_BY = 100;
                }
                else
                {
                    DR_CR = "C";
                    DR_CR1 = "D";
                    TRN_BY = 200;
                }
                if (txttrn.Text == null || txttrn.Text=="") txttrn.Text = "0";
                    string entryno;
                    entryno = txttrn.Text;
                    load_ini();
                    txttrn.Text=entryno;
                sql = "select t.Reference_No,t.TRN_BY,t.DR_CR,t.pay_date,t.NARRATION,t.currency_code,t.currency_rate , g.* from TRAN_ACC as t inner join grid_master as g on t.trn_by=g.trn_by and  t.entry_no=g.doc_no where  t.trn_by = "+ TRN_BY + " and  t.ENTRY_no = " + Convert.ToDouble(txttrn.Text) + " order by g.row ";

                isedit = false;
                 ADODB.Recordset gridrec = new ADODB.Recordset();
                gridrec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                 if (gridrec.RecordCount >0)
                 {
                     isedit = true;
                     txtrefno.Text =  gridrec.Fields["reference_no"].Value.ToString();
                     dt1.Value = Convert.ToDateTime( gridrec.Fields["pay_date"].Value.ToString());
                     txtremark.Text = gridrec.Fields["NARRATION"].Value.ToString();
                     cmbcurrency.SelectedValue = gridrec.Fields["currency_code"].Value;
                     txtrate.Text = gridrec.Fields["currency_rate"].Value.ToString();
                     int i =0;
                     do
                     {
                         for (int j = 0; j < dgv1.ColumnCount; j++)
                         {
                             int coli = j + 1;
                             string fld = "col" + coli;
                             dgv1[j, i].Value = gridrec.Fields[fld].Value.ToString();
                         }

                         gridrec.MoveNext();
                         i++;
                     } while (!gridrec.EOF);

                     gridrec.Close();
                     Conn.Close();
                     if(txtpriv.Text.Substring(2,1)=="1")
                     saveToolStripButton.Enabled = true;
                    }
                 }
                
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);
                            }
            }

            private void txtentryno_TextChanged(object sender, EventArgs e)
            {
                if (txtentryno.Text != null)
                {
                    txttrn.Text = txtentryno.Text;
                    search_entry();
                }
            }

            private void txttrn_KeyDown(object sender, KeyEventArgs e)
            {
                //if (!isedit) return;
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (txttrn.Text==null || txttrn.Text=="") return;
                        search_entry();
                        //SendKeys.Send("{Tab}");
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

                    case Keys.Up:
                        if (txttrn.Text == null || txttrn.Text.Trim() == "") return;
                        txtentryno.Text = (Convert.ToInt64(txttrn.Text) + 1).ToString();
                        e.Handled = true;
                        break;
                    case Keys.Down:
                        if (txttrn.Text == null || txttrn.Text.Trim() == "" || txttrn.Text == "0") return;
                        txtentryno.Text = (Convert.ToInt64(txttrn.Text) - 1).ToString();
                        e.Handled = true;
                        break;

                }


            
            }

            private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void chkcustomer_CheckedChanged(object sender, EventArgs e)
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

            private void frmAccTran_KeyUp(object sender, KeyEventArgs e)
            {
                if (dgv1.CurrentCell == null) return;
                if (dgv1.CurrentCell.IsInEditMode && (cur_col  == "accno1" ) && !GrdLookup.Visible && acntrl == "dgv1")
                {
                    dgv1_CellDoubleClick(null, null);
                    dgv1.EndEdit();
                    dgv1.BeginEdit(false);
                    if (dgv1.CurrentCell.Value != null)
                        textBox1.Text = dgv1.CurrentCell.Value.ToString();
                    textBox1.Focus();
                    textBox1.SelectionStart = textBox1.Text.Length;

                }
            }

        }
    }










    

