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

    
    public partial class frmcustomer : Form
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



                //if (txtvehno.Text.Trim() == "") txtvehno.Text = "0";
               if (txtcuscode.Text.Trim() == "") txtcuscode.Text = "0";

                //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());


               Int64 acno=0;
               
                try
                {

                    sql = "SELECT * FROM Veh_Customer where Cus_code =" + Convert.ToDouble(txtcuscode.Text.Trim());
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (rec.RecordCount == 0)
                    {
                       


                        sql = "SELECT MAX(cus_code)+1 FROM Veh_Customer";
                        ADODB.Recordset tmp = new ADODB.Recordset();
                        tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (tmp.Fields[0].Value!= DBNull.Value)
                        {
                            txtcuscode.Text = tmp.Fields[0].Value.ToString();
                            
                        }
                        else
                        {
                            txtcuscode.Text = "1";
                        }

                       
                       



                         tmp = new ADODB.Recordset();

                            sql = "update ACC_TYPE SET CUR_NO = " + acno + " WHERE  ACC_TYPE_CODE=1";
                            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                       
                        rec.AddNew();
                    }


                    acno = Convert.ToInt64(txtcuscode.Text.Trim());

                    rec.Fields["Cus_code"].Value = Convert.ToDouble(txtcuscode.Text.Trim());
                    //rec.Fields["Cus_code"].Value = txtcuscode.Text.Trim();
                    rec.Fields["Cus_cat"].Value = cmbcat.SelectedValue;
                    rec.Fields["Cus_Name"].Value = txtcusname.Text.Trim();
                    rec.Fields["Sponsor_code"].Value = cmbsponsor.SelectedValue;
                    rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    rec.Fields["Mobile"].Value = txtmobileno.Text.Trim();
                    rec.Fields["Address"].Value = txtcusadd.Text.Trim();

                    rec.Fields["office_no"].Value = txtofficeno.Text.Trim();

                    rec.Fields["careof"].Value = txtcareof.Text.Trim();
                    rec.Fields["contact_no"].Value = txtcontactno.Text.Trim();


                    rec.Fields["ID_issued_at"].Value = txtissueplace.Text.Trim();
                    rec.Fields["License_No"].Value = txtlicno.Text.Trim();
                    rec.Fields["ID_Expiry_Date"].Value = txtidexiry.Text.Trim();
                    rec.Fields["Licence_Issued_at"].Value = txtlicissueplace.Text.Trim();

                    rec.Fields["Licence_exp_date"].Value = txtlicexpiry.Text.Trim();
                    rec.Fields["Nationality"].Value = txtnationality.Text.Trim();
                    rec.Fields["EmailId"].Value = txtemailid.Text.Trim();

                    rec.Update();


                   


                    //if (txtvehno.Text.Trim() == "") txtvehno.Text = "0";
                    //if (txtvalue.Text.Trim() == "") txtvalue.Text = "0";

                    //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());
                    sql = "SELECT * FROM Accounts where Acc_no ='" + acno + "'";
                    rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();



                    }


                    //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());
                    rec.Fields["ACC_NO"].Value = txtcuscode.Text.Trim();
                    rec.Fields["ACC_NAME"].Value = txtcusname.Text.Trim();
                    rec.Fields["ACC_CATEGORY"].Value = 1;
                    rec.Fields["ACC_TYPE_CODE"].Value = 1;
                    rec.Fields["ACC_TELE_NO"].Value = txtofficeno.Text.Trim();
                    rec.Fields["ACC_FAX_NO"].Value = txtofficeno.Text.Trim();

                    rec.Fields["ACC_ADDRESS"].Value = txtcusadd.Text.Trim();
                    rec.Fields["CONTACT_PERSON"].Value = txtcusname.Text.Trim(); 


                    rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    rec.Fields["ACC_Mobile_NO"].Value = txtmobileno.Text.Trim();
                    rec.Fields["flag"].Value = "A";

                   

                    rec.Update();


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

        private void load_leaders()
        {
            Conn.Close();
            Conn.Open();

            try
            {


                string sql = "select Sponsor_code,Sponsor_name from veh_Sponsor  order by Sponsor_name";

                SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                DataTable dt2 = new DataTable("Veh_Sponsor");
                ada2.Fill(dt2);

                cmbsponsor.DisplayMember = "Sponsor_name";
                cmbsponsor.ValueMember = "Sponsor_code";
                cmbsponsor.DataSource = dt2;

                sql = "select Cus_cat_Code,Cus_Cat_Name from Veh_Cus_Category order by Cus_Cat_Name ";
                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("Veh_Cus_Category");
                ada3.Fill(dt3);

                cmbcat.DisplayMember = "Cus_Cat_Name";
                cmbcat.ValueMember = "Cus_cat_Code";
                cmbcat.DataSource = dt3;

                if (chksort.Checked)
                    sql = "SELECT cast(Cus_code as Varchar) as Cus_code,[Cus_Name], Sponsor_Name  ,v.[Id_Number] ,v.Mobile,V.Address,C.cus_cat_name,v.sponsor_code FROM [veh_Customer] v inner join veh_sponsor s on v.sponsor_code=s.sponsor_code left join Veh_cus_category as c on v.cus_cat=c.cus_cat_code order by v.cus_name";

                else
                    sql = "SELECT cast(Cus_code as Varchar) as Cus_code,[Cus_Name], Sponsor_Name  ,v.[Id_Number] ,v.Mobile,V.Address,C.cus_cat_name,v.sponsor_code FROM [veh_Customer] v inner join veh_sponsor s on v.sponsor_code=s.sponsor_code left join Veh_cus_category as c on v.cus_cat=c.cus_cat_code order by cast(v.cus_code as numeric)";
              
             
                grid.Visible = true;

               


                cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

                ada = new SqlDataAdapter(cmd);

                dt = new DataTable("Veh_Master");
                ada.Fill(dt);
                
                dv.Table = dt;



                grid.DataSource = dv;
                grid.Visible = true;
                //dgv1.DataMember = "Veh_Customer";
                grid.Columns[0].HeaderText = "Cus.Code";
                grid.Columns[1].HeaderText = "Cus. Name";
                grid.Columns[2].HeaderText = "Sponsor";
                grid.Columns[3].HeaderText = "Cus. ID ";
                grid.Columns[4].HeaderText = "Mobile";

                grid.Columns[1].Width = 200;
                grid.Columns[2].Width = 200;

                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void delete_leaders()
        {
            grid.Select();
        }

  
        public frmcustomer()
        {
            InitializeComponent(); 
            txtpriv.Text = Gvar.frm_priv.ToString();
            load_leaders();
        }

        private void FrmLeader_Load(object sender, EventArgs e)
        {

            //dt = dataGrid1.DataContext;
            //dt.BeginInit();
            





        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            //load_leaders();

           // grid.CurrentCell = grid[grid.CurrentCell.ColumnIndex+1, grid.CurrentCell.ColumnIndex];
            try
            {
                EventArgs v = new DataGridViewCellEventArgs(grid.CurrentCell.ColumnIndex, grid.CurrentCell.ColumnIndex);
                dgv1_CellContentClick(null, (DataGridViewCellEventArgs)v);
            }
            catch
            {
            }
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
            
           
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

            txtcuscode.Text = "";
            txtcusname.Text = "";
            cmbsponsor.SelectedIndex = -1;

            txtidnumber.Text = "";
            txtmobileno.Text = "";
            txtcusadd.Text = "";

            txtofficeno.Text = "";
            txtcareof.Text = "";
            txtcontactno.Text = "";

            txtissueplace.Text = "";
            txtlicno.Text = "";
            txtidexiry.Text = "";

            txtlicexpiry.Text = "";
            txtnationality.Text = "";

            txtlicissueplace.Text = "";

            txtemailid.Text = "";
        }

        private void cmdbrand_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = "100";// MdiParent.mnubrand.Tag.ToString();
            Gvar.Gind = 8;
            Form childForm = new FrmBrand();
            // childForm.MdiParent = MDIParent1.ActiveForm;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Category Entry Screen";
            childForm.ShowDialog();
            sql = "select Cus_cat_Code,Cus_Cat_Name from Veh_Cus_Category ";

            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
            DataTable dt2 = new DataTable("Veh_Cus_Category");
            ada2.Fill(dt2);

            cmbcat.DisplayMember = "Cus_Cat_Name";
            cmbcat.ValueMember = "Cus_cat_Code";
            cmbcat.DataSource = dt2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string txt = textBox1.Text.Trim();
                
                
                
                if (txt != "")
                {
                    //issearch = false;
                
                    if (chkcmp.Checked)
                        
                    dv.RowFilter = "sponsor_code = " + cmbsponsor.SelectedValue + " and ( Cus_Code = '" + txt + "' OR Cus_name LIKE '%" + txt + "%')";
                    else
                        dv.RowFilter = "Cus_Code = '" + txt + "' OR Cus_name LIKE '%" + txt + "%'";


                }

                else
                    if (chkcmp.Checked)

                        dv.RowFilter = "sponsor_code = " + cmbsponsor.SelectedValue + " and  Cus_Code <> '0'";
                    else
                    dv.RowFilter = "Cus_Code <> '0'";
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void chkcmp_CheckedChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(null, null);
        }

        private void chksort_CheckedChanged(object sender, EventArgs e)
        {
            load_leaders();
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grid.EndEdit();
                //DataGridViewCell ccell = dgv1.CurrentCell;
                //dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                //dgv1.CurrentCell = ccell;
                if (e.RowIndex < 0) return;
                if (string.IsNullOrEmpty(grid[e.ColumnIndex, e.RowIndex].Value.ToString())) return;

                ac_code = grid[0, e.RowIndex].Value.ToString();



                string sql = "SELECT [Cus_code],[Cus_Name], Sponsor_code  ,[Id_Number] ,Mobile,address,cus_cat,office_no,careof,contact_no, ID_issued_at,License_No,ID_Expiry_Date,Licence_exp_date,Nationality,Licence_Issued_at,EmailId  FROM [Veh_Customer] where cus_code='" + ac_code + "'";

                //rd.Close();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                bool find = false;
                while (rd.Read())
                {

                    txtcuscode.Text = rd["Cus_code"].ToString();
                    txtcusname.Text = rd["Cus_Name"].ToString();
                    cmbsponsor.SelectedValue = rd["Sponsor_code"].ToString();


                    if (!rd["cus_cat"].Equals(null))
                    {
                        cmbcat.SelectedValue = rd["cus_cat"].ToString();
                    }
                    txtidnumber.Text = rd["Id_Number"].ToString();
                    txtmobileno.Text = rd["Mobile"].ToString();
                    txtcusadd.Text = rd["Address"].ToString();

                    txtofficeno.Text = rd["office_no"].ToString(); ;
                    txtcareof.Text = rd["careof"].ToString();
                    txtcontactno.Text = rd["contact_no"].ToString();



                    txtissueplace.Text = rd["ID_issued_at"].ToString();
                    txtlicno.Text = rd["License_No"].ToString();
                    txtidexiry.Text = rd["ID_Expiry_Date"].ToString();

                    txtlicexpiry.Text = rd["Licence_exp_date"].ToString(); ;
                    txtnationality.Text = rd["Nationality"].ToString();

                    txtlicissueplace.Text = rd["Licence_Issued_at"].ToString();

                    txtemailid.Text = rd["EmailId"].ToString();

                    find = true;

                }
                rd.Close();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
