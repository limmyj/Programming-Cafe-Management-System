namespace Assignment
{
    partial class LoginPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPage));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTPNum = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.forgotPW = new System.Windows.Forms.LinkLabel();
            this.currentHide = new System.Windows.Forms.PictureBox();
            this.currentShow = new System.Windows.Forms.PictureBox();
            this.pboUser = new System.Windows.Forms.PictureBox();
            this.pboClear = new System.Windows.Forms.PictureBox();
            this.pboLogout = new System.Windows.Forms.PictureBox();
            this.pboLogin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.currentHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 41);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // txtTPNum
            // 
            this.txtTPNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTPNum.Location = new System.Drawing.Point(314, 360);
            this.txtTPNum.Name = "txtTPNum";
            this.txtTPNum.Size = new System.Drawing.Size(209, 38);
            this.txtTPNum.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(314, 435);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(209, 38);
            this.txtPassword.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 41);
            this.label4.TabIndex = 8;
            this.label4.Text = "TP Number:";
            // 
            // forgotPW
            // 
            this.forgotPW.AutoSize = true;
            this.forgotPW.Font = new System.Drawing.Font("Modern No. 20", 10.125F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgotPW.Location = new System.Drawing.Point(312, 486);
            this.forgotPW.Name = "forgotPW";
            this.forgotPW.Size = new System.Drawing.Size(211, 29);
            this.forgotPW.TabIndex = 12;
            this.forgotPW.TabStop = true;
            this.forgotPW.Text = "forgot password?";
            this.forgotPW.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.forgotPW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPW_LinkClicked);
            // 
            // currentHide
            // 
            this.currentHide.Image = ((System.Drawing.Image)(resources.GetObject("currentHide.Image")));
            this.currentHide.Location = new System.Drawing.Point(548, 435);
            this.currentHide.Name = "currentHide";
            this.currentHide.Size = new System.Drawing.Size(42, 38);
            this.currentHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentHide.TabIndex = 34;
            this.currentHide.TabStop = false;
            this.currentHide.Click += new System.EventHandler(this.currentHide_Click);
            // 
            // currentShow
            // 
            this.currentShow.Image = ((System.Drawing.Image)(resources.GetObject("currentShow.Image")));
            this.currentShow.Location = new System.Drawing.Point(548, 435);
            this.currentShow.Name = "currentShow";
            this.currentShow.Size = new System.Drawing.Size(42, 38);
            this.currentShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentShow.TabIndex = 33;
            this.currentShow.TabStop = false;
            this.currentShow.Click += new System.EventHandler(this.currentShow_Click);
            // 
            // pboUser
            // 
            this.pboUser.Image = ((System.Drawing.Image)(resources.GetObject("pboUser.Image")));
            this.pboUser.Location = new System.Drawing.Point(192, 39);
            this.pboUser.Name = "pboUser";
            this.pboUser.Size = new System.Drawing.Size(259, 252);
            this.pboUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboUser.TabIndex = 35;
            this.pboUser.TabStop = false;
            // 
            // pboClear
            // 
            this.pboClear.Image = ((System.Drawing.Image)(resources.GetObject("pboClear.Image")));
            this.pboClear.Location = new System.Drawing.Point(71, 579);
            this.pboClear.Name = "pboClear";
            this.pboClear.Size = new System.Drawing.Size(80, 71);
            this.pboClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboClear.TabIndex = 37;
            this.pboClear.TabStop = false;
            this.pboClear.Click += new System.EventHandler(this.pboClear_Click);
            // 
            // pboLogout
            // 
            this.pboLogout.Image = ((System.Drawing.Image)(resources.GetObject("pboLogout.Image")));
            this.pboLogout.Location = new System.Drawing.Point(510, 579);
            this.pboLogout.Name = "pboLogout";
            this.pboLogout.Size = new System.Drawing.Size(80, 71);
            this.pboLogout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboLogout.TabIndex = 39;
            this.pboLogout.TabStop = false;
            this.pboLogout.Click += new System.EventHandler(this.pboLogout_Click);
            // 
            // pboLogin
            // 
            this.pboLogin.Image = ((System.Drawing.Image)(resources.GetObject("pboLogin.Image")));
            this.pboLogin.Location = new System.Drawing.Point(263, 562);
            this.pboLogin.Name = "pboLogin";
            this.pboLogin.Size = new System.Drawing.Size(139, 110);
            this.pboLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboLogin.TabIndex = 40;
            this.pboLogin.TabStop = false;
            this.pboLogin.Click += new System.EventHandler(this.pboLogin_Click);
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(672, 740);
            this.Controls.Add(this.pboLogin);
            this.Controls.Add(this.pboLogout);
            this.Controls.Add(this.pboClear);
            this.Controls.Add(this.pboUser);
            this.Controls.Add(this.currentShow);
            this.Controls.Add(this.forgotPW);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtTPNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentHide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginPage";
            this.Text = "Login Page";
            ((System.ComponentModel.ISupportInitialize)(this.currentHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTPNum;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel forgotPW;
        private System.Windows.Forms.PictureBox currentHide;
        private System.Windows.Forms.PictureBox currentShow;
        private System.Windows.Forms.PictureBox pboUser;
        private System.Windows.Forms.PictureBox pboClear;
        private System.Windows.Forms.PictureBox pboLogout;
        private System.Windows.Forms.PictureBox pboLogin;
    }
}

