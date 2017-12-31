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
using System.Threading;
namespace FinOrg
{
    public partial class frmsearchbysup : FinOrgForm
    {


        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
        DataView dv2 = new DataView();
        DataView projdv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();
        DataSet ds2 = new DataSet();
        string pono = "0";
        public frmsearchbysup()
        {
            InitializeComponent();
            load_supplier();
        }

        private void frmsearch_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void load_items()
        {
            try
            {
                if ( dv.Count>0)
                {
                   
                    dv.Table.Clear();
                }
                if (dgvtrans.CurrentCell == null)
                {
                    // if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
                    return;
                }
            //string sql ="SELECT    I.Item_Code,I.DESCRIPTION, ITM_CAT_NAME, Unit_Name, S.STOCK, S.AVG_PUR_PRICE, S.OP_STOCK, S.LAST_PUR_PRICE ";
            //    sql = sql + " FROM   dbo.HD_ITEMMASTER as I INNER JOIN  dbo.STOCK_MASTER as S ON I.Item_Code = S.Item_Code INNER JOIN ";
            //    sql = sql + "dbo.ITEM_CAT ON I.ITM_CAT_CODE = dbo.ITEM_CAT.ITM_CAT_CODE INNER JOIN dbo.UnitMaster ON UNIT = dbo.UnitMaster.Unit_id";
                string sql = "SELECT     ITEM_CODE, DESCRIPTION, QTY, PRICE, UNIT, Budg_Code FROM  dbo.DATA_ENTRY_GRID where trn_type= " + Convert.ToInt32(dgvtrans["trn_type", dgvtrans.CurrentCell.RowIndex].Value) + " and " + " invoice_no='" + dgvtrans["inv_no", dgvtrans.CurrentCell.RowIndex].Value + "'";
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("hd_itemmaster");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                  
                dv.Table = dtlkp;
                dgv2.DataSource = dv;
                dgv2.Columns[0].Width = 175;
                dgv2.Columns[1].Width = 250;
                dgv2.Columns[2].Width = 175;
               // dgv1.Columns[0].Width = 175;
                dgv2.Columns[0].Name = "Item_Code";
                dgv2.Columns[1].Name = "description";
                
                dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //if (dgv1.RowCount > 0 )
                //dgv1.CurrentCell = dgv1["Item_Code", 0];
                //dgv1.ReadOnly = true;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void load_supplier()
        
    {
            
         try
            {
            //    dgv1.EndEdit();
            //    if (dgv1.CurrentCell == null)
            //    {
            //       // if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
            //        return;
            //    }
           
                txtitem.Text = "";
                lblitem.Text = "";
                //dgvtrans.Rows.Clear();

                Conn.Close();
                Conn.Open();


                string sql = "select top 1 sup_ac_type from ac_options WHERE  ac_options.ID =1";
                bool find;
                int ac_code;
                //rd.Close();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                find = false;
                ac_code = 0;
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


          
             if(pono=="0")

                 sql = "SELECT    cast( ACC_NO as varchar) as acc_no, ACC_NAME, ACC_MOBILE_NO, CONTACT_PERSON FROM   dbo.ACCOUNTS where acc_type_code =  " + ac_code;
             else
                 sql = "SELECT    cast( ACC_NO as varchar) as acc_no, ACC_NAME, ACC_MOBILE_NO, CONTACT_PERSON FROM   dbo.ACCOUNTS where acc_type_code =  " + ac_code + " and acc_no = (select accode from data_entry where invoice_no='" + txtpono.Text.Trim() + "' and trn_type=12)  " ;

                Conn.Close();
                Conn.Open();
                 cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("ACCOUNTS");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                  
                dv1.Table = dtlkp;
                dgv1.DataSource = dv1;
                dgv1.Columns[3].Width = 175;
                dgv1.Columns[1].Width = 250;
                dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv1.ReadOnly = true;
                dgv1.Columns[0].Name = "sup_code";
                dgv1.Columns[1].Name = "sup_name";
                //dgv2.Columns[1].Width = 250;
               // dgv2.Columns[2].Width = 175;
               // dgv1.Columns[0].Width = 175;
        
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        

     private void search_trans()
        {
            
         try
            {
                dgv1.EndEdit();
                if (dgv1.CurrentCell == null)
                {
                    MessageBox.Show("Please select a Supplier from The List", "Wrong Supplier_Code");
                    // if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
                    return;
                }
                //string sql = "SELECT ABRV, d.CURDATE, d.INVOICE_NO, G.QTY, G.PRICE,G.QTY*G.PRICE AS TOTAL,G.UNIT, W.WR_NAME, D.TRN_TYPE FROM  dbo.DATA_ENTRY AS D ";
                //sql = sql + " INNER JOIN  dbo.DATA_ENTRY_GRID AS G ON (D.TRN_TYPE = G.TRN_TYPE AND D.REC_NO = g.REC_NO) LEFT  JOIN ";
                //sql = sql + " dbo.WRHOUSE_MASTER AS W ON D.WHCODE = W.WR_CODE INNER JOIN  dbo.TRN_TYPE ON D.TRN_TYPE = dbo.TRN_TYPE.TRN_CODE WHERE Item_Code='" + dgv1["Item_Code", dgv1.CurrentCell.RowIndex].Value + "' ORDER BY D.REC_NO";
                int accode = Convert.ToInt32(dgv1["sup_code", dgv1.CurrentCell.RowIndex].Value) ;

             string sql =  "SELECT dbo.TRN_TYPE.ABRV, dbo.DATA_ENTRY.INVOICE_NO, dbo.DATA_ENTRY.CURDATE, dbo.DATA_ENTRY.NET_AMOUNT, dbo.DATA_ENTRY.TRN_TYPE,  dbo.DATA_ENTRY.QOUT_NO ";
             if (txtpono.Text.Trim() != "")
                 sql = sql + "  FROM         dbo.DATA_ENTRY INNER JOIN   dbo.TRN_TYPE ON dbo.DATA_ENTRY.TRN_TYPE = dbo.TRN_TYPE.TRN_CODE where (dbo.DATA_ENTRY.TRN_TYPE = 12   and dbo.DATA_ENTRY.INVOICE_NO= '" + txtpono.Text.Trim() + "') OR (dbo.DATA_ENTRY.TRN_TYPE = 2   and dbo.DATA_ENTRY.ORDER_NO= '" + txtpono.Text.Trim() + "') order by DATA_ENTRY.TRN_TYPE desc";
             else
                 sql = sql + "  FROM         dbo.DATA_ENTRY INNER JOIN   dbo.TRN_TYPE ON dbo.DATA_ENTRY.TRN_TYPE = dbo.TRN_TYPE.TRN_CODE where accode= " + accode;
             
             txtitem.Text = dgv1["sup_Code", dgv1.CurrentCell.RowIndex].Value.ToString();
                lblitem.Text = dgv1["sup_name", dgv1.CurrentCell.RowIndex].Value.ToString();
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("DATA_ENTRY");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                  
                dv2.Table = dtlkp;
                dgvtrans.DataSource = dv2;
                dgvtrans.Columns[1].Width = 175;
                dgvtrans.Columns[4].Name = "trn_type";
                dgvtrans.Columns[1].Name = "inv_no";
                dgvtrans.Columns[1].HeaderText = "Document No.";
               // dgvtrans.Columns[7].Width = 175;
                dgvtrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvtrans.ReadOnly = true;
                tabControl1.SelectedTab = tabControl1.TabPages[1];
               
               // dgv2.Columns[2].Width = 175;
               // dgv1.Columns[0].Width = 175;
        
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = textBox1.Text.Trim();
                if (txt != "")
                {
                    dv1.RowFilter = "acc_no LIKE  '%" + txt + "%' OR acc_name LIKE '%" + txt + "%'";
                }
                else
                    dv1.RowFilter = "acc_no <> '0'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void btnsearch_Click(object sender, EventArgs e)
        {
            //load_items();
            pono = "0";
            if (txtpono.Text.Trim() != "")
            {
                pono=txtpono.Text.Trim();
               

                
            }
            load_supplier();
           
        }

        private void dgv1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv1.Columns[1].Width < 250) return;
           // wr_serachitem();
        }

        private void btntrans_Click(object sender, EventArgs e)
        {
            search_trans();
        }

        private void btnvouchers_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvtrans.CurrentCell == null) return;
                int trn_type =Convert.ToInt32( dgvtrans["trn_type", dgvtrans.CurrentCell.RowIndex].Value);
                switch (trn_type)
                {
                
                    case 0:
                        {
                           
                            Gvar.invno = txtitem.Text;
                            Gvar.trntype = 0;
                            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = "000";
                            Form childForm = new FrmAssetMaster();
                             childForm.MdiParent = MDIParent1.ActiveForm;
                           
                                                //childForm.Text = "Window " + childFormNumber++;
                            childForm.Text = "Item Master Entry Screen";
                            childForm.Show(); //MDIParent1.
                        break;
                        }
                    case 2:
                        {
                            //Gvar.invno = dgvtrans["inv_no", dgvtrans.CurrentCell.RowIndex].Value.ToString();
                            //Gvar.trntype = 2;
                            //if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = "000";
                            //Form childForm = new FrmReciept();
                            // childForm.MdiParent = MDIParent1.ActiveForm;
                           
                            ////childForm.Text = "Window " + childFormNumber++;
                            //childForm.Text = "Item Reciept Entry Screen";
                            //childForm.Show(); //MDIParent1.
                            break;
                        }
                    
                    case 7:
                        {
                            
                            break;
                        }

                    case 8:
                        {
                           
                            break;
                        }

                      

                  }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dgvtrans_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            load_items();
        }

        
    }
}
