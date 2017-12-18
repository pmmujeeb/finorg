namespace FinOrg
{
    partial class frmReportProj
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
            this.cmbsaleagent = new System.Windows.Forms.ComboBox();
            this.cmblocaltion = new System.Windows.Forms.ComboBox();
            this.cmbtransaction = new System.Windows.Forms.ComboBox();
            this.cmbdept = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chklst2 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lst2 = new System.Windows.Forms.DataGridView();
            this.lbllst2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkinvoice = new System.Windows.Forms.CheckBox();
            this.chklst1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lst1 = new System.Windows.Forms.DataGridView();
            this.lbllst1 = new System.Windows.Forms.Label();
            this.lbldept = new System.Windows.Forms.Label();
            this.grpdate = new System.Windows.Forms.GroupBox();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdclose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbowner = new System.Windows.Forms.ComboBox();
            this.maintab1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst1)).BeginInit();
            this.grpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // maintab1
            // 
            this.maintab1.Controls.Add(this.tabPage1);
            this.maintab1.Location = new System.Drawing.Point(12, 12);
            this.maintab1.Name = "maintab1";
            this.maintab1.SelectedIndex = 0;
            this.maintab1.Size = new System.Drawing.Size(781, 576);
            this.maintab1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbsaleagent);
            this.tabPage1.Controls.Add(this.cmblocaltion);
            this.tabPage1.Controls.Add(this.cmbtransaction);
            this.tabPage1.Controls.Add(this.cmbdept);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.lbldept);
            this.tabPage1.Controls.Add(this.grpdate);
            this.tabPage1.Controls.Add(this.cmdclose);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnView);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cmbowner);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(773, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Options";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // cmbsaleagent
            // 
            this.cmbsaleagent.DisplayMember = "Unit_id";
            this.cmbsaleagent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsaleagent.FormattingEnabled = true;
            this.cmbsaleagent.ItemHeight = 13;
            this.cmbsaleagent.Items.AddRange(new object[] {
            "All",
            "Debit Only",
            "Credit Only"});
            this.cmbsaleagent.Location = new System.Drawing.Point(138, 516);
            this.cmbsaleagent.Name = "cmbsaleagent";
            this.cmbsaleagent.Size = new System.Drawing.Size(165, 21);
            this.cmbsaleagent.TabIndex = 31;
            this.cmbsaleagent.ValueMember = "Unit_id";
            // 
            // cmblocaltion
            // 
            this.cmblocaltion.DisplayMember = "Unit_id";
            this.cmblocaltion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmblocaltion.FormattingEnabled = true;
            this.cmblocaltion.ItemHeight = 13;
            this.cmblocaltion.Items.AddRange(new object[] {
            "All",
            "Debit Only",
            "Credit Only"});
            this.cmblocaltion.Location = new System.Drawing.Point(396, 492);
            this.cmblocaltion.Name = "cmblocaltion";
            this.cmblocaltion.Size = new System.Drawing.Size(165, 21);
            this.cmblocaltion.TabIndex = 31;
            this.cmblocaltion.ValueMember = "Unit_id";
            // 
            // cmbtransaction
            // 
            this.cmbtransaction.DisplayMember = "Unit_id";
            this.cmbtransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtransaction.FormattingEnabled = true;
            this.cmbtransaction.ItemHeight = 13;
            this.cmbtransaction.Items.AddRange(new object[] {
            "All",
            "Debit Only",
            "Credit Only"});
            this.cmbtransaction.Location = new System.Drawing.Point(138, 488);
            this.cmbtransaction.Name = "cmbtransaction";
            this.cmbtransaction.Size = new System.Drawing.Size(165, 21);
            this.cmbtransaction.TabIndex = 31;
            this.cmbtransaction.ValueMember = "Unit_id";
            // 
            // cmbdept
            // 
            this.cmbdept.DisplayMember = "Unit_name";
            this.cmbdept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdept.FormattingEnabled = true;
            this.cmbdept.ItemHeight = 13;
            this.cmbdept.Location = new System.Drawing.Point(530, 59);
            this.cmbdept.Name = "cmbdept";
            this.cmbdept.Size = new System.Drawing.Size(198, 21);
            this.cmbdept.TabIndex = 31;
            this.cmbdept.ValueMember = "Unit_id";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label6.Location = new System.Drawing.Point(36, 518);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 19);
            this.label6.TabIndex = 32;
            this.label6.Text = "Sales Agent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label4.Location = new System.Drawing.Point(309, 494);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = " Location";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chklst2);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.lst2);
            this.panel2.Controls.Add(this.lbllst2);
            this.panel2.Location = new System.Drawing.Point(0, 314);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 167);
            this.panel2.TabIndex = 23;
            this.panel2.Visible = false;
            // 
            // chklst2
            // 
            this.chklst2.AutoSize = true;
            this.chklst2.Checked = true;
            this.chklst2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chklst2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chklst2.ForeColor = System.Drawing.Color.Red;
            this.chklst2.Location = new System.Drawing.Point(8, 102);
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
            this.textBox2.Location = new System.Drawing.Point(138, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(592, 20);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
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
            this.lst2.Size = new System.Drawing.Size(592, 142);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label3.Location = new System.Drawing.Point(36, 490);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 19);
            this.label3.TabIndex = 32;
            this.label3.Text = "Transaction";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkinvoice);
            this.panel1.Controls.Add(this.chklst1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.lst1);
            this.panel1.Controls.Add(this.lbllst1);
            this.panel1.Location = new System.Drawing.Point(-2, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 216);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            // 
            // chkinvoice
            // 
            this.chkinvoice.AutoSize = true;
            this.chkinvoice.Location = new System.Drawing.Point(8, 147);
            this.chkinvoice.Name = "chkinvoice";
            this.chkinvoice.Size = new System.Drawing.Size(95, 17);
            this.chkinvoice.TabIndex = 21;
            this.chkinvoice.Text = "Show Invoices";
            this.chkinvoice.UseVisualStyleBackColor = true;
            // 
            // chklst1
            // 
            this.chklst1.AutoSize = true;
            this.chklst1.Checked = true;
            this.chklst1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chklst1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chklst1.ForeColor = System.Drawing.Color.Red;
            this.chklst1.Location = new System.Drawing.Point(10, 105);
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
            this.lst1.Size = new System.Drawing.Size(592, 184);
            this.lst1.TabIndex = 18;
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
            // lbldept
            // 
            this.lbldept.AutoSize = true;
            this.lbldept.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lbldept.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbldept.ImeMode = System.Windows.Forms.ImeMode.On;
            this.lbldept.Location = new System.Drawing.Point(428, 55);
            this.lbldept.Name = "lbldept";
            this.lbldept.Size = new System.Drawing.Size(92, 19);
            this.lbldept.TabIndex = 32;
            this.lbldept.Text = "Department";
            // 
            // grpdate
            // 
            this.grpdate.Controls.Add(this.chkdate);
            this.grpdate.Controls.Add(this.dt1);
            this.grpdate.Controls.Add(this.dt2);
            this.grpdate.Controls.Add(this.label1);
            this.grpdate.Controls.Add(this.label2);
            this.grpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpdate.Location = new System.Drawing.Point(37, 6);
            this.grpdate.Name = "grpdate";
            this.grpdate.Size = new System.Drawing.Size(693, 46);
            this.grpdate.TabIndex = 17;
            this.grpdate.TabStop = false;
            this.grpdate.Visible = false;
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Checked = true;
            this.chkdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkdate.Font = new System.Drawing.Font("Tahoma", 12F);
            this.chkdate.ForeColor = System.Drawing.Color.Red;
            this.chkdate.Location = new System.Drawing.Point(325, 13);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(84, 23);
            this.chkdate.TabIndex = 22;
            this.chkdate.Text = "Date All";
            this.chkdate.UseVisualStyleBackColor = true;
            // 
            // dt1
            // 
            this.dt1.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.dt1.CustomFormat = "dd/MM/yyyy";
            this.dt1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Location = new System.Drawing.Point(133, 10);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(175, 27);
            this.dt1.TabIndex = 1;
            // 
            // dt2
            // 
            this.dt2.CalendarFont = new System.Drawing.Font("Tahoma", 12F);
            this.dt2.CustomFormat = "dd/MM/yyyy";
            this.dt2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt2.Location = new System.Drawing.Point(528, 10);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(163, 27);
            this.dt2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(-3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(424, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "To Date";
            // 
            // cmdclose
            // 
            this.cmdclose.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmdclose.Location = new System.Drawing.Point(607, 518);
            this.cmdclose.Name = "cmdclose";
            this.cmdclose.Size = new System.Drawing.Size(121, 29);
            this.cmdclose.TabIndex = 16;
            this.cmdclose.Text = "&Close";
            this.cmdclose.UseVisualStyleBackColor = true;
            this.cmdclose.Click += new System.EventHandler(this.cmdclose_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(35, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Owner";
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnView.Location = new System.Drawing.Point(609, 485);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(121, 29);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "&Load Report";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(736, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbowner
            // 
            this.cmbowner.FormattingEnabled = true;
            this.cmbowner.Items.AddRange(new object[] {
            "All",
            "Social Club Re-Creation",
            "Social Club Business"});
            this.cmbowner.Location = new System.Drawing.Point(170, 57);
            this.cmbowner.Name = "cmbowner";
            this.cmbowner.Size = new System.Drawing.Size(252, 21);
            this.cmbowner.TabIndex = 15;
            this.cmbowner.SelectedIndexChanged += new System.EventHandler(this.cmbowner_SelectedIndexChanged);
            // 
            // frmReport
            // 
            this.AcceptButton = this.btnView;
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(805, 600);
            this.Controls.Add(this.maintab1);
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frmreport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.maintab1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lst1)).EndInit();
            this.grpdate.ResumeLayout(false);
            this.grpdate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl maintab1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dt2;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbowner;
        private System.Windows.Forms.Button cmdclose;
        private System.Windows.Forms.GroupBox grpdate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chklst2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView lst2;
        private System.Windows.Forms.Label lbllst2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chklst1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView lst1;
        private System.Windows.Forms.Label lbllst1;
        private System.Windows.Forms.ComboBox cmbdept;
        private System.Windows.Forms.Label lbldept;
        private System.Windows.Forms.ComboBox cmbtransaction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkinvoice;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.ComboBox cmbsaleagent;
        private System.Windows.Forms.ComboBox cmblocaltion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;

    }
}