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

namespace FinOrg
{


    public partial class FrmUsers : FinOrgForm
    {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string sql;

        bool isini = true;
       


        private void save_form()
        {
            try
            {


                dgv1.EndEdit();
                dgv1.CurrentCell = dgv1["userid", 0];
                if (string.IsNullOrEmpty(dgv1["Superuser", dgv1.CurrentCell.RowIndex].Value.ToString()) && !string.IsNullOrEmpty(dgv1["userid", dgv1.CurrentCell.RowIndex].Value.ToString()))
                    dgv1["Superuser", dgv1.CurrentCell.RowIndex].Value = false; ;

                ada.Update(ds, "UserInfo");


                // ada.Update(dt);

                //dt.AcceptChanges();
            }

            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }
        }

        private void load_Users()
        {

            try
            {
                //dgv1.Rows.Clear();
                dgv1.Columns.Clear();

                Conn.Close();
                Conn.Open();





                //dgv1.AutoGenerateColumns = false;




                sql = "SELECT     UserId, UserName,' ' as pass , Superuser, Group_Name,Password,WR_CODE,brn_code,MENU_DOCK,SH_TOPMENU,SH_SIDEMENU FROM         dbo.UserInfo";
                


                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                ds = new DataSet();



                this.ada.Fill(this.ds, "UserInfo");

                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);




                sql = "select distinct Group_name from UserPriv";

                SqlDataAdapter cost = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");

                DataSet dscost = new DataSet();

                lstgroup.Enabled = true;
                
                cost.Fill(dscost, "UserPriv");
                lstgroup.DisplayMember = "Group_name";
               // cmbcost.ValueMember = "Cost_code";
                lstgroup.DataSource = dscost.Tables[0];



                sql = "select WR_CODE,WR_NAME from WRHOUSE_MASTER";

                SqlCommand cmd2 = new SqlCommand(sql, Conn);
                DataTable dt2 = new DataTable("WRHOUSE_MASTER");

                DataSet wrs = new DataSet();

                SqlDataAdapter wr = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");

                DataSet wr1 = new DataSet();

                listBox1.Enabled = true;

                wr.Fill(wrs, "WRHOUSE_MASTER");
                listBox1.DisplayMember = "WR_NAME";
                listBox1.ValueMember = "WR_CODE";
                
                listBox1.DataSource = wrs.Tables[0];
                //listBox1.Visible = true;

                sql = "sELECT  * from branches";

                SqlDataAdapter adabranch = new SqlDataAdapter(sql, Conn);

                DataTable dtbranch = new DataTable("branch");
                adabranch.Fill(dtbranch);



                lstbranch.DisplayMember = "branch_name";
                lstbranch.ValueMember = "branch_code";
                lstbranch.DataSource = dtbranch;
                

                dgv1.Visible = true;




                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;

                /// dv.Table = dt;

               

                //sql = "select distinct Group_name from UserPriv";

                //SqlCommand cmd1 = new SqlCommand(sql, Conn);
                //DataTable dt = new DataTable("UserPriv");
                //dt.AcceptChanges();
                //SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                //ada1.Fill(dt);
                //dv.Table = dt;
                //DataGridViewComboBoxColumn colbox = new DataGridViewComboBoxColumn();
                //colbox.DataSource = dt;
                //colbox.DataPropertyName = "Group_name";
                
                //colbox.DefaultCellStyle = dgv1.DefaultCellStyle;

                //colbox.HeaderText = "Group_name";
                ////colbox.ValueMember = "LEADER_NO";
                //colbox.DisplayMember = "Group_name";
                ////colbox.Items.Add("as");
                ////   colbox.Items.Add("bs");
                //dgv1.Columns.Add(colbox);


                //sql = "select WR_CODE,WR_NAME from WRHOUSE_MASTER";

                //SqlCommand cmd2 = new SqlCommand(sql, Conn);
                //DataTable dt2 = new DataTable("WRHOUSE_MASTER");
                //dt2.AcceptChanges();
                //SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                //ada2.Fill(dt2);
                //dv1.Table = dt2;
                //DataGridViewComboBoxColumn colbox1 = new DataGridViewComboBoxColumn();
                //colbox1.DataSource = dt2;
                //colbox1.DataPropertyName = "WR_NAME";

                //colbox1.DefaultCellStyle = dgv1.DefaultCellStyle;

                //colbox1.HeaderText = "WR_NAME";
                //colbox1.ValueMember = "WR_CODE";
                //colbox1.DisplayMember = "WR_NAME";
                ////colbox.Items.Add("as");
                ////   colbox.Items.Add("bs");
                //dgv1.Columns.Add(colbox1);

                dgv1.DataSource = ds.Tables[0];

                dgv1.Refresh();
                dgv1.Refresh();
                //dgv1.Columns[0].Width = 150;
                //dgv1.Columns[2].Width = 300;
                dgv1.Visible = true;
               dgv1.Columns[4].ReadOnly = true;
               dgv1.Columns[6].ReadOnly = true;
                dgv1.Columns[0].Name="UserId";
                dgv1.Columns[1].Name="UserName";
                dgv1.Columns[2].Name="Pass";
                dgv1.Columns[3].Name="Superuser";
                dgv1.Columns[4].Name="Group_Name";
                dgv1.Columns[5].Name = "Password";
                dgv1.Columns[6].Name = "wr_code";
                dgv1.Columns[7].Name = "brn_code";
                dgv1.Columns[5].Visible = false;
                //lstgroup.Location = 
               

                //lstgroup.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;
                lstgroup.Location = dgv1.GetCellDisplayRectangle(4, 1, false).Location;
                lstgroup.Visible = true;
               // dgv1_password();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_leaders()
        {
            dgv1.Select();
        }


        public FrmUsers()
        {
            InitializeComponent();
            
            load_Users();
            dgv1.Width = this.Width;
            isini = false;
        }

       

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            load_Users();

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save_form();
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }


            

        
        

        private void cmbapprove_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Users();
        }

        private void tooldelete_Click(object sender, EventArgs e)
        {

        }

        private void FrmUsers_Load(object sender, EventArgs e)
        {

        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv1["Group_Name", e.RowIndex].ColumnIndex && string.IsNullOrWhiteSpace( dgv1["Group_Name", e.RowIndex].Value.ToString()))
            {
                lstgroup.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                lstgroup.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top + dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                if (lstgroup.Items.Count > 0)
                {
                    lstgroup.Visible = true;
                    if (dgv1.CurrentCell == null) return;
                    string v = dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value.ToString();
                    lstgroup.Text = v;

                }
            }

            if (e.ColumnIndex == dgv1["wr_code", e.RowIndex].ColumnIndex && string.IsNullOrWhiteSpace(dgv1["wr_code", e.RowIndex].Value.ToString()))
            {
                listBox1.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                listBox1.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top + dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                if (listBox1.Items.Count > 0)
                {
                    listBox1.Visible = true;
                    if (dgv1.CurrentCell == null) return;
                    string v = dgv1["wr_code", dgv1.CurrentCell.RowIndex].Value.ToString();
                    listBox1.Text = v;

                }
            }

            if (e.ColumnIndex == dgv1["pass", e.RowIndex].ColumnIndex && !string.IsNullOrWhiteSpace( dgv1["userid", e.RowIndex].Value.ToString()))
            {
                //txtpass.RightToLeft = true;
                txtpass.Location = dgv1.Location;// -dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location; // +dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                txtpass.Left = dgv1.Location.X + 240; // dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location.X; // +dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                txtpass.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top; // +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                txtpass.Text = dgv1["password", e.RowIndex].Value.ToString();
                txtpass.Visible = true;
                txtpass.Width = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                txtpass.Height = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height-25;
                txtpass.Focus();
            }

               
        }

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv1.CurrentCell != dgv1["Group_Name", e.RowIndex])
            lstgroup.Visible = false;

            if (dgv1.CurrentCell != dgv1["wr_code", e.RowIndex])
                listBox1.Visible = false;
        }

        private void lstgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dgv1.CurrentCell == null) return;
            //string v = lstgroup.Text;
            //dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value = v;
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgv1.CurrentCell == dgv1["Group_Name", dgv1.CurrentCell.RowIndex] && (msg.WParam.ToInt32() == (int)Keys.Up || msg.WParam.ToInt32() == (int)Keys.Down))
            {
                // SendKeys.Send("{Tab}");

                if (!lstgroup.Visible)
                    return base.ProcessCmdKey(ref msg, keyData);

               // keyData = Keys.Tab;
             if(msg.WParam.ToInt32() == (int)Keys.Up)
             {
                 if (lstgroup.SelectedIndex>0) lstgroup.SelectedIndex=lstgroup.SelectedIndex-1;
             }

                if(msg.WParam.ToInt32() == (int)Keys.Down)
             {
                 if (lstgroup.SelectedIndex<lstgroup.Items.Count-1) lstgroup.SelectedIndex=lstgroup.SelectedIndex+1;
             }
                //return base.ProcessCmdKey(ref msg, Keys.Up);
                //return base.ProcessCmdKey(ref msg, keyData);
                return true;
            }


            if (msg.WParam.ToInt32() == (int)Keys.Escape)
            {
                lstbranch.Visible = false;
                lstgroup.Visible = false;
                
            }
           

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FrmUsers_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;
                    if (dgv1.CurrentCell == null || !lstgroup.Visible) return;
                    string v = lstgroup.Text;
                    dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value = v;
                    lstgroup.Visible = false;
                    break;
                    break;


                     if (dgv1.CurrentCell == null || !listBox1.Visible) return;
                     v = lstgroup.Text;
                    dgv1["wr_code", dgv1.CurrentCell.RowIndex].Value = v;
                    listBox1.Visible = false;
                    break;
                    break;
                case Keys.Shift:
                    //DataGridViewCellEventArgs p this.dgv1.CurrentCell.;
                   // dgv1_CellDoubleClick(sender, this.e);

                    break;

                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    lstgroup.Visible = false;
                    listBox1.Visible = false;
                    //e.Handled = true;

                    break;

            }
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            string v = txtpass.Text;
            dgv1["password", dgv1.CurrentCell.RowIndex].Value = v;
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    dgv1.CurrentCell = dgv1["Superuser", dgv1.CurrentCell.RowIndex];
                    dgv1.Focus();
                    break;
                case Keys.Right:
                    dgv1.CurrentCell = dgv1["Superuser", dgv1.CurrentCell.RowIndex];
                    dgv1.Focus();
                    break;
                case Keys.Left:
                    dgv1.CurrentCell = dgv1["UserName", dgv1.CurrentCell.RowIndex];
                    dgv1.Focus();
                    break;
                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    lstgroup.Visible = false;
                    listBox1.Visible = false;
                    //e.Handled = true;

                    break;

            }
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv1["Group_Name", e.RowIndex].ColumnIndex)
            {
                lstgroup.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                lstgroup.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                if (lstgroup.Items.Count > 0)
                {
                    lstgroup.Visible = true;
                    if (dgv1.CurrentCell == null) return;
                    string v = dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value.ToString();
                    lstgroup.Text = v;

                }
            }


            if (e.ColumnIndex == dgv1["wr_code", e.RowIndex].ColumnIndex)
            {
                listBox1.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                listBox1.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top + dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                if (listBox1.Items.Count > 0)
                {
                    listBox1.Visible = true;
                    if (dgv1.CurrentCell == null) return;
                    string v = dgv1["wr_code", dgv1.CurrentCell.RowIndex].Value.ToString();
                    listBox1.Text = v;

                }
            }


            if (e.ColumnIndex == dgv1["brn_code", e.RowIndex].ColumnIndex)
            {

                var cellRectangle = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                lstbranch.Top = dgv1.Top + cellRectangle.Top + cellRectangle.Height;
                lstbranch.Left = cellRectangle.Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                //lstbranch.Left = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgv1.Left;// +dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
               // lstbranch.Top = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Top + dgv1.Top + dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;
                if (lstbranch.Items.Count > 0)
                {
                    lstbranch.Visible = true;
                    if (dgv1.CurrentCell == null) return;
                    string v = dgv1["brn_code", dgv1.CurrentCell.RowIndex].Value.ToString();
                    lstbranch.Text = v;

                }
            }
        }

        private void lstgroup_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            string v = lstgroup.Text;
            dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value = v;
            lstgroup.Visible = false;
        }

        private void lstgroup_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (dgv1.CurrentCell == null) return;
                    string v = lstgroup.Text;
                    dgv1["Group_Name", dgv1.CurrentCell.RowIndex].Value = v;
                    lstgroup.Visible = false;
                    break;
                
                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    lstgroup.Visible = false;

                    //e.Handled = true;

                    break;

            }
        }

        private void dgv1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(dgv1["Superuser", dgv1.CurrentCell.RowIndex].Value.ToString()) && !string.IsNullOrEmpty(dgv1["userid", dgv1.CurrentCell.RowIndex].Value.ToString()))
                dgv1["Superuser", dgv1.CurrentCell.RowIndex].Value = false; ;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            string v = listBox1.SelectedValue.ToString();
            dgv1["wr_code", dgv1.CurrentCell.RowIndex].Value = v;
            listBox1.Visible = false;
            
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (dgv1.CurrentCell == null) return;
                    string v = listBox1.Text;
                    dgv1["wr_code", dgv1.CurrentCell.RowIndex].Value = v;
                    listBox1.Visible = false;
                    break;

                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    listBox1.Visible = false;

                    //e.Handled = true;

                    break;

            }
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstbranch_DoubleClick(object sender, EventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            string v = lstbranch.SelectedValue.ToString();
            dgv1["brn_code", dgv1.CurrentCell.RowIndex].Value = v;
            lstbranch.Visible = false;
        }

        private void lstbranch_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (dgv1.CurrentCell == null) return;
                    string v = lstbranch.Text;
                    dgv1["brn_code", dgv1.CurrentCell.RowIndex].Value = v;
                    lstbranch.Visible = false;
                    break;

                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    lstbranch.Visible = false;

                    //e.Handled = true;

                    break;

            }
        }

        private void l(object sender, EventArgs e)
        {

        }

        private void dgv1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dgv1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //dgv1[8, e.RowIndex].Value = "Top";
            //dgv1[9, e.RowIndex].Value = 1;
            //dgv1[10, e.RowIndex].Value = 1;
        }

       

       
    }
}
