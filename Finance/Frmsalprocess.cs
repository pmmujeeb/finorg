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
using CrystalDecisions.CrystalReports.Engine;

//using Microsoft.VisualBasic;
namespace FinOrg
{


    public partial class Frmsalprocess : Form
    {

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string ac_code;
        string acntrl;
        int start_no;
        int end_no;
        int cur_no;
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        bool issearch;
        object emp_ac_type;
        public Frmsalprocess()
        {
            InitializeComponent();
        }

        private void Frmsalprocess_Load(object sender, EventArgs e)
        {
            
           
            load_leaders();
            isini = true;
            dt1.Value = DateTime.Now;
            populate_empdet();
            isini = false;
        }
       

     


        private void load_leaders()
        {
            Conn.Close();
            Conn.Open();

            try
            {
                isini = true;
                
                isedit = true;
                string sql = "";
                string sql1 = "";
                txtpriv.Text = Gvar.frm_priv;
                sql = "select MOnth_code,Month_name from Month_Names order by Month_code ";

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("MOnth");
                ada3.Fill(dt3);

                cmbmonth.DisplayMember = "Month_name";
                cmbmonth.ValueMember = "MOnth_code";
                cmbmonth.DataSource = dt3;
                //MessageBox.Show(DateTime.Now.Date.Month.ToString());
                cmbmonth.SelectedValue = DateTime.Now.Date.Month;

               

              


                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();
                sql = "SELECT SALARY_AC FROM AC_OPTIONS WHERE  ac_options.ID =1";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                   if (rec.RecordCount != 0)
                   {
                       txtsalacno.Text = rec.Fields[0].Value.ToString();
                   }

                isini = false;
                // dgv1.Columns[2].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter )
            {
                // SendKeys.Send("{Tab}");

                switch (acntrl)
                {

                    case "dgv2":
                    
                    case "dgv1":
                        {
                            return (false);
                        }
                        break;

                }
                bool nextskip = false;
                string btn;
                btn = this.ActiveControl.GetType().ToString();
                if (this.ActiveControl.GetType().ToString() == "System.Windows.Forms.Button") nextskip = true;
                if (!nextskip)
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);

                keyData = Keys.Tab;


                //   SendKeys.Send("{Tab}");
                return true;
                return base.ProcessCmdKey(ref msg, keyData);


                //return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void populate_empdet()
        {
            try
        {
            //Conn.Open();


            load_ini();
            string sql = " SELECT     cast(E.EMP_Id as varchar) EmpID, E.Fullname,  Basic, Transportation,Housing, Other,Overtime,[DEDUCTION],[LOAN_AMT],[REMARKS],[NET_AMOUNT],[EMP_ACNO],EFFECT_DATE,WORKED_DAYS,CONTRACT_ID,PAID_AMOUNT,TRN_NO1,TRN_NO2,ID,EMP_BRANCH FROM  SALARY_DET  as E " +
                 " WHERE  SALARY_MONTH = '" +  cmbmonth.Text + "' order by Fullname";
            //sql = sql + " where  '" + cmbmonth.Text + "'  between DATENAME(mm ,convert(date,start_date,103)) and   DATENAME(mm ,convert(date,start_end,103))";




            SqlDataAdapter ada1 = new SqlDataAdapter(sql, Conn);

              DataTable dt1 = new DataTable("SALARY");
            //dt.AcceptChanges();
            ada1.Fill(dt1);

           

            dv1.Table = dt1;

            dgv2.DataSource = dv1;

            //dgv1.DataSource = dt;
            dgv2.Visible = true;
            dgv2.Columns[0].Visible = false; ;
            dgv2.Columns[6].Visible = false; ;
            dgv2.Columns[1].Width = 200;




             sql = " SELECT     cast(E.EMP_Id as varchar) EmpID, E.Fullname,  C.Basic, C.Transportation, C.Housing, C.Other,a.acc_no,C.CONTRACT_ID,cast( 'True' as bit) as [Select],Branch_Code FROM  EMP_DETAIL AS E INNER JOIN " +
                      "EMP_CONTRACTS AS C ON E.EMP_Id = C.Emp_id inner join accounts as a on a.Id_Number = cast(e.emp_id as varchar) " +
                      " WHERE E.EMP_ID NOT IN (SELECT EMP_ID FROM SALARY_DET WHERE  SALARY_MONTH = '" +  cmbmonth.Text + "') order by Fullname"; 
                //sql = sql + " where  '" + cmbmonth.Text + "'  between DATENAME(mm ,convert(date,start_date,103)) and   DATENAME(mm ,convert(date,start_end,103))";




            ada = new SqlDataAdapter(sql, Conn);
           
            ds = new DataSet();
            
            dt = new DataTable("Accounts");
            //dt.AcceptChanges();
            ada.Fill(dt);
           
            dgv1.Visible = true;

            dv.Table = dt;

            dgv1.DataSource = dv;

            //dgv1.DataSource = dt;
            dgv1.Visible = true;
            dgv1.Columns[0].Visible = false; ;
            dgv1.Columns[6].Visible = false; ;
            dgv1.Columns[1].Width = 200;
            dgv1.Columns[2].Width = 75;
            dgv1.Columns[3].Width = 75;
            dgv1.Columns[4].Width = 75;
            dgv1.Columns[5].Width = 75;
            dgv1.Columns[7].Width = 75;

            dgv2.Columns[2].Width = 75;
            dgv2.Columns[3].Width = 75;
            dgv2.Columns[4].Width = 75;
            dgv2.Columns[5].Width = 75;
            dgv2.Columns[7].Width = 75;
            dgv2.Columns[6].Width = 75;
            dgv2.Columns[8].Width = 75;
            dgv2.Columns[9].Width = 75;
            // dgv1.DataMember = "Accounts";
            //dgv1.Columns[0].HeaderText = "Acc.Code";
           // dgv1.Columns[1].HeaderText = "Acc. Name";
            //dgv1.Columns[2].HeaderText = "Sponsor";
            //dgv1.Columns[3].HeaderText = "Cus. ID ";
            //dgv1.Columns[4].HeaderText = "Mobile";
            //this.Left = 10;

            double tot = 0;
            for (int i = 0; i < dgv2.Rows.Count; i++)
            {
                if(dgv2[10,i].Value!=null )
                tot+= Convert.ToDouble(dgv2[10,i].Value);
                    

                
            }
            txttotal.Text = tot.ToString();

        }
           catch (Exception ex)
            {

            }

        }

        private void Frmsalprocess_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.F1:

                    if (newToolStripButton.Enabled)
                    {
                        //newToolStripButton_Click(null, null);
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
                       // saveToolStripButton_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F6:

                    if (SearchToolStripButton.Enabled)
                    {
                        //SearchToolStripButton_Click(null, null);
                    }
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;

                    break;
                case Keys.F8:

                    if (toolclose.Enabled)
                    {
                        //toolclose_Click(null, null);
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
                  

                    //e.Handled = true;

                    break;

            }
        }

        private void cmbmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isini == true) return;
                string dtnow = dt1.Value.Year + "/" + cmbmonth.SelectedValue + "/" + dt1.Value.Day;

                dt1.Value = Convert.ToDateTime(dtnow);
            }
            catch(Exception ex)
            {
                string dtnow =  "28/" + cmbmonth.SelectedValue + "/" + dt1.Value.Year;

                dt1.Value = Convert.ToDateTime(dtnow);

            }
            populate_empdet();
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtempacno.Text = dgv1[6, e.RowIndex].Value.ToString();
                txtempid.Text = dgv1[0, e.RowIndex].Value.ToString();
                txtactrnno.Text = "";
                txtactrnno.Text = "0";
                lblname.Text = dgv1[1, e.RowIndex].Value.ToString();
                txtbasic.Text = dgv1[2, e.RowIndex].Value.ToString();
                txthousing.Text = dgv1[3, e.RowIndex].Value.ToString();
                txttransport.Text = dgv1[4, e.RowIndex].Value.ToString();
                txtbasic1.Text = dgv1[2, e.RowIndex].Value.ToString();
                txthousing1.Text = dgv1[3, e.RowIndex].Value.ToString();
                txttransport1.Text = dgv1[4, e.RowIndex].Value.ToString();
                txtother.Text = dgv1[5, e.RowIndex].Value.ToString();
                txtother1.Text = dgv1[5, e.RowIndex].Value.ToString();
                txtcontractno.Text = dgv1[7, e.RowIndex].Value.ToString();
                txtbrncode.Text = dgv1[9, e.RowIndex].Value.ToString();
                txtworkeddays.Text = "30";
                txttrnno1.Text = "";
                txttrnno2.Text = "";
                txtpaidamt.Text = "0";
                txtid.Text = "0";

                calc_sal();
                tooldelete.Visible = false;

            }
            catch(Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Trim();
            if (txt != "")
            {
                dv.RowFilter = "Empid LIKE  '%" + txt + "%' OR Fullname LIKE '%" + txt + "%'";
            }
            else
                dv.RowFilter = "Empid <> '0'";
        }

        private void dgv1_Enter(object sender, EventArgs e)
        {
            acntrl = "dgv1";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            acntrl = "dgv1";
        }

        private void dgv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtempacno.Text = dgv2[11, e.RowIndex].Value.ToString();
                txtempid.Text = dgv2[0, e.RowIndex].Value.ToString();
                txtactrnno.Text = dgv2[16, e.RowIndex].Value.ToString();
                lblname.Text = dgv2[1, e.RowIndex].Value.ToString();
                txtbasic.Text = dgv2[2, e.RowIndex].Value.ToString();
                txthousing.Text = dgv2[3, e.RowIndex].Value.ToString();
                txttransport.Text = dgv2[4, e.RowIndex].Value.ToString();
                txtother.Text = dgv2[5, e.RowIndex].Value.ToString();
                txtovertime.Text = dgv2[6, e.RowIndex].Value.ToString();
                txtdeduction.Text = dgv2[7, e.RowIndex].Value.ToString();
                txtloan.Text = dgv2[8, e.RowIndex].Value.ToString();
                txtremarks.Text = dgv2[9, e.RowIndex].Value.ToString();
                txtnet.Text = dgv2[10, e.RowIndex].Value.ToString();
                txtworkeddays.Text = dgv2[13, e.RowIndex].Value.ToString();
                txtcontractno.Text = dgv2[14, e.RowIndex].Value.ToString();
                txtpaidamt.Text = dgv2[15, e.RowIndex].Value.ToString();
                txttrnno1.Text = dgv2[16, e.RowIndex].Value.ToString();
                txttrnno2.Text = dgv2[17, e.RowIndex].Value.ToString();
                txtid.Text = dgv2[18, e.RowIndex].Value.ToString();
                txtbrncode.Text = dgv2[19, e.RowIndex].Value.ToString();
                tooldelete.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }

        private void dgv2_Enter(object sender, EventArgs e)
        {
             acntrl = "dgv2";
        }

        private void dgv2_Leave(object sender, EventArgs e)
        {
            acntrl = "";
        }

        private void dgv1_Leave(object sender, EventArgs e)
        {
            acntrl = "";
        }

        private void find_total()
        {
            try
            {
                if(isini==true) return;
                decimal tot = 0;
                if (txtbasic.Text != null && txtbasic.Text != "") tot += Convert.ToDecimal(txtbasic.Text);
                if (txthousing.Text != null && txthousing.Text != "") tot += Convert.ToDecimal(txthousing.Text);
                if (txttransport.Text != null && txttransport.Text != "") tot += Convert.ToDecimal(txttransport.Text);
                if (txtother.Text != null && txtother.Text != "") tot += Convert.ToDecimal(txtother.Text);
                if (txtdeduction.Text != null && txtdeduction.Text != "") tot -= Convert.ToDecimal(txtdeduction.Text);
                if (txtloan.Text != null && txtloan.Text != "") tot -= Convert.ToDecimal(txtloan.Text);
                if (txtovertime.Text != null && txtovertime.Text != "") tot += Convert.ToDecimal(txtovertime.Text);
                txtnet.Text = tot.ToString();

            }
            catch(Exception ex)
            {

            }


        }

        private void txtbasic_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txthousing_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txttransport_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txtovertime_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txtother_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txtdeduction_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void txtloan_TextChanged(object sender, EventArgs e)
        {
            find_total();
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            cmbmonth.SelectedValue = dt1.Value.Date.Month;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
       
            ADODB.Recordset tmp = new ADODB.Recordset();
            try
            {


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

                





                //if (isempty)
                //{
                //    MessageBox.Show("Entry Not Completed, Please fill all Yellow Marked fileds!!", "Invalid Entry");
                //    return;

                //}



                try
                {
                //    ADOconn.BeginTrans();
                //    if (!isedit)
                //    {
                //        if (txtismanual.Text == "0")
                //        {
                //            gen_accno();

                //            tmp = new ADODB.Recordset();

                //            sql = "update ACC_TYPE SET CUR_NO = CUR_NO+1 WHERE  ACC_TYPE_CODE=" + cmbtype.SelectedValue;
                //            tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                //        }
                //    }

                    if (txtsalacno.Text == "")
                    {
                        MessageBox.Show("Invalid Salary Account Number, Please Enter a Valid Number", "Invalid Entry");
                        return;


                    }

                    if (cmbmonth.SelectedIndex<0)
                    {
                        MessageBox.Show("Invalid Salary Month, Please Select a Valid Month", "Invalid Entry");
                        return;


                    }



                    if (txtnet.Text.Trim() == "") txtnet.Text = "0";
                //    //if (txtvalue.Text.Trim() == "") txtvalue.Text = "0";

                    isini = true;
                if (txtbasic.Text == "") txtbasic.Text = "0";
                if (txtempid.Text == "") txtempid.Text = "0";
                if (txthousing.Text == "") txthousing.Text = "0";
                if (txttransport.Text == "") txttransport.Text = "0";
                if (txtother.Text == "") txtother.Text = "0";
                if (txtdeduction.Text == "") txtdeduction.Text = "0";
                if (txtloan.Text == "") txtloan.Text = "0";
                if (txtcontractno.Text == "") txtcontractno.Text = "0";
                if (txtworkeddays.Text == "") txtworkeddays.Text = "0";
                if (txtnet.Text == "") txtnet.Text = "0";
                if (txtsalacno.Text == "") txtsalacno.Text = "0";
                if (txtempid.Text == "") txtempid.Text = "0";
                if (txtpaidamt.Text == "") txtpaidamt.Text = "0";
                if (txtbrncode.Text == "") txtbrncode.Text = Gvar.brn_code.ToString();
                if (txtcontractno.Text=="0")
                {
                    MessageBox.Show("Invalid Contract Numebr, Please check and Try Again", "Invalid Entry");
                    return;


                }
                if (Convert.ToDecimal(txtpaidamt.Text) > Convert.ToDecimal(txtnet.Text))
                {
                    MessageBox.Show("There is already a higher Paid amount found, Please check and Try Again", "Invalid Entry");
                    return;


                }  
                    isini = false;
                find_total();

                //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());


                ADOconn.BeginTrans();


                int TRN_BY = 13;
                sql = "select * from TRN_accounts where trn_NO =  '" + txtactrnno.Text.Trim() + "' AND TRN_BY = 13 and Sno = 1";
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
                trnno = Convert.ToInt64(rec.Fields["trn_no"].Value);
                double amt = Convert.ToDouble(txtnet.Text);

                long doc_no = -1 * trnno;
                string DR_CR = "D";
                string DR_CR1 = "C";

                rec.Fields["acc_no"].Value = txtempacno.Text;
                rec.Fields["EntrY_no"].Value = 0;
                double rate = 1;
                rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                rec.Fields["F_RATE"].Value = 1;
                rec.Fields["TRN_BY"].Value = TRN_BY;
                rec.Fields["DR_CR"].Value = DR_CR;
                rec.Fields["user_ID"].Value = Gvar.Userid;
                rec.Fields["PAYBY"].Value = txtsalacno.Text;
                //rec.Fields["RQTY"].Value = 0;
                rec.Fields["SNO"].Value = 1;
                // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                rec.Fields["NARRATION"].Value = "Salary Process of " +cmbmonth.Text ;
                rec.Fields["Voucher_No"].Value = 0;
                rec.Fields["cost_code"].Value = 0;
                rec.Fields["dept_code"].Value = 0;
                rec.Fields["pay_date"].Value = dt1.Value;
                rec.Fields["doc_no"].Value = doc_no;
                rec.Fields["NYEAR"].Value = dt1.Value.Year;
                rec.Fields["brn_code"].Value = Gvar.brn_code;
                rec.Fields["currency"].Value = "SR";
                rec.Fields["brn_code"].Value = txtbrncode.Text;
                rec.Fields["trn_type"].Value = TRN_BY;

                rec.Update();



                sql = "select * from TRN_accounts where trn_NO2 =  '" + txtactrnno.Text.Trim() + "' AND TRN_BY = 13 and Sno = 2";
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
                        trnno2 = 1;
                    }
                    else
                    {
                        trnno2 = Convert.ToInt64(tmp.Fields[0].Value.ToString());
                       
                    }
                    rec.Fields["trn_no"].Value = trnno2;
                    rec.Fields["trn_no2"].Value = trnno ;
                }


                trnno2 = Convert.ToInt64(rec.Fields["trn_no"].Value);
                rec.Fields["acc_no"].Value = txtsalacno.Text;
                rec.Fields["EntrY_no"].Value = 0;
              
                rec.Fields["PAY_AMOUNT"].Value = Math.Abs(amt * rate);
                rec.Fields["F_PAY_AMOUNT"].Value = Math.Abs(amt);
                rec.Fields["F_RATE"].Value = 1;
                rec.Fields["TRN_BY"].Value = TRN_BY;
                rec.Fields["DR_CR"].Value = DR_CR1;
                rec.Fields["user_ID"].Value = Gvar.Userid;
                rec.Fields["PAYBY"].Value = txtempacno.Text;
                //rec.Fields["RQTY"].Value = 0;
                rec.Fields["SNO"].Value = 2;
                // rec.Fields["FRACTION"].Value = dgv1["fraction", i].Value;
                rec.Fields["NARRATION"].Value = "Salary Process of " + cmbmonth.Text + " for " +  lblname.Text; 
                rec.Fields["Voucher_No"].Value = 0;
                rec.Fields["cost_code"].Value = 0;
                rec.Fields["dept_code"].Value = 0;
                rec.Fields["pay_date"].Value = dt1.Value;
                rec.Fields["doc_no"].Value = doc_no;
                rec.Fields["NYEAR"].Value = dt1.Value.Year;
                rec.Fields["brn_code"].Value = txtbrncode.Text;
                rec.Fields["trn_type"].Value = TRN_BY;
                rec.Fields["currency"].Value = "SR";

                rec.Update();

                rec = new ADODB.Recordset();
                sql = "SELECT * FROM salary_det  where salary_month = '" + cmbmonth.Text + "' and  emp_id =" + txtempid.Text ;
                //    sql = "SELECT * FROM Accounts where Acc_no ='" + txtaccno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (rec.RecordCount == 0)
                {
                    rec.AddNew();



                }

                rec.Fields["EMP_ID"].Value = txtempid.Text.Trim();
                rec.Fields["SALARY_MONTH"].Value = cmbmonth.Text.Trim();
                rec.Fields["EFFECT_DATE"].Value = dt1.Value.Date.ToString("yyyy-MM-dd");
                rec.Fields["FULLNAME"].Value = lblname.Text;
                rec.Fields["BASIC"].Value = txtbasic.Text;

                rec.Fields["HOUSING"].Value = txthousing.Text.Trim();

                rec.Fields["TRANSPORTATION"].Value = txttransport.Text.Trim();
                rec.Fields["OTHER"].Value = txtother.Text.Trim();
                rec.Fields["DEDUCTION"].Value = txtdeduction.Text.Trim();

                rec.Fields["LOAN_AMT"].Value = txtloan.Text.Trim();
                rec.Fields["REMARKS"].Value = txtremarks.Text.Trim();
                rec.Fields["NET_AMOUNT"].Value = txtnet.Text.Trim();
                rec.Fields["SAL_ACNO"].Value = txtsalacno.Text.Trim();
                rec.Fields["EMP_ACNO"].Value = txtempacno.Text.Trim();
                rec.Fields["worked_days"].Value = txtworkeddays.Text.Trim();
                rec.Fields["CONTRACT_ID"].Value = txtcontractno.Text.Trim();
                rec.Fields["NYEAR"].Value =dt1.Value.Year.ToString();
                rec.Fields["TRN_NO1"].Value = trnno;
                rec.Fields["TRN_NO2"].Value = trnno2;
                rec.Fields["EMP_BraNCH"].Value = txtbrncode.Text.Trim();
              



                rec.Update();

                


                    ADOconn.CommitTrans();
                    populate_empdet();

                    //DataGridViewRow row = dgv1.CurrentRow;

                    //if (txtempid.Text.ToString() == dgv1[1, dgv1.CurrentRow.Index].Value.ToString())
                    //{
                    //    dgv1.Rows.Remove(row);
                    //}
                    isedit = true;
                    MessageBox.Show("Successfully Saved");
                    
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

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            load_ini();

        }
        private void load_ini()
        {
            txtbasic.Text = "";
            txtempid.Text = "";
            txthousing.Text = "";
            txttransport.Text = "";
            txtother.Text = "";
            txtdeduction.Text = "";
            txtloan.Text = "";
            txtactrnno.Text = "";
            txtempacno.Text = "";
            txtnet.Text = "";
            txttotal.Text = "";
            txtbasic1.Text = "";
            txthousing1.Text = "";
            txttransport1.Text = "";
            txtother1.Text = "";
            txtworkeddays.Text = "30";
            txtcontractno.Text = "";
            txtpaidamt.Text = "";
            txttrnno1.Text = "";
            txttrnno2.Text = "";
            txtid.Text = "0";
            txtbrncode.Text = "";
        }
        private void chkselect_CheckedChanged(object sender, EventArgs e)
        {
            for(int i=0;i<dgv1.Rows.Count;i++)
            {
                dgv1[7, i].Value = chkselect.Checked;
            }
        }

        private void print_reciept()
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


                if (cmbmonth.SelectedIndex<0) return;

                ReportDocument CrRep = new ReportDocument();

                    rep_path = Application.StartupPath + "\\reports\\Rptsalary.rpt";


                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{SALARY_DET.SALARY_MONTH} = '" + cmbmonth.Text + "' and {SALARY_DET.NYEAR}='" + dt1.Value.Date.Year + "'";


                if (crt != "") CrRep.RecordSelectionFormula = crt;

                //CrRep.VerifyDatabase = false;
                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
                //crconnectioninfo.ServerName = "SqlZoomERP";
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

                CrRep.SummaryInfo.ReportTitle = "Salary Report for Month " +  cmbmonth.Text ;
                    //CrRep.DataDefinition.FormulaFields["shipaddress"].Text = "'" + cmbaddress.Text + "'";
               
                // CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);
                CrRep.ReadRecords();
                //CrRep.Load();
                // CrRep.ReadRecords();

                CrRep.Refresh();

                //if (chkprinter.Checked)
                //{


                //    CrRep.PrintToPrinter(1, true, 0, 0);
                //}
                //else
                //{

                if (chkprintview.Checked)
                {
                    FrmrepView frm = new FrmrepView();
                    frm.MdiParent = this.ParentForm;

                    frm.crv1.ReportSource = CrRep;
                    frm.Show();
                    return;
                }



                CrRep.PrintToPrinter(1, true, 0, 0);
                return;
                //}


            }
            catch (System.Exception excep)
            {

                MessageBox.Show(excep.Message);

            }


        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            print_reciept();
        }

        private void txtworkeddays_TextChanged(object sender, EventArgs e)
        {
           
            calc_sal();
        }
        private void calc_sal()
        {
            try
            {
               
                if (txtbasic.Text == "") txtbasic.Text = "0";
                if (txtbasic1.Text == "") txtbasic1.Text = "0";
                if (txtother.Text == "") txtother.Text = "0";
                if (txtother1.Text == "") txtother1.Text = "0";
                if (txthousing.Text == "") txthousing.Text = "0";
                if (txthousing1.Text == "") txthousing1.Text = "0";
                if (txttransport.Text == "") txttransport.Text = "0";
                if (txttransport1.Text == "") txttransport1.Text = "0";
                if (txtworkeddays.Text == "") txtworkeddays.Text = "0";


                txtbasic.Text = Math.Round(Convert.ToDecimal(txtbasic1.Text) / 30 * Convert.ToDecimal(txtworkeddays.Text), 0).ToString();
                txthousing.Text = Math.Round(Convert.ToDecimal(txthousing1.Text) / 30 * Convert.ToDecimal(txtworkeddays.Text), 0).ToString();
                txttransport.Text = Math.Round(Convert.ToDecimal(txttransport1.Text) / 30 * Convert.ToDecimal(txtworkeddays.Text), 0).ToString();
                txtother.Text = Math.Round(Convert.ToDecimal(txtother1.Text) / 30 * Convert.ToDecimal(txtworkeddays.Text), 0).ToString();


            }
            catch (Exception)
            {
                
                throw;
            }

        }

        private void tooldelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpaidamt.Text == "") txtpaidamt.Text = "0";
                if(Convert.ToDecimal(txtpaidamt.Text)>0)
                {
                    MessageBox.Show("Cannot delete a Entry with a Paid Amount", "Invalid Deletion");
                      return;
                }


                if (Convert.ToDecimal(txtid.Text) < 1)
                {
                    MessageBox.Show("Invalid Entry to Delete", "Invalid Deletion");
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to Delete This Entry?", "Delete Entry", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                sql = "delete from  Salary_det where id = " + txtid.Text ;

                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                sql = "delete from  trn_accounts  where trn_no = " + txttrnno1.Text + " or trn_no=" + txttrnno2.Text;

                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Operation Succeeded Successfully", "Success Deletion");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }
    

    }
}
