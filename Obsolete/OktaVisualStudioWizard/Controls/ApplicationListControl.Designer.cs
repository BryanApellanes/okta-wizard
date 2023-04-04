namespace Okta.VisualStudio.Wizard.Controls
{
    partial class ApplicationListControl
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
            this.ApplicationNameComboBox = new System.Windows.Forms.ComboBox();
            this.RegisterApplicationButton = new System.Windows.Forms.Button();
            this.ApplicationNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplicationNameComboBox
            // 
            this.ApplicationNameComboBox.FormattingEnabled = true;
            this.ApplicationNameComboBox.Location = new System.Drawing.Point(105, 3);
            this.ApplicationNameComboBox.Name = "ApplicationNameComboBox";
            this.ApplicationNameComboBox.Size = new System.Drawing.Size(211, 21);
            this.ApplicationNameComboBox.TabIndex = 1;
            // 
            // RegisterApplicationButton
            // 
            this.RegisterApplicationButton.Enabled = false;
            this.RegisterApplicationButton.Location = new System.Drawing.Point(189, 29);
            this.RegisterApplicationButton.Name = "RegisterApplicationButton";
            this.RegisterApplicationButton.Size = new System.Drawing.Size(127, 23);
            this.RegisterApplicationButton.TabIndex = 2;
            this.RegisterApplicationButton.Text = "Register Application";
            this.RegisterApplicationButton.UseVisualStyleBackColor = true;
            // 
            // ApplicationNameLabel
            // 
            this.ApplicationNameLabel.AutoSize = true;
            this.ApplicationNameLabel.Location = new System.Drawing.Point(9, 6);
            this.ApplicationNameLabel.Name = "ApplicationNameLabel";
            this.ApplicationNameLabel.Size = new System.Drawing.Size(90, 13);
            this.ApplicationNameLabel.TabIndex = 4;
            this.ApplicationNameLabel.Text = "Application Name";
            // 
            // ApplicationListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ApplicationNameLabel);
            this.Controls.Add(this.RegisterApplicationButton);
            this.Controls.Add(this.ApplicationNameComboBox);
            this.MaximumSize = new System.Drawing.Size(0, 55);
            this.MinimumSize = new System.Drawing.Size(335, 55);
            this.Name = "ApplicationListControl";
            this.Size = new System.Drawing.Size(335, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox ApplicationNameComboBox;
        private System.Windows.Forms.Button RegisterApplicationButton;
        private System.Windows.Forms.Label ApplicationNameLabel;
    }
}
