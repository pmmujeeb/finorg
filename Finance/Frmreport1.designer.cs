namespace FinOrg
{
    partial class frmreport1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.maintab1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtmove = new System.Windows.Forms.TextBox();
            this.chkminstock = new System.Windows.Forms.CheckBox();
            this.chkstock = new System.Windows.Forms.CheckBox();
            this.lblmsg = new System.Windows.Forms.Label();
            this.cmb3 = new System.Windows.Forms.ComboBox();
            this.lblcmb3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chklst2 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lst2 = new System.Windows.Forms.DataGridView();
            this.lbllst2 = new System.Windows.Forms.Label();
            this.grpdate = new System.Windows.Forms.Panel();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.repdt2 = new System.Windows.Forms.DateTimePicker();
            this.lbldt2 = new System.Windows.Forms.Label();
            this.repdt1 = new System.Windows.Forms.DateTimePicker();
            this.lbldt1 = new System.Windows.Forms.Label();
            this.cmb2 = new System.Windows.Forms.ComboBox();
            this.lblcmb2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chklst1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lst1 = new System.Windows.Forms.DataGridView();
            this.lbllst1 = new System.Windows.Forms.Label();
            this.cmdclose = new System.Windows.Forms.Button();
            this.cmb1 = new System.Windows.Forms.ComboBox();
            this.lblcmb1 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.maintab1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst2)).BeginInit();
            this.grpdate.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst1)).BeginInit();
            this.SuspendLayout();
            // 
            // maintab1
            // 
            this.maintab1.Controls.Add(this.tabPage1);
            this.maintab1.Location = new System.Drawing.Point(12, 12);
            this.maintab1.Name = "maintab1";
            this.maintab1.SelectedIndex = 0;
            this.maintab1.Size = new System.Drawing.Size(830, 640);
            this.maintab1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.maintab1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtmove);
            this.tabPage1.Controls.Add(this.chkminstock);
            this.tabPage1.Controls.Add(this.chkstock);
            this.tabPage1.Controls.Add(this.lblmsg);
            this.tabPage1.Controls.Add(this.cmb3);
            this.tabPage1.Controls.Add(this.lblcmb3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.grpdate);
            this.tabPage1.Controls.Add(this.cmb2);
            this.tabPage1.Controls.Add(this.lblcmb2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.cmdclose);
            this.tabPage1.Controls.Add(this.cmb1);
            this.tabPage1.Controls.Add(this.lblcmb1);
            this.tabPage1.Controls.Add(this.btnView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(822, 614);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Options";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // txtmove
            // 
            this.txtmove.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtmove.Location = new System.Drawing.Point(707, 322);
            this.txtmove.Name = "txtmove";
            this.txtmove.Size = new System.Drawing.Size(62, 24);
            this.txtmove.TabIndex = 35;
            this.txtmove.Text = "30";
            this.txtmove.Visible = false;
            // 
            // chkminstock
            // 
            this.chkminstock.AutoSize = true;
            this.chkminstock.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkminstock.ForeColor = System.Drawing.Color.Red;
            this.chkminstock.Location = new System.Drawing.Point(196, 321);
            this.chkminstock.Name = "chkminstock";
            this.chkminstock.Size = new System.Drawing.Size(228, 21);
            this.chkminstock.TabIndex = 20;
            this.chkminstock.Text = "Show only Not in Minimum Stock";
            this.chkminstock.UseVisualStyleBackColor = true;
            this.chkminstock.Visible = false;
            this.chkminstock.CheckedChanged += new System.EventHandler(this.chklst1_CheckedChanged);
            // 
            // chkstock
            // 
            this.chkstock.AutoSize = true;
            this.chkstock.Checked = true;
            this.chkstock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkstock.Location = new System.Drawing.Point(465, 321);
            this.chkstock.Name = "chkstock";
            this.chkstock.Size = new System.Drawing.Size(152, 17);
            this.chkstock.TabIndex = 34;
            this.chkstock.Text = "Show Only Stock Available";
            this.chkstock.UseVisualStyleBackColor = true;
            this.chkstock.Visible = false;
            // 
            // lblmsg
            // 
            this.lblmsg.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(39, 588);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(786, 21);
            this.lblmsg.TabIndex = 33;
            this.lblmsg.Text = "....";
            // 
            // cmb3
            // 
            this.cmb3.FormattingEnabled = true;
            this.cmb3.Items.AddRange(new object[] {
            "All",
            "Social Club Re-Creation",
            "Social Club Business"});
            this.cmb3.Location = new System.Drawing.Point(177, 321);
            this.cmb3.Name = "cmb3";
            this.cmb3.Size = new System.Drawing.Size(523, 21);
            this.cmb3.TabIndex = 24;
            this.cmb3.Visible = false;
            this.cmb3.SelectedIndexChanged += new System.EventHandler(this.cmb3_SelectedIndexChanged);
            // 
            // lblcmb3
            // 
            this.lblcmb3.AutoSize = true;
            this.lblcmb3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcmb3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblcmb3.Location = new System.Drawing.Point(61, 319);
            this.lblcmb3.Name = "lblcmb3";
            this.lblcmb3.Size = new System.Drawing.Size(97, 19);
            this.lblcmb3.TabIndex = 23;
            this.lblcmb3.Text = "Entry Type";
            this.lblcmb3.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chklst2);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.lst2);
            this.panel2.Controls.Add(this.lbllst2);
            this.panel2.Location = new System.Drawing.Point(39, 348);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 202);
            this.panel2.TabIndex = 22;
            this.panel2.Visible = false;
            // 
            // chklst2
            // 
            this.chklst2.AutoSize = true;
            this.chklst2.Checked = true;
            this.chklst2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chklst2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chklst2.ForeColor = System.Drawing.Color.Red;
            this.chklst2.Location = new System.Drawing.Point(23, 106);
            this.chklst2.Name = "chklst2";
            this.chklst2.Size = new System.Drawing.Size(93, 23);
            this.chklst2.TabIndex = 20;
            this.chklst2.Text = "Select All";
            this.chklst2.UseVisualStyleBackColor = true;
            this.chklst2.CheckedChanged += new System.EventHandler(this.chklst2_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(138, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(592, 20);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lst2
            // 
            this.lst2.AllowUserToAddRows = false;
            this.lst2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lst2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.lst2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lst2.Enabled = false;
            this.lst2.Location = new System.Drawing.Point(138, 26);
            this.lst2.Name = "lst2";
            this.lst2.ReadOnly = true;
            this.lst2.Size = new System.Drawing.Size(592, 171);
            this.lst2.TabIndex = 18;
            // 
            // lbllst2
            // 
            this.lbllst2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllst2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbllst2.Location = new System.Drawing.Point(19, 39);
            this.lbllst2.Name = "lbllst2";
            this.lbllst2.Size = new System.Drawing.Size(113, 49);
            this.lbllst2.TabIndex = 7;
            this.lbllst2.Text = "-";
            // 
            // grpdate
            // 
            this.grpdate.Controls.Add(this.chkdate);
            this.grpdate.Controls.Add(this.repdt2);
            this.grpdate.Controls.Add(this.lbldt2);
            this.grpdate.Controls.Add(this.repdt1);
            this.grpdate.Controls.Add(this.lbldt1);
            this.grpdate.Location = new System.Drawing.Point(42, 3);
            this.grpdate.Name = "grpdate";
            this.grpdate.Size = new System.Drawing.Size(725, 45);
            this.grpdate.TabIndex = 21;
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Checked = true;
            this.chkdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkdate.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chkdate.ForeColor = System.Drawing.Color.Red;
            this.chkdate.Location = new System.Drawing.Point(353, 11);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(84, 23);
            this.chkdate.TabIndex = 21;
            this.chkdate.Text = "Date All";
            this.chkdate.UseVisualStyleBackColor = true;
            // 
            // repdt2
            // 
            this.repdt2.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.repdt2.CustomFormat = "dd/MM/yyyy";
            this.repdt2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repdt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.repdt2.Location = new System.Drawing.Point(523, 11);
            this.repdt2.Name = "repdt2";
            this.repdt2.Size = new System.Drawing.Size(135, 27);
            this.repdt2.TabIndex = 2;
            // 
            // lbldt2
            // 
            this.lbldt2.AutoSize = true;
            this.lbldt2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbldt2.Location = new System.Drawing.Point(443, 12);
            this.lbldt2.Name = "lbldt2";
            this.lbldt2.Size = new System.Drawing.Size(74, 19);
            this.lbldt2.TabIndex = 4;
            this.lbldt2.Text = "To Date";
            // 
            // repdt1
            // 
            this.repdt1.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.repdt1.CustomFormat = "dd/MM/yyyy";
            this.repdt1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repdt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.repdt1.Location = new System.Drawing.Point(176, 11);
            this.repdt1.Name = "repdt1";
            this.repdt1.Size = new System.Drawing.Size(157, 27);
            this.repdt1.TabIndex = 1;
            // 
            // lbldt1
            // 
            this.lbldt1.AutoSize = true;
            this.lbldt1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbldt1.Location = new System.Drawing.Point(19, 17);
            this.lbldt1.Name = "lbldt1";
            this.lbldt1.Size = new System.Drawing.Size(94, 19);
            this.lbldt1.TabIndex = 3;
            this.lbldt1.Text = "From Date";
            // 
            // cmb2
            // 
            this.cmb2.FormattingEnabled = true;
            this.cmb2.Items.AddRange(new object[] {
            "All",
            "Social Club Re-Creation",
            "Social Club Business"});
            this.cmb2.Location = new System.Drawing.Point(218, 81);
            this.cmb2.Name = "cmb2";
            this.cmb2.Size = new System.Drawing.Size(482, 21);
            this.cmb2.TabIndex = 19;
            this.cmb2.Visible = false;
            this.cmb2.SelectedIndexChanged += new System.EventHandler(this.cmb2_SelectedIndexChanged);
            // 
            // lblcmb2
            // 
            this.lblcmb2.AutoSize = true;
            this.lblcmb2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcmb2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblcmb2.Location = new System.Drawing.Point(61, 79);
            this.lblcmb2.Name = "lblcmb2";
            this.lblcmb2.Size = new System.Drawing.Size(97, 19);
            this.lblcmb2.TabIndex = 18;
            this.lblcmb2.Text = "Entry Type";
            this.lblcmb2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chklst1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.lst1);
            this.panel1.Controls.Add(this.lbllst1);
            this.panel1.Location = new System.Drawing.Point(42, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 209);
            this.panel1.TabIndex = 17;
            this.panel1.Visible = false;
            // 
            // chklst1
            // 
            this.chklst1.AutoSize = true;
            this.chklst1.Checked = true;
            this.chklst1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chklst1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chklst1.ForeColor = System.Drawing.Color.Red;
            this.chklst1.Location = new System.Drawing.Point(21, 106);
            this.chklst1.Name = "chklst1";
            this.chklst1.Size = new System.Drawing.Size(93, 23);
            this.chklst1.TabIndex = 20;
            this.chklst1.Text = "Select All";
            this.chklst1.UseVisualStyleBackColor = true;
            this.chklst1.CheckedChanged += new System.EventHandler(this.chklst1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(138, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(592, 20);
            this.textBox1.TabIndex = 19;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // lst1
            // 
            this.lst1.AllowUserToAddRows = false;
            this.lst1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lst1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.lst1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lst1.Enabled = false;
            this.lst1.Location = new System.Drawing.Point(138, 29);
            this.lst1.Name = "lst1";
            this.lst1.ReadOnly = true;
            this.lst1.Size = new System.Drawing.Size(592, 178);
            this.lst1.TabIndex = 18;
            this.lst1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lst1_CellContentClick);
            this.lst1.DoubleClick += new System.EventHandler(this.lst1_DoubleClick);
            // 
            // lbllst1
            // 
            this.lbllst1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllst1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbllst1.Location = new System.Drawing.Point(19, 39);
            this.lbllst1.Name = "lbllst1";
            this.lbllst1.Size = new System.Drawing.Size(113, 49);
            this.lbllst1.TabIndex = 7;
            this.lbllst1.Text = "-";
            // 
            // cmdclose
            // 
            this.cmdclose.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmdclose.Location = new System.Drawing.Point(480, 556);
            this.cmdclose.Name = "cmdclose";
            this.cmdclose.Size = new System.Drawing.Size(121, 29);
            this.cmdclose.TabIndex = 16;
            this.cmdclose.Text = "&Close";
            this.cmdclose.UseVisualStyleBackColor = true;
            this.cmdclose.Click += new System.EventHandler(this.cmdclose_Click);
            // 
            // cmb1
            // 
            this.cmb1.FormattingEnabled = true;
            this.cmb1.Items.AddRange(new object[] {
            "All",
            "Social Club Re-Creation",
            "Social Club Business"});
            this.cmb1.Location = new System.Drawing.Point(218, 54);
            this.cmb1.Name = "cmb1";
            this.cmb1.Size = new System.Drawing.Size(482, 21);
            this.cmb1.TabIndex = 15;
            this.cmb1.Visible = false;
            this.cmb1.SelectedIndexChanged += new System.EventHandler(this.cmb1_SelectedIndexChanged);
            // 
            // lblcmb1
            // 
            this.lblcmb1.AutoSize = true;
            this.lblcmb1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcmb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblcmb1.Location = new System.Drawing.Point(61, 51);
            this.lblcmb1.Name = "lblcmb1";
            this.lblcmb1.Size = new System.Drawing.Size(97, 19);
            this.lblcmb1.TabIndex = 14;
            this.lblcmb1.Text = "Entry Type";
            this.lblcmb1.Visible = false;
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnView.Location = new System.Drawing.Point(268, 556);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(121, 29);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "&Load Report";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // frmreport1
            // 
            this.AcceptButton = this.btnView;
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1148, 664);
            this.Controls.Add(this.maintab1);
            this.Name = "frmreport1";
            this.Text = "Frmreport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.maintab1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst2)).EndInit();
            this.grpdate.ResumeLayout(false);
            this.grpdate.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl maintab1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lbllst1;
        private System.Windows.Forms.Label lbldt2;
        private System.Windows.Forms.Label lbldt1;
        private System.Windows.Forms.DateTimePicker repdt2;
        private System.Windows.Forms.DateTimePicker repdt1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblcmb1;
        private System.Windows.Forms.ComboBox cmb1;
        private System.Windows.Forms.Button cmdclose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView lst1;
        private System.Windows.Forms.CheckBox chklst1;
        private System.Windows.Forms.ComboBox cmb2;
        private System.Windows.Forms.Label lblcmb2;
        private System.Windows.Forms.Panel grpdate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chklst2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView lst2;
        private System.Windows.Forms.Label lbllst2;
        private System.Windows.Forms.ComboBox cmb3;
        private System.Windows.Forms.Label lblcmb3;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.CheckBox chkstock;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.CheckBox chkminstock;
        private System.Windows.Forms.TextBox txtmove;

    }
}