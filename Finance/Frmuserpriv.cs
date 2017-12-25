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
    public partial class Frmuserpriv : Form
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string sql;


        public Frmuserpriv()
        {
            InitializeComponent();
            load_form();
        }

        private void Frmuserpriv_Load(object sender, EventArgs e)
        {

        }
        private void load_form()
        {

            try
            {
                //dgv1.Rows.Clear();
                dgv1.Columns.Clear();

                Conn.Close();
                Conn.Open();





                //dgv1.AutoGenerateColumns = false;

                sql = "select '_All' as Head from Menu_master Union select Distinct replace(Head,'&','') from Menu_master";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet dswh = new DataSet();
                this.ada.Fill(dswh, "Menu_master");
                cmbmenu.DisplayMember = "Head";

                cmbmenu.DataSource = dswh.Tables[0];




                //sql = "SELECT   REPLACE(Menu_Name, '&', '') as MenuName, Ins as Addition, upd as Changes, del as Delete_, dsp as View_, ID,Menu_Name,REPLACE(Head, '&', '') as Head,Group_name FROM         dbo.V_UserPriv where Menu_name <> Head Union SELECT top 1   '                        All', 'False', 'False', 'False', 'False', 0,'All','All','All' Head FROM         dbo.V_UserPriv where Menu_name <> Head order by id ";
                //ada = new SqlDataAdapter(sql, Conn);
                /////ada.TableMappings.Add("Table", "Leaders");
                //dt = new DataTable("V_UserPriv");

                //this.ada.Fill(this.dt);

                //dv.Table = dt;
                //dgv1.DataSource = dv;


                sql = "SELECT   distinct group_name  FROM UserPriv ";
                SqlDataAdapter adagrp = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet grpds = new DataSet();

                adagrp.Fill(grpds, "UserPriv");
                cmbgrp.DisplayMember = "group_name";
                cmbgrp.DataSource = grpds.Tables[0];

                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                dgv1.Visible = true;
                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;

                /// dv.Table = dt;

                //dgv1.DataSource = ds.Tables[0];

                dgv1.Refresh();
                dgv1.Refresh();
                //dgv1.Columns[0].Width = 150;
                //dgv1.Columns[2].Width = 300;
                dgv1.Visible = true;

                dgv1.Columns[0].Name = "HeadName";
                dgv1.Columns[1].Name = "MenuName";
                dgv1.Columns[2].Name = "ins";

                dgv1.Columns[3].Name = "upd";
                dgv1.Columns[4].Name = "del";

                dgv1.Columns[5].Name = "dsp";
                dgv1.Columns[6].Name = "id";
                dgv1.Columns[7].Name = "Menu_Name";
                dgv1.Columns[8].Name = "Head";
                dgv1.Columns[9].Name = "group_name";
                dgv1.Columns[9].Visible = false;
                dgv1.Columns[6].Visible = false;
                dgv1.Columns[7].Visible = false;
                dgv1.Columns[8].Visible = false;
                //dgv1.Columns[10].Visible = false;
                dgv1.Rows[0].Height = 70;
                //dgv1.Rows[0].DataGridView.BackgroundColor = Color.Blue;
                dgv1.Rows[0].DefaultCellStyle.BackColor = Color.Blue;
                dgv1.Columns[0].ReadOnly = true;
                dgv1.Columns[1].ReadOnly = true;
                dgv1["MenuName", 0].Style.ForeColor = Color.Blue;
                //dgv1.Columns[6].ReadOnly = true;
                
                dgv1.Columns[0].Width = 180;
                dgv1.Columns[1].Width = 300;
                dgv1.Columns[1].HeaderCell.Style.Alignment = 0;
                //dgv1.Columns[1].HeaderCell.Style.Alignment = 1;
                //dgv1.Columns[2].HeaderCell.Style.Alignment = 2;
                //dgv1.Columns[3].HeaderCell.Style.Alignment = 3;


                // dgv1_password();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // dgv1.EndEdit();
            try{

            

            if (e.RowIndex > 0) return;
            dgv1[e.ColumnIndex, 0].Value = dgv1.CurrentCell.EditedFormattedValue;
           // dgv1.EndEdit();
            if (dgv1.CurrentCell.RowIndex == 0 && e.ColumnIndex > 1)
            {
                for (int i = 1; i < dgv1.RowCount; i++)
                {
                    dgv1[e.ColumnIndex, i].Value = dgv1[e.ColumnIndex, 0].Value;
                }

            }
             }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbmenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_data();
        }


        private void refresh_data()
        {
            try
            {
                string txt = "";
                string txt1 = "";
                string txt2 = "";



                txt1 = "1=1";
                txt2 = "1=1";

                if (cmbmenu.SelectedIndex > 0)  //txt1 = "(Head ='" +cmbmenu.Text + "' or Head='All')";

                    txt1 = "   replace(Head,'&','') in (select  replace(Menu_name,'&','') from Menu_Master where head_det=0  and replace(Head,'&','') ='" + cmbmenu.Text + "')  or group_name is null";
                if (cmbgrp.SelectedIndex >= 0)
                    txt2 = "(Group_Name = '" + cmbgrp.Text + "' or group_name='All' )";

                if (cmbmenu.SelectedIndex == 0)
                    txt2 = txt2 + " or group_name is null";

                //if (cmbgrp.SelectedIndex > 0) txt2 = "(Group_Name = '" + cmbgrp.Text + "' or group_name='All')";
                sql = "SELECT   REPLACE(M.Menu_Name, '&', '') as MenuName, P.Ins as Addition, P.upd as Changes, P.del as Delete_, P.dsp as View_, M.ID,M.Menu_Name,REPLACE(M.Head, '&', '') as Head,P.Group_name FROM  Menu_Master as M left join Userpriv P  ON m.ID = p.form_id  where M.Menu_name <> M.Head and head_det =1  Union SELECT top 1   '                        All', 'False', 'False', 'False', 'False', 0,'All','All','All' Head FROM         Menu_Master where Menu_name <> Head and head_det =1 order by id ";
                sql = "SELECT  REPLACE(M.Head, '&', '') as MenuHead, REPLACE(M.Menu_Name, '&', '') as MenuName, P.Ins as Addition, P.upd as Changes, P.del as Delete_, P.dsp as View_, M.ID,M.Menu_Name,REPLACE(M.Head, '&', '') as Head,P.Group_name FROM  Menu_Master as M left join Userpriv P  ON (m.ID = p.form_id and P.Group_Name='" + cmbgrp.Text + "')  where Flag='A' and " + txt1 + " and " + txt2 + "  Union SELECT top 1  ' ' , '                        All', 'False', 'False', 'False', 'False', 0,'All','All','All' Head FROM   Menu_Master where Menu_name <> Head and head_det =1 order by id ";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                dt = new DataTable("Menu_Master");

                this.ada.Fill(this.dt);

                dv.Table = dt;
                dgv1.DataSource = dv;
                txt = txt1;
                //if (txt2 != "")
                //{
                //    if (txt != "")
                //    {
                //        txt = txt + " and " + txt2; 
                //    }
                //    else
                //    {
                //        txt = txt2;
                //    }
                //}
                //else
                //{
                //    //txt = txt2;
                //}


                //if (txt != "")
                //{
                //    dv.RowFilter = txt;
                //}
                //else
                    dv.RowFilter = "Head <> '0'";
            }
            catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void cmbgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_data();


            

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save_form();
        }
        private void save_form()
        {
            try
            {
        //ADOconn.BeginTrans();
               ADODB.Recordset cus = new ADODB.Recordset();
                //ADOconn.BeginTrans();

               if (cmbgrp.Text == "")
               {
                   MessageBox.Show("Invalid User Group", "Invalid Entry");
                   return;


               }
               dgv1.EndEdit();
               ADODB.Connection ADOconn = new ADODB.Connection();
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
          
                for(int i=1;i<dgv1.RowCount;i++)
                {

                  if (dgv1["id", i].Value == null) continue;
                    cus = new ADODB.Recordset();
                    sql = "SELECT * FROM userpriv where GROUP_NAME ='" + cmbgrp.Text + "' and form_id="+ dgv1["id",i].Value;
                    cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    if (cus.RecordCount == 0) cus.AddNew();
                    cus.Fields["GROUP_NAME"].Value = cmbgrp.Text;
                     cus.Fields["form_id"].Value = dgv1["id",i].Value;
                    cus.Fields["ins"].Value = dgv1["ins",i].Value;
                    cus.Fields["upd"].Value = dgv1["upd",i].Value;
                    //cus.Fields["qry"].Value = dgv1["qry",i].Value;
                    cus.Fields["del"].Value = dgv1["del",i].Value;
                    cus.Fields["dsp"].Value = dgv1["dsp",i].Value;
                    cus.Fields["Menu_Name"].Value = dgv1["Menu_Name",i].Value;
                   cus.Update();

                }

                string grp = cmbgrp.Text;
                sql = "SELECT   distinct group_name  FROM UserPriv ";
                SqlDataAdapter adagrp = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet grpds = new DataSet();

                adagrp.Fill(grpds, "UserPriv");
                cmbgrp.DisplayMember = "group_name";
                cmbgrp.DataSource = grpds.Tables[0];
                cmbgrp.Text = grp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void cmbgrp_Validated(object sender, EventArgs e)
        {
            try
            {

                for (int i = 1; i < dgv1.RowCount; i++)
                {
                    dgv1[5, i].Value = false;
                    dgv1[2, i].Value = false;
                    dgv1[3, i].Value = false;
                    dgv1[4, i].Value = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void cmbgrp_TextUpdate(object sender, EventArgs e)
        {

        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            if (e.ColumnIndex <2)
            {
                if (dgv1[3, e.RowIndex].Value == DBNull.Value) dgv1[3, e.RowIndex].Value = false;
                bool tick = Convert.ToBoolean( dgv1[3, e.RowIndex].Value);

                for (int i =2;i<6;i++)
                {
                    dgv1[i, e.RowIndex].Value = !tick;
                }

            }
             }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
