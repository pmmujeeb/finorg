namespace FinOrg.Training_Module
{
	partial class TrainingPeriod
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingPeriod));
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
			this.dgv1 = new System.Windows.Forms.DataGridView();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.BackColor = System.Drawing.Color.DimGray;
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
			this.toolStrip.Size = new System.Drawing.Size(743, 44);
			this.toolStrip.Stretch = true;
			this.toolStrip.TabIndex = 21;
			this.toolStrip.Text = "ToolStrip";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.BackColor = System.Drawing.Color.Gray;
			this.newToolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.DarkRed;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(101, 41);
			this.newToolStripButton.Text = "&New Receipt";
			this.newToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.newToolStripButton.Visible = false;
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
			this.printToolStripButton.Visible = false;
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
			// 
			// dgv1
			// 
			this.dgv1.AllowDrop = true;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgv1.BackgroundColor = System.Drawing.Color.White;
			this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv1.Location = new System.Drawing.Point(12, 68);
			this.dgv1.Name = "dgv1";
			this.dgv1.Size = new System.Drawing.Size(717, 301);
			this.dgv1.TabIndex = 22;
			// 
			// TrainingPeriod
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(743, 381);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.dgv1);
			this.Name = "TrainingPeriod";
			this.Text = "TrainingPeriod";
			this.Load += new System.EventHandler(this.TrainingPeriod_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
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
		private System.Windows.Forms.DataGridView dgv1;
	}
}