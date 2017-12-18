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
namespace FinOrg
{
    public partial class Frmtools : Form

    {
        SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
     
        public Frmtools()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           sf1.InitialDirectory = Application.StartupPath;
           sf1.ShowDialog();
       textBox1.Text = sf1.FileName;
        }

        private void btnbackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid FileName");
                    return;
                }


                string sql = "BACKUP DATABASE [CardexNet] TO  DISK = N'" + textBox1.Text.Trim() + "' WITH NOFORMAT, INIT,  NAME = N'CardexNet-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);

                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();

                Conn.Close();

                MessageBox.Show("Successfully Backed Up to " + textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

                
           
        }

        private void btmrestore_Click(object sender, EventArgs e)
        {
           
         try
            {
                if (textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid FileName");
                    return;
                }
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Mstr"].ConnectionString);

                Conn.Close();
             

                string sql = " RESTORE DATABASE [CardexNet] FROM  DISK = N'" + textBox2.Text.Trim() + "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10";

                
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();

                Conn.Close();

                MessageBox.Show("Successfully Restored The Backup file from " + textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            of1.InitialDirectory = Application.StartupPath;
            of1.ShowDialog();
            textBox2.Text = of1.FileName;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
