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
using System.Drawing.Imaging;

namespace FinOrg
{
    public partial class FrmHRMS : Form
    {

        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dvemp = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlDataReader rd;
        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter aditem = new SqlDataAdapter();

        int dblclk_row;
        int last_row;
        int last_col;
        DataTable itemdt = new DataTable();
        bool isini;
        bool isedit;
        string sql;
        bool fnd;
        int first_grdrow;
        int last_grdrow;
        bool iserror;
        bool issearch;
        int cont_currow = 0;
        string acntrl = "";
        public FrmHRMS()
        {

            InitializeComponent();
        }

        private void FrmHRMS_Load(object sender, EventArgs e)
        {

            try
            {
                sql = "sELECT  * from Hv_Empinfo order by 1";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

                aditem = new SqlDataAdapter(cmd);

                itemdt = new DataTable("empinfo");
                aditem.Fill(itemdt);
                //grditem.Visible = true;
                dvemp.Table = itemdt;

                dgv1.DataSource = dvemp;
                dgv1.Columns[1].Width = 300;
                dgv1.Columns[0].Width = 175;
                txtpriv.Text = Gvar.frm_priv;

                Load_grid();
                Load_grid_contract();
                grdbutton.Rows.Add(1);
                txttrn_type.Text = "203";
            }
            catch (Exception ex)
            {

            }

        }

        private void Load_grid()
        {
            try
            {
                Conn.Close();
                Conn.Open();



                string sql = "";

               
                   txtismanual.Text = "0";
                

                sql = "select ismanual,employees_acc from acc_type inner join ac_options on acc_type_code=emp_ac_type";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader rd = cmd.ExecuteReader();
                cmd = new SqlCommand(sql, Conn);

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            if ( rd[0].ToString()=="True")
                                txtismanual.Text = "1";
                            else
                                txtismanual.Text = "0";

                            txtglacno.Text = rd[1].ToString();


                        }
                    }
                }
                rd.Close();
                cmd.Cancel();
                sql = "sELECT *  from form_caption  where form_code=3 and  flag <> 'X' order by Order_by";

               
                //SqlDataReader rd = cmd.ExecuteReader();
                cmd = new SqlCommand(sql, Conn);

                

                
                grdmain.Rows.Clear();


                 rd = cmd.ExecuteReader();
                System.Drawing.Image image1;
                int i = 0;
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            grdmain.Rows.Add();


                            if (Convert.ToBoolean(rd["Lookup"]))
                            {
                                grdmain[3, i].Style.BackColor = Color.Red;

                                grdmain["Lookupsql", i].Value = rd["lookup_sql"].ToString();
                                //image1 = Image.FromFile(
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\lookup.jpg");
                                //grdmain[3, i].Value = image1;
                                grdmain.Rows[i].Cells[3].Value = image1;
                                if (rd["default_val"].ToString() != "")
                                    if (rd["default_val"].ToString().Contains("="))
                                    {
                                        string[] ary = rd["default_val"].ToString().Split('=');
                                        grdmain["colvalue", i].Value = ary[0].ToString();
                                        grdmain["fieldval", i].Value = ary[1].ToString();

                                    }
                                    else
                                    {
                                        grdmain["colvalue", i].Value = rd["default_val"].ToString();
                                        grdmain["fieldval", i].Value = rd["default_val"].ToString();
                                    }

                            }
                            else
                            {
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\white.jpg");
                                grdmain.Rows[i].Cells[3].Value = image1;
                                grdmain["Lookupsql", i].Value = "";
                            }
                            if (Convert.ToBoolean(rd["Is_Mandatory"]))
                                grdmain[1, i].Value = "*";

                            grdmain["fieldname", i].Value = rd["field_name"].ToString();
                            grdmain["tablename", i].Value = rd["table_name"].ToString();
                            grdmain["textval", i].Value = rd["text_val"].ToString();
                            grdmain.Rows[i].HeaderCell.Value = rd["field_name"].ToString();

                            if (rd["default_val"].ToString() != "")
                            {
                                if (rd["default_val"].ToString().Contains("="))
                                {
                                    string[] ary = rd["default_val"].ToString().Split('=');
                                    grdmain["colvalue", i].Value = ary[0].ToString();
                                    grdmain["fieldval", i].Value = ary[1].ToString();

                                }
                                else
                                {
                                    grdmain["colvalue", i].Value = rd["default_val"].ToString();
                                    grdmain["fieldval", i].Value = rd["default_val"].ToString();
                                }
                            }
                            grdmain["flag", i].Value = rd["flag"].ToString();


                            if (rd["flag"].ToString() == "R")
                            {
                                grdmain.Rows[i].ReadOnly = true;
                                //grdmain.Rows[i].Visible = false;
                                for (int c = 1; c < grdmain.ColumnCount; c++)
                                {
                                    grdmain[c, i].Style.BackColor = Color.LightGray;
                                    // grdmain[c, i ].Style.ForeColor = Color.Beige;
                                }

                            }
                            grdmain[0, i].Value = rd[0].ToString();
                            grdmain["remarks", i].Value = rd["remarks"].ToString();
                            i++;
                            if (rd["rlposition"].ToString() == "H")
                            {
                                MergeCellsInRow(i - 1, 1, 2);
                                for (int c = 0; c < grdmain.ColumnCount; c++)
                                {
                                    grdmain[c, i - 1].Style.BackColor = Color.Honeydew;
                                    grdmain[c, i - 1].Style.ForeColor = Color.Red;
                                }
                            }
                            if (rd["flag"].ToString() == "H")
                            {
                                grdmain.Rows[i - 1].Visible = false;


                            }
                            
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Load_grid_contract()
        {
            try
            {
                Conn.Close();
                Conn.Open();



                string sql = "";


                sql = "sELECT *  from form_caption  where form_code=5 and  flag <> 'X' order by Order_by";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();

                grdcontract.Rows.Clear();

                txtcontractid.Text = "";

                SqlDataReader rd = cmd.ExecuteReader();
                System.Drawing.Image image1;
                int i = 0;
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            grdcontract.Rows.Add();


                            if (Convert.ToBoolean(rd["Lookup"]))
                            {
                                grdcontract[3, i].Style.BackColor = Color.Red;

                                grdcontract["Lookupsql1", i].Value = rd["lookup_sql"].ToString();
                                //image1 = Image.FromFile(
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\lookup.jpg");
                                //grdcontract[3, i].Value = image1;
                                grdcontract.Rows[i].Cells[3].Value = image1;
                                if (rd["default_val"].ToString() != "")
                                    if (rd["default_val"].ToString().Contains("="))
                                    {
                                        string[] ary = rd["default_val"].ToString().Split('=');
                                        grdcontract["colvalue1", i].Value = ary[0].ToString();
                                        grdcontract["fieldval1", i].Value = ary[1].ToString();

                                    }
                                    else
                                    {
                                        grdcontract["colvalue1", i].Value = rd["default_val"].ToString();
                                        grdcontract["fieldval1", i].Value = rd["default_val"].ToString();
                                    }

                            }
                            else
                            {
                                image1 = Image.FromFile(Application.StartupPath + "\\Images\\white.jpg");
                                grdcontract.Rows[i].Cells[3].Value = image1;
                                grdcontract["Lookupsql1", i].Value = "";
                            }
                            if (Convert.ToBoolean(rd["Is_Mandatory"]))
                                grdcontract[1, i].Value = "*";

                            grdcontract["fieldname1", i].Value = rd["field_name"].ToString();
                            grdcontract["tablename1", i].Value = rd["table_name"].ToString();
                            grdcontract["textval1", i].Value = rd["text_val"].ToString();
                            grdcontract.Rows[i].HeaderCell.Value = rd["field_name"].ToString();

                            if (rd["default_val"].ToString() != "")
                            {
                                if (rd["default_val"].ToString().Contains("="))
                                {
                                    string[] ary = rd["default_val"].ToString().Split('=');
                                    grdcontract["colvalue1", i].Value = ary[0].ToString();
                                    grdcontract["fieldval1", i].Value = ary[1].ToString();

                                }
                                else
                                {
                                    grdcontract["colvalue1", i].Value = rd["default_val"].ToString();
                                    grdcontract["fieldval1", i].Value = rd["default_val"].ToString();
                                }
                            }
                            grdcontract["flag1", i].Value = rd["flag"].ToString();


                            if (rd["flag"].ToString() == "R")
                            {
                                grdcontract.Rows[i].ReadOnly = true;
                                //grdcontract.Rows[i].Visible = false;
                                for (int c = 1; c < grdcontract.ColumnCount; c++)
                                {
                                    grdcontract[c, i].Style.BackColor = Color.LightGray;
                                    // grdcontract[c, i ].Style.ForeColor = Color.Beige;
                                }

                            }
                            grdcontract[0, i].Value = rd[0].ToString();
                            grdcontract["remarks1", i].Value = rd["remarks"].ToString();
                            i++;


                            if (rd["rlposition"].ToString() == "H")
                            {
                                MergeCellsInRow(i - 1, 1, 2);
                                for (int c = 0; c < grdcontract.ColumnCount; c++)
                                {
                                    grdcontract[c, i - 1].Style.BackColor = Color.Honeydew;
                                    grdcontract[c, i - 1].Style.ForeColor = Color.Red;
                                }
                            }
                            if (rd["flag"].ToString() == "H")
                            {
                                grdcontract.Rows[i - 1].Visible = false;


                            }
                           

                        }
                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void merge()
        {

            //  {
            //If Header item

            //If (e.Item.ItemType = = ListItemType.Header)
            //{
            //    e.Item.Cells.RemoveAt(2);
            //    e.Item.Cells(1).ColumnSpan = 2;
            //    //Insert the table shown in the diagram 3
            //    // to the Text property of the Cell
            //    e.Item.Cells(1).Text = "<table style='FONT-WEIGHT: bold; WIDTH:" + 
            //          " 100%; COLOR: white; TEXT-ALIGN: center'><tr align" + 
            //          " =center><td colspan = 2 style='BORDER-BOTTOM:" + 
            //          " cccccc 1pt solid'>Name</td></tr>" + 
            //          "<tr align =center ><td style ='BORDER-RIGHT:" + 
            //          " cccccc 1pt solid'>F Name</td><td>L" + 
            //          " Name</td></tr></table>";
            //}  


            //            public class JobTreeNode : TreeNode {

            //    private int intField1;

            //    public int Field1 {
            //        get {
            //            return intField1;
            //        }
            //        set {
            //            intField1 = value;
            //        }
            //    }
            //}
            //Usage (added after comments)

            //// Add the node
            //JobTreeNode CustomNode = new JobTreeNode();
            //CustomNode.Text = "Test";
            //CustomNode.Field1 = 10
            //treeView1.Nodes.add(CustomNode);


            //// SelectedNode 
            //((CustomNode)(treeView1.SelectedNode)).Field1;

        }


        private void MergeCellsInColumn(int col, int row1, int row2)
        {
            Graphics g = grdmain.CreateGraphics();
            Pen p = new Pen(grdmain.GridColor);
            Rectangle r1 = grdmain.GetCellDisplayRectangle(col, row1, true);
            Rectangle r2 = grdmain.GetCellDisplayRectangle(col, row2, true);
            int recHeight = 0;
            string recValue = string.Empty;
            for (int i = row1; i <= row2; i++)
            {
                recHeight += grdmain.GetCellDisplayRectangle(col, i, true).Height;
                if (grdmain[col, i].Value != null)
                    recValue += grdmain[col, i].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, r1.Width, recHeight);
            g.FillRectangle(new SolidBrush(grdmain.DefaultCellStyle.BackColor), newCell); g.DrawRectangle(p, newCell); g.DrawString(recValue, grdmain.DefaultCellStyle.Font, new SolidBrush(grdmain.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }

        private void MergeCellsInRow(int row, int col1, int col2)
        {
            Graphics g = grdmain.CreateGraphics();
            Pen p = new Pen(grdmain.GridColor);
            Rectangle r1 = grdmain.GetCellDisplayRectangle(col1, row, true);
            Rectangle r2 = grdmain.GetCellDisplayRectangle(col2, row, true);
            int recWidth = 0; string recValue = string.Empty;
            for (int i = col1; i <= col2; i++)
            {
                recWidth += grdmain.GetCellDisplayRectangle(i, row, true).Width;
                if (grdmain[i, row].Value != null)
                    recValue += grdmain[i, row].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, recWidth, r1.Height);
            g.FillRectangle(new SolidBrush(grdmain.DefaultCellStyle.BackColor), newCell);
            g.DrawRectangle(p, newCell);
            g.DrawString(recValue, grdmain.DefaultCellStyle.Font, new SolidBrush(grdmain.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }

        private void poplookup(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                grdlookup.DataSource = null;
                dblclk_row = grd.CurrentCell.RowIndex;

                Conn.Close();
                Conn.Open();
                string sql = grd["Lookupsql", e.RowIndex].Value.ToString();
                if (sql.Contains("?up"))
                {
                    string up = "'" + grd["fieldval", e.RowIndex - 1].Value.ToString() + "'";
                    sql = sql.Replace("?up", up);
                }

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,W.stock AS WStock,S.STOCK AS TStock from hd_itemmaster  left join wr_stock_master as w on (hd_itemmaster.Item_Code=w.Item_Code) left join stock_master as s on (hd_itemmaster.Item_Code=s.Item_Code)", Conn);
                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Lookupsql");
                adalkp.Fill(dtlkp);
                // last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                var cellRectangle = grd.GetCellDisplayRectangle(grd.CurrentCell.ColumnIndex, grd.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                grdlookup.Parent = grd;
                grdlookup.Tag = "Item_Code";
                dv.Table = dtlkp;
                grdlookup.DataSource = dv;
                grdlookup.Width = grd.Columns["colvalue"].Width + grd.Columns["collookup"].Width;
                grdlookup.Columns[0].Width = 150;
                grdlookup.Columns[1].Width = 300;
                grdlookup.Refresh();
                grdlookup.Left = cellRectangle.Left;
                grdlookup.Top = cellRectangle.Top + grd.Rows[0].Height;
                int a = grdlookup.Top + grdlookup.Height;

                if (a > grdmain.Height)
                {
                    grdlookup.Top = cellRectangle.Top - (grdlookup.Height);
                }

                object txt = txtgrd.Text.ToString();
                if (txt != "")
                {
                    dv.RowFilter = "Code LIKE  '%" + txt + "%' OR desc1 LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Code <> '0'";


                grdlookup.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void poplookupcontract(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;
                grdlookup.DataSource = null;
                dblclk_row = grd.CurrentCell.RowIndex;

                Conn.Close();
                Conn.Open();
                string sql = grd["Lookupsql1", e.RowIndex].Value.ToString();
                if (sql.Contains("?up"))
                {
                    string up = "'" + grd["fieldval1", e.RowIndex - 1].Value.ToString() + "'";
                    sql = sql.Replace("?up", up);
                }

                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlCommand cmd = new SqlCommand("select hd_itemmaster.Item_Code,hd_itemmaster.Description,W.stock AS WStock,S.STOCK AS TStock from hd_itemmaster  left join wr_stock_master as w on (hd_itemmaster.Item_Code=w.Item_Code) left join stock_master as s on (hd_itemmaster.Item_Code=s.Item_Code)", Conn);
                SqlDataAdapter adalkp = new SqlDataAdapter(cmd);


                DataTable dtlkp = new DataTable("Lookupsql");
                adalkp.Fill(dtlkp);
                // last_col = dgv1.Columns[dgv1.CurrentCell.ColumnIndex].Name;
                var cellRectangle = grd.GetCellDisplayRectangle(grd.CurrentCell.ColumnIndex, grd.CurrentCell.RowIndex, true);
                // dataGrid1.DataContext = dt.DefaultView;
                //GrdLookup.Left = dgv1.Left + dgv1.CurrentCell.ContentBounds.Left;
                //GrdLookup.Top = dgv1.Top + dgv1.CurrentCell.Size.Height + cellRectangle.Top;
                grdlookup.Parent = grd;
                grdlookup.Tag = "Item_Code";
                dv.Table = dtlkp;
                grdlookup.DataSource = dv;
                grdlookup.Width = grd.Columns["colvalue1"].Width + grd.Columns["collookup1"].Width;
                grdlookup.Columns[0].Width = 150;
                grdlookup.Columns[1].Width = 300;
                grdlookup.Refresh();
                grdlookup.Left = cellRectangle.Left;
                grdlookup.Top = cellRectangle.Top + grd.Rows[0].Height;
                int a = grdlookup.Top + grdlookup.Height;

                if (a > grdmain.Height)
                {
                    grdlookup.Top = cellRectangle.Top - (grdlookup.Height);
                }

                object txt = txtgrd.Text.ToString();
                if (txt != "")
                {
                    dv.RowFilter = "Code LIKE  '%" + txt + "%' OR desc1 LIKE '%" + txt + "%'";
                }
                else
                    dv.RowFilter = "Code <> '0'";


                grdlookup.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void Select_grid()
        {
            try
            {
                switch ("1")
                {
                    case "id":
                        {


                        }


                        break;
                    case "sno":
                        {



                        }

                        break;
                    case "rno":
                        {


                        }

                        break;


                }
            }

            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);

            }
        }

        public void ClearTextBoxes(Control parent)
        {
            object trntype = 0;
            foreach (Control child in parent.Controls)
            {
                TextBox textBox = child as TextBox;
                if (textBox == null)
                    ClearTextBoxes(child);
                else
                    if (textBox.Name == "txtpriv")
                    {
                    }

                    else
                        if (textBox.Name != "textBox1" && textBox.Name != "txtempno")
                            textBox.Text = string.Empty;
            }

            //lblrefund.Text = "";


        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            issearch = true;

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            issearch = true;
            ini_form();
            textBox1.Focus();
        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {

            issearch = false;
            //if (textBox1.Text.Trim() != "")
            //{
            //    search_data(textBox1.Text.Trim());

            //}
            //else
            //{
            //if (grditem.CurrentCell != null)
            //{

            //    int row = grditem.CurrentCell.RowIndex;
            //    if (!issearch && row >= 0) search_data(grditem["Item_Code", row].Value.ToString());
            //}

            if (txtempno.Text.Trim() != "")
                search_all();
            //}

        }


        private void tooldelete_Click(object sender, EventArgs e)
        {
            delete_Item();
            ini_form();
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();

            this.Close();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            print_Report(1);
        }

        private void print_Report(int idx)
        {
            string crt;
            string rep_path;
            fnd = false;
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
                            rep_path = Gvar.report_path + "\\reports\\HRMS\\Rptempdet.rpt";
                            CrRep.Load(rep_path);
                            CrRep.SummaryInfo.ReportTitle = "Employee Record";
                            //CrRep.SummaryInfo.ReportTitle = "Item Stock Report for all";
                            crt = "{tbl_EmpDetails.EMP_Id} = " + txtempno.Text;
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
                CrRep.OpenSubreport("HEADER.rpt").DataDefinition.FormulaFields["RPTHEAD"].Text = "'" + CrRep.SummaryInfo.ReportTitle + "'";

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
        private void search_Employee(string empid)
        {
            try
            {
                try
                {

                    ini_form();
                    //txtempno.Text = empid;
                    Conn.Close();
                    Conn.Open();
                    //textBox1.Text = Item_Code;
                    saveToolStripButton.Enabled = true;


                    sql = "sELECT * from  Emp_Fulldet where emp_id='" + empid + "'";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();

                    rd = cmd.ExecuteReader();



                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            if (!string.IsNullOrEmpty(rd["emp_id"].ToString()))
                            {

                                txtempno.Text = rd["emp_id"].ToString();
                                txtaccno.Text = txtempno.Text;
                                txtoldempno.Text = rd["emp_id"].ToString();

                                txtempname.Text = rd["Fullname"].ToString();


                                DataRowView drw = null;

                                //cmbcat.SelectedValue = ctcode;






                                if (!string.IsNullOrEmpty(rd["emp_id"].ToString()))
                                {


                                    for (int i = 0; i <= grdmain.Rows.Count - 1; i++)
                                    {
                                        int a = grdmain["tablename", i].Value.ToString().IndexOf("Tbl_Empdetails");
                                        if (grdmain["tablename", i].Value.ToString() != "" && grdmain["fieldname", i].Value.ToString() != "")
                                        {
                                            if (grdmain["fieldname", i].Value != null)
                                            {


                                                // rec.Fields[grdmain["fieldname", i].Value.ToString()].Value =
                                                grdmain["fieldval", i].Value = rd[grdmain["fieldname", i].Value.ToString()].ToString();
                                                grdmain["colvalue", i].Value = rd[grdmain["fieldname", i].Value.ToString()].ToString();


                                                if (grdmain["textval", i].Value.ToString() == "1")
                                                {
                                                    grdmain["colvalue", i].Value = rd[grdmain["fieldname", i].Value.ToString() + "Name"].ToString();
                                                }

                                            }

                                        }

                                    }



                                    isedit = true;
                                }



                            }




                            isedit = true;
                            //txtempno.Focus();
                        }

                    }
                    else
                    {
                        rd.Close();
                        return;
                    }




                    search_foto();
                    rd.Close();
                    Conn.Close();
                    // grdmain.CurrentCell = grdmain[2, 5];
                    grdmain.CurrentCell = grdmain[2, first_grdrow];

                    isini = false;
                    grdmain.ClearSelection();

                }
                //}

                catch (System.Data.SqlClient.SqlException excep)
                {

                    MessageBox.Show(excep.Message);


                }
            }
            catch (Exception ex)
            {

            }
        }
        private void search_foto()
        {
            try
            {
                if (string.IsNullOrEmpty(txtempno.Text)) return;



                SqlCommand cmd1;

                System.IO.MemoryStream ms;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConImage"].ConnectionString);

                con.Open();


                sql = "SELECT *  from emp_image where emp_id='" + txtempno.Text.Trim() + "'";



                cmd1 = new SqlCommand(sql, con);
                SqlDataReader rd1 = cmd1.ExecuteReader();


                while (rd1.Read())
                {
                    if (rd1["photo"] != DBNull.Value)
                    {
                        showdata((byte[])rd1["photo"]);

                    }

                }
                rd1.Close();

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void showdata(byte[] photo_aray)
        {


            pictureBox1.Image = null;

            // photo_aray = (byte[])ds.Tables[0].Rows[rno][4];
            System.IO.MemoryStream ms = new System.IO.MemoryStream(photo_aray);
            pictureBox1.Image = Image.FromStream(ms);


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isini) return;
            string txt = textBox1.Text.Trim();
            if (txt != "")
            {
                isini = true;


                dvemp.RowFilter = "Code LIKE  '%" + txt + "%' OR Fullname LIKE '%" + txt + "%'";
                isini = false;
                // if (!issearch && dv.Count >0 ) search_data(grditem["Item_Code", 0].Value.ToString());
            }
            else
                dvemp.RowFilter = "Code <> '0'";


            isini = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtempno_Validated(object sender, EventArgs e)
        {
            //if (txtempno.Text.Length==16)

            //if (txtempno.Text.Trim() != "" && txtoldempno.Text != txtempno.Text)
            //    search_Employee(txtempno.Text);
        }
        private void FrmHRMS_KeyDown(object sender, KeyEventArgs e)
        {
            {
                switch (e.KeyData & Keys.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                    case Keys.Left:

                        if (grdlookup.Visible)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        break;
                    case Keys.Escape:
                        grdlookup.Visible = false;
                        if (Tabmain.TabIndex == 5)
                        {
                            textBox1.Text = "";
                            textBox1.Focus();

                        }
                        break;
                    case Keys.Tab:
                        // grditem.Visible = false;
                        break;

                }
            }
        }
        private void grdbutton_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdmain_Enter(object sender, EventArgs e)
        {
            acntrl = "grdmain";
            try
            {
                grdmain.CurrentCell = grdmain[2, 0];
                grdmain.CurrentCell = grdmain[2, 5];
                grdmain.CurrentCell = grdmain[2, 0];
            }
            catch
            { }
            grdmain.BeginEdit(true);
        }

        private void grdmain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Tab:

                    break;
                case Keys.Escape:

                    break;
                case Keys.Enter:
                    int i;
                    for (i = grdmain.CurrentCell.RowIndex + 1; i < grdmain.Rows.Count - 1; i++)
                    {
                        if (i == grdmain.Rows.Count) break;
                        if (grdmain[0, i].Visible) break;
                    }
                    if (i == grdmain.Rows.Count - 1)
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);



                    break;

            }

            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:

                    if (grdlookup.Visible)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        private void delete_Item()
        {

            try
            {

                double val1;

                if (double.TryParse(txtempno.Text, out val1))
                {
                    if (Convert.ToDouble(txtempno.Text) < 1)
                    {
                        txtempno.Text = "";
                        MessageBox.Show("Invalid Employee Number, PLease Try Again", "Plese Enter Correct Value");
                        Conn.Close();
                        return;
                    }
                }
                else
                {
                    txtempno.Text = "";
                    MessageBox.Show("Invalid Employee Number, Please Try Again", "Plese Enter Correct Value");
                    Conn.Close();
                    return;

                }

                DialogResult result = MessageBox.Show("Do You want to Delete This Employee  " + txtempname.Text + "?", "Delete Employee", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    Conn.Close();
                    Conn.Open();


                    sql = "delete from   dbo.HV_docs  where emp_id ='" + txtoldempno.Text + "';";
                    sql = "delete from   dbo.HV_DependentDetails  where emp_id ='" + txtoldempno.Text + "';";
                    sql = "delete from   dbo.HV_ContractDetails  where emp_id ='" + txtoldempno.Text + "';";
                    sql = "delete from   dbo.EMP_DETAIL  where emp_id ='" + txtoldempno.Text + "';";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();



                    //cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Employee  Completed Successfully!!");


                }


            }
            catch (System.Data.SqlClient.SqlException excep)
            {

                MessageBox.Show(excep.Message);


            }
        }
        private void ini_form()
        {

            //if (cmbcat.Items.Count > 0) cmbcat.SelectedIndex = 0;



            ClearTextBoxes(this);
            textBox1.Text = "";

            dt1.Value = DateTime.Now;

            Gvar.ArCalendar(dt1.Value);
            saveToolStripButton.Enabled = true;
            if (Gvar._SuperUserid != 1)
            {

                tooldelete.Enabled = false;

            }


            foreach (DataGridViewRow row1 in this.grdmain.Rows)
            {
                //foreach (DataGridViewCell cell in row.Cells)

                row1.Cells["colvalue"].Value = "";
                row1.Cells["fieldval"].Value = "";
            }



            isedit = false;
            isini = true;
            pictureBox1.Image = null;

            isini = false;


        }
        private void grdbutton_Enter(object sender, EventArgs e)
        {
            grdbutton.CurrentCell = grdbutton[0, 0];
        }

        private bool ProcessCmdKey2(ref Message msg, Keys keyData)
        // protected override bool ProcessCmdKey2(ref Message msg, Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                // SendKeys.Send("{Tab}");

                switch (acntrl)
                {
                    case "grdmain":


                    case "grdbutton":
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
                var txt = "";


                if (grdlookup.Visible)
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
                    txt = txtgrd.Text;
                    // grdmain.BeginEdit(true);

                }
                switch (keyData & Keys.KeyCode)
                {
                    case Keys.Up:
                        //case Keys.Right:
                        if (grdlookup.Visible)
                        {
                            if (grdlookup.CurrentCell.RowIndex != 0)
                                grdlookup.CurrentCell = grdlookup[0, grdlookup.CurrentCell.RowIndex - 1];

                            return true;


                        }
                        break;
                    case Keys.Down:
                        //case Keys.Left:

                        if (grdlookup.Visible)
                        {
                            if (grdlookup.Rows.Count - 1 != grdlookup.CurrentCell.RowIndex)
                                grdlookup.CurrentCell = grdlookup[0, grdlookup.CurrentCell.RowIndex + 1];
                            return true;


                        }
                        break;

                    case Keys.Escape:
                        {
                            grdlookup.Visible = false;
                            break;

                        }
                        break;
                    case Keys.Enter:
                        {
                            if (grdlookup.Visible && grdlookup.Rows.Count > 0 && acntrl == "grdmain")
                            {
                                grdmain.EndEdit();
                                grdmain["fieldval", grdmain.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                                grdmain["colvalue", grdmain.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;
                                if (grdmain["textval", grdmain.CurrentCell.RowIndex].Value == "0")
                                {
                                    grdmain["fieldval", grdmain.CurrentCell.RowIndex].Value = grdmain["colvalue", grdmain.CurrentCell.RowIndex].Value;
                                }


                                grdlookup.Visible = false;
                            }
                            if (grdlookup.Visible && grdlookup.Rows.Count > 0 && acntrl == "grdcontract")
                            {
                                grdcontract.EndEdit();
                                grdcontract["fieldval1", grdcontract.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                                grdcontract["colvalue1", grdcontract.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;
                                if (grdcontract["textval1", grdcontract.CurrentCell.RowIndex].Value.ToString() == "0")
                                {
                                    grdcontract["fieldval1", grdcontract.CurrentCell.RowIndex].Value = grdcontract["colvalue1", grdlookup.CurrentCell.RowIndex].Value;
                                }
                                grdlookup.Visible = false;
                            }
                        }
                        break;
                    default:
                        if (grdlookup.Visible)
                        {
                            //string txt = grdlookup[grdlookup.CurrentCell.ColumnIndex, grdlookup.CurrentCell.RowIndex].Value.ToString().Trim();
                            //object txt = grdmain[2, 4].EditedFormattedValue.ToString();
                            // if (txt != "")
                            //{
                            //  dv.RowFilter = "Code LIKE  '%" + txt + "%' OR desc1 LIKE '%" + txt + "%'";
                            //}
                            //else
                            //  dv.RowFilter = "Code <> '0'";


                        }
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


        private void grdmain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;

                if (grd.CurrentCell == null) return;

                if (grd["lookupsql", e.RowIndex].Value.ToString() != "")
                {
                    poplookup(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void grdmain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grd = (DataGridView)sender;

            if (grd["lookupsql", e.RowIndex].Value.ToString() == "")
            {
                grd["fieldval", e.RowIndex].Value = grd["colvalue", e.RowIndex].Value;
            }



        }

        private void grdmain_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            last_col = e.ColumnIndex;
            last_row = e.RowIndex;
            lblremarks.Text = "";
        }

        private void grdmain_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;

            tb.TextChanged += new EventHandler(tb_TextChanged);
        }
        void tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // string txt = tb_TextChanged();
                TextBox tb = (TextBox)sender;
                txtgrd.Text = tb.Text;
                //MessageBox.Show("changed");
                string txt = tb.Text;
                if (grdlookup.Visible)
                {
                    //string txt = grdlookup[grdlookup.CurrentCell.ColumnIndex, grdlookup.CurrentCell.RowIndex].Value.ToString().Trim();
                    //object txt = grdmain[2, 4].EditedFormattedValue.ToString();
                    if (txt != "")
                    {
                        dv.RowFilter = "Code LIKE  '%" + txt + "%' OR desc1 LIKE '%" + txt + "%'";
                    }
                    else
                        dv.RowFilter = "Code <> '0'";


                }
            }
            catch
            {

            }

        }
        private void grdmain_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                DataGridView grd = (DataGridView)sender;
                int addrow = 0;


                if (grd.CurrentCell == null) return;
                if (last_row == grd.CurrentCell.RowIndex) return;
                //DataGridCell cel = grdmain.CurrentCell;
                if (last_row > grd.CurrentCell.RowIndex)
                    addrow = -1;
                else
                    addrow = 1;
            chkread:

                if (grd["flag", grd.CurrentCell.RowIndex].Value.ToString() == "R")
                {
                    if (grd.CurrentCell.RowIndex < grd.Rows.Count - 1 && grd.CurrentCell.RowIndex > 0)
                    //this.BeginInvoke(new MethodInvoker(grd_CellEnter(sender,e)));
                    {
                        grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex + addrow];
                        goto chkread;
                    }
                    else
                    {
                        addrow = addrow * -1;
                        grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex + addrow];
                        goto chkread;
                    }
                    //  grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex - addrow];

                }
                if (grd.CurrentCell.ColumnIndex != grd["colvalue", grd.CurrentCell.RowIndex].ColumnIndex)
                {
                    grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void grdcontract_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;

                if (grd.CurrentCell == null) return;

                if (grd["lookupsql1", e.RowIndex].Value.ToString() != "")
                {
                    poplookupcontract(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void grdcontract_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            last_col = e.ColumnIndex;
            last_row = e.RowIndex;
            lblremarks.Text = "";
        }

        private void grdcontract_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = (TextBox)e.Control;

            tb.TextChanged += new EventHandler(tb_TextChanged);
        }

        private void grdcontract_Enter(object sender, EventArgs e)
        {
            acntrl = "grdcontract";
            try
            {
                grdcontract.CurrentCell = grdcontract[2, 0];
                grdcontract.CurrentCell = grdcontract[2, 5];
                grdcontract.CurrentCell = grdcontract[2, 0];
            }
            catch
            { }
            grdcontract.BeginEdit(true);
        }

        private void grdcontract_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Tab:

                    break;
                case Keys.Escape:

                    break;
                case Keys.Enter:
                    int i;
                    for (i = grdcontract.CurrentCell.RowIndex + 1; i < grdcontract.Rows.Count - 1; i++)
                    {
                        if (i == grdcontract.Rows.Count) break;
                        if (grdcontract[0, i].Visible) break;
                    }
                    if (i == grdcontract.Rows.Count - 1)
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);



                    break;

            }

            switch (e.KeyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                case Keys.Left:

                    if (grdlookup.Visible)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        private void grdcontract_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                DataGridView grd = (DataGridView)sender;
                int addrow = 0;


                if (grd.CurrentCell == null) return;
                if (last_row == grd.CurrentCell.RowIndex) return;
                //DataGridCell cel = grdmain.CurrentCell;
                if (last_row > grd.CurrentCell.RowIndex)
                    addrow = -1;
                else
                    addrow = 1;
            chkread:
                object flg = "R";
            if (grd["flag1", grd.CurrentCell.RowIndex].Value == null) grd["flag1", grd.CurrentCell.RowIndex].Value = "";
                if (grd["flag1", grd.CurrentCell.RowIndex].Value.ToString() == "R")
                {
                    if (grd.CurrentCell.RowIndex < grd.Rows.Count - 1 && grd.CurrentCell.RowIndex > 0)
                    //this.BeginInvoke(new MethodInvoker(grd_CellEnter(sender,e)));
                    {
                        grd.CurrentCell = grd["colvalue1", grd.CurrentCell.RowIndex + addrow];
                        goto chkread;
                    }
                    else
                    {
                        addrow = addrow * -1;
                        grd.CurrentCell = grd["colvalue1", grd.CurrentCell.RowIndex + addrow];
                        goto chkread;
                    }
                    //  grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex - addrow];

                }
                if (grd.CurrentCell.ColumnIndex != grd["colvalue1", grd.CurrentCell.RowIndex].ColumnIndex)
                {
                    grd.CurrentCell = grd["colvalue1", grd.CurrentCell.RowIndex];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if(Tabmain.SelectedTab.Text!="Personal Info")
            {
                MessageBox.Show("Please Open The Personal Info Tab To save the Employee Master Information", "Wrong Tab Selection");
                return;

            }
            save_data();
        }

        private void save_data()
        {
            bool isbegintran = false;
            try
            {
                //if (Txtitem.Text.Trim().Length != 16)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 16 Digit!!");
                //    return;
                //}

                Conn.Close();
                Conn.Open();

                bool isempty;


                isempty = false;
                // foreach (Control tx in this.Controls)
                //foreach (TextBox tx in this.Controls.OfType<TextBox>())
                //{
                //    //if (tx.GetType == System.Windows.Forms.TextBox) 
                //   //{
                //        if (tx.Tag == "1")
                //        {

                //            tx.BackColor = System.Drawing.Color.White;
                //            if (string.IsNullOrEmpty(tx.Text.Trim()))
                //            {
                //                tx.BackColor = System.Drawing.Color.Yellow;
                //                isempty = true;
                //            }
                //        }

                //   //}
                //}

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

                for (int i = 0; i < grdmain.Rows.Count;i++ )
                {
                    if (grdmain["ismandatory", i].Value == "*" && grdmain["fieldval", i].Value==""   )
                    {
                        MessageBox.Show( grdmain["caption", i].Value + " is Not Allowed Blank, Please Enter a Valid value!!", "Invalid Entry");
                          return;
                    }
                }


                    //foreach (Control gb in this.Controls)
                    //{
                    //    if (gb is GroupBox)
                    //    {
                    //        foreach (Control tb in gb.Controls)
                    //        {
                    //            if (tb is TextBox)
                    //            {
                    //                if (tb.Tag == "1")
                    //                {

                    //                    tb.BackColor = System.Drawing.Color.White;
                    //                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                    //                    {
                    //                        tb.BackColor = System.Drawing.Color.Yellow;
                    //                        isempty = true;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}



                    //if (isempty)
                    //{
                    //    MessageBox.Show("Entry Not Completed, Please fill all Yellow Marked fileds!!", "Invalid Entry");
                    //    return;

                    //}



                    if (isedit == false)
                    {


                        sql = "SELECT emp_id FROM emp_detail where emp_id ='" + txtempno.Text.Trim() + "'";

                        cmd = new SqlCommand(sql, Conn);

                        rd = cmd.ExecuteReader();



                        if (rd.HasRows)
                        {


                            DialogResult result = MessageBox.Show("This Employee Already Existing!!, Do You want to Update?", "Item Found", MessageBoxButtons.YesNoCancel);

                            if (result == DialogResult.Yes) isedit = true; else return;

                        }
                    }


                if (string.IsNullOrEmpty(txtempname.Text.ToString()))
                {
                    MessageBox.Show("Invalid Employee Name", "Wrong Employee Info");

                    return;
                }



                Conn.Close();
                Conn.Open();
                //Conn.BeginTransaction();
                fnd = false;


                //DataGridViewRow row;
                //row = grdmain.Rows
                //         .Cast<DataGridViewRow>()
                //         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                //         .First();

                ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                ADODB.Recordset rec = new ADODB.Recordset();


                if (txtempno.Text.Trim() == "") txtempno.Text = "0";
                ADODB.Recordset emp = new ADODB.Recordset();
                sql = "SELECT * FROM emp_detail where emp_id ='" + txtempno.Text.Trim() + "'";

                emp = new ADODB.Recordset();
                ADOconn.BeginTrans();
                isbegintran = true;
                try
                {
                    emp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (emp.RecordCount == 0) emp.AddNew();
                    for (int i = 0; i < grdmain.Rows.Count; i++)
                    {

                        //if (grdbarcode["saleprice1", i].Value == "") grdbarcode["saleprice1", i].Value = 0;
                        //if (grdbarcode["saleprice2", i].Value == "") grdbarcode["saleprice2", i].Value = 0;
                        //if (bcode.RecordCount == 0) bcode.AddNew();
                        if (grdmain["textval", i].Value.ToString() == "1" && grdmain["fieldval", i].Value == null)
                        {
                            grdmain["fieldval", i].Value = "0";
                        }
                        emp.Fields[grdmain["fieldname", i].Value].Value = grdmain["fieldval", i].Value;


                    }
                    emp.Fields["Fullname"].Value = txtempname.Text;
                    txtarname.Text = emp.Fields["AFullname"].Value.ToString();
                    if (emp.Fields["code"].Value == null)
                    {
                        rec = new ADODB.Recordset();
                        rec.Open("SELECT max(emp_id)  from emp_detail ", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        if (rec.Fields[0].Value == DBNull.Value)
                        {
                            emp.Fields["code"].Value = "1";

                        }
                        else
                        {
                            emp.Fields["code"].Value = Convert.ToDecimal(emp.Fields["code"].Value) + 1;
                        }
                    }

                    emp.Update();
                    if (!isedit)
                    {
                        rec = new ADODB.Recordset();
                        rec.Open("SELECT @@IDENTITY", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                        //rec.GetRows();

                        txtempno.Text = rec.Fields[0].Value.ToString();
                    }
                   txtaccno.Text = txtempno.Text;

                    create_empgl();

                    iserror = false;
                    

                    ADOconn.CommitTrans();

                    // sql = "update  [HD_ITEMMASTER]  set [Item_Code]='" + Txtitem.Text.Trim() + "',[DESCRIPTION]='" + txtempname.Text + "',[ar_desc]='" + txtarabic + "',[ITM_CAT_CODE]='" + cmbcat.SelectedValue + "',[UNIT]='" + cmbunit.SelectedValue + "',[FRACTION]='" + txtfraction.Text + "',[ALIAS_NAME]='" + txtalias + "',BARCODE ='" + txtbarcode + "'  where Item_Code ='" + txtolditm.Text + "'";


                    //  cmd = new SqlCommand(sql, Conn);

                    //    cmd.ExecuteNonQuery();


                    isedit = true;




                    Conn.Close();

                    if (pictureBox1.Image != null)
                    {
                        save_foto(isedit);
                    }

                    isini = false;



                    MessageBox.Show("Successfully Added/Updated Item", "Successfull");
                    load_dep();
                    return;


                    // MessageBox.Show("Successfully Inserted New reciept", "Successfull");
                }

                catch (System.Data.SqlClient.SqlException excep)
                {
                    if (isbegintran)
                        ADOconn.RollbackTrans();
                    MessageBox.Show(excep.Message);


                }

            }

            catch (Exception ex)
            {
                if (isbegintran)
                    ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }


        }

        private void save_foto(bool update)
        {
            try
            {
                //SqlConnection con;
                SqlCommand cmd;

                System.IO.MemoryStream ms;

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConImage"].ConnectionString);

                //con = new SqlConnection("user id=sa;password=123;database=adil");
                if (update)
                    cmd = new SqlCommand("Update  Item_Image set emp_id='" + txtempno.Text + "',photo=@photo where emp_id='" + txtoldempno.Text + "'", con);
                else
                    cmd = new SqlCommand("insert into Item_Image(emp_id,photo) values('" + txtempno.Text + "',@photo)", con);

                if (pictureBox1.Image != null)
                {
                    //using FileStream:(will not work while updating, if image is not changed)
                    //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    //byte[] photo_aray = new byte[fs.Length];
                    //fs.Read(photo_aray, 0, photo_aray.Length);  

                    //using MemoryStream:
                    ms = new System.IO.MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    cmd.Parameters.AddWithValue("@photo", photo_aray);
                }
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if (n == 0)
                {
                    cmd = new SqlCommand("insert into Emp_Image(emp_id,photo) values('" + txtempno.Text + "',@photo)", con);
                    if (pictureBox1.Image != null)
                    {

                        ms = new System.IO.MemoryStream();
                        pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] photo_aray = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(photo_aray, 0, photo_aray.Length);
                        cmd.Parameters.AddWithValue("@photo", photo_aray);
                    }
                    n = cmd.ExecuteNonQuery();

                }
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {

            }

        }

        void conv_photo()
        {
            //converting photo to binary data
            if (pictureBox1.Image != null)
            {
                //using FileStream:(will not work while updating, if image is not changed)
                //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                //byte[] photo_aray = new byte[fs.Length];
                //fs.Read(photo_aray, 0, photo_aray.Length);  

                //using MemoryStream:
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                cmd.Parameters.AddWithValue("@photo", photo_aray);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dtdob_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row;
                row = grdmain.Rows
                         .Cast<DataGridViewRow>()
                         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("DOB"))
                         .First();


                row.Cells["colvalue"].Value = dtdob.Value.Date.ToString("yyyy-MM-dd");

                row.Cells["fieldval"].Value = dtdob.Value.Date.ToString("yyyy-MM-dd");

                //if (grdcontract["Caption1", grdmain.CurrentCell.RowIndex].Value.ToString().IndexOf("Dob") > 0)
                //{
                //    dgv1["fieldval", grdmain.CurrentCell.RowIndex].Value = dtdob.Value.Date.ToString("yyyy-MM-dd");
                //    dgv1["cellval", grdmain.CurrentCell.RowIndex].Value = dtdob.Value.Date.ToString("yyyy-MM-dd");
                //}


            }
            catch (Exception ex)
            {

            }


        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpeg)|*.jpg|All Files (*.png)|*.bmp";
            openFileDialog.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtfilename.Text = openFileDialog.FileName;
                pictureBox1.Load(txtfilename.Text);
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 17:

                    break;
                case 27:
                    //dgv1.Visible = false;
                    break;
                case 13:

                    textBox1.Text = dgv1.CurrentRow.Cells["emp_id"].Value.ToString();
                    if (textBox1.Text.Trim() != "" && txtoldempno.Text != textBox1.Text)



                        search_Employee(textBox1.Text.Trim());


                    break;
                case 38:

                    int crow = dgv1.CurrentRow.Index;
                    int mros = dgv1.Rows.Count;
                    // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                    //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;
                    //if (Txtitem.Text.Trim() == "" && mros > 0)
                    //{
                    //    search_data(grditem[0, crow].Value.ToString());
                    //    return;
                    //}
                    if (crow > 0)
                        dgv1.CurrentCell = dgv1.Rows[crow - 1].Cells[0];



                    break;
                case 40:

                    crow = dgv1.CurrentRow.Index;
                    mros = dgv1.Rows.Count;
                    // this.dgv1.CurrentCell = this.dgv1[crow+1, 0];

                    //  dgv1.CurrentRow.Index = dgv1.CurrentRow.Index + 1;

                    //if (Txtitem.Text.Trim() == "" && mros > 0)
                    //{
                    //    search_data(grditem[0, crow].Value.ToString());
                    //        return;
                    //}
                    {
                        if (crow < mros - 1)
                            dgv1.CurrentCell = dgv1.Rows[crow + 1].Cells[0];
                    }

                    break;

            }
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            isini = true;
            textBox1.Text = dgv1.CurrentRow.Cells["emp_id"].Value.ToString();
            txtempno.Text = dgv1.CurrentRow.Cells["emp_id"].Value.ToString();
            txtaccno.Text = txtempno.Text;
            if (textBox1.Text.Trim() != "" && txtoldempno.Text != textBox1.Text)

                search_all();

            isini = false;
        }

        private void dgv1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {

                textBox1.Text = dgv1.CurrentRow.Cells["emp_id"].Value.ToString();
                txtempno.Text = textBox1.Text;
                txtaccno.Text = txtempno.Text;
                if (textBox1.Text.Trim() != "" && txtoldempno.Text != textBox1.Text)

                    search_all();


            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsavedep_Click(object sender, EventArgs e)
        {
            bool isbegintran = false;

            try
            {
                if (txtempno.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid Employee ID");
                    return;
                }


                if (cmbrelation.SelectedIndex < 0 || txtdepname.Text == "")
                {
                    MessageBox.Show("Inavalid/Empty  Information , Please check Entered Iformation");
                    return;
                }
                ADOconn = new ADODB.Connection();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                ADODB.Recordset rec = new ADODB.Recordset();



                if (txtdepid.Text.Trim() == "") txtdepid.Text = "0";
                ADODB.Recordset emp = new ADODB.Recordset();
                sql = "SELECT * FROM HV_DependentDetails where emp_id ='" + txtempno.Text.Trim() + "' and dep_id=" + txtdepid.Text;

                emp = new ADODB.Recordset();
                ADOconn.BeginTrans();
                isbegintran = true;

                emp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (emp.RecordCount == 0) emp.AddNew();

                emp.Fields["Emp_id"].Value = txtempno.Text;
                emp.Fields["Relation"].Value = cmbrelation.Text;
                emp.Fields["Ename"].Value = txtdepname.Text;
                emp.Fields["Aname"].Value = txtdepaname.Text;
                emp.Fields["DOB"].Value = dtdepdob.Value.Date.ToString("yyyy-MM-dd");
                emp.Update();
                ADOconn.CommitTrans();
                txtdepid.Text = "";
                MessageBox.Show("SuccessFully Saved Dependant");

                search_dependant();
                load_dep();
            }
            catch (Exception ex)
            {
                if (isbegintran)
                    ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }
        }


        private void load_dep()
        {
            try
            {
                sql = "SELECT 0 AS EMP_ID,FULLNAME FROM EMP_DETAIL WHERE EMP_ID=" + txtempno.Text + " UNION  SELECT  dep_id,ename  froM dbo.HV_DependentDetails where emp_id IN (select emp_id from HV_DependentDetails where emp_id=" + txtempno.Text + " ) ";
                if (Conn.State==0)
              
                Conn.Open();
                SqlDataAdapter ada3 = new SqlDataAdapter(sql, Conn);

                DataTable dt3 = new DataTable("dep");
                ada3.Fill(dt3);
                cmbholdername.DataSource = dt3;
                cmbholdername.DisplayMember = "FULLNAME";
                cmbholdername.ValueMember = "EMP_ID";
                cmbholdername.SelectedIndex = -1;
                cmbidtype.SelectedIndex = -1;
               
            }
            catch (Exception ex)
            {

            }
        }

        private void search_dependant()
        {
            try
            {
                try
                {
                    sql = "sELECT  * from HV_DependentDetails where emp_id=" + txtempno.Text + " order by 1";

                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();

                    SqlDataAdapter addep = new SqlDataAdapter(cmd);

                    DataTable dtdep = new DataTable("depentant");
                    addep.Fill(dtdep);
                    //grditem.Visible = true;
                    //dv.Table = itemdt;

                    dgdep.DataSource = dtdep;
                    dgdep.Columns[2].Width = 100;
                    dgdep.Columns[3].Width = 200;
                    dgdep.Columns[4].Width = 200;

                    dgdep.Columns[0].Visible = false;
                    dgdep.Columns[1].Visible = false;
                    dgdep.Columns[5].Visible = false;
                    dgdep.Columns[6].Visible = false;

                    cmbrelation.SelectedIndex = -1;
                    txtdepname.Text = "";
                    txtdepaname.Text = "";


                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgdep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtdepid.Text = dgdep["dep_id", e.RowIndex].Value.ToString();
                txtdepname.Text = dgdep["ename", e.RowIndex].Value.ToString();
                cmbrelation.Text = dgdep["relation", e.RowIndex].Value.ToString();
                txtdepaname.Text = dgdep["aname", e.RowIndex].Value.ToString();
                dtdepdob.Text = dgdep["dob", e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {

            }


        }

        private void cmdsavedocs_Click(object sender, EventArgs e)
        {

            bool isbegintran = false;

            try
            {


                ADOconn = new ADODB.Connection();
                if (ADOconn.State == 0)
                    ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                ADODB.Recordset rec = new ADODB.Recordset();

                if (cmbholdername.SelectedIndex < 0 || txtidnumber.Text == "" || cmbidtype.SelectedIndex < 0)
                {
                    MessageBox.Show("Inavalid/Empty  Information , Please check Entered Iformation");
                    return;
                }
                if (txtempno.Text.Trim() == "") txtempno.Text = "0";
                if (txtdocid.Text.Trim() == "") txtdocid.Text = "0";

                ADODB.Recordset emp = new ADODB.Recordset();
                sql = "SELECT * FROM HV_Docs where emp_id ='" + txtempno.Text.Trim() + "' and [IdentityId]=" + txtdocid.Text;

                emp = new ADODB.Recordset();
                ADOconn.BeginTrans();
                isbegintran = true;

                emp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (emp.RecordCount == 0) emp.AddNew();

                emp.Fields["Emp_id"].Value = txtempno.Text;
                emp.Fields["Type"].Value = cmbidtype.Text;
                emp.Fields["Id"].Value = txtidnumber.Text;
                emp.Fields["Issue_Date"].Value = dtidissue.Value.Date.ToString("yyyy-MM-dd");
                emp.Fields["Expiry_Date"].Value = dtidexpire.Value.Date.ToString("yyyy-MM-dd");

                emp.Fields["Issue_Place"].Value = txtidissueplace.Text;
                emp.Fields["Dep_Id"].Value = cmbholdername.SelectedValue;
                emp.Update();
                ADOconn.CommitTrans();
                txtdocid.Text = "";
                MessageBox.Show("SuccessFully Saved Document");
                search_docs();

            }
            catch (Exception ex)
            {
                if (isbegintran)
                    ADOconn.RollbackTrans();

                MessageBox.Show(ex.Message);
            }



        }


        private void search_docs()
        {
            try
            {
                try
                {
                    sql = "sELECT  * from HV_Docs where emp_id=" + txtempno.Text + " order by 1";

                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();.

                    SqlDataAdapter addoc = new SqlDataAdapter(cmd);

                    DataTable dtdoc = new DataTable("docs");
                    addoc.Fill(dtdoc);
                    //grditem.Visible = true;
                    //dv.Table = itemdt;

                    dgdocs.DataSource = dtdoc;
                    dgdocs.Columns[2].Width = 125;
                    dgdocs.Columns[3].Width = 150;
                    dgdocs.Columns[4].Width = 150;
                    dgdocs.Columns[0].Visible = false;
                    dgdocs.Columns[1].Visible = false;
                    dgdocs.Columns[7].Visible = false;
                    dgdocs.Columns[8].Visible = false;
                   
                    load_dep();

                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
            }

        }

        private void btncontract_Click(object sender, EventArgs e)
        {
            bool isbegintran = false;
            try
            {
                //if (Txtitem.Text.Trim().Length != 16)
                //{
                //    MessageBox.Show("Invalid Length of Code, Must Be 16 Digit!!");
                //    return;
                //}

                Conn.Close();
                Conn.Open();

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







                Conn.Close();
                Conn.Open();
                //Conn.BeginTransaction();
                fnd = false;


                //DataGridViewRow row;
                //row = grdcontract.Rows
                //         .Cast<DataGridViewRow>()
                //         .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("AR_DESC"))
                //         .First();

                ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                ADODB.Recordset rec = new ADODB.Recordset();


                if (txtempno.Text.Trim() == "") txtempno.Text = "0";
                if (txtcontractid.Text.Trim() == "") txtcontractid.Text = "0";
                ADODB.Recordset emp = new ADODB.Recordset();
                sql = "SELECT * FROM HV_ContractDetails where emp_id ='" + txtempno.Text.Trim() + "' AND contract_id=" + txtcontractid.Text;

                emp = new ADODB.Recordset();
                ADOconn.BeginTrans();
                isbegintran = true;
                try
                {
                    emp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    if (emp.RecordCount == 0) emp.AddNew();
                    for (int i = 0; i < grdcontract.Rows.Count; i++)
                    {

                        //if (grdbarcode["saleprice1", i].Value == "") grdbarcode["saleprice1", i].Value = 0;
                        //if (grdbarcode["saleprice2", i].Value == "") grdbarcode["saleprice2", i].Value = 0;
                        //if (bcode.RecordCount == 0) bcode.AddNew();
                        if (grdcontract["textval1", i].Value.ToString() == "1" && grdcontract["fieldval1", i].Value == null)
                        {
                            grdcontract["fieldval1", i].Value = "0";
                        }                         //  if (grdcontract["fieldval1", i].Value == null) grdcontract["fieldval1", i].Value = "0";
                        string ftype = emp.Fields[grdcontract["fieldname1", i].Value].Type.ToString();
                        if ((ftype == "adSingle" || ftype == "adNumeric" || ftype == "adInteger") && grdcontract["fieldval1", i].Value == "")
                            grdcontract["fieldval1", i].Value = "0";
                        emp.Fields[grdcontract["fieldname1", i].Value].Value = grdcontract["fieldval1", i].Value;
                        emp.Fields["emp_id"].Value = txtempno.Text;
                        emp.Fields["active"].Value = true;

                        if (grdcontract["ismandatory1", i].Value != null && grdcontract["fieldval1", i].Value == null)
                        {
                            MessageBox.Show("Mandatory (*) Entry Not allowed Blank, Please Confirm");
                            ADOconn.RollbackTrans();
                            return;
                        }

                    }

                    if (emp.Fields["basic"].Value == null) emp.Fields["basic"].Value = 0;
                    if (emp.Fields["Transportation"].Value == null) emp.Fields["basic"].Value = 0;
                    if (emp.Fields["Housing"].Value == null) emp.Fields["basic"].Value = 0;
                    if (emp.Fields["Other"].Value == null) emp.Fields["basic"].Value = 0;

                    emp.Update();

                    rec = new ADODB.Recordset();
                    rec.Open("SELECT @@IDENTITY", ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //rec.GetRows();

                    txtcontractid.Text = rec.Fields[0].Value.ToString();

                    iserror = false;


                    ADOconn.CommitTrans();

                    // sql = "update  [HD_ITEMMASTER]  set [Item_Code]='" + Txtitem.Text.Trim() + "',[DESCRIPTION]='" + txtempname.Text + "',[ar_desc]='" + txtarabic + "',[ITM_CAT_CODE]='" + cmbcat.SelectedValue + "',[UNIT]='" + cmbunit.SelectedValue + "',[FRACTION]='" + txtfraction.Text + "',[ALIAS_NAME]='" + txtalias + "',BARCODE ='" + txtbarcode + "'  where Item_Code ='" + txtolditm.Text + "'";


                    //  cmd = new SqlCommand(sql, Conn);

                    //    cmd.ExecuteNonQuery();


                    isedit = true;




                    Conn.Close();



                    isini = false;



                    MessageBox.Show("Successfully Added/Updated Contract", "Successfull");
                    search_contract();
                    return;


                    // MessageBox.Show("Successfully Inserted New reciept", "Successfull");
                }

                catch (System.Data.SqlClient.SqlException excep)
                {
                    if (isbegintran)
                        ADOconn.RollbackTrans();
                    MessageBox.Show(excep.Message);


                }


            }

            catch (Exception ex)
            {
                if (isbegintran)
                    ADOconn.RollbackTrans();
                MessageBox.Show(ex.Message);
            }


        }

        private void txtpriv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtempno_TextChanged(object sender, EventArgs e)
        {
            if (txtempno.Text == "")
            {
                btncontract.Enabled = false;
                btnsavedep.Enabled = false;
                btnsavedocs.Enabled = false;
            }
            else
            {
                btncontract.Enabled = true;
                btnsavedep.Enabled = true;
                btnsavedocs.Enabled = true;
            }
        }

        private void search_all()
        {
            try
            {
                search_Employee(txtempno.Text);
                search_foto();
                search_dependant();
                search_docs();
                search_contract();

            }
            catch (Exception ex)
            {

            }

        }

        private void dgdep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgdocs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmbholdername.SelectedValue = dgdocs["Dep_Id", e.RowIndex].Value;
                txtidnumber.Text = dgdocs["Id", e.RowIndex].Value.ToString();
                cmbidtype.Text = dgdocs["type", e.RowIndex].Value.ToString();
                //cmbidtype.SelectedValue = dgdocs["Id", e.RowIndex].Value;
                dtidissue.Value = Convert.ToDateTime(dgdocs["Issue_Date", e.RowIndex].Value);
                dtidexpire.Value = Convert.ToDateTime(dgdocs["Expiry_Date", e.RowIndex].Value);
                txtidissueplace.Text = dgdocs["Issue_Place", e.RowIndex].Value.ToString();
                txtdocid.Text = dgdocs["IdentityId", e.RowIndex].Value.ToString();



            }
            catch (Exception ex)
            {

            }
        }

        private void grdcontract_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cont_currow = e.RowIndex;
                if(grdcontract["remarks1",e.RowIndex].Value==null) grdcontract["remarks1",e.RowIndex].Value="";
                lblremarks.Text = grdcontract["remarks1",e.RowIndex].Value.ToString();
            }
            catch ( Exception ex)
            {

            }

        }

        private void dtselect_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdcontract["Caption1", cont_currow].Value.ToString().IndexOf("Date") > 0)
                {
                    grdcontract["fieldval1", cont_currow].Value = dtselect.Value.Date.ToString("yyyy-MM-dd");
                    grdcontract["colvalue1", cont_currow].Value = dtselect.Value.Date.ToString("yyyy-MM-dd");

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void search_contract()
        {
            try
            {
                try
                {
                    sql = "sELECT  Contract_Id,Start_Date,End_Date from  HV_ContractDetails where emp_id=" + txtempno.Text + " order by 1";

                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();

                    SqlDataAdapter addep = new SqlDataAdapter(cmd);

                    DataTable dtdep = new DataTable("contract");
                    addep.Fill(dtdep);
                    //grditem.Visible = true;
                    //dv.Table = itemdt;

                    dgcontract.DataSource = dtdep;
                    dgcontract.Columns[2].Width = 300;
                    dgcontract.Columns[0].Width = 175;
                    dgcontract.Columns[1].Width = 175;
                    Load_grid_contract();

                    //for(int i=0;i<grdcontract.Rows.Count;i++)
                    //{
                    //    grdcontract["fieldval", i].Value = "";
                    //    grdcontract["colval", i].Value = "";

                    //}




                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }

        }

        private void dgcontract_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                try
                {


                    Conn.Close();
                    Conn.Open();

                    txtcontractid.Text = dgcontract["contract_id", e.RowIndex].Value.ToString();
                    sql = "sELECT * from  HV_ContractDetails where emp_id='" + txtempno.Text + "' and contract_id=" + dgcontract["contract_id", e.RowIndex].Value;
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();

                    rd = cmd.ExecuteReader();



                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            if (!string.IsNullOrEmpty(rd["emp_id"].ToString()))
                            {



                                DataRowView drw = null;

                                //cmbcat.SelectedValue = ctcode;






                                if (!string.IsNullOrEmpty(rd["emp_id"].ToString()))
                                {


                                    for (int i = 0; i <= grdcontract.Rows.Count - 1; i++)
                                    {

                                        if (grdcontract["fieldname1", i].Value != null)
                                        {


                                            // rec.Fields[grdcontract["fieldname", i].Value.ToString()].Value =
                                            grdcontract["fieldval1", i].Value = rd[grdcontract["fieldname1", i].Value.ToString()].ToString();
                                            grdcontract["colvalue1", i].Value = rd[grdcontract["fieldname1", i].Value.ToString()].ToString();


                                            if (grdcontract["textval1", i].Value.ToString() == "1")
                                            {
                                                grdcontract["colvalue1", i].Value = rd[grdcontract["fieldname1", i].Value.ToString() + "Name"].ToString();
                                            }

                                        }



                                    }

                                    try
                                    {
                                        txtcontractid.Text = rd["Contract_id"].ToString();
                                        decimal tot = Convert.ToDecimal(rd["Basic"].ToString()) + Convert.ToDecimal(rd["Transportation"].ToString()) + Convert.ToDecimal(rd["Housing"].ToString()) + Convert.ToDecimal(rd["Other"].ToString());
                                        txttotalsal.Text = tot.ToString();
                                    }
                                        
                                        
                                    catch (Exception ex)
                                    {

                                    }


                                    isedit = true;
                                }



                            }




                            isedit = true;
                            //txtempno.Focus();
                        }

                    }
                    else
                    {
                        rd.Close();
                        return;
                    }




                    search_foto();
                    rd.Close();
                    Conn.Close();
                    // grdcontract.CurrentCell = grdcontract[2, 5];
                    grdcontract.CurrentCell = grdcontract[2, first_grdrow];

                    isini = false;
                    grdcontract.ClearSelection();

                }
                //}

                catch (System.Data.SqlClient.SqlException excep)
                {

                    MessageBox.Show(excep.Message);


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void grdcontract_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = "BasicTransportationHousingOther";
                if (str.Contains(grdcontract["caption1", e.RowIndex].Value.ToString()))
                {
                    decimal amt = 0;
                    decimal tot = 0;
                    DataGridViewRow row;
                    row = grdcontract.Rows
                             .Cast<DataGridViewRow>()
                             .Where(r => r.Cells["fieldname1"].Value.ToString().ToUpper().Equals("BASIC"))
                             .First();
                    if (row.Cells["colvalue1"].Value != null) amt = Convert.ToDecimal(row.Cells["colvalue1"].Value);

                    tot = tot + amt;
                    amt = 0;
                    row = grdcontract.Rows
                           .Cast<DataGridViewRow>()
                           .Where(r => r.Cells["fieldname1"].Value.ToString().ToUpper().Equals("TRANSPORTATION"))
                           .First();
                    if (row.Cells["colvalue1"].Value != null) amt = Convert.ToDecimal(row.Cells["colvalue1"].Value);

                    tot = tot + amt;
                    amt = 0;
                    row = grdcontract.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["fieldname1"].Value.ToString().ToUpper().Equals("HOUSING"))
                          .First();
                    if (row.Cells["colvalue1"].Value != null) amt = Convert.ToDecimal(row.Cells["colvalue1"].Value);

                    tot = tot + amt;
                    amt = 0;
                    row = grdcontract.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["fieldname1"].Value.ToString().ToUpper().Equals("OTHER"))
                          .First();
                    if (row.Cells["colvalue1"].Value != null) amt = Convert.ToDecimal(row.Cells["colvalue1"].Value);

                    if (grdcontract["lookupsql1", e.RowIndex].Value.ToString() == "")
                    {
                        grdcontract["fieldval1", e.RowIndex].Value = grdcontract["colvalue1", e.RowIndex].Value;
                    }

                    tot = tot + amt;
                    amt = 0;
                    txttotalsal.Text = tot.ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.Text.Length;
            textBox1.Select(0, textBox1.Text.Length);
            textBox1.Text = "";
        }

        private void grdbutton_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdbutton_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            switch (grdbutton.CurrentCell.ColumnIndex)
            {
                case 0:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Gvar.frm_priv = MDIParent1.find_tag("mnubrand");

                        Gvar.Gind = 9;
                        Form childForm = new FrmBrand();
                        childForm.MdiParent = MDIParent1.ActiveForm;

                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Department Entry Screen";
                        childForm.Show();
                        break;
                    }
                case 1:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Gvar.frm_priv = MDIParent1.find_tag("mnubrand");

                        Gvar.Gind = 11;
                        Form childForm = new FrmBrand();
                        childForm.MdiParent = MDIParent1.ActiveForm; ;

                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Nationality Entry Screen";
                        childForm.Show();
                        break;
                    }
                case 2:
                    {
                        if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Gvar.frm_priv = MDIParent1.find_tag("mnubrand");

                        Gvar.Gind = 10;
                        Form childForm = new FrmBrand();
                        childForm.MdiParent = MDIParent1.ActiveForm; ;

                        //childForm.Text = "Window " + childFormNumber++;
                        childForm.Text = "Position Entry Screen";
                        childForm.Show();
                        break;
                    }
            }
        }

        private void btncontdelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You want to Delete This Contract?", "Delete Contract", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Conn.Close();
                Conn.Open();


                sql = "delete from   dbo.HV_ContractDetails  where emp_id ='" + txtoldempno.Text + "' AND contract_id=" + txtcontractid.Text;



                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                search_contract();
                Load_grid_contract();
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Employee Contract  Completed Successfully!!");
            }
        }

        private void btndocdelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You want to Delete This Document?", "Delete Document ID", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Conn.Close();
                Conn.Open();


                sql = "delete from   dbo.HV_docs  where emp_id ='" + txtoldempno.Text + "'  and [IdentityId]=" + txtdocid.Text;


                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                search_docs();


                txtidissueplace.Text = "";

                //cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Employee Document  Completed Successfully!!");
            }
        }

        private void btndepdelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You want to Delete This Dependant?", "Delete Employee Depenetant", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Conn.Close();
                Conn.Open();



                sql = "delete from   dbo.HV_DependentDetails  where emp_id ='" + txtoldempno.Text + "' and dep_id=" + txtdepid.Text;


                cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();

                search_dependant();

                txtdepname.Text = "";
                txtdepaname.Text = "";

                //cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Employee  Completed Successfully!!");
            }
        }

        private void btncontclear_Click(object sender, EventArgs e)
        {
            Load_grid_contract();
        }

        private void grdlookup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                // DataGridView grd = (DataGridView)sender;

                Type obj = sender.GetType();

                var c2 = sender.GetType();


                object oc2 = c2;

                //nder.GetType() obj  = (sender.GetType()) sender;
                DataGridView grd = (DataGridView)((DataGridView)sender).Parent;
                string fld = "";
                string val = "";

                if (acntrl == "grdmain")
                {
                    fld = "fieldval";
                    val = "colvalue";

                }
                if (acntrl == "grdcontract")
                {
                    fld = "fieldval1";
                    val = "colvalue1";

                }

                if (grdlookup.Visible && grdlookup.Rows.Count > 0)
                {
                    grd[fld, grd.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                    grd[val, grd.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;

                    grdlookup.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void grdlookup_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        grdlookup.Visible = false;
                        break;
                    }
                    break;


            }
        }

        private void grdmain_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if(grdmain["remarks1",e.RowIndex].Value==null) grdmain["remarks1",e.RowIndex].Value="";
                lblremarks.Text = grdmain["remarks",e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void dgcontract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void create_empgl()
        {
            try
            {
                ADODB.Recordset tmp = new ADODB.Recordset();
                ADODB.Recordset rec = new ADODB.Recordset();
                //if (txtismanual.Text == "0")
                //{
                //    if (txtglacno.Text =="0")
                //    {

                //        MessageBox.Show("Invalid TrailBalance Account Number, Please Enter a Valid Number", "Invalid Entry");
                //        return;

                //    }
                //    gen_accno();


                    

                //    //sql = "update ACC_TYPE SET CUR_NO = CUR_NO+1 WHERE  ACC_TYPE_CODE=" + cmbtype.SelectedValue;
                //    //tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                //}

               

                //sql = "SELECT * FROM Customer where Customer_no =" + Convert.ToDecimal(txtcuscode.Text.Trim());
                sql = "SELECT * FROM Accounts where Acc_no ='" + txtaccno.Text.Trim() + "'";

                rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                if (rec.RecordCount == 0)
                {
                    rec.AddNew();



                }


                //rec.Fields["veh_no"].Value = Convert.ToDouble(txtvehno.Text.Trim());
                rec.Fields["ACC_NO"].Value = txtaccno.Text.Trim();
                if (txtarname.Text.Trim() == "") txtarname.Text = txtempname.Text;

                rec.Fields["ACC_NAME"].Value = txtempname.Text.Trim();
                rec.Fields["ACC_ANAME"].Value = txtarname.Text.Trim();



                rec.Fields["UPDATE_TIME"].Value = DateTime.Now;
                
                    rec.Fields["flag"].Value = "A";

               

                
                                rec.Fields["Group_Ac"].Value = txtaccno.Text.Trim();

                                rec.Fields["Def_currency"].Value = Gvar._currency;

                
                    rec.Fields["Id_Number"].Value = txtempno.Text;
               
                                 rec.Fields["ACC_TYPE_CODE"].Value = 4;
                                 rec.Fields["ACC_TYPE"].Value = "EM";
                                 rec.Fields["ACC_LEVEL"].Value = 4;
                                 rec.Fields["GL_ACC_NO"].Value = txtglacno.Text;
                                 rec.Fields["ACC_CLASS"].Value = 2;
                                 rec.Fields["LEVEL2_NO"].Value = 0;
                                 rec.Fields["LEVEL3_NO"].Value = 0;
                



                rec.Update();
               
                    //rec = new ADODB.Recordset();
                    //sql = "SELECT * FROM Accounts_INFO where Acc_no ='" + txtaccno.Text.Trim() + "'";

                    //rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    //if (rec.RecordCount == 0)
                    //{
                    //    rec.AddNew();



                    //}
                    ////rec.Fields["ACC_NO"].Value = txtaccno.Text.Trim();
                    ////rec.Fields["ACC_TELE_NO"].Value = txttelephone.Text.Trim();
                    ////rec.Fields["ACC_FAX_NO"].Value = txtfaxno.Text.Trim();
                    ////rec.Fields["ACC_ADDRESS"].Value = txtaddress.Text.Trim();
                    ////rec.Fields["IBAN_NO"].Value = txtiban.Text.Trim();
                    ////rec.Fields["BANK_DET"].Value = txtbankdet.Text.Trim();
                    ////rec.Fields["CONTACT_PERSON"].Value = txtcontact.Text.Trim();
                    ////rec.Fields["AREA_NAME"].Value = cmbarea.Text.Trim();
                    ////rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    ////rec.Fields["ACC_Mobile_NO"].Value = txtmobile.Text.Trim();
                    ////rec.Fields["Id_Number"].Value = txtidnumber.Text.Trim();
                    ////rec.Fields["ACC_Mobile_NO"].Value = txtmobile.Text.Trim();
                    ////rec.Fields["EMAIL"].Value = txtemailid.Text.Trim();
                    ////rec.Fields["credit_limit"].Value = Convert.ToDouble(txtcrlimit.Text);
                    ////rec.Fields["send_sms"].Value = chksms.Checked;

                    //rec.Update();
                

            }
            catch(Exception ex)
            {
                iserror = false;
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
                {
                     sql = "SELECT start_no from acc_type inner join ac_options on acc_type_code=emp_ac_type ";
                tmp = new ADODB.Recordset();
                 tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockReadOnly, 1);
               
                 trn = Convert.ToInt64(tmp.Fields[0].Value);
                }
               

                   
                else
                    trn = Convert.ToInt64(tmp.Fields[0].Value);
                txtaccno.Text = trn.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

    }
}

   


