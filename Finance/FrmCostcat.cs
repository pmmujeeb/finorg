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

    
    public partial class FrmRentValue : Form
  {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

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
                ADODB.Connection ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                string sql;
                ADODB.Recordset cus = new ADODB.Recordset();
                bool hd = false;
                bool price = false;
                bool error = false;
                cus = new ADODB.Recordset();
              
                dgv1.EndEdit();

                //ada.Update(ds, "ITEM_PRICES");


                //ada.Update(ds);
                for (int i = 0; i < dgv1.RowCount; i++)
                {

                    dgv1.CurrentCell = dgv1[0, i];

                    string celval = Convert.ToString(dgv1["Item_Code", i].Value);
                    hd = false;
                    price = false;
                    if (celval.Trim() != "")
                    {
                       if (Math.Round( Convert.ToDouble( dgv1["sale_price", i].Value),2) < Math.Round( Convert.ToDouble( dgv1["pur_price", i].Value),2))
                        {
                            dgv1["sale_price", i].Style.BackColor = Color.Red;
                            error = true;
                           MessageBox.Show("Invalid Entry on Row " + i);
                            
                            
                        }

                       //if (dgv1.IsCurrentRowDirty)
                       //{

                       //    for (int j = 1; j < dgv1.ColumnCount; j++)
                       //    {

                       //        dgv1.CurrentCell = dgv1[j, i];

                       //        if (dgv1.IsCurrentCellDirty && j < 3)
                       //        {
                       //            hd = true;
                                  
                       //        }


                       //        if (dgv1.IsCurrentCellDirty && j > 2)
                       //        {
                                   
                       //            price = true;
                       //        }


                              
                       //    }

                       //    if (hd)
                       //    {
                               sql = "update hd_itemmaster set description='" + dgv1["description", i].Value + "', flag=UPPER('" + dgv1["flag", i].Value + "') where item_code ='" + dgv1["item_code", i].Value + "'";
                               cus = new ADODB.Recordset();

                               cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          // }

                          // if (price)
                          // {
                               sql = "update stock_master set sale_price='" + dgv1["sale_price", i].Value + "',  avg_pur_price='" + dgv1["pur_price", i].Value + "' where item_code ='" + dgv1["item_code", i].Value + "'";
                               cus = new ADODB.Recordset();

                               cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                          // }



                      // }


                        

                    }

                }

                MessageBox.Show("Update Successfully, OK");

               // dgv1.EndEdit();
               // DataGridViewCell ccell = dgv1.CurrentCell;
               // dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
               // dgv1.CurrentCell = ccell;


               // ada.Update(ds, "ITEM_PRICES");

                
               //// ada.Update(dt);
               
               // //dt.AcceptChanges();
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

            if (cmbcat.SelectedIndex<1)

            ada = new SqlDataAdapter("select *  from ITEM_PRICES where brn_code=" + Gvar.brn_code , Conn);
            else
                ada = new SqlDataAdapter("select *  from ITEM_PRICES where brn_code=" + Gvar.brn_code + " and itm_cat_code="+ cmbcat.SelectedValue, Conn);
            ///ada.TableMappings.Add("Table", "Leaders");
            ds = new DataSet();
            DataTable dt = new DataTable("ITEM_PRICES");
            dt.AcceptChanges();
            ///ada.Fill(dt);

            this.ada.Fill(this.ds, "ITEM_PRICES");
            dv.Table = dt;
            SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

     
            this.ds.AcceptChanges();
            //set the table as the datasource for the grid in order to show that data in the grid
           
            dgv1.Visible = true;
           

            
            
            dv.AllowEdit = true;
            dv.AllowNew = false;
            dv.AllowDelete = false;
            
           /// dv.Table = dt;
            
            dgv1.DataSource = ds;
            dgv1.DataMember = "ITEM_PRICES";
            
            
           dgv1.Columns[1].Width = 300;
            dgv1.Visible = true;

            dgv1.Columns[0].HeaderText = "رمز البند";
            dgv1.Columns[1].HeaderText = " اسم البند";
            dgv1.Columns[2].HeaderText = "الحال";
            dgv1.Columns[3].HeaderText = "سعر اليبع";
            dgv1.Columns[4].HeaderText = "سعر الشراع";
            dgv1.Columns[5].HeaderText = "رصيد";
         


            dgv1.Columns[0].Name = "item_code";
            dgv1.Columns[1].Name = "description";
            dgv1.Columns[3].Name = "sale_price";
            dgv1.Columns[4].Name = "pur_price";
            dgv1.Columns[5].Name = "stock";
            dgv1.Columns[2].Name = "flag";
            dgv1.Columns[0].ReadOnly = true;
            dgv1.Columns[5].ReadOnly = true;
            dgv1.AllowUserToAddRows = false;
            dgv1.Columns[6].Visible = false;
            dgv1.Columns[7].Visible = false;

            //dgv1.Columns[1].HeaderText = "";




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

  
        public FrmRentValue()
        {
            InitializeComponent(); 
            txtpriv.Text = Gvar.frm_priv.ToString();
        }

        private void FrmLeader_Load(object sender, EventArgs e)
        {

            //dt = dataGrid1.DataContext;
            //dt.BeginInit();


            string sql = "sELECT  itm_cat_code,ITM_CAT_name froM ITEM_CAT where itm_cat_code>0 union select 0,'كلى' from ITEM_CAT  order by itm_cat_code";

            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
            DataTable dt2 = new DataTable("ITEM_CAT");
            ada2.Fill(dt2);

            cmbcat.DisplayMember = "ITM_CAT_name";
            cmbcat.ValueMember = "itm_cat_code";
            cmbcat.DataSource = dt2;





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

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgv1.NotifyCurrentCellDirty(true);
        }

        private void toolRefund_Click(object sender, EventArgs e)
        {
           
                  ADODB.Connection ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                string sql;
                ADODB.Recordset cus = new ADODB.Recordset();
               
               
                ADODB.Recordset rec = new ADODB.Recordset();
                ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {
            
                bool error = false;
                bool hd = false;
                bool price = false;
                dgv1.EndEdit();

                //ADOconn.BeginTrans();
                sql = "update wr_stock_master set stock=0";
                rec = new ADODB.Recordset();

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                sql = "update stock_master set stock=0";
                rec = new ADODB.Recordset();

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                sql = "select item_code,stock,wr_code,brn_code from stock_item where stock <>0";
                rec = new ADODB.Recordset();

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                 while (!rec.EOF)
                {
                    sql = "update wr_stock_master set stock='" + rec.Fields[1].Value + "' where item_code ='" + rec.Fields[0].Value + "' and wr_code='" + rec.Fields[2].Value + "' and brn_code='" + rec.Fields[3].Value + "'";
                               tmp = new ADODB.Recordset();

                               tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    rec.MoveNext();
                }

                 sql = "select item_code,sum(stock),brn_code from stock_item  group by item_code,brn_code";
                 rec = new ADODB.Recordset();

                 rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                 while (!rec.EOF)
                 {
                     sql = "update stock_master set stock='" + rec.Fields[1].Value + "' where item_code ='" + rec.Fields[0].Value + "'  and brn_code='" + rec.Fields[2].Value + "'";
                     tmp = new ADODB.Recordset();

                     tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                     rec.MoveNext();
                 }


                 //ADOconn.CommitTrans();
                 MessageBox.Show("Succeeded, OK");


            }
            catch (Exception ex)
            {
                //ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
                
            }
        }

        private void cmbcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_leaders();
        }
    }
}
