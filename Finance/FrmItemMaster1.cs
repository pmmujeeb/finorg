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
        DataView dvtree = new DataView();
        bool isedit;
        bool iserror;
        string lastlookval;
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        string [] chart_acc;
        int dblclk_row;
        int last_row;
        int last_col;
        int max_level;
        string parent_id = "0";
        bool fnd = false;
        string sql = "";
        bool isini = false;
        public FrmPayExpense()
        {
            InitializeComponent();
        }

        private void FrmAccMaster_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(0, 0);

            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            
            Load_grid();
            dtentry.Value = DateTime.Now.Date;
            this.grdmain.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(grdmain_EditingControlShowing);
            
            
        }

        private void FrmAccMaster_ResizeBegin(object sender, EventArgs e)
        {
           
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

        private void Load_grid()
        {
            try
            {
                Conn.Close();
                Conn.Open();



                string sql = "sELECT *  from form_caption  where flag <> 'X' order by Order_by";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();


            
                

                SqlDataReader rd = cmd.ExecuteReader();
                System.Drawing.Image image1;
                int i=0;
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
                            grdmain.Rows[i].HeaderCell.Value = rd["field_name"].ToString();
                            
                            if (rd["default_val"].ToString() != "")
                            {
                                if (rd["default_val"].ToString().Contains("=") )
                                {
                                    string [] ary = rd["default_val"].ToString().Split('=');
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
                            grdmain[0, i++].Value = rd[0].ToString();


                            if (rd["rlposition"].ToString() =="H")
                            {
                                MergeCellsInRow(i-1, 1, 2);
                                for (int c = 0; c < grdmain.ColumnCount; c++)
                                {
                                    grdmain[c, i-1].Style.BackColor = Color.Honeydew;
                                    grdmain[c, i-1].Style.ForeColor = Color.Red;
                                }
                            }
                            if (rd["flag"].ToString() == "H")
                            {
                                grdmain.Rows[i - 1].Visible = false;
                                if (rd["col_name"].ToString() == "WHprice")
                                {
                                    grdbarcode.Columns["bwhprice"].Visible = false;
                                    grdbarcode.Columns["bretprice"].Visible = false;

                                }

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

        private void grdmain_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            //    if (grdmain.CurrentCell == null) return;

            //    if ( grdmain["flag", e.RowIndex].Value.ToString()=="R")
            //    {
            //        if(e.RowIndex< grdmain.Rows.Count-1)
            //           //this.BeginInvoke(new MethodInvoker(grdmain_CellEnter(sender,e)));
            //        grdmain.CurrentCell = grdmain["colvalue", e.RowIndex+1];
            //        else
            //            grdmain.CurrentCell = grdmain["colvalue", e.RowIndex - 1];

            //    }
            //    if (e.ColumnIndex != grdmain["colvalue", e.RowIndex].ColumnIndex)
            //    {
            //        grdmain.CurrentCell = grdmain["colvalue", e.RowIndex];
            //    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void poplookup(object sender, DataGridViewCellCancelEventArgs e)
        {
            try{
                DataGridView grd = (DataGridView)sender;
                grdlookup.DataSource = null;
            dblclk_row = grd.CurrentCell.RowIndex;
           
            Conn.Close();
            Conn.Open();
            string sql = grd["Lookupsql", e.RowIndex].Value.ToString();
                if(sql.Contains("?up"))
                {
                    string up = "'" + grd["fieldval", e.RowIndex - 1].Value.ToString() + "'";
                    sql=sql.Replace("?up",up);
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
            grdlookup.Left =  cellRectangle.Left ;
            grdlookup.Top =  cellRectangle.Top + grd.Rows[0].Height  ;
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

        private void FrmAccMaster_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void FrmAccMaster_SizeChanged(object sender, EventArgs e)
        {
            
            tabControl1.Width = this.Width;
            grdmain.Height = tabControl1.Height;
            grdmain.Width = tabControl1.Width;
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            grdmain.Height = tabControl1.Height;
            grdmain.Width = tabControl1.Width;
        }

        private void grdmain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView grd = (DataGridView)sender;

                if (grd.CurrentCell == null) return;

                if (grd["lookupsql", e.RowIndex].Value.ToString() != "")
                {
                    poplookup(sender,   e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void grdlookup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try{
               // DataGridView grd = (DataGridView)sender;

                Type obj = sender.GetType();

                var c2 = sender.GetType();

              
                object oc2 = c2;
                
                //nder.GetType() obj  = (sender.GetType()) sender;
               DataGridView grd = (DataGridView)((DataGridView)sender).Parent;
               
            if (grdlookup.Visible && grdlookup.Rows.Count > 0)
            {
                grd["fieldval", grd.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                grd["colvalue", grd.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;

                grdlookup.Visible = false;
            }
          }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

            }
        }

        private void grdlookup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdlookup_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    {
                        grdlookup.Visible=false;
                    break;
                    }
                    break;


            }
        }

        private void grdmain_KeyDown(object sender, KeyEventArgs e)
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
            }

        }

        private void FrmAccMaster_KeyDown(object sender, KeyEventArgs e)
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
                    
                    break;

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
                            if (grdlookup.Visible && grdlookup.Rows.Count >0)
                            {
                                grdmain.EndEdit();
                                grdmain["fieldval", grdmain.CurrentCell.RowIndex].Value = grdlookup["code", grdlookup.CurrentCell.RowIndex].Value;
                                grdmain["colvalue", grdmain.CurrentCell.RowIndex].Value = grdlookup["desc1", grdlookup.CurrentCell.RowIndex].Value;

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

       

      
        private void grdmain_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
           

              TextBox tb = (TextBox)e.Control;

                tb.TextChanged += new EventHandler(tb_TextChanged);
                
            

        }
        void tb_TextChanged(object sender, EventArgs e)
        {
            try{
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
                    if (grdlookup.Rows.Count<1)
                    {
                       // grdmain.EndEdit();
                        grdmain["fieldval", grdmain.CurrentCell.RowIndex].Value = "";
                        //grdmain.CurrentCell.Value = lastlookval;
                    }
                    else
                    {
                        //grdmain.EndEdit();
                        //if (grdmain.CurrentCell.Value!=null)
                        //lastlookval = grdmain.CurrentCell.Value.ToString();
                        //grdmain.BeginEdit(false);
                    }
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
                int addrow=0;
                

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
                    if (grd.CurrentCell.RowIndex < grd.Rows.Count - 1 && grd.CurrentCell.RowIndex >0)
                    //this.BeginInvoke(new MethodInvoker(grd_CellEnter(sender,e)));
                    {
                        grd.CurrentCell = grd["colvalue", grd.CurrentCell.RowIndex + addrow];
                        goto chkread;
                    }
                    else
                    {
                        addrow=addrow * -1;
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

        

        private void grdmain_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            last_col = e.ColumnIndex;
            last_row = e.RowIndex;
        }

        private void toolclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        private string get_pkey(string[,] key_ary  )  
        {
             string pkey;
            try
            {
                pkey="";
               for(int i=0; i<key_ary.GetUpperBound(0);i++)
               {
                   if (key_ary[i,0]!="" && key_ary[i,0]!=null)
                   {
                       if (pkey=="") 
                           pkey = key_ary[i,0] + "='"+ key_ary[i,1] + "'";
                       else
                           pkey += " and " + key_ary[i,0] + "='"+ key_ary[i,1] + "'";
                   }
               }

               if (pkey == "") pkey = "1=0";
                return pkey;

            }
            catch
            {
                pkey="1=0";
            return pkey;
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

       

       private void display_acc(long acc)
        {
            try
            {
                ADODB.Recordset tmp = new ADODB.Recordset();
                if (ADOconn.State == 1) ADOconn.Close();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
                Conn.Close();
                Conn.Open();

               
                string sql = "SELECT * FROM ledger where acc_no="+ acc;

                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if(!tmp.EOF)
                {
                    for (int i=1;i<grdmain.Rows.Count-1;i++)
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
         private void update_itemcode(string oldcode, string newcode)
         {
             if (oldcode != newcode)
                    {

                        DialogResult result = MessageBox.Show("Do You want to Update The Item Code from " + oldcode + " To " + newcode + "?", "Confirm Item Update", MessageBoxButtons.YesNoCancel);

                        if (result != DialogResult.Yes)
                        {
                            Conn.Close();
                            return;

                        }
                    }

                   string sql = "update  [HD_ITEMMASTER]  set [Item_Code]='" + newcode + "''  where Item_Code ='" + oldcode + "'";
                    

                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();




                    sql = " update  [STOCK_MASTER] set [Item_Code]='" + newcode + "' where Item_Code ='" + oldcode + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();

                    sql = " update  [wr_STOCK_MASTER] set [Item_Code]='" + newcode + "' where Item_Code ='" + oldcode + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                    sql = " update  [data_entry_grid] set [Item_Code]='" + newcode + "' where Item_Code ='" + oldcode + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


                    sql = " update  [trn_itm_detail] set [Item_Code]='" + newcode + "' where Item_Code ='" +oldcode + "'";


                    cmd = new SqlCommand(sql, Conn);

                    cmd.ExecuteNonQuery();


         }
       private void saveToolStripButton_Click(object sender, EventArgs e)
       {
           save_Item();
       }
       private void save_Item()
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
               if (ADOconn.State == 1) ADOconn.Close();
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
               Conn.Close();
               Conn.Open();
               double trnno = 0;
               double trnno2 = 0;
               iserror = false;
             


               string table_name = "";
               string[] tbl_ary = new string[grdmain.Rows.Count];
               string[,] key_ary = new string[10, 2];
               int c = 0;
              
              
               string sql = "";
               string acc_id = "0";
               int totdigit = 0;
               int curdgit = 0;

                DataGridViewRow row = grdmain.Rows
                       .Cast<DataGridViewRow>()
                       .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("ITEM_CODE"))
                       .First();

               if (!isedit)
               {

                  
                   //row.Cells["fieldval"].Value = acc_id;
                   //row.Cells["colvalue"].Value = acc_id;

                  // rowIndex = row.Index;

                   sql = "SELECT Item_Code FROM HD_ITEMMASTER where Item_Code ='" + row.Cells["fieldval"].Value + "'";

                   cmd = new SqlCommand(sql, Conn);

                   SqlDataReader rd = cmd.ExecuteReader();



                   if (rd.HasRows)
                   {


                       DialogResult result = MessageBox.Show("This Item Already Existing!!, Do You want to Update?", "Item Found", MessageBoxButtons.YesNoCancel);

                       if (result == DialogResult.Yes) isedit = true; else return;


                       if (result != DialogResult.Yes)
                       {
                           Conn.Close();
                           return;

                       }

                   }
                 
                  
                   int rowIndex = -1;

                   //DataGridViewRow row = grdmain.Rows
                   //    .Cast<DataGridViewRow>()
                   //    .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("ACC_ID"))
                   //    .First();
                   //row.Cells["fieldval"].Value = acc_id;
                   //row.Cells["colvalue"].Value = acc_id;

                   //rowIndex = row.Index;
                   //sql = "SELECT max(acc_no)+1 FROM ledger ";
                   //rec = new ADODB.Recordset();
                   //rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                   //if (rec.RecordCount != 0)
                   //{
                   //    row = grdmain.Rows
                   //   .Cast<DataGridViewRow>()
                   //   .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("ACC_NO")).First();
                   //    row.Cells["fieldval"].Value = rec.Fields[0].Value.ToString();
                   //    row.Cells["colvalue"].Value = rec.Fields[0].Value.ToString();
                   //}

                   //return;

                   // ADOconn.BeginTrans();
               }



                sql = "SELECT * FROM HD_ITEMMASTER  where Item_Code ='" + row.Cells["fieldval"].Value + "'";
                  
                   rec = new ADODB.Recordset();
                   rec.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                   if (rec.RecordCount == 0)
                   {
                       rec.AddNew();
                       
                   }



                   for (int i = 0; i <= grdmain.Rows.Count - 1; i++)
                   {
                       int a = grdmain["tablename", i].Value.ToString().IndexOf("Hd_Itemmaster");
                       if (grdmain["tablename", i].Value.ToString() != "" && a >= 0 && grdmain["fieldname", i].Value.ToString() != "")
                       {
                           if (grdmain["fieldval", i].Value != null)
                           {


                               rec.Fields[grdmain["fieldname", i].Value.ToString()].Value = grdmain["fieldval", i].Value.ToString();


                           }

                       }
                   }

                   // sql = "INSERT INTO [HD_ITEMMASTER]([Item_Code],[DESCRIPTION],AR_DESC,[USER],[ITM_CAT_CODE],[UNIT],[FRACTION],[ALIAS_NAME],[BRN_CODE],BARCODE)";
                  
                    rec.Fields["USER"].Value = Gvar.username;
                    rec.Fields["BRN_CODE"].Value = Gvar.brn_code;
                   if(rec.Fields["BARCODE"].Value=="" || rec.Fields["BARCODE"].Value==null || rec.Fields["BARCODE"].Value==DBNull.Value)
                       rec.Fields["BARCODE"].Value=rec.Fields["Item_Code"].Value;


                    if(rec.Fields["AR_DESC"].Value=="" || rec.Fields["AR_DESC"].Value==null || rec.Fields["AR_DESC"].Value==DBNull.Value)
                       rec.Fields["AR_DESC"].Value=rec.Fields["DESCRIPTION"].Value;


                   if(rec.Fields["FRACTION"].Value=="" || rec.Fields["FRACTION"].Value==null || rec.Fields["FRACTION"].Value==DBNull.Value)
                       rec.Fields["FRACTION"].Value="1";
                   rec.Update();





               //if (tmp.Fields[0].Value)


             



               isedit = true;




               //  if (!iserror) save_data();


           }

           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }

       }

       private void grdbutton_CellClick(object sender, DataGridViewCellEventArgs e)
       {
           switch(grdbutton.CurrentCell.ColumnIndex)
           {
               case 0:
                   {
                       int i = 0;
                       bool fnd=false;
                        DataGridViewRow row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("UNIT"))
                      .First();

                        if (row.Cells["colvalue"].Value == null  || row.Cells["fieldval"].Value == null)

                        

                        {
                            MessageBox.Show("Invalid Unit Selection,Please check and confirm!!");
                            return;
                        }


                       if( row.Cells["colvalue"].Value.ToString() == "")
                       {
                           MessageBox.Show("Invalid Unit Selection,Please check and confirm!!");
                           return;
                       }



                       for (i = 0; i < grdbarcode.Rows.Count ;i++ )
                       {
                           if (grdbarcode["bunit", i].Value == row.Cells["colvalue"].Value)
                           {
                               fnd = true;
                               break;
                           }
                       }

                       if(!fnd)
                       grdbarcode.Rows.Add(1);
                       string unitcode="";
                       grdbarcode["bunit", i].Value = row.Cells["colvalue"].Value;
                       if(row.Cells["fieldval"].Value!=null)
                        unitcode = row.Cells["fieldval"].Value.ToString();
                       //row.Cells["colvalue"].Value = "";

                      
                      
                          
                        row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("ITEM_CODE"))
                      .First();
                       grdbarcode["bitemcode", i].Value = row.Cells["colvalue"].Value;
                      // row.Cells["colvalue"].Value = "";


                       row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("BARCODE"))
                      .First();
                      
                      // row.Cells["colvalue"].Value = "";
                       grdmain.CurrentCell = grdmain["colvalue", row.Index];
                       if ( grdbarcode["bbarcode", i].Value == null) grdbarcode["bbarcode", i].Value = grdbarcode["bitemcode", i].Value;

                       if (grdbarcode["bbarcode", i].Value.ToString() == "" ) grdbarcode["bbarcode", i].Value = grdbarcode["bitemcode", i].Value;


                       if (i > 0)

                           grdbarcode["bbarcode", i].Value = grdbarcode["bbarcode", i].Value.ToString() + unitcode;
                       
                       //  row = grdmain.Rows
                     //.Cast<DataGridViewRow>()
                     //.Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("UNIT"))
                     //.First();
                     //  grdbarcode["bunit", i].Value = row.Cells["col_val"].Value;


                       row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("FRACTION"))
                      .First();
                       grdbarcode["bfraction", i].Value = row.Cells["colvalue"].Value;
                      // row.Cells["colvalue"].Value = "";

                       row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("SALE_PRICE"))
                      .First();
                       grdbarcode["bwhprice", i].Value = row.Cells["colvalue"].Value;
                      // row.Cells["colvalue"].Value = "";

                       row = grdmain.Rows
                      .Cast<DataGridViewRow>()
                      .Where(r => r.Cells["fieldname"].Value.ToString().ToUpper().Equals("RETAIL_PRICE"))
                      .First();
                       grdbarcode["bretprice", i].Value = row.Cells["colvalue"].Value;
                       //row.Cells["colvalue"].Value = "";
                       grdbarcode["bdelete", i].Value = "Delete";
                       grdmain.BeginEdit(false);
                   }
                   break;
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


                           for (int i = 0; i <= grdmain.Rows.Count - 1; i++)
                           {
                               int a = grdmain["tablename", i].Value.ToString().IndexOf("Hd_Itemmaster");
                               if (grdmain["tablename", i].Value.ToString() != ""  && grdmain["fieldname", i].Value.ToString() != "")
                               {
                                   if (grdmain["fieldval", i].Value != null)
                                   {


                                      // rec.Fields[grdmain["fieldname", i].Value.ToString()].Value =
                                           grdmain["fieldval", i].Value = rd["fieldname"].ToString();
                                           grdmain["colvalue", i].Value = rd["fieldname"].ToString();


                                   }

                               }
                           }


                       }




                       isedit = true;
                   }

               }








               //if (Gvar._SuperUserid != 1)
               //{
               //    saveToolStripButton.Enabled = false;


               //}







               _load_stock("");

               //search_suplier();
               search_barcode("");
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

       private void search_barcode(string txtitem)
       {
           try
           {
               if (string.IsNullOrEmpty("")) return;
               Conn.Close();




               Conn.Open();


               sql = "SELECT *  from barcode where item_code='" + txtitem + "'";



               SqlCommand cmd = new SqlCommand(sql, Conn);
               SqlDataReader rd = cmd.ExecuteReader();
               grdbarcode.Rows.Clear();
               isini = true;
               while (rd.Read())
               {
                   grdbarcode.Rows.Add();



                   grdbarcode["barcode", grdbarcode.Rows.Count - 1].Value = rd["barcode"];
                   grdbarcode["description", grdbarcode.Rows.Count - 1].Value = rd["description"];
                   grdbarcode["unit", grdbarcode.Rows.Count - 1].Value = rd["unit"];
                   grdbarcode["fraction", grdbarcode.Rows.Count - 1].Value = rd["fraction"];
                   grdbarcode["itemcode", grdbarcode.Rows.Count - 1].Value = rd["item_code"]; ;
                   grdbarcode["saleprice1", grdbarcode.Rows.Count - 1].Value = rd["sale_price"];
                   grdbarcode["saleprice2", grdbarcode.Rows.Count - 1].Value = rd["retail_price"];
                   grdbarcode["descriptionAr", grdbarcode.Rows.Count - 1].Value = rd["description_ar"];


                   //dt1.Text = rd["Cur_date"].ToString();


                   fnd = true;

               }

               isini = false;

               Conn.Close();
           }
           catch (SqlException ex)
           {
               MessageBox.Show(ex.Message);
           }



       }
       private void FrmItemMaster1_Activated(object sender, EventArgs e)
       {
           grdbutton.Rows.Add(1);
           grdmain.Focus();
           grdmain.CurrentCell = grdmain["colvalue", 0];
           grdmain.BeginEdit(false);
       }

       private void load_sup_list(int ACC_NO)
       {
           try
           {
               Conn.Close();
               Conn.Open();

               sql = " SELECT ACCOUNTS.ACC_NO  ,ACC_NAME,ACC_MOBILE_NO,CONTACT_PERSON  ";
               sql = sql + " FROM ACCOUNTS INNER JOIN ACCOUNTS_INFO ON   ACCOUNTS.ACC_NO=ACCOUNTS_INFO.ACC_NO WHERE ACCOUNTS.ACC_NO  =" + ACC_NO;
               cmd = new SqlCommand(sql, Conn);
               SqlDataAdapter ada2 = new SqlDataAdapter(cmd);



               grdsup.Visible = true;

               SqlDataReader rd = cmd.ExecuteReader();
               grdsup.Rows.Clear();


               while (rd.Read())
               {
                   grdsup.Rows.Add();
                   grdsup.Rows[grdsup.Rows.Count - 1].Cells[0].Value = rd[0];
                   grdsup.Rows[grdsup.Rows.Count - 1].Cells[1].Value = rd[1];
                   grdsup.Rows[grdsup.Rows.Count - 1].Cells[2].Value = rd[2];
                   grdsup.Rows[grdsup.Rows.Count - 1].Cells[3].Value = rd[3];


                   //dt1.Text = rd["Cur_date"].ToString();


                   fnd = true;

               }
               rd.Close();

           }

           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }


       }


       private void _load_stock(string itemcode)
       {
           // if (string.IsNullOrEmpty(Txtitem.Text)) return;
           Conn.Close();
           Conn.Open();
           try
           {
               string sql = " SELECT WRHOUSE_MASTER.WR_NAME, WR_STOCK_MASTER.STOCK, WR_STOCK_MASTER.OP_STOCK, WRHOUSE_MASTER.WR_CODE FROM WR_STOCK_MASTER RIGHT JOIN WRHOUSE_MASTER ON (WR_STOCK_MASTER.WR_CODE = WRHOUSE_MASTER.WR_CODE AND WR_STOCK_MASTER.Item_Code='" + itemcode + "')";

               cmd = new SqlCommand(sql, Conn);

               SqlDataReader rd = cmd.ExecuteReader();
               grdstock.ReadOnly = false;
               //grdstock.BeginEdit(true);
               grdstock.Rows.Clear();
              // isini = true;
               while (rd.Read())
               {

                   grdstock.Rows.Add();
                   grdstock.Rows[grdstock.Rows.Count - 1].Cells[0].Value = rd[0];
                   grdstock.Rows[grdstock.Rows.Count - 1].Cells[1].Value = rd[1];
                   grdstock.Rows[grdstock.Rows.Count - 1].Cells[2].Value = rd[2];
                   grdstock.Rows[grdstock.Rows.Count - 1].Cells[3].Value = rd[3];

                   grdstock.Columns[0].ReadOnly = true;
                   grdstock.Columns[1].ReadOnly = true;
                   //grdstock.Columns[2].ReadOnly = true;
                   //dt1.Text = rd["Cur_date"].ToString();
                   double v1;
                   double v2;
                   v1 = 0;
                   v2 = 0;
                   foreach (DataGridViewRow row in this.grdstock.Rows)
                   {



                       if (!string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                       {
                           v1 += Convert.ToDouble(row.Cells[1].Value);


                       }


                       if (!string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                       {
                           v2 += Convert.ToDouble(row.Cells[2].Value);


                       }

                   }
                   txtclstock.Text = v1.ToString();
                   txtopstock.Text = v2.ToString();
                   fnd = true;

               }
               rd.Close();
           }



           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }


       }




       private void Load_data()
       {
           try
           {
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
    
    
    }
}
