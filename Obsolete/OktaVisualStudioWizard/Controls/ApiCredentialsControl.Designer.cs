namespace Okta.VisualStudio.Wizard.Controls
{
    partial class ApiCredentialsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApiCredentialsControl));
            this.OktaDomainTextBox = new System.Windows.Forms.TextBox();
            this.OktaDomainHelpPictureBox = new System.Windows.Forms.PictureBox();
            this.ApiTokenTextBox = new System.Windows.Forms.TextBox();
            this.ApiTokenHelpPictureBox = new System.Windows.Forms.PictureBox();
            this.OktaDomainLabel = new System.Windows.Forms.Label();
            this.OktaOrgApiTokenLabel = new System.Windows.Forms.Label();
            this.ApiTokenWarningLabel = new System.Windows.Forms.Label();
            this.OktaDomainWarningLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OktaDomainHelpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApiTokenHelpPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OktaDomainTextBox
            // 
            this.OktaDomainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OktaDomainTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OktaDomainTextBox.Location = new System.Drawing.Point(41, 44);
            this.OktaDomainTextBox.Name = "OktaDomainTextBox";
            this.OktaDomainTextBox.Size = new System.Drawing.Size(659, 29);
            this.OktaDomainTextBox.TabIndex = 1;
            this.OktaDomainTextBox.Tag = "OktaDomain";
            // 
            // OktaDomainHelpPictureBox
            // 
            this.OktaDomainHelpPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OktaDomainHelpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("OktaDomainHelpPictureBox.Image")));
            this.OktaDomainHelpPictureBox.ImageLocation = "";
            this.OktaDomainHelpPictureBox.InitialImage = null;
            this.OktaDomainHelpPictureBox.Location = new System.Drawing.Point(706, 48);
            this.OktaDomainHelpPictureBox.Name = "OktaDomainHelpPictureBox";
            this.OktaDomainHelpPictureBox.Size = new System.Drawing.Size(20, 20);
            this.OktaDomainHelpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OktaDomainHelpPictureBox.TabIndex = 1;
            this.OktaDomainHelpPictureBox.TabStop = false;
            // 
            // ApiTokenTextBox
            // 
            this.ApiTokenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiTokenTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApiTokenTextBox.Location = new System.Drawing.Point(41, 137);
            this.ApiTokenTextBox.Name = "ApiTokenTextBox";
            this.ApiTokenTextBox.Size = new System.Drawing.Size(659, 29);
            this.ApiTokenTextBox.TabIndex = 2;
            this.ApiTokenTextBox.Tag = "ApiToken";
            // 
            // ApiTokenHelpPictureBox
            // 
            this.ApiTokenHelpPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ApiTokenHelpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ApiTokenHelpPictureBox.Image")));
            this.ApiTokenHelpPictureBox.InitialImage = null;
            this.ApiTokenHelpPictureBox.Location = new System.Drawing.Point(706, 140);
            this.ApiTokenHelpPictureBox.Name = "ApiTokenHelpPictureBox";
            this.ApiTokenHelpPictureBox.Size = new System.Drawing.Size(20, 20);
            this.ApiTokenHelpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ApiTokenHelpPictureBox.TabIndex = 3;
            this.ApiTokenHelpPictureBox.TabStop = false;
            // 
            // OktaDomainLabel
            // 
            this.OktaDomainLabel.AutoSize = true;
            this.OktaDomainLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OktaDomainLabel.Location = new System.Drawing.Point(38, 11);
            this.OktaDomainLabel.Name = "OktaDomainLabel";
            this.OktaDomainLabel.Size = new System.Drawing.Size(77, 15);
            this.OktaDomainLabel.TabIndex = 999;
            this.OktaDomainLabel.Text = "Okta Domain";
            // 
            // OktaOrgApiTokenLabel
            // 
            this.OktaOrgApiTokenLabel.AutoSize = true;
            this.OktaOrgApiTokenLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OktaOrgApiTokenLabel.Location = new System.Drawing.Point(38, 109);
            this.OktaOrgApiTokenLabel.Name = "OktaOrgApiTokenLabel";
            this.OktaOrgApiTokenLabel.Size = new System.Drawing.Size(110, 15);
            this.OktaOrgApiTokenLabel.TabIndex = 999;
            this.OktaOrgApiTokenLabel.Text = "Okta Org Api Token";
            // 
            // ApiTokenWarningLabel
            // 
            this.ApiTokenWarningLabel.AutoSize = true;
            this.ApiTokenWarningLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApiTokenWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.ApiTokenWarningLabel.Location = new System.Drawing.Point(37, 169);
            this.ApiTokenWarningLabel.Name = "ApiTokenWarningLabel";
            this.ApiTokenWarningLabel.Size = new System.Drawing.Size(232, 21);
            this.ApiTokenWarningLabel.TabIndex = 1000;
            this.ApiTokenWarningLabel.Text = "Please enter your Org Api Token";
            this.ApiTokenWarningLabel.Visible = false;
            // 
            // OktaDomainWarningLabel
            // 
            this.OktaDomainWarningLabel.AutoSize = true;
            this.OktaDomainWarningLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OktaDomainWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.OktaDomainWarningLabel.Location = new System.Drawing.Point(37, 76);
            this.OktaDomainWarningLabel.Name = "OktaDomainWarningLabel";
            this.OktaDomainWarningLabel.Size = new System.Drawing.Size(226, 21);
            this.OktaDomainWarningLabel.TabIndex = 1001;
            this.OktaDomainWarningLabel.Text = "Please enter your Okta Domain";
            this.OktaDomainWarningLabel.Visible = false;
            // 
            // ApiCredentialsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OktaDomainWarningLabel);
            this.Controls.Add(this.ApiTokenWarningLabel);
            this.Controls.Add(this.OktaOrgApiTokenLabel);
            this.Controls.Add(this.OktaDomainLabel);
            this.Controls.Add(this.ApiTokenHelpPictureBox);
            this.Controls.Add(this.ApiTokenTextBox);
            this.Controls.Add(this.OktaDomainHelpPictureBox);
            this.Controls.Add(this.OktaDomainTextBox);
            this.Name = "ApiCredentialsControl";
            this.Size = new System.Drawing.Size(755, 214);
            ((System.ComponentModel.ISupportInitialize)(this.OktaDomainHelpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApiTokenHelpPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox OktaDomainTextBox;
        private System.Windows.Forms.PictureBox OktaDomainHelpPictureBox;
        private System.Windows.Forms.TextBox ApiTokenTextBox;
        private System.Windows.Forms.PictureBox ApiTokenHelpPictureBox;
        private System.Windows.Forms.Label OktaDomainLabel;
        private System.Windows.Forms.Label OktaOrgApiTokenLabel;
        private System.Windows.Forms.Label ApiTokenWarningLabel;
        private System.Windows.Forms.Label OktaDomainWarningLabel;
    }
}
