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


    public partial class frmReportProj : Form
        
    {
        
         SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
         //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
        
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
       
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataView dv2 = new DataView();
        DataView dv3 = new DataView();

        string sql;
        string crt1;
        string  CRT2;
        string CRT3;
        bool fnd;
        string RPTHEAD;
        string rep_path;
        string rep_formula;
   
        public frmReportProj()
        {
            InitializeComponent();
            ini_tab();
            SqlDataAdapter ada2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            try
            {
                    Conn.Close();
                    Conn.Open();

                    btnView_Click(null, null);
                   // this.Dispose();
                    return;

                ADODB.Connection ADOconn = new ADODB.Connection();
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
                try
                {
                    sql = "SELECT * FROM AC_OPTIONS WHERE  ac_options.ID =1";

                    rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (rec.RecordCount > 0)
                    {
                        if (int.Parse(rec.Fields["show_dept"].Value.ToString()) == 0)
                        {
                            lbldept.Visible = false;
                            cmbdept.Visible = false;
                        }

                        if (int.Parse(rec.Fields["show_cost"].Value.ToString()) == 0)
                        {
                            panel2.Visible = false;
                        }

                        emptype = Convert.ToInt16( rec.Fields["emp_ac_type"].ToString());
                    }
                }
                catch
                {
                }



               if (Gvar._Gind == 1 || Gvar._Gind == 2)
                          {
                              sql = "select acc_type_code,acc_type_name from acc_type UNION sELECT   -1,'All' froM acc_type order by acc_type_code ";
                        label5.Text = "Account Type";
                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("acc_type");
                        ada2.Fill(dt2);

                        cmbowner.DisplayMember = "acc_type_name";
                        cmbowner.ValueMember = "acc_type_code";
                        cmbowner.DataSource = dt2;
                        cmbowner.SelectedIndex = 0;

                    }
               



                    sql = "select Dept_code,dept_name from dept_master UNION sELECT   -1,'All' froM dept_master order by Dept_code ";
               
                    ada2 = new SqlDataAdapter(sql, Conn);
                    dt2 = new DataTable("dept_master");
                    ada2.Fill(dt2);

                    cmbdept.DisplayMember = "dept_name";
                    cmbdept.ValueMember = "Dept_code";
                    cmbdept.DataSource = dt2;
                    cmbdept.SelectedIndex = 0;


                    sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts where acc_type_code=" + emptype + " UNION sELECT   'All',-1,0 " ;

                    SqlDataAdapter empad = new SqlDataAdapter(sql, Conn);
                    DataTable empdt = new DataTable("emp");
                    empad.Fill(empdt);

                    cmbsaleagent.DisplayMember = "Acc_Name";
                    cmbsaleagent.ValueMember = "Acc_no";
                    cmbsaleagent.DataSource = empdt;
                    cmbsaleagent.SelectedIndex = 0;


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
                                panel1.Visible = true;
                               // panel2.Visible = true;
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
                                

                                label5.Text = "Customer Cat.";
                                 panel1.Visible = true;
                                // label5.Text = "Customer Cat.";
                                grpdate.Visible = true;

                                lbllst1.Text = "Accounts";



                                sql = "sELECT  Acc_Name,cast(Acc_No as varchar) as Acc_no,row_number() over (order by Acc_Name) as Rownum froM Accounts";

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

                    }

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
                maintab1.Height=this.Height;
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
 string filePath = Application.UserAppDataPath +  "\\Report\\CrystalReport.rpt";
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



                DateTime edt1=dt1.Value;
                DateTime edt2 = dt2.Value; ;
              
                
               // string sdt2 = string.Format("yyyy,MM,dd,00,00,00", Gvar.ArCalendar(dt2.Value, true));
                string adate1= dt1.Value.Date.ToString("dd-MM-yyyy");
                string adate2 = dt2.Value.Date.ToString("dd-MM-yyyy");

                string sdt1 = edt1.Date.ToString("yyyy,MM,dd,00,00,00");
                string sdt2 = edt2.Date.ToString("yyyy,MM,dd,23,59,59");
                string hd3;
                hd3 = "'Report'";
                crt1 = "";
                switch (Gvar._Gind)
                {
                    
                    case 1:
                        {


                            rep_path = Gvar.report_path + "\\reports\\RepProject.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));

                            goto nxt;


                            string rdt1 = dt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = dt2.Value.ToString("dd/MM/yyyy");

                            if (!chkdate.Checked)
                            { 

                            crt1 = "  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Account Summary From " + rdt1 + " To " + rdt2; 
                        }
                            else
                            {
                                RPTHEAD = "Account Summary Report for All Dates";
                                crt1 = "";

                            }




                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                            string crt2 = "";
                            if (cmbowner.SelectedIndex < 1)
                            {

                               // crt2 = "{accounts.acc_type_code} <> -1";
                            }
                            else
                            {
                                crt2 = "{accounts.acc_type_code}  = " + cmbowner.SelectedValue;
                                RPTHEAD = RPTHEAD + " for Type " + cmbowner.Text;
                            }




                            fnd = false;
                            string crt4 = "";
                            lst1.EndEdit();
                            int i = 0;
                            if (chklst1.Checked)
                            {
                                //crt4 = "{accounts.acc_no} <> -1";
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
                                            if (crt4 == "")
                                            {
                                                crt4 = lst1[2, i].Value.ToString() ;
                                            }
                                            else
                                            {
                                                crt4 = crt4 + "," + lst1[2, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{accounts.acc_no}  in [" + crt4 + "]";
                                RPTHEAD = RPTHEAD + ", for Accounts " + crt4;


                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Accounts selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            fnd = false;
                            string crt3 = "";
                            lst2.EndEdit();

                            //if (chklst2.Checked)
                            //{
                            //   // crt3 = "{accounts.acc_no} <> -1";
                            //    fnd = true;
                            //}
                            //else
                            //{
                            //    for (i = 0; i < lst2.RowCount; i++)
                            //    {
                            //        if (lst2[0, i].Value != null)
                            //        {

                            //            if ((bool)lst2[0, i].Value)
                            //            {
                            //                if (crt3 == "")
                            //                {
                            //                    crt3 = lst2[1, i].Value.ToString();
                            //                }
                            //                else
                            //                {
                            //                    crt3 = crt3 + "," + lst2[1, i].Value.ToString();
                            //                }
                            //                fnd = true;
                            //            }
                            //        }

                            //    }
                            //    crt3 = "{TRN_accounts.acc_no}  in [" + crt3 + "]";
                            //}

                            //if (!fnd)
                            //{
                            //    MessageBox.Show("There is No Account selected, Please Try Again", "Wrong Vehicle Selection");
                            //    return;
                            //}


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

                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;

                           


                            if (cmbtransaction.SelectedIndex == 1)
                            {
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.DR_CR}='D'";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.DR_CR}='D'";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            }


                            if (cmbtransaction.SelectedIndex == 2)
                            {
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.DR_CR}='C'";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.DR_CR}='C'";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + " , FOR " + cmbtransaction.Text;

                            }
                        nxt: ;
                        }
                        break;

                    case 2:
                        {


                            rep_path = Gvar.report_path + "\\reports\\repacctrans.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                           

                            string rdt1 = dt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = dt2.Value.ToString("dd/MM/yyyy");

                            if (!chkdate.Checked)
                            {
                            crt1 = "  {TRN_ACCOUNTS.PAY_DATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Account Detail From " + rdt1 + " To " + rdt2;
                            }
                            else
                            {
                                RPTHEAD = "Accounts Detail Report for All Date ";
                            }





                            //crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                           string crt2 = "";
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
                            string crt4 = "";
                            lst1.EndEdit();
                            int i = 0;
                            if (chklst1.Checked)
                            {
                                //crt4 = "{accounts.acc_no} <> -1";
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
                                            if (crt4 == "")
                                            {
                                                crt4 =  lst1[2, i].Value.ToString() ;
                                            }
                                            else
                                            {
                                                crt4 = crt4 + "," + lst1[2, i].Value.ToString() ;
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{accounts.acc_no}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Accounts selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            fnd = false;
                            string crt3 = "";
                            lst2.EndEdit();
                      
                            //if (chklst2.Checked)
                            //{
                            //    //crt3 = "{accounts.acc_no} <> -1";
                            //    fnd = true;
                            //}
                            //else
                            //{
                            //    for (i = 0; i < lst2.RowCount; i++)
                            //    {
                            //        if (lst2[0, i].Value != null)
                            //        {

                            //            if ((bool)lst2[0, i].Value)
                            //            {
                            //                if (crt3 == "")
                            //                {
                            //                    crt3 = lst2[1, i].Value.ToString() ;
                            //                }
                            //                else
                            //                {
                            //                    crt3 = crt3 + "," + lst2[1, i].Value.ToString();
                            //                }
                            //                fnd = true;
                            //            }
                            //        }

                            //    }
                            //    crt3 = "{TRN_accounts.acc_no}  in [" + crt3 + "]";
                            //}

                            //if (!fnd)
                            //{
                            //    MessageBox.Show("There is No vehicle selected, Please Try Again", "Wrong Vehicle Selection");
                            //    return;
                            //}



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

                            //if (crt1 != "") crt2 = crt1 + " And " + crt2;
                            rep_formula = crt; // 2 + " aND " + crt3 + " aND " + crt4;// +" AND {QRY_ITEM.QTY}<>0";




                            //rep_formula = crt1 + " aND " + crt2 + " aND " + crt3 + " and " + crt4; //" AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = RPTHEAD;// "Account Detail  Report from " + rdt1 + " To " + rdt2;

                            if (cmbtransaction.SelectedIndex == 1)
                            {
                                if(crt=="")
                                    rep_formula = "{TRN_accounts.DR_CR}='D'";
                                else
                                rep_formula = crt +  " AND {TRN_accounts.DR_CR}='D'";


                                CrRep.SummaryInfo.ReportTitle =  RPTHEAD  + ", For Trans of " + cmbtransaction.Text;

                            }


                            if (cmbtransaction.SelectedIndex == 2)
                            {
                                if (crt == "")
                                    rep_formula = "{TRN_accounts.DR_CR}='C'";
                                else
                                    rep_formula = crt + " AND {TRN_accounts.DR_CR}='C'";


                                CrRep.SummaryInfo.ReportTitle = RPTHEAD + ", For Trans of " + cmbtransaction.Text;

                            }
                            if (!cmbdept.Visible)
                            {


                                CrRep.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                                CrRep.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                                CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = true;
                                CrRep.ReportDefinition.Sections[10].SectionFormat.EnableSuppress = true;
                            }
                            if (!panel2.Visible)
                            {
                            }

                            if (chkinvoice.Checked)
                            // CrRep.GetComponentName("invoice")
                            {
                                string sec = CrRep.ReportDefinition.Sections[8].Name;
                                CrRep.ReportDefinition.Sections[8].SectionFormat.EnableSuppress = false;
                            }
                               

                        }
                        break;
                }

                


                //if (CRT4 != "")

                //    rep_formula = rep_formula + " AND " + CRT4;

                CrRep.Load(rep_path);

                //MessageBox.Show(rep_formula);




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

                CrRep.SummaryInfo.ReportTitle = RPTHEAD;
                
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
                string txt = "%" + textBox1.Text.Trim() + "%";
                if (txt != "")
                {
                    int c = dv1.Count;
                    switch (Gvar._Gind)
                    {

                        case 1:
                        case 2:
                        
                            dv1.RowFilter = "Acc_no LIKE  '%" + txt + "%' OR Acc_name LIKE '%" + txt + "%'";
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
                string txt = "%" + textBox2.Text.Trim() + "%";
                if (txt != "")
                {
                    int c = dv2.Count;
                    switch (Gvar._Gind)
                    {

                        case 2:
                        case 1:
                       
                            dv2.RowFilter = "cost_code like  '%" + txt + "%' OR cost_NAME LIKE '%" + txt + "%'";
                            c = dv2.Count;
                            if (c > 0)
                            {
                               
                               // c = Convert.ToInt32(dv2[0][3].ToString());
                                lst2.CurrentCell = lst2[0,0];


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
        
    }
}
