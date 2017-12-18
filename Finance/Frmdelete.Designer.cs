namespace FinOrg
{
    partial class frmdelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmdelete));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpdate = new System.Windows.Forms.Panel();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.lbldt2 = new System.Windows.Forms.Label();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.lbldt1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.cmdclose = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.cmbtrntype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btndelete = new System.Windows.Forms.Button();
            this.grpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpdate
            // 
            this.grpdate.Controls.Add(this.dt2);
            this.grpdate.Controls.Add(this.lbldt2);
            this.grpdate.Controls.Add(this.dt1);
            this.grpdate.Controls.Add(this.lbldt1);
            resources.ApplyResources(this.grpdate, "grpdate");
            this.grpdate.Name = "grpdate";
            // 
            // dt2
            // 
            resources.ApplyResources(this.dt2, "dt2");
            this.dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt2.Name = "dt2";
            // 
            // lbldt2
            // 
            resources.ApplyResources(this.lbldt2, "lbldt2");
            this.lbldt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbldt2.Name = "lbldt2";
            // 
            // dt1
            // 
            resources.ApplyResources(this.dt1, "dt1");
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Name = "dt1";
            // 
            // lbldt1
            // 
            resources.ApplyResources(this.lbldt1, "lbldt1");
            this.lbldt1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbldt1.Name = "lbldt1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // dgv1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.dgv1, "dgv1");
            this.dgv1.Name = "dgv1";
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            // 
            // cmdclose
            // 
            resources.ApplyResources(this.cmdclose, "cmdclose");
            this.cmdclose.Name = "cmdclose";
            this.cmdclose.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cmbtrntype
            // 
            this.cmbtrntype.DisplayMember = "Unit_name";
            this.cmbtrntype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtrntype.FormattingEnabled = true;
            resources.ApplyResources(this.cmbtrntype, "cmbtrntype");
            this.cmbtrntype.Name = "cmbtrntype";
            this.cmbtrntype.ValueMember = "Unit_id";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Name = "label4";
            // 
            // btndelete
            // 
            resources.ApplyResources(this.btndelete, "btndelete");
            this.btndelete.Name = "btndelete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // frmdelete
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.cmbtrntype);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdclose);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.grpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmdelete";
            this.Load += new System.EventHandler(this.frmdelete_Load);
            this.grpdate.ResumeLayout(false);
            this.grpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel grpdate;
        private System.Windows.Forms.DateTimePicker dt2;
        private System.Windows.Forms.Label lbldt2;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.Label lbldt1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button cmdclose;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox cmbtrntype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btndelete;
    }
}