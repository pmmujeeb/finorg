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
using CrystalDecisions.CrystalReports.Engine;
using ADODB;
using System.Threading;
namespace FinOrg
{
    public partial class frmitemview : FinOrgForm
    {


        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
        DataSet ds = new DataSet();
        DataView dv2 = new DataView();
        DataView projdv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();
        DataSet ds2 = new DataSet();
        bool isini;
        public frmitemview()
        {
            isini = true;
            InitializeComponent();
           
            txtitem.Focus();
        }

        private void frmsearch_Load(object sender, EventArgs e)
        {
            string sql = "sELECT  itm_cat_code,ITM_CAT_name froM ITEM_CAT where itm_cat_code>0 union select 0,'All' from ITEM_CAT  order by itm_cat_code";

            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
            DataTable dt2 = new DataTable("ITEM_CAT");
            ada2.Fill(dt2);

            cmbcat.DisplayMember = "ITM_CAT_name";
            cmbcat.ValueMember = "itm_cat_code";
            cmbcat.DataSource = dt2;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void load_items()
        {
            try
            {
                isini = true;
                string sql = @"SELECT [ITEM_CODE],[DESCRIPTION] as ENGLISH_NAME  ,[AR_DESC] AS ARABIC_NAME,STOCK ,[ALIAS_NAME] AS SHORT_NAME ,[AVG_PUR_PRICE] AS AVG_COST_PRICE" +
                             ",[RETAIL_PRICE] ,[SALE_PRICE] AS WHOLESALE "
                    //  + " ,[STOCK],[LAST_PUR_PRICE] AS LAST_COST,[BSTOCK] AS TOT_STOCK, [LOCATION]" [RE_ORDER] AS MIN_STOCK ,[VAT_PERCENT]"
                 //               + "   ,[ITM_CAT_CODE],[UNIT],[WR_CODE]  "
                + " FROM [ITEMMASTER]";

                //sql = "select * from hd_itemmaster";
                //sql = sql + " FROM   dbo.HD_ITEMMASTER as I INNER JOIN  dbo.STOCK_MASTER as S ON I.Item_Code = S.Item_Code LEFT JOIN ";
                //sql = sql + " (SELECT SUM(QTY-RQTY) AS ORDERED,ITEM_CODE FROM   dbo.DATA_ENTRY_GRID WHERE TRN_TYPE=12 AND QTY-RQTY>0 GROUP BY ITEM_CODE) AS B ON I.Item_Code = B.Item_Code INNER JOIN ";
                //sql = sql + "dbo.ITEM_CAT ON I.ITM_CAT_CODE = dbo.ITEM_CAT.ITM_CAT_CODE INNER JOIN dbo.UnitMaster ON UNIT = dbo.UnitMaster.Unit_id where i.brn_code= " + Gvar.brn_code ;

                if (cmbcat.SelectedIndex > 0)
                    sql = sql + " where itm_cat_code=" + cmbcat.SelectedValue;

                Conn.Close();
                Conn.Open();
                ada = new SqlDataAdapter(sql, Conn);
                 ds = new DataSet();
                 dt = new DataTable("hd_itemmaster");
                dt.AcceptChanges();
                ///ada.Fill(dt);

                this.ada.Fill(this.ds, "hd_itemmaster");
                dv.Table = ds.Tables[0];
                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);


                this.ds.AcceptChanges();
                //set the table as the datasource for the grid in order to show that data in the grid
                dgv1.DataSource = dv;
                //dgv1.DataMember = "hd_itemmaster";

                dv.AllowEdit = true;
                dgv1.Visible = true;

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);

                dv.AllowEdit = true;
                dv.AllowNew = true;
                dv.AllowDelete = true;
                //return;
               // dgv1.DataSource = dv;
                dv.AllowEdit = true;
                dgv1.Columns[0].Width = 175;
                dgv1.Columns[1].Width = 350;
                dgv1.Columns[2].Width = 350;
                dgv1.Columns[4].Width = 200;
                //dgv1.Columns[1].Width = 250;
                //dgv1.Columns[2].Width = 175;
               // dgv1.Columns[0].Width = 175;
               // dgv1.Columns[0].Name = "Item_Code";
                //dgv1.Columns[1].Name = "description";
                tabControl1.Width = this.Width;
                dgv1.Width = this.Width-50;
                dgv1.Columns[1].ReadOnly = false;
                dgv1.Columns[2].ReadOnly = false;
                dgv1.Columns[3].ReadOnly = false;
                dgv1.Columns[4].ReadOnly = false;
                dgv1.Columns[5].ReadOnly = false;
                 dgv1.Columns[0].ReadOnly = true;
                for ( int i=8 ; i< dgv1.Columns.Count;i++)
                {
                    dgv1.Columns[i].ReadOnly=true;
                }

                dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                if (dgv1.RowCount > 0 )
                dgv1.CurrentCell = dgv1["Item_Code", 0];
               // dgv1.ReadOnly = true;

                dgupdate.DataSource = null;
                dgupdate.Rows.Clear();
                dgupdate.Rows.Add(5);
                dgupdate[0, 0].Value = "English Name";
                dgupdate[0, 1].Value = "Arabic Name";
                dgupdate[0, 2].Value = "Short Name";
                dgupdate[0, 3].Value = "Cost Price";
                dgupdate[0, 4].Value = "Retail Price";
               // dgupdate[0, 5].Value = "Item Code";
                dgupdate.Columns[0].Width = 150;
                dgupdate.Columns[1].Width = 350;
               // dgupdate.Columns[5].Visible = false;
                isini = false;
                //dgupdate[0, 0].Value = "Englis Name";
               // dgupdate[0, 0].Value = "Englis Name";
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void wr_serachitem()
        {
            
         try
            {
                dgv1.EndEdit();
                if (dgv1.CurrentCell == null)
                {
                   // if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
                    return;
                }
                if (dv2.Count>0)   
             dv2.Table.Clear();
                txtitem.Text = "";
                lblitem.Text = "";
                //dgvtrans.Rows.Clear();
                string sql = "SELECT   w.WR_CODE, w.WR_NAME, s.STOCK, s.OP_STOCK fROM  dbo.WR_STOCK_MASTER s INNER JOIN  dbo.WRHOUSE_MASTER as w ON s.WR_CODE = w.WR_CODE where s.Item_Code='" + dgv1["Item_Code", dgv1.CurrentCell.RowIndex].Value + "'";
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("WRHOUSE_MASTER");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                  
                dv1.Table = dtlkp;
                dgv2.DataSource = dv1;
                dgv2.Columns[1].Width = 175;
                dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv2.ReadOnly = true;
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
                    MessageBox.Show("please select a Item Code form The List", "Wrong Item_Code");
                    // if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
                    return;
                }
                string sql = "SELECT ABRV, d.CURDATE, d.INVOICE_NO, G.QTY, G.PRICE,G.QTY*G.PRICE AS TOTAL,G.UNIT, W.WR_NAME, D.TRN_TYPE FROM  dbo.DATA_ENTRY AS D ";
                sql = sql + " INNER JOIN  dbo.DATA_ENTRY_GRID AS G ON (D.TRN_TYPE = G.TRN_TYPE AND D.REC_NO = g.REC_NO) LEFT  JOIN ";
                sql = sql + " dbo.WRHOUSE_MASTER AS W ON D.WHCODE = W.WR_CODE INNER JOIN  dbo.TRN_TYPE ON D.TRN_TYPE = dbo.TRN_TYPE.TRN_CODE WHERE Item_Code='" + dgv1["Item_Code", dgv1.CurrentCell.RowIndex].Value + "' ORDER BY D.REC_NO";
                txtitem.Text = dgv1["Item_Code", dgv1.CurrentCell.RowIndex].Value.ToString();
                lblitem.Text = dgv1["description", dgv1.CurrentCell.RowIndex].Value.ToString();
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("WRHOUSE_MASTER");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);
                  
                dv2.Table = dtlkp;
                dgvtrans.DataSource = dv2;
                dgvtrans.Columns[1].Width = 175;
                dgvtrans.Columns[7].Width = 175;
                dgvtrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvtrans.ReadOnly = true;
               // tabControl1.SelectedTab = tabControl1.TabPages[1];
                dgvtrans.Columns[8].Name = "trn_type";
                dgvtrans.Columns[2].Name = "inv_no";
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
                    dv.RowFilter = "Item_Code LIKE  '%" + txt + "%' OR english_name LIKE '%" + txt + "%' OR arabic_name LIKE '%" + txt + "%' OR short_name LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Item_Code <> '0'";
            }
            catch(Exception ex)
            {
            }
        }
        

        private void btnsearch_Click(object sender, EventArgs e)
        {
            load_items();
            if (dgv1.CurrentCell == null)
            {
                if (dgv1.RowCount > 0) dgv1.CurrentCell = dgv1["Item_Code", 0]; else return;
                
            }
            wr_serachitem();
        }

        private void dgv1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isini) return;
            if (dgv1.CurrentCell == null) return;
            dgupdate[1, 0].Value = dgv1[1, e.RowIndex].Value;
            dgupdate[1, 1].Value = dgv1[2, e.RowIndex].Value;
            dgupdate[1, 2].Value = dgv1[4, e.RowIndex].Value;
            dgupdate[1, 3].Value = dgv1[5, e.RowIndex].Value;
            dgupdate[1, 4].Value = dgv1[6, e.RowIndex].Value;
            dgupdate[2, 0].Value = dgv1[0, e.RowIndex].Value;
            if (dgv1.Columns[1].Width < 250) return;
            wr_serachitem();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string crt;
            string rep_path;

            try
            {
                ReportDocument CrRep = new ReportDocument();
                rep_path = "";

                crt = "";
                {

                    //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                    rep_path = Gvar.report_path + "\\reports\\ItemMaster.rpt";
                    CrRep.Load(rep_path);


                    CrRep.SummaryInfo.ReportTitle = "Material Stock Report for all";

                }
                //if (checkBox1.Checked)
                //    crt = "{STOCK_MASTER.STOCK}>0";

            }
            catch
            {
            }
        }

        private void cmbcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isini) return;
            load_items();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgupdate[1,3].Value==null) dgupdate[1,3].Value="0";
                if(dgupdate[1,4].Value==null) dgupdate[1,4].Value="0";
                //if(dgupdate[1,5].Value==null) dgupdate[1,5].Value="0";
               // if(dgupdate[1,3].Value==null) dgupdate[1,3].Value="0";

                string sql = "update Hd_itemmaster set description='" + dgupdate[1, 0].Value + "',ar_desc='" + dgupdate[1, 1].Value + "',Alias_name='" + dgupdate[1, 2].Value + "' where item_code ='" + dgupdate[2, 0].Value + "'";
                cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();


                sql = "update stock_master set avg_pur_price='" + dgupdate[1, 3].Value + "',retail_price='" + dgupdate[1, 4].Value + "' where item_code ='" + dgupdate[2, 0].Value + "'";
                cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();

                sql = "update barcode set description='" + dgupdate[1, 0].Value + "',description_ar='" + dgupdate[1, 1].Value + "',retail_price='" + dgupdate[1, 4].Value + "' where item_code ='" + dgupdate[2, 0].Value + "'";
                cmd = new SqlCommand(sql, Conn); 
                //cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();
               // dgv1.EndEdit();
               // DataGridViewCell ccell = dgv1.CurrentCell;
               // dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
               // dgv1.CurrentCell = ccell;
               // ds.Tables[0].DataSet.GetChanges();
               //dt.AcceptChanges();
               // ada.Update(ds, "hd_itemmaster");
                load_items();
                MessageBox.Show("Updated Successfully ");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Update Error , Not Updated");
            }
        }

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgupdate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgupdate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               // if (dgupdate.CurrentCell == null) return;
               
                
            }
            catch(Exception ex)
            {

            }
            // dgupdate[1, 0].Value = dgv1[1, e.RowIndex].Value;

        }

        private void dgupdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

            }
        }

        private void frmitemview_Shown(object sender, EventArgs e)
        {
            load_items();
        }

        private void dgv1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isini) return;
            // if (dgupdate.CurrentCell == null) return;
            if (dgv1.CurrentCell == null) return;
            dgupdate[1, 0].Value = dgv1[1, e.RowIndex].Value;
            dgupdate[1, 1].Value = dgv1[2, e.RowIndex].Value;
            dgupdate[1, 2].Value = dgv1[4, e.RowIndex].Value;
            dgupdate[1, 3].Value = dgv1[5, e.RowIndex].Value;
            dgupdate[1, 4].Value = dgv1[6, e.RowIndex].Value;
            dgupdate[2,0].Value = dgv1[0, e.RowIndex].Value;

        }
  
    }
}