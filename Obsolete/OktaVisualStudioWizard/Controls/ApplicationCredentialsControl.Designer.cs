namespace Okta.VisualStudio.Wizard.Controls
{
    partial class ApplicationCredentialsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationCredentialsControl));
            this.ClientSecretWarningLabel = new System.Windows.Forms.Label();
            this.ApplicationNameLabel = new System.Windows.Forms.Label();
            this.HelpPictureBox = new System.Windows.Forms.PictureBox();
            this.ClientSecretLabel = new System.Windows.Forms.Label();
            this.ClientIdLabel = new System.Windows.Forms.Label();
            this.LoadingPictureBox = new System.Windows.Forms.PictureBox();
            this.ClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.ClientIdWarningLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ClientSecretWarningLabel
            // 
            this.ClientSecretWarningLabel.AutoSize = true;
            this.ClientSecretWarningLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSecretWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.ClientSecretWarningLabel.Location = new System.Drawing.Point(77, 233);
            this.ClientSecretWarningLabel.Name = "ClientSecretWarningLabel";
            this.ClientSecretWarningLabel.Size = new System.Drawing.Size(180, 21);
            this.ClientSecretWarningLabel.TabIndex = 8;
            this.ClientSecretWarningLabel.Text = "Please enter client secret";
            this.ClientSecretWarningLabel.Visible = false;
            // 
            // ApplicationNameLabel
            // 
            this.ApplicationNameLabel.AutoSize = true;
            this.ApplicationNameLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationNameLabel.Location = new System.Drawing.Point(17, 18);
            this.ApplicationNameLabel.Name = "ApplicationNameLabel";
            this.ApplicationNameLabel.Size = new System.Drawing.Size(233, 32);
            this.ApplicationNameLabel.TabIndex = 7;
            this.ApplicationNameLabel.Tag = "ApplicationName";
            this.ApplicationNameLabel.Text = "APPLICATION_NAME";
            // 
            // HelpPictureBox
            // 
            this.HelpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("HelpPictureBox.Image")));
            this.HelpPictureBox.InitialImage = null;
            this.HelpPictureBox.Location = new System.Drawing.Point(732, 155);
            this.HelpPictureBox.Name = "HelpPictureBox";
            this.HelpPictureBox.Size = new System.Drawing.Size(20, 20);
            this.HelpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.HelpPictureBox.TabIndex = 6;
            this.HelpPictureBox.TabStop = false;
            // 
            // ClientSecretLabel
            // 
            this.ClientSecretLabel.AutoSize = true;
            this.ClientSecretLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSecretLabel.Location = new System.Drawing.Point(78, 183);
            this.ClientSecretLabel.Name = "ClientSecretLabel";
            this.ClientSecretLabel.Size = new System.Drawing.Size(73, 15);
            this.ClientSecretLabel.TabIndex = 5;
            this.ClientSecretLabel.Text = "Client Secret";
            // 
            // ClientIdLabel
            // 
            this.ClientIdLabel.AutoSize = true;
            this.ClientIdLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientIdLabel.Location = new System.Drawing.Point(78, 101);
            this.ClientIdLabel.Name = "ClientIdLabel";
            this.ClientIdLabel.Size = new System.Drawing.Size(51, 15);
            this.ClientIdLabel.TabIndex = 4;
            this.ClientIdLabel.Text = "Client Id";
            // 
            // LoadingPictureBox
            // 
            this.LoadingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LoadingPictureBox.Image")));
            this.LoadingPictureBox.InitialImage = null;
            this.LoadingPictureBox.Location = new System.Drawing.Point(732, 155);
            this.LoadingPictureBox.Name = "LoadingPictureBox";
            this.LoadingPictureBox.Size = new System.Drawing.Size(22, 20);
            this.LoadingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LoadingPictureBox.TabIndex = 2;
            this.LoadingPictureBox.TabStop = false;
            this.LoadingPictureBox.Visible = false;
            // 
            // ClientSecretTextBox
            // 
            this.ClientSecretTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSecretTextBox.Location = new System.Drawing.Point(80, 201);
            this.ClientSecretTextBox.Name = "ClientSecretTextBox";
            this.ClientSecretTextBox.Size = new System.Drawing.Size(639, 29);
            this.ClientSecretTextBox.TabIndex = 0;
            this.ClientSecretTextBox.Tag = "ClientSecret";
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientIdTextBox.Location = new System.Drawing.Point(80, 119);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(639, 29);
            this.ClientIdTextBox.TabIndex = 99;
            this.ClientIdTextBox.Tag = "ClientId";
            // 
            // ClientIdWarningLabel
            // 
            this.ClientIdWarningLabel.AutoSize = true;
            this.ClientIdWarningLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientIdWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.ClientIdWarningLabel.Location = new System.Drawing.Point(77, 151);
            this.ClientIdWarningLabel.Name = "ClientIdWarningLabel";
            this.ClientIdWarningLabel.Size = new System.Drawing.Size(152, 21);
            this.ClientIdWarningLabel.TabIndex = 100;
            this.ClientIdWarningLabel.Text = "Please enter client id";
            this.ClientIdWarningLabel.Visible = false;
            // 
            // ApplicationCredentialsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClientIdWarningLabel);
            this.Controls.Add(this.ClientSecretWarningLabel);
            this.Controls.Add(this.ApplicationNameLabel);
            this.Controls.Add(this.HelpPictureBox);
            this.Controls.Add(this.ClientSecretLabel);
            this.Controls.Add(this.ClientIdLabel);
            this.Controls.Add(this.LoadingPictureBox);
            this.Controls.Add(this.ClientSecretTextBox);
            this.Controls.Add(this.ClientIdTextBox);
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ApplicationCredentialsControl";
            this.Size = new System.Drawing.Size(800, 400);
            ((System.ComponentModel.ISupportInitialize)(this.HelpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ClientIdTextBox;
        private System.Windows.Forms.TextBox ClientSecretTextBox;
        private System.Windows.Forms.PictureBox LoadingPictureBox;
        private System.Windows.Forms.Label ClientIdLabel;
        private System.Windows.Forms.Label ClientSecretLabel;
        private System.Windows.Forms.PictureBox HelpPictureBox;
        private System.Windows.Forms.Label ApplicationNameLabel;
        private System.Windows.Forms.Label ClientSecretWarningLabel;
        private System.Windows.Forms.Label ClientIdWarningLabel;
    }
}
