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

    
    public partial class frmSponsor : Form
  {
          SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        
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



                //if (txtcuscode.Text.Trim() == "")
                //{
                //    MessageBox.Show("Invalid SponsorCode , Please enter a Valid Numeber!!", "Invalid Entry");
                //    return;

                //}


                if (txtcusname.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid Sponsor Name , Please enter a Valid Name!!", "Invalid Entry");
                    return;

                }
                    
                    //txtvehno.Text = "0";
                if (txtcuscode.Text.Trim() == "") txtcuscode.Text = "-0";

                //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());
                sql = "SELECT * FROM veh_sponsor where sponsor_code ='" + txtcuscode.Text.Trim() + "'";

                try
                {
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();
                    }




                    //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());
                    //rec.Fields["sponsor_code"].Value = Convert.ToDouble(txtcuscode.Text.Trim());
                    rec.Fields["sponsor_name"].Value = txtcusname.Text.Trim();
                    rec.Fields["contact_name"].Value = txtcontact.Text.Trim();
                    rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    rec.Fields["Mobile"].Value = txtmobileno.Text.Trim();
                    rec.Fields["Address"].Value = txtcusadd.Text.Trim();
                    rec.Update();
                    rec = new ADODB.Recordset();
                    rec.Open("SELECT @@IDENTITY", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //rec.GetRows();
                    txtcuscode.Text = rec.Fields[0].Value.ToString();
                    //rec.Requery();
                    //txtcuscode.Text = rec.Fields["sponsor_code"].Value.ToString();
                    MessageBox.Show("Successfully Saved");
                    load_leaders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            
        }

        private void  load_leaders()
        {
            Conn.Close();
            Conn.Open();

            try
            {

                object c = txtcuscode.Text;

                ada = new SqlDataAdapter("SELECT cast(Sponsor_code as varchar) as sponsor_code,[Sponsor_Name],Contact_Name  ,[Id_Number] ,Mobile,Address FROM [veh_sponsor] order by sponsor_name", Conn);
                //ada = new SqlDataAdapter("SELECT [Cus_code],[Cus_Name] ,[Id_Number] ,[Mobile]  FROM [Customer]", Conn);


                ///ada.TableMappings.Add("Table", "Leaders");
                ds = new DataSet();
                dt = new DataTable("veh_sponsor");
                //dt.AcceptChanges();
                ///ada.Fill(dt);
                //SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                //dgv1.DataSource = ds;
                //this.ds.AcceptChanges();
                //set the table as the datasource for the grid in order to show that data in the grid

                dgv1.Visible = true;

                this.ada.Fill(dt);
                dv.Table = dt;

                dgv1.DataSource = dv;
                dgv1.Visible = true;
                //dgv1.DataMember = "veh_sponsor";
                dgv1.Columns[0].HeaderText = "Spon.Code";
                dgv1.Columns[1].HeaderText = "Spon. Name";
                dgv1.Columns[2].HeaderText = "Contact Name";
                dgv1.Columns[3].HeaderText = "Spon. ID";
                dgv1.Columns[4].HeaderText = "Mobile";
                dgv1.Columns[5].HeaderText = "Address";
                dgv1.Columns[1].Width = 200;
                dgv1.Columns[2].Width = 200;
                
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

  
        public frmSponsor()
        {
            InitializeComponent(); 
            txtpriv.Text = Gvar.frm_priv.ToString();
        }

        private void FrmLeader_Load(object sender, EventArgs e)
        {

            //dt = dataGrid1.DataContext;
            //dt.BeginInit();

            load_leaders();




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

       
        private void tooldelete_Click(object sender, EventArgs e)
        {
            delete_leaders();
        }

       
      

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {

                Conn.Close();
                Conn.Open();
                dgv1.EndEdit();
                //DataGridViewCell ccell = dgv1.CurrentCell;
                //dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                //dgv1.CurrentCell = ccell;
                if (string.IsNullOrEmpty(dgv1[e.ColumnIndex, e.RowIndex].Value.ToString())) return;

                ac_code = dgv1[e.ColumnIndex, e.RowIndex].Value.ToString();



                string sql = "SELECT [sponsor_code],[Sponsor_Name], Contact_Name  ,[Id_Number] ,Mobile,Address FROM [veh_sponsor] where sponsor_code='" + ac_code + "'";


               //rd.Close();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                bool find = false;
                while (rd.Read())
                {

                    txtcuscode.Text = rd["sponsor_code"].ToString();
                    txtcusname.Text = rd["Sponsor_Name"].ToString();
                  txtcontact.Text = rd["contact_name"].ToString();

                    txtidnumber.Text = rd["Id_Number"].ToString();
                    txtmobileno.Text = rd["Mobile"].ToString();
                    txtcusadd.Text = rd["Address"].ToString();
                    find = true;

                }
                rd.Close();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

            txtcuscode.Text = "";
            txtcusname.Text = "";
            txtcontact.Text = "";
            txtidnumber.Text = "";
            txtmobileno.Text = "";
            txtcusadd.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string txt = textBox1.Text.Trim();



                if (txt != "")
                {
                    //issearch = false;

                
                        dv.RowFilter = "sponsor_code like '%" + txt + "%' OR sponsor_name LIKE '%" + txt + "%'";


                }

                else
               
                        dv.RowFilter = "sponsor_code <> '0'";
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
    }
}
