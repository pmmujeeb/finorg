namespace FinOrg
{
    partial class Frmlogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmlogin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdpasscancel = new System.Windows.Forms.Button();
            this.cmdpass = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnewpass2 = new System.Windows.Forms.TextBox();
            this.txtnewpass1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdchange = new System.Windows.Forms.Button();
            this.cmdcancel = new System.Windows.Forms.Button();
            this.cmdok = new System.Windows.Forms.Button();
            this.btnlang = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtpass);
            this.panel1.Controls.Add(this.txtuser);
            this.panel1.Location = new System.Drawing.Point(16, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 305);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmdpasscancel);
            this.panel3.Controls.Add(this.cmdpass);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtnewpass2);
            this.panel3.Controls.Add(this.txtnewpass1);
            this.panel3.Location = new System.Drawing.Point(418, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(472, 257);
            this.panel3.TabIndex = 4;
            this.panel3.Visible = false;
            // 
            // cmdpasscancel
            // 
            this.cmdpasscancel.Location = new System.Drawing.Point(248, 177);
            this.cmdpasscancel.Name = "cmdpasscancel";
            this.cmdpasscancel.Size = new System.Drawing.Size(129, 36);
            this.cmdpasscancel.TabIndex = 9;
            this.cmdpasscancel.Text = "&Cancel";
            this.cmdpasscancel.UseVisualStyleBackColor = true;
            this.cmdpasscancel.Click += new System.EventHandler(this.cmdpasscancel_Click);
            // 
            // cmdpass
            // 
            this.cmdpass.AutoEllipsis = true;
            this.cmdpass.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdpass.Location = new System.Drawing.Point(54, 177);
            this.cmdpass.Name = "cmdpass";
            this.cmdpass.Size = new System.Drawing.Size(104, 36);
            this.cmdpass.TabIndex = 8;
            this.cmdpass.Text = "&OK";
            this.cmdpass.UseVisualStyleBackColor = true;
            this.cmdpass.Click += new System.EventHandler(this.cmdpass_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(17, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Confirm Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(17, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "New Password";
            // 
            // txtnewpass2
            // 
            this.txtnewpass2.Location = new System.Drawing.Point(201, 89);
            this.txtnewpass2.Name = "txtnewpass2";
            this.txtnewpass2.PasswordChar = '*';
            this.txtnewpass2.Size = new System.Drawing.Size(189, 27);
            this.txtnewpass2.TabIndex = 5;
            // 
            // txtnewpass1
            // 
            this.txtnewpass1.Location = new System.Drawing.Point(201, 48);
            this.txtnewpass1.Name = "txtnewpass1";
            this.txtnewpass1.PasswordChar = '*';
            this.txtnewpass1.Size = new System.Drawing.Size(189, 27);
            this.txtnewpass1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(95, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(95, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name";
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(251, 232);
            this.txtpass.Name = "txtpass";
            this.txtpass.PasswordChar = '*';
            this.txtpass.Size = new System.Drawing.Size(189, 27);
            this.txtpass.TabIndex = 1;
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(249, 189);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(191, 27);
            this.txtuser.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btnlang);
            this.panel2.Controls.Add(this.cmdchange);
            this.panel2.Controls.Add(this.cmdcancel);
            this.panel2.Controls.Add(this.cmdok);
            this.panel2.Location = new System.Drawing.Point(16, 327);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(563, 93);
            this.panel2.TabIndex = 1;
            // 
            // cmdchange
            // 
            this.cmdchange.Location = new System.Drawing.Point(387, 32);
            this.cmdchange.Name = "cmdchange";
            this.cmdchange.Size = new System.Drawing.Size(158, 36);
            this.cmdchange.TabIndex = 2;
            this.cmdchange.Text = "Change &Password";
            this.cmdchange.UseVisualStyleBackColor = true;
            this.cmdchange.Click += new System.EventHandler(this.cmdchange_Click);
            // 
            // cmdcancel
            // 
            this.cmdcancel.Location = new System.Drawing.Point(157, 32);
            this.cmdcancel.Name = "cmdcancel";
            this.cmdcancel.Size = new System.Drawing.Size(129, 36);
            this.cmdcancel.TabIndex = 1;
            this.cmdcancel.Text = "&Cancel";
            this.cmdcancel.UseVisualStyleBackColor = true;
            this.cmdcancel.Click += new System.EventHandler(this.cmdcancel_Click);
            // 
            // cmdok
            // 
            this.cmdok.AutoEllipsis = true;
            this.cmdok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdok.Location = new System.Drawing.Point(32, 32);
            this.cmdok.Name = "cmdok";
            this.cmdok.Size = new System.Drawing.Size(104, 36);
            this.cmdok.TabIndex = 0;
            this.cmdok.Text = "&OK";
            this.cmdok.UseVisualStyleBackColor = true;
            this.cmdok.Click += new System.EventHandler(this.cmdok_Click);
            // 
            // btnlang
            // 
            this.btnlang.Location = new System.Drawing.Point(300, 33);
            this.btnlang.Name = "btnlang";
            this.btnlang.Size = new System.Drawing.Size(81, 35);
            this.btnlang.TabIndex = 70;
            this.btnlang.Tag = "0";
            this.btnlang.Text = "عربي";
            this.btnlang.UseVisualStyleBackColor = true;
            this.btnlang.Click += new System.EventHandler(this.btnlang_Click);
            // 
            // Frmlogin
            // 
            this.AcceptButton = this.cmdok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 436);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frmlogin";
            this.Text = "Frmlogin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdcancel;
        private System.Windows.Forms.Button cmdok;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cmdpasscancel;
        private System.Windows.Forms.Button cmdpass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnewpass2;
        private System.Windows.Forms.TextBox txtnewpass1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdchange;
        private System.Windows.Forms.Button btnlang;
    }
}