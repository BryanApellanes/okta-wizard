using Okta.VisualStudio.Wizard.Controls;

namespace Okta.VisualStudio.Wizard.Forms
{
    partial class AutoRegisterApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoRegisterApplicationForm));
            this.AutoConfigureApplicationStatusStrip = new System.Windows.Forms.StatusStrip();
            this.AutoRegisterApplicationOktaDomainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OkButton = new System.Windows.Forms.Button();
            this.ApplicationCredentialsControl = new Okta.VisualStudio.Wizard.Controls.ApplicationCredentialsControl();
            this.SetupPictureBox = new System.Windows.Forms.PictureBox();
            this.AutoConfigureApplicationStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetupPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AutoConfigureApplicationStatusStrip
            // 
            this.AutoConfigureApplicationStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoRegisterApplicationOktaDomainToolStripStatusLabel,
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel});
            this.AutoConfigureApplicationStatusStrip.Location = new System.Drawing.Point(0, 653);
            this.AutoConfigureApplicationStatusStrip.Name = "AutoConfigureApplicationStatusStrip";
            this.AutoConfigureApplicationStatusStrip.Size = new System.Drawing.Size(1020, 22);
            this.AutoConfigureApplicationStatusStrip.TabIndex = 1;
            this.AutoConfigureApplicationStatusStrip.Text = "statusStrip1";
            // 
            // AutoRegisterApplicationOktaDomainToolStripStatusLabel
            // 
            this.AutoRegisterApplicationOktaDomainToolStripStatusLabel.Name = "AutoRegisterApplicationOktaDomainToolStripStatusLabel";
            this.AutoRegisterApplicationOktaDomainToolStripStatusLabel.Size = new System.Drawing.Size(102, 17);
            this.AutoRegisterApplicationOktaDomainToolStripStatusLabel.Text = "dev-xxx.okta.com";
            // 
            // AutoRegisterApplicationApiTokenToolStripStatusLabel
            // 
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel.Name = "AutoRegisterApplicationApiTokenToolStripStatusLabel";
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel.Size = new System.Drawing.Size(67, 17);
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel.Tag = "ApiToken";
            this.AutoRegisterApplicationApiTokenToolStripStatusLabel.Text = "xxxxxxxxxx";
            // 
            // OkButton
            // 
            this.OkButton.Enabled = false;
            this.OkButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(560, 329);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(175, 32);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // ApplicationCredentialsControl
            // 
            this.ApplicationCredentialsControl.ApplicationName = "APPLICATION_NAME";
            this.ApplicationCredentialsControl.ClientId = "";
            this.ApplicationCredentialsControl.ClientSecret = "";
            this.ApplicationCredentialsControl.HelpUri = new System.Uri("https://developer.okta.com/docs/guides/find-your-app-credentials/findcreds/", System.UriKind.Absolute);
            this.ApplicationCredentialsControl.Location = new System.Drawing.Point(35, 26);
            this.ApplicationCredentialsControl.MaximumSize = new System.Drawing.Size(800, 400);
            this.ApplicationCredentialsControl.MinimumSize = new System.Drawing.Size(800, 400);
            this.ApplicationCredentialsControl.Model = null;
            this.ApplicationCredentialsControl.Name = "ApplicationCredentialsControl";
            this.ApplicationCredentialsControl.Size = new System.Drawing.Size(800, 400);
            this.ApplicationCredentialsControl.TabIndex = 0;
            this.ApplicationCredentialsControl.Tag = "ApplicationCredentials";
            // 
            // SetupPictureBox
            // 
            this.SetupPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SetupPictureBox.Image")));
            this.SetupPictureBox.Location = new System.Drawing.Point(741, 332);
            this.SetupPictureBox.Name = "SetupPictureBox";
            this.SetupPictureBox.Size = new System.Drawing.Size(25, 25);
            this.SetupPictureBox.TabIndex = 3;
            this.SetupPictureBox.TabStop = false;
            // 
            // AutoRegisterApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1020, 675);
            this.ControlBox = false;
            this.Controls.Add(this.SetupPictureBox);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AutoConfigureApplicationStatusStrip);
            this.Controls.Add(this.ApplicationCredentialsControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1020, 675);
            this.Name = "AutoRegisterApplicationForm";
            this.Text = "New Okta Project - Register Application";
            this.AutoConfigureApplicationStatusStrip.ResumeLayout(false);
            this.AutoConfigureApplicationStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetupPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip AutoConfigureApplicationStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel AutoRegisterApplicationOktaDomainToolStripStatusLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.ToolStripStatusLabel AutoRegisterApplicationApiTokenToolStripStatusLabel;
        private System.Windows.Forms.PictureBox SetupPictureBox;
        public ApplicationCredentialsControl ApplicationCredentialsControl;
    }
}