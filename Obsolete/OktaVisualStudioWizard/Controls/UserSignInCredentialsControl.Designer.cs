
namespace Okta.VisualStudio.Wizard.Controls
{
    partial class UserSignInCredentialsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSignInCredentialsControl));
            this.SignInUrlTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.SignUpLinkLabel = new System.Windows.Forms.LinkLabel();
            this.DontKnowYourSignInUrlLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ForgotPasswordLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SignInUrlLabel = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.ForgotPasswordToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HelpPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SignInUrlTextBox
            // 
            this.SignInUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SignInUrlTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInUrlTextBox.Location = new System.Drawing.Point(111, 18);
            this.SignInUrlTextBox.Name = "SignInUrlTextBox";
            this.SignInUrlTextBox.Size = new System.Drawing.Size(386, 29);
            this.SignInUrlTextBox.TabIndex = 0;
            this.SignInUrlTextBox.Tag = "SignInUrl";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNameTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameTextBox.Location = new System.Drawing.Point(111, 78);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(386, 29);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.Tag = "UserName";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(111, 121);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '•';
            this.PasswordTextBox.Size = new System.Drawing.Size(386, 29);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.Tag = "Password";
            // 
            // SignUpLinkLabel
            // 
            this.SignUpLinkLabel.AutoSize = true;
            this.SignUpLinkLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignUpLinkLabel.Location = new System.Drawing.Point(108, 50);
            this.SignUpLinkLabel.Name = "SignUpLinkLabel";
            this.SignUpLinkLabel.Size = new System.Drawing.Size(47, 15);
            this.SignUpLinkLabel.TabIndex = 3;
            this.SignUpLinkLabel.TabStop = true;
            this.SignUpLinkLabel.Text = "Sign up";
            // 
            // DontKnowYourSignInUrlLinkLabel
            // 
            this.DontKnowYourSignInUrlLinkLabel.AutoSize = true;
            this.DontKnowYourSignInUrlLinkLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DontKnowYourSignInUrlLinkLabel.Location = new System.Drawing.Point(211, 50);
            this.DontKnowYourSignInUrlLinkLabel.Name = "DontKnowYourSignInUrlLinkLabel";
            this.DontKnowYourSignInUrlLinkLabel.Size = new System.Drawing.Size(164, 15);
            this.DontKnowYourSignInUrlLinkLabel.TabIndex = 4;
            this.DontKnowYourSignInUrlLinkLabel.TabStop = true;
            this.DontKnowYourSignInUrlLinkLabel.Text = "Don\'t know your sign-in URL?";
            // 
            // ForgotPasswordLinkLabel
            // 
            this.ForgotPasswordLinkLabel.AutoSize = true;
            this.ForgotPasswordLinkLabel.Enabled = false;
            this.ForgotPasswordLinkLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForgotPasswordLinkLabel.Location = new System.Drawing.Point(108, 153);
            this.ForgotPasswordLinkLabel.Name = "ForgotPasswordLinkLabel";
            this.ForgotPasswordLinkLabel.Size = new System.Drawing.Size(95, 15);
            this.ForgotPasswordLinkLabel.TabIndex = 5;
            this.ForgotPasswordLinkLabel.TabStop = true;
            this.ForgotPasswordLinkLabel.Text = "Forgot password";
            // 
            // SignInUrlLabel
            // 
            this.SignInUrlLabel.AutoSize = true;
            this.SignInUrlLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInUrlLabel.Location = new System.Drawing.Point(18, 26);
            this.SignInUrlLabel.Name = "SignInUrlLabel";
            this.SignInUrlLabel.Size = new System.Drawing.Size(69, 15);
            this.SignInUrlLabel.TabIndex = 6;
            this.SignInUrlLabel.Text = "Sign-in URL";
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(22, 86);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(65, 15);
            this.UserNameLabel.TabIndex = 7;
            this.UserNameLabel.Text = "User Name";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(30, 129);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(57, 15);
            this.PasswordLabel.TabIndex = 8;
            this.PasswordLabel.Text = "Password";
            // 
            // HelpPictureBox
            // 
            this.HelpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("HelpPictureBox.Image")));
            this.HelpPictureBox.InitialImage = null;
            this.HelpPictureBox.Location = new System.Drawing.Point(209, 153);
            this.HelpPictureBox.Name = "HelpPictureBox";
            this.HelpPictureBox.Size = new System.Drawing.Size(15, 15);
            this.HelpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HelpPictureBox.TabIndex = 9;
            this.HelpPictureBox.TabStop = false;
            // 
            // UserSignInCredentialsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HelpPictureBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.SignInUrlLabel);
            this.Controls.Add(this.ForgotPasswordLinkLabel);
            this.Controls.Add(this.DontKnowYourSignInUrlLinkLabel);
            this.Controls.Add(this.SignUpLinkLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.SignInUrlTextBox);
            this.Name = "UserSignInCredentialsControl";
            this.Size = new System.Drawing.Size(546, 190);
            ((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SignInUrlLabel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        public System.Windows.Forms.LinkLabel SignUpLinkLabel;
        public System.Windows.Forms.LinkLabel DontKnowYourSignInUrlLinkLabel;
        public System.Windows.Forms.LinkLabel ForgotPasswordLinkLabel;
        public System.Windows.Forms.TextBox SignInUrlTextBox;
        public System.Windows.Forms.TextBox UserNameTextBox;
        public System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.ToolTip ForgotPasswordToolTip;
        private System.Windows.Forms.PictureBox HelpPictureBox;
    }
}
