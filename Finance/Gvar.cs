using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ADODB;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace FinOrg
{
    
    class Gvar
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

            
        }
        
        public static string username;
        public static int _Userid;
        public static int usr_wrcode;
        public static string brn_name;
        public static string cmp_name;
        public static string Glb_strval;

        public static string report_path;
        public static string frm_priv;
        public static bool frm_search;
        public static string invno;
        public static int rptidx;
        public static int trntype;
        public static int multibrn;
        public static int multiwr;
        public static string menu_dock;
       
        public static int pos_user;
        public static int nyear;
        public static string orgdup;
        public static string _currency;
        public static decimal _cur_rate;
        public static string sale_acno;
        public static string pur_acno;

        public static string _defaultcolor;


//public static string _ArCalendar();

        public static string trn_no(int trntype)
        {
            try
            {

                SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                Conn.Close();
                Conn.Open();


                string sql;
                sql = "SELECT MAX(CAST(INVOICE_NO AS NUMERIC))+1 FROM  DATA_ENTRY WHERE TRN_TYPE=" + trntype;
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            return rd[0].ToString();

                        }
                    }
                }

                return "1";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "0";
            }
        }


        public static  void Send_Sms_test(string sms)
        {

            
            
                string username = "limar";
                string pwd = "15limar123";
                string strLanguage = "64";

                string mobileno = "966500175709";
                string smsid = "Limar";
                string mesg = "Test Message مجيب بولوملايل";

                string strURL = "http://smsc1.kaamilsms.com/kaamil/custapi/sendTextSMS_single_http.cfm";
                var postData = "usrName=" + username + "&usrPass=" + pwd;
                postData = postData + "&msgLNG=" + strLanguage + "&msgSenderID=" + smsid + "&gsmNumber=" + mobileno;
                postData = postData + "&msgtext=" + ConvertToUnicode(mesg) + "&gsmMCC=966";

                var request = (HttpWebRequest)System.Net.WebRequest.Create(strURL);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
               
           

        }
        public static   string Send_Sms(string url, string postdata, string mesg, int lang)
        {

                
               // mesg = "Dear client, # we would like to inform you that we have completed the work required on your vehicle $eplateno  you can pass by to collect it, # # Limar Sun Center";

               

                //  var request = (HttpWebRequest)WebRequest.Create("http://smsc1.kaamilsms.com/kaamil/custAPI/sendTextSMS_Single_http.cfm");
                //  var postData = "usrName=limar&usrPass=15limar123&msgLNG=0&msgSenderID=Limar&gsmNumber=966500175709";
                //  postData += "&msgtext=" + "test message from mujeeb";

                var request = (HttpWebRequest)WebRequest.Create(url);

                
                //postdata = "usrName=limar&usrPass=15limar123&msgLNG=0&msgSenderID=Limar&gsmNumber=966500175709&msgtext=";

               

                //if (lang == 0)
                //{
                //    postdata += mesg;
                //    string strLanguage = "0";
                  



                //    //postdata = postdata.Replace("$strlang", strLanguage);



                //}
                //else
                //{
                //    postdata += mesg;
                //    string strLanguage = "64";
                   
                //    postdata = postdata + ConvertToUnicode(mesg);

                //    // postdata = postdata.Replace("رقم", ConvertToUnicode(plateno));
                //    postdata = postdata.Replace("$strlang", strLanguage);

                //}

                //var request = (HttpWebRequest)WebRequest.Create("http://smsc1.kaamilsms.com/kaamil/custAPI/sendTextSMS_Single_http.cfm");
               // var postData = "usrName=limar&usrPass=15limar123&msgLNG=0&msgSenderID=Limar&gsmNumber=966500175709";
                postdata +=  mesg;
                //postData += "&msgtext=" + ConvertToUnicode(txtMessage.Text);
                var data = Encoding.ASCII.GetBytes(postdata);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                return responseString.ToString();
            
                //try
                //{
                //    var response = (HttpWebResponse)request.GetResponse();

                //    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //    return responseString;
                //}
                //catch (Exception ex)
                //{
                //    return ex.Message;

                //}



            }


      
        public static string build_sms(ADODB.Recordset  rec )          
        {

                SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                            ADODB.Connection ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                string sql;


                sql = "SELECT * from sms_ini";

                ADODB.Recordset sms = new Recordset();
                
                sms.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               

    
            string post_data = sms.Fields["sms_parameter"].Value.ToString();
            //post_data = post_data.Replace("$strlang", cb_sms_lang.SelectedIndex.ToString());
            post_data = post_data.Replace("$smsid", sms.Fields["sms_header"].Value.ToString());

            while (!rec.EOF)
            {
            string mob_no = rec.Fields["ACC_MOBILE_NO"].Value.ToString().Trim();
            if (mob_no.Length > 9)
            {
                mob_no = "966" + mob_no.Substring(mob_no.Length - 9);

            }
            else
            {
                mob_no = "0";
                return "";
            }
            // == "\r\n"
            post_data = post_data.Replace("$mobileno", mob_no);
            post_data = post_data.Replace("$smsid", sms.Fields["sms_header"].Value.ToString());
            string mesg = "";
           
                mesg = sms.Fields["sms_customer_en"].Value.ToString();
                mesg = mesg.Replace("$payamt", rec.Fields["payamt"].Value.ToString());
                mesg = mesg.Replace("$balamt", rec.Fields["balamt"].Value.ToString());

                mesg = mesg.Replace("#", "\n");
           
                       string responseString =  Send_Sms(sms.Fields["sms_url"].Value.ToString(), post_data, mesg,0);

           // Task taskA = new Task( () => Send_Sms(sms.Fields["sms_url"].Value.ToString(), post_data, mesg,1));
      //Start the task.
     // taskA.Start();
    // taskA.Wait();
      
            rec.MoveNext();
        }
            return "";

           

      
            //foreach (DataRowView r in sms)
            //{

            //    if (!Validation.IsNull(r["sms_ini"]))


            //         r["veh_model_code"] [0][0];
            //}

        }

        public static string GetDateCast(DateTime t)
        {
            if (t == null)
                return null;

            return "CAST('" + t.ToString("MM/dd/yyyy h:mm:ss tt") + "' as datetime)";
        }

        private static string ConvertToUnicode(string val)
        {
            string msg2 = string.Empty;

            for (int i = 0; i < val.Length; i++)
            {
                msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
            }

            return msg2;
        }
        //------------------------------ related funciton -----------------------------
        private static string convertToUnicode(char ch)
        {
            System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
            byte[] msg = class1.GetBytes(System.Convert.ToString(ch));

            return fourDigits(msg[1] + msg[0].ToString("X"));
        }
        private static string fourDigits(string val)
        {
            string result = string.Empty;

            switch (val.Length)
            {
                case 1: result = "000" + val; break;
                case 2: result = "00" + val; break;
                case 3: result = "0" + val; break;
                case 4: result = val; break;
            }

            return result;
        }
        public static bool isdate(String date)
        {

            try
            {

                DateTime dt = DateTime.Parse(date);

                return true;
            }
            catch
            {

                return false;

            }

        }

        public class Menu_item
        {

            public string Menu { get; set; }
            public string priv { get; set; }
            public string code { get; set; }
            public string mtype { get; set; }
            public string head { get; set; }

        }

        public static List<Menu_item> User_Menu = new List<Menu_item>(); 


        public static int Userid
       
        {
            get { return _Userid; }
            set { _Userid = value; }
        }


        public static int _Gind;
        public static int Gind
        {
            get { return _Gind; }
            set { _Gind = value; }
        }


       

        public static int _brn_code;
        public static int brn_code
        {
            get { return _brn_code; }
            set { _brn_code = value; }
        }

        public static int wr_code;
        
        public static int _SuperUserid;
        //public static string _ArCalendar();
        public static int SuperUserid
        {
            get { return _SuperUserid; }
            set { _SuperUserid = value; }
        }

        public static int _EntryUserid;
        //public static string _ArCalendar();
        public static int EntryUserid
        {
            get { return _EntryUserid; }
            set { _EntryUserid = value; }
        }


        public static string ArCalendar(DateTime dt)
        {
           DateTime miladi = dt;
           
            string ardate;
                          
           System.Globalization.HijriCalendar shamsi = new System.Globalization.HijriCalendar();
           ardate = string.Format(string.Format("{2}/{1}/{0}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi)),"dd/MM/yyyy");
           
                return ardate;
             
            //   get { return ardate; }
            //set { _ArCalendar = value; }          

        }

        public static string trn_no(int trntype,string entry_month)
{
try
{
    
        SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
 Conn.Close();
     Conn.Open();
  
    
    string sql;
             sql = "SELECT MAX(CAST(INVOICE_NO AS NUMERIC))+1 FROM  DATA_ENTRY WHERE entry_month ='"+  entry_month +"' and TRN_TYPE=" + trntype;
                SqlCommand cmd = new SqlCommand(sql, Conn);
                //SqlDataReader rd = cmd.ExecuteReader();





                SqlDataReader rd = cmd.ExecuteReader();



                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            return rd[0].ToString();
                           
                        }
                    }
                }

    return "1";


}
catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
 return "0";
    }
}


        public static void update_stock()
        {
            try
            {

                SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                Conn.Close();
                Conn.Open();
                ADODB.Connection ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                string sql;


                sql = "SELECT item_code,STOCK FROM STOCK_ITEM WHERE WR_CODE=" + Gvar.wr_code;

                ADODB.Recordset tmp = new Recordset();
                ADODB.Recordset tmp1 = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
                do
                {
                    object ST = 0;
                    object itm = "";
                    ST = 0;
                    // if (tmp.RecordCount != 0) ST = tmp.Fields[0].Value;

                    ST = tmp.Fields[1].Value;
                    itm = tmp.Fields[0].Value;

                    sql = "SELECT * FROM WR_STOCK_MASTER WHERE WR_CODE=" + Gvar.wr_code + " AND ITEM_CODE='" + itm + "'";
                    tmp1 = new Recordset();
                    tmp1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);



                    tmp1.Fields["stock"].Value = ST;
                    tmp1.Update();


                    sql = "SELECT SUM(STOCK) FROM  WR_STOCK_MASTER WHERE  ITEM_CODE='" + itm + "'";
                     tmp1 = new Recordset();
                    tmp1.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);

                    ST = 0;


                    if (!tmp1.EOF) ST = tmp1.Fields[0].Value;

                    if (!Convert.IsDBNull(tmp1.Fields[0].Value))


                        sql = "Update STOCK_MASTER set stock = " + Math.Round(Convert.ToDecimal(tmp1.Fields[0].Value), 2) + " where    ITEM_CODE='" + itm + "'";
                    else
                        sql = "Update STOCK_MASTER set stock = 0 where    ITEM_CODE='" + itm + "'";

                    object a;
                   
                    ADOconn.Execute(sql, out a, -1);


                    tmp.MoveNext();
                } while (!tmp.EOF);
            }



            catch(Exception ex)
            {

            }


        }

        public static double Get_Currency_rate(double acc_no,string currency)
        {
            try
            {

                SqlConnectionStringBuilder decoder = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                Conn.Close();
                Conn.Open();
                ADODB.Connection ADOconn = new ADODB.Connection();
                ADOconn.Open("Provider=SQLOLEDB;Initial Catalog= " + decoder.InitialCatalog + ";Data Source=" + decoder.DataSource + ";", decoder.UserID, decoder.Password, 0);

                string sql;


                sql = "SELECT C.currency_code as ac_currency,C.currency_rate as ac_rate,c1.currency_code, c1.currency_rate FROM currency_master AS C " +
                "inner join accounts on def_currency=currency_CODE inner join currency_master as c1 on c1.currency_code='" + currency + "'  WHERE acc_no=" + acc_no;


                ADODB.Recordset tmp = new Recordset();
                ADODB.Recordset tmp1 = new Recordset();
                tmp.Open(sql, ADOconn, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic, -1);
               

                do
                {

                    if (tmp.Fields["ac_currency"].Value.ToString() == tmp.Fields["currency_code"].Value.ToString())
                        //return 1;
                         return Convert.ToDouble(tmp.Fields["ac_rate"].Value);
                    else

                        return Convert.ToDouble(tmp.Fields["ac_rate"].Value);

                    tmp.MoveNext();
                    
                } while (!tmp.EOF);

                return 1;
            }
               

            catch (Exception ex)
            {
                 return 1;
            }
            


        }
    }
}
