using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinOrg
{
    public partial class FrmrepView : Form
    {
        public FrmrepView()
        {
            InitializeComponent();
        }

        private void FrmrepView_Load(object sender, EventArgs e)
        {

           // this.reportViewer1.RefreshReport();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
