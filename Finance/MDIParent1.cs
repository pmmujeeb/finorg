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
using System.Reflection;
namespace FinOrg
{
    public partial class MDIParent1 : FinOrgForm
    {
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        ADODB.Connection ADOconn = new ADODB.Connection();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        bool menumatch = false;
        SqlCommand cmd1 = new SqlCommand();
        int mnutype = 0;
        
        Gvar.Menu_item menu_item = new Gvar.Menu_item();
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
           
            enablemenu();
            //return;
           

            switch(Gvar.menu_dock)
            {
                case "Left":
                    menuStrip.Dock = DockStyle.Left;
                    break;
                case "Top":
                    menuStrip.Dock = DockStyle.Top;
                    break;
                case "Right":
                    menuStrip.Dock = DockStyle.Right;
                    break;
            }
            
            menuStrip.Height = 35;
            if (Gvar._SuperUserid == 1) return;
            foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
            {

              
               
               
                disableMenu(toolSripItem);
               // toolSripItem.Visible = true;

            }

           
           
        }

        private void enablemenu()
        {
         ADODB.Connection ADOconn = new ADODB.Connection();
               ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
               ADODB.Recordset cus = new ADODB.Recordset();
               
            cus = new ADODB.Recordset();
            //string sql = "SELECT menu_code FROM menu_master where id in (select form_id from userpriv where dsp=1 and group_name = (select group_name from userinfo where userid='" + Gvar.Userid+ "'))";
            string sql = "";
            sql = "SELECT m.id,m.menu_code, m.menu_type,m.menu_name as Mmenu_name,m.menu_code,m.head,p.* FROM menu_master as M left join userpriv as p on  m.id=p.form_id where p.dsp=1 and p.group_name = (select group_name from userinfo where Flag='A' and  userid='" + Gvar.Userid + "') Order by id";
            if (Gvar._SuperUserid == 1)
            {
                sql = "SELECT distinct m.id, m.menu_code, m.menu_type,m.menu_name as Mmenu_name,m.menu_code,m.head,p.* FROM menu_master as M left join userpriv as p on  m.id=p.form_id where Flag='A' order by id";
            }
            cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
            while(!cus.EOF)
            {
                string mnu = cus.Fields["menu_code"].Value.ToString();
                string prv ;
                int ins, upd, del;
               

                if (string.IsNullOrEmpty(cus.Fields["ins"].Value.ToString()) || (bool)cus.Fields["ins"].Value ==false ) ins = 0; else ins=1;;
                if (string.IsNullOrEmpty(cus.Fields["upd"].Value.ToString()) || (bool)cus.Fields["upd"].Value == false) upd = 0; else upd=1;
                if (string.IsNullOrEmpty(cus.Fields["del"].Value.ToString()) || (bool)cus.Fields["del"].Value == false) del = 0; else del = 1;


                //if ((bool)cus.Fields["ins"].Value) prv = "1"; else prv = "0";
                //if ((bool)cus.Fields["upd"].Value) prv = prv + "1"; else prv = prv + "0";
                //if ((bool)cus.Fields["del"].Value) prv = prv + "1"; else prv = prv + "0";
                prv = ins.ToString() + upd.ToString() + del.ToString(); 
                if (Gvar._SuperUserid == 1)
                {
                    prv = "111";
                }
                lstmenu.Items.Add(mnu);
                lstpriv.Items.Add(prv);
                Gvar.Menu_item menu_item= new Gvar.Menu_item();
                menu_item.Menu = mnu;
                menu_item.priv = prv;
                menu_item.code = cus.Fields["menu_code"].Value.ToString();
                menu_item.mtype = cus.Fields["menu_type"].Value.ToString();
                menu_item.head = cus.Fields["head"].Value.ToString();
                
                Gvar.User_Menu.Add( menu_item);
                if (Convert.ToInt16(cus.Fields["menu_type"].Value) == 1 && !string.IsNullOrEmpty(cus.Fields["Mmenu_name"].Value.ToString()))
                {
                    var values = new [] {"MainMenu","mnuexit","mnuwindow"};

                   if( !values.Any(cus.Fields["menu_code"].Value.ToString().Contains))
                    //if (cus.Fields["menu_code"].Value.ToString().Containany.IndexOf("MainMenu","mnuexit",'mnuwindow') >0)
                    cmbMainMenu.Items.Add(cus.Fields["Mmenu_name"].Value.ToString().Replace('&',' ').Trim());
                }

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
            populate_sidemenu();
            ADOconn.Close();
            //cus.Close();
            if(cmbMainMenu.Items.Count>0)
            cmbMainMenu.SelectedIndex = 0;
            cmbMainMenu.Focus();
        }



        private void disableMenu(ToolStripMenuItem menuItem)
        {            //toolSripItems.Add(menuItem);   
            // if sub menu contain child dropdown items 
            Boolean fnd = false;
            Boolean isitemfound = false;
            string head_name;
            ToolStripMenuItem m = new ToolStripMenuItem() ;
            menuItem.Visible = true;
            if (menuItem.HasDropDownItems)
            {
                foreach (ToolStripItem toolSripItem in menuItem.DropDownItems)
                {
                    head_name = "";
                    if (toolSripItem is ToolStripSeparator)
                        if (fnd) continue;
                        else 
                        toolSripItem.Visible = false; ;
                    
                        try
                        {
                    //if (toolSripItem is ToolStripMenuItem)
                            if (toolSripItem is ToolStripSeparator)
                            {

                            }
                            else
                            {
                                m = (ToolStripMenuItem)toolSripItem;


                                if (m.HasDropDownItems)
                                {
                                    head_name = toolSripItem.Name;
                                    disableMenu(m);
                                   
                                }
                            }
                       }
                    catch(Exception ex)
                       {

                       }
                        //    insert_menu(menuItem.Text, toolSripItem.Name, 1, toolSripItem.Text, "", 'A');
                        fnd = false;
                        if (toolSripItem is ToolStripSeparator) continue;
                        for (int i = 0; i < lstmenu.Items.Count; i++)
                        {
                            string mnu =  lstmenu.Items[i].ToString();
                            if (toolSripItem.Name == mnu)
                            {
                                toolSripItem.Tag = lstpriv.Items[i].ToString();
                                fnd = true;
                               // toolSripItem.GetCurrentParent();
                                toolSripItem.Visible = true;
                               
                                isitemfound = true;
                                //continue;
                                
                            }
                            
                        }
                        if (!fnd && toolSripItem.Name != head_name )
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
            DialogResult result = MessageBox.Show("Do you want to Exit from Application?", "Exit Apps", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
                Environment.Exit(0);
               
            }
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
            int hd_det = 0;
            ToolStripMenuItem submenu;
            if (menuItem.HasDropDownItems)                 
            {              
                foreach (ToolStripItem toolSripItem in menuItem.DropDownItems)   
                {
                  
                    //call recursively   
                    hd_det = 1;
                    if (toolSripItem is ToolStripSeparator) continue;
                    submenu = (ToolStripMenuItem)toolSripItem;
                    if (submenu.HasDropDownItems ) hd_det = 0;
                    if (toolSripItem is ToolStripMenuItem)
                        insert_menu(menuItem.Text, toolSripItem.Name, mnutype, toolSripItem.Text, "", 'A', hd_det);

                    
                    
                       //toolSripItem.Enabled = false;
                {                        //call recursively   
                    mnutype = 3;
                    GetSubMenuStripItems((ToolStripMenuItem)toolSripItem);
                    mnutype = 2;
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
            sql = "truncate table [Menu_Master]";

            SqlCommand cmd = new SqlCommand(sql, Conn);

            cmd.ExecuteNonQuery();


            sql = "truncate table  [userpriv]";

             cmd = new SqlCommand(sql, Conn);

            cmd.ExecuteNonQuery();
            int hd_det = 0;
            foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
            {
                mnutype = 1;
                hd_det = 0;
               // if (toolSripItem.HasDropDownItems) hd_det = 1;
                insert_menu(toolSripItem.Text, toolSripItem.Name, mnutype, toolSripItem.Text, "", 'A', hd_det);
                mnutype = 2;
                GetSubMenuStripItems(toolSripItem);


            }

            
        }

        private void insert_menu(string Head, string menu_code, int menu_type, string menu_name, string form_name, char flag,int hd_det)
        {

            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
        
            string sql;
            Conn.Close();
                Conn.Open();
            sql = "INSERT INTO [Menu_Master]([Head],[Menu_Code],[Menu_type],[Menu_Name],[Form_name],[FLAG],Head_det)";
            sql = sql  +  " VALUES ('" + Head + "','" + menu_code + "',"+ menu_type +",'" + menu_name  + "','','"+ flag +"','" + hd_det + "')";

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
            Gvar.Gind = 3;
            Gvar.trntype = 100;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;
          
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Receivable Entry/Edit Screen";
            childForm.Show();
        }

        

        private void payableEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = receivableEntryToolStripMenuItem.Tag.ToString();
            Gvar.Gind = 4;
            Gvar.trntype = 200;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;
          
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account payable Entry/Edit Screen";
            childForm.Show();
        }

        
        private void accountSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 105;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Account Summary Reports Screen";
            childForm.Show();
        }

        private void accountDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 106;
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
            Gvar.trntype = 200;
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
            Gvar.trntype = 0;
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
            Gvar.trntype = 1;
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
            

            childForm.Text = "Cash Purchase Entry Screen";

            childForm.Show();
        }

        private void mnuSales_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 6;
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
            

            childForm.Text = "Cash Sales Entry Screen";

            childForm.Show();
        }

        private void mnuCreditSale_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 7;
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
            

            childForm.Text = "Credit Sales Entry Screen";

            childForm.Show();
        }

        private void mnucrPurchase_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 2;
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
            Gvar.trntype = 3;
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
           

            childForm.Text = "Cash Sales Return Entry Screen";

            childForm.Show();
        }

        private void creditSalesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 4;
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
           

            childForm.Text = "Credit Sales Return Entry Screen";

            childForm.Show();
        }

        private void cashPurchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 8;
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
           

            childForm.Text = "Cash Purchase Return Entry Screen";

            childForm.Show();
        }

        private void creditPurchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 9;
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
            Gvar.trntype = -1;
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
            
            Form childForm = new FrmProduct();
            childForm.MdiParent = this;


            childForm.Text = "Product Cost Entry Screen";

            childForm.Show();
        }

        private void damagedItemMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 11;
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
            
            Form childForm = new FrmProduct();
            childForm.MdiParent = this;


            childForm.Text = "Product Damaged Entry Screen";

            childForm.Show();

        }

       

        private void PurchaseOrderMenuItem_Click_1(object sender, EventArgs e)
        {
        Gvar.invno = "0";
             Gvar.trntype = 22;
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = PurchaseOrderMenuItem.Tag.ToString();
            Form childForm = new FrmPurOrder();
            childForm.MdiParent = this;
           

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Entry Screen";

            childForm.Show();
        }

        private void orderRecieptMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 2;
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = orderRecieptMenuItem.Tag.ToString();
            Form childForm = new FrmPurOrder();
            childForm.MdiParent = this;
         

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Reciept Screen";

            childForm.Show();
        }

        private void mnutransferMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 5;
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnutransferMenuItem.Tag.ToString();
            Form childForm = new FrmTransfer();
            childForm.MdiParent = this;
           

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
            Gvar.trntype = 201;
            Form childForm = new frmAccounts();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Entry/Edit Screen";
            childForm.Show();
        }

        private void SupplierMenuItem_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Acccreationtool.Tag.ToString();
            Gvar.trntype = 202;
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

            foreach (Gvar.Menu_item p in Gvar.User_Menu)
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
            Gvar.trntype = 0;
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuItemMaster.Tag.ToString();
            Form childForm = new FrmItemMaster();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Raw Meterial Master Screen";
                        childForm.Show();
         
            
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgmenu.Visible = !dgmenu.Visible;
        }

        private void cmbMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               //populate_sidemenu()
            }
            catch(Exception ex) 
            {
            
            //a
            }
        }

        private void btnarow_Click(object sender, EventArgs e)
        {
            if (dgmenu.Width > 200)
            {
                dgmenu.Width = 20;
                btnarow.Text = "->>";
            }
            else
            {
                dgmenu.Width = 208;
                btnarow.Text = "<<-";
            }
            btnarow.Width = dgmenu.Width;
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OPStockMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (OPStockMenuItem.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = OPStockMenuItem.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new FrmProduct();
            childForm.MdiParent = this;


            childForm.Text = "Product Opening Stock  Entry Screen";

            childForm.Show();
        }

        private void purchaseExpensemenu_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (purchaseExpensemenu.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = purchaseExpensemenu.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new FrmPayExpense();
            childForm.MdiParent = this;


            childForm.Text = "Product Purchase Expense  Entry Screen";

            childForm.Show();
        }

        private void salaryProcessmenu_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (purchaseExpensemenu.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = purchaseExpensemenu.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new Frmsalprocess();
            childForm.MdiParent = this;


            childForm.Text = "Salary Process Entry Screen";

            childForm.Show();

        }

        private void MDIParent1_Deactivate(object sender, EventArgs e)
        {

        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Exit from Application?", "Exit Apps", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
            {
                e.Cancel=true;
            }
        }

        private void bankReconciliationmnu_Click(object sender, EventArgs e)
        {

            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (bankReconciliationmnu.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = bankReconciliationmnu.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new FrmBankrecons();
            childForm.MdiParent = this;


            childForm.Text = "Bank Reconciliation Screen";

            childForm.Show();


        }

        private void hRMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            
            //Gvar.trntype = 0;
            //Form childForm = new Frmbrowser ();
            //childForm.MdiParent = this;


            //childForm.Text = "Salary Process Entry Screen";

            //childForm.Show();

        }

        private void assetCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (assetCreationToolStripMenuItem.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = assetCreationToolStripMenuItem.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new FrmAssetMaster();
            childForm.MdiParent = this;


            childForm.Text = "Asset Master Screen";

            childForm.Show();


        }

        private void assetCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar._Gind = 3;
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = assetCategoryToolStripMenuItem.Tag.ToString();

            Form childForm = new FrmItemCat();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "ASSET Category Entry Screen";
            childForm.Show();
        }

        private void assetSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 17;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Asset Reports Screen";
            childForm.Show();
        }

        private void salaryPaymentmnu_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (salaryPaymentmnu.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = salaryPaymentmnu.Tag.ToString();
                }


            }
            Gvar.trntype = 0;
            Form childForm = new Frmsalpaid();
            childForm.MdiParent = this;


            childForm.Text = "Salary Payment Entry Screen";

            childForm.Show();
        }

        private void purchaseOrdermnu_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 18;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Purchase Order Reports Screen";
            childForm.Show();
        }

        private void packingSlipmnu_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 19;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Packing Slip Reports Screen";
            childForm.Show();
        }

        private void employeeInfoMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (employeeInfoMenuItem.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = employeeInfoMenuItem.Tag.ToString();
                }


            }
            
            Form childForm = new FrmHRMS();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Employee Info Screen";
            childForm.Show();
        }

        private void deparmentsmenu_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = deparmentsmenu.Tag.ToString();

            Gvar.Gind = 9;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Department Entry Screen";
            childForm.Show();
        }

        private void Positionsmenu_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = Positionsmenu.Tag.ToString();

            Gvar.Gind = 10;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Position Entry Screen";
            childForm.Show();
        }

        private void nationalitiesmenu_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = nationalitiesmenu.Tag.ToString();

            Gvar.Gind = 11;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Nationality Entry Screen";
            childForm.Show();
        }

        private void employeesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 20;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Employee List Reports Screen";
            childForm.Show();
        }

        private void employeesRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 21;
            Form childForm = new frmreport1();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Employee Records";
            childForm.Show();

        }

        private void mnuincomerpt_Click(object sender, EventArgs e)
        {

            Gvar.Gind = 12;
            Gvar.rptidx = 1;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Income and Expense Reports Screen";
            childForm.Show();

        }

        private void mnutrailbalance_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 13;
            Gvar.rptidx = 1;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Trial Balance Reports Screen";
            childForm.Show();

        }

        private void mnuprofitnloss_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 14;
            Gvar.rptidx = 1;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Profit amd Loss  Reports Screen";
            childForm.Show();


        }

        private void mnubalancesheet_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 15;
            Gvar.rptidx = 1;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Balance Sheet Reports Screen";
            childForm.Show();
        }

        private void mnucusreceivale_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnucusreceivale.Tag.ToString();
            Gvar.Gind = 1;
            Gvar.trntype = 100;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Receivable Entry/Edit Screen";
            childForm.Show();
        }

        private void mnusuppayable_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnusuppayable.Tag.ToString();
            Gvar.Gind = 2;
            Gvar.trntype = 200;
            Form childForm = new frmAccTran();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Payable Entry/Edit Screen";
            childForm.Show();
        }

        private void mnucusSummary_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 101;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Account Summary Reports Screen";
            childForm.Show();
        }

        private void mnucusdet_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 102;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Customer Account Detail Reports Screen";
            childForm.Show();
        }

        private void mnuSupsummary_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 1;
            Gvar.rptidx = 103;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Account Summary Reports Screen";
            childForm.Show();
        }

        private void mnuSupdet_Click(object sender, EventArgs e)
        {
            Gvar.Gind = 2;
            Gvar.rptidx = 104;
            Form childForm = new frmReport();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Supplier Account Detail Reports Screen";
            childForm.Show();
        }

        private void mnuentrysetting_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuentrysetting.Tag.ToString();

            Gvar.Gind = 12;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Entry Settingr Entry Screen";
            childForm.Show();
        }

        private void mnuglentrysetting_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuglentrysetting.Tag.ToString();

            Gvar.Gind = 13;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "GL Entry Settingr Entry Screen";
            childForm.Show();
        }

        private void mnusetting_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnusetting.Tag.ToString();

            Gvar.Gind = 14;
            Form childForm = new FrmBrand();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "Entry Settingr Entry Screen";
            childForm.Show();

        }

        private void mnuglvoucher_Click(object sender, EventArgs e)
        {
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111"; else Gvar.frm_priv = mnuglvoucher.Tag.ToString();
            Gvar.Gind = 5;
            Gvar.trntype = 100;
            Form childForm = new frmGLTran();
            childForm.MdiParent = this;

            //childForm.Text = "Window " + childFormNumber++;
            childForm.Text = "GL Voucher Entry/Edit Screen";
            childForm.Show();
        }

        private void mnuitemadjust_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuadjustaddition_Click(object sender, EventArgs e)
        {
            Gvar.trntype = -2;
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuadjustaddition.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuadjustaddition.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new Frmadjust();
            childForm.MdiParent = this;


            childForm.Text = "Adjust entry Item Addition Entry Screen";

            childForm.Show();
        }

        private void mnuadjustdeletion_Click(object sender, EventArgs e)
        {
            Gvar.trntype = 17;
            Gvar.invno = "0";
            if (Gvar._SuperUserid == 1) Gvar.frm_priv = "111";
            else
            {
                if (mnuadjustdeletion.Tag == null)
                {
                    Gvar.frm_priv = "100";
                }
                else
                {
                    Gvar.frm_priv = mnuadjustdeletion.Tag.ToString();
                }

            }
            //childForm.Text = "Window " + childFormNumber++;

            Form childForm = new Frmadjust();
            childForm.MdiParent = this;


            childForm.Text = "Adjust entry Item Deletion Entry Screen";

            childForm.Show();
        }

        private void MDIParent1_Enter(object sender, EventArgs e)
        {
            dgmenu.Visible = true;
        }

        
        private void MDIParent1_Leave(object sender, EventArgs e)
        {

        }

        private void MDIParent1_Activated(object sender, EventArgs e)
        {
            dgmenu.Height = this.Height;
            dgmenu.Top = menuStrip.Top + menuStrip.Height + 2;

        }

        private void MDIParent1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void MDIParent1_Validated(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            try
            {
                int ky = msg.WParam.ToInt32();
                if (msg.WParam.ToInt32() == 18 )
                {
                    if (dgmenu.Width == 200)
                    {
                        dgmenu.Width = 2;
                        dgmenu.Focus();

                    }
                    else
                    {
                        dgmenu.Width = 200;
                        dgmenu.Columns[0].Width = 197;
                    }
                    
                }

                








                    //return true;
               
                //return base.ProcessCmdKey(ref msg, Keys.Up);
                //return base.ProcessCmdKey(ref msg, keyData);


                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);

            }
        }
        private void MDIParent1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void MDIParent1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void dgmenu_MouseMove(object sender, MouseEventArgs e)
        {
            dgmenu.Width = 200;
            dgmenu.Columns[0].Width = 197;
            dgmenu.Focus();
        }

        private void dgmenu_MouseLeave(object sender, EventArgs e)
        {
            dgmenu.Width = 2;
        }

        private void dgmenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            menumatch = false;
            foreach (ToolStripMenuItem toolSripItem in menuStrip.Items)
            {
              
            if (toolSripItem.HasDropDownItems)                 
            {
             bool fnd =   SerachSubMenuStripItems(toolSripItem, dgmenu[1,e.RowIndex].Value.ToString());
                if (fnd)
                {
                    return;

                }

            }

        
        }
        }
        

        
         private bool SerachSubMenuStripItems(ToolStripMenuItem menuItem, string menu_name) 
        {            

            int hd_det = 0;
           
            ToolStripMenuItem submenu;
            if (menuItem.HasDropDownItems)                 
            {              
                foreach (ToolStripItem toolSripItem in menuItem.DropDownItems)   
                {
                    if (menumatch)
                    {
                        return true;
                    }
                    //call recursively   
                    hd_det = 1;
                    if (toolSripItem is ToolStripSeparator) continue;
                    submenu = (ToolStripMenuItem)toolSripItem;
                    if (submenu.HasDropDownItems ) hd_det = 0;
                    if (toolSripItem is ToolStripMenuItem)
                    { 
                        if(toolSripItem.Name == menu_name)
                        {
                            menumatch = true;
                            toolSripItem.PerformClick();

                            return true;
                        }
                    }

                {
                    if (!menumatch)
                    SerachSubMenuStripItems((ToolStripMenuItem)toolSripItem, menu_name);
                   
                }           
                }
               
            }
            return false;  
        }

         private void mnuxpressmenu_Click(object sender, EventArgs e)
         {
             {
                 Form childForm = new Frmxmenu();
                 childForm.MdiParent = this;
                 //childForm.Text = "Window " + childFormNumber++;
                 childForm.Text = "Users Xpress Menu Screen";
                 childForm.Show();
             }
         }

		 private void btnlang_Click(object sender, EventArgs e)
		 {
			 try
			 {
				 if (Languages.currentLanguage.Simplified(true) == "english")
				 {
					Languages.ChangeLanguage("arabic");
				 }
				 else
				 {
					Languages.ChangeLanguage("english");
				 }
			 }
			 catch
			 {

			 }
		 }


       
      private void populate_sidemenu()
         {
             try
             {



                 //ADODB.Connection ADOconn = new ADODB.Connection();
              if(ADOconn.State==0)
                 ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);
              
                 
                 ADODB.Recordset cus = new ADODB.Recordset();
               
            cus = new ADODB.Recordset();
            //string sql = "SELECT menu_code FROM menu_master where id in (select form_id from userpriv where dsp=1 and group_name = (select group_name from userinfo where userid='" + Gvar.Userid+ "'))";
            string sql = "";
            sql = "SELECT m.id,m.menu_code,m.menu_name as Mmenu_name FROM menu_xpress as M inner join userpriv as p on  m.id=p.form_id where  m.group_name = (select group_name from userinfo where userid='" + Gvar.Userid + "') Order by id";
            if (Gvar._SuperUserid == 1)
            {
               // sql = "SELECT distinct m.id, m.menu_code, m.menu_type,m.menu_name as Mmenu_name,m.menu_code,m.head,p.* FROM menu_master as M left join userpriv as p on  m.id=p.form_id order by id";
            }
            cus.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
           
                 dgmenu.Rows.Clear();
                 int i = 0;

                 while (!cus.EOF)
                 {


                     dgmenu.Rows.Add(1);

                 dgmenu[0, i].Value = cus.Fields["Mmenu_name"].Value;
                 dgmenu[1, i].Value = cus.Fields["menu_code"].Value; ;
                 dgmenu[2, i].Value = cus.Fields["id"].Value; ;
                         i++;

                 //foreach (var item in Gvar.User_Menu)
                 //{
                 //    string head = item.head.Replace('&', ' ').Trim();
                 //    string mnu = item.Menu.Replace('&', ' ').Trim();
                 //    if (cmbMainMenu.Text == head && cmbMainMenu.Text != mnu && mnu != "")
                 //    {
                 //        // if (dgmenu.Rows.Count<=i)
                 //        dgmenu.Rows.Add(1);
                 //        dgmenu[0, i].Value = mnu;
                 //        dgmenu[1, i].Value = item.code;
                 //        dgmenu[2, i].Value = item.priv;
                 //        i++;
                 //        //dgmenu.Rows.Add(1);
                 //        //dgmenu[1, i].Value = item.


                 //    }

                         cus.MoveNext();
                 }

                         if (dgmenu.Rows.Count == 0) dgmenu.Visible = false;
                // dgmenu.EndEdit();
                 //if (dgmenu[0, i].Value == null)
                 //{
                 //    dgmenu.EndEdit();
                 //    dgmenu.Rows.RemoveAt(i);
                 //}
                 //dgmenu.Columns[0].Width = dgmenu.Width - 5;
                 //if (dgmenu.Rows.Count > 0)
                 //{
                 //    dgmenu.Focus();


                 //}
             }
          catch(Exception ex)
             {

             }
         }

      private void mnuitemmovement_Click(object sender, EventArgs e)
      {
          Gvar.Gind = 1;
          Gvar.rptidx = 22;
          Form childForm = new frmreport1();
          childForm.MdiParent = this;
          //childForm.Text = "Window " + childFormNumber++;
          childForm.Text = "Item Movement Reports Screen";
          childForm.Show();
      }

      private void mnudailyTransaction_Click(object sender, EventArgs e)
      {
          Gvar.Gind = 1;
          Gvar.rptidx = 23;
          Form childForm = new frmreport1();
          childForm.MdiParent = this;
          //childForm.Text = "Window " + childFormNumber++;
          childForm.Text = "Daily Transaction Reports Screen";
          childForm.Show();

      }

      private void mnudailyCashBook_Click(object sender, EventArgs e)
      {
          Gvar.Gind = 3;
          Gvar.rptidx = 107;
          Form childForm = new frmReport();
          childForm.MdiParent = this;
          //childForm.Text = "Window " + childFormNumber++;
          childForm.Text = "Cash Flow Detail Reports Screen";
          childForm.Show();
      }

        }

       

        
    }

