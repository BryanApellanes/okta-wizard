using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Okta.Wizard;
using System.IO;
using Okta.VisualStudio.Wizard.Forms;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class ApiCredentialsControl : OktaUserControl
    {
        public ApiCredentialsControl()
        {
            InitializeComponent();

            OktaDomainHelpUri = new Uri("https://developer.okta.com/docs/guides/find-your-domain/-/findorg/");
            ApiTokenHelpUri = new Uri("https://developer.okta.com/docs/guides/create-an-api-token/create-the-token/");
            OktaDomainHelpClick += (s, a) => OpenOktaDomainHelpUri();
            ApiTokenHelpClick += (s, a) => OpenApiTokenHelpUri();
            OktaDomainHelpPictureBox.Click += OktaDomainHelpClick.Invoke;
            ApiTokenHelpPictureBox.Click += ApiTokenHelpClick.Invoke;
            OktaDomainTextBox.KeyUp += (s, a) =>
            {
                ConditionallyShowDomainWarning();
            };
            ApiTokenTextBox.KeyUp += (s, a) =>
            {
                ConditionallyShowApiTokenWarning();
            };
        }

        public bool HasWarnings()
        {
            bool domainMissing = ConditionallyShowDomainWarning();
            bool tokenMissing = ConditionallyShowApiTokenWarning();

            return domainMissing || tokenMissing;
        }

        private bool ConditionallyShowApiTokenWarning()
        {
            bool result = string.IsNullOrEmpty(ApiTokenTextBox.Text);
            SetVisible(ApiTokenWarningLabel, result);
            return result;
        }

        private bool ConditionallyShowDomainWarning()
        {
            bool result = string.IsNullOrEmpty(OktaDomainTextBox.Text);
            SetVisible(OktaDomainWarningLabel, result);
            return result;
        }

        public event EventHandler OktaDomainHelpClick;
        public event EventHandler ApiTokenHelpClick;

        /// <summary>
        /// Gets or sets the Uri to open when the OktaDomain help link is clicked.
        /// </summary>
        public Uri OktaDomainHelpUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Uri to open when the ApiToken help link is clicked.
        /// </summary>
        public Uri ApiTokenHelpUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the OktaDomain.
        /// </summary>
        public string OktaDomain
        {
            get => OktaDomainTextBox.Text;
            set
            {
                SetProperty(OktaDomainTextBox, "Text", value);
            }
        }

        /// <summary>
        /// Gets or sets the ApiToken.
        /// </summary>
        public string ApiToken
        {
            get => ApiTokenTextBox.Text;
            set
            {
                SetProperty(ApiTokenTextBox, "Text", value);
            }
        }

        /// <summary>
        /// Returns true if both OktaDomain and ApiToken properties are set.
        /// </summary>
        public bool IsConfigured
        {
            get
            {
                return !string.IsNullOrEmpty(OktaDomain) && !string.IsNullOrEmpty(ApiToken);
            }
        }

        protected void OpenOktaDomainHelpUri()
        {
            Process.Start(OktaDomainHelpUri.ToString());
        }

        protected void OpenApiTokenHelpUri()
        {
            Process.Start(ApiTokenHelpUri.ToString());
        }
    }
}
