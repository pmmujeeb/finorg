using System.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace StartApp
{



    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static public bool IsProcessOpen(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running.
                //Remember, if you have the process running more than once, 
                //say IE open 4 times the loop thr way it is now will close all 4,
                //if you want it to just close the first one it finds
                //then add a return; after the Kill
                if (clsProcess.ProcessName.Contains(name))
                {
                    //if the process is found to be running then we
                    //return a true
                    return true;
                }
            }
            //otherwise we return a false
            return false;
        }

        static void Main()
        {

            try
            {



                
                


                if (File.GetLastWriteTime(Application.StartupPath + "\\logs\\DropBox\\Cardex.exe") > File.GetLastWriteTime(Application.StartupPath + "\\Cardex.exe"))
                {
                    
                agn:
                    if (IsProcessOpen(Application.StartupPath + "\\Cardex.exe"))
                    {
                      
                      DialogResult result;
                      result = MessageBox.Show("Please close The Application \n" + "الرجاء إغلاق البرنامج مفتوح وحاول مرة أخرى ", "Copy New Program", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                      // Displays the MessageBox.


                      if (result == DialogResult.Cancel)
                      {

                          // Closes the parent form. 

                          Environment.Exit(0);

                      }

                      if (result == DialogResult.Retry)
                      {

                          // Closes the parent form. 

                          goto agn;

                      }

                    }

                File.Copy(Application.StartupPath + "\\logs\\DropBox\\Cardex.exe", Application.StartupPath + "\\Cardex.exe", true);

                }
                Process proc = Process.Start(Application.StartupPath + "\\Cardex.exe");
               
                //ConnectionOptions theConnection = new ConnectionOptions();
                ////theConnection.Username = "manu";
                ////theConnection.Password = "nopass";
                //theConnection.Impersonation = ImpersonationLevel.Impersonate;

                //ManagementScope theScope = new ManagementScope(String.Format(@"\\{0}\root\directory", "manu-pc"), theConnection);
                //theScope.Connect();





                //ManagementObjectSearcher search = new ManagementObjectSearcher(String.Format(@"\{0}\\root\\CIMV2", "SELECT * FROM Win32_BaseBoard"));

                //foreach (ManagementObject obj in search.Get())
                //{

                //    string info = obj["Model"].ToString();

                //    ;

                //}

              // ManagementScope theScope = new  ManagementScope(String.Format(@"\\{0}\root\cimv2", conDet.Server), theConnection);

                Environment.Exit(0);

                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
            }
            finally
            {
            }
        }
    


        
}

}


