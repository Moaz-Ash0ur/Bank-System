namespace BankManagementSystem
{
    partial class Form1
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
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnManPerson = new Guna.UI2.WinForms.Guna2Button();
            this.btnShowLogin = new Guna.UI2.WinForms.Guna2Button();
            this.btnTransaction = new Guna.UI2.WinForms.Guna2Button();
            this.bbtnManUser = new Guna.UI2.WinForms.Guna2Button();
            this.btnManClient = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 2;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BorderRadius = 5;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.IndianRed;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(725, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(36, 28);
            this.guna2ControlBox1.TabIndex = 0;
            // 
            // btnManPerson
            // 
            this.btnManPerson.BorderRadius = 3;
            this.btnManPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManPerson.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnManPerson.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnManPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnManPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnManPerson.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnManPerson.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnManPerson.ForeColor = System.Drawing.Color.White;
            this.btnManPerson.Location = new System.Drawing.Point(365, 58);
            this.btnManPerson.Name = "btnManPerson";
            this.btnManPerson.Size = new System.Drawing.Size(216, 42);
            this.btnManPerson.TabIndex = 2;
            this.btnManPerson.Text = "Manage People";
            this.btnManPerson.Click += new System.EventHandler(this.btnManPerson_Click);
            // 
            // btnShowLogin
            // 
            this.btnShowLogin.BorderRadius = 2;
            this.btnShowLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowLogin.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnShowLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnShowLogin.ForeColor = System.Drawing.Color.White;
            this.btnShowLogin.Location = new System.Drawing.Point(365, 346);
            this.btnShowLogin.Name = "btnShowLogin";
            this.btnShowLogin.Size = new System.Drawing.Size(216, 42);
            this.btnShowLogin.TabIndex = 3;
            this.btnShowLogin.Text = "Login Register";
            this.btnShowLogin.Click += new System.EventHandler(this.btnShowLogin_Click);
            // 
            // btnTransaction
            // 
            this.btnTransaction.BorderRadius = 2;
            this.btnTransaction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransaction.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTransaction.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTransaction.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTransaction.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTransaction.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnTransaction.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTransaction.ForeColor = System.Drawing.Color.White;
            this.btnTransaction.Location = new System.Drawing.Point(365, 275);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(216, 42);
            this.btnTransaction.TabIndex = 4;
            this.btnTransaction.Text = "Transaction";
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // bbtnManUser
            // 
            this.bbtnManUser.BorderRadius = 2;
            this.bbtnManUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bbtnManUser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.bbtnManUser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.bbtnManUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.bbtnManUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.bbtnManUser.FillColor = System.Drawing.Color.MidnightBlue;
            this.bbtnManUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.bbtnManUser.ForeColor = System.Drawing.Color.White;
            this.bbtnManUser.Location = new System.Drawing.Point(365, 130);
            this.bbtnManUser.Name = "bbtnManUser";
            this.bbtnManUser.Size = new System.Drawing.Size(216, 42);
            this.bbtnManUser.TabIndex = 5;
            this.bbtnManUser.Text = "Manage User";
            this.bbtnManUser.Click += new System.EventHandler(this.bbtnManUser_Click);
            // 
            // btnManClient
            // 
            this.btnManClient.BorderRadius = 2;
            this.btnManClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManClient.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnManClient.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnManClient.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnManClient.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnManClient.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnManClient.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnManClient.ForeColor = System.Drawing.Color.White;
            this.btnManClient.Location = new System.Drawing.Point(365, 203);
            this.btnManClient.Name = "btnManClient";
            this.btnManClient.Size = new System.Drawing.Size(216, 42);
            this.btnManClient.TabIndex = 6;
            this.btnManClient.Text = "Manage Client";
            this.btnManClient.Click += new System.EventHandler(this.btnManClient_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.guna2HtmlLabel2);
            this.panel1.Controls.Add(this.guna2HtmlLabel1);
            this.panel1.Controls.Add(this.guna2PictureBox1);
            this.panel1.Location = new System.Drawing.Point(-4, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 447);
            this.panel1.TabIndex = 7;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(16, 380);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(3, 2);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = null;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblDate.Location = new System.Drawing.Point(16, 348);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(117, 17);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "guna2HtmlLabel3";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(75, 242);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(96, 22);
            this.guna2HtmlLabel2.TabIndex = 3;
            this.guna2HtmlLabel2.Text = "Our  System";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(11, 214);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(101, 22);
            this.guna2HtmlLabel1.TabIndex = 2;
            this.guna2HtmlLabel1.Text = "Welcome To";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::BankManagementSystem.Properties.Resources.Banktest;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(35, 41);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(136, 143);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::BankManagementSystem.Properties.Resources.Logout;
            this.pictureBox1.Location = new System.Drawing.Point(208, 401);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 442);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnManClient);
            this.Controls.Add(this.bbtnManUser);
            this.Controls.Add(this.btnTransaction);
            this.Controls.Add(this.btnShowLogin);
            this.Controls.Add(this.btnManPerson);
            this.Controls.Add(this.guna2ControlBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btnManClient;
        private Guna.UI2.WinForms.Guna2Button bbtnManUser;
        private Guna.UI2.WinForms.Guna2Button btnTransaction;
        private Guna.UI2.WinForms.Guna2Button btnShowLogin;
        private Guna.UI2.WinForms.Guna2Button btnManPerson;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

