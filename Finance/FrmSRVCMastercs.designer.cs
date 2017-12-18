namespace FinOrg
{
    partial class FrmSRVCMaster
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSRVCMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SearchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolRefund = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tooldelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolclose = new System.Windows.Forms.ToolStripButton();
            this.cmbcat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txtitem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtalias = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbunit = new System.Windows.Forms.ComboBox();
            this.txtfraction = new System.Windows.Forms.TextBox();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtcost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grditem = new System.Windows.Forms.DataGridView();
            this.txtclstock = new System.Windows.Forms.TextBox();
            this.txtopstock = new System.Windows.Forms.TextBox();
            this.cmbcatcode = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblhead = new System.Windows.Forms.Label();
            this.txtpriv = new System.Windows.Forms.TextBox();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grditem)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.SearchToolStripButton,
            this.toolStripSeparator3,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolRefund,
            this.toolStripSeparator2,
            this.tooldelete,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolclose});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Stretch = true;
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.newToolStripButton, "newToolStripButton");
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // SearchToolStripButton
            // 
            resources.ApplyResources(this.SearchToolStripButton, "SearchToolStripButton");
            this.SearchToolStripButton.Name = "SearchToolStripButton";
            this.SearchToolStripButton.Click += new System.EventHandler(this.SearchToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // saveToolStripButton
            // 
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // printToolStripButton
            // 
            resources.ApplyResources(this.printToolStripButton, "printToolStripButton");
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // toolRefund
            // 
            resources.ApplyResources(this.toolRefund, "toolRefund");
            this.toolRefund.Name = "toolRefund";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tooldelete
            // 
            resources.ApplyResources(this.tooldelete, "tooldelete");
            this.tooldelete.Name = "tooldelete";
            this.tooldelete.Click += new System.EventHandler(this.tooldelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolclose
            // 
            resources.ApplyResources(this.toolclose, "toolclose");
            this.toolclose.Name = "toolclose";
            this.toolclose.Click += new System.EventHandler(this.toolclose_Click);
            // 
            // cmbcat
            // 
            this.cmbcat.DisplayMember = "itm_cat_name";
            this.cmbcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcat.FormattingEnabled = true;
            resources.ApplyResources(this.cmbcat, "cmbcat");
            this.cmbcat.Name = "cmbcat";
            this.cmbcat.ValueMember = "itm_cat_code";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Txtitem
            // 
            resources.ApplyResources(this.Txtitem, "Txtitem");
            this.Txtitem.Name = "Txtitem";
            this.Txtitem.Tag = "1";
            this.Txtitem.Validated += new System.EventHandler(this.Txtitem_Validated);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dt1
            // 
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dt1, "dt1");
            this.dt1.Name = "dt1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtalias);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.cmbunit);
            this.groupBox1.Controls.Add(this.txtfraction);
            this.groupBox1.Controls.Add(this.txtname);
            this.groupBox1.Controls.Add(this.txtcost);
            this.groupBox1.Controls.Add(this.dt1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbcat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txtitem);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label7.Name = "label7";
            // 
            // txtalias
            // 
            resources.ApplyResources(this.txtalias, "txtalias");
            this.txtalias.Name = "txtalias";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label9.Name = "label9";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label25.Name = "label25";
            // 
            // cmbunit
            // 
            this.cmbunit.DisplayMember = "Unit_name";
            this.cmbunit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbunit.FormattingEnabled = true;
            resources.ApplyResources(this.cmbunit, "cmbunit");
            this.cmbunit.Name = "cmbunit";
            this.cmbunit.ValueMember = "Unit_id";
            // 
            // txtfraction
            // 
            resources.ApplyResources(this.txtfraction, "txtfraction");
            this.txtfraction.Name = "txtfraction";
            this.txtfraction.Tag = "1";
            // 
            // txtname
            // 
            resources.ApplyResources(this.txtname, "txtname");
            this.txtname.Name = "txtname";
            this.txtname.Tag = "1";
            // 
            // txtcost
            // 
            this.txtcost.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtcost, "txtcost");
            this.txtcost.Name = "txtcost";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.Name = "label5";
            // 
            // grditem
            // 
            this.grditem.AllowUserToAddRows = false;
            this.grditem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grditem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grditem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.grditem, "grditem");
            this.grditem.Name = "grditem";
            this.grditem.ReadOnly = true;
            this.grditem.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grditem_RowEnter);
            // 
            // txtclstock
            // 
            resources.ApplyResources(this.txtclstock, "txtclstock");
            this.txtclstock.Name = "txtclstock";
            // 
            // txtopstock
            // 
            resources.ApplyResources(this.txtopstock, "txtopstock");
            this.txtopstock.Name = "txtopstock";
            // 
            // cmbcatcode
            // 
            this.cmbcatcode.DisplayMember = "itm_cat_code";
            this.cmbcatcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcatcode.FormattingEnabled = true;
            resources.ApplyResources(this.cmbcatcode, "cmbcatcode");
            this.cmbcatcode.Name = "cmbcatcode";
            this.cmbcatcode.ValueMember = "itm_cat_code";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // lblhead
            // 
            resources.ApplyResources(this.lblhead, "lblhead");
            this.lblhead.ForeColor = System.Drawing.Color.Red;
            this.lblhead.Name = "lblhead";
            this.lblhead.Tag = "I";
            // 
            // txtpriv
            // 
            resources.ApplyResources(this.txtpriv, "txtpriv");
            this.txtpriv.Name = "txtpriv";
            // 
            // FrmSRVCMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.txtpriv);
            this.Controls.Add(this.lblhead);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmbcatcode);
            this.Controls.Add(this.txtopstock);
            this.Controls.Add(this.txtclstock);
            this.Controls.Add(this.grditem);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "FrmSRVCMaster";
            this.Load += new System.EventHandler(this.Frmentry_Load_1);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grditem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton SearchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox cmbcat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txtitem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtfraction;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtcost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grditem;
        private System.Windows.Forms.ToolStripButton toolRefund;
        private System.Windows.Forms.ToolStripButton tooldelete;
        private System.Windows.Forms.ToolStripButton toolclose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbunit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtalias;
        private System.Windows.Forms.TextBox txtclstock;
        private System.Windows.Forms.TextBox txtopstock;
        private System.Windows.Forms.ComboBox cmbcatcode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblhead;
       public System.Windows.Forms.TextBox txtpriv;
    }
}