namespace Okta.VisualStudio.Wizard.Controls
{
    partial class ApplicationControl
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
            this.ApplicationLabel = new System.Windows.Forms.Label();
            this.ApplicationNameTextBox = new System.Windows.Forms.TextBox();
            this.RegisterApplicationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ApplicationLabel
            // 
            this.ApplicationLabel.AutoSize = true;
            this.ApplicationLabel.Location = new System.Drawing.Point(13, 6);
            this.ApplicationLabel.Name = "ApplicationLabel";
            this.ApplicationLabel.Size = new System.Drawing.Size(90, 13);
            this.ApplicationLabel.TabIndex = 0;
            this.ApplicationLabel.Tag = "ApplicationName";
            this.ApplicationLabel.Text = "Application Name";
            // 
            // ApplicationNameTextBox
            // 
            this.ApplicationNameTextBox.Location = new System.Drawing.Point(109, 3);
            this.ApplicationNameTextBox.Name = "ApplicationNameTextBox";
            this.ApplicationNameTextBox.Size = new System.Drawing.Size(211, 20);
            this.ApplicationNameTextBox.TabIndex = 1;
            // 
            // RegisterApplicationButton
            // 
            this.RegisterApplicationButton.Enabled = false;
            this.RegisterApplicationButton.Location = new System.Drawing.Point(193, 29);
            this.RegisterApplicationButton.Name = "RegisterApplicationButton";
            this.RegisterApplicationButton.Size = new System.Drawing.Size(127, 23);
            this.RegisterApplicationButton.TabIndex = 3;
            this.RegisterApplicationButton.Text = "Register Application";
            this.RegisterApplicationButton.UseVisualStyleBackColor = true;
            // 
            // ApplicationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RegisterApplicationButton);
            this.Controls.Add(this.ApplicationNameTextBox);
            this.Controls.Add(this.ApplicationLabel);
            this.MinimumSize = new System.Drawing.Size(335, 55);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(335, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ApplicationLabel;
        private System.Windows.Forms.TextBox ApplicationNameTextBox;
        public System.Windows.Forms.Button RegisterApplicationButton;
    }
}
