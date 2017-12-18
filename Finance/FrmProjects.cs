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

    
    public partial class frmproject : Form
  {
            SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
          ADODB.Connection ADOconn = new ADODB.Connection();
          SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
    
        
        
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string ac_code;
        int start_no;
        int end_no;
        int cur_no;
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        bool issearch;
        //string sql;

        private void save_form()
        {
            ADODB.Connection ADOconn = new ADODB.Connection();
            ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {
                //if (tooltip.Text.Trim().Length < 3)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 3 Digit!!");
                //    return;
                //}


                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();
               
                Conn.Close();
                // Conn.Open();

                bool isempty;
                isempty = false;

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

                

                try
                {
                    ADOconn.BeginTrans();
                if (!isedit)
                {

                    gen_accno();

                    tmp = new ADODB.Recordset();

                    //sql = "update ACC_TYPE SET CUR_NO = CUR_NO+1 WHERE  ACC_TYPE_CODE=" + cmbCostCode.SelectedValue;
                   // tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    
                }



                if (txtProjectAmt.Text.Trim() == "") txtProjectAmt.Text = "0";
                //if (txtvalue.Text.Trim() == "") txtvalue.Text = "0";

                //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());
                sql = "SELECT * FROM project_master where project_code ='" + txtprojectcode.Text.Trim() + "'";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();

                    }

                    if (txtProjectAmt.Text == "") txtProjectAmt.Text = "0";
                    if (txtpercent.Text == "") txtpercent.Text = "0";
                    
                    //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());
                    rec.Fields["Project_Code"].Value = txtprojectcode.Text.Trim();
                    rec.Fields["Project_Name"].Value = txtProjectname.Text.Trim();
                    rec.Fields["Client_code"].Value = cmbacc.SelectedValue;
                    rec.Fields["Cost_Code"].Value = Convert.ToInt32(txtprojectcode.Text); //cmbCostCode.SelectedValue;
                    rec.Fields["Start_date"].Value = dt1.Value.Date.ToString("yyyy-MM-dd"); ;
                    rec.Fields["End_Date"].Value =dtend.Value.Date.ToString("yyyy-MM-dd");
                    rec.Fields["Project_det"].Value = txtDescription.Text.Trim();
                    rec.Fields["completed"].Value = Convert.ToDouble(txtpercent.Text);

                     rec.Fields["Status"].Value = cmbstatus.Text.Trim();
                    rec.Fields["Incharge"].Value = cmbincharge.Text.Trim();
                    rec.Fields["Location"].Value = cmblocaltion.Text.Trim();
                    rec.Fields["Project_Amt"].Value = Convert.ToDouble(txtProjectAmt.Text);

                      
                    rec.Update();

                    sql = "select Cost_Code,Cost_name from Cost_Master where cost_code= " + Convert.ToInt32( txtprojectcode.Text);
                    rec = new ADODB.Recordset();

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();

                    }

                    rec.Fields["Cost_Code"].Value = Convert.ToInt32(txtprojectcode.Text);
                    rec.Fields["Cost_name"].Value = txtProjectname.Text.Trim();


                    rec.Update();



                    ADOconn.CommitTrans();
                    isedit = true;
                    MessageBox.Show("Successfully Saved");
                    load_leaders();
                }
                catch (Exception ex)
                {
                    ADOconn.RollbackTrans();
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        

        private void load_leaders()
        {

            Conn.Close();
            Conn.Open();
            //return;
            try
            {

                if(ADOconn.State==1)
                ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();

                string sql = "SELECT CUS_AC_TYPE,EMP_AC_TYPE FROM AC_OPTIONS  WHERE  ac_options.ID =1";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                // 
                object sl = 0;
                object emp = 0;
                if (rec.RecordCount > 0)
                {
                    sl = rec.Fields[0].Value;
                    emp = rec.Fields[1].Value;
                }

                isedit = true;
                 sql = "SELECT ACC_NO,ACC_NAME from Accounts where acc_type_code=" + sl; 

                SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                DataTable dt2 = new DataTable("Accounts");
                ada2.Fill(dt2);

                cmbacc.DisplayMember = "ACC_NAME";
                cmbacc.ValueMember = "ACC_NO";
                cmbacc.DataSource = dt2;


                sql = "SELECT ACC_NO,ACC_NAME from Accounts where acc_type_code=" + emp; 

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("Accounts");
                ada3.Fill(dt3);

                cmbincharge.DisplayMember = "ACC_NAME";
                cmbincharge.ValueMember = "ACC_NO";
                cmbincharge.DataSource = dt3;

                sql = "SELECT cost_code,COST_NAME from COST_MASTER";

                SqlDataAdapter cost = new SqlDataAdapter(sql, Conn);
                DataTable costdt = new DataTable("Accounts");
                cost.Fill(costdt);

                cmbCostCode.DisplayMember = "COST_NAME";
                cmbCostCode.ValueMember = "cost_code";
                cmbCostCode.DataSource = costdt;



                //sql = "SELECT status_code,STATUS_NAME from PROJ_STATUS";

                //SqlDataAdapter stada = new SqlDataAdapter(sql, Conn);
                //DataTable stdt = new DataTable("PROJ_STATUS");
                //stada.Fill(stdt);

                //cmbstatus.DisplayMember = "status_code";
                //cmbstatus.ValueMember = "status_name";
                //cmbstatus.DataSource = stdt;


                sql = "SELECT status_code,status_NAME from proj_status";

                SqlDataAdapter sts = new SqlDataAdapter(sql, Conn);
                DataTable stsdt = new DataTable("proj_status");
                sts.Fill(stsdt);

                cmbstatus.DisplayMember = "status_NAME";
                cmbstatus.ValueMember = "status_code";
                cmbstatus.DataSource = stsdt;

                ada = new SqlDataAdapter("SELECT CAST(PROJECT_code AS VARCHAR) AS PROJECT_CODE,PROJECT_NAME from Project_master ", Conn);
               

                //ada = new SqlDataAdapter("SELECT [Cus_code],[Cus_Name] ,[Id_Number] ,[Mobile]  FROM [Customer]", Conn);


                ///ada.TableMappings.Add("Table", "Leaders");
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                dt = new DataTable("Projects");
                //dt.AcceptChanges();
                ada.Fill(dt);
                //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

              //  dgv1.DataSource = ds;
                //this.ds.AcceptChanges();
                //set the table as the datasource for the grid in order to show that data in the grid

                dgv1.Visible = true;

                this.ada.Fill(this.ds, "Projects");
               
                dv.Table = dt;
                //textB


                //this.dgv1.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);

                isini = true;
              //dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv1_CellContentClick);
                //dgv1.CellContentClick -=  new DataGridViewCellEventHandler(sender  dgv1_CellContentClick ; //dataGridView1_CellContentClick;
                dgv1.DataSource = dv;
                dgv1.Visible = true;
               // dgv1.DataMember = "Accounts";
                dgv1.Columns[0].HeaderText = "Proj.Code";
                dgv1.Columns[1].HeaderText = "Proj. Name";
                //dgv1.Columns[2].HeaderText = "Sponsor";
                //dgv1.Columns[3].HeaderText = "Cus. ID ";
                //dgv1.Columns[4].HeaderText = "Mobile";
                //this.Left = 10;

                

                dgv1.Columns[1].Width = 350;

                isedit = false;
                isini = false;
               // dgv1.Columns[2].Width = 200;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void delete_leaders()
        {
            dgv1.Select();
        }


        public frmproject()
        {
            InitializeComponent(); 
            txtpriv.Text = Gvar.frm_priv.ToString();
        }

        private void FrmLeader_Load(object sender, EventArgs e)
        {

            //dt = dataGrid1.DataContext;
            //dt.BeginInit();
            





        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            load_leaders();

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

        private void FrmLeader_Activated(object sender, EventArgs e)
        {
            load_leaders();
        }

        private void tooldelete_Click(object sender, EventArgs e)
        {
            delete_leaders();
        }

        private void dgv1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

      

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                if (isini) return;
                dgv1.EndEdit();
                //DataGridViewCell ccell = dgv1.CurrentCell;
                //dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                //dgv1.CurrentCell = ccell;
                if (dgv1[e.ColumnIndex, e.RowIndex].Value==null) return;

                ac_code = dgv1[0, e.RowIndex].Value.ToString();



                string sql = @"SELECT    *      FROM         project_master where project_code='" + dgv1[0, e.RowIndex].Value + "' ";


                //rd.Close();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                bool find = false;
                txtprojectcode.Text = "";
                txtProjectname.Text = "";
                cmbacc.SelectedIndex = 1;

                
                txtProjectAmt.Text = "";


                txtprojectcode.Text = "";
                txtProjectname.Text = "";
                cmbacc.SelectedValue = -1;
                txtDescription.Text = "";
                cmbCostCode.SelectedValue = -1;
                cmblocaltion.SelectedIndex = -1;
                cmbstatus.Text = "";
                cmbincharge.Text = "";
                txtpercent.Text = "0";
                txtProjectAmt.Text = "0";
                dt1.Value = DateTime.Now;

                dtend.Value = DateTime.Now;
                while (rd.Read())
                {
                    try
                    {
                        isedit = true;
                        
                        txtprojectcode.Text = rd["project_code"].ToString();
                        txtProjectname.Text = rd["Project_NAME"].ToString();
                        cmbacc.SelectedValue = rd["client_code"].ToString();
                        txtDescription.Text = rd["Project_det"].ToString();
                       // cmbCostCode.SelectedValue = rd["cost_code"].ToString();

                        cmblocaltion.Text = rd["location"].ToString();
                        cmbstatus.SelectedItem= rd["status"].ToString();
                        cmbincharge.Text = rd["incharge"].ToString();
                           // if(!rd["percent"].ToString().Equals(DBNull.Value))
                        txtpercent.Text = rd["completed"].ToString();
                        txtProjectAmt.Text = rd["Project_amt"].ToString();
                       // txtProjectAmt.Text = rd["Start_date"].ToString();


                        if (!rd["Start_date"].Equals(DBNull.Value))
                        {
                            dt1.Value = Convert.ToDateTime(rd["Start_date"].ToString());
                        }

                        if (!rd["end_date"].Equals(DBNull.Value))
                        {
                            dtend.Value = Convert.ToDateTime(rd["end_date"].ToString());
                        }
                        
                        find = true;
                        isedit = true;
                        rd.Close();
                        break;
                    }
                    catch(Exception ex)  {

                        string err = ex.Message;
                        rd.Close();
                    }


                   


                    

                }
                rd.Close();



                
                
                 txtincome.Text = "0";
                
                 txtExpense.Text = "0";
                //rd.Close();
                 sql = @"SELECT     PAY_AMOUNT,DR_CR,PAY_DATE FROM TRN_ACCOUNTS 
                         WHERE ACC_NO =" + ac_code + " AND TRN_BY=12 AND SNO=-1";

                 cmd = new SqlCommand(sql, Conn);
                 rd = cmd.ExecuteReader();

                 while (rd.Read())
                     try{
                 {
                    
                 }

                     }
                catch{};
                 rd.Close();




                 
                 sql = @"SELECT     cr_amount-dr_amount  as balance FROM TRN_ACC_SUM 
                         WHERE ACC_NO ='" + ac_code + "'";

                 cmd = new SqlCommand(sql, Conn);
                 rd = cmd.ExecuteReader();
                 try
                 {
                     while (rd.Read())
                     {
                         if (Convert.ToDouble(rd["BALANCE"].ToString()) > 0)
                         {
                            // txtclcr.Text = Math.Abs(Convert.ToDouble(rd["BALANCE"].ToString())).ToString();
                         }
                         else
                         {
                             txtExpense.Text = Math.Abs(Convert.ToDouble(rd["BALANCE"].ToString())).ToString();
                         }
                     }
                 }
                 catch { }

                 rd.Close();
                
            }

                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            isedit = false;
            txtprojectcode.Text = "";
            txtProjectname.Text = "";
            cmbacc.SelectedIndex = 1;

           
            cmbacc.SelectedIndex = -1;

           
           // txtobcr.Text = "";
            txtincome.Text = "";
            //txtclcr.Text = "";
            txtExpense.Text = "";
            txtProjectAmt.Text = "";
            txtprojectcode.Text = "";
            txtProjectname.Text = "";
            cmbacc.SelectedValue = -1;
            txtDescription.Text = "";
            cmbCostCode.SelectedValue = -1;
            cmblocaltion.SelectedIndex=-1;
            cmbstatus.Text = "";
            cmbincharge.Text = "";
            txtpercent.Text = "0";
            txtProjectAmt.Text = "0";
            dt1.Value = DateTime.Now;

            dtend.Value = DateTime.Now;
            isedit = false;
        }


        private void gen_accno()
        {

            ADODB.Connection ADOconn = new ADODB.Connection();
            ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {
                //if (tooltip.Text.Trim().Length < 3)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 3 Digit!!");
                //    return;
                //}


                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();

                Conn.Close();
                // Conn.Open();






                tmp = new ADODB.Recordset();

                sql = "SELECT max(project_code)+1 FROM project_master";
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockReadOnly, 1);
                //if (tmp.Fields[0].Value)
                //    txttrn.Text="1";
                //else



                double trn;
                if (tmp.RecordCount != 0)
                {
                    trn = Convert.ToDouble(tmp.Fields[0].Value.ToString());
                    txtprojectcode.Text = trn.ToString();
                }
                else
               
                  trn = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
   

        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isedit==false)
            gen_accno();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Trim();
            if (txt != "")
            {
                dv.RowFilter = "Project_code LIKE  '%" + txt + "%' OR project_name LIKE '%" + txt + "%'";
            }
            else
                dv.RowFilter = "project_code <> '0'";
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }




    }
}
