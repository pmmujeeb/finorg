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


    public partial class frmAccounts : Form
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string ac_code;
        int start_no;
        int end_no;
        int cur_no;
        bool isini;
        bool isedit;
        string sql;
        string sql1;
        bool fnd;
        bool issearch;
        object emp_ac_type;
        object receivable_acno;
        object payable_acno;

        bool permit_save;
        bool permit_delete;
        //string sql;

        private void save_form()
        {

            ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {
                //if (tooltip.Text.Trim().Length < 3)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 3 Digit!!");
                //    return;
                //}

                if (ADOconn.State == 1)
                    ADOconn.Close();


                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();


                Conn.Close();
                // Conn.Open();

                bool isempty;
                isempty = false;

                if (isedit)
                {
                    if (txtpriv.Text.Substring(1, 1) == "0")
                    {
                        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
                        return;
                    }
                }
                else
                {

                    if (txtpriv.Text.Substring(0, 1) == "0")
                    {
                        MessageBox.Show("Insufficient Priveleges ", "Insufficient Priveleges ");
                        return;
                    }
                }

                foreach (Control gb in this.Controls)
                {
                    if (gb is GroupBox)
                    {
                        foreach (Control tb in gb.Controls)
                        {
                            if (tb is TextBox)
                            {
                                if (tb.Tag == "1")
                                {

                                    tb.BackColor = System.Drawing.Color.White;
                                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                                    {
                                        tb.BackColor = System.Drawing.Color.Yellow;
                                        isempty = true;
                                    }
                                }
                            }
                        }
                    }
                }





                if (isempty)
                {
                    MessageBox.Show("Entry Not Completed, Please fill all Yellow Marked fileds!!", "Invalid Entry");
                    return;

                }



                try
                {
                  
                    if (!isedit)
                    {
                        if (txttrn_type.Text == "200")
                            get_ac_no();
                        else


                        if (txtismanual.Text == "0")
                        {
                            if(cmbtbno.SelectedIndex<0)
                            {

                                MessageBox.Show("Invalid TrailBalance Account Number, Please Enter a Valid Number", "Invalid Entry");
                                return;

                            }
                            gen_accno();


                            tmp = new ADODB.Recordset();

                            //sql = "update ACC_TYPE SET CUR_NO = CUR_NO+1 WHERE  ACC_TYPE_CODE=" + cmbtype.SelectedValue;
                            //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        }
                    }

                    if (txtaccno.Text == "")
                    {
                        MessageBox.Show("Invalid Account Number, Please Enter a Valid Number", "Invalid Entry");
                        return;


                    }

                    ADOconn.BeginTrans();

                    if (txtcrlimit.Text.Trim() == "") txtcrlimit.Text = "0";
                    //if (txtvalue.Text.Trim() == "") txtvalue.Text = "0";

                    //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());
                    sql = "SELECT * FROM Accounts where Acc_no ='" + txtaccno.Text.Trim() + "'";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount == 0)
                    {
                        rec.AddNew();



                    }


                    //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());
                    rec.Fields["ACC_NO"].Value = txtaccno.Text.Trim();
                    if (txtarname.Text.Trim() == "") txtarname.Text = txtaccname.Text;
                    if (txtaccname.Text.Trim() == "") txtaccname.Text = txtarname.Text;
                    rec.Fields["ACC_NAME"].Value = txtaccname.Text.Trim();
                    rec.Fields["ACC_ANAME"].Value = txtarname.Text.Trim();
                    rec.Fields["VAT_TINNO"].Value = txtvatno.Text.Trim();
                    
                    
                   
                    rec.Fields["UPDATE_TIME"].Value = DateTime.Now;
                    if (chkacchide.Checked)
                        rec.Fields["flag"].Value = "C";
                    else
                        rec.Fields["flag"].Value = "A";
                    if (cmbacclink.SelectedIndex >= 0)
                        rec.Fields["account_link"].Value = cmbacclink.SelectedValue;
                    else
                        rec.Fields["UPDATE_TIME"].Value = 0;

                    if (cmbGroupac.SelectedIndex >= 0)
                        rec.Fields["Group_Ac"].Value = cmbGroupac.SelectedValue;
                    else
                        rec.Fields["Group_Ac"].Value = txtaccno.Text.Trim();

                    rec.Fields["Def_currency"].Value = cmbcurrency.SelectedValue;

                    rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    //rec.Fields["ACC_Mobile_NO"].Value = txtmobile.Text.Trim();

                    if (txttrn_type.Text=="204")
                    {
                        rec.Fields["Id_Number"].Value = cmbempid.SelectedValue;
                    }

                     rec.Fields["PREFEX_NO"].Value = "0";
                    
                    
                    
                     rec.Fields["LEVEL1_NO"].Value = cmbclass.SelectedIndex;
                     if (txttrn_type.Text == "200")
                     {
                         switch (cmblevel.SelectedIndex)
                         {
                             case 1:
                                 {
                                     rec.Fields["LEVEL2_NO"].Value = 0;
                                     rec.Fields["LEVEL3_NO"].Value = 0;
                                     break;
                                 }
                             case 2:
                                 {
                                     rec.Fields["LEVEL2_NO"].Value = txtaccno.Text.Trim();
                                     rec.Fields["LEVEL3_NO"].Value = 0;
                                     break;

                                 }
                             case 3:
                                 {

                                     rec.Fields["LEVEL2_NO"].Value = cmbtype.SelectedValue.ToString().Substring(0, 2);
                                     rec.Fields["LEVEL3_NO"].Value = cmbtype.SelectedValue;
                                     break;

                                 }
                             case 4:
                                 {
                                     rec.Fields["LEVEL2_NO"].Value = cmbtype.SelectedValue.ToString().Substring(0, 2);
                                     rec.Fields["LEVEL3_NO"].Value = cmbtype.SelectedValue;
                                     break;
                                 }

                         }
                     }
                     
                     
                     switch (txttrn_type.Text)
                     {
                         case "200":
                             {
                                 if (cmbclass.SelectedIndex<2)
                                 rec.Fields["ACC_TYPE"].Value = "BS";
                                 else
                                     rec.Fields["ACC_TYPE"].Value = "PL";
                                 rec.Fields["ACC_TYPE_CODE"].Value = 1;
                                 rec.Fields["GL_ACC_NO"].Value = txtaccno.Text.Trim();
                                 rec.Fields["ACC_LEVEL"].Value = cmblevel.SelectedIndex;
                                 rec.Fields["ACC_CLASS"].Value = cmbclass.SelectedValue;
                                 break;
                             }


                         case "201":
                             {
                                 
                                     rec.Fields["ACC_TYPE"].Value = "CS";
                                     rec.Fields["ACC_TYPE_CODE"].Value = 2;
                                     rec.Fields["ACC_LEVEL"].Value = 4;
                                     rec.Fields["GL_ACC_NO"].Value = cmbtbno.SelectedValue;
                                     rec.Fields["ACC_CLASS"].Value = 1;
                                     rec.Fields["LEVEL2_NO"].Value = 0;
                                     rec.Fields["LEVEL3_NO"].Value = 0;
                                 break;
                             }

                         case "202":
                             {
                                 rec.Fields["ACC_TYPE_CODE"].Value =3;
                                 rec.Fields["ACC_TYPE"].Value = "SP";
                                 rec.Fields["ACC_LEVEL"].Value = 4;
                                 rec.Fields["GL_ACC_NO"].Value = cmbtbno.SelectedValue;
                                 rec.Fields["ACC_CLASS"].Value = 2;
                                 rec.Fields["LEVEL2_NO"].Value = 0;
                                 rec.Fields["LEVEL3_NO"].Value = 0;
                                 break;
                             }
                         case "203":
                             {
                                 rec.Fields["ACC_TYPE_CODE"].Value = 4;
                                 rec.Fields["ACC_TYPE"].Value = "EM";
                                 rec.Fields["ACC_LEVEL"].Value = 4;
                                 rec.Fields["GL_ACC_NO"].Value = cmbtbno.SelectedValue;
                                 rec.Fields["ACC_CLASS"].Value = 2;
                                 rec.Fields["LEVEL2_NO"].Value = 0;
                                 rec.Fields["LEVEL3_NO"].Value = 0;
                                 break;
                             }
                     }


                    rec.Update();
                if  (txttrn_type.Text !="200")

                   // if (!cmbtype.Visible)
                    {
                        rec = new ADODB.Recordset();
                        sql = "SELECT * FROM Accounts_INFO where Acc_no ='" + txtaccno.Text.Trim() + "'";

                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        if (rec.RecordCount == 0)
                        {
                            rec.AddNew();



                        }
                        rec.Fields["ACC_NO"].Value = txtaccno.Text.Trim();
                        rec.Fields["ACC_TELE_NO"].Value = txttelephone.Text.Trim();
                        rec.Fields["ACC_FAX_NO"].Value = txtfaxno.Text.Trim();
                        rec.Fields["ACC_ADDRESS"].Value = txtaddress.Text.Trim();
                        rec.Fields["IBAN_NO"].Value = txtiban.Text.Trim();
                        rec.Fields["BANK_DET"].Value = txtbankdet.Text.Trim();
                        rec.Fields["CONTACT_PERSON"].Value = txtcontact.Text.Trim();
                        rec.Fields["AREA_NAME"].Value = cmbarea.Text.Trim();
                        rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                        rec.Fields["ACC_Mobile_NO"].Value = txtmobile.Text.Trim();
                        rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                        rec.Fields["ACC_Mobile_NO"].Value = txtmobile.Text.Trim();
                        rec.Fields["EMAIL"].Value = txtemailid.Text.Trim();
                        rec.Fields["credit_limit"].Value = Convert.ToDouble(txtcrlimit.Text);
                        rec.Fields["send_sms"].Value = chksms.Checked;

                        rec.Update();
                    }


                    if (txtobcr.Text == "") txtobcr.Text = "0";
                    if (txtobdr.Text == "") txtobdr.Text = "0";

                    if (Convert.ToDouble(txtobcr.Text) - Convert.ToDouble(txtobdr.Text) != 0)
                    {


                        rec = new ADODB.Recordset();

                        sql = "SELECT OP_BAL_AC FROM AC_OPTIONS WHERE ID =1 ";

                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        // 
                        object OP = 0;

                        if (rec.RecordCount > 0)
                        {
                            if (rec.Fields[0].Value != DBNull.Value)
                                OP = rec.Fields[0].Value;

                        }

                        double amt = Convert.ToDouble(txtobcr.Text) - Convert.ToDouble(txtobdr.Text);
                        string DR_CR = "D";
                        string DR_CR1 = "C";
                        if (Convert.ToDouble(txtobcr.Text) - Convert.ToDouble(txtobdr.Text) > 0)
                        {
                            DR_CR = "C";
                            DR_CR1 = "D";
                        }
                        else
                        {
                            DR_CR = "D";
                            DR_CR1 = "C";
                        }

                        int TRN_BY = 12;
                        sql = "select * from TRN_accounts where ACC_NO =  '" + txtaccno.Text.Trim() + "' AND TRN_BY = 12 and Sno = -1";
                        rec = new ADODB.Recordset();
                        rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                        long trnno = 0;
                        long trnno2 = 0;
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

                        if (txtrate.Text == "") txtrate.Text = "0";

                        long doc_no = -1 * trnno;

                        rec.Fields["acc_no"].Value = txtaccno.Text;
                        rec.Fields["EntrY_no"].Value = 0;
                        double rate = Convert.ToDouble(txtrate.Text);// Gvar.Get_Currency_rate(Convert.ToDouble(txtaccno.Text), cmbcurrency.SelectedValue.ToString());
                        rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                        rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                        rec.Fields["F_RATE"].Value = txtrate.Text;
                        rec.Fields["TRN_BY"].Value = TRN_BY;
                        rec.Fields["DR_CR"].Value = DR_CR;
                        rec.Fields["user_ID"].Value = Gvar.Userid;
                        rec.Fields["PAYBY"].Value = OP;
                        //rec.Fields["RQTY"].Value = 0;
                        rec.Fields["SNO"].Value = -1;
                        // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                        rec.Fields["NARRATION"].Value = "Opening Balance ";
                        rec.Fields["Voucher_No"].Value = 0;
                        rec.Fields["trn_type"].Value = txttrn_type.Text;
                        rec.Fields["cost_code"].Value = 0;
                        rec.Fields["dept_code"].Value = 0;
                        rec.Fields["pay_date"].Value = dt1.Value;
                        rec.Fields["doc_no"].Value = doc_no;
                        rec.Fields["NYEAR"].Value = dt1.Value.Year;
                        rec.Fields["brn_code"].Value = Gvar.brn_code;
                        rec.Fields["currency"].Value = cmbcurrency.SelectedValue;

                        rec.Update();


                        sql = "select * from TRN_accounts   where ACC_NO =  '" + OP + "' AND TRN_BY = 12 and Sno = -2";
                        ADODB.Recordset rec1 = new ADODB.Recordset();
                        rec1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        if (rec1.RecordCount == 0)
                        {
                            rec1.AddNew();
                            rec1.Fields["trn_no"].Value = trnno + 1;
                            rec1.Fields["trn_no2"].Value = trnno;

                        }

                        rec1.Fields["acc_no"].Value = OP;
                        rec1.Fields["EntrY_no"].Value = 0;

                        rec1.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                        rec1.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                        rec1.Fields["F_RATE"].Value = rate;
                        rec1.Fields["TRN_BY"].Value = TRN_BY;
                        rec1.Fields["DR_CR"].Value = DR_CR1;
                        rec1.Fields["user_ID"].Value = Gvar.Userid;
                        rec1.Fields["PAYBY"].Value = txtaccno.Text;
                        //rec.Fields["RQTY"].Value = 0;
                        rec1.Fields["SNO"].Value = -2;
                        rec1.Fields["cost_code"].Value = 0;
                        rec1.Fields["dept_code"].Value = 0;
                        rec.Fields["trn_type"].Value = txttrn_type.Text;
                        // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                        rec1.Fields["NARRATION"].Value = "OPENING BALANCE";
                        rec1.Fields["pay_date"].Value = dt1.Value;
                        rec1.Fields["doc_no"].Value = doc_no;
                        // rec.Fields["NYEAR"].Value = dt1.Value.Year;
                        // ALTERNATIVE ACCOUUNT NUMBER
                        //rec1.Fields["CostCode"].Value = dgv1["CostCode", i].Value;
                        rec1.Fields["NYEAR"].Value = dt1.Value.Year;
                        rec1.Fields["brn_code"].Value = Gvar.brn_code;
                        rec1.Fields["Voucher_No"].Value = 0;
                        rec1.Fields["currency"].Value = cmbcurrency.SelectedValue; ;
                        rec1.Update();





                    }


                    ADOconn.CommitTrans();
                    isedit = true;
                    isini = false;
                    MessageBox.Show("Successfully Saved");
                    //cmbtype_SelectedIndexChanged_1(null, null);
                    populate_acc();
                    //load_leaders();
                }
                catch (Exception ex)
                {
                    ADOconn.RollbackTrans();
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void load_leaders()
        {
            Conn.Close();
            Conn.Open();

            try
            {
                form_ini();
                permit_delete = true;
                permit_save = true;
                if (txtpriv.Text.Substring(2, 1) == "0")
                {
                    permit_delete = false;
                    tooldelete.Visible = false;
                //    return;
                }

                if (txtpriv.Text.Substring(1, 1) == "0")
                {
                    permit_save = false;
                    saveToolStripButton.Enabled = false;
                    //    return;
                }


                isedit = true;
                string sql = "";
                sql1 = "";
                if (ADOconn.State == 0)
                {
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


                }

                cmblevel.SelectedIndex = 0;
                ADODB.Recordset tmp = new ADODB.Recordset();

                sql = "SELECT EMP_AC_TYPE,RECEIVABLE_ACC,PAYABLE_ACC FROM AC_OPTIONS WHERE ID =1";

                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                emp_ac_type = 0;
                if (tmp.RecordCount > 0)
                {
                    emp_ac_type = Convert.ToInt64(tmp.Fields[0].Value);
                    receivable_acno = Convert.ToInt64(tmp.Fields[1].Value);
                    payable_acno = Convert.ToInt64(tmp.Fields[2].Value);

                }
                sql = "select acc_CLASS_code,acc_CLASS_name from acc_CLASS Union select 0 , 'All'  order by acc_CLASS_code ";

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("acc_class");
                ada3.Fill(dt3);

                cmbclass.DisplayMember = "acc_class_name";
                cmbclass.ValueMember = "acc_class_code";
                cmbclass.DataSource = dt3;




                sql = "select area_code,area_name from area_master ";

                SqlDataAdapter ada4 = new SqlDataAdapter(sql, Conn);
                DataTable dt4 = new DataTable("area_Master");
                ada4.Fill(dt4);

                cmbarea.DisplayMember = "area_name";
                cmbarea.ValueMember = "area_code";
                cmbarea.DataSource = dt4;
                sql = "select CURRENCY_code,CURRENCY_code +' : ' +  cast(CURRENCY_RATE as varchar) AS CURRENCY from currency_master ";

                SqlDataAdapter adacurr = new SqlDataAdapter(sql, Conn);
                DataTable dtcurr = new DataTable("currency");
                adacurr.Fill(dtcurr);

                cmbcurrency.DisplayMember = "CURRENCY";
                cmbcurrency.ValueMember = "CURRENCY_code";

                cmbcurrency.DataSource = dtcurr;
                cmbcurrency.SelectedIndex = 0;
                switch (txttrn_type.Text)
                {
                    case "200":

                        //sql = "select acc_type_code,acc_type_name from acc_type inner join ac_options on acc_type_code <> cus_ac_type and acc_type_code <> sup_ac_type  ";
                        sql = "select acc_type_code,acc_type_name from acc_type  where gl_type=1";
                        //sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME from Accounts inner join ac_options on acc_type_code <> cus_ac_type and acc_type_code <> sup_ac_type ";
                        sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME from Accounts WHERE ACC_TYPE_CODE=1";
                        if (tabControl1.TabPages.Count > 1)
                            tabControl1.TabPages.RemoveAt(1);
                        lblcrlimit.Visible = false;
                        txtcrlimit.Visible = false;
                        

                        break;
                    case "201":
                        sql = "select acc_no,acc_name from accounts  inner join ac_options on LEVEL3_NO = cus_ac_type AND  ac_options.ID =1 WHERE ACC_TYPE_CODE=1  ";
                        // sql = "select acc_type_code,acc_type_name from acc_type  where gl_type=1";
                        sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME from Accounts WHERE ACC_TYPE_CODE=1";
                        txtaccno.ReadOnly = false;
                        cmbtype.Visible = false;
                        lblaccclass.Visible = false;
                        cmbclass.Visible = false;
                        lblaccno.Text = "Customer No";
                        lblacctype.Visible = false;
                        cmbclass.SelectedIndex = 0;
                        lblcurrency.Visible = true;
                        btncurrency.Visible = true;
                        cmbcurrency.Visible = true;
                        txtrate.Visible = true;
                        chksms.Visible = true;
                        chksms.Checked = true;
                        lblGLno.Visible = true;
                        cmbtbno.Visible = true;
                        cmblevel.Visible = false;
                        lbllevel.Visible = false;
                        newToolStripButton.Enabled = true;
                        saveToolStripButton.Enabled = true;
                        tooldelete.Enabled = true;
                        break;
                    case "202":
                        cmbtype.Visible = false;
                        sql = "select acc_no,acc_name from accounts inner join ac_options on  LEVEL3_NO = sup_ac_type AND  ac_options.ID =1  WHERE ACC_TYPE_CODE=1";
                        sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME from Accounts WHERE ACC_TYPE_CODE=3";
                        txtaccno.ReadOnly = false;
                        cmbtype.Visible = false;
                        lblaccclass.Visible = false;
                        cmbclass.Visible = false;
                        lblaccno.Text = "Supplier No";
                        lblacctype.Visible = false;
                        cmbclass.SelectedIndex = 1;
                        lblcurrency.Visible = true;
                        cmbcurrency.Visible = true;
                        txtrate.Visible = true;
                        btncurrency.Visible = true;
                        lblGLno.Visible = true;
                        cmbtbno.Visible = true;
                       cmblevel.Visible = false;
                        lbllevel.Visible = false;
                         newToolStripButton.Enabled = true;
                        saveToolStripButton.Enabled = true;
                        tooldelete.Enabled = true;
                        break;

                }






                ada = new SqlDataAdapter(sql, Conn);
                dt = new DataTable("Acc_group");

                ada.Fill(dt);
                cmbtbno.DisplayMember ="acc_name";
                cmbtbno.ValueMember = "acc_no";
                cmbtbno.DataSource = dt;
                if (txttrn_type.Text == "201")
                    cmbtbno.SelectedValue = receivable_acno;
                if(txttrn_type.Text=="202")
                cmbtbno.SelectedValue = payable_acno;

                //dv.Table = dt;

                //dgv1.DataSource = dv;
                //dgv1.Visible = true;

                //dgv1.Columns[0].HeaderText = "Acc.Code";
                //dgv1.Columns[1].HeaderText = "Acc. Name";

                SqlDataAdapter adalink = new SqlDataAdapter(sql1, Conn);


                //ds = new DataSet();
                DataSet ds1 = new DataSet();



                dgv1.Visible = true;

                //this.ada.Fill(this.ds, "Accounts");
                adalink.Fill(ds1, "Accounts");



                cmbacclink.DisplayMember = "ACC_NAME";
                cmbacclink.ValueMember = "ACC_NO";

                cmbacclink.DataSource = ds1.Tables[0];
                // cmbacclink.Sorted = true;
                cmbacclink.SelectedIndex = -1;

                dgv1.Columns[1].Width = 380;
                if (dgv1.Rows.Count > 0)
                    dgv1.CurrentCell = dgv1[0, 0];



                isedit = false;

                isini = false;
                // dgv1.Columns[2].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void delete_leaders()
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to Delete this Acccount,Please Make sure?", "Delete Chart Of Account", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                if (ADOconn.State == 1)
                    ADOconn.Close();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();
                sql = "SELECT count(*) FROM trn_Accounts where pay_amount > 0 and  Acc_no ='" + txtaccno.Text.Trim() + "'";


                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockPessimistic, 1);

                if (rec.RecordCount > 0)
                {
                    MessageBox.Show("There Is a Transaction Found For This Account, Cannot Delete This Account", "Invalid Deletion for Account");
                    return;
                }

                sql = "SELECT *  FROM Acc_Master where Acc_no ='" + txtaccno.Text.Trim() + "'";

                rec = new ADODB.Recordset();
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, 0);

                if (rec.RecordCount > 0)
                {
                    MessageBox.Show("System Generated Account Cannot Delete", "Invalid Deletion for Account");
                    return;
                }


                ADOconn.BeginTrans();
                object a;
                sql = "delete from Accounts_info where   Acc_no ='" + txtaccno.Text.Trim() + "'";

                ADOconn.Execute(sql, out a);
                sql = "delete from Accounts where   Acc_no ='" + txtaccno.Text.Trim() + "'";

                ADOconn.Execute(sql, out a);
                ADOconn.CommitTrans();
                MessageBox.Show("Request Succuessfully Completed ");

                populate_acc();

            }
            catch (Exception ex)
            {
                ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }
        }


        public frmAccounts()
        {
            InitializeComponent();
            txtpriv.Text = Gvar.frm_priv.ToString();
            txttrn_type.Text = Gvar._trntype.ToString();
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

        

       

        private void button1_Click(object sender, EventArgs e)
        {

        }



        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dgv1.CurrentCell == null) return;
                if (isini) return;
                isini = true;
                Conn.Close();
                Conn.Open();
                dgv1.EndEdit();
                //DataGridViewCell ccell = dgv1.CurrentCell;
                //dgv1.CurrentCell = dgv1.Rows[dgv1.RowCount - 1].Cells[1];
                //dgv1.CurrentCell = ccell;

                if (string.IsNullOrEmpty(dgv1[dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex].Value.ToString())) return;

                ac_code = dgv1[0, dgv1.CurrentCell.RowIndex].Value.ToString();

                SqlDataReader rd;

//                string sql = @"SELECT     A.ACC_NO, A.ACC_NAME, A.ACC_TYPE_CODE, A.ACC_CLASS, A.ID_NUMBER, A.FLAG, 
//                      A.ACC_ID, I.ACC_TELE_NO, I.ACC_MOBILE_NO, I.ACC_FAX_NO, I.ACC_ADDRESS, 
//                      I.CREDIT_LIMIT, I.CONTACT_PERSON,I.EMAIL,A.ACCOUNT_LINK,CREDIT_LIMIT,AREA_NAME,DEF_CURRENCY,send_sms
//                        FROM         ACCOUNTS AS A LEFT JOIN 
//                      ACCOUNTS_INFO AS I ON A.ACC_NO = I.ACC_NO WHERE A.ACC_NO =" + ac_code;
                string sql = @"SELECT     A.*,I.ACC_TELE_NO, I.ACC_MOBILE_NO, I.ACC_FAX_NO, I.ACC_ADDRESS, 
                      I.CREDIT_LIMIT, I.CONTACT_PERSON,I.EMAIL,A.ACCOUNT_LINK,CREDIT_LIMIT,AREA_NAME,DEF_CURRENCY,send_sms,IBAN_NO,BANK_DET,VAT_TINNO
                        FROM         ACCOUNTS AS A LEFT JOIN 
                      ACCOUNTS_INFO AS I ON A.ACC_NO = I.ACC_NO WHERE A.ACC_NO =" + ac_code;

                SqlCommand cmd = new SqlCommand(sql, Conn);
                rd = cmd.ExecuteReader();
                bool find = false;
                form_ini();
                while (rd.Read())
                {
                    try
                    {
                        isedit = true;
                        txtaccno.Text = rd["ACC_NO"].ToString();
                        txtaccname.Text = rd["ACC_NAME"].ToString();
                        txtarname.Text = rd["ACC_ANAME"].ToString();
                        cmbcurrency.SelectedValue = rd["DEF_CURRENCY"].ToString();
                       // cmbclass.SelectedValue = rd["ACC_CLASS"];
                       // cmbtype.SelectedValue = rd["acc_type_code"];

                        txtidnumber.Text = rd["Id_Number"].ToString();
                        txtmobile.Text = rd["ACC_Mobile_NO"].ToString();

                        txtaddress.Text = rd["ACC_ADDRESS"].ToString();
                        txtiban.Text = rd["IBAN_NO"].ToString();
                        txtvatno.Text = rd["VAT_TINNO"].ToString();

                        txtbankdet.Text = rd["BANK_DET"].ToString();
                        txtcontact.Text = rd["CONTACT_PERSON"].ToString();
                        txtemailid.Text = rd["EMAIL"].ToString();
                        txttelephone.Text = rd["ACC_TELE_NO"].ToString();
                        txtfaxno.Text = rd["ACC_FAX_NO"].ToString();
                        txtcrlimit.Text = rd["credit_limit"].ToString();
                        cmbcurrency.SelectedValue = rd["DEF_CURRENCY"].ToString();
                       // txtheadac.Text =

                        if (cmbacclink.Items.Count > 0 && !rd["ACCOUNT_LINK"].Equals(DBNull.Value))
                            cmbacclink.SelectedValue = rd["ACCOUNT_LINK"].ToString();
                        if (cmbGroupac.Items.Count > 0 && !rd["GROUP_AC"].Equals(DBNull.Value))
                            cmbGroupac.SelectedValue = rd["GROUP_AC"].ToString();

                        if (cmbtbno.Items.Count > 0 && !rd["gl_acc_no"].Equals(DBNull.Value))
                            cmbtbno.SelectedValue = rd["gl_acc_no"].ToString();

                        chksms.Checked = false;
                        if (!rd["send_sms"].Equals(DBNull.Value))
                            chksms.Checked = Convert.ToBoolean(rd["send_sms"].ToString());


                        string a = rd["group_ac"].ToString();

                        if (!rd["group_ac"].Equals(DBNull.Value))
                            cmbGroupac.SelectedValue = rd["group_ac"].ToString();
          
                        if (rd["flag"].ToString() == "C")

                            chkacchide.Checked = true;


                        //if (cmbtype.SelectedValue.ToString() == emp_ac_type.ToString())
                        //{
                        //    cmbempid.SelectedValue = txtidnumber.Text;
                        //}

                        string AREA = rd["AREA_NAME"].ToString();

                        cmbarea.Text = AREA;

                        isini = false;
                    }

                    catch (Exception ex)
                    {
                        rd.Close();
                    }



                    isini = false;

                    find = true;
                    isedit = true;

                }
                rd.Close();




                txtobcr.Text = "0";
                txtobdr.Text = "0";
                txtclcr.Text = "0";
                txtcldr.Text = "0";
                //rd.Close();
                sql = @"SELECT     PAY_AMOUNT,DR_CR,PAY_DATE FROM TRN_ACCOUNTS 
                         WHERE ACC_NO =" + ac_code + " AND TRN_BY=12 AND SNO=-1";

                cmd = new SqlCommand(sql, Conn);
                rd = cmd.ExecuteReader();

                while (rd.Read())
                    try
                    {
                        {
                            if (rd["dr_cr"].ToString() == "C")
                            {
                                txtobcr.Text = rd["pay_amount"].ToString();
                            }
                            else
                            {
                                txtobdr.Text = rd["pay_amount"].ToString();
                            }
                            dt1.Value = Convert.ToDateTime(rd["pay_date"].ToString());
                        }

                    }
                    catch { };
                rd.Close();





                sql = @"SELECT     cr_amount-dr_amount  as balance FROM TRN_ACC_SUM 
                         WHERE ACC_NO ='" + ac_code + "'";
                cmd.Cancel();
                cmd = new SqlCommand(sql, Conn);
                rd = cmd.ExecuteReader();
                try
                {
                    while (rd.Read())
                    {
                        if (Convert.ToDouble(rd["BALANCE"].ToString()) > 0)
                        {
                            txtclcr.Text = Math.Abs(Convert.ToDouble(rd["BALANCE"].ToString())).ToString();
                        }
                        else
                        {
                            txtcldr.Text = Math.Abs(Convert.ToDouble(rd["BALANCE"].ToString())).ToString();
                        }
                    }
                }
                catch { }

                rd.Close();
                isini = false;
            }


            catch (Exception ex)
            {
                isini = false;
                MessageBox.Show(ex.Message);

            }

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            form_ini();
            //if (cmbtype.SelectedValue.ToString() == emp_ac_type.ToString())
            //{

            //    cmbclass.SelectedIndex = 2;
            //}

            get_ac_no();
            isini = false;
        }
        private void form_ini()
        {
            isedit = false;
            txtaccno.Text = "";
            txtaccname.Text = "";
            //if (cmbclass.Items.Count>0)
            //cmbclass.SelectedIndex = 1;

            cmbempid.SelectedIndex = -1;
            txtidnumber.Text = "";
            txttelephone.Text = "";
            txtmobile.Text = "";
            txttelephone.Text = "";
            txtfaxno.Text = "";
            //cmbclass.SelectedIndex = -1;
            txtarname.Text = "";
            txtaddress.Text = "";
            txtcontact.Text = "";
            cmbacclink.SelectedIndex = -1;
            txtobcr.Text = "";
            txtobdr.Text = "";
            txtclcr.Text = "";
            txtcldr.Text = "";
            txtvatno.Text = "";
            txtcrlimit.Text = "";
            txtiban.Text = "";
            txtbankdet.Text = "";
            cmbarea.SelectedIndex = -1;
            if (txtismanual.Text == "1")
            {
                txtaccno.BackColor = Color.White;
                txtaccno.ReadOnly = false;
                txtaccno.Focus();
            }

        }

        private void gen_accno()
        {

            ADODB.Connection ADOconn = new ADODB.Connection();
            ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {
                //if (tooltip.Text.Trim().Length < 3)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 3 Digit!!");
                //    return;
                //}


                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();

                Conn.Close();
                // Conn.Open();






                tmp = new ADODB.Recordset();

                if (txttrn_type.Text == "201")
                {
                    sql = "SELECT max(acc_no)+1  FROM ACCOUNTS  WHERE  ACC_TYPE_CODE=2" ;
                }
                if (txttrn_type.Text == "202")
                {
                    sql = "SELECT max(acc_no)+1  FROM ACCOUNTS  WHERE  ACC_TYPE_CODE=3";
                }
                if (txttrn_type.Text == "203")
                {
                    sql = "SELECT max(acc_no)+1  FROM ACCOUNTS  WHERE  ACC_TYPE_CODE=4";
                }
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockReadOnly, 1);
                //if (tmp.Fields[0].Value)
                //    txttrn.Text="1";
                //else



                long trn;
                if (tmp.RecordCount == 0)
                    trn = 0;
                else
                    trn = Convert.ToInt64( tmp.Fields[0].Value);
                txtaccno.Text = trn.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isedit == false && txtismanual.Text == "0")
                gen_accno();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Trim();
            if (txt != "")
            {
                dv.RowFilter = "acc_no LIKE  '%" + txt + "%' OR acc_name LIKE '%" + txt + "%'";
            }
            else
                dv.RowFilter = "acc_no <> '0'";
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbGroupac_DropDown(object sender, EventArgs e)
        {

        }

        private void cmbtype_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            try
            {
                //if (cmbtype.SelectedIndex < 1) return;
                form_ini();
                isini = false;
                populate_acc();
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = MDIParent1.find_tag("cuurencyMenuItem");

            Form childForm = new Frmcurrency();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Currency Entry/Edit Screen";
            childForm.Show();
        }

        private void cmbcurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] currate = cmbcurrency.Text.Split(':');
                txtrate.Text = currate[1];
            }
            catch
            {

            }

        }

        private void frmAccounts_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.F1:

                    if (newToolStripButton.Enabled)
                    {
                        newToolStripButton_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F4:

                    if (printToolStripButton.Enabled)
                    {

                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F2:

                    if (saveToolStripButton.Enabled)
                    {
                        saveToolStripButton_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F6:

                    if (SearchToolStripButton.Enabled)
                    {
                        SearchToolStripButton_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F8:

                    if (toolclose.Enabled)
                    {
                        toolclose_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;



                case Keys.Enter:
                    bool nextskip = false;
                    string btn;
                    btn = this.ActiveControl.GetType().ToString();
                    if (this.ActiveControl.GetType().ToString() == "System.Windows.Forms.Button") nextskip = true;

                    if (!nextskip)
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);

                    }

                    break;

                case Keys.Escape:
                    //SendKeys.Send("{Tab}");
                    txtaccno.Focus();


                    //e.Handled = true;

                    break;

            }




        }

        private void cmbempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtype.SelectedValue.ToString() == emp_ac_type.ToString())
            {
                txtaccname.Text = cmbempid.Text;
            }
        }

        private void dgv1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgv1.CurrentCell == null) return;
            if (dgv1[0, dgv1.CurrentCell.RowIndex].Value == null) return;
            dgv1_CellContentClick(sender, null);




























        }

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    isini = false;
                    if (cmbclass.SelectedIndex < 0) return;
                    populate_headac();
                    populate_acc();

                    if (ADOconn.State == 0)
                    {
                        ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


                    }

                   // if (cmbclass.SelectedIndex < 1) return;
                    populate_headac();



                    ADODB.Recordset tmp = new ADODB.Recordset();

                    tmp.Open("select ismanual from acc_type where acc_type_code= " + cmbtype.SelectedValue, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                    txtismanual.Text = "0";
                    if (tmp.RecordCount > 0)
                    {
                        if (tmp.Fields[0].Value != DBNull.Value)
                            if (tmp.Fields[0].Value.ToString() == "True")
                            {
                                txtismanual.Text = "1";
                                txtaccno.BackColor = Color.White;
                                txtaccno.ReadOnly = false;

                                
                            }

                    }

                }


                catch (SqlException sex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void populate_acc()
        {
            try
            { 

                if((cmbclass.SelectedIndex<1 || cmblevel.SelectedIndex<2 || cmbtype.SelectedIndex<1) && txttrn_type.Text=="200")
                {
                    newToolStripButton.Enabled = false;
                    saveToolStripButton.Enabled = false;
                    tooldelete.Enabled = false;

                }
                else
                {
                    newToolStripButton.Enabled = true;
                    saveToolStripButton.Enabled = permit_save;
                    tooldelete.Enabled = permit_delete;

                }

                if (isini) return;
                isini = true;
               
                lblempid.Visible = false;
                cmbempid.Visible = false;
                string sql="";
                string crt1 = "1=1";
                string crt2 = "1=1";
                string crt3 = "1=1";
                if (cmbclass.SelectedIndex < 1)
                {

                }
                else
                {
                    crt1 = "acc_class = " + cmbclass.SelectedValue;

                }

                if (cmblevel.SelectedIndex <  1)
                {
                   
                }
                else
                {

                    crt2 =  "acc_level =" + cmblevel.SelectedIndex;
                }


                if (cmbtype.SelectedIndex < 1)
                {

                }
                else
                {

                    crt2 = crt2 + "  and level"+(Convert.ToInt16(cmblevel.SelectedIndex)-1).ToString() +  "_NO =" + cmbtype.SelectedValue;
                }


               




               

                //case "201":
                //    sql = "select acc_type_code,acc_type_name from acc_type inner join ac_options on acc_type_code = cus_ac_type   ";

                //    break;
                //case "202":
                //    cmbtype.Visible = false;
                //    sql = "select acc_type_code,acc_type_name from acc_type inner join ac_options on  acc_type_code = sup_ac_type ";

                //    break;





                //isini = true;

               
             
                Conn.Close();
                Conn.Open();
                // ada = new SqlDataAdapter("SELECT  * from Accounts where acc_type_code=" + cmbtype.SelectedValue + " order by acc_name", Conn);
                dt.Clear();

                //ada.Fill(dt);

                switch(txttrn_type.Text)
                {
                    case  "200":
                        {
                            sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =1 and " + crt1 + " and " + crt2 + " order by acc_no";
                            sql = "select acc_name,acc_no from accounts where " + crt1 + " order by acc_name";
                            break;
                        }
                    case "201":
                        {
                            sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =2  order by acc_no";
                            sql = "select acc_name,acc_no from accounts where acc_type_code =2  order by acc_name";
                            break;
                        }
                    case "202":
                        {
                            sql1 = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =3  order by acc_no";
                            sql = "select acc_name,acc_no from accounts where acc_type_code =3  order by acc_name";
                            break;
                        }
                }
               


                ada = new SqlDataAdapter(sql1, Conn);
                dt = new DataTable("Accounts");

                ada.Fill(dt);
                dv.Table = dt;

                dgv1.DataSource = dv;
                dgv1.Visible = true;

                dgv1.Columns[0].HeaderText = "Acc.Code";
                dgv1.Columns[1].HeaderText = "Acc. Name";



                ////dv.Table = dt;

                //dgv1.DataSource = dt;
                //dgv1.Visible = true;
                //// dgv1.DataMember = "Accounts";
                //dgv1.Columns[0].HeaderText = "Acc.Code";
                //dgv1.Columns[1].HeaderText = "Acc. Name";


                
                SqlDataAdapter adgrpac = new SqlDataAdapter(sql, Conn);
                DataTable dtgrpac = new DataTable("group_ac");
                adgrpac.Fill(dtgrpac);

                cmbGroupac.DisplayMember = "acc_name";
                cmbGroupac.ValueMember = "acc_no";
                cmbGroupac.DataSource = dtgrpac;
                cmbGroupac.SelectedIndex = -1;
                //if (cmbtype.SelectedValue.ToString() == emp_ac_type.ToString())
                //{
                //    lblempid.Visible = true;
                //    cmbempid.Visible = true;
                //    cmbclass.SelectedIndex = 2;

                //    sql = "select emp_id,fullname from emp_detail order by fullname";
                //    SqlDataAdapter ademp = new SqlDataAdapter(sql, Conn);
                //    DataTable dtemp = new DataTable("emp");
                //    ademp.Fill(dtemp);

                //    cmbempid.DisplayMember = "fullname";
                //    cmbempid.ValueMember = "emp_id";
                //    cmbempid.DataSource = dtemp;

                //}
                
                isini = false;
            }
            catch(Exception ex)
            {

            }

        }
    

        private void populate_headac()
        {
            try
            {

                string crt1 = "1=1";
                string crt2 = "1=1";
               
                if (cmbclass.SelectedIndex < 1)
                {

                }
                else
                {
                    crt1 = "acc_class = " + cmbclass.SelectedValue;

                }

                if (cmblevel.SelectedIndex < 1)
                {

                }
                else
                {

                    crt2 = "acc_level =" + cmblevel.SelectedIndex;
                }


               


               
                switch (txttrn_type.Text)
                {
                    case "200":
                        {
                            if (cmblevel.SelectedIndex == 1)
                            {
                                sql = "select acc_no,acc_name from accounts  where acc_no = " + cmbclass.SelectedValue;

                            }
                            else
                            {
                                sql = "select acc_no,acc_name from accounts  where acc_class = " + cmbclass.SelectedIndex + " and  acc_level <> 5 and   acc_level = " + cmblevel.SelectedIndex + "-1  Union select 0, ' All' order by acc_name ";
                            }

                            //sql = "select acc_no,acc_name from accounts  where acc_class = " + cmbclass.SelectedIndex + " and  acc_level <> 5 and   acc_level < " + cmblevel.SelectedIndex + " order by acc_name ";

                            
                            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                            DataTable dt2 = new DataTable("acc_type");
                            ada2.Fill(dt2);

                            cmbtype.DisplayMember = "acc_name";
                            cmbtype.ValueMember = "acc_no";
                            cmbtype.DataSource = dt2;

                        }
                        break;
                }
            }
            catch(Exception ex)
            {

            }

        }
    private void get_ac_no()
        {
        try
        {


            if (ADOconn.State == 0)
            {
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


            }

            // if (cmbclass.SelectedIndex < 1) return;
          



            ADODB.Recordset tmp = new ADODB.Recordset();
            string crt = "";
            crt = " level" + (Convert.ToInt16(cmblevel.SelectedIndex) - 1).ToString() + "_NO =" + cmbtype.SelectedValue;
       
            tmp.Open("select max(acc_no) from  accounts where acc_level = " + cmblevel.SelectedIndex + " and acc_class=" + cmbclass.SelectedValue + " and " + crt , ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
            while(!tmp.EOF)
            { 
                if(tmp.Fields[0].Value==DBNull.Value)
                {
                    switch(cmblevel.SelectedIndex)
                    {
                        case 2:
                            {
                                break;
                            }
                        case 3:
                            {
                                txtaccno.Text = cmbtype.SelectedValue.ToString() + "11";
                                break;
                            }
                        case 4:
                            {
                                txtaccno.Text = cmbtype.SelectedValue.ToString() + "0001";
                                break;
                            }
                    }

                   
                }
                else
                {
                    txtaccno.Text = (Convert.ToDouble(tmp.Fields[0].Value) + 1).ToString();
                }
                tmp.MoveNext();
            }
             
                }
           
            catch(Exception ex)
            {

            }

        }

        private void cmblevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            isini = false;
            if (cmbclass.SelectedIndex < 0) return;
            populate_headac();
            populate_acc();
        }

      
}
}
