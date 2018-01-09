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

    
    public partial class FrmBrand : FinOrgForm
  {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        //string sql;
        public int ind;
        private void save_form()
        {
            try
            {
                
                switch (Convert.ToInt16(trn_type.Text))
                {

                    case 1:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Cost_Master");
                        }
                        break;

                    case 2:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Dept_Master");
                        }
                        break;

                    case 3:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Area_Master");
                        }
                        break;


                    case 4:
                       
                    case 5:
                       

                    case 6:
                       

                    case 7:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "PurOrder_Option");
                        }
                        break;
                    case 8:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Signatory");
                        }
                        break;
                    case 9:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Department");
                        }
                        break;
                    case 10:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Position");
                        }
                        break;
                    case 11:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "Nationality");
                        }
                        break;
                    case 12:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "entrysetting");
                        }
                        break;
                    case 13:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "glentrysetting");
                        }
                        break;
                 case 14:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "options");
                        }
                        break;
                 case 15:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "finance");
                        }
                        break;
                 case 16:
                        {

                            dgv1.EndEdit();
                            DataGridViewCell ccell = dgv1.CurrentCell;
                            dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                            dgv1.CurrentCell = ccell;

                            ada.Update(ds, "company");
                        }
                        break;
                        
                }


               
                
               // ada.Update(dt);
               
                //dt.AcceptChanges();
            }
            
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }
        }






        private void load_form()
        {
            Conn.Close();
            Conn.Open();


          dt = new DataTable();

            trn_type.Text = Gvar.Gind.ToString();

            switch (Convert.ToInt16(trn_type.Text))
            {

                case 1:
                    {

                        ada = new SqlDataAdapter("select Cost_Code,Cost_name from Cost_Master ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("CostMaster");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);

                        this.ada.Fill(this.ds, "Cost_Master");
                        dv.Table = dt;
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid
                        dgv1.DataSource = ds;
                        dgv1.Visible = true;
                        dgv1.DataMember = "Cost_Master";
                        dgv1.Columns[0].HeaderText = "Cost Code";
                        dgv1.Columns[1].HeaderText = "Cost Name";
                    }
                    break;
                case 2:
                    {

                        ada = new SqlDataAdapter("select Dept_Code,Dept_name from Dept_master ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("Dept_Master");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "Dept_Master");
                        dv.Table = dt;
                        dgv1.DataMember = "Dept_Master";
                        dgv1.Columns[0].HeaderText = "Dept Code";
                        dgv1.Columns[1].HeaderText = "Dept Name";
                    }
                    break;


                case 3:
                    {

                        ada = new SqlDataAdapter("select Area_Code,Area_name from Area_master ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("Area_Master");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "Area_Master");
                        dv.Table = dt;
                        dgv1.DataMember = "Area_Master";
                        dgv1.Columns[0].HeaderText = "Area Code";
                        dgv1.Columns[1].HeaderText = "Area Name";
                    }
                    break;


                case 4:
                    {

                        ada = new SqlDataAdapter("select Type,Description,Code from PurOrder_Options where [Type]='ShipTerm' ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("PurOrder_Option");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "PurOrder_Option");
                        dv.Table = dt;
                        dgv1.DataMember = "PurOrder_Option";
                        dgv1.Columns[1].HeaderText = "Description";
                        dgv1.Columns[0].HeaderText = "Type";
                        dgv1.Columns[2].Visible = false;
                        //dgv1.Columns[0].Width = 300;
                        dgv1.Columns[0].Visible = false;
                    }
                    break;
                case 5:
                    {

                        ada = new SqlDataAdapter("select Type,Description,Code from PurOrder_Options  where [Type]='PayTerm' ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("PurOrder_Option");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "PurOrder_Option");
                        dv.Table = dt;
                        dgv1.DataMember = "PurOrder_Option";
                        dgv1.Columns[1].HeaderText = "Description";
                        dgv1.Columns[0].HeaderText = "Type";
                        dgv1.Columns[2].Visible = false;
                        //dgv1.Columns[0].Width = 300;
                        dgv1.Columns[0].Visible = false;
                    }
                    break;

                case 6:
                    {

                        ada = new SqlDataAdapter("select Type,Description,Code from PurOrder_Options  where [Type]='Mode' ", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("PurOrder_Option");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "PurOrder_Option");
                        dv.Table = dt;
                        dgv1.DataMember = "PurOrder_Option";
                        dgv1.Columns[0].HeaderText = "Description";
                        dgv1.Columns[1].HeaderText = "Description";
                        dgv1.Columns[0].HeaderText = "Type";
                        dgv1.Columns[2].Visible = false;
                        //dgv1.Columns[0].Width = 300;
                        dgv1.Columns[0].Visible = false;
                    }
                    break;

                case 7:
                    {

                        ada = new SqlDataAdapter("select Type,Description,Code from PurOrder_Options  where [Type]='Port'", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("PurOrder_Option");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "PurOrder_Option");
                        dv.Table = dt;
                        dgv1.DataMember = "PurOrder_Option";
                        dgv1.Columns[1].HeaderText = "Description";
                        dgv1.Columns[0].HeaderText = "Type";
                        dgv1.Columns[2].Visible = false;
                        //dgv1.Columns[0].Width = 300;
                        dgv1.Columns[0].Visible = false;
                    }
                    break;

                case 8:
                    {

                        ada = new SqlDataAdapter("select SNO,signatory as Position,Sign_Name as Name from signatory", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("signatory");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "signatory");
                        dv.Table = dt;
                        dgv1.DataMember = "signatory";
                        dgv1.Columns[1].HeaderText = "Position";
                        dgv1.Columns[0].HeaderText = "SNO";
                        dgv1.Columns[2].HeaderText = "Name";
                        dgv1.Columns[1].Width = 200;
                        dgv1.Columns[2].Width = 200;
                        //dgv1.Columns[0].Visible = false;
                    }
                    break;
                case 9:
                    {

                        ada = new SqlDataAdapter("select SectionId as DepartmentID,Name as Department_Name from HV_Section", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("Department");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "Department");
                        dv.Table = dt;
                        dgv1.DataMember = "Department";
                        dgv1.Columns[1].HeaderText = "Depart. Name";
                        dgv1.Columns[0].HeaderText = "Code";
                        
                        dgv1.Columns[1].Width = 200;
                       // dgv1.Columns[2].Width = 200;
                        //dgv1.Columns[0].Visible = false;
                    }
                    break;
                case 10:
                    {

                        ada = new SqlDataAdapter("select Position_ID,Position_eName from HV_POSITION", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("Position");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "Position");
                        dv.Table = dt;
                        dgv1.DataMember = "Position";
                        dgv1.Columns[1].HeaderText = "Position";
                        dgv1.Columns[0].HeaderText = "Code";
                        //dgv1.Columns[2].HeaderText = "Name";
                        dgv1.Columns[1].Width = 200;
                        //dgv1.Columns[2].Width = 200;
                        //dgv1.Columns[0].Visible = false;
                    }
                    break;
                case 11:
                    {

                        ada = new SqlDataAdapter("select Nat_id,Nat_Ename from HV_Nationality", Conn);


                        ///ada.TableMappings.Add("Table", "Leaders");
                        ds = new DataSet();
                        dt = new DataTable("Nationality");
                        dt.AcceptChanges();
                        ///ada.Fill(dt);
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                        //set the table as the datasource for the grid in order to show that data in the grid

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "nationality");
                        dv.Table = dt;
                        dgv1.DataMember = "nationality";
                        dgv1.Columns[1].HeaderText = "Nationality";
                        dgv1.Columns[0].HeaderText = "Code";
                        //dgv1.Columns[2].HeaderText = "Name";
                        dgv1.Columns[1].Width = 200;
                        //dgv1.Columns[2].Width = 200;
                        //dgv1.Columns[0].Visible = false;
                    }
                    break;
                case 12:
                    {

                        ada = new SqlDataAdapter("select * from Ac_Options", Conn);

                        ds = new DataSet();
                        dt = new DataTable("entrysetting");
                        dt.AcceptChanges();
                       
                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);
                         dgv1.DataSource = ds;
                        this.ds.AcceptChanges();
                      
                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "entrysetting");
                        dv.Table = dt;
                        dgv1.DataMember = "entrysetting";
                      
                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                        dgv1.AllowUserToAddRows = false;
                        dgv1.AllowUserToDeleteRows = false;
                    }
                    break;
                case 13:
                    {

                        ada = new SqlDataAdapter("select * from Acc_TRN_Option", Conn);

                        ds = new DataSet();
                        dt = new DataTable("glentrysetting");
                        dt.AcceptChanges();

                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);
                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "glentrysetting");
                        dv.Table = dt;
                        dgv1.DataMember = "glentrysetting";

                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                        dgv1.AllowUserToAddRows = true;
                       // dgv1.al
                        dgv1.AllowUserToDeleteRows = false;
                    }
                    break;
                case 14:
                    {

                        ada = new SqlDataAdapter("select * from Options", Conn);

                        ds = new DataSet();
                        dt = new DataTable("options");
                        dt.AcceptChanges();

                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);
                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "options");
                        dv.Table = dt;
                        dgv1.DataMember = "options";

                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                        dgv1.AllowUserToAddRows = true;
                        // dgv1.al
                        dgv1.AllowUserToDeleteRows = false;
                    }
                    break;
                case 15:
                    {

                        ada = new SqlDataAdapter("SELECT  FINANCE_NAME , FINANCE_START_DATE , FINANCE_END_DATE , FINANCE_STATUS AS Status FROM            FINANCE_SESSION", Conn);

                        ds = new DataSet();
                        dt = new DataTable("finance");
                        dt.AcceptChanges();

                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);
                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "finance");
                        dv.Table = dt;
                        dgv1.DataMember = "finance";

                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                        dgv1.AllowUserToAddRows = true;
                        // dgv1.al
                        dgv1.AllowUserToDeleteRows = false;

                       // DataGridViewComboBoxColumn FINANCE_STATUS = new DataGridViewComboBoxColumn();
                        //dgv1.Columns.Add(FINANCE_STATUS);
                    }
                    break;
                case 16:
                    {

                        ada = new SqlDataAdapter("select * from company", Conn);

                        ds = new DataSet();
                        dt = new DataTable("company");
                        dt.AcceptChanges();

                        SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);
                        dgv1.DataSource = ds;
                        this.ds.AcceptChanges();

                        dgv1.Visible = true;

                        this.ada.Fill(this.ds, "company");
                        dv.Table = dt;
                        dgv1.DataMember = "company";

                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                        dgv1.AllowUserToAddRows = true;
                        // dgv1.al
                        dgv1.AllowUserToDeleteRows = false;
                    }
                    break;
            }

            

            //sql = "select Empno,Employee_name,Tele_Home from Leaders";

            //SqlCommand cmd = new SqlCommand(sql, Conn);

            
            //SqlDataAdapter ada = new SqlDataAdapter(cmd);




            
            
            dv.AllowEdit = true;
            dv.AllowNew = true;
            dv.AllowDelete = true;
            
           /// dv.Table = dt;
            
           
            
            
           dgv1.Columns[1].Width = 300;
            dgv1.Visible = true;

            //dgv1.Columns[2].ReadOnly=true;

            



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

  
        public FrmBrand()
        {
            InitializeComponent();
            txtpriv.Text = Gvar.frm_priv.ToString();
        }

        private void FrmLeader_Load(object sender, EventArgs e)
        {

            //dt = dataGrid1.DataContext;
            //dt.BeginInit();


            load_form();



        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            load_form ();

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

     

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            load_form();
        }

        

       

        private void dgv1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (Convert.ToInt16(trn_type.Text) == 7 && dgv1.Focused && e.RowIndex > 0)
            //{
            //    dgv1[2, e.RowIndex].Value = Gvar.Glb_strval;
            //}


            switch (Convert.ToInt16(trn_type.Text))
            {

                case 4:
                    {
                        dgv1[0, e.RowIndex].Value = "ShipTerm";
                    }
                    break;
                case 5:
                    {
                        dgv1[0, e.RowIndex].Value = "PayTerm";
                    }
                    break;

                case 6:
                    {
                        dgv1[0, e.RowIndex].Value = "Mode";
                    }
                    break;

                case 7:
                    {
                        dgv1[0, e.RowIndex].Value = "Port";
                    }
                    break;


            }

        }
    }
}
