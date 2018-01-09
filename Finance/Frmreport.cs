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
using Microsoft.VisualBasic;//
using CrystalDecisions.CrystalReports.Engine;

namespace FinOrg
{


    public partial class frmReport : FinOrgForm
    {

        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        ADODB.Connection ADOconn = new ADODB.Connection();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataView dv2 = new DataView();
        DataView dv3 = new DataView();

        string sql;
        string crt1;
        string CRT2;
        string CRT3;
        bool fnd;
        string RPTHEAD;
        string rep_path;
        string rep_formula;
        bool isini = true;

        public frmReport()
        {
            InitializeComponent();
            ini_tab();
            SqlDataAdapter ada2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            try
            {
                Conn.Close();
                Conn.Open();




                string sql;
                ADODB.Recordset rec = new ADODB.Recordset();
                ADODB.Recordset tmp = new ADODB.Recordset();
                rec = new ADODB.Recordset();
                tmp = new ADODB.Recordset();


                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();
                rec = new ADODB.Recordset();
                int emptype = 0;




                if (Gvar._Gind == 1 || Gvar._Gind == 2)
                {
                    //      sql = "select acc_type_code,acc_type_name from acc_type UNION sELECT   -1,'All' froM acc_type order by acc_type_code ";
                    //label5.Text = "Account Type";
                    // ada2 = new SqlDataAdapter(sql, Conn);
                    // dt2 = new DataTable("acc_type");
                    //ada2.Fill(dt2);

                    //cmbowner.DisplayMember = "acc_type_name";
                    //cmbowner.ValueMember = "acc_type_code";
                    //cmbowner.DataSource = dt2;
                    //cmbowner.SelectedIndex = 0;
                    //if (Gvar._Gind == 2)
                    if (Gvar.rptidx == 101 || Gvar.rptidx == 102 || Gvar.rptidx == 103 || Gvar.rptidx == 104)
                    {
                        label5.Visible = false;
                        cmbowner.Visible = false;
                    }

                    if (Gvar.rptidx == 101 || Gvar.rptidx == 102)
                    {
                        lbllst1.Text = "Customer";
                    }
                    if (Gvar.rptidx == 103 || Gvar.rptidx == 104)
                    {
                        lbllst1.Text = "Supplier";
                    }

                    //if (Gvar.rptidx == 101 ||Gvar.rptidx == 102)    label5.Text = "Customer Cat.";
                    //if (Gvar.rptidx == 101 ||Gvar.rptidx == 102)    label5.Text = "Customer Cat.";
                    //         chkcurrency.Visible = true;

                    lblsaleagent.Visible = false;
                    cmbsaleagent.Visible = false;
                    lbllocation.Visible = false;
                    cmblocaltion.Visible = false;


                    //if (Gvar._Gind == 2)
                    if (Gvar.rptidx == 105 || Gvar.rptidx == 106)
                    {
                        label5.Visible = true;
                        cmbowner.Visible = true;
                        label5.Text = "GL Class";
                        lbldept.Visible = true;
                        cmbdept.Visible = true;
                        lbllst1.Text = "GL Accounts";
                        chkinvoice.Visible = false;

                        lbllevel.Visible = true;
                        cmblevel.Visible = true;

                        lblacheader.Visible = true;
                        cmbacheader.Visible = true;
                        chkcurrency.Visible = false;

                        sql = "select acc_CLASS_code,acc_class_name from acc_class UNION sELECT   -1,'All'  order by acc_CLASS_code ";

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("acc_class");
                        ada2.Fill(dt2);

                        cmbowner.DisplayMember = "acc_class_name";
                        cmbowner.ValueMember = "acc_CLASS_code";
                        cmbowner.DataSource = dt2;
                        cmbowner.SelectedIndex = 0;
                        cmblevel.SelectedIndex = 0;


                    }

                }




                sql = "select Dept_code,dept_name from dept_master UNION sELECT   -1,'All' froM dept_master order by Dept_code ";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("dept_master");
                ada2.Fill(dt2);

                cmbdept.DisplayMember = "dept_name";
                cmbdept.ValueMember = "Dept_code";
                cmbdept.DataSource = dt2;
                cmbdept.SelectedIndex = 0;




                sql = "sELECT  distinct Area_Name,cast(Area_Code as varchar) as Area_code,row_number() over (order by Area_Name) as Rownum froM Area_Master UNION sELECT  'All',-1,0 ";

                SqlDataAdapter locad = new SqlDataAdapter(sql, Conn);
                DataTable locdt = new DataTable("emp");
                locad.Fill(locdt);

                cmblocaltion.DisplayMember = "Area_Name";
                cmblocaltion.ValueMember = "Area_code";
                cmblocaltion.DataSource = locdt;
                cmblocaltion.SelectedIndex = 0;

                Conn.Close();
                Conn.Open();

                switch (Gvar._Gind)
                {
                    case 1:
                    case 2:
                        {
                            grpdate.Visible = true;

                            DataTable lstdt2 = new DataTable("Cost_master");



                            //cmbcatcode.DataSource = dt2;
                            //panel1.Visible = true;
                            panel2.Visible = true;
                            lbllst2.Text = "Cost Center ";

                            sql = "sELECT  cast(Cost_Code as Varchar) as Cost_Code,Cost_Name,row_number() over (order by Cost_Code) as Rownum from Cost_master  order by cost_code";

                            SqlDataAdapter lst2ada = new SqlDataAdapter(sql, Conn);
                            dv2 = new DataView();
                            dv3 = new DataView();
                            lstdt2.Clear();

                            lst2ada.Fill(lstdt2);


                            dv2.Table = lstdt2;
                            dv3.Table = lstdt2;

                            lst2.DataSource = dv2;
                            dv2.Sort = "Cost_Name";
                            lst2.Columns[0].Width = 50;
                            lst2.Columns[3].Visible = false;
                            lst2.Columns[2].Width = 300;
                            lst2.Columns[1].Width = 100;
                            lst2.ReadOnly = false;
                            lst2.Columns[2].ReadOnly = true;
                            lst2.Columns[1].ReadOnly = true;
                            lst2.Columns[0].ReadOnly = false;
                            lst2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            if (Gvar._Gind == 1)
                            {
                                chkinvoice.Visible = false;
                                cmbtransaction.Visible = false;
                                label3.Visible = false;
                            }

                            panel1.Visible = true;
                            // label5.Text = "Customer Cat.";
                            grpdate.Visible = true;

                         


                            if (Gvar.rptidx == 101 || Gvar.rptidx == 102)
                                sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=2";

                            if (Gvar.rptidx == 103 || Gvar.rptidx == 104)
                                sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=3";

                            if (Gvar.rptidx == 105 || Gvar.rptidx == 106)
                                sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=1 and acc_level=4";

                            ada2 = new SqlDataAdapter(sql, Conn);
                            dt2 = new DataTable("Accounts");
                            ada2.Fill(dt2);

                            dv.Table = dt2;
                            dv1.Table = dt2;
                            lst1.DataSource = dv;
                            dv.Sort = "Acc_Name";
                            lst1.Columns[0].Width = 50;
                            lst1.Columns[3].Visible = false;
                            lst1.Columns[1].Width = 300;
                            lst1.Columns[2].Width = 175;
                            lst1.ReadOnly = false;
                            lst1.Columns[2].ReadOnly = true;
                            lst1.Columns[1].ReadOnly = true;
                            lst1.Columns[0].ReadOnly = false;
                            lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            break;

                        }
                    case 3:
                        {
                            chkdate.Checked = false;
                            repdt1.Value = DateTime.Now.Date;
                            repdt2.Value = DateTime.Now.Date;
                            grpdate.Visible = true;
                            panel1.Visible = false;
                            panel2.Visible = false;
                            label5.Visible = false;
                            cmbowner.Visible = false;
                            cmblocaltion.DataSource = null;
                            lblsaleagent.Text = "Account Entry";
                           if (Gvar.SuperUserid==1)
                           {                             lbllocation.Text = "Branch";
                            sql = "sELECT   Branch_Name,Branch_Code froM Branches UNION sELECT  'All',-1 order by branch_code ";

                             locad = new SqlDataAdapter(sql, Conn);
                             locdt = new DataTable("brn");
                            locad.Fill(locdt);

                            cmblocaltion.DisplayMember = "Branch_Name";
                            cmblocaltion.ValueMember = "Branch_Code";
                            cmblocaltion.DataSource = locdt;
                            cmblocaltion.SelectedIndex = 0;
                            cmblocaltion.SelectedValue = Gvar.brn_code;

                        }
                           else
                           {
                               cmblocaltion.Visible = false;
                               lbllocation.Visible = false;
                           }


                            sql = "sELECT   'C' as Code,'Credit Only' as Ename Union Select 'D','Debit Only'  UNION sELECT  'A','All' order by code ";
                            cmbsaleagent.DataSource = null;
                            locad = new SqlDataAdapter(sql, Conn);
                            locdt = new DataTable("cdt");
                            locad.Fill(locdt);

                            cmbsaleagent.DisplayMember = "Ename";
                            cmbsaleagent.ValueMember = "Code";
                            cmbsaleagent.DataSource = locdt;
                            cmbsaleagent.SelectedIndex = 0;


                        }
                        break;
                    case 112:
                        {

                            lblsaleagent.Visible = false;
                            cmbsaleagent.Visible = false;
                            lbllocation.Visible = false;
                            cmblocaltion.Visible = false;
                            label5.Visible = false;
                            cmbowner.Visible = false;
                            label3.Visible = false;
                            cmbtransaction.Visible = false;
                            grpdate.Visible = true;
                        }
                        break;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        {

                            lblsaleagent.Visible = false;
                            cmbsaleagent.Visible = false;
                            lbllocation.Visible = false;
                            cmblocaltion.Visible = false;
                            label5.Visible = false;
                            cmbowner.Visible = false;
                            label3.Visible = false;
                            cmbtransaction.Visible = false;
                            label1.Visible = false;
                            repdt1.Visible = false;
                            chkdate.Visible = false;
                            label2.Text = "As of Date";
                            grpdate.Visible = true;
                        }
                        break;

                }


                try
                {

                    //if (Gvar.rptidx < 100 && Gvar.rptidx > 106)
                    //{ 
                    sql = "SELECT * FROM AC_OPTIONS WHERE  ac_options.ID =1";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount > 0)
                    {
                        if (int.Parse(rec.Fields["show_dept"].Value.ToString()) == 1)
                        {
                            lbldept.Visible = true;
                            cmbdept.Visible = true;
                        }
                        else
                        {
                            lbldept.Visible = false;
                            cmbdept.Visible = false;
                        }

                        if (int.Parse(rec.Fields["show_cost"].Value.ToString()) == 1)
                        {
                            panel2.Visible = true;
                        }
                        else
                        {
                            panel2.Visible = false;
                        }
                    }
                        emptype = Convert.ToInt16(rec.Fields["emp_ac_type"].Value.ToString());
                       
                    //}

                    //else
                    {
                        lbldept.Visible = false;
                        cmbdept.Visible = false;
                    }
                    isini = false;
                   

                
                }
                catch
                {
                }

                if (Gvar._Gind != 3)
                {
                    sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=" + emptype + " UNION sELECT   'All',-1,0 ";

                    SqlDataAdapter empad = new SqlDataAdapter(sql, Conn);
                    DataTable empdt = new DataTable("emp");
                    empad.Fill(empdt);

                    cmbsaleagent.DisplayMember = "Acc_Name";
                    cmbsaleagent.ValueMember = "Acc_no";
                    cmbsaleagent.DataSource = empdt;
                    cmbsaleagent.SelectedIndex = 0;

                    cmbtransaction.SelectedIndex = 0;
                }


                // sql = "sELECT  Trn_Name,cast(Trn_code as varchar) as Trn_Code,row_number() over (order by Trn_Name) as Rownum froM Trn_Type  UNION sELECT   'All',-1,0  order by 3";
                sql = "SELECT  Trn_Name,CODES froM Trn_Type_rep  order by TRN_CODE";

                SqlDataAdapter trnad = new SqlDataAdapter(sql, Conn);
                DataTable trndt = new DataTable("trntype");
                trnad.Fill(trndt);

                cmbtransaction.DisplayMember = "Trn_Name";
                cmbtransaction.ValueMember = "CODES";
                cmbtransaction.DataSource = trndt;
                cmbtransaction.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void ini_tab()
        {
            maintab1.Left = this.Left;
            maintab1.Width = this.Width;
            maintab1.Height = this.Height;
            //cmbowner.SelectedIndex = 0;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Select";
            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
            chk2.HeaderText = "Select";
            lst1.Columns.Add(chk);

            chk.ReadOnly = false;
            lst2.Columns.Add(chk2);
            chk2.ReadOnly = false;

        }


        private void test()
        {

            ReportDocument rpt = new ReportDocument();
            string filePath = Application.UserAppDataPath + "\\Report\\CrystalReport.rpt";
            rpt.Load(filePath);


            //   Crv1.ReportSource=rpt;

            //objReport.SetDatabaseLogon 26("amit", "password", "AVDHESH\SQLEXPRESS", "TestDB")
            // crystalReportViewer.ReportSource = reportDocument;
            //ConnectionInfo connInfo = new ConnectionInfo();
            //connInfo.ServerName = "dbservername";
            //connInfo.DatabaseName = "dbname";
            //connInfo.UserID = "dbusername";
            //connInfo.Password = "dbpassword";
            //reportViewer.ReportSource = GetReportSource(connInfo);
            //reportViewer.RefreshReport();

        }
        private void btnView_Click(object sender, EventArgs e)
        {

            try
            {


                ReportDocument CrRep = new ReportDocument();
                CRT2 = "";



                CRT3 = "";

                fnd = false;



                DateTime edt1 = repdt1.Value;
                DateTime edt2 = repdt2.Value; ;

                RPTHEAD = "";
                // string sdt2 = string.Format("yyyy,MM,dd,00,00,00", Gvar.ArCalendar(dt2.Value, true));
                string adate1 = repdt1.Value.Date.ToString("dd-MM-yyyy");
                string adate2 = repdt2.Value.Date.ToString("dd-MM-yyyy");

                string sdt1 = edt1.Date.ToString("yyyy,MM,dd,00,00,00");
                string sdt2 = edt2.Date.ToString("yyyy,MM,dd,23,59,59");
                string hd3;
                hd3 = "'Report'";
                crt1 = "";
                switch (Gvar._Gind)
                {
                    #region case1;
                    case 1:
                        {


                            rep_path = Gvar.report_path + "\\reports\\repaccsum.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));




                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");
                            string crt2 = "1=1";
                             string crt4 = "";
                             string crt3 = "";
                            if (Gvar.rptidx == 101)
                            {
                                crt1 = "  {ACCOUNTS.ACC_TYPE_CODE}=2";
                                crt4 = "{accounts.acc_no} ";
                            }
                            else
                            {
                                if (Gvar.rptidx == 103)
                                {
                                    crt1 = "  {ACCOUNTS.ACC_TYPE_CODE}=3";
                                    crt4 = " {accounts.acc_no} ";
                                }
                            }

                            if (Gvar.rptidx == 105)
                            {
                                crt1 = "{ACCOUNTS.ACC_TYPE_CODE}=1";
                                crt4 = "{accounts.gl_acc_no} ";
                                if (cmbowner.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = "{accounts.ACC_CLASS}  = " + cmbowner.SelectedValue;
                                    RPTHEAD = RPTHEAD + " for " + cmbowner.Text;
                                }

                                if (cmblevel.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = crt2 +  " and {accounts.ACC_LEVEL}  = " + cmblevel.SelectedIndex;
                                    RPTHEAD = RPTHEAD + " , level" + cmblevel.Text;
                                }

                                if (cmbacheader.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = crt2 + "  and level" + (Convert.ToInt16(cmblevel.SelectedIndex) - 1).ToString() + "_NO =" + cmbacheader.SelectedValue;
                                    RPTHEAD = RPTHEAD + ", for " + cmbacheader.Text;
                                }

                                

                            }


                            if (!chkdate.Checked)
                            {

                                crt1 = " AND  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                RPTHEAD = RPTHEAD + " date  From " + rdt1 + " To " + rdt2;
                            }
                            else
                            {
                                RPTHEAD = RPTHEAD + "Account Summary Report for All Dates";
                                //crt1 = "";

                            }




                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                            
                           




                            fnd = false;
                            string crite4 = "";
                            lst1.EndEdit();
                            int i = 0;
                            if (chklst1.Checked)
                            {
                                crt4 = "1=1";
                                fnd = true;
                            }
                            else
                            {
                                for (i = 0; i < lst1.RowCount; i++)
                                {
                                    if (lst1[0, i].Value != null)
                                    {

                                        if ((bool)lst1[0, i].Value)
                                        {
                                            if (crite4 == "")
                                            {
                                                crite4 = lst1[2, i].Value.ToString();
                                            }
                                            else
                                            {
                                                crite4 = crite4 + "," + lst1[2, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                RPTHEAD = RPTHEAD + ", for Accounts " + crite4;
                                crt4 = crt4 + " in [" + crite4 + "]";
                                



                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Accounts selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            fnd = false;
                            
                            lst2.EndEdit();

                            if (chklst2.Checked)
                            {
                                // crt3 = "{accounts.acc_no} <> -1";
                                fnd = true;
                            }
                            else
                            {
                                for (i = 0; i < lst2.RowCount; i++)
                                {
                                    if (lst2[0, i].Value != null)
                                    {

                                        if ((bool)lst2[0, i].Value)
                                        {
                                            if (crt3 == "")
                                            {
                                                crt3 = lst2[1, i].Value.ToString();
                                            }
                                            else
                                            {
                                                crt3 = crt3 + "," + lst2[1, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt3 = "{TRN_accounts.COST_CODE}  in [" + crt3 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Cost Center selected, Please Try Again", "Wrong Cost Center Selection");
                                return;
                            }

                            string crt7 = "";
                            if (cmbdept.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt7 = "{trn_accounts.dept_code}  = " + cmbdept.SelectedValue;
                                RPTHEAD = RPTHEAD + " for Department " + cmbdept.Text;
                            }

                            string crt5 = "";
                            if (cmbsaleagent.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt5 = "{accounts.account_link}  = " + cmbsaleagent.SelectedValue;
                                //RPTHEAD = RPTHEAD + " for Saleagent " + cmbsaleagent.Text;
                            }


                            string crt6 = "";
                            if (cmblocaltion.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt6 = "{accounts_info.area_name}  = '" + cmblocaltion.Text + "'";
                                RPTHEAD = RPTHEAD + " for Area " + cmblocaltion.Text;
                            }


                            string crt = "";
                            if (crt1 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt1; }
                                else
                                {
                                    crt = crt1;
                                }
                            }

                            if (crt2 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt2; }
                                else
                                {
                                    crt = crt2;
                                }
                            }

                            if (crt3 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt3; }
                                else
                                {
                                    crt = crt3;
                                }
                            }
                            // if (crt3 != "") crt = crt + " aND " + crt3;
                            //if (crt4 != "") crt = crt + " aND " + crt4;
                            //crt = crt + " aND " + crt4 + " AND {DATA_ENTRY.TRN_TYPE} = 7";
                            if (crt4 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt4; }
                                else
                                {
                                    crt = crt4;
                                }
                            }

                            if (crt5 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt5; }
                                else
                                {
                                    crt = crt5;
                                }
                            }

                            if (crt6 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt6; }
                                else
                                {
                                    crt = crt6;
                                }
                            }

                            if (crt7 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt7; }
                                else
                                {
                                    crt = crt7;
                                }
                            }
                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            //CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;




                            if (cmbtransaction.SelectedIndex > 0)
                            {
                                RPTHEAD = RPTHEAD + " for Transactions of " + cmbtransaction.Text;
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            }

                            if (chkcurrency.Checked)
                            {
                                CrRep.DataDefinition.FormulaFields["Cur"].Text = "1";

                            }
                            //if (cmbtransaction.SelectedIndex == 2)
                            //{
                            //    if (crt == "")
                            //        rep_formula = "{TRN_accounts.DR_CR}='C'";
                            //    else
                            //        rep_formula = crt + " AND {TRN_accounts.DR_CR}='C'";


                            //    CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            //}

                        }
                        break;
                    #endregion case1;
                    #region case2;
                    case 2:
                        {

                            if (chkcurrency.Checked)
                                rep_path = Gvar.report_path + "\\reports\\repacctrans_dlr1.rpt";
                            else

                                rep_path = Gvar.report_path + "\\reports\\repacctrans.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));




                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");
                             string crt4="1=1";
                            if (Gvar.rptidx == 102)
                            {
                                crt1 = "  {ACCOUNTS.ACC_TYPE_CODE}=2";
                                crt4 = "{accounts.acc_no} ";
                            }
                            else
                            {
                                if (Gvar.rptidx == 104)
                                {
                                    crt1 = "  {ACCOUNTS.ACC_TYPE_CODE}=3";
                                    crt4 = "{accounts.acc_no} ";
                                }

                            }
                           
                            string crt2 = "1=1";
                            
                           
                            if (Gvar.rptidx == 106)
                            {
                                
                                crt1 = "{ACCOUNTS.ACC_TYPE_CODE}=1";
                                crt4 = "{accounts.gl_acc_no} ";
                                if (cmbowner.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = "{accounts.ACC_CLASS}  = " + cmbowner.SelectedValue;
                                    RPTHEAD = RPTHEAD + " for " + cmbowner.Text;
                                }

                                if (cmblevel.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = crt2 + " and {accounts.ACC_LEVEL}  = " + cmblevel.SelectedIndex;
                                    RPTHEAD = RPTHEAD + " , level" + cmblevel.Text;
                                }

                                if (cmbacheader.SelectedIndex < 1)
                                {

                                    // crt2 = "{accounts.acc_type_code} <> -1";
                                }
                                else
                                {
                                    crt2 = crt2 + "  and level" + (Convert.ToInt16(cmblevel.SelectedIndex) - 1).ToString() + "_NO =" + cmbacheader.SelectedValue;
                                    RPTHEAD = RPTHEAD + ", for " + cmbacheader.Text;
                                }



                            }


                            if (!chkdate.Checked)
                            {
                                crt1 = " AND  ( {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")  OR {TRN_ACCOUNTS.ENTRY_NO}=-999)";
                                RPTHEAD = "Account Detail From " + rdt1 + " To " + rdt2;
                                // var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];

                                CrRep.Subreports[1].RecordSelectionFormula = "{TRN_ACCOUNTS.PAY_DATE} < DateTime (" + sdt1 + ") and {TRN_ACCOUNTS.ACC_NO} = {?Pm-TRN_ACCOUNTS.ACC_NO}";
                                //CrRep.Subreports[0].Refresh();

                                // CrRep.SetParameterValue("@dt1",dt1.Value.ToString("yyyy-MM-dd"));
                                //CrRep.SetParameterValue("@dt2", dt2.Value.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                
                                var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];
                                subrpt.ObjectFormat.EnableSuppress = true;
                                RPTHEAD = "Accounts Detail Report for All Date ";
                                //CrRep.SetParameterValue("@dt1", "1900-01-01");
                                //CrRep.SetParameterValue("@dt2", "2900-01-01");
                            }



                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                            
                            if (cmbowner.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt2 = "{accounts.acc_type_code}  = " + cmbowner.SelectedValue;
                                RPTHEAD = RPTHEAD + ", for Type " + cmbowner.Text;
                            }




                            fnd = false;
                            string crite4 = "";
                            lst1.EndEdit();
                            int i = 0;
                            if (chklst1.Checked)
                            {
                                crt4 = "";
                                fnd = true;
                            }
                            else
                            {
                                for (i = 0; i < lst1.RowCount; i++)
                                {
                                    if (lst1[0, i].Value != null)
                                    {

                                        if ((bool)lst1[0, i].Value)
                                        {
                                            if (crite4 == "")
                                            {
                                                crite4 = lst1[2, i].Value.ToString();
                                            }
                                            else
                                            {
                                                crite4 = crite4 + "," + lst1[2, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                               // crt4 = "{accounts.acc_no}  in [" + crt4 + "]";
                                crt4 = crt4 + "  in [" + crite4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Accounts selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            fnd = false;
                            string crt3 = "";
                            lst2.EndEdit();

                            if (chklst2.Checked)
                            {
                                // crt3 = "{accounts.acc_no} <> -1";
                                fnd = true;
                            }
                            else
                            {
                                for (i = 0; i < lst2.RowCount; i++)
                                {
                                    if (lst2[0, i].Value != null)
                                    {

                                        if ((bool)lst2[0, i].Value)
                                        {
                                            if (crt3 == "")
                                            {
                                                crt3 = lst2[1, i].Value.ToString();
                                            }
                                            else
                                            {
                                                crt3 = crt3 + "," + lst2[1, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt3 = "{TRN_accounts.COST_CODE}  in [" + crt3 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Cost Center selected, Please Try Again", "Wrong Cost Center Selection");
                                return;
                            }

                            string crt7 = "";
                            if (cmbdept.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt7 = "{trn_accounts.dept_code}  = " + cmbdept.SelectedValue;
                                RPTHEAD = RPTHEAD + " for Department " + cmbdept.Text;
                            }


                            string crt5 = "";
                            if (cmbsaleagent.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt5 = "{accounts.account_link}  = " + cmbsaleagent.SelectedValue;
                                RPTHEAD = RPTHEAD + " for Saleagent " + cmbsaleagent.Text;
                            }


                            string crt6 = "";
                            if (cmblocaltion.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt6 = "{accounts_info.area_name}  = '" + cmblocaltion.Text + "'";
                                RPTHEAD = RPTHEAD + " for Area " + cmblocaltion.Text;
                            }

                            string crt = "";
                            if (crt1 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt1; }
                                else
                                {
                                    crt = crt1;
                                }
                            }

                            if (crt2 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt2; }
                                else
                                {
                                    crt = crt2;
                                }
                            }

                            if (crt3 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt3; }
                                else
                                {
                                    crt = crt3;
                                }
                            }
                            // if (crt3 != "") crt = crt + " aND " + crt3;
                            //if (crt4 != "") crt = crt + " aND " + crt4;
                            //crt = crt + " aND " + crt4 + " AND {DATA_ENTRY.TRN_TYPE} = 7";


                            if (crt4 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt4; }
                                else
                                {
                                    crt = crt4;
                                }
                            }

                            if (crt5 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt5; }
                                else
                                {
                                    crt = crt5;
                                }
                            }

                            if (crt6 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt6; }
                                else
                                {
                                    crt = crt6;
                                }
                            }

                            if (crt7 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt7; }
                                else
                                {
                                    crt = crt7;
                                }
                            }

                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            //rep_formula = crt1 + " aND " + crt2 + " aND " + crt3 + " and " + crt4; //" AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;

                            if (cmbtransaction.SelectedIndex > 0)
                            {
                                RPTHEAD = RPTHEAD + " for Transactions of " + cmbtransaction.Text;
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            }


                            //if (cmbtransaction.SelectedIndex == 2)
                            //{
                            //    if (crt == "")
                            //        rep_formula = "{TRN_accounts.DR_CR}='C'";
                            //    else
                            //        rep_formula = crt + " AND {TRN_accounts.DR_CR}='C'";


                            //    CrRep.SummaryInfo.ReportTitle = RPTHEAD + ", For Trans of " + cmbtransaction.Text;

                            //}
                            if (!cmbdept.Visible)
                            {


                                //CrRep.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[10].SectionFormat.EnableSuppress = true;
                            }
                            if (!panel2.Visible)
                            {
                            }

                            if (chkinvoice.Checked)
                            // CrRep.GetComponentName("invoice")
                            {
                                string sec = CrRep.ReportDefinition.Sections[9].Name;
                                CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = false;
                            }


                        }
                        break;
                    #endregion case2;
                    #region case3;
                    case 3:
                        {

                           

                                rep_path = Gvar.report_path + "\\reports\\repacctrans.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));

                             sql = "SELECT def_cash_ac FROM AC_OPTIONS WHERE  ac_options.ID =1";
                ADODB.Recordset rec = new ADODB.Recordset();
                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                object  cash_ac=0;
                    if (rec.RecordCount > 0)
                    {
                        cash_ac = rec.Fields[0].Value;
                    }


                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");


                            crt1 = "1=1";
                            string crt2 = "1=1";

                            crt1 = " {accounts.acc_no}  = " + cash_ac;
                                    RPTHEAD = "Cash Workbook Detail  report ";
                                


                           


                            if (!chkdate.Checked)
                            {
                                crt1 = crt1 + " AND  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                RPTHEAD = "Cash Workbook Detail  report From " + rdt1 + " To " + rdt2;
                                // var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];

                              //  CrRep.Subreports[1].RecordSelectionFormula = "{TRN_ACCOUNTS.PAY_DATE} < DateTime (" + sdt1 + ") and {TRN_ACCOUNTS.ACC_NO} = {?Pm-TRN_ACCOUNTS.ACC_NO}";
                                //CrRep.Subreports[0].Refresh();

                                // CrRep.SetParameterValue("@dt1",dt1.Value.ToString("yyyy-MM-dd"));
                                //CrRep.SetParameterValue("@dt2", dt2.Value.ToString("yyyy-MM-dd"));
                            }
                            else
                            {

                                var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];
                                subrpt.ObjectFormat.EnableSuppress = true;
                                RPTHEAD = "Cash Workbook Detail  report for All Date ";
                                //CrRep.SetParameterValue("@dt1", "1900-01-01");
                                //CrRep.SetParameterValue("@dt2", "2900-01-01");
                            }



                        


                            if (cmbtransaction.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt2 = crt2 + " and {trn_accounts.trn_by}  = " + cmbtransaction.SelectedValue;
                                RPTHEAD = RPTHEAD + ", for  " + cmbtransaction.Text;
                            }

                            if (cmbsaleagent.SelectedIndex < 1)
                            {

                               
                            }
                            else
                            {
                                crt2 = crt2 + " and {trn_accounts.dr_cr}  = " + cmbsaleagent.SelectedValue;
                                RPTHEAD = RPTHEAD + ", for Account Type  " + cmbsaleagent.Text;
                            }

                            if (cmblocaltion.SelectedIndex < 1)
                            {

                                // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt2 = crt2 + " and {trn_accounts.brn_code}  = " + cmblocaltion.SelectedValue;
                                RPTHEAD = RPTHEAD + ", for Branch  " + cmblocaltion.Text;
                            }





                            rep_formula = crt1 + " aND " + crt2;




                            //rep_formula = crt1 + " aND " + crt2 + " aND " + crt3 + " and " + crt4; //" AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;

                          

                        }
                        break;
                    #endregion case2;
                    #region case 12;
                    case 112:
                        {


                            rep_path = Gvar.report_path + "\\reports\\rpIncomeExp.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));




                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                            if (!chkdate.Checked)
                            {
                                crt1 = "  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                RPTHEAD = "Income and Expense Detail From " + rdt1 + " To " + rdt2;
                                // var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];

                                CrRep.Subreports[0].RecordSelectionFormula = "{TRN_ACCOUNTS.PAY_DATE} < DateTime (" + sdt1 + ") and {TRN_ACCOUNTS.ACC_NO} = {?Pm-TRN_ACCOUNTS.ACC_NO}";
                                //CrRep.Subreports[0].Refresh();

                                // CrRep.SetParameterValue("@dt1",dt1.Value.ToString("yyyy-MM-dd"));
                                //CrRep.SetParameterValue("@dt2", dt2.Value.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];
                                subrpt.ObjectFormat.EnableSuppress = true;
                                RPTHEAD = "Income and Expense Detail Report for All Date ";
                                //CrRep.SetParameterValue("@dt1", "1900-01-01");
                                //CrRep.SetParameterValue("@dt2", "2900-01-01");
                            }



                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                            string crt2 = "";

                            crt2 = "{accounts.acc_type_code}  = 7";//cmbowner.SelectedValue;




                            string crt = "";
                            if (crt1 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt1; }
                                else
                                {
                                    crt = crt1;
                                }
                            }

                            if (crt2 != "")
                            {
                                if (crt != "")
                                { crt = crt + " aND " + crt2; }
                                else
                                {
                                    crt = crt2;
                                }
                            }

                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            //rep_formula = crt1 + " aND " + crt2 + " aND " + crt3 + " and " + crt4; //" AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;

                            if (cmbtransaction.SelectedIndex > 0)
                            {
                                RPTHEAD = RPTHEAD + " for Transactions of " + cmbtransaction.Text;
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.trn_by} in [" + cmbtransaction.SelectedValue + "]";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            }

                            if (!cmbdept.Visible)
                            {


                                //CrRep.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = true;
                                //CrRep.ReportDefinition.Sections[10].SectionFormat.EnableSuppress = true;
                            }
                            if (!panel2.Visible)
                            {
                            }

                            if (chkinvoice.Checked)
                            // CrRep.GetComponentName("invoice")
                            {
                                string sec = CrRep.ReportDefinition.Sections[9].Name;
                                CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = false;
                            }


                        }
                        break;
                    #endregion case2;
                    #region case 12-15;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        {
                            object out1;

                            if (Gvar._Gind == 12)
                            {
                                sql = "update Report_dt set incdate='" + repdt2.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59'";
                                ADOconn.Execute(sql, out out1);

                                RPTHEAD = "Income and Expense  Report as of " + adate2;
                                rep_path = Gvar.report_path + "\\reports\\rpIncomeExp.rpt";
                            }
                            if (Gvar._Gind == 13)
                            {
                                sql = "update Report_dt set trail_date='" + repdt2.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59'";
                                ADOconn.Execute(sql, out out1);

                                RPTHEAD = "Trail Balance Report as of " + adate2;
                                rep_path = Gvar.report_path + "\\reports\\rptTrailBalance.rpt";
                            }

                            if (Gvar._Gind == 14)
                            {
                                sql = "update Report_dt set pnldate='" + repdt2.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59'";
                                ADOconn.Execute(sql, out out1);
                                RPTHEAD = "Profit and Loss Report as of " + adate2;

                                rep_path = Gvar.report_path + "\\reports\\repProfitNloss1.rpt";
                            }



                            if (Gvar._Gind == 15)
                            {
                                sql = "update Report_dt set Baldate='" + repdt2.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59'";
                                ADOconn.Execute(sql, out out1);
                                RPTHEAD = "Balance Sheet Report as of " + adate2;
                                rep_path = Gvar.report_path + "\\reports\\repBalSheet1.rpt";
                            }
                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));




                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");




                            //crt1 = "  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            //RPTHEAD = "Income and Expense Detail From " + rdt1 + " To " + rdt2;
                            //// var subrpt = (SubreportObject)CrRep.ReportDefinition.ReportObjects["CLBAL"];

                            //CrRep.Subreports[0].RecordSelectionFormula = "{TRN_ACCOUNTS.PAY_DATE} < DateTime (" + sdt1 + ") and {TRN_ACCOUNTS.ACC_NO} = {?Pm-TRN_ACCOUNTS.ACC_NO}";



                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";







                            string crt = "";


                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            //rep_formula = crt1 + " aND " + crt2 + " aND " + crt3 + " and " + crt4; //" AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;





                        }
                        break;
                    #endregion case2;
                }




                //if (CRT4 != "")

                //    rep_formula = rep_formula + " AND " + CRT4;

                CrRep.Load(rep_path);

                //MessageBox.Show(rep_formula);

                CrRep.OpenSubreport("HEADER.rpt").DataDefinition.FormulaFields["RPTHEAD"].Text = "'" + RPTHEAD + "'";
              


                CrRep.ReportOptions.EnableSaveDataWithReport = false;
                CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
                CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
                CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

                Tables CrTables;

                crconnectioninfo.ServerName = decoder.DataSource;
                crconnectioninfo.DatabaseName = decoder.InitialCatalog;
                crconnectioninfo.UserID = decoder.UserID;
                crconnectioninfo.Password = decoder.Password;


                //crconnectioninfo.ServerName = "Mujeeb-pc";
                //crconnectioninfo.DatabaseName = "sqlStockex";
                //crconnectioninfo.UserID = "sa";
                //crconnectioninfo.Password = "sa0101";
                CrTables = CrRep.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtablelogoninfo = CrTable.LogOnInfo;
                    crtablelogoninfo.ConnectionInfo = crconnectioninfo;
                    CrTable.ApplyLogOnInfo(crtablelogoninfo);
                }







                CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);

                if (rep_formula != "")
                {
                    CrRep.RecordSelectionFormula = rep_formula;
                }

               // CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                // crystalReportViewer.ReportSource = reportDocument;
                //ConnectionInfo connInfo = new ConnectionInfo();
                //connInfo.ServerName = "dbservername";
                //connInfo.DatabaseName = "dbname";
                //connInfo.UserID = "dbusername";
                //connInfo.Password = "dbpassword";
                //reportViewer.ReportSource = GetReportSource(connInfo);
                //reportViewer.RefreshReport();




                // CrRep.Database.SetDataSource (db)
                // CrRep.VerifyOnEveryPrint = False
                //CrRep.DiscardSavedData
                //CrRep.ReadRecords
                //CrRep.Load(rep_path);


                //Crv1.ReportSource = CrRep;
                //Crv1.Width = this.Width;




                FrmrepView frm = new FrmrepView();
                frm.MdiParent = this.ParentForm;

                frm.crv1.ReportSource = CrRep;
                frm.Show();

                //Control[] ctrls = frm.Controls.Find("crv1", false);
                //if (ctrls.Length > 0)
                //{

                //    CrystalDecisions.Windows.Forms.CrystalReportViewer  rep = (CrystalDecisions.Windows.Forms.CrystalReportViewer) ctrls[0];

                //     rep.ReportSource = CrRep;
                //     frm.Show();
                //}




                CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, "Mujeeb", decoder.InitialCatalog);










                //FRMREPORT.CRV1.ReportSource = CrRep
                //FRMREPORT.CRV1.Refresh
                //FRMREPORT.CRV1.RefreshEx (True)

                //FRMREPORT.CRV1.ViewReport

                //FRMREPORT.Show




            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }



        private void frmReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbowner.SelectedIndex = -1;
        }

        private void chklst1_CheckedChanged(object sender, EventArgs e)
        {

            if (chklst1.Checked)
            {
                textBox1.Enabled = false;
                lst1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                lst1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string txt = textBox1.Text.Trim();
                if (txt != "")
                {
                    int c = dv1.Count;
                    switch (Gvar._Gind)
                    {

                        case 1:
                        case 2:

                            dv1.RowFilter = "Acc_no LIKE  '" + txt + "%' OR Acc_name LIKE '" + txt + "%'";
                            c = dv1.Count;
                            if (c > 0)
                            {
                                c = Convert.ToInt32(dv1[0][2].ToString());
                                lst1.CurrentCell = lst1[0, c - 1];
                            }
                            break;


                    }

                }
                else
                    dv.RowFilter = "Acc_no <> '0'";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbowner_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (isini) return;

            switch (Gvar.rptidx)
            {

                case 105:
                case 106:
                    {
                        populate_headac();
                        populate_acc();
                        break;
                    }
            }
           
            return;
            switch (Gvar._Gind)
            {
                case 2:

                case 1:
                    {


                        chklst1.Checked = true;

                        panel1.Visible = true;
                        // label5.Text = "Customer Cat.";
                        grpdate.Visible = true;

                        lbllst1.Text = "Accounts";


                        sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts";
                        SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                        DataTable dt2 = new DataTable("Accounts");
                        if (cmbowner.SelectedIndex == 0 || cmbowner.Items.Count < 1)
                        {

                            ;



                            ada2.Fill(dt2);

                            dv.Table = dt2;
                            dv1.Table = dt2;
                            lst1.DataSource = dv;
                            dv.Sort = "Acc_Name";
                            lst1.Columns[0].Width = 50;
                            lst1.Columns[3].Visible = false;
                            lst1.Columns[1].Width = 300;
                            lst1.Columns[2].Width = 175;
                            lst1.ReadOnly = false;
                            lst1.Columns[2].ReadOnly = true;
                            lst1.Columns[1].ReadOnly = true;
                            lst1.Columns[0].ReadOnly = false;
                            lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            break;
                        }



                        sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=" + cmbowner.SelectedValue;

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("Accounts");
                        ada2.Fill(dt2);

                        dv.Table = dt2;
                        dv1.Table = dt2;
                        lst1.DataSource = dv;
                        dv.Sort = "Acc_Name";
                        lst1.Columns[0].Width = 50;
                        lst1.Columns[3].Visible = false;
                        lst1.Columns[2].Width = 100;
                        lst1.Columns[2].Width = 175;
                        lst1.ReadOnly = false;
                        lst1.Columns[2].ReadOnly = true;
                        lst1.Columns[1].ReadOnly = true;
                        lst1.Columns[0].ReadOnly = false;
                        lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        break;
                    }


            }

        }

        private void chklst2_CheckedChanged(object sender, EventArgs e)
        {

            if (chklst2.Checked)
            {
                textBox2.Enabled = false;
                lst2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                lst2.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = textBox2.Text.Trim();
                if (txt != "")
                {
                    int c = dv2.Count;
                    switch (Gvar._Gind)
                    {

                        case 2:
                        case 1:

                            dv2.RowFilter = "cost_code like  '" + txt + "%' OR cost_NAME LIKE '" + txt + "%'";
                            c = dv2.Count;
                            if (c > 0)
                            {

                                // c = Convert.ToInt32(dv2[0][3].ToString());
                                lst2.CurrentCell = lst2[0, 0];


                            }
                            break;


                    }

                }
                else
                    dv2.RowFilter = "cost_code <> '0'";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:

                        int lkprow = 0;

                        return;

                    case Keys.LControlKey:
                        if (lst2[0, lst2.CurrentCell.RowIndex].Value == null) lst2[0, lst2.CurrentCell.RowIndex].Value = 0;
                        lst1[0, lst2.CurrentCell.RowIndex].Value = !(bool)lst2[0, lst2.CurrentCell.RowIndex].Value;
                        e.Handled = false;
                        //textBox1.Text = textBox1.Text.Trim();
                        return;
                    case Keys.Up:

                        if (lst2.CurrentCell.RowIndex > 0)
                            lst2.CurrentCell = lst2[0, lst2.CurrentCell.RowIndex - 1];
                        e.Handled = true;
                        return;
                    case Keys.Down:
                        if (lst2.CurrentCell.RowIndex < lst2.RowCount - 1)
                            lst2.CurrentCell = lst2[0, lst2.CurrentCell.RowIndex + 1];
                        e.Handled = true;
                        return;

                }




                //e.Handled =false;
                //base.OnKeyDown(e) ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:

                        int lkprow = 0;

                        return;

                    case Keys.LControlKey:
                        if (lst1[0, lst1.CurrentCell.RowIndex].Value == null) lst1[0, lst1.CurrentCell.RowIndex].Value = 0;
                        lst1[0, lst1.CurrentCell.RowIndex].Value = !(bool)lst1[0, lst1.CurrentCell.RowIndex].Value;
                        e.Handled = false;
                        //textBox1.Text = textBox1.Text.Trim();
                        return;
                    case Keys.Up:

                        if (lst1.CurrentCell.RowIndex > 0)
                            lst1.CurrentCell = lst1[0, lst1.CurrentCell.RowIndex - 1];
                        e.Handled = true;
                        return;
                    case Keys.Down:
                        if (lst1.CurrentCell.RowIndex < lst1.RowCount - 1)
                            lst1.CurrentCell = lst1[0, lst1.CurrentCell.RowIndex + 1];
                        e.Handled = true;
                        return;

                }




                //e.Handled =false;
                //base.OnKeyDown(e) ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmblevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void cmbacheader_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                populate_acc();
            }
            catch (Exception ex)
            {

            }

        }

        private void cmblevel_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (isini) return;
            populate_headac();
            populate_acc();
        }
        private void populate_headac()
        {
            try
            {

                string crt1 = "1=1";
                string crt2 = "1=1";

                if (cmbowner.SelectedIndex < 1)
                {

                }
                else
                {
                    crt1 = "acc_class = " + cmbowner.SelectedValue;

                }

                if (cmblevel.SelectedIndex < 1)
                {

                }
                else
                {

                    crt2 = "acc_level =" + cmblevel.SelectedIndex;
                }






                switch (Gvar.rptidx)
                {
                    case 105:
                    case 106:
                        {
                            if (cmblevel.SelectedIndex == 1)
                            {
                                sql = "select acc_no,acc_name from accounts  where acc_no = " + cmbowner.SelectedValue;

                            }
                            else
                            {
                                sql = "select acc_no,acc_name from accounts  where acc_class = " + cmbowner.SelectedIndex + " and  acc_level <> 5 and   acc_level = " + cmblevel.SelectedIndex + "-1  Union select 0, ' All' order by acc_name ";
                            }

                            //sql = "select acc_no,acc_name from accounts  where acc_class = " + cmbclass.SelectedIndex + " and  acc_level <> 5 and   acc_level < " + cmblevel.SelectedIndex + " order by acc_name ";


                            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                            DataTable dt2 = new DataTable("acc_head");
                            ada2.Fill(dt2);

                            cmbacheader.DisplayMember = "acc_name";
                            cmbacheader.ValueMember = "acc_no";
                            cmbacheader.DataSource = dt2;

                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void change_class()
        {
            try
            {
                try
                {

                    if (cmbowner.SelectedIndex < 0) return;
                    populate_headac();
                    //populate_acc();

                    if (ADOconn.State == 0)
                    {
                        ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);


                    }

                    // if (cmbclass.SelectedIndex < 1) return;
                    populate_headac();



                    ADODB.Recordset tmp = new ADODB.Recordset();



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

               

                if (isini) return;
                isini = true;

                
                string sql = "";
                string crt1 = "1=1";
                string crt2 = "1=1";
                string crt3 = "1=1";
                if (cmbowner.SelectedIndex < 1)
                {

                }
                else
                {
                    crt1 = "acc_class = " + cmbowner.SelectedValue;

                }

                if (cmblevel.SelectedIndex < 1)
                {

                }
                else
                {

                    crt2 = "acc_level =" + cmblevel.SelectedIndex;
                }


                if (cmbacheader.SelectedIndex < 1)
                {

                }
                else
                {

                    crt2 = crt2 + "  and level" + (Convert.ToInt16(cmblevel.SelectedIndex) - 1).ToString() + "_NO =" + cmbacheader.SelectedValue;
                }












                Conn.Close();
                Conn.Open();
                // ada = new SqlDataAdapter("SELECT  * from Accounts where acc_type_code=" + cmbtype.SelectedValue + " order by acc_name", Conn);
                dt.Clear();

                //ada.Fill(dt);

                switch (Gvar.rptidx)
                {
                    case 105:
                    case 106:

                        {
                            sql = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =1 and " + crt1 + " and " + crt2 + " order by acc_no";
                            //sql = "select acc_name,acc_no from accounts where " + crt1 + " order by acc_name";
                            break;
                        }
                    case 101:
                        {
                            sql = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =2  order by acc_no";
                            //sql = "select acc_name,acc_no from accounts where acc_type_code =2  order by acc_name";
                            break;
                        }
                    case 102:
                        {
                            sql = "SELECT CAST(ACC_NO AS VARCHAR) AS ACC_NO,ACC_NAME  from Accounts where acc_type_code =3  order by acc_no";
                            //sql = "select acc_name,acc_no from accounts where acc_type_code =3  order by acc_name";
                            break;
                        }
                }



                ada = new SqlDataAdapter(sql, Conn);
                dt = new DataTable("Accounts");

                ada.Fill(dt);
                dv.Table = dt;

                lst1.DataSource = dv;
                




                isini = false;
            }
            catch (Exception ex)
            {

            }

        }
    
    }
}
