namespace FinOrg.Training_Module
{
	partial class TrainingService
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
			this.searchBox = new System.Windows.Forms.TextBox();
			this.search_label = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStrip_addButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_saveButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_editButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip_cancelButton = new System.Windows.Forms.ToolStripButton();
			this.servicesDataGrid = new System.Windows.Forms.DataGridView();
			this.servicegrid_codeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.servicegrid_nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.servicegrid_anameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.service_ename_tb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.service_aname_tb = new System.Windows.Forms.TextBox();
			this.ratesDataGrid = new System.Windows.Forms.DataGridView();
			this.rates_periodCBColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.rates_priorityCBColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.rates_rateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.service_code_tb = new System.Windows.Forms.TextBox();
			this.toolStrip_deleteButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.servicesDataGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ratesDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// searchBox
			// 
			this.searchBox.Location = new System.Drawing.Point(107, 49);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(304, 20);
			this.searchBox.TabIndex = 0;
			this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
			this.searchBox.Leave += new System.EventHandler(this.searchBox_Leave);
			// 
			// search_label
			// 
			this.search_label.AutoSize = true;
			this.search_label.Location = new System.Drawing.Point(27, 52);
			this.search_label.Name = "search_label";
			this.search_label.Size = new System.Drawing.Size(41, 13);
			this.search_label.TabIndex = 1;
			this.search_label.Text = "Search";
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
			this.toolStrip1.Size = new System.Drawing.Size(734, 38);
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
			this.toolStrip_cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_cancelButton.Name = "toolStrip_cancelButton";
			this.toolStrip_cancelButton.Size = new System.Drawing.Size(47, 35);
			this.toolStrip_cancelButton.Text = "Cancel";
			this.toolStrip_cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_cancelButton.Click += new System.EventHandler(this.toolStrip_cancelButton_Click);
			// 
			// servicesDataGrid
			// 
			this.servicesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.servicesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.servicegrid_codeColumn,
            this.servicegrid_nameColumn,
            this.servicegrid_anameColumn});
			this.servicesDataGrid.Location = new System.Drawing.Point(-218, 335);
			this.servicesDataGrid.Name = "servicesDataGrid";
			this.servicesDataGrid.ReadOnly = true;
			this.servicesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.servicesDataGrid.Size = new System.Drawing.Size(565, 210);
			this.servicesDataGrid.TabIndex = 3;
			this.servicesDataGrid.DoubleClick += new System.EventHandler(this.servicesDataGrid_DoubleClick);
			// 
			// servicegrid_codeColumn
			// 
			this.servicegrid_codeColumn.DataPropertyName = "TR_SERVICE_CODE";
			this.servicegrid_codeColumn.HeaderText = "Code";
			this.servicegrid_codeColumn.Name = "servicegrid_codeColumn";
			this.servicegrid_codeColumn.ReadOnly = true;
			// 
			// servicegrid_nameColumn
			// 
			this.servicegrid_nameColumn.DataPropertyName = "TR_SERVICE_NAME";
			this.servicegrid_nameColumn.HeaderText = "Name";
			this.servicegrid_nameColumn.Name = "servicegrid_nameColumn";
			this.servicegrid_nameColumn.ReadOnly = true;
			// 
			// servicegrid_anameColumn
			// 
			this.servicegrid_anameColumn.DataPropertyName = "TR_SERVICE_ANAME";
			this.servicegrid_anameColumn.HeaderText = "Arabic Name";
			this.servicegrid_anameColumn.Name = "servicegrid_anameColumn";
			this.servicegrid_anameColumn.ReadOnly = true;
			// 
			// service_ename_tb
			// 
			this.service_ename_tb.Location = new System.Drawing.Point(153, 123);
			this.service_ename_tb.Name = "service_ename_tb";
			this.service_ename_tb.Size = new System.Drawing.Size(156, 20);
			this.service_ename_tb.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(27, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "English Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(397, 129);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Arabic Name";
			// 
			// service_aname_tb
			// 
			this.service_aname_tb.Location = new System.Drawing.Point(516, 126);
			this.service_aname_tb.Name = "service_aname_tb";
			this.service_aname_tb.Size = new System.Drawing.Size(156, 20);
			this.service_aname_tb.TabIndex = 6;
			// 
			// ratesDataGrid
			// 
			this.ratesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ratesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rates_periodCBColumn,
            this.rates_priorityCBColumn,
            this.rates_rateColumn});
			this.ratesDataGrid.Location = new System.Drawing.Point(30, 191);
			this.ratesDataGrid.Name = "ratesDataGrid";
			this.ratesDataGrid.Size = new System.Drawing.Size(317, 138);
			this.ratesDataGrid.TabIndex = 8;
			// 
			// rates_periodCBColumn
			// 
			this.rates_periodCBColumn.HeaderText = "Training Period";
			this.rates_periodCBColumn.Name = "rates_periodCBColumn";
			// 
			// rates_priorityCBColumn
			// 
			this.rates_priorityCBColumn.HeaderText = "Training Priority";
			this.rates_priorityCBColumn.Name = "rates_priorityCBColumn";
			// 
			// rates_rateColumn
			// 
			this.rates_rateColumn.HeaderText = "Rate";
			this.rates_rateColumn.Name = "rates_rateColumn";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(27, 175);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Rates";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Code";
			// 
			// service_code_tb
			// 
			this.service_code_tb.Location = new System.Drawing.Point(153, 97);
			this.service_code_tb.Name = "service_code_tb";
			this.service_code_tb.Size = new System.Drawing.Size(156, 20);
			this.service_code_tb.TabIndex = 10;
			// 
			// toolStrip_deleteButton
			// 
			this.toolStrip_deleteButton.Image = global::FinOrg.Properties.Resources.WZDELETE;
			this.toolStrip_deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStrip_deleteButton.Name = "toolStrip_deleteButton";
			this.toolStrip_deleteButton.Size = new System.Drawing.Size(44, 35);
			this.toolStrip_deleteButton.Text = "Delete";
			this.toolStrip_deleteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolStrip_deleteButton.Click += new System.EventHandler(this.toolStrip_deleteButton_Click);
			// 
			// TrainingService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 438);
			this.Controls.Add(this.servicesDataGrid);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.service_code_tb);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ratesDataGrid);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.service_aname_tb);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.service_ename_tb);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.search_label);
			this.Controls.Add(this.searchBox);
			this.Name = "TrainingService";
			this.Text = "TrainingService";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.servicesDataGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ratesDataGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox searchBox;
		private System.Windows.Forms.Label search_label;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStrip_addButton;
		private System.Windows.Forms.ToolStripButton toolStrip_saveButton;
		private System.Windows.Forms.DataGridView servicesDataGrid;
		private System.Windows.Forms.TextBox service_ename_tb;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox service_aname_tb;
		private System.Windows.Forms.DataGridView ratesDataGrid;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolStripButton toolStrip_editButton;
		private System.Windows.Forms.ToolStripButton toolStrip_cancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox service_code_tb;
		private System.Windows.Forms.DataGridViewComboBoxColumn rates_periodCBColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn rates_priorityCBColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn rates_rateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicegrid_codeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicegrid_nameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn servicegrid_anameColumn;
		private System.Windows.Forms.ToolStripButton toolStrip_deleteButton;
	}
}