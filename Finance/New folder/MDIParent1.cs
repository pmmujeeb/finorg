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
using ADODB;
namespace FinOrg
{
    public partial class MDIParent1 : Form
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
       
        SqlCommand cmd1 = new SqlCommand();
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
            if (Gvar._SuperUserid == 1) return;
            enablemenu();
            //return;
            foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
            {
                
                
               
               
                disableMenu(toolSripItem);

            }
           
        }

        private void enablemenu()
        {
         ADODB.Connection ADOconn = new ADODB.Connection();
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
               ADODB.Recordset cus = new ADODB.Recordset();
               
            cus = new ADODB.Recordset();
            //string sql = "SELECT menu_code FROM menu_master where id in (select form_id from userpriv where dsp=1 and group_name = (select group_name from userinfo where userid='" + Gvar.Userid+ "'))";
            string sql = "SELECT m.menu_code,p.* FROM menu_master as M inner join userpriv as p on  m.id=p.form_id where p.dsp=1 and p.group_name = (select group_name from userinfo where userid='" + Gvar.Userid + "')";
               cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
            while(!cus.EOF)
            {
                string mnu = cus.Fields[0].Value.ToString();
                string prv ;
                Gvar.Priv menu_item = new Gvar.Priv();

                if (string.IsNullOrEmpty(cus.Fields["ins"].Value.ToString())) cus.Fields["ins"].Value = 0;
                if (string.IsNullOrEmpty(cus.Fields["upd"].Value.ToString())) cus.Fields["upd"].Value = 0;
                if (string.IsNullOrEmpty(cus.Fields["del"].Value.ToString())) cus.Fields["del"].Value = 0;


                if ((bool)cus.Fields["ins"].Value) prv = "1"; else prv = "0";
                if ((bool)cus.Fields["upd"].Value) prv = prv + "1"; else prv = prv + "0";
                if ((bool)cus.Fields["del"].Value) prv = prv + "1"; else prv = prv + "0";
                lstmenu.Items.Add(mnu);
                lstpriv.Items.Add(prv);
                menu_item.Menu = mnu;
                menu_item.priv = prv;

                Gvar.User_Priv.Add(menu_item);
                cus.MoveNext();
               
                ////foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
                ////{
                ////    if (toolSripItem.HasDropDownItems)
                ////    {
                ////        foreach (ToolStripItem toolSripItem1 in toolSripItem.DropDownItems)
                ////        {
                ////            if (toolSripItem1 is ToolStripSeparator) continue;

                ////            if (toolSripItem1 is ToolStripMenuItem)
                ////                //    insert_menu(menuItem.Text, toolSripItem.Name, 1, toolSripItem.Text, "", 'A');

                ////                if (toolSripItem1.Name==mnu)

                ////                toolSripItem.Visible = true;
                ////            {                        //call recursively         
                ////               // disableMenu((ToolStripMenuItem)toolSripItem);
                ////            }
                ////        }
                ////    }
                   
                ////}


            }
            ADOconn.Close();
            //cus.Close();
        }



        private void disableMenu(ToolStripMenuItem menuItem)
        {            //toolSripItems.Add(menuItem);   
            // if sub menu contain child dropdown items 
            Boolean fnd = false;
            Boolean isitemfound = false;
            ToolStripMenuItem m = new ToolStripMenuItem() ;
            if (menuItem.HasDropDownItems)
            {
                foreach (ToolStripItem toolSripItem in menuItem.DropDownItems)
                {
                    if (toolSripItem is ToolStripSeparator) if (fnd) continue; else 
                        toolSripItem.Visible = false; ;
                    
                       
                    //if (toolSripItem is ToolStripMenuItem)
                    //    m = (ToolStripMenuItem)toolSripItem;
                    if (m.HasDropDownItems)
                    {
                        disableMenu(m);
                        break;
                    }
                        //    insert_menu(menuItem.Text, toolSripItem.Name, 1, toolSripItem.Text, "", 'A');
                        fnd = false;
                        for (int i = 0; i < lstmenu.Items.Count; i++)
                        {
                            string mnu = lstmenu.Items[i].ToString();
                            if (toolSripItem.Name == mnu)
                            {
                                toolSripItem.Tag = lstpriv.Items[i].ToString();
                                fnd = true;
                                isitemfound = true;
                                break;
                            }
                            
                        }
                        if (!fnd )
                            toolSripItem.Visible = false;
                        
                    {                        //call recursively         
                        //disableMenu((ToolStripMenuItem)toolSripItem);
                    }
                }

                if (!isitemfound)
                {
                    menuItem.Visible = false;
                }
            }
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
            

        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Dispose();
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }


        
       

        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Environment.Exit(0);
        }

       

        private void dailyRecieptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Form childForm = new frmReport();
                childForm.MdiParent = this;
                //childForm.Text = "Window " + childFormNumber++;
                childForm.Text = "Reports Screen";
                childForm.Show();
        }

        private void clear_alldata()
        {
            try
            {


     DialogResult result = MessageBox.Show("Do you want To Clear all exisiting data?", "Clear Data", MessageBoxButtons.YesNoCancel);
     if (result == DialogResult.Yes)
     {
         Conn.Close();
         Conn.Open();
         string sql = "truncate table  recieptslog";
         cmd = new SqlCommand(sql, Conn);
         cmd.ExecuteNonQuery();
         sql = "truncate table reciepts";
         cmd = new SqlCommand(sql, Conn);
         cmd.ExecuteNonQuery();
         sql = "truncate table  employees";
         cmd = new SqlCommand(sql, Conn);
         cmd.ExecuteNonQuery();

         sql = "truncate table Leaders";
         cmd = new SqlCommand(sql, Conn);
         cmd.ExecuteNonQuery();
         sql = "truncate table  sheet_master";
         cmd = new SqlCommand(sql, Conn);
         cmd.ExecuteNonQuery();

         Conn.Close();
         MessageBox.Show("Clear Data Completed Successfully!!");
     }

                }
                    catch (System.Data.SqlClient.SqlException ex)
                        {
                        MessageBox.Show(ex.Message);


                        }






                
        }

        private void MDIParent1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void clearAllDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_alldata();
        }

       
        
       

       
        
        

        
       

       

       

        private void supplierMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv ="111";
            Form childForm = new frmSponsor();
            childForm.MdiParent = this;
           
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Entry Screen";
            childForm.Show();
        }

       

       
       

      
        
        
        
        



        private void MDIParent1_Load(object sender, EventArgs e)
        {
           // this.BackColor = Color.White;
            //b1.Left = 0;
            //b1.Top = 0;
            //b1.Width = this.Width;
            //b1.Height = this.Height;
            try
            {
                this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\Tradex.jpg");
            }
            catch(Exception ex)
            {

            }
        }


        private void helpMenu_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < this.menuStrip.Items.Count; i++)
            //{
              // GetSubMenuStripItems(this.menuStrip.Items);


            //}

           
        }

       

        private void GetSubMenuStripItems(ToolStripMenuItem menuItem) 
        {            //toolSripItems.Add(menuItem);   
            // if sub menu contain child dropdown items 
            if (menuItem.HasDropDownItems)                 
            {              
                foreach (ToolStripItem toolSripItem in menuItem.DropDownItems)   
                {
                    if (toolSripItem is ToolStripSeparator) continue;

                    if (toolSripItem is ToolStripMenuItem)
                        insert_menu(menuItem.Text, toolSripItem.Name, 1, toolSripItem.Text, "", 'A');

                    
                    
                       //toolSripItem.Enabled = false;
                {                        //call recursively         
                    GetSubMenuStripItems((ToolStripMenuItem)toolSripItem); 
                }           
                }            
            }                 
        }

        private void button1_Click(object sender, EventArgs e)
            
        {

            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

            string sql;
            Conn.Close();
            Conn.Open();
            sql = "delete from [Menu_Master]";

            SqlCommand cmd = new SqlCommand(sql, Conn);

            cmd.ExecuteNonQuery();


            sql = "delete from  [userpriv]";

             cmd = new SqlCommand(sql, Conn);

            cmd.ExecuteNonQuery();

            foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
            {
                insert_menu(toolSripItem.Text, toolSripItem.Name, 1, toolSripItem.Text, "", 'A');
                GetSubMenuStripItems(toolSripItem);


            }

            
        }

        private void insert_menu(string Head, string menu_code, int menu_type, string menu_name, string form_name, char flag)
        {

            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        
            string sql;
            Conn.Close();
                Conn.Open();
            sql = "INSERT INTO [Menu_Master]([Head],[Menu_Code],[Menu_type],[Menu_Name],[Form_name],[FLAG])";
            sql = sql  +  " VALUES ('" + Head + "','" + menu_code + "',"+ menu_type +",'" + menu_name  + "','','"+ flag +"')";

                SqlCommand cmd = new SqlCommand(sql, Conn);

                cmd.ExecuteNonQuery();
            
        }

        private void userPrivelegesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                Form childForm = new Frmuserpriv();
                childForm.MdiParent = this;
                //childForm.Text = "Window " + childFormNumber++;
                childForm.Text = "Users Priveleges Screen";
                childForm.Show();
            }
        }

        private void usersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form childForm = new FrmUsers();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Users Entry Screen";
            childForm.Show();
        }

        
        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
            Form childForm = new frmsearchveh();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Search View Screen";
            childForm.Show();
        }

       
        

        
        private void mnubrand_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnubrand.Tag.ToString();

            Gvar.Gind = 1;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Cost Center Entry Screen";
            childForm.Show();
        }

      

       
       

     

      
        private void searchViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new frmsearchveh();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Vehicle Search Screen";
            childForm.Show();
        }

        private void receivableEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = receivableEntryToolStripMenuItem.Tag.ToString();
            Gvar._trntype = 100;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;
          
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Receivable Entry/Edit Screen";
            childForm.Show();
        }

        

        private void payableEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = receivableEntryToolStripMenuItem.Tag.ToString();
            Gvar._trntype = 200;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;
          
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account payable Entry/Edit Screen";
            childForm.Show();
        }

        
        private void accountSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Summary Reports Screen";
            childForm.Show();
        }

        private void accountDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Detail Reports Screen";
            childForm.Show();
        }

        private void mnudept_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnubrand.Tag.ToString();

            Gvar.Gind = 2;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Departmemnt Entry Screen";
            childForm.Show();
        }

        private void Acccreationtool_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Acccreationtool.Tag.ToString();
            Gvar._trntype = 200;
            Form childForm = new frmAccounts();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Creation Entry/Edit Screen";
            childForm.Show();
        }

        private void mnuItemcat_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemcat.Tag.ToString();

            Form childForm = new  FrmItemCat();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Category Entry Screen";
            childForm.Show();
        }

        private void mnuItemUnit_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemUnit.Tag.ToString();
            Form childForm = new FrmItemUnit();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Unit Entry Screen";
            childForm.Show();
        }

        private void mnuItemMaster_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemMaster.Tag.ToString();
            Gvar._Gind = 1;
            Form childForm = new FrmItemMaster();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Master Screen";

            childForm.Show();
        }

        private void serviceMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = serviceMasterToolStripMenuItem.Tag.ToString();
            //childForm.Text = "Window " + childFormNumber++;
            Form childForm = new FrmSRVCMaster();
            childForm.MdiParent = this;

            childForm.Text = "Service Master Screen";
            childForm.Show();
        }

        private void mnuPurchase_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashPurchase.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashPurchase.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;
            Gvar._trntype = 1;
            Form childForm = new FrmService();
            childForm.MdiParent = this;
            

            childForm.Text = "Cash Purchase Entry Screen";

            childForm.Show();
        }

        private void mnuSales_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 6;

            childForm.Text = "Cash Sales Entry Screen";

            childForm.Show();
        }

        private void mnuCreditSale_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 7;

            childForm.Text = "Credit Sales Entry Screen";

            childForm.Show();
        }

        private void mnucrPurchase_Click(object sender, EventArgs e)
        {

            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashPurchase.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashPurchase.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 2;

            childForm.Text = "Credit Purchase Entry Screen";

            childForm.Show();
        }

        private void mnuStock_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 1;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Stock Reports Screen";
            childForm.Show();
        }

        private void mnuinvdet_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 2;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Invoice Detail Reports Screen";
            childForm.Show();
        }

        private void mnuitmbycust_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 3;
            Gvar.rptidx = 3;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item By Customer Reports Screen";
            childForm.Show();
        }

        private void mnuitmbycusbyitm_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 4;
            Gvar.rptidx = 4;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item By Customer per Item Reports Screen";
            childForm.Show();
        }

        private void mnuProjectmaster_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuProjectmaster.Tag.ToString();
            Form childForm = new frmproject();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Project Entry Screen";
            childForm.Show();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuAreaMaster.Tag.ToString();

            Gvar.Gind = 3;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Area/Location Master Entry Screen";
            childForm.Show();
        }

        private void mnuwarehouse_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuwarehouse.Tag.ToString();
            Form childForm = new Frmwrhouse();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "WareHouse Entry Screen";
            childForm.Show();

        }

        private void mnuitemTransactionDetai_Click(object sender, EventArgs e)
        {

            Gvar.Gind = 9;
            Gvar.rptidx = 9;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Transaction Detail Reports Screen";
            childForm.Show();

        }

        private void cashSalesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 3;

            childForm.Text = "Cash Sales Return Entry Screen";

            childForm.Show();
        }

        private void creditSalesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 4;

            childForm.Text = "Credit Sales Return Entry Screen";

            childForm.Show();
        }

        private void cashPurchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 8;

            childForm.Text = "Cash Purchase Return Entry Screen";

            childForm.Show();
        }

        private void creditPurchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashSales.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashSales.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new FrmService();
            childForm.MdiParent = this;
            Gvar._trntype = 9;

            childForm.Text = "Credit Purchase Return Entry Screen";

            childForm.Show();
        }

        private void projectSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 1;
            Form childForm = new frmReportProj ();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Project Reports Screen";
            childForm.Show();
        }

        private void mnuUpdateStock_Click(object sender, EventArgs e)
        {
            Gvar.update_stock();
            MessageBox.Show("Finsh Update");
        }

        private void itemCostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashPurchase.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashPurchase.Tag.ToString();
                }

               
            }
            Gvar._trntype = -1;
            Form childForm = new FrmProduct();
            childForm.MdiParent = this;


            childForm.Text = "Product Cost Entry Screen";

            childForm.Show();
        }

        private void damagedItemMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuCashPurchase.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuCashPurchase.Tag.ToString();
                }


            }
            Gvar._trntype = 11;
            Form childForm = new FrmProduct();
            childForm.MdiParent = this;


            childForm.Text = "Product Damaged Entry Screen";

            childForm.Show();

        }

       

        private void PurchaseOrderMenuItem_Click_1(object sender, EventArgs e)
        {
        Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = PurchaseOrderMenuItem.Tag.ToString();
            Form childForm = new FrmPurOrder();
            childForm.MdiParent = this;
            Gvar._trntype = 22;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Entry Screen";

            childForm.Show();
        }

        private void orderRecieptMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = orderRecieptMenuItem.Tag.ToString();
            Form childForm = new FrmPurOrder();
            childForm.MdiParent = this;
            Gvar._trntype = 2;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Reciept Screen";

            childForm.Show();
        }

        private void mnutransferMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnutransferMenuItem.Tag.ToString();
            Form childForm = new FrmTransfer();
            childForm.MdiParent = this;
            Gvar._trntype = 10;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Transfer Item Screen";

            childForm.Show();
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {

        }

        private void customerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Acccreationtool.Tag.ToString();
            Gvar._trntype = 201;
            Form childForm = new frmAccounts();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Entry/Edit Screen";
            childForm.Show();
        }

        private void SupplierMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Acccreationtool.Tag.ToString();
            Gvar._trntype = 202;
            Form childForm = new frmAccounts();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Entry/Edit Screen";
            childForm.Show();
        }

        private void cuurencyMenuItem_Click(object sender, EventArgs e)
        {

            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemcat.Tag.ToString();

            Form childForm = new Frmcurrency();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Currency Entry/Edit Screen";
            childForm.Show();

        }
        public static string find_tag(string itm)
        {

            foreach (Gvar.Priv p in Gvar.User_Priv)
            {
                if (p.Menu == itm)
                    return p.priv;
            }
            return "100";


        }

        private void rawItemsMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            Gvar._Gind = 2;
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemMaster.Tag.ToString();
            Form childForm = new FrmItemMaster();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Item Master Screen";

            childForm.Show();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgmenu.Visible = !dgmenu.Visible;
        }
        

        
        

       
      

        }

       

        
    }

