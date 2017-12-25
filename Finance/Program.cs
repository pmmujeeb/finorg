using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
//using Microsoft.VisualBasic;

using ADODB;
namespace FinOrg
    
{
  
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
       

        [STAThread]

        static void Main()
        {
           
            try
            {
                //if (DateTime.Now > Convert.ToDateTime("31-dec-2016"))
                //{
                //    MessageBox.Show("Please Contact For support, pmmujeeb@hotmailcom", "Expired License");
                //    //Environment.Exit(0);as
                  //}
                //if (DateTime.Now > Convert.ToDateTime("01-dec-2016"))
                //{
                //    MessageBox.Show("Your 90 Days Grace Period will expire soon..For support, pmmujeeb@hotmailcom", "Expiring License");
                
                //}


                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Languages.Init();
                Application.Run(new Frmlogin());
                return;
              
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
                return;
            }




        }

        public static string ledger_ini(int trntype,string invno)
        {
            try
            {
                object TRNBY = 0;
                object DRCR = "D";
                object DRCR1 = "C";
                object NARR = "";
                object LACC = 0;
                object PAYBY = 0;
                object vat_acc = 0;
                object exp_acc = 0;

                ADODB.Connection ADOconn = new ADODB.Connection();
            ADODB.Recordset tmp = new ADODB.Recordset();

            SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
      

                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                ADODB.Recordset rec = new ADODB.Recordset();
                string sql = "SELECT * FROM TRN_TYPE WHERE TRN_code= " + trntype;

                Recordset TMP = new Recordset();
                TMP.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                if (TMP.RecordCount == 0)
                    LACC = 999;
                else
                {
                    LACC = TMP.Fields["ACC_NO"].Value;
                    TRNBY = TMP.Fields["TRN_CODE"].Value;
                    NARR = TMP.Fields["ABRV"].Value + invno;
                    DRCR = TMP.Fields["DRCR"].Value;
                    DRCR1 = TMP.Fields["DRCR1"].Value; ;

                    PAYBY = TMP.Fields["PAYBY"].Value; ;
                    vat_acc = TMP.Fields["VAT_ACC"].Value;
                    exp_acc = TMP.Fields["exp_acc"].Value;
                }

                //switch (trntype)
                //{
                //    case 0:
                //        {
                //            TRNBY = 0;
                //            NARR = "OP. close " + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 1;

                //        }
                //        break;
                //    case -1:
                //        {
                //            TRNBY = -1;
                //            NARR = "PRODUCT ITEM" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 0;

                //        }
                //        break;

                //    case -2:
                //        {
                //            TRNBY = -2;
                //            NARR = "EXTRA ITEM" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 0;

                //        }
                //        break;
                //    case 1:
                //        {
                //            TRNBY = 1;
                //            NARR = "CPUR " + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 1;

                //        }
                //        break;
                //    case 2:
                //        {
                //            TRNBY = 2;
                //            NARR = "RPUR" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 2;

                //        }
                //        break;
                //    case 8:
                //        {
                //            TRNBY = 8;
                //            NARR = "CPURET" + invno;
                //            DRCR = "D";
                //            DRCR1 = "C";

                //            PAYBY = 3;

                //        }
                //        break;
                //    case 9:
                //        {
                //            TRNBY = 9;
                //            NARR = "RPURET" + invno;
                //            DRCR = "D";
                //            DRCR1 = "C";

                //            PAYBY = 4;

                //        }
                //        break;
                //    case 6:
                //        {
                //            TRNBY = 6;
                //            NARR = "CSA" + invno;
                //            DRCR = "D";
                //            DRCR1 = "C";

                //            PAYBY = 5;

                //        }
                //        break;
                //    case 7:
                //        {
                //            TRNBY = 7;
                //            NARR = "RSA" + invno;
                //            DRCR = "D";
                //            DRCR1 = "C";

                //            PAYBY = 6;

                //        }
                //        break;
                //    case 3:
                //        {
                //            TRNBY = 3;
                //            NARR = "CSARET" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 7;

                //        }
                //        break;
                //    case 4:
                //        {
                //            TRNBY = 4;
                //            NARR = "RSARET" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 8;

                //        }
                //        break;

                //    case 11:
                //        {
                //            TRNBY = 11;
                //            NARR = "DAM. ITEMS" + invno;
                //            DRCR = "D";
                //            DRCR1 = "C";

                //            PAYBY = 11;

                //        }
                //        break;

                //    case 14:
                //        {
                //            TRNBY = 14;
                //            NARR = "DEL. ITEMS" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 14;

                //        }
                //        break;

                //    case 15:
                //        {
                //            TRNBY = 15;
                //            NARR = "DEL. ITEMS" + invno;
                //            DRCR = "C";
                //            DRCR1 = "D";

                //            PAYBY = 15;

                //        }
                //        break;
                //}


                return TRNBY + "`" + DRCR + "`" + DRCR1 + "`" + NARR + "`" + LACC + "`" + PAYBY+"`" + vat_acc + "`" + exp_acc;
            }
            catch
            {
                return "";
            }
        }

        //public static bool isdate(String date)
        //{

        //    try
        //    {

        //        DateTime dt = DateTime.Parse(date);

        //        return true;
        //    }
        //    catch
        //    {

        //        return false;

        //    }

        //}

        
        }


    
}
