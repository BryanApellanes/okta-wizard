namespace Okta.VisualStudio.Wizard.Forms
{
    partial class TestUserForm
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
            this.StackTraceTextBox = new System.Windows.Forms.TextBox();
            this.TestUserCredentialsTextBox = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StackTraceTextBox
            // 
            this.StackTraceTextBox.BackColor = System.Drawing.Color.White;
            this.StackTraceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StackTraceTextBox.Location = new System.Drawing.Point(0, 0);
            this.StackTraceTextBox.Multiline = true;
            this.StackTraceTextBox.Name = "StackTraceTextBox";
            this.StackTraceTextBox.Size = new System.Drawing.Size(800, 450);
            this.StackTraceTextBox.TabIndex = 1;
            this.StackTraceTextBox.Tag = "StackTrace";
            // 
            // TestUserCredentialsTextBox
            // 
            this.TestUserCredentialsTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestUserCredentialsTextBox.Location = new System.Drawing.Point(36, 129);
            this.TestUserCredentialsTextBox.Multiline = true;
            this.TestUserCredentialsTextBox.Name = "TestUserCredentialsTextBox";
            this.TestUserCredentialsTextBox.Size = new System.Drawing.Size(686, 184);
            this.TestUserCredentialsTextBox.TabIndex = 2;
            // 
            // MessageLabel
            // 
            this.MessageLabel.BackColor = System.Drawing.Color.White;
            this.MessageLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(32, 51);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(563, 58);
            this.MessageLabel.TabIndex = 3;
            this.MessageLabel.Text = "A test user was created for you to test your new Okta application.  To verify you" +
    "r application, run it and use these credentials to log in.";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButton.Location = new System.Drawing.Point(587, 362);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(175, 32);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // TestUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.TestUserCredentialsTextBox);
            this.Controls.Add(this.StackTraceTextBox);
            this.Name = "TestUserForm";
            this.Text = "TestUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StackTraceTextBox;
        private System.Windows.Forms.TextBox TestUserCredentialsTextBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button OkButton;
    }
}