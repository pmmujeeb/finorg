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


    public partial class FrmBankrecons : Form
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
        public FrmBankrecons()
        {
            InitializeComponent();
        }

        private void Frmsalprocess_Load(object sender, EventArgs e)
        {
            
           
            load_leaders();
            isini = true;
            dt1.Value = DateTime.Now;
            
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
                sql = "select Acc_no,Acc_name from Accounts inner join ac_options on acc_type_code=CASH_AC_TYPE and   ac_options.ID =1 and  acc_no <> DEF_CASH_AC order by Acc_Name ";

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("bank");
                ada3.Fill(dt3);

                cmbbank.DisplayMember = "Acc_name";
                cmbbank.ValueMember = "Acc_no";
                cmbbank.DataSource = dt3;



                sql = "select Description from Reconcil_remarks order by description ";


                ada = new SqlDataAdapter(sql, Conn);

                ds = new DataSet();

                dt = new DataTable("Accounts");
                //dt.AcceptChanges();
                ada.Fill(dt);

                

                dv1.Table = dt;

                dgvbanklookup.DataSource = dv;
                dgvbooklookup.DataSource = dv;

                load_ini();

                //dgvbanklookup.Columns[0].Width = 200;

                //sql = "select area_code,area_name from area_master ";

                




              


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

        private void populate_bankdet()
        {
            try
        {
            //Conn.Open();


           // dgv1.Rows.Clear();

            if (ADOconn.State == 0)
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

            ADODB.Recordset rec = new ADODB.Recordset();
                double camt =0;
                double damt = 0;
                double amt =0;

            string sql = " SELECT   sum(case when dr_cr='D' THEN PAY_AMOUNT ELSE 0 END),  SUM(case when dr_cr='C' THEN PAY_AMOUNT ELSE 0 END) " +

                   " FROM TRN_ACCOUNTS WHERE  ACC_NO  = " + cmbbank.SelectedValue + " and pay_date <'" + dt1.Value.Date.ToString("yyyy-MM-dd") + "'" ;
            rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                while (!rec.EOF)
           
            {
                if (rec.Fields[0].Value != DBNull.Value )  damt = Convert.ToDouble(rec.Fields[0].Value);
                 if (rec.Fields[1].Value != DBNull.Value )  camt = Convert.ToDouble(rec.Fields[1].Value);

                amt = damt-camt;
                rec.MoveNext();

            }


            sql = " SELECT  '0' AS Trn_No, '" + dt1.Value.Date.ToString("dd/MM/yyyy") + "' AS PAY_DATE, case when " + amt + " > 0 THEN 'D' ELSE 'C' END AS CR_DR, case when " + amt + " < 0 THEN " + amt + " else 0 END as CREDIT, case when " + amt + " >= 0 THEN " + amt + " else 0 END as DEBIT, 'Op. Balance' as NARRATION  " +
             " UNION ALL SELECT     cast(TRN_NO as varchar) Trn_No, Pay_Date,  DR_CR,case when DR_CR='D' THEN PAY_AMOUNT END DEBIT ,case when DR_CR='C' THEN PAY_AMOUNT END  CREDIT, NARRATION  " +
                 " FROM TRN_ACCOUNTS WHERE  ACC_NO  = " +  cmbbank.SelectedValue + " AND PAY_DATE BETWEEN '" + dt1.Value.Date.ToString("yyyy-MM-dd") + "' AND '" + dt2.Value.Date.ToString("yyyy-MM-dd") + "' order by pay_date,trn_no";
            //sql = sql + " where  '" + cmbmonth.Text + "'  between DATENAME(mm ,convert(date,start_date,103)) and   DATENAME(mm ,convert(date,start_end,103))";



            dgv1.Visible = true;
           

            ada = new SqlDataAdapter(sql, Conn);
           
            ds = new DataSet();
            
            dt = new DataTable("Accounts");
            //dt.AcceptChanges();
            ada.Fill(dt);
           
            dgv1.Visible = true;

            dv.Table = dt;

            dgv1.DataSource = dv;
            object sumdamt;
            sumdamt = dt.Compute("Sum(debit)", "");

            txtdtotal.Text = sumdamt.ToString();
            object sumcamt;
            sumcamt = dt.Compute("Sum(credit)", "");
            double t = 0;
            t =Convert.ToDouble( sumdamt) - Convert.ToDouble( sumcamt);
            txtctotal.Text = sumcamt.ToString();
            txtnet.Text = t.ToString();
            //dgv1.DataSource = dt;
            dgv1.Visible = true;
            dgv1.Columns[0].Visible = false; ;
            //dgv1.Columns[6].Visible = false; ; 
            dgv1.Columns[2].Width = 75;
            dgv1.Columns[3].Width = 75;
            dgv1.Columns[5].Width = 300;
          

            //double tot = 0;
            //for (int i = 0; i < dgvbank.Rows.Count; i++)
            //{
            //    if(dgvbank[10,i].Value!=null )
            //    tot+= Convert.ToDouble(dgvbank[10,i].Value);
                    

                
            //}
            //.Text = tot.ToString();

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

       
        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
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
               
                //if (txtovertime.Text != null && txtovertime.Text != "") tot += Convert.ToDecimal(txtovertime.Text);
                txtnet.Text = tot.ToString();

            }
            catch(Exception ex)
            {

            }


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

                





                if (isempty)
                {
                    MessageBox.Show("Entry Not Completed, Please fill all Yellow Marked fileds!!", "Invalid Entry");
                    return;

                }



                try
                {
                //    ADOconn.BeginTrans();
               

                    

                    //if (cmbmonth.SelectedIndex<0)
                    //{
                    //    MessageBox.Show("Invalid Salary Month, Please Select a Valid Month", "Invalid Entry");
                    //    return;


                    //}



                ADOconn.BeginTrans();

                   
int i=0;

                string entry_no = dtentry.Value.Date.ToString("yyyyMMdd");

                if (isedit)
                {
                sql = "delete from reconcil_det  where Reconcil_No =  '" + entry_no + "'";
                    object a;

                    ADOconn.Execute(sql,out a);
                }
                     sql = "select * from reconcil_det  where Reconcil_No =  '" + entry_no + "'";
                rec = new ADODB.Recordset();
                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               
                if (rec.RecordCount == 0)
                {
                   // rec.AddNew();
                
                 }
                else
                {
                    MessageBox.Show("Entry Already Exist , Cannot enter as a New entry", "Invalid Entry");
                       return;

                }
                for (i = 0; i < dgv1.Rows.Count; i++)
                {
                    if(dgv1[0,i].Value!=null)
                    {
                        rec.AddNew();
                    if (dgv1[3, i].Value == null) dgv1[3, i].Value = 0;
                    if (dgv1[4, i].Value == null) dgv1[4, i].Value = 0;
                    double amt = Convert.ToDouble(dgv1[3, i].Value) + Convert.ToDouble(dgv1[4, i].Value);

                    rec.Fields["Reconcil_No"].Value = entry_no;
                    rec.Fields["Reconcil_Date"].Value = dtentry.Value.Date;

                    rec.Fields["Reconcil_user"].Value = Gvar.username;
                    rec.Fields["Amount"].Value = Math.Abs(amt);
                    rec.Fields["Bank_No"].Value = cmbbank.SelectedValue;
                    
                    rec.Fields["DR_CR"].Value = dgv1[2, i].Value;

                    rec.Fields["plus_minus"].Value = "";
                    rec.Fields["pay_date"].Value = dgv1[0, i].Value;
                    rec.Fields["SNO"].Value = i+1;
                    rec.Fields["trn_type"].Value = "A";
                    rec.Fields["NARRATION"].Value = dgv1[5, i].Value;


                    rec.Update();
                    }
                }

                    for (i = 0; i < dgvbank.Rows.Count; i++)
                    {
                        if (dgvbank[2,i].Value != null)
                        {
                            rec.AddNew();
                            if (dgvbank[0, i].Value == null) dgvbank[0, i].Value = "";
                            if (dgvbank[1, i].Value == null) dgvbank[1, i].Value = 0;
                            double amt = Convert.ToDouble(dgvbank[2, i].Value); 

                            rec.Fields["Reconcil_No"].Value = entry_no;
                            rec.Fields["Reconcil_Date"].Value = dtentry.Value.Date;

                            rec.Fields["Reconcil_user"].Value = Gvar.username;
                            rec.Fields["Amount"].Value = Math.Abs(amt);
                            rec.Fields["Bank_No"].Value = cmbbank.SelectedValue;

                            if (dgvbank[1, i].Value == "+")

                            rec.Fields["DR_CR"].Value = "D";
                            else
                                rec.Fields["DR_CR"].Value = "C";


                            rec.Fields["plus_minus"].Value = dgvbank[1, i].Value;
                            rec.Fields["pay_date"].Value ="";
                            rec.Fields["SNO"].Value = i+1;
                            rec.Fields["trn_type"].Value = "B";
                            rec.Fields["NARRATION"].Value = dgvbank[0, i].Value;


                            rec.Update();
                        }
                    }
                    for (i = 0; i < dgvbook.Rows.Count; i++)
                    {
                        if (dgvbook[2, i].Value != null)
                        {
                            rec.AddNew();
                            if (dgvbook[0, i].Value == null) dgvbook[0, i].Value = "";
                            if (dgvbook[1, i].Value == null) dgvbook[1, i].Value = 0;
                            double amt = Convert.ToDouble(dgvbook[2, i].Value);

                            rec.Fields["Reconcil_No"].Value = entry_no;
                            rec.Fields["Reconcil_Date"].Value = dtentry.Value.Date;

                            rec.Fields["Reconcil_user"].Value = Gvar.username;
                            rec.Fields["Amount"].Value = Math.Abs(amt);
                            rec.Fields["Bank_No"].Value = cmbbank.SelectedValue;

                            if (dgvbook[1, i].Value == "+")

                                rec.Fields["DR_CR"].Value = "D";
                            else
                                rec.Fields["DR_CR"].Value = "C";


                            rec.Fields["plus_minus"].Value = dgvbook[1, i].Value;
                            rec.Fields["pay_date"].Value = "";
                            rec.Fields["SNO"].Value = i+1;
                            rec.Fields["trn_type"].Value = "C";
                            rec.Fields["NARRATION"].Value = dgvbook[0, i].Value;


                            rec.Update();
                        }
                    }

               

              


                    ADOconn.CommitTrans();
                

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

            sql = "select Reconcil_No from Reconcil_det  order by Reconcil_No desc ";

            SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
            DataTable dt3 = new DataTable("entry");
            ada3.Fill(dt3);

            cmbentry.DisplayMember = "Reconcil_No";
            cmbentry.ValueMember = "Reconcil_No";
            cmbentry.DataSource = dt3;

            txtBankCamt.Text = "";
            txtBankdamt.Text = "";
            txtBanknet.Text = "";
            txtbasic.Text = "";
            txtBookcamt.Text = "";
            txtBookdamt.Text = "";
            txtBooknetamt.Text = "";
            txtctotal.Text = "";
            txtdtotal.Text = "";
            txtfifamt.Text = "";
            txtnet.Text = "";
            cmbentry.Text = "";
            dgv1.Rows.Clear();
            dgvbank.Rows.Clear();
            dgvbook.Rows.Clear();

            txtBankCamt.Text = "";



        }
       

        private void print_reciept()
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {


               

                ReportDocument CrRep = new ReportDocument();

                    rep_path = Application.StartupPath + "\\reports\\Rptsalary.rpt";


                CrRep.Load(rep_path);

                //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;


                crt = "{SALARY_DET.SALARY_MONTH} = '" + 0 + "'";


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

                CrRep.SummaryInfo.ReportTitle = "Salary Report for Month "  ;
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

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            populate_bankdet();
        }

        private void dgvbank_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
                find_total_bank();
        }

    private void find_total_bank()
        
    {
        try
        {
            double tot = 0;
            double damt = 0;
            double camt = 0;
            double amt = 0;
            for (int i = 0; i < dgvbank.Rows.Count; i++)
            {
               
                if (dgvbank[2, i].Value != null)
                {
                    if (dgvbank[1, i].Value == null) dgvbank[1, i].Value = "+";
                    tot += Convert.ToDouble(dgvbank[2, i].Value);
                    if (dgvbank[1, i].Value.ToString() =="-")
                    {
                        camt+= Convert.ToDouble(dgvbank[2, i].Value);

                    }
                    else
                    {
                        dgvbank[1, i].Value = "+";
                        damt+= Convert.ToDouble(dgvbank[2, i].Value);
                    }
                
                }

            }

            txtBankCamt.Text = camt.ToString();
            txtBankdamt.Text = damt.ToString();
            amt = damt - camt;
               txtBanknet.Text = amt.ToString();
               find_diff();


        }
        catch
        {

        }
        }


        private void find_diff()
    {
            try
            {
                if (txtbasic.Text == "") txtbasic.Text = "0";
                if (txtnet.Text == "") txtnet.Text = "0";

                if (txtBooknetamt.Text == "") txtBooknetamt.Text = "0";
                if (txtBanknet.Text == "") txtBanknet.Text = "0";


                txtfifamt.Text = ((Convert.ToDouble(txtbasic.Text) + Convert.ToDouble(txtBanknet.Text)) - (Convert.ToDouble(txtnet.Text) + Convert.ToDouble(txtBooknetamt.Text))).ToString();
         
            }
            catch
            {

            }
    }

    private void find_total_book()
    {
        try
        {
            double tot = 0;
            double damt = 0;
            double camt = 0;
            double amt = 0;
            dgvbook.EndEdit();
            for (int i = 0; i < dgvbook.Rows.Count; i++)
            {
                if (dgvbook[2, i].Value != null)
                {
                    if (dgvbook[1, i].Value == null) dgvbook[1, i].Value = "+";
                    tot += Convert.ToDouble(dgvbook[2, i].Value);
                    if (dgvbook[1, i].Value.ToString() == "-")
                    {
                        camt += Convert.ToDouble(dgvbook[2, i].Value);

                    }
                    else
                    {
                        dgvbook[1, i].Value = "+";
                        damt += Convert.ToDouble(dgvbook[2, i].Value);
                    }

                }

            }


            txtBookcamt.Text = camt.ToString();
            txtBookdamt.Text = damt.ToString();
            amt = damt - camt;
            if (txtbasic.Text == "") txtbasic.Text = "0";
            if (txtnet.Text == "") txtnet.Text = "0";
            amt = damt - camt;

            txtBooknetamt.Text = amt.ToString();
            find_diff();

        }
        catch
        {

        }
    }

    private void dgvbook_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        find_total_book();
    }

  

    private void txtnet_TextChanged(object sender, EventArgs e)
    {
        find_diff();
    }

    private void txtbasic_Validated(object sender, EventArgs e)
    {
        find_diff();
    }
    }
}
