using Okta.Wizard.Binding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okta.VisualStudio.Wizard.Forms
{
    public partial class NotificationForm : BindableForm
    {
        public NotificationForm()
        {
            InitializeComponent();
        }

        private delegate void SetMessageDelegate(string message);
        protected void SetMessage(string message)
        {
            if (MessageLabel.InvokeRequired)
            {
                SetMessageDelegate smd = new SetMessageDelegate(SetMessage);
                smd.Invoke(message);
            }
            else
            {
                MessageLabel.Text = message;
            }
        }

        private delegate void SetTitleDelegate(string title);
        protected void SetTitle(string title)
        {
            if (InvokeRequired)
            {
                SetTitleDelegate std = new SetTitleDelegate(SetTitle);
                std.Invoke(title);
            }
            else
            {
                Text = title;
            }
        }

        private delegate void SetStackTraceDelegate(string stackTrace);
        protected void SetStackTrace(string stackTrace)
        {
            if (StackTraceTextBox.InvokeRequired)
            {
                SetStackTraceDelegate sstd = new SetStackTraceDelegate(SetStackTrace);
                sstd.Invoke(stackTrace);
            }
            else
            {
                StackTraceTextBox.Text = stackTrace;
            }
        }
            
        public static DialogResult Notify(string message, string stackTrace = "", string title = "Okta")
        {
            NotificationForm window = new NotificationForm();
            window.SetMessage(message);
            window.SetStackTrace(stackTrace);
            window.SetTitle(title);
            return window.ShowDialog();
        }
    }
}
