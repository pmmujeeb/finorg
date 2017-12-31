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
    public partial class frmdelete : FinOrgForm
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView projdv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();

        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();
        DataSet ds2 = new DataSet();

        DataTable itemdt = new DataTable();
        public frmdelete()
        {
            InitializeComponent();
        }

        private void frmdelete_Load(object sender, EventArgs e)
        {
 

            try
            {
                Conn.Close();
                Conn.Open();

                string sql = "select trn_code, trn_name from TRN_TYPE";

                SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");

                DataSet ds = new DataSet();



                ada1.Fill(ds, "TRN_TYPE");
                cmbtrntype.DisplayMember = "trn_name";
                cmbtrntype.ValueMember = "trn_code";
                cmbtrntype.DataSource = ds.Tables[0];



                
            }



                

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        

          


        private void btnView_Click(object sender, EventArgs e)
        {
        try
        {
            
                string sql = "select [Col1],[Col2] FROM [Grid_Master] where col='1'";
                //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";
            sql="SELECT       a.ITEM_CODE,a.DESCRIPTION,U.UNIT_NAME,  Qty, Price   FROM   HD_ITEMMASTER a INNER JOIN ";
            sql = sql + " (select item_code,round(avg(price),2) as price,round(sum(qty),2) as qty  from     dbo.TRN_ITM_DETAIL b inner join trn_master as m on m.TRN_no=b.TRN_no ";
            sql = sql + " where date_time between convert(datetime,'" + dt1.Value + "' ,103) and convert(datetime,'" + dt2.Value + "' ,103) and m.trn_type= " + cmbtrntype.SelectedIndex + " GROUP BY b.ITEM_CODE,m.trn_type   ) as b ";
             sql = sql + " ON a.ITEM_CODE = b.ITEM_CODE  inner join unitmaster as u on a.unit=u.unit_id  ";
            

         


            ada = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("HD_ITEMMASTER");
                
            dt.AcceptChanges();
                ///ada.Fill(dt);

            this.ada.Fill(ds, "HD_ITEMMASTER");
                dv.Table = dt;
                SqlCommandBuilder cmdBldr = new SqlCommandBuilder(ada);

                dgv1.DataSource = ds;
                dgv1.DataMember = "HD_ITEMMASTER";
                ds.AcceptChanges();
                dgv1.Columns[0].Width = 200;
                dgv1.Columns[1].Width = 400;
                //set the table as the datasource for the grid in order to show that data in the grid

                dgv1.Visible = true;
        }
            catch (Exception ex)
        {

            MessageBox.Show(ex.Message);
            }






        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
                 {
            
                      //          string sql = "select [Col1],[Col2] FROM [Grid_Master] where col='1'";
                      //          //sql = "select [Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col],[ColId] FROM [Grid_Master] where col='1'";
                      //      sql="SELECT       a.ITEM_CODE,a.DESCRIPTION,U.UNIT_NAME,  Qty, Price   FROM   HD_ITEMMASTER a INNER JOIN ";
                      //      sql = sql + " (select item_code,round(avg(price),2) as price,round(sum(qty),2) as qty  from     dbo.TRN_ITM_DETAIL b inner join trn_master as m on m.TRN_no=b.TRN_no ";
                      //      sql = sql + " where date_time between convert(datetime,'" + dt1.Value + "' ,103) and convert(datetime,'" + dt2.Value + "' ,103) and m.trn_type= " + cmbtrntype.SelectedIndex + " GROUP BY b.ITEM_CODE,m.trn_type   ) as b ";
                      //       sql = sql + " ON a.ITEM_CODE = b.ITEM_CODE  inner join unitmaster as u on a.unit=u.unit_id  ";
            

         


                      //      ada = new SqlDataAdapter(sql, Conn);
                      //          ///ada.TableMappings.Add("Table", "Leaders");
                      //          ///


                      //sql = "SELECT STOCK FROM STOCK_ITEM WHERE WR_CODE=" + cmbwh.SelectedValue + " AND  Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;



                      //Recordset tmp = new Recordset();
                      //  tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                      //  decimal qty = 0;
                      //  if (tmp.RecordCount != 0)
                      //  {
                      //      qty = (double)  tmp.Fields[0].Value;
                      //  }
                      //  sql = "SELECT *  FROM WR_STOCK_MASTER WHERE WR_CODE=" + cmbwh.SelectedValue + " AND   Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;

                      //  tmp = new Recordset();
                      //  tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                      //  if (tmp.RecordCount == 0)
                      //  {
                      //      tmp.AddNew();
                      //  }
                      //  tmp.Fields["WR_CODE"].Value = cmbwh.SelectedValue;
                      //  tmp.Fields["brn_CODE"].Value = Gvar._brn_code;
                      //  tmp.Fields["Item_Code"].Value = rec.Fields["Item_Code"].Value;
                      //  tmp.Fields["User"].Value = Gvar._Userid;
                      //  tmp.Fields["stock"].Value = qty;
                      //  tmp.Update();


                      //  sql = "SELECT sum(STOCK) FROM STOCK_ITEM WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;



                      //  tmp = new Recordset();
                      //  tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                      //  qty = 0;
                      //  if (tmp.RecordCount != 0)
                      //  {
                      //      qty = (double)  tmp.Fields[0].Value;
                      //  }

                      //  tmp.Close();

                      //  sql = "Update STOCK_MASTER set stock = " + Math.Round(qty,2) + " WHERE Item_Code ='" + rec.Fields["Item_Code"].Value + "' AND BRN_CODE=" + Gvar._brn_code;
                      //  tmp = new Recordset();
                      //  tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                      //  // ADOconn.Execute(sql, out a, -1);

                      //  tmp = new Recordset();
                      //  //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);


                    }
            catch
            {

            }
                }
    }

}
       
   
    

