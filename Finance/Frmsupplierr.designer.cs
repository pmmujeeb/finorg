namespace FinOrg
{
    partial class frmcustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcustomer));
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
            this.txtpriv = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtcusname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcuscode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtidnumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtmobileno = new System.Windows.Forms.TextBox();
            this.cmbsponsor = new System.Windows.Forms.ComboBox();
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
            this.tooldelete.Visible = false;
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(12, 47);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.Height = 27;
            this.dgv1.Size = new System.Drawing.Size(717, 234);
            this.dgv1.TabIndex = 15;
            this.dgv1.Visible = false;
            this.dgv1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellValidated);
            this.dgv1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_RowLeave);
            // 
            // txtpriv
            // 
            this.txtpriv.Location = new System.Drawing.Point(325, 190);
            this.txtpriv.Name = "txtpriv";
            this.txtpriv.Size = new System.Drawing.Size(91, 20);
            this.txtpriv.TabIndex = 20;
            this.txtpriv.Visible = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(532, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 27);
            this.button1.TabIndex = 21;
            this.button1.Text = "&Add ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtcusname
            // 
            this.txtcusname.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtcusname.Location = new System.Drawing.Point(123, 350);
            this.txtcusname.Name = "txtcusname";
            this.txtcusname.Size = new System.Drawing.Size(391, 25);
            this.txtcusname.TabIndex = 22;
            this.txtcusname.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(9, 352);
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
            this.label2.Location = new System.Drawing.Point(9, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 25;
            this.label2.Text = "Sponsor Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(9, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 27;
            this.label3.Text = "Customer Code";
            // 
            // txtcuscode
            // 
            this.txtcuscode.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtcuscode.Location = new System.Drawing.Point(123, 318);
            this.txtcuscode.Name = "txtcuscode";
            this.txtcuscode.Size = new System.Drawing.Size(391, 25);
            this.txtcuscode.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(9, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 18);
            this.label4.TabIndex = 29;
            this.label4.Text = "ID Number";
            // 
            // txtidnumber
            // 
            this.txtidnumber.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtidnumber.Location = new System.Drawing.Point(123, 381);
            this.txtidnumber.Name = "txtidnumber";
            this.txtidnumber.Size = new System.Drawing.Size(391, 25);
            this.txtidnumber.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 18);
            this.label5.TabIndex = 31;
            this.label5.Text = "Mobile No.";
            // 
            // txtmobileno
            // 
            this.txtmobileno.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtmobileno.Location = new System.Drawing.Point(123, 408);
            this.txtmobileno.Name = "txtmobileno";
            this.txtmobileno.Size = new System.Drawing.Size(391, 25);
            this.txtmobileno.TabIndex = 30;
            // 
            // cmbsponsor
            // 
            this.cmbsponsor.DisplayMember = "Unit_name";
            this.cmbsponsor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsponsor.FormattingEnabled = true;
            this.cmbsponsor.Location = new System.Drawing.Point(123, 291);
            this.cmbsponsor.Name = "cmbsponsor";
            this.cmbsponsor.Size = new System.Drawing.Size(391, 21);
            this.cmbsponsor.TabIndex = 32;
            this.cmbsponsor.ValueMember = "Unit_id";
            // 
            // frmcustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 445);
            this.Controls.Add(this.cmbsponsor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtmobileno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtidnumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcuscode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcusname);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtpriv);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.toolStrip);
            this.Name = "frmcustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLeader";
            this.Activated += new System.EventHandler(this.FrmLeader_Activated);
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
       private System.Windows.Forms.Button button1;
       private System.Windows.Forms.TextBox txtcusname;
       private System.Windows.Forms.Label label1;
       private System.Windows.Forms.Label label2;
       private System.Windows.Forms.Label label3;
       private System.Windows.Forms.TextBox txtcuscode;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.TextBox txtidnumber;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.TextBox txtmobileno;
       private System.Windows.Forms.ComboBox cmbsponsor;
    }
}