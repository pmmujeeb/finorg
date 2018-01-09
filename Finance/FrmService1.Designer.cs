namespace FinOrg
{
    partial class FrmService1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmService));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtissueNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbsite = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttrn = new System.Windows.Forms.TextBox();
            this.txttrn_type = new System.Windows.Forms.TextBox();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.cmbproject = new System.Windows.Forms.ComboBox();
            this.GrdLookup = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.txttotal = new System.Windows.Forms.TextBox();
            
            this.txtpriv = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLookup)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label5.Location = new System.Drawing.Point(453, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label7.Location = new System.Drawing.Point(37, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 19);
            this.label7.TabIndex = 9;
            this.label7.Text = "Project";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label6.Location = new System.Drawing.Point(453, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "Reference No.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label8.Location = new System.Drawing.Point(453, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 19);
            this.label8.TabIndex = 9;
            this.label8.Text = "Transaction #";
            // 
            // TxtissueNo
            // 
            this.TxtissueNo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.TxtissueNo.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.TxtissueNo.Location = new System.Drawing.Point(133, 15);
            this.TxtissueNo.Name = "TxtissueNo";
            this.TxtissueNo.Size = new System.Drawing.Size(173, 26);
            this.TxtissueNo.TabIndex = 1;
            this.TxtissueNo.TextChanged += new System.EventHandler(this.TxtmrnNo_TextChanged);
            this.TxtissueNo.DoubleClick += new System.EventHandler(this.TxtmrnNo_DoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 19);
            this.label9.TabIndex = 11;
            this.label9.Text = "Service No.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbsite);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txttrn);
            this.groupBox1.Controls.Add(this.txttrn_type);
            this.groupBox1.Controls.Add(this.txtrefno);
            this.groupBox1.Controls.Add(this.dt1);
            this.groupBox1.Controls.Add(this.cmbproject);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TxtissueNo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(6, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1114, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // cmbsite
            // 
            this.cmbsite.DisplayMember = "Unit_name";
            this.cmbsite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsite.FormattingEnabled = true;
            this.cmbsite.Location = new System.Drawing.Point(136, 71);
            this.cmbsite.Name = "cmbsite";
            this.cmbsite.Size = new System.Drawing.Size(311, 21);
            this.cmbsite.TabIndex = 25;
            this.cmbsite.ValueMember = "Unit_id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label3.Location = new System.Drawing.Point(60, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 19);
            this.label3.TabIndex = 26;
            this.label3.Text = "Site";
            // 
            // txttrn
            // 
            this.txttrn.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txttrn.Location = new System.Drawing.Point(568, 67);
            this.txttrn.Name = "txttrn";
            this.txttrn.Size = new System.Drawing.Size(172, 26);
            this.txttrn.TabIndex = 17;
            // 
            // txttrn_type
            // 
            this.txttrn_type.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txttrn_type.Location = new System.Drawing.Point(765, 49);
            this.txttrn_type.Name = "txttrn_type";
            this.txttrn_type.Size = new System.Drawing.Size(173, 26);
            this.txttrn_type.TabIndex = 16;
            this.txttrn_type.Visible = false;
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtrefno.Location = new System.Drawing.Point(567, 41);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(173, 26);
            this.txtrefno.TabIndex = 4;
            // 
            // dt1
            // 
            this.dt1.CalendarFont = new System.Drawing.Font("Tahoma", 10F);
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Location = new System.Drawing.Point(568, 15);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(174, 20);
            this.dt1.TabIndex = 14;
            // 
            // cmbproject
            // 
            this.cmbproject.DisplayMember = "Unit_name";
            this.cmbproject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbproject.FormattingEnabled = true;
            this.cmbproject.Location = new System.Drawing.Point(136, 45);
            this.cmbproject.Name = "cmbproject";
            this.cmbproject.Size = new System.Drawing.Size(311, 21);
            this.cmbproject.TabIndex = 3;
            this.cmbproject.ValueMember = "Unit_id";
            this.cmbproject.SelectedIndexChanged += new System.EventHandler(this.cmbproject_SelectedIndexChanged);
            // 
            // GrdLookup
            // 
            this.GrdLookup.AllowUserToAddRows = false;
            this.GrdLookup.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdLookup.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GrdLookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdLookup.Location = new System.Drawing.Point(956, 81);
            this.GrdLookup.Name = "GrdLookup";
            this.GrdLookup.ReadOnly = true;
            this.GrdLookup.Size = new System.Drawing.Size(685, 352);
            this.GrdLookup.TabIndex = 17;
            this.GrdLookup.Visible = false;
            this.GrdLookup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdLookup_CellContentClick);
            this.GrdLookup.DoubleClick += new System.EventHandler(this.GrdLookup_DoubleClick);
            this.GrdLookup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdLookup_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.textBox1.Location = new System.Drawing.Point(12, 159);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(441, 26);
            this.textBox1.TabIndex = 26;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F);
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
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1276, 44);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 27;
            this.toolStrip.Text = "ToolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.BackColor = System.Drawing.Color.White;
            this.newToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.DarkRed;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(81, 41);
            this.newToolStripButton.Text = "&New Item";
            this.newToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(52, 41);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openToolStripButton.Visible = false;
            // 
            // SearchToolStripButton
            // 
            this.SearchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchToolStripButton.Image")));
            this.SearchToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.SearchToolStripButton.Name = "SearchToolStripButton";
            this.SearchToolStripButton.Size = new System.Drawing.Size(61, 41);
            this.SearchToolStripButton.Text = "S&earch";
            this.SearchToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SearchToolStripButton.Click += new System.EventHandler(this.SearchToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 44);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(47, 41);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(47, 41);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // toolRefund
            // 
            this.toolRefund.Image = ((System.Drawing.Image)(resources.GetObject("toolRefund.Image")));
            this.toolRefund.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefund.Name = "toolRefund";
            this.toolRefund.Size = new System.Drawing.Size(64, 41);
            this.toolRefund.Text = "&Refund";
            this.toolRefund.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRefund.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // tooldelete
            // 
            this.tooldelete.Image = ((System.Drawing.Image)(resources.GetObject("tooldelete.Image")));
            this.tooldelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooldelete.Name = "tooldelete";
            this.tooldelete.Size = new System.Drawing.Size(58, 41);
            this.tooldelete.Text = "&Delete";
            this.tooldelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tooldelete.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 41);
            this.toolStripButton1.Text = "              ";
            // 
            // toolclose
            // 
            this.toolclose.Image = ((System.Drawing.Image)(resources.GetObject("toolclose.Image")));
            this.toolclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolclose.Name = "toolclose";
            this.toolclose.Size = new System.Drawing.Size(38, 41);
            this.toolclose.Text = "E&xit";
            this.toolclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolclose.Click += new System.EventHandler(this.toolclose_Click);
            // 
            // dgv1
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgv1.Location = new System.Drawing.Point(12, 185);
            this.dgv1.Name = "dgv1";
            this.dgv1.Size = new System.Drawing.Size(1328, 331);
            this.dgv1.TabIndex = 5;
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            this.dgv1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellEnter);
            this.dgv1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellLeave);
            this.dgv1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellValidated);
            this.dgv1.DoubleClick += new System.EventHandler(this.dgv1_DoubleClick);
            this.dgv1.Enter += new System.EventHandler(this.dgv1_Enter);
            this.dgv1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv1_KeyDown);
            this.dgv1.Leave += new System.EventHandler(this.dgv1_Leave);
            // 
            // txttotal
            // 
            this.txttotal.Location = new System.Drawing.Point(1149, 522);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(127, 20);
            this.txttotal.TabIndex = 30;
            // 
            // txtpriv
            // 
            this.txtpriv.Location = new System.Drawing.Point(593, 306);
            this.txtpriv.Name = "txtpriv";
            this.txtpriv.Size = new System.Drawing.Size(91, 20);
            this.txtpriv.TabIndex = 31;
            this.txtpriv.Visible = false;
            // 
            // FrmService
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1276, 633);
            this.Controls.Add(this.txtpriv);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.GrdLookup);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv1);
            this.KeyPreview = true;
            this.Name = "FrmService";
            this.Text = "FrmMRNEntry";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmMRNEntry_Activated);
            this.Load += new System.EventHandler(this.FrmMRNEntry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMRNEntry_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLookup)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtissueNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GrdLookup;
        private System.Windows.Forms.ComboBox cmbproject;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txttrn_type;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton SearchToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton toolRefund;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tooldelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolclose;
        private System.Windows.Forms.TextBox txttrn;
        private System.Windows.Forms.DataGridView dgv1;
        
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.ComboBox cmbsite;
        private System.Windows.Forms.Label label3;
       public System.Windows.Forms.TextBox txtpriv;
    }
}