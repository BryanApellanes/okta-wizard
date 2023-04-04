namespace Okta.VisualStudio.Wizard.Forms
{
    partial class NotificationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationForm));
            this.MessageLabel = new System.Windows.Forms.Label();
            this.StackTracePanel = new System.Windows.Forms.Panel();
            this.StackTraceTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.SeverityPictureBox = new System.Windows.Forms.PictureBox();
            this.StackTracePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SeverityPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(84, 13);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(93, 13);
            this.MessageLabel.TabIndex = 1;
            this.MessageLabel.Tag = "MessageText";
            this.MessageLabel.Text = "MESSAGE_TEXT";
            // 
            // StackTracePanel
            // 
            this.StackTracePanel.Controls.Add(this.StackTraceTextBox);
            this.StackTracePanel.Location = new System.Drawing.Point(13, 101);
            this.StackTracePanel.Name = "StackTracePanel";
            this.StackTracePanel.Size = new System.Drawing.Size(435, 100);
            this.StackTracePanel.TabIndex = 2;
            // 
            // StackTraceTextBox
            // 
            this.StackTraceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StackTraceTextBox.Location = new System.Drawing.Point(0, 0);
            this.StackTraceTextBox.Multiline = true;
            this.StackTraceTextBox.Name = "StackTraceTextBox";
            this.StackTraceTextBox.Size = new System.Drawing.Size(435, 100);
            this.StackTraceTextBox.TabIndex = 0;
            this.StackTraceTextBox.Tag = "StackTrace";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(373, 207);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // SeverityPictureBox
            // 
            this.SeverityPictureBox.Location = new System.Drawing.Point(13, 13);
            this.SeverityPictureBox.Name = "SeverityPictureBox";
            this.SeverityPictureBox.Size = new System.Drawing.Size(51, 46);
            this.SeverityPictureBox.TabIndex = 0;
            this.SeverityPictureBox.TabStop = false;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 280);
            this.ControlBox = false;
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.StackTracePanel);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.SeverityPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(490, 280);
            this.MinimumSize = new System.Drawing.Size(490, 280);
            this.Name = "NotificationForm";
            this.Text = "Okta";
            this.StackTracePanel.ResumeLayout(false);
            this.StackTracePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SeverityPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Panel StackTracePanel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.TextBox StackTraceTextBox;
        private System.Windows.Forms.PictureBox SeverityPictureBox;
    }
}