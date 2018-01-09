namespace FinOrg
{
    partial class FrmrepView
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
            this.crv1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crv1
            // 
            this.crv1.ActiveViewIndex = -1;
            this.crv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crv1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crv1.Location = new System.Drawing.Point(0, 0);
            this.crv1.Name = "crv1";
            this.crv1.Size = new System.Drawing.Size(842, 444);
            this.crv1.TabIndex = 0;
            this.crv1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(676, 0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(127, 23);
            this.btnclose.TabIndex = 1;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // FrmrepView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 444);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.crv1);
            this.Name = "FrmrepView";
            this.Text = "View Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmrepView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crv1;
        private System.Windows.Forms.Button btnclose;


    }
}