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
    public partial class frmsearchveh : FinOrgForm
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
        public frmsearchveh()
        {
            InitializeComponent();

        }





        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }



        private void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {

                switch (tabsearch.SelectedTab.Name)
                {
                    case "tabVehicle":
                        search_vehicle();
                        break;
                    case "Tabrent":
                        search_rent();
                        break;
                    case "TabService":
                        search_service();
                        break;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }





        private void search_vehicle()
        {
            try
            {
                string sql = "select * from veh_master";

                string crite;
                crite = "v.veh_no <> 0";


                txttab.Text = tabsearch.SelectedTab.Tag.ToString();

                switch (txttab.Text)
                {
                    case "0":
                        {
                            if (chkSbrand.Checked)
                            {
                                crite = crite + " and V.veh_brand ='" + cmbSbrand.SelectedItem + "'";

                            }


                            if (chkScat.Checked)
                            {
                                crite = crite + " and V.cmp_code =" + cmbScat.SelectedValue;

                            }

                            if (chkSstatus.Checked)
                            {
                                crite = crite + " and V.veh_status =" + cmbSstat.SelectedValue;

                            }


                            if (chkSmodel.Checked)
                            {
                                crite = crite + " and V.Model =" + cmbSmodel.SelectedValue;

                            }



                            if (chkStype.Checked)
                            {
                                crite = crite + " and V.veh_type ='" + cmbStype.SelectedItem + "'";

                            }


                            if (chkScrexp.Checked)
                            {
                                crite = crite + " and V.cr_expire_ar_date < '" + txtscrdate.Text + "'";

                            }



                            if (chkSinscomp.Checked)
                            {
                                crite = crite + " and V.ins_cmp_name = '" + cmbSins.SelectedItem + "'";

                            }

                            if (chkSinsexpdt.Checked)
                            {
                                crite = crite + " and convert(varchar,V.ins_end_date,112)  <= '" + dtSinsend.Value.ToString("yyyyMMdd") + "'";

                            }



                            //if (chkSinsexpdt.Checked)
                            //{
                            //    crite = crite + " and convert(varchar,V.ins_end_date,112)  <= '" + dtSinsend.Value.ToString("yyyyMMdd") + "'";

                            //}
                            sql = "SELECT     v.Veh_no, v.Plate_no, v.Veh_Name, dbo.VehBrand.Brand_name, dbo.VehModel.VehModel_name,  ";
                            sql = sql + " dbo.VehType.VehType_name, v.Ins_Cmp_Name, v.Ins_end_date, v.Cr_Expire_Ar_date, dbo.VehStatus.Vehstatus_name,  ";
                            sql = sql + " dbo.VehOwners.Vehowner_name FROM         dbo.Veh_Master as v LEFT OUTER JOIN ";
                            sql = sql + " dbo.VehModel ON v.Model = dbo.VehModel.VehModel_id LEFT OUTER JOIN ";
                            sql = sql + " dbo.VehType ON v.Veh_type = dbo.VehType.VehType_id LEFT OUTER JOIN ";
                            sql = sql + " dbo.VehBrand ON v.Veh_Brand = dbo.VehBrand.Brand_id LEFT OUTER JOIN ";
                            sql = sql + " dbo.VehStatus ON v.Veh_status = dbo.VehStatus.Vehstatus_id LEFT OUTER JOIN ";
                            sql = sql + " dbo.VehOwners ON v.Cmp_code = dbo.VehOwners.Vehowner_id where ";
                            sql = sql + crite;



                            break;
                        }
                }


                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("veh_master");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);

                dv2.Table = dtlkp;
                dgv1.DataSource = dv2;
                // dgvtrans.Columns[1].Width = 175;
                // dgvtrans.Columns[4].Name = "trn_type";
                // dgvtrans.Columns[1].Name = "inv_no";
                // dgvtrans.Columns[1].HeaderText = "Document No.";
                //// dgvtrans.Columns[7].Width = 175;
                // dgvtrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                // dgvtrans.ReadOnly = true;
                // tabControl1.SelectedTab = tabControl1.TabPages[1];

                // dgv2.Columns[2].Width = 175;
                // dgv1.Columns[0].Width = 175;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void search_rent()
        {
            try
            {
                string sql = "select * from veh_master";
                sql = "SELECT     v.Plate_no, v.Veh_Name,r.Rent_Start_date as RentFrom, r.Rent_End_Date RendTo,  ";
                
                sql = sql + " r.Received_date AS [PaidDate], r.Rent_Due_Amount as TotAmt, r.Rent_Received_Amount as RcvdAmt,r.Rent_Due_Amount - r.Rent_Received_Amount as Balance, ";

                sql = sql + " i.Rent_received_Amount AS [TotRcvd],c.Cus_Name as Customer, v.Veh_no, i.Issue_No,i.Issue_date as IssueDate,";
                sql = sql + "  i.Veh_Actual_Return as ReturnDate FROM  dbo.Veh_Master as v INNER JOIN ";
                sql = sql + "  dbo.Veh_Issue_Return as i ON v.Veh_no = i.Veh_no INNER JOIN veh_customer as c on i.cus_code=c.cus_code inner join";
                sql = sql + " dbo.Veh_Received_Monthly as r ON i.Veh_no = r.veh_no AND  ";
                sql = sql + " i.Issue_No = r.Issue_No   ";
                string crite;

                crite = "v.veh_no <> 0";



                if (chkrvehicle.Checked)
                {
                    crite = crite + " and V.veh_no =" + cmbrvehname.SelectedValue;

                }


                if (chkSrenttype.Checked)
                {
                    crite = crite + " and i.rent_type =" + cmbrenttype.SelectedValue;

                }

                if (chkSsponsor.Checked)
                {
                    crite = crite + " and V.cmp_code =" + cmbSsponsor.SelectedValue;

                }


                if (chkScustomer.Checked)
                {
                    crite = crite + " and i.cus_code =" + cmbScustomer.SelectedValue;

                }


                


                if (chkSrentmonth.Checked)
                {
                    if (cmbSrentmonth.SelectedIndex >0)
                    {

                        crite = crite + " and  year(r.Rent_Start_Date) =" + txtyear.Text + " and month(r.Rent_Start_Date) = " + cmbSrentmonth.SelectedIndex ;
                    }
                    else
                    {
                    }
                    if (cmbspaid.SelectedIndex == 1)
                    {
                        crite = crite + " and  r.rent_due_amount - r.rent_received_amount =0";

                    }
                    if (cmbspaid.SelectedIndex == 2)
                    {
                        crite = crite + " and  r.rent_due_amount - r.rent_received_amount > 0";


                    }
                }

                    if (crite == "")
                        crite = "v.veh_no <> 0";





                    if (chkrdates.Checked)
                    {
                        crite = crite + " and convert(varchar,i.rent_start_date,112) between  '" + dtrfrom.Value.ToString("yyyyMMdd") + "' and '" + dtrto.Value.ToString("yyyyMMdd") + "'";

                   }







                

                sql = sql + " where " + crite + " Order By r.Rent_Start_date";
                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("veh_master");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);

                dv2.Table = dtlkp;
                dgvrent.DataSource = dv2;
                // dgvtrans.Columns[1].Width = 175;
                // dgvtrans.Columns[4].Name = "trn_type";
                // dgvtrans.Columns[1].Name = "inv_no";
                // dgvtrans.Columns[1].HeaderText = "Document No.";
                //// dgvtrans.Columns[7].Width = 175;
                // dgvtrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                // dgvtrans.ReadOnly = true;
                // tabControl1.SelectedTab = tabControl1.TabPages[1];

                // dgv2.Columns[2].Width = 175;
                // dgv1.Columns[0].Width = 175;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search_service()
        {
            try
            {
                string sql = "select * from veh_master";

                string crite;
                crite = "v.veh_no <> 0";

                if (chksvehname.Checked)
                {
                    crite = crite + " and V.veh_no =" + cmbsvehname.SelectedValue;

                }
               

               
                            if (chkSsrvcdonedt.Checked)
                            {
                                crite = crite + " and convert(varchar, V.serviced_last_date,112)  <= '" + dtSsrvcdone.Value.ToString("yyyyMMdd") + "'";

                            }


                            if (chkSnextsrvcdt.Checked)
                            {
                                 crite = crite + " and convert(varchar, V.service_next_date,112)  <= '" + dtsnextsrvcdt.Value.ToString("yyyyMMdd") + "'";


                            }


                if (chkSnextodosrvc.Checked)
                            {
                                 crite = crite + " and (convert(varchar, V.service_next_date,112)  >= '" + DateTime.Now.Date.ToString("yyyyMMdd") + "' or v.sevice_next_odo > o.odo)";


                            }


                if (chkSododt.Checked)
                            {
                                 crite = crite + " and convert(varchar, o.ododate,112)  >= '" + dtSodonotupdate.Value.ToString("yyyyMMdd") + "'";


                            }


                if (chkSthumexp.Checked)
                            {
                                crite = crite + " and  v.veh_issue_no > 0  and (convert(varchar, t.thumdate,112)  <= '" + dtsthumexpire.Value.ToString("yyyyMMdd") + "' or t.thumdate is null) ";


                            }




                            if (chkSinscomp.Checked)
                            {
                                crite = crite + " and V.ins_cmp_name = '" + cmbSins.SelectedItem + "'";

                            }

                            if (chkSinsexpdt.Checked)
                            {
                                crite = crite + " and convert(varchar,V.ins_end_date,112)  <= '" + dtSinsend.Value.ToString("yyyyMMdd") + "'";

                            }



                            //if (chkSinsexpdt.Checked)
                            //{
                            //    crite = crite + " and convert(varchar,V.ins_end_date,112)  <= '" + dtSinsend.Value.ToString("yyyyMMdd") + "'";

                            //}


                            sql = "SELECT v.Veh_no, v.Plate_no, v.Veh_Name, v.Ins_Cmp_Name, v.Ins_end_date, c.Cus_name, v.Thum_No,t.ThumDate, v.Serviced_Last_ODO, v.Serviced_Last_Date,  ";
                            sql = sql + " v.Service_Next_Date, v.Sevice_Next_ODO,  o.ODODate, o.ODO FROM         dbo.Veh_Master AS v LEFT OUTER JOIN veh_customer as c on v.Thum_Cus_code = c.cus_code  LEFT OUTER JOIN ";
                            sql = sql + " (SELECT     MAX(Thum_date_To) AS ThumDate, Veh_no FROM dbo.Veh_Thum  GROUP BY Veh_no) AS t ON v.Veh_no = t.Veh_no LEFT OUTER JOIN ";
                            sql = sql + " (SELECT     Veh_no, MAX(ODO_Date) AS ODODate, MAX(ODO_Meter) AS ODO FROM  dbo.Veh_ODO   GROUP BY Veh_no) AS o ON v.Veh_no = o.Veh_no ";
                            
                           
                            sql = sql + " where " + crite + " order by v.Veh_Name";



                            
                        
                


                Conn.Close();
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);

                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("veh_master");
                adalkp.Fill(dtlkp);

                //var cellRectangle = dgv1.GetCellDisplayRectangle(dgv1.CurrentCell.ColumnIndex, dgv1.CurrentCell.RowIndex, true);

                dv2.Table = dtlkp;
                dgvservice.DataSource = dv2;
                // dgvtrans.Columns[1].Width = 175;
                // dgvtrans.Columns[4].Name = "trn_type";
                // dgvtrans.Columns[1].Name = "inv_no";
                // dgvtrans.Columns[1].HeaderText = "Document No.";
                //// dgvtrans.Columns[7].Width = 175;
                // dgvtrans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                // dgvtrans.ReadOnly = true;
                // tabControl1.SelectedTab = tabControl1.TabPages[1];

                // dgv2.Columns[2].Width = 175;
                // dgv1.Columns[0].Width = 175;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmsearchveh_Load(object sender, EventArgs e)
        {


            try
            {


                dtrfrom.Value = DateTime.Now.Date;
                dtrto.Value = DateTime.Now.Date;

                dtSinsend.Value = DateTime.Now.Date;
                dtsnextsrvcdt.Value = DateTime.Now.Date;


                dtSodonotupdate.Value = DateTime.Now.Date;
                dtSsrvcdone.Value = DateTime.Now.Date;

                dtsthumexpire.Value = DateTime.Now.Date;
                


                txtyear.Text = DateTime.Now.Year.ToString();
                string sql = "select brand_id,Brand_name from VehBrand ";

                SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                DataTable dt2 = new DataTable("VehBrand");
                ada2.Fill(dt2);

                cmbSbrand.DisplayMember = "Brand_name";
                cmbSbrand.ValueMember = "brand_id";
                cmbSbrand.DataSource = dt2;




                sql = "select Veh_no,Veh_name from Veh_Master order by veh_name";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("Veh_Master");
                ada2.Fill(dt2);

                cmbrvehname.DisplayMember = "Veh_name";
                cmbrvehname.ValueMember = "Veh_no";
                cmbrvehname.DataSource = dt2;

                cmbsvehname.DisplayMember = "Veh_name";
                cmbsvehname.ValueMember = "Veh_no";
                cmbsvehname.DataSource = dt2;


                sql = "select VehModel_id,VehModel_name from VehModel";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("VehModel");
                ada2.Fill(dt2);

                cmbSmodel.DisplayMember = "VehModel_name";
                cmbSmodel.ValueMember = "VehModel_id";
                cmbSmodel.DataSource = dt2;



                sql = "select Vehtype_id,VehType_name from VehType";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("VehType");
                ada2.Fill(dt2);

                cmbStype.DisplayMember = "VehType_name";
                cmbStype.ValueMember = "Vehtype_id";
                cmbStype.DataSource = dt2;


                sql = "select Vehowner_id,Vehowner_name from Vehowners";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("Vehowners");
                ada2.Fill(dt2);

                cmbScat.DisplayMember = "Vehowner_name";
                cmbScat.ValueMember = "Vehowner_id";
                cmbScat.DataSource = dt2;




                sql = "select Vehstatus_id,Vehstatus_name from Vehstatus";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("Vehstatus");
                ada2.Fill(dt2);

                cmbSstat.DisplayMember = "Vehstatus_name";
                cmbSstat.ValueMember = "Vehstatus_id";
                cmbSstat.DataSource = dt2;



                //sql = "select RentCode,Description from Veh_RentValue";

                //ada2 = new SqlDataAdapter(sql, Conn);
                //dt2 = new DataTable("Veh_RentValue");
                //ada2.Fill(dt2);

                //cmbrentvalue.DisplayMember = "Description";
                //cmbrentvalue.ValueMember = "RentCode";
                //cmbrentvalue.DataSource = dt2;



                sql = "select Ins_cmp_id,Ins_cmp_name from Veh_Ins_Company";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("Veh_Ins_Company");
                ada2.Fill(dt2);

                cmbSins.DisplayMember = "Ins_cmp_Name";
                cmbSins.ValueMember = "Ins_cmp_id";
                cmbSins.DataSource = dt2;



                sql = "select Cus_code,Cus_name from Veh_Customer  order by Cus_Name";

                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);
                DataTable dt3 = new DataTable("Veh_Customer");
                cmbScustomer.DisplayMember = "Cus_name";
                cmbScustomer.ValueMember = "Cus_code";
                ada3.Fill(dt3);

                cmbScustomer.DataSource = dt3;




                //cmbstamm.DisplayMember = "Cus_name";
                //cmbtamm.ValueMember = "Cus_code";
                //cmbtamm.DataSource = dt3;








            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btnprint_Click(object sender, EventArgs e)
        {


            {
                try
                {



                    //if (txtfilename.Text.Trim() != "")
                    //    sf1.FileName = txtfilename.Text;

                    sf1.InitialDirectory = Application.StartupPath;
                    sf1.Filter = "CSV File|*.Csv";


                    DialogResult res = sf1.ShowDialog();

                    if (res != DialogResult.OK) return;

                    string flname = res.ToString();

                    flname = sf1.FileName;

                    flname = flname.Replace(".Csv", "");
                    flname = flname + ".Csv";
                    //File.Create(Application.StartupPath + "\\" + txtfilename.Text) ;
                    //StreamWriter sw = new StreamWriter(Application.StartupPath + "\\" + txtfilename.Text);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(flname);
                    string txt;
                    string str1;
                    str1 = "";

                    //bool[] ary = new bool[dgvrent.Items.Count - 1];

                    //for (int i = 0; i < lstgridcols.Items.Count - 1; i++)
                    //{
                    //    ary[i] = lstgridcols.GetItemChecked(i);


                    //}

                    DataGridView dg;
                    dg = dgv1;
                    txt = "";
                    switch (tabsearch.SelectedTab.Name)
                    {
                        case "tabVehicle":
                            dg = dgv1;
                            break;
                        case "Tabrent":
                            dg = dgvrent;
                            break;
                        case "TabService":
                            dg = dgvservice;
                            break;
                    }


                    for (int r = 0; r < dg.Rows.Count - 1; r++)
                    {
                        str1 = "";
                        if (r == 0)
                        {

                            for (int i = 0; i < dg.ColumnCount; i++)
                            {
                                txt = dg.Columns[i].HeaderText;
                                str1 = str1 + txt + ",";
                            }
                            sw.WriteLine(str1);
                        }
                        str1 = "";
                        if (dg[0, r].Value != null && dg[0, r].Value != "")
                        {

                            for (int i = 0; i < dg.ColumnCount; i++)
                            {

                                if (dg[i, r].Value != null)
                                    txt = dg[i, r].Value.ToString();
                                else
                                    txt = "";
                                str1 = str1 + txt + ",";
                            }


                          


                            sw.WriteLine(str1);
                        }
                    }



                    sw.Close();
                    //returns a string for the directory




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }








            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }     
   
}
