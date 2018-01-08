namespace FinOrg.Training_Module
{
	partial class TrainingSubscription
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
			this.payment_period_cb = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.balance_tb = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.advance_tb = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.grandtotal_tb = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.discount_tb = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.total_tb = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.items = new System.Windows.Forms.DataGridView();
			this.service_CBColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.period_CBColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.priority_CBColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.rate_DGColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.start_date = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.trans_date = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.customer_tb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStrip_addButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_saveButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_editButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_cancelButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_deleteButton = new System.Windows.Forms.ToolStripButton();
			this.search_tb = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.code_tb = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.subscription_datagrid = new System.Windows.Forms.DataGridView();
			this.customer_datagrid = new System.Windows.Forms.DataGridView();
			this.customer_codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.customer_nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.customer_anameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.customer_name = new System.Windows.Forms.Label();
			this.servicesearch_codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.servicesearch_customerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.servicesearch_grandtotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.items)).BeginInit();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.subscription_datagrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.customer_datagrid)).BeginInit();
			this.SuspendLayout();
			// 
			// payment_period_cb
			// 
			this.payment_period_cb.FormattingEnabled = true;
			this.payment_period_cb.Location = new System.Drawing.Point(488, 561);
			this.payment_period_cb.Name = "payment_period_cb";
			this.payment_period_cb.Size = new System.Drawing.Size(158, 21);
			this.payment_period_cb.TabIndex = 21;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(356, 564);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 13);
			this.label10.TabIndex = 20;
			this.label10.Text = "Payment Period";
			// 
			// balance_tb
			// 
			this.balance_tb.Location = new System.Drawing.Point(488, 535);
			this.balance_tb.Name = "balance_tb";
			this.balance_tb.ReadOnly = true;
			this.balance_tb.Size = new System.Drawing.Size(158, 20);
			this.balance_tb.TabIndex = 19;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(357, 538);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(46, 13);
			this.label9.TabIndex = 18;
			this.label9.Text = "Balance";
			// 
			// advance_tb
			// 
			this.advance_tb.Location = new System.Drawing.Point(488, 509);
			this.advance_tb.Name = "advance_tb";
			this.advance_tb.Size = new System.Drawing.Size(158, 20);
			this.advance_tb.TabIndex = 17;
			this.advance_tb.Leave += new System.EventHandler(this.discount_advance_tb_Leave);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(357, 512);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(50, 13);
			this.label8.TabIndex = 16;
			this.label8.Text = "Advance";
			// 
			// grandtotal_tb
			// 
			this.grandtotal_tb.Location = new System.Drawing.Point(488, 483);
			this.grandtotal_tb.Name = "grandtotal_tb";
			this.grandtotal_tb.ReadOnly = true;
			this.grandtotal_tb.Size = new System.Drawing.Size(158, 20);
			this.grandtotal_tb.TabIndex = 15;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(357, 486);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Grand Total";
			// 
			// discount_tb
			// 
			this.discount_tb.Location = new System.Drawing.Point(488, 457);
			this.discount_tb.Name = "discount_tb";
			this.discount_tb.Size = new System.Drawing.Size(158, 20);
			this.discount_tb.TabIndex = 13;
			this.discount_tb.Leave += new System.EventHandler(this.discount_advance_tb_Leave);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(357, 460);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Discount";
			// 
			// total_tb
			// 
			this.total_tb.Location = new System.Drawing.Point(488, 431);
			this.total_tb.Name = "total_tb";
			this.total_tb.ReadOnly = true;
			this.total_tb.Size = new System.Drawing.Size(158, 20);
			this.total_tb.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(357, 434);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Total";
			// 
			// items
			// 
			this.items.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.items.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.items.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.service_CBColumn,
            this.period_CBColumn,
            this.priority_CBColumn,
            this.rate_DGColumn});
			this.items.Location = new System.Drawing.Point(15, 183);
			this.items.Name = "items";
			this.items.Size = new System.Drawing.Size(631, 232);
			this.items.TabIndex = 9;
			this.items.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.items_CellEndEdit);
			// 
			// service_CBColumn
			// 
			this.service_CBColumn.DataPropertyName = "TR_SUBITEM_SERVICE_CODE";
			this.service_CBColumn.HeaderText = "Service";
			this.service_CBColumn.Name = "service_CBColumn";
			// 
			// period_CBColumn
			// 
			this.period_CBColumn.DataPropertyName = "TR_SUBITEM_PERIOD_CODE";
			this.period_CBColumn.HeaderText = "Period";
			this.period_CBColumn.Name = "period_CBColumn";
			// 
			// priority_CBColumn
			// 
			this.priority_CBColumn.DataPropertyName = "TR_SUBITEM_PRIORITY_CODE";
			this.priority_CBColumn.HeaderText = "Priority";
			this.priority_CBColumn.Name = "priority_CBColumn";
			// 
			// rate_DGColumn
			// 
			this.rate_DGColumn.DataPropertyName = "TR_SUBITEM_RATE";
			this.rate_DGColumn.HeaderText = "Rate";
			this.rate_DGColumn.Name = "rate_DGColumn";
			// 
			// start_date
			// 
			this.start_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.start_date.Location = new System.Drawing.Point(447, 134);
			this.start_date.Name = "start_date";
			this.start_date.Size = new System.Drawing.Size(199, 20);
			this.start_date.TabIndex = 8;
			this.start_date.Value = new System.DateTime(2018, 1, 12, 0, 0, 0, 0);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(352, 137);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Start Date";
			// 
			// trans_date
			// 
			this.trans_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.trans_date.Location = new System.Drawing.Point(447, 108);
			this.trans_date.Name = "trans_date";
			this.trans_date.Size = new System.Drawing.Size(200, 20);
			this.trans_date.TabIndex = 6;
			this.trans_date.Value = new System.DateTime(2018, 1, 12, 0, 0, 0, 0);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(352, 111);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Date";
			// 
			// customer_tb
			// 
			this.customer_tb.Location = new System.Drawing.Point(106, 134);
			this.customer_tb.Name = "customer_tb";
			this.customer_tb.Size = new System.Drawing.Size(79, 20);
			this.customer_tb.TabIndex = 4;
			this.customer_tb.TextChanged += new System.EventHandler(this.customer_tb_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 137);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Customer";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_addButton,
            this.toolStrip_saveButton,
            this.toolStrip_editButton,
            this.toolStrip_cancelButton,
            this.toolStrip_deleteButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(680, 38);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStrip_addButton
			// 
			this.toolStrip_addButton.Image = global::FinOrg.Properties.Resources.ADD;
			this.toolStrip_addButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolStrip_addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_addButton.Name = "toolStrip_addButton";
			this.toolStrip_addButton.Size = new System.Drawing.Size(33, 35);
			this.toolStrip_addButton.Text = "Add";
			this.toolStrip_addButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_addButton.Click += new System.EventHandler(this.toolStrip_addButton_Click);
			// 
			// toolStrip_saveButton
			// 
			this.toolStrip_saveButton.Image = global::FinOrg.Properties.Resources.SAVE;
			this.toolStrip_saveButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolStrip_saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_saveButton.Name = "toolStrip_saveButton";
			this.toolStrip_saveButton.Size = new System.Drawing.Size(35, 35);
			this.toolStrip_saveButton.Text = "Save";
			this.toolStrip_saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_saveButton.Click += new System.EventHandler(this.toolStrip_saveButton_Click);
			// 
			// toolStrip_editButton
			// 
			this.toolStrip_editButton.Image = global::FinOrg.Properties.Resources.WZEDIT;
			this.toolStrip_editButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolStrip_editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_editButton.Name = "toolStrip_editButton";
			this.toolStrip_editButton.Size = new System.Drawing.Size(31, 35);
			this.toolStrip_editButton.Text = "Edit";
			this.toolStrip_editButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_editButton.Click += new System.EventHandler(this.toolStrip_editButton_Click);
			// 
			// toolStrip_cancelButton
			// 
			this.toolStrip_cancelButton.Image = global::FinOrg.Properties.Resources.REVERT;
			this.toolStrip_cancelButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolStrip_cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_cancelButton.Name = "toolStrip_cancelButton";
			this.toolStrip_cancelButton.Size = new System.Drawing.Size(47, 35);
			this.toolStrip_cancelButton.Text = "Cancel";
			this.toolStrip_cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_cancelButton.Click += new System.EventHandler(this.toolStrip_cancelButton_Click);
			// 
			// toolStrip_deleteButton
			// 
			this.toolStrip_deleteButton.Image = global::FinOrg.Properties.Resources.WZDELETE;
			this.toolStrip_deleteButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolStrip_deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_deleteButton.Name = "toolStrip_deleteButton";
			this.toolStrip_deleteButton.Size = new System.Drawing.Size(44, 35);
			this.toolStrip_deleteButton.Text = "Delete";
			this.toolStrip_deleteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_deleteButton.Click += new System.EventHandler(this.toolStrip_deleteButton_Click);
			// 
			// search_tb
			// 
			this.search_tb.Location = new System.Drawing.Point(106, 68);
			this.search_tb.Name = "search_tb";
			this.search_tb.Size = new System.Drawing.Size(199, 20);
			this.search_tb.TabIndex = 1;
			this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 71);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Search";
			// 
			// code_tb
			// 
			this.code_tb.Location = new System.Drawing.Point(106, 108);
			this.code_tb.Name = "code_tb";
			this.code_tb.Size = new System.Drawing.Size(199, 20);
			this.code_tb.TabIndex = 23;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(12, 111);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(32, 13);
			this.label11.TabIndex = 22;
			this.label11.Text = "Code";
			// 
			// subscription_datagrid
			// 
			this.subscription_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.subscription_datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.servicesearch_codeColumn,
            this.servicesearch_customerNameColumn,
            this.servicesearch_grandtotalColumn});
			this.subscription_datagrid.Location = new System.Drawing.Point(-61, 512);
			this.subscription_datagrid.Name = "subscription_datagrid";
			this.subscription_datagrid.ReadOnly = true;
			this.subscription_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.subscription_datagrid.Size = new System.Drawing.Size(398, 206);
			this.subscription_datagrid.TabIndex = 24;
			this.subscription_datagrid.DoubleClick += new System.EventHandler(this.subscription_datagrid_DoubleClick);
			// 
			// customer_datagrid
			// 
			this.customer_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.customer_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.customer_datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customer_codeColumn,
            this.customer_nameColumn,
            this.customer_anameColumn});
			this.customer_datagrid.Location = new System.Drawing.Point(510, -86);
			this.customer_datagrid.Name = "customer_datagrid";
			this.customer_datagrid.ReadOnly = true;
			this.customer_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.customer_datagrid.Size = new System.Drawing.Size(457, 174);
			this.customer_datagrid.TabIndex = 25;
			this.customer_datagrid.DoubleClick += new System.EventHandler(this.customer_datagrid_DoubleClick);
			// 
			// customer_codeColumn
			// 
			this.customer_codeColumn.DataPropertyName = "ACC_NO";
			this.customer_codeColumn.HeaderText = "Customer Code";
			this.customer_codeColumn.Name = "customer_codeColumn";
			this.customer_codeColumn.ReadOnly = true;
			// 
			// customer_nameColumn
			// 
			this.customer_nameColumn.DataPropertyName = "ACC_NAME";
			this.customer_nameColumn.HeaderText = "Name";
			this.customer_nameColumn.Name = "customer_nameColumn";
			this.customer_nameColumn.ReadOnly = true;
			// 
			// customer_anameColumn
			// 
			this.customer_anameColumn.DataPropertyName = "ACC_ANAME";
			this.customer_anameColumn.HeaderText = "Arabic Name";
			this.customer_anameColumn.Name = "customer_anameColumn";
			this.customer_anameColumn.ReadOnly = true;
			// 
			// customer_name
			// 
			this.customer_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.customer_name.CausesValidation = false;
			this.customer_name.Location = new System.Drawing.Point(103, 157);
			this.customer_name.Name = "customer_name";
			this.customer_name.Size = new System.Drawing.Size(544, 23);
			this.customer_name.TabIndex = 26;
			this.customer_name.Text = "Customer Name";
			this.customer_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// servicesearch_codeColumn
			// 
			this.servicesearch_codeColumn.DataPropertyName = "TR_SUBSCRIPTION_CODE";
			this.servicesearch_codeColumn.HeaderText = "Code";
			this.servicesearch_codeColumn.Name = "servicesearch_codeColumn";
			this.servicesearch_codeColumn.ReadOnly = true;
			// 
			// servicesearch_customerNameColumn
			// 
			this.servicesearch_customerNameColumn.DataPropertyName = "CUSTOMER_NAME";
			this.servicesearch_customerNameColumn.HeaderText = "Customer Name";
			this.servicesearch_customerNameColumn.Name = "servicesearch_customerNameColumn";
			this.servicesearch_customerNameColumn.ReadOnly = true;
			// 
			// servicesearch_grandtotalColumn
			// 
			this.servicesearch_grandtotalColumn.DataPropertyName = "GRAND_TOTAL";
			this.servicesearch_grandtotalColumn.HeaderText = "Grand Total";
			this.servicesearch_grandtotalColumn.Name = "servicesearch_grandtotalColumn";
			this.servicesearch_grandtotalColumn.ReadOnly = true;
			// 
			// TrainingSubscription
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 625);
			this.Controls.Add(this.customer_datagrid);
			this.Controls.Add(this.subscription_datagrid);
			this.Controls.Add(this.code_tb);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.payment_period_cb);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.balance_tb);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.advance_tb);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.grandtotal_tb);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.discount_tb);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.total_tb);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.items);
			this.Controls.Add(this.start_date);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.trans_date);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.customer_tb);
			this.Controls.Add(this.customer_name);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.search_tb);
			this.Controls.Add(this.label1);
			this.Name = "TrainingSubscription";
			this.Text = "TrainingSubscription";
			((System.ComponentModel.ISupportInitialize)(this.items)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.subscription_datagrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.customer_datagrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox search_tb;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox customer_tb;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker trans_date;
		private System.Windows.Forms.DateTimePicker start_date;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView items;
		private System.Windows.Forms.TextBox total_tb;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox discount_tb;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox grandtotal_tb;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox advance_tb;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox balance_tb;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox payment_period_cb;
		private System.Windows.Forms.ToolStripButton toolStrip_saveButton;
		private System.Windows.Forms.ToolStripButton toolStrip_editButton;
		private System.Windows.Forms.ToolStripButton toolStrip_cancelButton;
		private System.Windows.Forms.ToolStripButton toolStrip_deleteButton;
		private System.Windows.Forms.ToolStripButton toolStrip_addButton;
		private System.Windows.Forms.TextBox code_tb;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DataGridView subscription_datagrid;
		private System.Windows.Forms.DataGridViewComboBoxColumn service_CBColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn period_CBColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn priority_CBColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn rate_DGColumn;
		private System.Windows.Forms.DataGridView customer_datagrid;
		private System.Windows.Forms.Label customer_name;
		private System.Windows.Forms.DataGridViewTextBoxColumn customer_codeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn customer_nameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn customer_anameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicesearch_codeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicesearch_customerNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicesearch_grandtotalColumn;
	}
}