namespace FinOrg
{
    partial class frmSponsor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSponsor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.txtpriv = new System.Windows.Forms.TextBox();
            this.txtcusname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcuscode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtidnumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtmobileno = new System.Windows.Forms.TextBox();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.chksort = new System.Windows.Forms.CheckBox();
            this.chkcmp = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtcusadd = new System.Windows.Forms.TextBox();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(741, 44);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 3;
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
            this.newToolStripButton.Size = new System.Drawing.Size(50, 41);
            this.newToolStripButton.Text = "&New ";
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
            this.tooldelete.Click += new System.EventHandler(this.tooldelete_Click);
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
            this.dgv1.AllowDrop = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(12, 70);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.Height = 27;
            this.dgv1.Size = new System.Drawing.Size(717, 236);
            this.dgv1.TabIndex = 15;
            this.dgv1.Visible = false;
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            this.dgv1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            // 
            // txtpriv
            // 
            this.txtpriv.Location = new System.Drawing.Point(325, 215);
            this.txtpriv.Name = "txtpriv";
            this.txtpriv.Size = new System.Drawing.Size(91, 20);
            this.txtpriv.TabIndex = 20;
            this.txtpriv.Visible = false;
            // 
            // txtcusname
            // 
            this.txtcusname.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtcusname.Location = new System.Drawing.Point(126, 339);
            this.txtcusname.Name = "txtcusname";
            this.txtcusname.Size = new System.Drawing.Size(391, 25);
            this.txtcusname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 25;
            this.label2.Text = "Contact Person";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "Company Code";
            // 
            // txtcuscode
            // 
            this.txtcuscode.BackColor = System.Drawing.Color.Silver;
            this.txtcuscode.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtcuscode.Location = new System.Drawing.Point(126, 307);
            this.txtcuscode.Name = "txtcuscode";
            this.txtcuscode.ReadOnly = true;
            this.txtcuscode.Size = new System.Drawing.Size(391, 25);
            this.txtcuscode.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 18);
            this.label4.TabIndex = 29;
            this.label4.Text = "ID Number";
            // 
            // txtidnumber
            // 
            this.txtidnumber.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtidnumber.Location = new System.Drawing.Point(126, 370);
            this.txtidnumber.Name = "txtidnumber";
            this.txtidnumber.Size = new System.Drawing.Size(391, 25);
            this.txtidnumber.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(12, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 18);
            this.label5.TabIndex = 31;
            this.label5.Text = "Mobile No.";
            // 
            // txtmobileno
            // 
            this.txtmobileno.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtmobileno.Location = new System.Drawing.Point(126, 397);
            this.txtmobileno.Name = "txtmobileno";
            this.txtmobileno.Size = new System.Drawing.Size(391, 25);
            this.txtmobileno.TabIndex = 3;
            // 
            // txtcontact
            // 
            this.txtcontact.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtcontact.Location = new System.Drawing.Point(125, 428);
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(391, 25);
            this.txtcontact.TabIndex = 4;
            // 
            // chksort
            // 
            this.chksort.AutoSize = true;
            this.chksort.Location = new System.Drawing.Point(466, 47);
            this.chksort.Name = "chksort";
            this.chksort.Size = new System.Drawing.Size(140, 17);
            this.chksort.TabIndex = 46;
            this.chksort.Text = "Sort By Customer Name";
            this.chksort.UseVisualStyleBackColor = true;
            // 
            // chkcmp
            // 
            this.chkcmp.AutoSize = true;
            this.chkcmp.Location = new System.Drawing.Point(334, 47);
            this.chkcmp.Name = "chkcmp";
            this.chkcmp.Size = new System.Drawing.Size(113, 17);
            this.chkcmp.TabIndex = 45;
            this.chkcmp.Text = "Filter By Company";
            this.chkcmp.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.textBox1.Location = new System.Drawing.Point(12, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(307, 26);
            this.textBox1.TabIndex = 44;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label11.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.On;
            this.label11.Location = new System.Drawing.Point(540, 313);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 19);
            this.label11.TabIndex = 48;
            this.label11.Text = "Address";
            // 
            // txtcusadd
            // 
            this.txtcusadd.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtcusadd.Location = new System.Drawing.Point(530, 339);
            this.txtcusadd.Multiline = true;
            this.txtcusadd.Name = "txtcusadd";
            this.txtcusadd.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtcusadd.Size = new System.Drawing.Size(199, 88);
            this.txtcusadd.TabIndex = 47;
            this.txtcusadd.Tag = "1";
            // 
            // frmSponsor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 503);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtcusadd);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chksort);
            this.Controls.Add(this.chkcmp);
            this.Controls.Add(this.txtcontact);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtmobileno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtidnumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcuscode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcusname);
            this.Controls.Add(this.txtpriv);
            this.Controls.Add(this.toolStrip);
            this.Name = "frmSponsor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCompany";
            this.Load += new System.EventHandler(this.FrmLeader_Load);
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
        public System.Windows.Forms.TextBox txtpriv;
       private System.Windows.Forms.TextBox txtcusname;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.TextBox txtcuscode;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.TextBox txtidnumber;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.TextBox txtmobileno;
       private System.Windows.Forms.TextBox txtcontact;
       private System.Windows.Forms.CheckBox chksort;
       private System.Windows.Forms.CheckBox chkcmp;
       private System.Windows.Forms.TextBox textBox1;
       private System.Windows.Forms.Label label11;
       private System.Windows.Forms.TextBox txtcusadd;
    }
}