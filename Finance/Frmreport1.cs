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
//using Microsoft.VisualBasic;//
using CrystalDecisions.CrystalReports.Engine;
using System.Threading;
namespace FinOrg
{


    public partial class frmreport1 : Form
        
    {
        
         SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
         //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
        
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);


        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataView dv1 = new DataView();
        DataView dv2 = new DataView();
        DataView dv3 = new DataView();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter ada = new SqlDataAdapter();
        string cus_code = "-1";
         string sql;
        string crt1;
            string  CRT2;
        string CRT3;
            bool fnd;
        string RPTHEAD;
        string rep_path;
        string rep_formula;
   
        public frmreport1()
        {
            InitializeComponent();
            ini_tab();
        
            try
                 
            {
                    Conn.Close();
                    Conn.Open();

                    //sql = "sELECT  Leader_no,Leader_name from Leaders ";


                    //SqlCommand cmd = new SqlCommand(sql, Conn);
                    //SqlDataReader rd = cmd.ExecuteReader();

                    //lstleaderid.Items.Add("All");
                    //lstleadername.Items.Add("All");
                    //while (rd.Read())
                    //{
                    //    lstleaderid.Items.Add(rd["Leader_no"]);
                    //    lstleadername.Items.Add(rd["Leader_name"]);
                        
                    //}
                   

              switch (Gvar.rptidx)
                {

                    case 1:
                        grpdate.Visible = false;
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Item Category";
                        cmb1.Visible = true;
                        string sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

                        SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                        DataTable dt2 = new DataTable("ITEM_CAT");
                        ada2.Fill(dt2);
                        cmb1.DisplayMember = "itm_cat_name";
                        cmb1.ValueMember = "itm_cat_code";

                        chkminstock.Visible = true;
                
                        cmb1.DataSource = dt2;
                        chkstock.Visible=true;;
                        break;

                    case 17:
                        grpdate.Visible = false;
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Asset Category";
                        cmb1.Visible = true;
                         sql = "sELECT   ast_cat_code,ast_CAT_name froM asset_CAT UNION sELECT   -10,'All'  ";

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("ITEM_CAT");
                        ada2.Fill(dt2);
                        cmb1.DisplayMember = "ast_CAT_name";
                        cmb1.ValueMember = "ast_cat_code";

                        chkminstock.Visible = true;
                        chkminstock.Text = "Show Only With Value";
                        cmb1.DataSource = dt2;
                        chkstock.Visible = false; ;
                        break;
                case 2:
                case 3:
                case 4:
                case 9:
                        #region 2-4
                        if (Gvar.rptidx == 4)
                        {
                            chkstock.Text = "Hide Detail";
                            chkstock.Visible = true;

                        }
                        grpdate.Visible = false;
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "WareHouse";
                        cmb1.Visible = true;
                         sql = "sELECT   WR_code,WR_name froM WRHOUSe_MASTER Union sELECT   0,'All' froM WRHOUSe_MASTER  ";

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("WRHOUS_MASTER");
                        ada2.Fill(dt2);
                        cmb1.DisplayMember = "WR_name";
                        cmb1.ValueMember = "WR_code";


                
                        cmb1.DataSource = dt2;
                        //cmbcatcode.DataSource = dt2;
                        grpdate.Visible = true;        
                        lblcmb2.Visible = true;
                        lblcmb2.Text = "Transaction";
                        cmb2.Visible = true;
                        sql = "select top 1 cus_ac_type ,sup_ac_type from ac_options WHERE  ac_options.ID =1";
                        bool find;
            
                        //rd.Close();
                        SqlCommand cmd = new SqlCommand(sql, Conn);
                        SqlDataReader rd = cmd.ExecuteReader();
                        find = false;
                        int ac_code=0;
                        

                        while (rd.Read())
                        {

                            cus_code =  rd[0].ToString() + "," + rd[1].ToString();
                            find = true;

                        }
                        if (!find)
                        {
                            MessageBox.Show("Please Define the Supplier/Customer Account Type Code on Ac_option Table on Database", "Wrong Account Type Code");
                            return;
                        }
                        rd.Close();


                         sql = "SELECT  Trn_Name,CODES froM Trn_Type_rep  order by TRN_CODE";

                    SqlDataAdapter trnad = new SqlDataAdapter(sql, Conn);
                    DataTable trndt = new DataTable("trntype");
                    trnad.Fill(trndt);

                    cmb2.DisplayMember = "Trn_Name";
                    cmb2.ValueMember = "CODES";
                    cmb2.DataSource = trndt;
                    cmb2.SelectedIndex = 0;


                        //sql = "sELECT  TRN_CODE,TRN_NAME froM TRN_TYPE   UNION SELECT TRN_CODE,TRN_NAME froM TRN_TYPE_REP";

                        //             SqlDataAdapter adaacc = new SqlDataAdapter(sql, Conn);
                        //             DataTable dtacc = new DataTable("TRN_TYPE");
                            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();


                            //adaacc.Fill(dtacc);
                            //cmb2.DisplayMember = "TRN_NAME";
                            //cmb2.ValueMember = "TRN_CODE";

                        
                       
                            chk.HeaderText = "Select";
                            chk2.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst1.Columns.Add(chk);
                            lst2.Columns.Add(chk2);
                            chk.ReadOnly = false;
                       
                                   // cmb2.DataSource = dtacc;



                        //cmbcatcode.DataSource = dt2;

                        panel1.Visible = true;
                        lbllst1.Text = "Inventory Items";

                        sql = "sELECT  Item_Code,Description,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("HD_ITEMMASTER");
                         ada2.Fill(dt2);
                        
                         dv.Table = dt2;
                         //dv1.Table = dt2;
                         lst1.DataSource = dv;
                         dv.Sort = "Description";
                         lst1.Columns[0].Width = 50;
                        //lst1.Columns[3].Visible = false;
                         lst1.Columns[2].Width = 300;
                         lst1.Columns[1].Width = 175;
                         lst1.ReadOnly = false;
                        lst1.Columns[2].ReadOnly = true;
                        lst1.Columns[1].ReadOnly = true;
                         lst1.Columns[0].ReadOnly = false;
                         lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                         sql = "sELECT  ACC_TYPE_CODE,ACC_TYPE_NAME froM ACC_TYPE where ACC_TYPE_CODE in (2,3)   UNION sELECT  -10,'All' ";

                                     SqlDataAdapter adaactype = new SqlDataAdapter(sql, Conn);
                                     DataTable dtactype = new DataTable("ACC_TYPE");
                            //DataGridViewCheckBoxColumn chkacc = new DataGridViewCheckBoxColumn();


                                     cmb3.Visible = true;
                                     adaactype.Fill(dtactype);
                                     cmb3.DataSource = dtactype;
                            cmb3.DisplayMember = "ACC_TYPE_NAME";
                            cmb3.ValueMember = "ACC_TYPE_CODE";
                            lblcmb3.Visible = true;
                            lblcmb3.Text = "Acc. Type";
                            cmb3.SelectedIndex=0;
                        
                       
                            chk.HeaderText = "Select";
                            chk2.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                           
                         
                         
                         
                         panel2.Visible = true;

                         string crt1 = "";

                        lbllst2.Text = "Accounts";
                        if (cmb3.SelectedIndex == 0)
                            crt1 = " acc_type_code in (2,3)";

                        else crt1 = " acc_type_code =" + cmb3.SelectedValue;

                        sql = "sELECT  cast(ACC_NO as varchar) as acc_no,ACC_NAME,row_number() over (order by ACC_NAME) as Rownum froM ACCOUNTS where " + crt1;

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("accounts");
                         ada2.Fill(dt2);
                        
                         
                         dv2.Table = dt2;
                         lst2.DataSource = dv2;
                         dv2.Sort = "ACC_NAME";
                         lst2.Columns[0].Width = 50;
                        //lst1.Columns[3].Visible = false;
                         lst2.Columns[2].Width = 300;
                         lst2.Columns[1].Width = 175;
                         lst2.ReadOnly = false;
                         lst2.Columns[2].ReadOnly = true;
                         lst2.Columns[1].ReadOnly = true;
                         lst2.Columns[0].ReadOnly = false;
                         lst2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


                        
                        break;
                    case 30:
                        grpdate.Visible = false;
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Material Category";
                        cmb1.Visible = true;
                        sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

               ada2 = new SqlDataAdapter(sql, Conn);
                 dt2 = new DataTable("ITEM_CAT");
                ada2.Fill(dt2);
                cmb1.DisplayMember = "itm_cat_name";
                cmb1.ValueMember = "itm_cat_code";


                
                cmb1.DataSource = dt2;
                //cmbcatcode.DataSource = dt2;


                        break;
                        #endregion 2-4
                    case 41:
                        grpdate.Visible = false;
                        break;

                    case 5: case 6: case 18: case 19:
                        grpdate.Visible = true;        
                lblcmb1.Visible = true;
                        lblcmb1.Text = "Supplier";
                        cmb1.Visible = true;
                        sql = "select top 1 sup_ac_type from ac_options WHERE  ac_options.ID =1";
            
            
            //rd.Close();
             cmd = new SqlCommand(sql, Conn);
             rd = cmd.ExecuteReader();
            find = false;
             ac_code=0;

            while (rd.Read())
            {

                ac_code = Convert.ToInt32(rd[0].ToString());
                find = true;

            }
            if (!find)
            {
                MessageBox.Show("Please Define the Supplier Account Type Code on Ac_option Table on Database", "Wrong Account Type Code");
                return;
            }
            rd.Close();


            


            sql = "sELECT  ACC_NO,ACC_NAME froM ACCOUNTS where acc_type_code=" + ac_code + " UNION sELECT  0,'All' froM ACCOUNTS";

            SqlDataAdapter adaacc = new SqlDataAdapter(sql, Conn);
            DataTable dtacc = new DataTable("ACCOUNTS");

                adaacc.Fill(dtacc);
                cmb1.DisplayMember = "ACC_NAME";
                cmb1.ValueMember = "ACC_NO";

                        
                         chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                lst1.Columns.Add(chk);
                chk.ReadOnly = false;
                        cmb1.DataSource = dtacc;



                        //cmbcatcode.DataSource = dt2;

                        panel1.Visible = true;
                        lbllst1.Text = "Material Items";

                        sql = "sELECT  Item_Code,Description,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("HD_ITEMMASTER");
                         ada2.Fill(dt2);
                        
                         dv.Table = dt2;
                         dv1.Table = dt2;
                         lst1.DataSource = dv;
                         dv.Sort = "Description";
                         lst1.Columns[0].Width = 50;
                        lst1.Columns[3].Visible = false;
                        lst1.Columns[1].Width = 175;
                         lst1.Columns[2].Width = 300;
                         lst1.ReadOnly = false;
                        lst1.Columns[2].ReadOnly = true;
                        lst1.Columns[1].ReadOnly = true;
                         lst1.Columns[0].ReadOnly = false;
                         lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        break;


                    case 7:
                        grpdate.Visible = true; 
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Cost Category";
                        cmb1.Visible = true;
                      
                        find = false;

                        sql = "sELECT  COST_CODE,COST_NAME froM COST_MASTER union select 0 , 'All' from COST_MASTER";

                       SqlDataAdapter adacmb1 = new SqlDataAdapter(sql, Conn);
                    // cmd = new SqlCommand(sql, Conn);

                    //     rd = cmd.ExecuteReader();
                    //     cmb1.Items.Clear();
                    //     cmb1.Items.Add("All");
                   
                        

                    //while (rd.Read())
                    //{
                    //    Gvar.ComboboxItem itm= new Gvar.ComboboxItem();
                    //    itm.Text=rd["COST_NAME"].ToString();
                    //    itm.Value=rd["COST_CODE"];

                    //    cmb1.Items.Add(itm);
                      
                    //}

                    //if (cmb1.Items.Count > 0) cmb1.SelectedIndex = 1;
                    //rd.Close();
                        
                        DataTable dtcmb1 = new DataTable("COST_MASTER");

                        adacmb1.Fill(dtcmb1);
                        cmb1.DisplayMember = "COST_NAME";
                        cmb1.ValueMember = "COST_CODE";
                        cmb1.DataSource = dtcmb1;

                        lblcmb2.Visible = true;
                        lblcmb2.Text = "Projects";
                        cmb2.Visible = true;


                         chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = "Select";
                        //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                        lst1.Columns.Add(chk);
                        chk.ReadOnly = false;

                        sql = "sELECT  0 as Proj_code,'All' as Proj_name  froM PROJ_MASTER Union sELECT  PROJ_CODE,PROJ_NAME froM PROJ_MASTER ";



                        SqlDataAdapter adacmb2 = new SqlDataAdapter(sql, Conn);
                        DataTable dtcmb2 = new DataTable("PROJ_MASTER");
                         adacmb2.Fill(dtcmb2);
                         cmb2.DisplayMember = "PROJ_NAME";
                        cmb2.ValueMember = "PROJ_CODE";
                        cmb2.DataSource = dtcmb2;
                       
                       // sql = "sELECT  Site_code,Site_Name,row_number() over (order by Site_Code) as Rownum froM site_master  where proj_code=" + cmb2.SelectedValue ;

                       //ada2 = new SqlDataAdapter(sql, Conn);
                       // dt2 = new DataTable("site_master");
                       
                       // ada2.Fill(dt2);
                        

                       // dv.Table = dt2;
                       // dv1.Table = dt2;
                       // lst1.DataSource = dv;
                        dv.Sort = "Site_Name";
                        panel1.Visible = true;
                        lbllst1.Text = "Site Name";
                        lst1.Columns[0].Width = 50;
                        lst1.Columns[3].Visible = false;
                        lst1.Columns[2].Width = 300;
                        lst1.Columns[1].Width = 100;
                        lst1.ReadOnly = false;
                        lst1.Columns[2].ReadOnly = true;
                        lst1.Columns[1].ReadOnly = true;
                        lst1.Columns[0].ReadOnly = false;
                        lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        break;
                    case 8:
                        
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Material Category";
                        cmb1.Visible = true;
                        sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

               ada2 = new SqlDataAdapter(sql, Conn);
                 dt2 = new DataTable("ITEM_CAT");
                ada2.Fill(dt2);
                cmb1.DisplayMember = "itm_cat_name";
                cmb1.ValueMember = "itm_cat_code";


                
                cmb1.DataSource = dt2;
                //cmbcatcode.DataSource = dt2;
                        panel1.Visible = true;
                        lbllst1.Text = "Material Items";

                         
                      chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                lst1.Columns.Add(chk);
                chk.ReadOnly = false;
                        


                        sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("HD_ITEMMASTER");
                         ada2.Fill(dt2);
                        
                         dv.Table = dt2;
                         dv1.Table = dt2;
                         lst1.DataSource = dv;
                         dv.Sort = "Description";
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
                                               
           case 90:
                        grpdate.Visible = true;
                         lblcmb2.Visible = true;
                        lblcmb2.Text = "WareHouse";
                        cmb2.Visible = true;
                         sql = "sELECT   WR_code,WR_name froM WRHOUSe_MASTER Union sELECT   0,'All' froM WRHOUSe_MASTER  ";

                SqlDataAdapter adawh = new SqlDataAdapter(sql, Conn);
                DataTable whdt = new DataTable("WRHOUSe_MASTER");
                adawh.Fill(whdt);
                cmb2.DisplayMember = "WR_name";
                cmb2.ValueMember = "WR_code";
                cmb2.DataSource = whdt;

                        lblcmb1.Visible = true;
                        lblcmb1.Text = "Material Category";
                        cmb1.Visible = true;
                        sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("ITEM_CAT");
                        ada2.Fill(dt2);
                        cmb1.DisplayMember = "itm_cat_name";
                        cmb1.ValueMember = "itm_cat_code";



                        cmb1.DataSource = dt2;
                        //cmbcatcode.DataSource = dt2;
                        panel1.Visible = true;
                        lbllst1.Text = "Material Items";


                        chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = "Select";
                        //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                        lst1.Columns.Add(chk);
                        chk.ReadOnly = false;



                        sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("HD_ITEMMASTER");
                        ada2.Fill(dt2);

                        dv.Table = dt2;
                        dv1.Table = dt2;
                        lst1.DataSource = dv;
                        dv.Sort = "Description";
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
                   


           case 10:
           case 11:
                                      
                            chk2 = new DataGridViewCheckBoxColumn();
                            chk2.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst2.Columns.Add(chk2);
                            chk2.ReadOnly = false;
                            grpdate.Visible = true;
                            lblcmb2.Visible = true;
                            lblcmb2.Text = "Projects";
                            cmb2.Visible = true;
                            sql = "sELECT  0 as Proj_code,'All' as Proj_name  froM PROJ_MASTER Union sELECT  PROJ_CODE,PROJ_NAME froM PROJ_MASTER ";
                            adacmb2 = new SqlDataAdapter(sql, Conn);
                            dtcmb2 = new DataTable("PROJ_MASTER");
                            adacmb2.Fill(dtcmb2);
                            cmb2.DisplayMember = "PROJ_NAME";
                            cmb2.ValueMember = "PROJ_CODE";
                            cmb2.DataSource = dtcmb2;

                            lblcmb1.Visible = true;
                            lblcmb1.Text = "Material Category";
                            cmb1.Visible = true;
                            sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

                            ada2 = new SqlDataAdapter(sql, Conn);
                            dt2 = new DataTable("ITEM_CAT");
                            ada2.Fill(dt2);
                            cmb1.DisplayMember = "itm_cat_name";
                            cmb1.ValueMember = "itm_cat_code";

                            cmb1.DataSource = dt2;
                            //cmbcatcode.DataSource = dt2;
                            panel1.Visible = true;
                            lbllst1.Text = "Material Items";

                            chk = new DataGridViewCheckBoxColumn();
                            chk.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst1.Columns.Add(chk);
                            chk.ReadOnly = false;

                            sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                            ada2 = new SqlDataAdapter(sql, Conn);
                            dt2 = new DataTable("HD_ITEMMASTER");
                            ada2.Fill(dt2);

                            dv.Table = dt2;
                            dv1.Table = dt2;
                            lst1.DataSource = dv;
                            dv.Sort = "Description";
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
                
                    case 12:
                    
                            grpdate.Visible = true;
                            lblcmb1.Visible = true;
                            lblcmb1.Text = "Supplier";
                            cmb1.Visible = true;
                            sql = "select top 1 sup_ac_type from ac_options WHERE  ac_options.ID =1";


                            //rd.Close();
                            cmd = new SqlCommand(sql, Conn);
                            rd = cmd.ExecuteReader();
                            find = false;

                            ac_code=0;
                            while (rd.Read())
                            {

                                ac_code = Convert.ToInt32(rd[0].ToString());
                                find = true;

                            }
                            if (!find)
                            {
                                MessageBox.Show("Please Define the Supplier Account Type Code on Ac_option Table on Database", "Wrong Account Type Code");
                                return;
                            }
                            rd.Close();





                            sql = "sELECT  ACC_NO,ACC_NAME froM ACCOUNTS where acc_type_code=" + ac_code + " UNION sELECT  0,'All' froM ACCOUNTS";

                            adaacc = new SqlDataAdapter(sql, Conn);
                            dtacc = new DataTable("ACCOUNTS");

                            adaacc.Fill(dtacc);
                            cmb1.DisplayMember = "ACC_NAME";
                            cmb1.ValueMember = "ACC_NO";


                            DataGridViewCheckBoxColumn chk1 = new DataGridViewCheckBoxColumn();
                            chk1.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst1.Columns.Add(chk1);
                            chk1.ReadOnly = false;
                            cmb1.DataSource = dtacc;


                        lblcmb2.Visible = true;
                        lblcmb2.Text = "WareHouse";
                        cmb2.Visible = true;



                        //sql = "sELECT  0 as Proj_code,'All' as Proj_name  froM PROJ_MASTER Union sELECT  PROJ_CODE,PROJ_NAME froM PROJ_MASTER ";

                         sql = "sELECT   WR_code,WR_name froM WRHOUSe_MASTER Union sELECT   0,'All' froM WRHOUSe_MASTER  ";

                         adacmb2 = new SqlDataAdapter(sql, Conn);
                         dtcmb2 = new DataTable("WRHOUSe_MASTER");
                         adacmb2.Fill(dtcmb2);
                         cmb2.DisplayMember = "WR_name";
                         cmb2.ValueMember = "WR_code";
                        cmb2.DataSource = dtcmb2;


                            break;
              case 13:
                   

               grpdate.Visible = true;
                  chk2 = new DataGridViewCheckBoxColumn();
                            chk2.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst1.Columns.Add(chk2);
                            chk2.ReadOnly = false;
                            grpdate.Visible = true;
                            lblcmb2.Visible = true;
                            lblcmb2.Text = "Projects";
                            cmb2.Visible = true;
                            cmb2.SelectedIndex =- 1;
                            sql = "sELECT  0 as Proj_code,'All' as Proj_name  froM PROJ_MASTER Union sELECT  PROJ_CODE,PROJ_NAME froM PROJ_MASTER ";
                            adacmb2 = new SqlDataAdapter(sql, Conn);
                            dtcmb2 = new DataTable("PROJ_MASTER");
                            adacmb2.Fill(dtcmb2);
                            cmb2.DisplayMember = "PROJ_NAME";
                            cmb2.ValueMember = "PROJ_CODE";
                            cmb2.DataSource = dtcmb2;

                          dv.Sort = "Site_Name";
                          if (lst1.ColumnCount > 2)
                          {
                              lst1.Columns[0].Width = 50;
                              lst1.Columns[3].Visible = false;
                              lst1.Columns[2].Width = 300;
                              lst1.Columns[1].Width = 100;
                              lst1.ReadOnly = false;
                              lst1.Columns[2].ReadOnly = true;
                              lst1.Columns[1].ReadOnly = true;
                              lst1.Columns[0].ReadOnly = false;
                          }
                        lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        break; 
               
               
                    case 14:
                        Conn.Close();
                Conn.Open();



                         grpdate.Visible = true;
                  chk2 = new DataGridViewCheckBoxColumn();
                            chk2.HeaderText = "Select";
                            //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                            lst1.Columns.Add(chk2);
                            chk2.ReadOnly = false;
                            grpdate.Visible = true;
                            lblcmb2.Visible = true;
                            lblcmb2.Text = "Projects";
                            cmb2.Visible = true;
                            cmb2.SelectedIndex =- 1;
                            sql = "sELECT  0 as Proj_code,'All' as Proj_name  froM PROJ_MASTER Union sELECT  PROJ_CODE,PROJ_NAME froM PROJ_MASTER ";
                            adacmb2 = new SqlDataAdapter(sql, Conn);
                            dtcmb2 = new DataTable("PROJ_MASTER");
                            adacmb2.Fill(dtcmb2);
                            cmb2.DisplayMember = "PROJ_NAME";
                            cmb2.ValueMember = "PROJ_CODE";
                            cmb2.DataSource = dtcmb2;

                          dv.Sort = "Site_Name";
                          if (lst1.ColumnCount > 2)
                          {
                              lst1.Columns[0].Width = 50;
                              lst1.Columns[3].Visible = false;
                              lst1.Columns[2].Width = 300;
                              lst1.Columns[1].Width = 100;
                              lst1.ReadOnly = false;
                              lst1.Columns[2].ReadOnly = true;
                              lst1.Columns[1].ReadOnly = true;
                              lst1.Columns[0].ReadOnly = false;
                          }
                        lst1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


                        sql = "select Cost_code, Cost_name from Cost_master UNION select 0, 'All' from Cost_master";

                SqlDataAdapter cost = new SqlDataAdapter(sql, Conn);
                ///ada.TableMappings.Add("Table", "Leaders");

                DataSet dscost = new DataSet();



                cost.Fill(dscost, "Cost_master");
                cmb3.DisplayMember = "Cost_name";
                cmb3.ValueMember = "Cost_code";
                cmb3.DataSource = dscost.Tables[0];
                cmb3.Visible = true;
                lblcmb3.Visible = true;
                lblcmb3.Text = "Category";

                break;


                    case 15:
                grpdate.Visible = true;
                lblcmb1.Visible = true;
                lblcmb1.Text = "WareHouse";
                cmb1.Visible = true;
                sql = "sELECT   WR_code,WR_name froM WRHOUSe_MASTER Union sELECT   0,'All' froM WRHOUSe_MASTER  ";

                 ada2 = new SqlDataAdapter(sql, Conn);
                 dt2 = new DataTable("WRHOUS_MASTER");
                ada2.Fill(dt2);
                cmb1.DisplayMember = "WR_name";
                cmb1.ValueMember = "WR_code";



                cmb1.DataSource = dt2;
                //cmbcatcode.DataSource = dt2;



               
                lblcmb2.Visible = true;
                lblcmb2.Text = "Material Category";
                cmb2.Visible = true;
                sql = "sELECT   itm_cat_code,ITM_CAT_name froM ITEM_CAT UNION sELECT   -10,'All' froM ITEM_CAT  ";

                ada2 = new SqlDataAdapter(sql, Conn);
                dt2 = new DataTable("ITEM_CAT");
                ada2.Fill(dt2);
                cmb2.DisplayMember = "itm_cat_name";
                cmb2.ValueMember = "itm_cat_code";



                cmb2.DataSource = dt2;
                //cmbcatcode.DataSource = dt2;


                break;
             case 16:
                grpdate.Visible = true;
                lblcmb1.Visible = true;
                lblcmb1.Text = "WareHouse From";
                cmb1.Visible = true;

                find = false;

                sql = "sELECT  WR_CODE,WR_NAME froM WRHOUSE_MASTER union select 0 , 'All' from WRHOUSE_MASTER";

                 adacmb1 = new SqlDataAdapter(sql, Conn);
               

                  dtcmb1 = new DataTable("WRHOUSE_MASTER");

                adacmb1.Fill(dtcmb1);
                cmb1.DisplayMember = "WR_NAME";
                cmb1.ValueMember = "WR_CODE";
                cmb1.DataSource = dtcmb1;

                lblcmb2.Visible = true;
                lblcmb2.Text = "WareHouse To";
                cmb2.Visible = true;


                chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                //chk.DefaultCellStyle = lst1.DefaultCellStyle;
                lst1.Columns.Add(chk);
                chk.ReadOnly = false;
                sql = "sELECT  WR_CODE,WR_NAME froM WRHOUSE_MASTER union select 0 , 'All' from WRHOUSE_MASTER";

                        adacmb2 = new SqlDataAdapter(sql, Conn);
               

                         dtcmb2 = new DataTable("WRHOUSE_MASTER");

                adacmb2.Fill(dtcmb2);
                cmb2.DisplayMember = "WR_NAME";
                cmb2.ValueMember = "WR_CODE";
                cmb2.DataSource = dtcmb2;

               
                    panel1.Visible = true;
                            lbllst1.Text = "Material Items";

                            

                            sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER  ";

                            ada2 = new SqlDataAdapter(sql, Conn);
                            dt2 = new DataTable("HD_ITEMMASTER");
                            ada2.Fill(dt2);

                            dv.Table = dt2;
                            dv1.Table = dt2;
                            lst1.DataSource = dv;
                            dv.Sort = "Description";
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
                  case 20:
                  case 21:
                            //print_Report1(20);
                            grpdate.Visible = false;
                            //this.Visible = false;
                            break;
                  case 22:
                    grpdate.Visible = true;
                    lblcmb3.Visible = true;
                    txtmove.Visible = true;
                    lblcmb3.Text = "Report For";
                        lblcmb1.Visible = false;
                        lblcmb1.Text = "Item Category";
                        cmb1.Visible = false;
                        cmb3.Visible = true;
                         sql = "sELECT   0 as code,'Item Movement Percent Less Than ' as Rname Union Select 1, 'Item Movement Percent Greter Than'  ";

                         ada2 = new SqlDataAdapter(sql, Conn);
                         dt2 = new DataTable("ITEM");
                        ada2.Fill(dt2);
                        cmb3.DataSource = dt2;
                        cmb3.DisplayMember = "Rname";
                        cmb3.ValueMember = "code";

                        chkminstock.Visible = false;
                
                        
                        chkstock.Visible=false;;
                        break;
                  case 23:
                        grpdate.Visible = true;

                        chkdate.Checked = false;
                        repdt1.Value = DateTime.Now.Date;
                        repdt2.Value = DateTime.Now.Date;
                      
                        lblcmb1.Visible = true;
                        lblcmb1.Text = "WareHouse";
                        cmb1.Visible = true;
                         sql = "sELECT   WR_code,WR_name froM WRHOUSe_MASTER Union sELECT   0,'All' froM WRHOUSe_MASTER  ";

                        ada2 = new SqlDataAdapter(sql, Conn);
                        dt2 = new DataTable("WRHOUS_MASTER");
                        ada2.Fill(dt2);
                        cmb1.DisplayMember = "WR_name";
                        cmb1.ValueMember = "WR_code";


                
                        cmb1.DataSource = dt2;
                        //cmbcatcode.DataSource = dt2;
                        grpdate.Visible = true;        
                        lblcmb2.Visible = true;
                        lblcmb2.Text = "Transaction";
                        cmb2.Visible = true;
                        

                       

                         sql = "SELECT  Trn_Name,CODES froM Trn_Type_rep  order by TRN_CODE";

                     trnad = new SqlDataAdapter(sql, Conn);
                     trndt = new DataTable("trntype");
                    trnad.Fill(trndt);

                    cmb2.DisplayMember = "Trn_Name";
                    cmb2.ValueMember = "CODES";
                    cmb2.DataSource = trndt;
                    cmb2.SelectedIndex = 0;

                        chkminstock.Visible = false;


                        chkstock.Visible = false; ;
                        break;
                        
                }   


                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void ini_tab()
        {
            maintab1.Left = (this.Width - maintab1.Width) / 2;
            maintab1.Width = this.Width;
                maintab1.Height=this.Height;
                cmb1.SelectedIndex = 0;
                

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

                lblmsg.Text= "Please be patient, It is Loading the Report.......";

                print_Report1(Gvar.rptidx);
                lblmsg.Text = ".";
                //ReportDocument CrRep = new ReportDocument();
                //rep_path = Gvar.report_path + "\\reports\\ItemMaster.rpt";
                //CrRep.Load(rep_path);

                //CrRep.SummaryInfo.ReportTitle = "Material Stock Report for all";
                //FrmrepView frm = new FrmrepView();
                //frm.MdiParent = this.ParentForm;

                //frm.crv1.ReportSource = CrRep;
                //frm.Show();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void print_Report1(int idx)
        {
            string crt;
            string rep_path;
            fnd = false;
            try
            {
                ReportDocument CrRep = new ReportDocument();
                rep_path = "";
                crt1 = "";
                string crt2 = "";
                string crt3 = "";
                string crt4="";
                string crt6 = ""; 
                crt = "";
                switch (idx)
                {
                    case 1:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\rptItemMaster.rpt";
                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report for all";
                        }

                       
                        if (cmb1.SelectedIndex > 0)
                        {
                            crt1 = "{ITEMMASTER.ITM_CAT_CODE}  =" + cmb1.SelectedValue;
                            CrRep.SummaryInfo.ReportTitle = "Item Stock Report for Category " + cmb1.Text;
                        }
                        else
                        {
                         //   crt = "{hd_itemmaster.itm_cat_code} > 4  ";
                            }


                        if (chkstock.Checked)
                        {
                            crt2 = "  {STOCK_MASTER.STOCK}>0";
                            CrRep.SummaryInfo.ReportTitle = CrRep.SummaryInfo.ReportTitle + " for Stock Available";
                        }

                        if (chkminstock.Checked)
                        {
                            crt3 = " ({STOCK_MASTER.STOCK}<{STOCK_MASTER.RE_ORDER} AND {STOCK_MASTER.RE_ORDER} >0)";
                            CrRep.SummaryInfo.ReportTitle = "Inventory Stock Report for Only Not in Minimum Stock";
                        }

                        crt = "";
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
                             // if (crt3 != "") crt
                       

                        break;

                    case 17:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\Repasset.rpt";
                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Asset Report for all";
                        }


                        if (cmb1.SelectedIndex > 0)
                        {
                            crt1 = "{ASSET_MASTER.AST_CAT_CODE}  =" + cmb1.SelectedValue;
                            CrRep.SummaryInfo.ReportTitle = "Asset Report for Category " + cmb1.Text;
                        }
                        else
                        {
                            //   crt = "{hd_itemmaster.itm_cat_code} > 4  ";
                        }


                       

                        if (chkminstock.Checked)
                        {
                            crt3 = " {@CUR_VALUE} > 1";
                            CrRep.SummaryInfo.ReportTitle =  CrRep.SummaryInfo.ReportTitle + "  With Valued";
                        }

                        crt = "";
                        if (crt1 != "")
                        {
                            if (crt != "")
                            { crt = crt + " aND " + crt1; }
                            else
                            {
                                crt = crt1;
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
                        // if (crt3 != "") crt


                        break;

                    case 2:
                    case 3:
                    case 4:
                    
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            if (Gvar.rptidx == 2)
                            {
                                rep_path = Gvar.report_path + "\\reports\\RptInvoiceDet.rpt";
                            }

                            if (Gvar.rptidx == 3)
                            {
                                rep_path = Gvar.report_path + "\\reports\\Rptitmbycust.rpt";
                            }
                            if (Gvar.rptidx == 4)
                            {
                                rep_path = Gvar.report_path + "\\reports\\RptItmbyCustByItm.rpt";
                            }
                            CrRep.Load(rep_path);

                            
                        }

                        crt2 = "";
                        crt1 = "";

                        RPTHEAD = "Invoice Report ";
                            if (cmb1.SelectedIndex >0)
                            {
                                RPTHEAD = "Invoice Report for WareHouse " + cmb1.Text;
                                crt2 = "{WRHOUSE_MASTER.WR_CODE}  = " + cmb1.SelectedValue;
                              // crt2 = "{WRHOUSE_MASTER.WR_CODE}="+ cmb1.SelectedValue; 
                            }
                            //else
                            //{
                            //    crt2 = "{WRHOUSE_MASTER.WR_CODE}  = " + cmb1.SelectedValue;
                            //    CrRep.SummaryInfo.ReportTitle = "Invoice  Report for WareHouse  " + cmb1.Text;
                            //}

                            if (cmb2.SelectedIndex>0)
                            {

                                crt3 = " {DATA_ENTRY.TRN_TYPE} in ["+ cmb2.SelectedValue + "] ";

                                //if ((int)cmb2.SelectedValue== -9)
                                //{
                                //    crt3 = " {DATA_ENTRY.TRN_TYPE} in [3,4,6,7] ";
                                //   // RPTHEAD += ", Sales N Return";

                                //}
                                //else
                                //    if ((int)cmb2.SelectedValue == -8)
                                //    {
                                //        crt3 = " {DATA_ENTRY.TRN_TYPE} in [1,2,8,9] ";
                                //       // RPTHEAD += ", Purchase N Return";

                                //    }
                                //else

                                //crt3 =  " {DATA_ENTRY.TRN_TYPE} =  " + cmb2.SelectedValue ;
                                RPTHEAD += ", For  " + cmb2.Text;
                                //crt = "{HD_ITEMMASTER.ITM_CAT_CODE} > 0 and {WRHOUSE_MASTER.WR_CODE} = 1";
                            }

                            if (!chkdate.Checked)
                            {


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                                crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                RPTHEAD = RPTHEAD + ", From Date  " + rdt1 + " To " + rdt2;
                            }

                        fnd = false;
                       
                            lst1.EndEdit();
                            int i = 0;
                            if (chklst1.Checked)
                            {
                                //crt3 = "{DATA_ENTRY_GRID.Item_Code} <> '-1'";
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
                                            if (crt6 == "")
                                            {
                                                crt6 = "'" + lst1[1, i].Value.ToString() + "'";
                                            }
                                            else
                                            {
                                                crt6 = crt6 + ",'" + lst1[1, i].Value.ToString() + "'";
                                            }
                                            fnd = true;
                                        }
                                    }

                                    //RPTHEAD = RPTHEAD + ", Items In   [" + crt6 + "]";

                                }
                                crt6 = "{DATA_ENTRY_GRID.Item_Code}  in [" + crt6 + "]";



                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No  Item selected, Please Try AGain", "Wrong Selection");
                                return;
                            }
                            //crt2 = crt2 + " AND " + crt3;





                        fnd = false;
                        
                           
                            lst2.EndEdit();
                            
                            if (chklst2.Checked)
                            {
                               // crt4 = "{DATA_ENTRY.ACCode} <> '-1'";
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
                                            if (crt4 == "")
                                            {
                                                crt4 =  lst2[1, i].Value.ToString() ;
                                            }
                                            else
                                            {
                                                crt4 = crt4 + "," + lst2[1, i].Value.ToString();
                                            }
                                            fnd = true;
                                        }
                                    }
                                    //RPTHEAD = RPTHEAD + ", Acounts In   [" + crt4 + "]";

                                }
                                crt4 = "{DATA_ENTRY.ACCode}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Account selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            //crt2 = crt2 + " AND " + crt3;

                            //if (checkBox1.Checked)
                            //    crt = crt + " and {WR_STOCK_MASTER.STOCK}>0";

                         
                              crt = "";
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

                              if (crt6 != "")
                              {
                                  if (crt != "")
                                  { crt = crt + " aND " + crt6; }
                                  else
                                  {
                                      crt = crt6;
                                  }
                              }
                              if (crt != "")
                              {

                                  crt = crt + " And {Data_Entry.flag} <>'D'";
                              }
                                else
                                  crt = "{Data_Entry.flag} <>'D'";

                              if (Gvar.rptidx == 4)
                              {
                                  if (chkstock.Checked)
                                  {


                                      CrRep.ReportDefinition.Sections[4].SectionFormat.EnableSuppress = true;
                                      CrRep.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                                      //CrRep.ReportDefinition.Sections[9].SectionFormat.EnableSuppress = true;
                                      //CrRep.ReportDefinition.Sections[10].SectionFormat.EnableSuppress = true;
                                  }
                              }
                              CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                        break;

                    #region porder
                    case 18:
                    case 19:
                        {

                            //crt = "{Reciepts.Reciept_no} =" + Convert.ToDouble(Txtitem.Text);
                            if (Gvar.rptidx == 18)
                            {
                                rep_path = Gvar.report_path + "\\reports\\RptOrder.rpt";
                                RPTHEAD = "Purchase Order Report for All";
                            }
                            if (Gvar.rptidx == 19)
                            {
                                rep_path = Gvar.report_path + "\\reports\\RptRECOrder.rpt";
                                RPTHEAD = "Packing Slip Report for All";
                            }
                         
                            CrRep.Load(rep_path);


                        }

                        crt2 = "";
                        crt1 = "";

                       
                       

                      

                        if (!chkdate.Checked)
                        {


                            string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                            string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                            crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = RPTHEAD + ", From Date  " + rdt1 + " To " + rdt2;
                        }

                        fnd = false;
                       
                        lst1.EndEdit();
                      
                        if (chklst1.Checked)
                        {
                            //crt3 = "{DATA_ENTRY_GRID.Item_Code} <> '-1'";
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
                                        if (crt6 == "")
                                        {
                                            crt6 = "'" + lst1[1, i].Value.ToString() + "'";
                                        }
                                        else
                                        {
                                            crt6 = crt6 + ",'" + lst1[1, i].Value.ToString() + "'";
                                        }
                                        fnd = true;
                                    }
                                }

                                

                            }
                           // RPTHEAD = RPTHEAD + ", Items In   (" +  crt6.Replace("'","") + ")";
                            if (Gvar.rptidx == 18)
                            {
                                crt6 = "{PUR_ORDER_GRID.Item_Code}  in [" + crt6 + "]";

                            }
                            else
                            {
                                crt6 = "{DATA_ENTRY_GRID.Item_Code}  in [" + crt6 + "]";
                            }

                          

                        }

                        if (!fnd)
                        {
                            MessageBox.Show("There is No  Item selected, Please Try AGain", "Wrong Selection");
                            return;
                        }
                        //crt2 = crt2 + " AND " + crt3;





                        fnd = false;


                        lst2.EndEdit();
                      
                        if (chklst2.Checked)
                        {
                            // crt4 = "{DATA_ENTRY.ACCode} <> '-1'";
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
                                        if (crt4 == "")
                                        {
                                            crt4 = lst2[1, i].Value.ToString();
                                        }
                                        else
                                        {
                                            crt4 = crt4 + "," + lst2[1, i].Value.ToString();
                                        }
                                        fnd = true;
                                    }
                                }
                                RPTHEAD = RPTHEAD + ", Acounts In   [" + crt4 + "]";

                            }
                            crt4 = "{DATA_ENTRY.ACCode}  in [" + crt4 + "]";
                        }

                        if (!fnd)
                        {
                            MessageBox.Show("There is No Account selected, Please Try AGain", "Wrong Selection");
                            return;
                        }

                        if (cmb1.SelectedIndex>0)
                        {
                            crt3 = "{DATA_ENTRY.ACCode} = " + cmb1.SelectedValue ;

                        }
                        //crt2 = crt2 + " AND " + crt3;

                        //if (checkBox1.Checked)
                        //    crt = crt + " and {WR_STOCK_MASTER.STOCK}>0";


                        crt = "";
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

                        if (crt6 != "")
                        {
                            if (crt != "")
                            { crt = crt + " aND " + crt6; }
                            else
                            {
                                crt = crt6;
                            }
                        }
                        if (crt != "")
                        {

                            crt = crt + " And {Data_Entry.flag} <>'D'";
                        }
                        else
                            crt = "{Data_Entry.flag} <>'D'";

                       
                        CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                        break;

                    #endregion

                    case 31:
                        {
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";

                            CrRep.Load(rep_path);
                            crt = "";
                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report for All Category ";
                            if (cmb1.SelectedIndex > 0)
                            {
                                crt = "{HD_ITEMMASTER.ITM_CAT_CODE}  =" + cmb1.SelectedValue;
                                CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Category " + cmb1.Text;
                            }
                            else
                                crt = "{hd_itemmaster.itm_cat_code} > 4  ";

                            // Convert.ToDouble(Txtitem.Text);


                            if (chkstock.Checked)
                                if (crt=="")
                                    
                                        crt = "{STOCK_MASTER.STOCK}>0";
                                else
                                crt = crt + " and {STOCK_MASTER.STOCK}>0";

                           
                        }


                        break;


                    case 41:
                        {

                            crt = "{STOCK_MASTER.STOCK} <= {STOCK_MASTER.RE_ORDER} ";
                            // Convert.ToDouble(Txtitem.Text);
                            rep_path = Gvar.report_path + "\\reports\\ItemMasterN.rpt";

                            CrRep.Load(rep_path);

                            CrRep.SummaryInfo.ReportTitle = "Material Stock Report for Re-Order Items";
                        }

                        break;
                    case 5: 
                        {
                            

                            rep_path = Gvar.report_path + "\\reports\\itemBysup.rpt";
                          

                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                            string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                            string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                            crt1 = "  {QRY_ITEM.DATE_TIME} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Receipt Entry Detail From " + sdt1 + " To " + sdt2;





                            crt1 = crt1 + " And ({QRY_ITEM.TRN_TYPE} = 2 Or {QRY_ITEM.TRN_TYPE} =8)";

                            crt2 = "00";
                            if (cmb1.SelectedIndex < 1)
                            {

                                crt2 = "{QRY_ITEM.cus_code} <> -1";
                            }
                            else
                            {
                                crt2 = "{QRY_ITEM.cus_code} = " + cmb1.SelectedValue;
                            }




                            fnd = false;
                            crt4 = "'00'";
                            lst1.EndEdit();
                             i = 0;
                            if (chklst1.Checked)
                            {
                                crt4 = "{QRY_ITEM.Item_Code} <> '-1'";
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
                                                crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            else
                                            {
                                                crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{QRY_ITEM.Item_Code}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Material Item selected, Please Try AGain", "Wrong Selection");
                                return;
                            }

                            crt = crt1 + " aND " + crt2 + " aND " + crt4 + " AND {QRY_ITEM.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = "Material Item Purchase  Report from " + rdt1 + " To " + rdt2;

                        }
                            break;
                    case  6:
                         {
                            if (idx==5)

                            rep_path = Gvar.report_path + "\\reports\\itemBysup.rpt";
                            else
                                rep_path = Gvar.report_path + "\\reports\\itemBysupinv.rpt";


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                            string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                            string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                            crt1 = "  {TRN_DETAIL.DATE_TIME} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Receipt Entry Detail From " + rdt1 + " To " + rdt2;





                            crt1 = crt1 + " And ({TRN_DETAIL.TRN_TYPE} =2 OR {TRN_DETAIL.TRN_TYPE} =8)";

                            crt2 = "00";
                            if (cmb1.SelectedIndex < 1)
                            {

                                crt2 = "{TRN_DETAIL.cus_code} <> -1";
                            }
                            else
                            {
                                crt2 = "{TRN_DETAIL.cus_code} = " + cmb1.SelectedValue;
                            }




                            fnd = false;
                            crt4 = "'00'";
                            lst1.EndEdit();
                             i = 0;
                            if (chklst1.Checked)
                            {
                                crt4 = "{TRN_DETAIL.Item_Code} <> '-1'";
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
                                                crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            else
                                            {
                                                crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{TRN_DETAIL.Item_Code}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Item selected, Please Try AGain", "Wrong Selection");
                                return;
                            }

                            crt= crt1 + " aND " + crt2 + " aND " + crt4 + " AND {TRN_DETAIL.QTY}<>0";

                            CrRep.SummaryInfo.ReportTitle = "Material Item Purchase  Report from " + rdt1 + " To " + rdt2;

                        }
                            break;
                    case 7:
                            {

                                rep_path = Gvar.report_path + "\\reports\\RPTISU.rpt";



                                CrRep.Load(rep_path);
                                //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                                //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                                crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                RPTHEAD = "Material Issue Detail From " + rdt1 + " To " + rdt2;

                                crt2 = "";
                                if (cmb1.SelectedIndex < 1)
                                {
                                   // crt2 = "{cost_master.cost_code} <>  -1";
                                }
                                else
                                {
                                    crt2 = "{cost_master.cost_code} = " + cmb1.SelectedValue;
                                }

                                fnd = false;
                                 crt3 = "";
                                if (cmb2.SelectedIndex < 1)
                                {
                                    //crt3 = "{Proj_master.Proj_code} <> 0";
                                    fnd = true;
                                }
                                else
                                {
                                    crt3 = "{Proj_master.Proj_code} = " + cmb2.SelectedValue;
                                }



                                 crt4 = "";
                                lst1.EndEdit();
                                 i = 0;
                                if (chklst1.Checked)
                                {
                                  //  crt4 = "{DATA_ENTRY_GRID.PLOT} <> '-1'";
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
                                                    crt4 = "'" + lst1[1, i].Value.ToString() + "'";
                                                }
                                                else
                                                {
                                                    crt4 = crt4 + ",'" + lst1[1, i].Value.ToString() + "'";
                                                }
                                                fnd = true;
                                            }
                                        }

                                    }
                                    crt4 = "{DATA_ENTRY_GRID.PLOT}  in [" + crt4 + "]";
                                }

                                if (!fnd)
                                {
                                    MessageBox.Show("There is No Material Item selected, Please Try AGain", "Wrong Selection");
                                    return;
                                }






                                //rep_path = App.path & "\reports\itemBysupinv.rpt"


                                crt = crt1;
                                if (crt2 != "") crt = crt + " aND " + crt2;
                                if (crt3 != "") crt = crt + " aND " + crt3;
                                if (crt4 != "") crt = crt + " aND " + crt4;
                                crt = crt + " AND {DATA_ENTRY.TRN_TYPE} = 7";



                                CrRep.SummaryInfo.ReportTitle = "Material Item Issue  Report from " + rdt1 + " To " + rdt2;


                                break;
                            }
                


                       case 8:
                       
                          {

                            rep_path = Gvar.report_path + "\\reports\\Item_Detail.rpt";
                            


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                            string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                           string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                           string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                           crt1 = "  {TRN_MASTER.DATE_TIME} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Material Summary From " + rdt1 + " To " + rdt2;

                             crt2 = "";
                            if (cmb1.SelectedIndex < 1)
                            {
                                crt2 = "{hd_itemmaster.itm_cat_code} > 4 ";
                            }
                            else
                            {
                                crt2 = "{hd_itemmaster.itm_cat_code} = " + cmb1.SelectedValue;
                            }

                            fnd = false;
                        //string crt3 = "";
                        //    if (cmb2.SelectedIndex < 1)
                        //    {
                        //        crt3 = "{Proj_master.Proj_code} <> 0";
                        //        fnd = true;
                        //    }
                        //    else
                        //    {
                        //        crt3 = "{Proj_master.Proj_code} = " + cmb1.SelectedValue;
                        //    }



                             crt4 = "";
                            lst1.EndEdit();
                             i = 0;
                            if (chklst1.Checked)
                            {
                                //crt4 = "{hd_itemmaster.Item_Code} <> '-1'";
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
                                                crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            else
                                            {
                                                crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{hd_itemmaster.Item_Code}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Material Item selected, Please Try AGain", "Wrong Selection");
                                return;
                            }


                            



                            //rep_path = App.path & "\reports\itemBysupinv.rpt"


                            crt = crt1;
                            if (crt2 != "") crt = crt + " aND " + crt2;
                            if (crt4 != "") crt = crt + " aND " + crt4;
                            //crt = crt + " aND " + crt4 + " AND {DATA_ENTRY.TRN_TYPE} = 7";



                            CrRep.SummaryInfo.ReportTitle = "Material Item Summary  Report from " + rdt1 + " To " + rdt2;


                            break;
                        }
                       case 9:
                          {

                              rep_path = Gvar.report_path + "\\reports\\Trn_Detail.rpt";

                             

                              CrRep.Load(rep_path);
                              //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                              //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                              string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                              string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                              string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                              string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");

                              crt1 = "";
                              if (!chkdate.Checked)
                              {






                                  crt1 = "   {TRN_DETAIL.DATE_TIME}  in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                  RPTHEAD = RPTHEAD + "Item Transaction For Date From " + rdt1 + " To " + rdt2;
                              }

                              //crt1 = "  {TRN_DETAIL.DATE_TIME} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                              //RPTHEAD = "Material Movement Report from " + rdt1 + " To " + rdt2;

                              crt2 = "";
                             


                                  if (cmb1.SelectedIndex < 1)
                                  {
                                      CrRep.SummaryInfo.ReportTitle = "Item Transaction Report ";
                                      // crt2 = "{WRHOUSE_MASTER.WR_CODE}="+ cmb1.SelectedValue; 
                                  }
                                  else
                                  {
                                      crt2 = "{TRN_DETAIL.WR_CODE}  = " + cmb1.SelectedValue;
                                      CrRep.SummaryInfo.ReportTitle = "Item Transaction By WareHouse for " + cmb1.Text;
                                  }
                                  string crt5 = "";
                                  if (cmb2.SelectedIndex > 0)
                                  {
                                       crt5 = " {TRN_DETAIL.TRN_TYPE} in [" + cmb2.SelectedValue + "]";
                                      //crt = "{HD_ITEMMASTER.ITM_CAT_CODE} > 0 and {WRHOUSE_MASTER.WR_CODE} = 1";
                                  }


                                  fnd = false;
                                   crt3 = "'00'";
                                  lst1.EndEdit();
                                  i = 0;
                                  if (chklst1.Checked)
                                  {
                                      crt3 = "{TRN_DETAIL.Item_Code} <> '-1'";
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
                                                  if (crt3 == "")
                                                  {
                                                      crt3 = "'" + lst1[1, i].Value.ToString() + "'";
                                                  }
                                                  else
                                                  {
                                                      crt3 = crt3 + ",'" + lst1[1, i].Value.ToString() + "'";
                                                  }
                                                  fnd = true;
                                              }
                                          }

                                      }
                                      crt3 = "{TRN_DETAIL.Item_Code}  in [" + crt3 + "]";
                                  }

                                  if (!fnd)
                                  {
                                      MessageBox.Show("There is No Stock Item selected, Please Try AGain", "Wrong Selection");
                                      return;
                                  }
                                 // crt2 = crt2 + " AND " + crt3;

                                  fnd = false;


                                  lst2.EndEdit();
                                   crt4 = "";
                                  if (chklst2.Checked)
                                  {
                                     // crt4 = "{TRN_DETAIL.CUS_CODE} <> '-1'";
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
                                                  if (crt4 == "")
                                                  {
                                                      crt4 =  lst2[1, i].Value.ToString() ;
                                                  }
                                                  else
                                                  {
                                                      crt4 = crt4 + "," + lst2[1, i].Value.ToString() ;
                                                  }
                                                  fnd = true;
                                              }
                                          }

                                      }
                                      crt4 = "{TRN_DETAIL.CUS_CODE}  in [" + crt4 + "]";
                                  }

                                  if (!fnd)
                                  {
                                      MessageBox.Show("There is No Account selected, Please Try AGain", "Wrong Selection");
                                      return;
                                  }


                                
                              
                              crt = crt1;
                              

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


                              CrRep.SummaryInfo.ReportTitle = "Item Movement Report from " + rdt1 + " To " + rdt2;


                              break;
                          }
                    case 10: case 11:

                          {
                             
                             

                             // rep_path = Gvar.report_path + "\\reports\\itemissue_byplotcat.rpt";
                             // else
                                  rep_path = Gvar.report_path + "\\reports\\itemissue_bycat1.rpt";
                                  CrRep.Load(rep_path);
                                  if (Gvar.rptidx == 10)
                                  {
                                      CrRep.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                                      CrRep.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                                  }
                              
                             // DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                              //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                              string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                              string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                              string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                              string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");

                              crt1 = "";

                              crt1 = "{DATA_ENTRY.CURDATE}  in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                              RPTHEAD = "Material Issue Report ";

                              crt2 = "";
                              if (cmb1.SelectedIndex < 1)
                              {
                                  crt2 = "{cost_master.cost_code} >4";
                              }
                              else
                              {
                                  crt2 = "{ITEM_CAT.itm_cat_code} = " + cmb1.SelectedValue;
                              }

                              fnd = false;
                               crt3 = "";
                              if (cmb2.SelectedIndex < 1)
                              {
                                  //crt3 = "{Proj_master.Proj_code} <> 0";
                                  fnd = true;
                              }
                              else
                              {
                                  crt3 = "{proj_master.proj_code} = " + cmb2.SelectedValue;
                              }



                               crt4 = "";
                              lst1.EndEdit();
                               i = 0;
                              if (chklst1.Checked)
                              {
                                  //crt4 = "{hd_itemmaster.Item_Code} <> '-1'";
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
                                                  crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                              }
                                              else
                                              {
                                                  crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                              }

                                              fnd = true;
                                          }
                                      }

                                  }
                                  crt4 = "{DATA_ENTRY_GRID.Item_Code}  in [" + crt4 + "]";
                              }

                              if (!fnd)
                              {
                                  MessageBox.Show("There is No Material Item selected, Please Try AGain", "Wrong Selection");
                                  return;
                              }

                              fnd = false;
                              string crt5 = "";
                              lst2.EndEdit();
                               i = 0;
                              if (chklst2.Checked || cmb2.SelectedIndex==0)
                              {
                                  //crt4 = "{hd_itemmaster.Item_Code} <> '-1'";
                                  fnd = true;
                              }
                              else
                              {
                                  for (i = 0; i < lst2.RowCount; i++)
                                  {
                                      if (lst2[0, i].Value != null)
                                      {
                                          fnd = true;
                                          if ((bool)lst2[0, i].Value)
                                          {
                                              if (crt5 == "")
                                              {
                                                  crt5 =  lst2[1, i].Value.ToString() ;
                                              }
                                              else
                                              {
                                                  crt5 = crt5 + "," + lst2[1, i].Value.ToString() ;
                                              }
                                          }
                                      }

                                  }
                                  crt5 = "{SITE_MASTER.SITE_CODE}  in [" + crt5 + "]";
                              }

                              if (!fnd)
                              {
                                  MessageBox.Show("There is No Site  selected, Please Try AGain", "Wrong Selection");
                                  return;
                              }




                              //rep_path = App.path & "\reports\itemBysupinv.rpt"



                              crt = crt1;

                              if (crt2 != "")
                              {
                                  if (crt != "")
                                  { crt = crt + " aND " + crt2; }
                                  else
                                  {
                                      crt = crt2;
                                  }
                              }
                              if (crt3 != "") crt = crt + " aND " + crt3;
                              if (crt4 != "") crt = crt + " aND " + crt4;
                              if (crt5 != "") crt = crt + " aND " + crt5;
                              //crt = crt + " aND " + crt4 + " AND {DATA_ENTRY.TRN_TYPE} = 7";
                              crt = crt + " AND {DATA_ENTRY.TRN_TYPE} = 7";


                              CrRep.SummaryInfo.ReportTitle = "Material Issue Report";
                             

                              break;
                          }
                    case 12:
                          {
                              rep_path = Gvar.report_path + "\\reports\\inv_detail.rpt";


                              CrRep.Load(rep_path);
                              //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                              //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                              string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                              string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                              string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                              string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                              crt1 = "  {data_entry.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                              RPTHEAD = "Receipt Entry Detail From " + rdt1 + " To " + rdt2;





                              crt1 = crt1 + " And {data_entry.TRN_TYPE} =2";

                              crt2 = "";
                              if (cmb1.SelectedIndex < 1)
                              {

                                  // crt2 = "{data_entry.cus_code} <> -1";
                              }
                              else
                              {
                                  crt2 = "{data_entry.accode} = " + cmb1.SelectedValue;
                              }



                               crt3 = "";
                              if (cmb2.SelectedIndex < 1)
                              {

                                  // crt3 = "{data_entry.cus_code} <> -1";
                              }
                              else
                              {
                                  crt3 = "{data_entry.whcode} = " + cmb2.SelectedValue;
                              }


                              crt = crt1;

                              if (crt2 != "")
                              {
                                  if (crt != "")
                                  { crt = crt + " aND " + crt2; }
                                  else
                                  {
                                      crt = crt2;
                                  }
                              }
                              if (crt3 != "") crt = crt + " aND " + crt3;

                              CrRep.SummaryInfo.ReportTitle = "Purchase  Report from " + rdt1 + " To " + rdt2;
                              break;
                          }
                    case 13:
                          {

                              rep_path = Gvar.report_path + "\\reports\\ITM_SUM_BYCOST.rpt";



                              CrRep.Load(rep_path);
                              //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                              //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                              string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                              string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                              string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                              string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");

                              Conn.Close();
                              Conn.Open();
                              string sql = "Update report_dt set dt1 ='" + repdt1.Value.Year + "-" + repdt1.Value.Month + "-" + repdt1.Value.Day + "', dt2='" + repdt2.Value.Year + "-" + repdt2.Value.Month + "-" + repdt2.Value.Day + "'";
                              cmd = new SqlCommand(sql, Conn);
                              cmd.ExecuteNonQuery();

                              //crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                              RPTHEAD = "Material Issue Summary By Cost Category From " + rdt1 + " To " + rdt2;

                              //crt2 = "";
                              //if (cmb1.SelectedIndex < 1)
                              //{
                              //    crt2 = "{DATA_ENTRY.ACCCODE} <>  -1";
                              //}
                              //else
                              //{
                              //    crt2 = "{DATA_ENTRY_GRID.BUDGE_CODE} = " + cmb1.SelectedValue;
                              //}

                               crt3 = "";
                              if (cmb2.SelectedIndex < 1)
                              {
                                 // crt2 = "{DATA_ENTRY.ACCCODE} <>  -1";
                              }
                              else
                              {
                                  crt3 = "{trn_sum_by_cat.Proj_Name}= '" + cmb2.Text + "'";
                              }

                              fnd = false;
                              
                               crt4 = "";
                              lst1.EndEdit();
                               i = 0;
                              if (chklst1.Checked)
                              {
                                  //crt4 = "{trn_sum_by_cat.site_name} <> '-1'";
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
                                                  crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                              }
                                              else
                                              {
                                                  crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                              }
                                              fnd = true;
                                          }
                                      }

                                  }
                                  crt4 = "{trn_sum_by_cat.site_name}  in [" + crt4 + "]";
                              }

                              if (!fnd)
                              {
                                  MessageBox.Show("There is No Site selected, Please Try AGain", "Wrong Selection");
                                  return;
                              }






                              //rep_path = App.path & "\reports\itemBysupinv.rpt"


                              //if (crt2 != "")
                              //{
                              //    if (crt != "")
                              //    { crt = crt + " aND " + crt2; }
                              //    else
                              //    {
                              //        crt = crt2;
                              //    }
                              //}

                              if (crt3 != "")
                              {
                                  if (crt != "")
                                  { crt = crt + " aND " + crt3; }
                                  else
                                  {
                                      crt = crt3;
                                  }
                              }

                              if (crt4 != "") crt = crt + " aND " + crt4;


                              CrRep.SummaryInfo.ReportTitle = "Material Issue Summary By Cost Category From " + rdt1 + " To " + rdt2;


                              break;
                          }

                 case 14:
                            {

                                rep_path = Gvar.report_path + "\\reports\\rptbudget.rpt";



                                CrRep.Load(rep_path);
                                //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                                //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");

                                Conn.Close();
                                Conn.Open();
                                string sql = "Update report_dt set dt1 ='" + repdt1.Value.Year + "-"  + repdt1.Value.Month + "-" + repdt1.Value.Day + "', dt2='" + repdt2.Value.Year + "-"  + repdt2.Value.Month + "-" + repdt2.Value.Day + "'";
                                cmd = new SqlCommand(sql, Conn);
                                cmd.ExecuteNonQuery();

                                crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                               

                                RPTHEAD = "Material Issue  By Budget Category From " + rdt1 + " To " + rdt2;
                                crt2 = "";
                                if (cmb1.SelectedIndex < 1)
                                {
                                    // crt2 = "{DATA_ENTRY.ACCCODE} <>  -1";
                                }
                                else
                                {
                                    crt2 = "{DATA_ENTRY_GRID.BUDGE_CODE} = " + cmb1.SelectedValue;
                                }
                                fnd = false;
                                 crt3 = "";
                                if (cmb2.SelectedIndex < 1)
                                {
                                    //crt3 = "{trn_sum_by_cat.Proj_code} <> 0";
                                    fnd = true;
                                }
                                else
                                {
                                    crt3 = "{DATA_ENTRY.ACCODE}  = "+ cmb2.SelectedValue ;
                                }



                                 crt4 = "";
                                lst1.EndEdit();
                                 i = 0;
                                if (chklst1.Checked)
                                {
                                    //crt4 = "{trn_sum_by_cat.site_name} <> '-1'";
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
                                                    crt4 =  "" + lst1[1, i].Value + ""; 
                                                }
                                                else
                                                {
                                                    crt4 = crt4 + "," + lst1[1, i].Value;
                                                }
                                                fnd = true;
                                            }
                                        }

                                    }
                                    crt4 = "{DATA_ENTRY.sales_code} in [" + crt4 + "]";
                                }

                                if (!fnd)
                                {
                                    MessageBox.Show("There is No Site selected, Please Try AGain", "Wrong Selection");
                                    return;
                                }



                                string crt5 = "";
                                if (cmb3.SelectedIndex < 1)
                                {
                                    //crt3 = "{trn_sum_by_cat.Proj_code} <> 0";
                                    fnd = true;
                                }
                                else
                                {
                                    crt5 = "{DATA_ENTRY.COST_CODE}  = " + cmb3.SelectedValue;
                                }


                                //rep_path = App.path & "\reports\itemBysupinv.rpt"


                                crt = crt1;

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

                                if (crt4 != "") crt = crt + " aND " + crt4 + " AND {DATA_ENTRY.TRN_TYPE}>5";
                                if (crt5 != "") crt = crt + " aND " + crt5;

                                CrRep.SummaryInfo.ReportTitle = "Material Issue Summary By Cost Category From " + rdt1 + " To " + rdt2;
                                
                                break;
                          }

                 case 15:
                            {
                                rep_path = Gvar.report_path + "\\reports\\rptstockbydate.rpt";

                                CrRep.Load(rep_path);
                                //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                                //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");

                                Conn.Close();
                                Conn.Open();


                                crt1 = "  {ITM_TRN_DETAIL.DATE_TIME} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";

                                RPTHEAD = "Stock Report From " + rdt1 + " To " + rdt2;
                                CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                                crt2 = "";
                                if (cmb1.SelectedIndex < 1)
                                {

                                }
                                else
                                {
                                    crt2 = "{ITM_TRN_DETAIL.WR_CODE}  = " + cmb1.SelectedValue ;
                                    CrRep.SummaryInfo.ReportTitle = CrRep.SummaryInfo.ReportTitle + " for WHouse  " + cmb1.Text;
                                }


                                 crt3 = "";
                                crt3 = "";
                                if (cmb2.SelectedIndex > 0)
                                {
                                    crt3 = "{ITM_TRN_DETAIL.ITM_CAT_CODE}  =" + cmb2.SelectedValue;
                                    CrRep.SummaryInfo.ReportTitle = CrRep.SummaryInfo.ReportTitle + " for Category " + cmb2.Text;
                                }
                                else
                                    crt3 = "{ITM_TRN_DETAIL.ITM_CAT_CODE}  >4";



                                crt = crt1;

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
                                if (chkstock.Checked)
                                    crt = crt + " and {itm_trn_detail.qty}>0";

                                break;
                            }
                    case  16:
                         {
                            

                            rep_path = Gvar.report_path + "\\reports\\transfer_detail.rpt";
                           


                            CrRep.Load(rep_path);
                            //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                            //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                            string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                            string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                            string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                            string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");



                            crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                            RPTHEAD = "Transfer Voucher Detail From " + rdt1 + " To " + rdt2;





                            crt1 = crt1 + " And {DATA_ENTRY.TRN_TYPE} =10";

                            crt2 = "00";
                            if (cmb1.SelectedIndex < 1)
                            {

                                crt2 = "{DATA_ENTRY.WHCODE} <> -1";
                            }
                            else
                            {
                                crt2 = "{DATA_ENTRY.WHCODE} = " + cmb1.SelectedValue;
                            }


                             crt3 = "00";
                            if (cmb2.SelectedIndex < 1)
                            {

                                crt3 = "{DATA_ENTRY.SALES_CODE} <> -1";
                            }
                            else
                            {
                                crt3 = "{DATA_ENTRY.SALES_CODE} = " + cmb2.SelectedValue;
                            }

                            fnd = false;
                             crt4 = "'00'";
                            lst1.EndEdit();
                             i = 0;
                            if (chklst1.Checked)
                            {
                                crt4 = "{DATA_ENTRY_GRID.Item_Code} <> '-1'";
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
                                                crt4 = "'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            else
                                            {
                                                crt4 = crt4 + ",'" + lst1[2, i].Value.ToString() + "'";
                                            }
                                            fnd = true;
                                        }
                                    }

                                }
                                crt4 = "{DATA_ENTRY_GRID.Item_Code}  in [" + crt4 + "]";
                            }

                            if (!fnd)
                            {
                                MessageBox.Show("There is No Material Item selected, Please Try AGain", "Wrong Selection");
                                return;
                            }

                            crt = crt1 + " aND " + crt2 + " aND " + crt3 + " aND " + crt4 ;

                            CrRep.SummaryInfo.ReportTitle = "Material Transfer  Report from " + rdt1 + " To " + rdt2;

                        }
                            break;

                    case 20:
                            {
                                rep_path = Gvar.report_path + "\\reports\\HRMS\\Rptemplist.rpt";
                                CrRep.Load(rep_path);
                                CrRep.SummaryInfo.ReportTitle = "Employees List";

                            }
                            break;
                    case 21:
                            {
                                rep_path = Gvar.report_path + "\\reports\\HRMS\\Rptempdet.rpt";
                                CrRep.Load(rep_path);
                                CrRep.SummaryInfo.ReportTitle = "Employees Record";

                            }
                            break;

                    case 22:
                            {


                                rep_path = Gvar.report_path + "\\reports\\Item_SaleStats.rpt";


                                CrRep.Load(rep_path);
                                //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                                //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");


                                string sql = "";

                                RPTHEAD = "Item Movement Statitstic  From " + rdt1 + " To " + rdt2;
                                if (!chkdate.Checked)
                                {
                                     sql = "Update report_dt set dt1 ='" + repdt1.Value.Year + "-" + repdt1.Value.Month + "-" + repdt1.Value.Day + "', dt2='" + repdt2.Value.Year + "-" + repdt2.Value.Month + "-" + repdt2.Value.Day + " 23:59:59'";
                                }
                                else
                                {
                                     sql = "Update report_dt set dt1 ='2000-01-01', dt2='2100-01-01'";
                                }
                                cmd = new SqlCommand(sql, Conn);
                                cmd.ExecuteNonQuery();


                               

                                crt2 = "00";
                                if (cmb3.SelectedIndex ==0)
                                {

                                    crt = "{V_ITEM_MOVE_PRCNT.PRCNT} <= " + txtmove.Text ;
                                    RPTHEAD = RPTHEAD + " for Less " + txtmove.Text + " %";
                                }
                                else
                                {
                                    crt = "{V_ITEM_MOVE_PRCNT.PRCNT} >= " + txtmove.Text;
                                    RPTHEAD = RPTHEAD + " for Greater " + txtmove.Text + " %";
                                }







                                CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                            }
                            break;


                    case 23:
                            {


                                rep_path = Gvar.report_path + "\\reports\\repdailytrans.rpt";


                                CrRep.Load(rep_path);
                                //DateTime edt1 = Convert.ToDateTime(Gvar.ArCalendar(dt1.Value, true,false));
                                //DateTime edt2 = Convert.ToDateTime(Gvar.ArCalendar(dt2.Value, true,false));


                                string sdt1 = repdt1.Value.ToString("yyyy,MM,dd,00,00,00");
                                string sdt2 = repdt2.Value.ToString("yyyy,MM,dd,23,59,59");

                                string rdt1 = repdt1.Value.ToString("dd/MM/yyyy");
                                string rdt2 = repdt2.Value.ToString("dd/MM/yyyy");
                                crt1 = "1=1";
                                RPTHEAD = "Transaction Summary Report  " ;
                                if (!chkdate.Checked)
                                {
                                    crt1 = "  {DATA_ENTRY.CURDATE} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
                                    RPTHEAD = "Transaction Summary Report  From " + rdt1 + " To " + rdt2;
                                }
                                string sql = "";

                              


                                crt3 = "1=1";
                                crt2 = "1=1";
                                if (cmb1.SelectedIndex == 0)
                                {

                                   
                                }
                                else
                                {
                                    crt3 = "{DATA_ENTRY.WR_CODE} = " + cmb1.SelectedValue;
                                    RPTHEAD = RPTHEAD + " for WareHouse " + cmb1.Text;
                                }

                                if (cmb2.SelectedIndex == 0)
                                {


                                }
                                else
                                {
                                    crt2 = "{DATA_ENTRY.TRN_TYPE} in [ " + cmb2.SelectedValue + "]";
                                    RPTHEAD = RPTHEAD + " for Transactions " + cmb2.Text;
                                }




                                crt = crt1 + " aND " + crt2 + " aND " + crt3;



                                CrRep.SummaryInfo.ReportTitle = RPTHEAD;

                            }
                            break;
                            
                            
                }
                          
                



                //MessageBox.Show(crt);



                            //CrRep.PrintOptions.PaperSize= CrystalDecisions.Shared.PaperSize.PaperEnvelope10;
                CrRep.OpenSubreport("HEADER.rpt").DataDefinition.FormulaFields["RPTHEAD"].Text = "'" + CrRep.SummaryInfo.ReportTitle + "'";
                    
                
                            if (crt != "")
                            {
                                CrRep.RecordSelectionFormula = crt;
                            }
                            CrRep.ReportOptions.EnableSaveDataWithReport=false;
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

                            ////CrRep.Load();
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
            

        private void cmdclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt =  textBox1.Text.Trim() ;
                if (txt != "")
                {
                    int c = dv.Count;
                    switch (Gvar.rptidx)
                    {

                        case 2:
                        case 3:
                        case 9:
                        case 4:
                            dv.RowFilter = "Item_Code LIKE  '" + txt + "%' OR description LIKE '" + txt + "%'";
                           c = dv.Count;
                           if (c > 0)
                           {
                               c = Convert.ToInt32(dv[0][2].ToString());
                               //lst1.CurrentCell = lst1[0, c - 1];
                           }
                        break;
                        case 7:case 13:case 14:
                            dv.RowFilter = "SITE_code like  '" + txt + "%' OR SITE_NAME LIKE '" + txt + "%'";
                            c = dv.Count;
                    if (c > 0)
                    {
                        c = Convert.ToInt32(dv[0][2].ToString());
                    //lst1.CurrentCell = lst1[0, c - 1];
                    
                    
                    }
                    break;
                        case 8:

                    dv.RowFilter = "Item_Code LIKE  '" + txt + "%' OR description LIKE '" + txt + "%'";
                    c = dv.Count;
                    if (c > 0)
                    {
                        c = Convert.ToInt32(dv[0][2].ToString());
                      //  lst1.CurrentCell = lst1[0, c - 1];
                    }
                    break;

                    }
                    
                }
                else
                    dv.RowFilter = "Item_Code <> '0'";

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
                       if (lst1[0, lst1.CurrentCell.RowIndex].Value==null) lst1[0, lst1.CurrentCell.RowIndex].Value=0;
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
                catch (Exception  ex)
            {
                MessageBox.Show(ex.Message);
                }
        }

        private void lst1_DoubleClick(object sender, EventArgs e)
        {
            if (lst1[0, lst1.CurrentCell.RowIndex].Value == null) lst1[0, lst1.CurrentCell.RowIndex].Value = false;
            lst1[0, lst1.CurrentCell.RowIndex].Value = !(bool)lst1[0, lst1.CurrentCell.RowIndex].Value;
  
        }

        private void chklst1_CheckedChanged(object sender, EventArgs e)
        {
            if (chklst1.Checked)
            {
                textBox1.Enabled = false;
                lst1.Enabled=false;
            }
            else
            {
                textBox1.Enabled = true;
                lst1.Enabled = true;
            }
        }

        private void cmb2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                switch (Gvar.rptidx)
                {

                    case 7:case 13:case 14:

                        if (cmb2.SelectedValue==null) return;

                        DataTable dt2 = new DataTable("site_master");


                        //cmbcatcode.DataSource = dt2;

                        panel1.Visible = true;
                        lbllst1.Text = "Site Name";
                    
                        sql = "sELECT  convert(varchar,site_code) as site_code,Site_Name,row_number() over (order by Site_Code) as Rownum froM site_master    where proj_code=" + cmb2.SelectedValue + " order by site_code" ;

                        SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                        dt2.Clear();
                       
                        ada2.Fill(dt2);
                        

                        dv.Table = dt2;
                        dv1.Table = dt2;
                        
                        lst1.DataSource = dv;
                        dv.Sort = "Site_Code";
                        if (lst1.ColumnCount > 2)
                        {
                            lst1.Columns[0].Width = 50;
                            lst1.Columns[3].Visible = false;
                            lst1.Columns[2].Width = 300;
                            lst1.Columns[1].Width = 100;
                            lst1.ReadOnly = false;
                            lst1.Columns[2].ReadOnly = true;
                            lst1.Columns[1].ReadOnly = true;
                            lst1.Columns[0].ReadOnly = false;
                        }
                        break;
                    case 10: case 11:
                        
                   

                        if (cmb2.SelectedValue==null) return;

                        DataTable lstdt2 = new DataTable("site_master");


                        //cmbcatcode.DataSource = dt2;

                        panel2.Visible = true;
                        lbllst2.Text = "Project Site";    
                    
                        sql = "sELECT  convert(varchar,site_code) as site_code,Site_Name,row_number() over (order by Site_Code) as Rownum froM site_master    where proj_code=" + cmb2.SelectedValue + " order by site_code" ;

                        SqlDataAdapter lst2ada = new SqlDataAdapter(sql, Conn);
                        lstdt2.Clear();

                        lst2ada.Fill(lstdt2);


                        dv2.Table = lstdt2;
                        dv3.Table = lstdt2;
                        
                        lst2.DataSource = dv2;
                        dv2.Sort = "Site_Code";
                        lst2.Columns[0].Width = 50;
                        lst2.Columns[3].Visible = false;
                        lst2.Columns[2].Width = 300;
                        lst2.Columns[1].Width = 175;
                        lst2.ReadOnly = false;
                        lst2.Columns[2].ReadOnly = true;
                        lst2.Columns[1].ReadOnly = true;
                        lst2.Columns[0].ReadOnly = false;
                         lst2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        break;
                }
            
            }
            catch (Exception ex)
                
               
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                switch (Gvar.rptidx)
                {

                    case 8:
                    case 9:

                        if (cmb1.SelectedValue == null) return;

                        if (cmb1.SelectedIndex==0)
                               sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER" ;
                        else
                            sql = "sELECT  Description,Item_Code,row_number() over (order by description) as Rownum froM HD_ITEMMASTER where itm_cat_code= " + cmb1.SelectedValue;

                        SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
                        DataTable dt2 = new DataTable("HD_ITEMMASTER");
                         ada2.Fill(dt2);
                        
                         dv.Table = dt2;
                         dv1.Table = dt2;
                         lst1.DataSource = dv;
                         break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                string txt =  textBox2.Text.Trim() ;
                if (txt != "")
                {
                    int c = dv2.Count;
                    switch (Gvar.rptidx)
                    {


                        case 2:
                        case 3:
                        case 4:
                        case 9:
                            dv2.RowFilter = "acc_no like  '" + txt + "%' OR acc_NAME LIKE '" + txt + "%'";
                            c = dv2.Count;
                            if (c > 0)
                            {
                               // c = Convert.ToInt32(dv3[0][2].ToString());
                              //  lst2.CurrentCell = lst2[0, c - 1];


                            }
                            break;
                        

                    }

                }
                else
                    dv2.RowFilter = "acc_no <> '0'";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lst1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Gvar.rptidx != 4 && Gvar.rptidx != 3 && Gvar.rptidx != 2 && Gvar.rptidx != 9) return;

            if (cmb3.SelectedIndex==0)
            sql = "sELECT  ACC_NO,ACC_NAME,row_number() over (order by ACC_NAME) as Rownum froM ACCOUNTS where acc_type_code in (" + cus_code + ")";
            else
                sql = "sELECT  ACC_NO,ACC_NAME,row_number() over (order by ACC_NAME) as Rownum froM ACCOUNTS where acc_type_code =" + cmb3.SelectedValue ;
            SqlDataAdapter ada2 = new SqlDataAdapter(sql, Conn);
            DataTable dt2 = new DataTable("accounts");
            ada2.Fill(dt2);


            dv2.Table = dt2;
            lst2.DataSource = dv2;
            dv2.Sort = "ACC_NAME";
            lst2.Columns[0].Width = 50;
            //lst1.Columns[3].Visible = false;
            lst2.Columns[2].Width = 300;
            lst2.Columns[1].Width = 175;
            lst2.ReadOnly = false;
            lst2.Columns[2].ReadOnly = true;
            lst2.Columns[1].ReadOnly = true;
            lst2.Columns[0].ReadOnly = false;
            lst2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        //private void test1()
        //{
        //    ReportDocument CrRep = new ReportDocument();
        //    CRT2 = "'0'";

        //    if (lst1.GetItemChecked(0))
        //    {

        //        CRT2 = "{Reciepts.Reciept_no}   <> 0";
        //        fnd = true;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < lst1.Items.Count; i++)
        //        {
        //            if (lst1.GetItemChecked(i))
        //            {
        //                CRT2 = CRT2 + ",'" + lstleaderid.GetItemText(i) + "'";
        //                fnd = true;
        //            }

        //        }

        //        CRT2 = "{SHEET_MASTER.LEADER_NO}  in [" + CRT2 + "]";
        //    }




        //    if (fnd = false && cmb1.SelectedIndex != 1)
        //    {
        //        MessageBox.Show("There no selction for Leader, Please check and Try again", "Invalid Leader");
        //        return;
        //    }



        //    CRT3 = "'0'";

        //    fnd = false;


        //    if (lst2.GetItemChecked(0))
        //    {

        //        CRT3 = "{Reciepts.Reciept_no}   <> 0";
        //        fnd = true;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < lst2.Items.Count; i++)
        //        {
        //            if (lst2.GetItemChecked(i))
        //            {
        //                CRT3 = CRT3 + ",'" + lstsheetno.GetItemText(i) + "'";
        //                fnd = true;
        //            }

        //        }
        //    }




        //    if (fnd == false)
        //    {
        //        MessageBox.Show("There no selction for Master Sheet, Please check and Try again", "Invalid Master Sheet");
        //        return;
        //    }

        //    string sdt1 = dt1.Value.ToString("yyyy,MM,dd,00,00,00");
        //    string sdt2 = dt2.Value.ToString("yyyy,MM,dd,23,59,59");

        //    switch (Gvar._Gind)
        //    {
        //        case 0:
        //            {

        //                crt1 = "  {Reciepts.Cur_date} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";
        //                RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value;
        //                rep_path = Application.StartupPath + "\\Report\\RptReceipt.rpt";

        //                // rep_path = App.Path & "\\reports\\RptReceipt.rpt";
        //                //Set CrRep = CrAppl.OpenReport(rep_path)'
        //                rep_formula = crt1 + " AND " + CRT2 + " AND " + CRT3;

        //                switch (cmb1.SelectedIndex)
        //                {
        //                    case 0:
        //                        {

        //                            RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value;
        //                        }
        //                        break;
        //                    case 1:
        //                        {
        //                            rep_formula = rep_formula + " AND {Reciepts.Entry_Type} = 0";
        //                            RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value + " for " + cmb1;
        //                        }
        //                        break;

        //                    case 2:
        //                        {
        //                            rep_formula = rep_formula + " AND {Reciepts.Entry_Type} = 1";
        //                            RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value;
        //                        }
        //                        break;


        //                }




        //            }
        //            break;


        //        case 1:
        //            {
        //                crt1 = "  {Reciepts.Cur_date} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";


        //                RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value;

        //                rep_path = Application.StartupPath + "\\reports\\RptReceiptBYLEADER.rpt";

        //                rep_formula = crt1 + " AND " + CRT2 + " AND " + CRT3;

        //            }
        //            break;
        //        case 2:
        //            {
        //                crt1 = "  {Reciepts.Cur_date} in DateTime (" + sdt1 + ") to DateTime (" + sdt2 + ")";

        //                RPTHEAD = "Receipt Entry Detail From " + dt1.Value + " To " + dt2.Value;


        //                rep_path = Application.StartupPath + "\\reports\\RptReceiptBYSHEET.rpt";
        //                //Set CrRep = CrAppl.OpenReport(rep_path)
        //                rep_formula = crt1 + " AND " + CRT2 + " AND " + CRT3;
        //            }
        //            break;


        //    }

        //    CrRep.Load(rep_path);

        //    //MessageBox.Show(rep_formula);
        //    if (rep_formula != "")
        //    {
        //        CrRep.RecordSelectionFormula = rep_formula;
        //    }

        //    CrRep.SummaryInfo.ReportTitle = RPTHEAD;





        //    CrystalDecisions.Shared.ConnectionInfo crconnectioninfo = new CrystalDecisions.Shared.ConnectionInfo();
        //    CrystalDecisions.Shared.TableLogOnInfos crtablelogoninfos = new CrystalDecisions.Shared.TableLogOnInfos();
        //    CrystalDecisions.Shared.TableLogOnInfo crtablelogoninfo = new CrystalDecisions.Shared.TableLogOnInfo();

        //    Tables CrTables;

        //    crconnectioninfo.ServerName = decoder.DataSource;
        //    crconnectioninfo.DatabaseName = decoder.InitialCatalog;
        //    crconnectioninfo.UserID = decoder.UserID;
        //    crconnectioninfo.Password = decoder.Password;



        //    CrTables = CrRep.Database.Tables;

        //    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //    {
        //        crtablelogoninfo = CrTable.LogOnInfo;
        //        crtablelogoninfo.ConnectionInfo = crconnectioninfo;
        //        CrTable.ApplyLogOnInfo(crtablelogoninfo);
        //    }





        //    CrRep.SetDatabaseLogon(decoder.UserID, decoder.Password, decoder.DataSource, decoder.InitialCatalog);
        //    // crystalReportViewer.ReportSource = reportDocument;
        //    //ConnectionInfo connInfo = new ConnectionInfo();
        //    //connInfo.ServerName = "dbservername";
        //    //connInfo.DatabaseName = "dbname";
        //    //connInfo.UserID = "dbusername";
        //    //connInfo.Password = "dbpassword";
        //    //reportViewer.ReportSource = GetReportSource(connInfo);
        //    //reportViewer.RefreshReport();




        //    // CrRep.Database.SetDataSource (db)
        //    // CrRep.VerifyOnEveryPrint = False
        //    //CrRep.DiscardSavedData
        //    //CrRep.ReadRecords
        //    //CrRep.Load(rep_path);


        //    //Crv1.ReportSource = CrRep;
        //    //Crv1.Width = this.Width;




        //    FrmrepView frm = new FrmrepView();
        //    frm.MdiParent = this.ParentForm;

        //    frm.crv1.ReportSource = CrRep;
        //    frm.Show();

        //    //Control[] ctrls = frm.Controls.Find("crv1", false);
        //    //if (ctrls.Length > 0)
        //    //{

        //    //    CrystalDecisions.Windows.Forms.CrystalReportViewer  rep = (CrystalDecisions.Windows.Forms.CrystalReportViewer) ctrls[0];

        //    //     rep.ReportSource = CrRep;
        //    //     frm.Show();
        //    //}















        //    //FRMREPORT.CRV1.ReportSource = CrRep
        //    //FRMREPORT.CRV1.Refresh
        //    //FRMREPORT.CRV1.RefreshEx (True)

        //    //FRMREPORT.CRV1.ViewReport

        //    //FRMREPORT.Show

        //}
        
    }
}
