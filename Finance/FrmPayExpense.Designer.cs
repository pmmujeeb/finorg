namespace FinOrg
{

    partial class FrmPayExpense
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPayExpense));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeimage1 = new System.Windows.Forms.ImageList(this.components);
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblacc = new System.Windows.Forms.Label();
            this.dtentry = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.txtexpense = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtinvno = new System.Windows.Forms.TextBox();
            this.cmbtrntype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgexp = new System.Windows.Forms.DataGridView();
            this.Expense = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expensecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dginvoice = new System.Windows.Forms.DataGridView();
            this.entryitem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryvalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnaddexp = new System.Windows.Forms.Button();
            this.lstexptype = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GrdLookup = new System.Windows.Forms.DataGridView();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbbranch = new System.Windows.Forms.ComboBox();
            this.NYEAR = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fraction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rec_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgexp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dginvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeimage1
            // 
            this.treeimage1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.treeimage1.ImageSize = new System.Drawing.Size(16, 16);
            this.treeimage1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.DimGray;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.toolStrip.Size = new System.Drawing.Size(959, 52);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 28;
            this.toolStrip.Text = "ToolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.BackColor = System.Drawing.Color.DimGray;
            this.newToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.newToolStripButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.newToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.DarkRed;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(81, 49);
            this.newToolStripButton.Text = "&New Item";
            this.newToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.newToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.openToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(52, 49);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.openToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openToolStripButton.Visible = false;
            // 
            // SearchToolStripButton
            // 
            this.SearchToolStripButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.SearchToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchToolStripButton.Image")));
            this.SearchToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SearchToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.SearchToolStripButton.Name = "SearchToolStripButton";
            this.SearchToolStripButton.Size = new System.Drawing.Size(61, 49);
            this.SearchToolStripButton.Text = "S&earch";
            this.SearchToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SearchToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(47, 49);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.saveToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.ForeColor = System.Drawing.Color.Chartreuse;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(47, 49);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.printToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolRefund
            // 
            this.toolRefund.ForeColor = System.Drawing.Color.Chartreuse;
            this.toolRefund.Image = ((System.Drawing.Image)(resources.GetObject("toolRefund.Image")));
            this.toolRefund.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolRefund.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefund.Name = "toolRefund";
            this.toolRefund.Size = new System.Drawing.Size(64, 49);
            this.toolRefund.Text = "&Refund";
            this.toolRefund.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolRefund.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRefund.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // tooldelete
            // 
            this.tooldelete.ForeColor = System.Drawing.Color.Chartreuse;
            this.tooldelete.Image = ((System.Drawing.Image)(resources.GetObject("tooldelete.Image")));
            this.tooldelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tooldelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooldelete.Name = "tooldelete";
            this.tooldelete.Size = new System.Drawing.Size(58, 49);
            this.tooldelete.Text = "&Delete";
            this.tooldelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tooldelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tooldelete.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.Chartreuse;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 49);
            this.toolStripButton1.Text = "              ";
            // 
            // toolclose
            // 
            this.toolclose.ForeColor = System.Drawing.Color.Chartreuse;
            this.toolclose.Image = ((System.Drawing.Image)(resources.GetObject("toolclose.Image")));
            this.toolclose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolclose.Name = "toolclose";
            this.toolclose.Size = new System.Drawing.Size(38, 49);
            this.toolclose.Text = "E&xit";
            this.toolclose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolclose.Click += new System.EventHandler(this.toolclose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 583);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(959, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblacc
            // 
            this.lblacc.BackColor = System.Drawing.Color.Transparent;
            this.lblacc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblacc.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblacc.ForeColor = System.Drawing.Color.Purple;
            this.lblacc.Location = new System.Drawing.Point(390, 53);
            this.lblacc.Name = "lblacc";
            this.lblacc.Size = new System.Drawing.Size(306, 28);
            this.lblacc.TabIndex = 32;
            this.lblacc.Text = "Purchase Expense";
            this.lblacc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtentry
            // 
            this.dtentry.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtentry.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtentry.Location = new System.Drawing.Point(668, 102);
            this.dtentry.Name = "dtentry";
            this.dtentry.Size = new System.Drawing.Size(144, 24);
            this.dtentry.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label11.Location = new System.Drawing.Point(221, 533);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 19);
            this.label11.TabIndex = 36;
            this.label11.Text = "Total Expense";
            // 
            // txtexpense
            // 
            this.txtexpense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtexpense.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtexpense.Location = new System.Drawing.Point(378, 526);
            this.txtexpense.Name = "txtexpense";
            this.txtexpense.ReadOnly = true;
            this.txtexpense.Size = new System.Drawing.Size(100, 26);
            this.txtexpense.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label7.Location = new System.Drawing.Point(84, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 19);
            this.label7.TabIndex = 39;
            this.label7.Text = "Year-Invoice No.";
            // 
            // txtinvno
            // 
            this.txtinvno.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtinvno.Location = new System.Drawing.Point(306, 201);
            this.txtinvno.Name = "txtinvno";
            this.txtinvno.Size = new System.Drawing.Size(205, 26);
            this.txtinvno.TabIndex = 1;
            this.txtinvno.Tag = "";
            this.txtinvno.TextChanged += new System.EventHandler(this.txtinvno_TextChanged);
            this.txtinvno.DoubleClick += new System.EventHandler(this.txtinvno_DoubleClick);
            this.txtinvno.Enter += new System.EventHandler(this.txtinvno_Enter);
            this.txtinvno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtinvno_KeyDown);
            this.txtinvno.Validated += new System.EventHandler(this.txtinvno_Validated);
            // 
            // cmbtrntype
            // 
            this.cmbtrntype.DisplayMember = "Unit_name";
            this.cmbtrntype.FormattingEnabled = true;
            this.cmbtrntype.Location = new System.Drawing.Point(225, 167);
            this.cmbtrntype.Name = "cmbtrntype";
            this.cmbtrntype.Size = new System.Drawing.Size(286, 21);
            this.cmbtrntype.TabIndex = 0;
            this.cmbtrntype.ValueMember = "Unit_id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label5.Location = new System.Drawing.Point(84, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 19);
            this.label5.TabIndex = 41;
            this.label5.Text = "Invoice Type";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dgexp
            // 
            this.dgexp.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            this.dgexp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgexp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgexp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgexp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgexp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Expense,
            this.expamount,
            this.expensecode});
            this.dgexp.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgexp.Location = new System.Drawing.Point(111, 389);
            this.dgexp.Name = "dgexp";
            this.dgexp.RowHeadersVisible = false;
            this.dgexp.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            this.dgexp.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.dgexp.RowTemplate.DividerHeight = 1;
            this.dgexp.RowTemplate.Height = 30;
            this.dgexp.Size = new System.Drawing.Size(400, 135);
            this.dgexp.TabIndex = 5;
            this.dgexp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgexp_CellContentClick);
            this.dgexp.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgexp_CellEndEdit);
            this.dgexp.Enter += new System.EventHandler(this.dgexp_Enter);
            // 
            // Expense
            // 
            this.Expense.HeaderText = "Expense Type";
            this.Expense.Name = "Expense";
            this.Expense.Width = 270;
            // 
            // expamount
            // 
            this.expamount.HeaderText = "Amount";
            this.expamount.Name = "expamount";
            // 
            // expensecode
            // 
            this.expensecode.HeaderText = "code";
            this.expensecode.Name = "expensecode";
            this.expensecode.Visible = false;
            // 
            // dginvoice
            // 
            this.dginvoice.AllowUserToAddRows = false;
            this.dginvoice.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dginvoice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dginvoice.BackgroundColor = System.Drawing.Color.White;
            this.dginvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dginvoice.ColumnHeadersVisible = false;
            this.dginvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entryitem,
            this.entryvalue});
            this.dginvoice.Location = new System.Drawing.Point(540, 132);
            this.dginvoice.Name = "dginvoice";
            this.dginvoice.ReadOnly = true;
            this.dginvoice.Size = new System.Drawing.Size(410, 218);
            this.dginvoice.TabIndex = 10;
            this.dginvoice.TabStop = false;
            // 
            // entryitem
            // 
            this.entryitem.HeaderText = "Entry Item";
            this.entryitem.Name = "entryitem";
            this.entryitem.ReadOnly = true;
            this.entryitem.Width = 150;
            // 
            // entryvalue
            // 
            this.entryvalue.HeaderText = "Entry Amount";
            this.entryvalue.Name = "entryvalue";
            this.entryvalue.ReadOnly = true;
            this.entryvalue.Width = 200;
            // 
            // btnaddexp
            // 
            this.btnaddexp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnaddexp.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnaddexp.ForeColor = System.Drawing.Color.Navy;
            this.btnaddexp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnaddexp.ImageIndex = 4;
            this.btnaddexp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnaddexp.Location = new System.Drawing.Point(484, 272);
            this.btnaddexp.Name = "btnaddexp";
            this.btnaddexp.Size = new System.Drawing.Size(27, 25);
            this.btnaddexp.TabIndex = 4;
            this.btnaddexp.TabStop = false;
            this.btnaddexp.Text = "+";
            this.btnaddexp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnaddexp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnaddexp.UseVisualStyleBackColor = true;
            this.btnaddexp.Click += new System.EventHandler(this.btnaddexp_Click);
            // 
            // lstexptype
            // 
            this.lstexptype.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lstexptype.FormattingEnabled = true;
            this.lstexptype.Location = new System.Drawing.Point(225, 269);
            this.lstexptype.Name = "lstexptype";
            this.lstexptype.Size = new System.Drawing.Size(253, 104);
            this.lstexptype.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label1.Location = new System.Drawing.Point(84, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 39;
            this.label1.Text = "Expense Type";
            // 
            // txtrefno
            // 
            this.txtrefno.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtrefno.Location = new System.Drawing.Point(225, 233);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(286, 26);
            this.txtrefno.TabIndex = 2;
            this.txtrefno.Tag = "";
            this.txtrefno.TextChanged += new System.EventHandler(this.txtrefno_TextChanged);
            this.txtrefno.DoubleClick += new System.EventHandler(this.txtrefno_DoubleClick);
            this.txtrefno.Enter += new System.EventHandler(this.txtrefno_Enter);
            this.txtrefno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtrefno_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label2.Location = new System.Drawing.Point(84, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 19);
            this.label2.TabIndex = 39;
            this.label2.Text = "Reference No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label3.Location = new System.Drawing.Point(589, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.TabIndex = 41;
            this.label3.Text = "Date ";
            this.label3.Click += new System.EventHandler(this.label5_Click);
            // 
            // GrdLookup
            // 
            this.GrdLookup.AllowUserToAddRows = false;
            this.GrdLookup.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GrdLookup.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GrdLookup.BackgroundColor = System.Drawing.Color.White;
            this.GrdLookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdLookup.Location = new System.Drawing.Point(710, 198);
            this.GrdLookup.Name = "GrdLookup";
            this.GrdLookup.ReadOnly = true;
            this.GrdLookup.Size = new System.Drawing.Size(719, 326);
            this.GrdLookup.TabIndex = 42;
            this.GrdLookup.Visible = false;
            this.GrdLookup.DoubleClick += new System.EventHandler(this.GrdLookup_DoubleClick);
            // 
            // dgv1
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.barcode,
            this.Description,
            this.qty,
            this.price,
            this.disc,
            this.fraction,
            this.unit,
            this.cost,
            this.exp,
            this.total,
            this.itemcode,
            this.rec_no,
            this.rownum});
            this.dgv1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgv1.Location = new System.Drawing.Point(799, 226);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            this.dgv1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.dgv1.RowTemplate.DividerHeight = 1;
            this.dgv1.RowTemplate.Height = 30;
            this.dgv1.Size = new System.Drawing.Size(219, 215);
            this.dgv1.TabIndex = 43;
            this.dgv1.TabStop = false;
            this.dgv1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label4.Location = new System.Drawing.Point(84, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 19);
            this.label4.TabIndex = 41;
            this.label4.Text = "Branch";
            this.label4.Click += new System.EventHandler(this.label5_Click);
            // 
            // cmbbranch
            // 
            this.cmbbranch.DisplayMember = "Unit_name";
            this.cmbbranch.FormattingEnabled = true;
            this.cmbbranch.Location = new System.Drawing.Point(225, 132);
            this.cmbbranch.Name = "cmbbranch";
            this.cmbbranch.Size = new System.Drawing.Size(286, 21);
            this.cmbbranch.TabIndex = 0;
            this.cmbbranch.ValueMember = "Unit_id";
            // 
            // NYEAR
            // 
            this.NYEAR.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.NYEAR.Location = new System.Drawing.Point(225, 201);
            this.NYEAR.Name = "NYEAR";
            this.NYEAR.Size = new System.Drawing.Size(84, 26);
            this.NYEAR.TabIndex = 1;
            this.NYEAR.Tag = "";
            this.NYEAR.Enter += new System.EventHandler(this.txtinvno_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label6.Location = new System.Drawing.Point(536, 364);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 19);
            this.label6.TabIndex = 46;
            this.label6.Text = "Remarks";
            // 
            // txtremarks
            // 
            this.txtremarks.BackColor = System.Drawing.Color.White;
            this.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtremarks.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtremarks.Location = new System.Drawing.Point(540, 389);
            this.txtremarks.Multiline = true;
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(407, 52);
            this.txtremarks.TabIndex = 6;
            // 
            // barcode
            // 
            this.barcode.HeaderText = "barcode";
            this.barcode.Name = "barcode";
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // qty
            // 
            this.qty.HeaderText = "qty";
            this.qty.Name = "qty";
            // 
            // price
            // 
            this.price.HeaderText = "price";
            this.price.Name = "price";
            // 
            // disc
            // 
            this.disc.HeaderText = "disc";
            this.disc.Name = "disc";
            // 
            // fraction
            // 
            this.fraction.HeaderText = "fraction";
            this.fraction.Name = "fraction";
            // 
            // unit
            // 
            this.unit.HeaderText = "unit";
            this.unit.Name = "unit";
            // 
            // cost
            // 
            this.cost.HeaderText = "cost";
            this.cost.Name = "cost";
            // 
            // exp
            // 
            this.exp.HeaderText = "expense";
            this.exp.Name = "exp";
            // 
            // total
            // 
            this.total.HeaderText = "total";
            this.total.Name = "total";
            // 
            // itemcode
            // 
            this.itemcode.HeaderText = "itemcode";
            this.itemcode.Name = "itemcode";
            // 
            // rec_no
            // 
            this.rec_no.HeaderText = "rec_no";
            this.rec_no.Name = "rec_no";
            this.rec_no.Visible = false;
            // 
            // rownum
            // 
            this.rownum.HeaderText = "rownum";
            this.rownum.Name = "rownum";
            this.rownum.Visible = false;
            // 
            // FrmPayExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(959, 605);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.GrdLookup);
            this.Controls.Add(this.lstexptype);
            this.Controls.Add(this.btnaddexp);
            this.Controls.Add(this.dginvoice);
            this.Controls.Add(this.dgexp);
            this.Controls.Add(this.cmbbranch);
            this.Controls.Add(this.cmbtrntype);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtrefno);
            this.Controls.Add(this.NYEAR);
            this.Controls.Add(this.txtinvno);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtexpense);
            this.Controls.Add(this.dtentry);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblacc);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmPayExpense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAccMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAccMaster_Load);
            this.ResizeBegin += new System.EventHandler(this.FrmAccMaster_ResizeBegin);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAccMaster_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgexp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dginvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ImageList treeimage1;
        private System.Windows.Forms.Label lblacc;
       // private ControlLib.dbTreeViewCtrl treeacc;
        private System.Windows.Forms.DateTimePicker dtentry;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtexpense;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtinvno;
        private System.Windows.Forms.ComboBox cmbtrntype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgexp;
        private System.Windows.Forms.DataGridView dginvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryitem;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryvalue;
        private System.Windows.Forms.Button btnaddexp;
        private System.Windows.Forms.CheckedListBox lstexptype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expense;
        private System.Windows.Forms.DataGridViewTextBoxColumn expamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn expensecode;
        private System.Windows.Forms.DataGridView GrdLookup;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbbranch;
        private System.Windows.Forms.TextBox NYEAR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn disc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fraction;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn exp;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn rec_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
    }
}