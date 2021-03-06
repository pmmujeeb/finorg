﻿using System;
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

    
    public partial class FrmSiteMaster : Form
  {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        string sql;
        bool isedit = false;
        private void save_form()
        {
            try
            {


                dgv1.EndEdit();
                DataGridViewCell ccell = dgv1.CurrentCell;
                dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                dgv1.CurrentCell = ccell;
                ada.Update(ds1,"Site_Master");
                isedit = false;
                
               // ada.Update(dt);
               
                //dt.AcceptChanges();
            }
            
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }
        }

        private void load_leaders()
        {
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



            //sql = "select Empno,Employee_name,Tele_Home from Leaders";

            //SqlCommand cmd = new SqlCommand(sql, Conn);

            
            //SqlDataAdapter ada = new SqlDataAdapter(cmd);


            site_master();
           




            //OdbcDataAdapter ada = new OdbcDataAdapter(cmd);

            
            //dt = new DataTable("Leaders");
            //ada.Fill(dt);


            //dataGrid1.DataSource=dt.DefaultView();
            //dataGrid1.DataSource=
            //    .DataContext = dt.DefaultView;
        }


        private void site_master()
        {
            try
            {
                ada = new SqlDataAdapter("select * from Site_Master where proj_code=" + cmbproject.SelectedValue, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                ds1 = new DataSet();
                DataTable dt = new DataTable("Site_Master");
                dt.AcceptChanges();
                ///ada.Fill(dt);

                this.ada.Fill(this.ds1, "Site_Master");
                dv.Table = dt;
                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                this.ds1.AcceptChanges();
                //set the table as the datasource for the grid in order to show that data in the grid

                dgv1.Visible = true;




                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;

                /// dv.Table = dt;

                dgv1.DataSource = ds1;
                dgv1.DataMember = "Site_Master";


                dgv1.Columns[2].Width = 300;
                dgv1.Columns[0].Name = "proj_code";

                dgv1.Columns[0].Visible = false;
                dgv1.Visible = true;


            }
            catch
            {
            }
        }
        private void delete_leaders()
        {
            dgv1.Select();
        }

  
        public FrmSiteMaster()
        {
            InitializeComponent(); txtpriv.Text = Gvar.frm_priv.ToString();
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

        private void cmbproject_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isedit)
            {
               
                DialogResult result = MessageBox.Show("The Current Records are not Saved!!,Do you want to Save it now??", "Save Records", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    save_form();
                }
            }

            isedit = false;
            site_master();
        }

        

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isedit = true;
            dgv1["Proj_code", dgv1.CurrentCell.RowIndex].Value = cmbproject.SelectedValue;


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

                    if (Convert.IsDBNull(dgv1[3, i].Value)) dgv1[3, i].Value = 0;
                    //if (Convert.IsDBNull(dgv1["qty", i].Value)) dgv1["qty", i].Value = 0;
                    price = Convert.ToDouble(dgv1[3, i].Value); //* Convert.ToDouble(dgv1["qty", i].Value);
                   // dgv1["total", i].Value = price;
                    tot = tot + price;
                }

                txttotal.Text = tot.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) find_total();
        }
    }
}
