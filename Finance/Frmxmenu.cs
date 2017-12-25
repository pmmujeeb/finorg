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
    public partial class Frmxmenu : Form
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


        public Frmxmenu()
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




//                sql = "SELECT ID, Menu_Code,  REPLACE(Menu_Name, '&', '') as MenuName,  cast( 0 as bit) as 'Select' FROM   Menu_master where head_det=1 and Flag='A' " ; // Union SELECT top 1   'All', 'False', 'False', 'False', 'False', 0,'0' FROM         dbo.V_UserPriv where Menu_name <> Head order by id ";

               

                

                
                sql = "SELECT   distinct group_name  FROM UserPriv ";
                SqlDataAdapter adagrp = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                DataSet grpds = new DataSet();

                adagrp.Fill(grpds, "UserPriv");
                cmbgrp.DisplayMember = "group_name";
                cmbgrp.DataSource = grpds.Tables[0];



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
            dgv1[e.ColumnIndex, 0].Value = dgv1.CurrentCell.EditedFormattedValue;
           // dgv1.EndEdit();
            if (dgv1.CurrentCell.RowIndex == 0 && e.ColumnIndex == 3)
            {
                for (int i = 1; i < dgv1.RowCount; i++)
                {
                    dgv1[e.ColumnIndex, i].Value = dgv1[e.ColumnIndex, 0].Value;
                }

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
                string crt1 = "1=1";
                string crt2 = "1=1";


                if (cmbmenu.SelectedIndex > 0) crt1 = "REPLACE(Head, '&', '') ='" + cmbmenu.Text + "'" ;
                if (cmbgrp.SelectedIndex >= 0) crt2 = " group_name ='" + cmbgrp.Text + "'";
                sql = "SELECT Menu_master.ID, Menu_master.Menu_Code,  REPLACE(Menu_master.Menu_Name, '&', '') as MenuName,  cast(case when Menu_Xpress.id is not null then 1 else 0 end as bit) as 'Select' FROM   Menu_master left join Menu_Xpress on (Menu_master.Id=Menu_Xpress.id and " + crt2 + ")  where head_det=1 and Flag='A' and " + crt1 + " Union SELECT    0,'','All',cast(0 as bit) Order By ID ";







                ////if (cmbgrp.SelectedIndex > 0) txt2 = "(Group_Name = '" + cmbgrp.Text + "' or group_name='All')";
                //sql = "SELECT   REPLACE(M.Menu_Name, '&', '') as MenuName, P.Ins as Addition, P.upd as Changes, P.del as Delete_, P.dsp as View_, M.ID,M.Menu_Name,REPLACE(M.Head, '&', '') as Head,P.Group_name FROM  Menu_Master as M left join Userpriv P  ON m.ID = p.form_id  where M.Menu_name <> M.Head and head_det =1  Union SELECT top 1   '                        All', 'False', 'False', 'False', 'False', 0,'All','All','All' Head FROM         Menu_Master where Menu_name <> Head and head_det =1 order by id ";

                //if (cmbgrp.SelectedIndex >= 0) sql = "SELECT   REPLACE(M.Menu_Name, '&', '') as MenuName, P.Ins as Addition, P.upd as Changes, P.del as Delete_, P.dsp as View_, M.ID,M.Menu_Name,REPLACE(M.Head, '&', '') as Head,P.Group_name FROM  Menu_Master as M left join Userpriv P  ON (m.ID = p.form_id and P.Group_Name='" + cmbgrp.Text + "')  where M.Menu_name <> M.Head and head_det =1  Union SELECT top 1   '                        All', 'False', 'False', 'False', 'False', 0,'All','All','All' Head FROM   Menu_Master where Menu_name <> Head and head_det =1 order by id ";

                ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
                dt = new DataTable("Menu_Master");

                this.ada.Fill(this.dt);

                dv.Table = dt;
                dgv1.DataSource = dv;
                txt = txt1;



                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                dgv1.Visible = true;
                dv.AllowEdit = true;
                dv.AllowNew = false;
                dv.AllowDelete = false;


                dgv1.Refresh();

                dgv1.Visible = true;

                dgv1.Columns[0].Name = "id";
                dgv1.Columns[1].Name = "menucode";
                dgv1.Columns[2].Name = "menuname";

                dgv1.Columns[3].Name = "select";
                dgv1.Columns[0].Visible = false;
                dgv1.Columns[1].Visible = false;

                //dgv1.Columns[10].Visible = false;
                if (dgv1.Rows.Count > 1)
                dgv1.Rows[0].Height = 50;

                //dgv1.Rows[0].DefaultCellStyle.BackColor = Color.Blue;
                //dgv1.Columns[0].ReadOnly = true;
                dgv1["MenuName", 0].Style.ForeColor = Color.Blue;
                //dgv1.Columns[0].Width = 100;
                dgv1.Columns[2].Width = 380;
                dgv1.Columns[2].HeaderCell.Style.Alignment = 0;
                //dgv1.Columns[1].HeaderCell.Style.Alignment = 1;
                //dgv1.Columns[2].HeaderCell.Style.Alignment = 2;
                //dgv1.Columns[3].HeaderCell.Style.Alignment = 3;


                // dgv1_password();



                if (txt != "")
                {
                    dv.RowFilter = txt;
                }
                else
                    dv.RowFilter = "id <> -1";
            }
            catch(Exception ex)
            {

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
                    if (!Convert.ToBoolean( dgv1["select", i].Value))
                    {
                        sql = "delete FROM Menu_Xpress where id=" + dgv1["id", i].Value;
                        object a;
                        ADOconn.Execute(sql, out a);
                        continue;

                    }
                    else
                    {


                        sql = "SELECT * FROM Menu_Xpress where id=" + dgv1["id", i].Value;
                        cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (cus.RecordCount == 0) cus.AddNew();
                        cus.Fields["GROUP_NAME"].Value = cmbgrp.Text;
                        cus.Fields["id"].Value = Convert.ToInt16( dgv1["id", i].Value);
                        cus.Fields["Menu_code"].Value = dgv1["menucode", i].Value;
                        cus.Fields["Menu_Name"].Value = dgv1["menuname", i].Value;
                        cus.Update();
                    }

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
           
                //for (int i = 1; i < dgv1.RowCount; i++)
                //{
                //    dgv1["select", i].Value = false;
                //}

            
        }

        private void cmbgrp_TextUpdate(object sender, EventArgs e)
        {

        }

    }
}
