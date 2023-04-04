using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Okta.Wizard.Messages;
using Microsoft.VisualStudio.Threading;
using Okta.VisualStudio.Wizard.Controls;
using Okta.VisualStudio.Wizard.Forms;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class ApplicationControl : OktaUserControl
    {
        public ApplicationControl()
        {
            InitializeComponent();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            RegisterApplicationButton.Click += (s, a) => RegisterApplicationAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            ApplicationNameTextBox.KeyUp += (s, a) =>
            {
                if (!string.IsNullOrEmpty(ApplicationNameTextBox.Text?.Trim()))
                {
                    EnableRegisterApplicationButton();
                }
                else
                {
                    DisableRegisterApplicationButton();
                }
            };
        }

        private bool enableRegistration;

        public bool EnableRegistration
        {
            get => enableRegistration;
            set
            {
                enableRegistration = value;
                RegisterApplicationButton.SetControlProperty("Visible", enableRegistration);
            }
        }

        public event EventHandler RegisteringApplication;
        public event EventHandler RegisteredApplication;
        public event EventHandler RegisterApplicationException;

        public async Task RegisterApplicationAsync()
        {
            RegisteringApplication?.Invoke(this, new ApplicationRegistrationEventArgs());
            try
            {
                if (string.IsNullOrEmpty(ApplicationName))
                {
                    throw new ArgumentException("ApplicationName not specified");
                }
                ApplicationRegistrationResponse response = await RegistrationManager.RegisterApplicationAsync(ApplicationName);
                RegisteredApplication?.Invoke(this, new ApplicationRegistrationEventArgs { Response = response });
            }
            catch (Exception ex)
            {
                RegisterApplicationException?.Invoke(this, new ApplicationRegistrationEventArgs { Exception = ex });
            }
        }

        private IApplicationRegistrationManager registrationManager;

        public IApplicationRegistrationManager RegistrationManager
        {
            get => registrationManager ?? ApplicationRegistrationManager.Default;
            set => registrationManager = value;
        }

        public string ApplicationName
        {
            get { return ApplicationNameTextBox.Text; }
            set { SetText(ApplicationNameTextBox, value); }
        }

        private void DisableRegisterApplicationButton()
        {
            SetEnabled(RegisterApplicationButton, false);
        }

        private void EnableRegisterApplicationButton()
        {
            SetEnabled(RegisterApplicationButton, true);
        }
    }
}
