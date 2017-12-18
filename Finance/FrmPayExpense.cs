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
    public partial class FrmPayExpense : Form
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();

       
        
        
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
       
        bool isedit;
        bool iserror;
        string lastlookval;
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
       
        int dblclk_row;
        int last_row;
        int last_col;
        int max_level;
        string acntrl;
        bool fnd = false;
        string sql = "";
        bool isini = false;
        public FrmPayExpense()
        {
            InitializeComponent();
            Load_data();
            dtentry.Value = DateTime.Now.Date;
        }

        private void FrmAccMaster_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(0, 0);

            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            
            
            
            
        }

        private void FrmAccMaster_ResizeBegin(object sender, EventArgs e)
        {
           
        }
     

   
        private void FrmAccMaster_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:

                case Keys.Left:

                    if (dginvoice.Visible)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Escape:
                    {
                        GrdLookup.Visible = false;

                    }
                    break;


            }

               

                    if (GrdLookup.Visible)
            {

                switch (e.KeyCode)
                {



                    case Keys.Up:

                        int crow = GrdLookup.CurrentRow.Index;
                        int mros = GrdLookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow > 0)
                            GrdLookup.CurrentCell = GrdLookup.Rows[crow - 1].Cells[0];

                        e.Handled = true;

                        break;
                    case Keys.Down:

                        crow = GrdLookup.CurrentRow.Index;
                        mros = GrdLookup.Rows.Count;
                        // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                        //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                        if (crow < mros - 1)
                            GrdLookup.CurrentCell = GrdLookup.Rows[crow + 1].Cells[0];
                        e.Handled = true;

                        break;
                    
                   


                }

            }
        }

        public class NoArrowKeysDataGridView : DataGridView
        {
            protected override void OnKeyDown(KeyEventArgs e)
            {
                switch (e.KeyData & Keys.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                    case Keys.Left:
                        if (!this.IsCurrentCellInEditMode)
                        {
                            // Swallow arrow keys.
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        break;
                }
                base.OnKeyDown(e);
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                var txt="";

               
                if (dginvoice.Visible)
                {
                   // grdmain.EndEdit();
                    //object txt = grdmain[grdmain.CurrentCell.ColumnIndex, grdmain.CurrentCell.RowIndex].GetEditedFormattedValue(;
                   // var txt = grdmain[grdmain.CurrentCell.ColumnIndex, grdmain.CurrentCell.RowIndex].EditedFormattedValue.ToString();
                    
                        var key = new KeyEventArgs(keyData);
                       // if (keyData.ToString() == "Back")
                            //txtgrd.Text = txtgrd.Text.Substring(0, txtgrd.TextLength - 1);
                        //else
                           // txtgrd.Text = txtgrd.Text + (char)key.KeyCode;
                        //txt = txt + string.Concat(key.KeyValue);
                       
                   // grdmain.BeginEdit(true);
                    
                }
                switch (keyData & Keys.KeyCode)
                {
                    case Keys.Up:
                        //case Keys.Right:
                        
                        break;
                    case Keys.Down:
                        //case Keys.Left:

                        
                        break;

                    case Keys.Escape:
                        {
                            GrdLookup.Visible = false;
                            break;

                        }
                        break;
                    case Keys.Enter:
                        {
                            

                        }
                        break;
                    default:
                       
                        break;


                    //todo special handling

                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

       

      
       
        
       

       private void display_Inv(string Invno,int trntype)
        {
            try
            {
                ADODB.Recordset tmp = new ADODB.Recordset();
                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();


                string sql = "SELECT * FROM ledger where acc_no=" + Invno;

                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if(!tmp.EOF)
                {
                    for (int i=1;i<dginvoice.Rows.Count-1;i++)
                    {


                    }
                    
                    //for (int j = 1; j <= tbl_ary.Count() - 1; j++)
                    //{
                    //}
                }
            }
           catch
            {

            }
        }
       
       private void saveToolStripButton_Click(object sender, EventArgs e)
       {
           iserror = false;
           item_exp();
           if(!iserror)
           save_Expense();

           if(!iserror)
           {
               ADOconn.CommitTrans();
               MessageBox.Show("Expense Updated Successfully!!");

           }
           else
           {
               ADOconn.RollbackTrans();
               //MessageBox.Show("Expense Updated Successfully!!");

           }
       }
       private void item_exp()
       {

           try
           {
               if(txtexpense.Text=="")
               {
                   txtexpense.Text = "0";
                   return;
               }

               if (dginvoice[1, 5].Value == "" || dginvoice[1, 5].Value == null) dginvoice[1, 5].Value = "0";
               if (dginvoice[1, 8].Value == "" || dginvoice[1, 8].Value == null) dginvoice[1, 8].Value = "0";
              
               decimal rate = Convert.ToDecimal( dginvoice [1, 5].Value);
               decimal expamt = Convert.ToDecimal(txtexpense.Text);
               decimal totinvamt = Convert.ToDecimal(dginvoice[1, 8].Value);
               decimal exp = 0;

               for (int i = 0; i < dgv1.Rows.Count; i++)
               {
                   if(dgv1[0,i].Value!="" && dgv1[0,i].Value!=null)
                   {
                       if (dgv1["price", i].Value == "" || dgv1["price", i].Value == null) dgv1["price", i].Value = "0";
                         exp = (Convert.ToDecimal(dgv1["total", i].Value) / totinvamt * 100) * expamt / 100;
                         exp = exp / Convert.ToDecimal(dgv1["qty", i].Value);
                         dgv1["exp", i].Value = exp;
                   }
               }

               iserror = false;

           }
    

           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               iserror = true;
           }

       }
       private void save_Expense()
       {

           try
           {

               //if (isedit)
               //{
               //    if (txtpriv.Text.Substring(1, 1) == "0")
               //    {
               //        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
               //        return;
               //    }
               //}
               //else
               //{

               //    if (txtpriv.Text.Substring(0, 1) == "0")
               //    {
               //        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
               //        return;
               //    }
               //}


               ADODB.Recordset rec = new ADODB.Recordset();
               ADODB.Recordset op = new ADODB.Recordset();
               ADODB.Recordset tmp = new ADODB.Recordset();
               rec = new ADODB.Recordset();
               tmp = new ADODB.Recordset();
               if (ADOconn.State == 0) 
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
               Conn.Close();
               Conn.Open();
               long trnno = 0;
               long trnno2 = 0;
               iserror = false;
             


               string table_name = "";
               string[] tbl_ary = new string[dginvoice.Rows.Count];
               string[,] key_ary = new string[10, 2];
               int c = 0;
              
              
               string sql = "";
               string acc_id = "0";
               int totdigit = 0;
               int curdgit = 0;

               string exp_ac="0";
               string exp_itm_acc="0";

               sql = "SELECT EXP_ACC,EXP_ITEM_AC from AC_OPTIONS WHERE  ac_options.ID =1";
               rec = new ADODB.Recordset();
                   rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                   if (rec.RecordCount != 0)
                   {
                       exp_ac = rec.Fields["EXP_ACC"].Value.ToString();
                         exp_itm_acc = rec.Fields["EXP_ITEM_AC"].Value.ToString();
                       
                       
                   }
                   

               if (exp_ac=="0" || exp_ac==null)
               {
                   MessageBox.Show("Invalid Expense account Defined, Please Contact Administrator");
                   return;

               }

                if (exp_itm_acc=="0" || exp_itm_acc==null)
               {
                   MessageBox.Show("Invalid Item Expense account Defined, Please Contact Administrator");
                   return;

               }


              

               object a;

               ADOconn.BeginTrans();

               sql = "update data_entry set expense_amt=" + txtexpense.Text + " where nyear=" + NYEAR.Text + " and brn_code=" + cmbbranch.SelectedValue + " and trn_type=" + cmbtrntype.SelectedValue + " and invoice_no=" + txtinvno.Text;
                  ADOconn.Execute(sql,out a,1);

                for (int i=0;i<dgv1.Rows.Count;i++)
               {
                   if(dgv1[0,i].Value!="" && dgv1[0,i].Value!=null)
                   {
                        
                        sql = "update data_entry_grid set expense_amt="  +  dgv1["exp", i].Value + " where rec_no=" + dgv1["rec_no", i].Value + " and rownum =" + dgv1["rownum", i].Value;
                  ADOconn.Execute(sql,out a,1);
                       decimal  itmexp ;
                       itmexp = Convert.ToDecimal( dgv1["exp", i].Value) * Convert.ToDecimal(dgv1["qty", i].Value)  ;//* Convert.ToDecimal( dginvoice [1, 5].Value);
                        sql = "update stock_master set AVG_EXPENSE_AMT=((stock*AVG_EXPENSE_AMT)+"+ itmexp + ")/(stock+" + Convert.ToDecimal(dgv1["qty", i].Value) + ") , LAST_EXPENSE_AMT="  +  dgv1["exp", i].Value + " where item_code='" + dgv1["itemcode", i].Value + "'";
                  ADOconn.Execute(sql,out a,1);

                   }
               }


                sql = "update INV_EXP_DETAIL set flag='A' where nyear=" + NYEAR.Text + " and brn_code=" + cmbbranch.SelectedValue + " and trn_type=" + cmbtrntype.SelectedValue + " and invoice_no=" + txtinvno.Text;
                ADOconn.Execute(sql, out a, 1);

                for (int i = 0; i < dgexp.Rows.Count; i++)
                {
                    if (dgexp[0, i].Value != "" && dgexp[0, i].Value != null)
                    {
                        if (dgexp[1, i].Value == "" || dgexp[1, i].Value == null) dgexp[1, i].Value = "0";



                        sql = "select * from dbo.INV_EXP_DETAIL  where EXP_TYPE_CODE = " + dgexp[2, i].Value + " and  nyear=" + NYEAR.Text + " and brn_code=" + cmbbranch.SelectedValue + " and trn_type=" + cmbtrntype.SelectedValue + " and invoice_no=" + txtinvno.Text;
                        rec = new ADODB.Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        if (rec.RecordCount == 0)
                        {
                            rec.AddNew();

                        }




                        // sql = "INSERT INTO [HD_ITEMMASTER]([Item_Code],[DESCRIPTION],AR_DESC,[USER],[ITM_CAT_CODE],[UNIT],[FRACTION],[ALIAS_NAME],[BRN_CODE],BARCODE)";

                        rec.Fields["USER_ID"].Value = Gvar.Userid;
                        rec.Fields["BRN_CODE"].Value = Gvar.brn_code;

                        rec.Fields["TRN_TYPE"].Value = cmbtrntype.SelectedValue;

                        rec.Fields["NYEAR"].Value = NYEAR.Text;
                      rec.Fields["INVOICE_NO"].Value = txtinvno.Text.Trim();
                      rec.Fields["EXP_TYPE_CODE"].Value = dgexp[2, i].Value;
                      rec.Fields["AMOUNT"].Value = dgexp[1, i].Value;
                        rec.Fields["REMARKS"].Value = txtremarks.Text;
                        rec.Fields["FAMOUNT"].Value = dgexp[1, i].Value;
                        rec.Fields["flag"].Value = "U";
                        rec.Fields["exp_date"].Value = dtentry.Value.Date;
                        rec.Update();


                    }
                }


               
               

                        double amt = Convert.ToDouble(txtexpense.Text) ;
                        string DR_CR = "D";
                        string DR_CR1 = "C";
                       
                        object TRN_BY = cmbtrntype.SelectedValue;
                        sql = "select * from TRN_accounts where DR_CR = 'D' AND DOC_NO =  '" + txtinvno.Text.Trim() + "' AND TRN_BY = " + cmbtrntype.SelectedValue + " AND TRN_TYPE = 22";
                        rec = new ADODB.Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                       
                        if (rec.RecordCount == 0)
                        {
                            rec.AddNew();
                            tmp = new ADODB.Recordset();
                            sql = "SELECT * FROM TRNNO";

                            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            //if (tmp.Fields[0].Value)
                            //    txttrn.Text="1";
                            //else

                            if (tmp.Fields[0].Value == DBNull.Value)
                            {
                                trnno = 1;
                            }
                            else
                            {
                                trnno = Convert.ToInt64(tmp.Fields[0].Value.ToString());
                                trnno2 = trnno + 1;
                            }
                            rec.Fields["trn_no"].Value = trnno;
                            rec.Fields["trn_no2"].Value = trnno + 1;
                        }

                       // if (txtrate.Text == "") txtrate.Text = "0";

                        long doc_no = -1 * trnno;

                        rec.Fields["acc_no"].Value = exp_ac;
                        rec.Fields["EntrY_no"].Value = 0;
                        double rate = 1; //Convert.ToDouble(txtrate.Text);// Gvar.Get_Currency_rate(Convert.ToDouble(txtaccno.Text), cmbcurrency.SelectedValue.ToString());
                        rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                        rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                        rec.Fields["F_RATE"].Value = 1;//txtrate.Text;
                        rec.Fields["TRN_BY"].Value = TRN_BY;
                        rec.Fields["DR_CR"].Value = DR_CR;
                        rec.Fields["user_ID"].Value = Gvar.Userid;
                        rec.Fields["PAYBY"].Value = exp_itm_acc;
                        rec.Fields["trn_type"].Value = 22;
                        rec.Fields["SNO"].Value = 1;
                        // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                        rec.Fields["NARRATION"].Value = "Invoice Expense of " + txtinvno + "(" + cmbtrntype.SelectedValue + ")" ;
                        rec.Fields["Voucher_No"].Value = 0;
                        rec.Fields["cost_code"].Value = 0;
                        rec.Fields["dept_code"].Value = 0;
                        rec.Fields["pay_date"].Value = dtentry.Value;
                        rec.Fields["doc_no"].Value = txtinvno.Text.Trim();
                        rec.Fields["NYEAR"].Value = NYEAR.Text;
                        rec.Fields["brn_code"].Value = Gvar.brn_code;
                        rec.Fields["currency"].Value = 1;

                        rec.Update();
           

                        sql = "select * from TRN_accounts where DR_CR = 'C' AND DOC_NO =  '" + txtinvno.Text.Trim() + "' AND TRN_BY = " + cmbtrntype.SelectedValue + " AND TRN_TYPE = 22";
                        rec = new ADODB.Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                       
                        if (rec.RecordCount == 0)
                        {
                            rec.AddNew();
                            tmp = new ADODB.Recordset();
                            sql = "SELECT * FROM TRNNO";

                            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                            //if (tmp.Fields[0].Value)
                            //    txttrn.Text="1";
                            //else

                            if (tmp.Fields[0].Value == DBNull.Value)
                            {
                                trnno = 1;
                            }
                            else
                            {
                                trnno = Convert.ToInt64(tmp.Fields[0].Value.ToString());
                                trnno2 = trnno2 - 1;
                            }
                            rec.Fields["trn_no"].Value = trnno;
                            rec.Fields["trn_no2"].Value = trnno - 1;
                        }

                       // if (txtrate.Text == "") txtrate.Text = "0";

                       

                        rec.Fields["acc_no"].Value = exp_itm_acc;
                        rec.Fields["EntrY_no"].Value = 0;
                         //Convert.ToDouble(txtrate.Text);// Gvar.Get_Currency_rate(Convert.ToDouble(txtaccno.Text), cmbcurrency.SelectedValue.ToString());
                        rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                        rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                        rec.Fields["F_RATE"].Value = 1;//txtrate.Text;
                        rec.Fields["TRN_BY"].Value = TRN_BY;
                        rec.Fields["DR_CR"].Value = DR_CR1;
                        rec.Fields["user_ID"].Value = Gvar.Userid;
                        rec.Fields["PAYBY"].Value = exp_ac;
                         rec.Fields["trn_type"].Value = 22;
                        rec.Fields["SNO"].Value = 2;
                        // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                        rec.Fields["NARRATION"].Value = "Invoice Expense of " + txtinvno + "(" + cmbtrntype.SelectedValue + ")" ;
                        rec.Fields["Voucher_No"].Value = 0;
                        rec.Fields["cost_code"].Value = 0;
                        rec.Fields["dept_code"].Value = 0;
                        rec.Fields["pay_date"].Value = dtentry.Value;
                        rec.Fields["doc_no"].Value = txtinvno.Text.Trim();
                        rec.Fields["NYEAR"].Value = NYEAR.Text;
                        rec.Fields["brn_code"].Value = Gvar.brn_code;
                        rec.Fields["currency"].Value = 1;

                        rec.Update();

                       




           iserror=false;
      



               //  if (!iserror) save_data();


           }

           catch (Exception ex)
           {
               iserror = true;
               MessageBox.Show(ex.Message);
           }

       }

     
       private void search_data(string Item_Code)
       {
           try
           {

              
               Conn.Close();
               Conn.Open();
               //textBox1.Text = Item_Code;
              

               string sql = "sELECT  h.Item_Code,h.DESCRIPTION,h.ITM_CAT_CODE,h.UNIT,h.FRACTION,h.ALIAS_NAME,s.AVG_PUR_PRICE,s.RE_ORDER,S.AVG_PUR_PRICE,H.BARCODE,H.AR_DESC from hd_ITEMMASTER h left join stock_master s on h.Item_Code=s.Item_Code where h.brn_code=" + Gvar.brn_code + "  and h.Item_Code='" + Item_Code + "'";
               SqlCommand cmd = new SqlCommand(sql, Conn);
               //SqlDataReader rd = cmd.ExecuteReader();


               
               


              SqlDataReader rd = cmd.ExecuteReader();



               if (rd.HasRows)
               {
                   while (rd.Read())
                   {
                       if (!string.IsNullOrEmpty(rd[0].ToString()))
                       {


                           for (int i = 0; i <= dginvoice.Rows.Count - 1; i++)
                           {
                               
                           }


                       }




                       isedit = true;
                   }

               }




               rd.Close();
               Conn.Close();
               isini = false;
           }
           //}

           catch (System.Data.SqlClient.SqlException excep)
           {

               MessageBox.Show(excep.Message);

           }
       }

       



       private void Load_data()
       {
           try
           {



               Conn.Close();
               Conn.Open();
               //textBox1.Text = Item_Code;


               string sql = "sELECT  * from item_exp_type";

               SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);

               DataTable dt3 = new DataTable("ExpType");
               ada3.Fill(dt3);

               ((ListBox)lstexptype).DataSource = dt3;
               ((ListBox)lstexptype).DisplayMember = "exp_type_desc";
               ((ListBox)lstexptype).ValueMember = "exp_type_code";

               SqlCommand cmd = new SqlCommand(sql, Conn);

               SqlDataReader rd =  cmd.ExecuteReader();


               int  i=0;
               if (rd.HasRows)
               {
                   while (rd.Read())
                   {
                       if (rd["exp_type_selected"].ToString()=="True")
                       {
                           lstexptype.SetItemChecked(i,true);
                           i++;
                       }
                   }
               }


               rd.Close();
               btnaddexp_Click(null, null);


               sql = "sELECT  * from trn_type where trn_code in ('1','2')";

               SqlDataAdapter adatrntype = new SqlDataAdapter(sql, Conn);

               DataTable dttrntype = new DataTable("TrnType");
               adatrntype.Fill(dttrntype);

               ((ListBox)lstexptype).DataSource = dt3;
               ((ListBox)lstexptype).DisplayMember = "exp_type_desc";
               ((ListBox)lstexptype).ValueMember = "exp_type_code";


               cmbtrntype.DisplayMember = "trn_name";
               cmbtrntype.ValueMember = "trn_code";
               cmbtrntype.DataSource = dttrntype;
               cmbtrntype.SelectedIndex = 1;


               sql = "sELECT  * from branches";

               SqlDataAdapter adabranch = new SqlDataAdapter(sql, Conn);

               DataTable dtbranch = new DataTable("branch");
               adabranch.Fill(dtbranch);

               

               cmbbranch.DisplayMember = "branch_name";
               cmbbranch.ValueMember = "branch_code";
               cmbbranch.DataSource = dtbranch;
               cmbbranch.SelectedValue= Gvar.brn_code;
               dtentry.Value = DateTime.Now;
               NYEAR.Text = dtentry.Value.Year.ToString();

               //SqlCommand cmd = new SqlCommand(sql, Conn);
               ////SqlDataReader rd = cmd.ExecuteReader();





               //SqlDataReader rd = cmd.ExecuteReader();



               //if (rd.HasRows)
               //{
               //    while (rd.Read())
               //    {
               //        if (!string.IsNullOrEmpty(rd[0].ToString()))
               //        {

                           


               //        }




               //        isedit = true;
               //    }

               //}




               //rd.Close();
               Conn.Close();


               //Conn.Close();
               //Conn.Open();
               //saveToolStripButton.Enabled = true;
               //toolRefund.Enabled = true;
               //sql = "sELECT  DISTINCT Item_Code,DESCRIPTION,ITM_CAT_CODE,AVG_PUR_PRICE ,BSTOCK from ITEMMASTER where itm_cat_code <> 0";

               //SqlCommand cmd = new SqlCommand(sql, Conn);
               ////SqlDataReader rd = cmd.ExecuteReader();

               //aditem = new SqlDataAdapter(cmd);

               //itemdt = new DataTable("ITEMMASTER");
               //aditem.Fill(itemdt);
               //grditem.Visible = true;
               //dv.Table = itemdt;

               //grditem.DataSource = dv;
               //grditem.Columns[1].Width = 300;
               //grditem.Columns[0].Width = 175;
               ////MessageBox.Show(rd["isrefund"].ToString());
               ////if (Gvar._SuperUserid != 1)
               ////{
               ////    saveToolStripButton.Enabled = false;


               ////}



               //load_sup_list(-1);
               //_load_stock();
               ////rd.Close();


               ////DataGridViewRow row = (DataGridViewRow)grdsup.Rows[0].Clone();
               ////row.Cells["Column2"].Value = "XYZ";
               ////row.Cells["Column6"].Value = 50.2;


               //// grdsup.DataSource = dt2;
               ////dgv1.Columns[1].Width = 300;

               //sql = "sELECT  acc_no,acc_name froM accounts inner join ac_options on accounts.ACC_TYPE_CODE=ac_options.sup_ac_type ";

               //SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);
               //DataTable dt1 = new DataTable("accounts");
               //ada1.Fill(dt1);
               //CMBSUP.DataSource = dt1;

               //sql = "sELECT  itm_cat_code,ITM_CAT_name froM ITEM_CAT where itm_cat_code<>0 ";

               //SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
               //DataTable dt2 = new DataTable("ITEM_CAT");
               //ada2.Fill(dt2);


               //cmbcat.DataSource = dt2;
               //cmbcatcode.DataSource = dt2;
               //sql = "sELECT  Unit_id,unit_name froM Unitmaster WHERE UNIT_TYPE='I'";

               //SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
               //DataTable dt3 = new DataTable("Unitmaster");
               //ada3.Fill(dt3);
               //cmbunit.DataSource = dt3;



               Conn.Close();
           }
           catch (System.Data.SqlClient.SqlException excep)
           {

               MessageBox.Show(excep.Message);

           }


       }

       private void print_Report(int idx)
       {
           string crt;
           string rep_path;
            bool fnd = false;
           try
           {





               ReportDocument CrRep = new ReportDocument();
               rep_path = "";

               crt = "";
               switch (idx)
               {
                   case 1:
                       {

                           //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                           rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";
                           CrRep.Load(rep_path);

                           CrRep.SummaryInfo.ReportTitle = "Material Stock Report for all";
                       }


                       break;


                   case 2:
                       {

                           //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                           rep_path = Gvar.report_path + "\\reports\\ItemMasterbywh.rpt";
                           CrRep.Load(rep_path);

                           CrRep.SummaryInfo.ReportTitle = "Material Stock Report By WareHouse for all";
                       }


                       break;

                   case 3:
                       {

                         //  crt = "{HD_ITEMMASTER.ITM_CAT_CODE}  =" + cmbcat.SelectedValue;
                           // Convert.ToDouble(Txtitem.Text);
                           rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";

                           CrRep.Load(rep_path);

                          // CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Category " + cmbcat.Text;
                       }


                       break;


                   case 4:
                       {

                           crt = "{STOCK_MASTER.STOCK} <= {STOCK_MASTER.RE_ORDER} ";
                           // Convert.ToDouble(Txtitem.Text);
                           rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";

                           CrRep.Load(rep_path);

                           CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Re-Order Items";
                       }


                       break;
               }

               //crconnectioninfo.ServerName = "Mujeeb";
               //crconnectioninfo.DatabaseName = "Printex";
               //crconnectioninfo.UserID = "sa";
               //crconnectioninfo.Password = "sa0101";





               //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


               if (crt != "")
               {
                   CrRep.RecordSelectionFormula = crt;
               }


               CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
               CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
               CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

               Tables CrTables;

               crconnectioninfo.ServerName = decoder.DataSource;

               crconnectioninfo.DatabaseName = decoder.InitialCatalog;
               crconnectioninfo.UserID = decoder.UserID;
               crconnectioninfo.Password = decoder.Password;



               CrTables = CrRep.Database.Tables;

               foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
               {
                   crtablelogoninfo = CrTable.LogOnInfo;
                   crtablelogoninfo.ConnectionInfo = crconnectioninfo;
                   CrTable.ApplyLogOnInfo(crtablelogoninfo);
               }



               //CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);

               //CrRep.Load();
               //CrRep.ReadRecords();

               //CrRep.Refresh();

               //if (chkprinter.Checked)
               //{


               //    CrRep.PrintToPrinter(1, true, 0, 0);
               //}
               //else
               //{
               FrmrepView frm = new FrmrepView();
               frm.MdiParent = this.ParentForm;

               frm.crv1.ReportSource = CrRep;
               frm.Show();
               //}










               //ConnectionInfo connInfo = new ConnectionInfo();
               //connInfo.ServerName = "dbservername";
               //connInfo.DatabaseName = "dbname";
               //connInfo.UserID = "dbusername";
               //connInfo.Password = "dbpassword";
               //reportViewer.ReportSource = GetReportSource(connInfo);






               CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, "Mujeeb", decoder.InitialCatalog);

               //CrRep.Load();
               // CrRep.ReadRecords();

               //CrRep.Refresh();


           }
           catch (System.Exception excep)
           {

               MessageBox.Show(excep.Message);

           }


       }

       private void label5_Click(object sender, EventArgs e)
       {

       }

       private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
       {

       }

       private void btnaddexp_Click(object sender, EventArgs e)
       {
           string desc = "";
           int j = 0;
          // bool fnd = false;
           
        foreach(object itemChecked in lstexptype.CheckedItems)
        {
             DataRowView castedItem = itemChecked as DataRowView;
              desc = castedItem["exp_type_desc"].ToString();
             object id = castedItem["exp_type_code"];

             fnd = false;

               for ( j = 0; j < dgexp.Rows.Count; j++)
               {
                   if (desc == dgexp[0, j].Value.ToString())
                   {
                       fnd = true;
                       break;
                   }

               }
               if (!fnd)
               {
                   dgexp.Rows.Add(1);
                   dgexp[0, dgexp.Rows.Count - 1].Value = desc;
                   dgexp[2, dgexp.Rows.Count - 1].Value = id;
                   

               }
        }

          



           }

       private void txtinvno_TextChanged(object sender, EventArgs e)
       {
           try
           {
               if (GrdLookup.Visible == true)
               {
                   //dgv1.EndEdit();


                   //dgv1.BeginEdit(false);
                   string txt = txtinvno.Text.Trim();
                   if (txt != "")
                   {
                       dv.RowFilter = "Invoice_NO LIKE  '%" + txt + "%' OR ENAME LIKE '%" + txt + "%'";
                   }
                   else
                       dv.RowFilter = "ENAME <> '0'";

               }

               //btnsave.Enabled = false;
               //btndelete.Enabled = false;


           }
           catch
           { }
       }

       private void txtinvno_KeyDown(object sender, KeyEventArgs e)
       {
           try
           {

               switch (e.KeyCode)
               {

                   case Keys.Control:
                       txtinvno_DoubleClick(sender, null);
                       e.Handled = true;
                       break;
                   case Keys.ControlKey:
                       txtinvno_DoubleClick(sender, null);
                       e.Handled = true;
                       break;
               }

               if (GrdLookup.Visible)
               {
                   switch (e.KeyCode)
                   {
                       case Keys.Enter:

                        if (GrdLookup.Visible)
                        {
                            if (GrdLookup.Rows.Count < 1) return;


                            int lkprow = 0;


                            lkprow = GrdLookup.CurrentCell.RowIndex;
                            int rw = GrdLookup.CurrentCell.RowIndex;

                            txtinvno.TextChanged -= txtinvno_TextChanged;
                            txtinvno.Text = GrdLookup.Rows[rw].Cells[0].Value.ToString();
                            txtrefno.Text = GrdLookup.Rows[rw].Cells[2].Value.ToString();
                            txtinvno.TextChanged += txtinvno_TextChanged;


                            GrdLookup.Visible = false;
                            //dgv1.Focus();
                           
                            return;
                            //e.Handled = true;
                            //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                        }
                        break;
                       case Keys.Up:

                           int crow = GrdLookup.CurrentRow.Index;
                           int mros = GrdLookup.Rows.Count;
                           // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                           //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                           if (crow > 0)
                               GrdLookup.CurrentCell = GrdLookup.Rows[crow - 1].Cells[0];

                           e.Handled = true;

                           break;
                       case Keys.Down:

                           crow = GrdLookup.CurrentRow.Index;
                           mros = GrdLookup.Rows.Count;
                           // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                           //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                           if (crow < mros - 1)
                               GrdLookup.CurrentCell = GrdLookup.Rows[crow + 1].Cells[0];
                           e.Handled = true;

                           break;



                   }
               }

           }

           catch (Exception ex)
           {


           }
       }

       private void txtinvno_Enter(object sender, EventArgs e)
       {
           acntrl = "nyear";
       }

       private void txtinvno_DoubleClick(object sender, EventArgs e)
       {
           try
           {
               Conn.Close();
               Conn.Open();
               SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME,REF_NO,   CURDATE from  DATA_ENTRY WHERE BRN_CODE=" + cmbbranch.SelectedValue  + " AND TRN_TYPE=" + cmbtrntype.SelectedValue + "  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

               SqlDataAdapter ada = new SqlDataAdapter(cmd);


               DataTable dt = new DataTable("DATA_ENTRY");
               ada.Fill(dt);


               // dataGrid1.DataContext = dt.DefaultView;
               GrdLookup.Left = txtinvno.Left;
               GrdLookup.Top = txtinvno.Top + txtinvno.Height;
               dv.AllowEdit = true;
               dv.Table = dt;
               GrdLookup.Tag = "inv";
               GrdLookup.DataSource = dv;
               GrdLookup.Columns[0].Width = 170;
               GrdLookup.Columns[1].Width = 300;
               GrdLookup.Visible = true;
               //grdIssues.Columns[1].ItemStyle.Width = 100;

               //DataGridTableStyle ts = new DataGridTableStyle;

               //foreach (DataControlField column in dgv1.Columns)
               //{
               //    column.ItemStyle.Width = Unit.Pixel(100);
               //}



           }
           catch (System.Data.SqlClient.SqlException excep)
           {

               MessageBox.Show(excep.Message);


           }
       }

       private void txtrefno_DoubleClick(object sender, EventArgs e)
       {
           try
           {
               Conn.Close();
               Conn.Open();
               SqlCommand cmd = new SqlCommand("select Invoice_NO,ENAME, REF_NO,  CURDATE from  DATA_ENTRY WHERE BRN_CODE=" + cmbbranch.SelectedValue + " AND   TRN_TYPE=" + cmbtrntype.SelectedValue + "  ORDER BY CAST(INVOICE_NO AS NUMERIC) DESC", Conn);

               SqlDataAdapter ada = new SqlDataAdapter(cmd);


               DataTable dt = new DataTable("DATA_ENTRY");
               ada.Fill(dt);


               // dataGrid1.DataContext = dt.DefaultView;
               GrdLookup.Left = txtrefno.Left;
               GrdLookup.Top = txtrefno.Top + txtrefno.Height;
               dv.AllowEdit = true;
               dv.Table = dt;
               GrdLookup.Tag = "inv";
               GrdLookup.DataSource = dv;
               GrdLookup.Columns[0].Width = 170;
               GrdLookup.Columns[1].Width = 300;
               GrdLookup.Visible = true;
               //grdIssues.Columns[1].ItemStyle.Width = 100;

               //DataGridTableStyle ts = new DataGridTableStyle;

               //foreach (DataControlField column in dgv1.Columns)
               //{
               //    column.ItemStyle.Width = Unit.Pixel(100);
               //}



           }
           catch (System.Data.SqlClient.SqlException excep)
           {

               MessageBox.Show(excep.Message);


           }
       }

       private void search_mrn()
       {



           ADODB.Connection ADOconn = new ADODB.Connection();
           string sql;
           ADODB.Recordset rec = new ADODB.Recordset();
           ADODB.Recordset tmp = new ADODB.Recordset();
           rec = new ADODB.Recordset();
           tmp = new ADODB.Recordset();

           try
           {

              


               
               isedit = false;
               if (ADOconn.State==0)
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
               Conn.Close();
               Conn.Open();
               rec = new ADODB.Recordset();

               sql = "SELECT * FROM DATA_ENTRY WHERE  BRN_CODE = " + cmbbranch.SelectedValue + " AND NYEAR=" + NYEAR.Text + " AND  TRN_TYPE=" + cmbtrntype.SelectedValue + "  AND INVOICE_NO= '" + txtinvno.Text.Trim() + "'";

               rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               //if (tmp.Fields[0].Value)

               dginvoice.Rows.Clear();
               dgv1.Rows.Clear();
               if (rec.RecordCount > 0)
               {

                   dginvoice.Rows.Add(9);



                   object rec_no = rec.Fields["REC_NO"].Value;
                  
                   

                   dginvoice[0, 0].Value = "Date";
                   dginvoice[1, 0].Value = rec.Fields["CURDATE"].Value;
                   dginvoice[0, 1].Value = "Customer Code";
                   dginvoice[1, 1].Value = rec.Fields["ACCODE"].Value;
                   dginvoice[0, 2].Value = "Customer Name";
                   dginvoice[1, 2].Value = rec.Fields["ename"].Value;
                  
                   dginvoice[0, 3].Value = "Invoice No.";
                   dginvoice[1, 3].Value = rec.Fields["INVOICE_NO"].Value;


                   
                   dginvoice[0, 4].Value = "Currency";
                   dginvoice[1, 4].Value = rec.Fields["currency"].Value;
                   dginvoice[0, 5].Value = "Exch. Rate";
                   dginvoice[1, 5].Value = rec.Fields["CRATE"].Value;


                 

                   txtrefno.Text = rec.Fields["REF_NO"].Value.ToString();

                   dginvoice[0, 6].Value = "Total Amount";
                   dginvoice[1, 6].Value = rec.Fields["FRN_AMOUNT"].Value;

                   dginvoice[0, 7].Value = "Discount";
                   dginvoice[1, 7].Value = (Convert.ToDecimal(rec.Fields["DISC_AMT"].Value.ToString()) / Convert.ToDecimal(dginvoice[1, 5].Value)).ToString();

                   dginvoice[0, 8].Value = "Net Amount";
                   dginvoice[1, 8].Value = (Convert.ToDecimal(rec.Fields["NET_AMOUNT"].Value.ToString()) / Convert.ToDecimal(dginvoice[1, 5].Value)).ToString();

                  // txtremarks.Text = rec.Fields["remarks"].Value.ToString();


                  

                   var a = 0;

                   rec = new ADODB.Recordset();

                   sql = "SELECT  DATA_ENTRY_GRID.*,stock FROM DATA_ENTRY_GRID left join stock_master  on DATA_ENTRY_GRID.Item_Code=stock_master.Item_Code WHERE REC_NO=" + rec_no;

                   rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                   //    //dgv1.Rows.Clear();
                   //for (a=0; a< dgv1.RowCount-1;a++)
                   //{
                   //    dgv1.Rows.RemoveAt(a);
                   //    }
                   int i = 0;
                   // dgv1.Rows.Add(rec.RecordCount+1);

                  


                   // foreach (DataGridViewRow row in this.dgv1.Rows)
                   while (!rec.EOF)
                   {
                       //ds2.Tables[0].Rows.Add();
                       dgv1.Rows.Add();
                       dgv1["barcode", i].Value = rec.Fields["barcode"].Value.ToString();
                       dgv1["itemcode", i].Value = rec.Fields["item_code"].Value.ToString();
                       dgv1["price", i].Value = rec.Fields["FPRICE"].Value.ToString();
                       dgv1["Description", i].Value = rec.Fields["Description"].Value.ToString();
                       dgv1["qty", i].Value = rec.Fields["QTY"].Value.ToString();
                       dgv1["fraction", i].Value = rec.Fields["FRACTION"].Value.ToString();

                       dgv1["unit", i].Value = rec.Fields["Unit"].Value.ToString();
                       dgv1["cost", i].Value = rec.Fields["SALE_PUR_AMT"].Value.ToString();
                       dgv1["total", i].Value = Convert.ToDecimal(rec.Fields["QTY"].Value.ToString()) * Convert.ToDecimal(rec.Fields["FPRICE"].Value.ToString());
                       dgv1["disc", i].Value = rec.Fields["disc"].Value.ToString();

                       dgv1["rec_no", i].Value = rec.Fields["rec_no"].Value.ToString();
                       dgv1["rownum", i].Value = rec.Fields["rownum"].Value.ToString();
                       i = i + 1;
                       rec.MoveNext();

                   }

                 

                   isedit = true;
                  
               }


               else
               {
                   
                   MessageBox.Show("Invalid Invoice Number", "Invalid Invoice Entry");
                   return;
               }

               sql = "select INV_EXP_DETAIL.*,EXP_TYPE_DESC from dbo.INV_EXP_DETAIL inner join Item_Exp_Type on INV_EXP_DETAIL.EXP_TYPE_CODE=Item_Exp_Type.EXP_TYPE_CODE  where   nyear=" + NYEAR.Text + " and brn_code=" + cmbbranch.SelectedValue + " and trn_type=" + cmbtrntype.SelectedValue + " and invoice_no=" + txtinvno.Text;
               rec = new ADODB.Recordset();
               rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               int r=0;
               dgexp.Rows.Clear();
               dgexp.Rows.Add(rec.RecordCount);
                while (!rec.EOF)
              {
               
               dgexp[2, r].Value = rec.Fields["EXP_TYPE_CODE"].Value ;
               dgexp[0, r].Value = rec.Fields["EXP_TYPE_DESC"].Value;
               dgexp[1, r].Value =rec.Fields["AMOUNT"].Value;
               txtremarks.Text= rec.Fields["REMARKS"].Value.ToString();
                dgexp[1, r].Value = rec.Fields["FAMOUNT"].Value;
              
                dtentry.Value = Convert.ToDateTime( rec.Fields["exp_date"].Value.ToString()) ;
                rec.MoveNext();
                r++;

              }
                decimal tot = 0;
                for (int i = 0; i < dgexp.Rows.Count; i++)
                {
                    if (dgexp[0, i].Value != null && dgexp[0, i].Value != "")
                    {
                        if (dgexp[1, i].Value != null && dgexp[1, i].Value == "") dgexp[0, i].Value = "0";
                        tot = tot + Convert.ToDecimal(dgexp[1, i].Value);
                    }
                }

                txtexpense.Text = tot.ToString();
                cmbbranch.Enabled = false;
                cmbtrntype.Enabled = false;
                NYEAR.ReadOnly = true;

           }
           catch (SqlException ex)
           {
               MessageBox.Show(ex.Message);
               
           }

       }

       private void txtrefno_KeyDown(object sender, KeyEventArgs e)
       {
           try
           {

               switch (e.KeyCode)
               {
                   case Keys.Enter:


                       if (GrdLookup.Visible)
                       {
                           if (GrdLookup.Rows.Count < 1) return;


                           int lkprow = 0;


                           lkprow = GrdLookup.CurrentCell.RowIndex;
                           int rw = GrdLookup.CurrentCell.RowIndex;

                           txtinvno.TextChanged -= txtinvno_TextChanged;
                           txtinvno.Text = GrdLookup.Rows[rw].Cells[0].Value.ToString();

                           txtinvno.TextChanged += txtinvno_TextChanged;


                           GrdLookup.Visible = false;
                           //dgv1.Focus();
                           break;
                           return;
                           //e.Handled = true;
                           //this.dgv1.CurrentCell = this.dgv1[dgv1.FirstDisplayedCell.ColumnIndex + 2, cur_row];
                       }
                       break;
                   case Keys.Control:
                       txtinvno_DoubleClick(sender, null);
                       e.Handled = true;
                       break;
                   case Keys.ControlKey:
                       txtinvno_DoubleClick(sender, null);
                       e.Handled = true;
                       break;

                   case Keys.Escape:
                       {
                           GrdLookup.Visible = false;

                       }
                       break;


               }
           }

           catch (Exception ex)
           {


           }

       }

       private void txtrefno_TextChanged(object sender, EventArgs e)
       {
           try
           {
               if (GrdLookup.Visible == true)
               {
                   //dgv1.EndEdit();


                   //dgv1.BeginEdit(false);
                   string txt = txtinvno.Text.Trim();
                   if (txt != "")
                   {
                       dv.RowFilter = "Invoice_NO LIKE  '%" + txt + "%' OR ENAME LIKE '%" + txt + "%'";
                   }
                   else
                       dv.RowFilter = "ENAME <> '0'";

               }

               //btnsave.Enabled = false;
               //btndelete.Enabled = false;


           }
           catch
           { }
       }

       private void txtrefno_Enter(object sender, EventArgs e)
       {
           acntrl = "txtrefno";

       }

       private void GrdLookup_DoubleClick(object sender, EventArgs e)
       {
           try
           {
               int lkprow = 0;


               lkprow = GrdLookup.CurrentCell.RowIndex;

               switch (GrdLookup.Tag.ToString())
               {

                   case "inv":
                       if (GrdLookup.Rows.Count < 1) return;
                       txtinvno.Text = GrdLookup[0, GrdLookup.CurrentCell.RowIndex].Value.ToString();
                       txtrefno.Text = GrdLookup[2, GrdLookup.CurrentCell.RowIndex].Value.ToString();
                       GrdLookup.Visible = false;
                       search_mrn();
                       dgexp.Focus();
                       break;


               }


           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       private void txtinvno_Validated(object sender, EventArgs e)
       {
           if (txtinvno.Text.Trim() == "") return;
           search_mrn();
       }

       

       private void dgexp_Enter(object sender, EventArgs e)
       {
           dgexp.CurrentCell = dgexp["expamount", 0];
       }

       private void dgexp_CellEndEdit(object sender, DataGridViewCellEventArgs e)
       {
           try
           {
               decimal tot = 0;
               for (int i = 0; i < dgexp.Rows.Count ; i++)
               {
                   if (dgexp[0, i].Value != null && dgexp[0, i].Value != "")
                   {
                       if (dgexp[1, i].Value != null && dgexp[1, i].Value == "") dgexp[0, i].Value = "0";
                       tot = tot + Convert.ToDecimal(dgexp[1, i].Value);
                   }
               }

               txtexpense.Text = tot.ToString();
           }
           catch(Exception ex)
           {

           }
       }

       private void toolclose_Click(object sender, EventArgs e)
       {
           this.Dispose();
           this.Close();
       }

       private void dgexp_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {

       }

       private void newToolStripButton_Click(object sender, EventArgs e)
       {
           cmbbranch.Enabled = true;
           cmbtrntype.Enabled = true;
           NYEAR.ReadOnly = false;
           dgv1.Rows.Clear();
           dgexp.Rows.Clear();
           dginvoice.Rows.Clear();
           txtrefno.Text = "";
           txtinvno.Text = "";
           txtrefno.Text = "";

       }
    
    }
}
