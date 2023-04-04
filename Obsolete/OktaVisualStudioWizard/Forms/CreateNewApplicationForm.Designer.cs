using Okta.VisualStudio.Wizard.Controls;

namespace Okta.VisualStudio.Wizard.Forms
{
    partial class CreateNewApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewApplicationForm));
            this.OkButton = new System.Windows.Forms.Button();
            this.CreateNewApplicationLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.oktaApplicationTypeControl = new Okta.VisualStudio.Wizard.Controls.OktaApplicationTypeControl();
            this.OktaApplicationTypeWarningLabel = new System.Windows.Forms.Label();
            this.ApplicationNameLabel = new System.Windows.Forms.Label();
            this.UserSignInCredentialsControl = new Okta.VisualStudio.Wizard.Controls.UserSignInCredentialsControl();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(1005, 731);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(175, 32);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // CreateNewApplicationLabel
            // 
            this.CreateNewApplicationLabel.AutoSize = true;
            this.CreateNewApplicationLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateNewApplicationLabel.Location = new System.Drawing.Point(57, 48);
            this.CreateNewApplicationLabel.Name = "CreateNewApplicationLabel";
            this.CreateNewApplicationLabel.Size = new System.Drawing.Size(323, 32);
            this.CreateNewApplicationLabel.TabIndex = 7;
            this.CreateNewApplicationLabel.Text = "Create New Okta Application";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(824, 731);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(175, 32);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Visible = false;
            // 
            // oktaApplicationTypeControl
            // 
            this.oktaApplicationTypeControl.BackColor = System.Drawing.Color.White;
            this.oktaApplicationTypeControl.Location = new System.Drawing.Point(102, 306);
            this.oktaApplicationTypeControl.Margin = new System.Windows.Forms.Padding(2);
            this.oktaApplicationTypeControl.Model = null;
            this.oktaApplicationTypeControl.Name = "oktaApplicationTypeControl";
            this.oktaApplicationTypeControl.SelectedOktaApplicationType = null;
            this.oktaApplicationTypeControl.Size = new System.Drawing.Size(1027, 346);
            this.oktaApplicationTypeControl.TabIndex = 9;
            // 
            // OktaApplicationTypeWarningLabel
            // 
            this.OktaApplicationTypeWarningLabel.AutoSize = true;
            this.OktaApplicationTypeWarningLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OktaApplicationTypeWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.OktaApplicationTypeWarningLabel.Location = new System.Drawing.Point(416, 654);
            this.OktaApplicationTypeWarningLabel.Name = "OktaApplicationTypeWarningLabel";
            this.OktaApplicationTypeWarningLabel.Size = new System.Drawing.Size(330, 30);
            this.OktaApplicationTypeWarningLabel.TabIndex = 10;
            this.OktaApplicationTypeWarningLabel.Text = "Please select an application type";
            this.OktaApplicationTypeWarningLabel.Visible = false;
            // 
            // ApplicationNameLabel
            // 
            this.ApplicationNameLabel.AutoSize = true;
            this.ApplicationNameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationNameLabel.Location = new System.Drawing.Point(386, 57);
            this.ApplicationNameLabel.Name = "ApplicationNameLabel";
            this.ApplicationNameLabel.Size = new System.Drawing.Size(20, 21);
            this.ApplicationNameLabel.TabIndex = 11;
            this.ApplicationNameLabel.Tag = "ApplicationName";
            this.ApplicationNameLabel.Text = "()";
            // 
            // UserSignInCredentialsControl
            // 
            this.UserSignInCredentialsControl.ForgotPasswordUrl = null;
            this.UserSignInCredentialsControl.Location = new System.Drawing.Point(102, 110);
            this.UserSignInCredentialsControl.Model = null;
            this.UserSignInCredentialsControl.Name = "UserSignInCredentialsControl";
            this.UserSignInCredentialsControl.Password = "";
            this.UserSignInCredentialsControl.SignInUrl = "";
            this.UserSignInCredentialsControl.SignUpUriHelpUrl = null;
            this.UserSignInCredentialsControl.SignUpUrl = null;
            this.UserSignInCredentialsControl.Size = new System.Drawing.Size(699, 190);
            this.UserSignInCredentialsControl.TabIndex = 12;
            this.UserSignInCredentialsControl.UserName = "";
            // 
            // CreateNewApplicationForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1263, 821);
            this.ControlBox = false;
            this.Controls.Add(this.UserSignInCredentialsControl);
            this.Controls.Add(this.ApplicationNameLabel);
            this.Controls.Add(this.OktaApplicationTypeWarningLabel);
            this.Controls.Add(this.oktaApplicationTypeControl);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CreateNewApplicationLabel);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CreateNewApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Okta Visual Studio Wizard - Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label CreateNewApplicationLabel;
        private System.Windows.Forms.Button CancelButton;
        private OktaApplicationTypeControl oktaApplicationTypeControl;
        private System.Windows.Forms.Label OktaApplicationTypeWarningLabel;
        private System.Windows.Forms.Label ApplicationNameLabel;
        private UserSignInCredentialsControl UserSignInCredentialsControl;
    }
}