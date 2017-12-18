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

namespace Stockex
{

    
    public partial class Frmrequester : Form
  {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        int ac_code;
        int start_no;
        int end_no;
        int cur_no;
        //string sql;
        
        private void save_form()
        {
            try
            {



                dgv1.EndEdit();
                DataGridViewCell ccell = dgv1.CurrentCell;
                dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                dgv1.CurrentCell = ccell;

                ada.Update(ds, "accounts");

                string sql = "update acc_type set cur_no = (select max(acc_no) from accounts where acc_type_code=" + ac_code + ") where acc_type_code=" + ac_code;


                //rd.Close();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();

               // ada.Update(dt);
               
                //dt.AcceptChanges();
            }
            
            catch (System.Exception excep)
            {

               // MessageBox.Show(excep.Message);

            }
        }

        private void load_leaders()
        {
            Conn.Close();
            Conn.Open();


            string sql = "select top 1 emp_ac_type from ac_options";
            bool find;
            
            //rd.Close();
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader rd = cmd.ExecuteReader();
            find = false;
            while (rd.Read())
            {

                ac_code = Convert.ToInt32(rd[0].ToString());
                find = true;

            }
            if (!find)
            {
                MessageBox.Show("Please Define the Supplier Account Type Code on Ac_option Table on Database", "Wrong Account Type Code");
                return;
            }
            rd.Close();


            sql = "select * from acc_type where acc_type_code=" + ac_code;
          

            //rd.Close();
             cmd = new SqlCommand(sql, Conn);
            rd = cmd.ExecuteReader();
            find = false;
            while (rd.Read())
            {

                start_no = Convert.ToInt32(rd["start_no"].ToString());
                end_no = Convert.ToInt32(rd["end_no"].ToString());

                cur_no = Convert.ToInt32(rd["cur_no"].ToString());
                
                
                
                find = true;

            }
            if (!find)
            {
                MessageBox.Show("Please Define the Account detail on Acc_Type Table on Database", "Wrong Account Type Code");
                return;
            }
            rd.Close();


            //sql = "select Empno,Employee_name,Tele_Home from Leaders";

            //SqlCommand cmd = new SqlCommand(sql, Conn);

            
            //SqlDataAdapter ada = new SqlDataAdapter(cmd);
            ada = new SqlDataAdapter("select ACC_NAME,ACC_MOBILE_NO,ACC_FAX_NO,CONTACT_PERSON,ACC_NO  ,ACC_TYPE_CODE  froM accounts WHERE ACC_TYPE_CODE = " + ac_code + " order by acc_no", Conn);
            ///ada.TableMappings.Add("Table", "Leaders");
            ds = new DataSet();
            DataTable dt = new DataTable("accounts");
            dt.AcceptChanges();
            ///ada.Fill(dt);

            this.ada.Fill(this.ds, "accounts");
            dv.Table = dt;
            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

     
            this.ds.AcceptChanges();
            //set the table as the datasource for the grid in order to show that data in the grid
           
            dgv1.Visible = true;
           

            
            
            dv.AllowEdit = true;
            dv.AllowNew = true;
            dv.AllowDelete = true;
            
           /// dv.Table = dt;
            
            dgv1.DataSource = ds;
            dgv1.DataMember = "accounts";
            
            
           dgv1.Columns[0].Width = 300;
           dgv1.Columns[1].Width = 150;
           dgv1.Columns[2].Width = 150;
           dgv1.Columns[3].Width = 175;
            dgv1.Visible = true;

            dgv1.Columns[4].ReadOnly = true;
            dgv1.Columns[5].ReadOnly = true;
            dgv1.Columns[4].Name = "Acc_no";
            dgv1.Columns[5].Name = "Acc_type_code";
            



            //OdbcDataAdapter ada = new OdbcDataAdapter(cmd);

            
            //dt = new DataTable("Leaders");
            //ada.Fill(dt);


            //dataGrid1.DataSource=dt.DefaultView();
            //dataGrid1.DataSource=
            //    .DataContext = dt.DefaultView;
        }

        private void delete_leaders()
        {
            dgv1.Select();
        }

  
        public Frmrequester()
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
            if(string.IsNullOrEmpty(dgv1["Acc_no",e.RowIndex].Value.ToString()))
            {
                dgv1["Acc_no",e.RowIndex].Value=++cur_no;
                dgv1["Acc_type_code",e.RowIndex].Value=ac_code;
            }

        }
    }
}
