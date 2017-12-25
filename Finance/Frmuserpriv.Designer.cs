namespace FinOrg
{
    partial class Frmuserpriv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmuserpriv));
            this.dgv1 = new System.Windows.Forms.DataGridView();
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
            this.cmbmenu = new System.Windows.Forms.ComboBox();
            this.lblmenu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbgrp = new System.Windows.Forms.ComboBox();
            this.txtpriv = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowDrop = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(12, 83);
            this.dgv1.Name = "dgv1";
            this.dgv1.Size = new System.Drawing.Size(988, 431);
            this.dgv1.TabIndex = 16;
            this.dgv1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellClick);
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            this.dgv1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellDoubleClick);
            this.dgv1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellValidated);
            this.dgv1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellValueChanged);
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
            this.toolStrip.Size = new System.Drawing.Size(1031, 44);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 17;
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
            this.SearchToolStripButton.Visible = false;
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
            this.toolclose.Click += new System.EventHandler(this.toolclose_Click);
            // 
            // cmbmenu
            // 
            this.cmbmenu.FormattingEnabled = true;
            this.cmbmenu.Location = new System.Drawing.Point(567, 53);
            this.cmbmenu.Name = "cmbmenu";
            this.cmbmenu.Size = new System.Drawing.Size(240, 21);
            this.cmbmenu.TabIndex = 18;
            this.cmbmenu.SelectedIndexChanged += new System.EventHandler(this.cmbmenu_SelectedIndexChanged);
            // 
            // lblmenu
            // 
            this.lblmenu.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblmenu.ForeColor = System.Drawing.Color.Red;
            this.lblmenu.Location = new System.Drawing.Point(480, 54);
            this.lblmenu.Name = "lblmenu";
            this.lblmenu.Size = new System.Drawing.Size(81, 20);
            this.lblmenu.TabIndex = 23;
            this.lblmenu.Text = "Menu List";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(20, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "User Group";
            // 
            // cmbgrp
            // 
            this.cmbgrp.FormattingEnabled = true;
            this.cmbgrp.Location = new System.Drawing.Point(117, 53);
            this.cmbgrp.Name = "cmbgrp";
            this.cmbgrp.Size = new System.Drawing.Size(240, 21);
            this.cmbgrp.TabIndex = 24;
            this.cmbgrp.SelectedIndexChanged += new System.EventHandler(this.cmbgrp_SelectedIndexChanged);
            this.cmbgrp.TextUpdate += new System.EventHandler(this.cmbgrp_TextUpdate);
            this.cmbgrp.Validated += new System.EventHandler(this.cmbgrp_Validated);
            // 
            // txtpriv
            // 
            this.txtpriv.Location = new System.Drawing.Point(401, 257);
            this.txtpriv.Name = "txtpriv";
            this.txtpriv.Size = new System.Drawing.Size(91, 20);
            this.txtpriv.TabIndex = 26;
            this.txtpriv.Visible = false;
            // 
            // Frmuserpriv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1031, 535);
            this.Controls.Add(this.txtpriv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbgrp);
            this.Controls.Add(this.lblmenu);
            this.Controls.Add(this.cmbmenu);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dgv1);
            this.Name = "Frmuserpriv";
            this.Text = "Frmuserpriv";
            this.Load += new System.EventHandler(this.Frmuserpriv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
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
        private System.Windows.Forms.ComboBox cmbmenu;
        private System.Windows.Forms.Label lblmenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbgrp;
       public System.Windows.Forms.TextBox txtpriv;
    }
}