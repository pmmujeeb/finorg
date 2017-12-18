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

    
    public partial class FrmItemCat : Form
  {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        //string sql;
        
        private void save_form()
        {
            try
            {



                dgv1.EndEdit();
                DataGridViewCell ccell = dgv1.CurrentCell;
                dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                dgv1.CurrentCell = ccell;

                ada.Update(ds,"Item_Cat");
                string sql = "Update ITEM_CAT  set VAT_PERCENT =0 WHERE VAT_PERCENT IS NULL";
                cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();
                
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







            //sql = "select Empno,Employee_name,Tele_Home from Leaders";

            //SqlCommand cmd = new SqlCommand(sql, Conn);

            
            //SqlDataAdapter ada = new SqlDataAdapter(cmd);
            if(txtindex.Text=="3")

                ada = new SqlDataAdapter("select * from asset_Cat", Conn);
            else
                ada = new SqlDataAdapter("select * from Item_Cat", Conn);
            ///ada.TableMappings.Add("Table", "Leaders");
            ds = new DataSet();
            DataTable dt = new DataTable("Item_Cat");
            dt.AcceptChanges();
            ///ada.Fill(dt);

            this.ada.Fill(this.ds, "Item_Cat");
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
            dgv1.DataMember = "Item_Cat";

            dgv1.Columns[0].Name = "item_code";
            dgv1.Columns[0].HeaderText = "Cat Code";
            dgv1.Columns[1].HeaderText = "English Name";
            dgv1.Columns[2].HeaderText = "Arabic Name";


           dgv1.Columns[1].Width = 300;
           dgv1.Columns[2].Width = 300;
            dgv1.Visible = true;


           




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

  
        public FrmItemCat()
        {
            InitializeComponent();
            txtindex.Text = Gvar._Gind.ToString();
              
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

        private void dgv1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgv1.CurrentCell==dgv1["item_code",dgv1.CurrentCell.RowIndex] && !Convert.IsDBNull( dgv1["item_code",dgv1.CurrentCell.RowIndex].Value ))
            //if (Convert.ToInt32( dgv1["item_code",dgv1.CurrentCell.RowIndex].Value.ToString()) < 5 )
            //{
            //    MessageBox.Show("Invalid Code. Must be Greater than 4");
            //dgv1["item_code",dgv1.CurrentCell.RowIndex].Value=DBNull.Value;
            //}


        }
    }
}
