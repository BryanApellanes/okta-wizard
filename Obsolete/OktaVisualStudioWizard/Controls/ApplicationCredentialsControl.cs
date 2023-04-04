// <copyright file="ApplicationCredentialsControl.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Diagnostics;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class ApplicationCredentialsControl : OktaUserControl
    {
        public ApplicationCredentialsControl()
        {
            InitializeComponent();
            HelpUri = new Uri("https://developer.okta.com/docs/guides/find-your-app-credentials/findcreds/");
            HelpPictureBox.Click += (s, a) => OpenHelp();
            ClientSecretTextBox.KeyUp += (s, a) =>
            {
                if (!string.IsNullOrEmpty(ClientSecretTextBox.Text))
                {
                    HideClientSecretWarningLabel();
                }
                else
                {
                    ShowClientSecret();
                }
            };
            ClientIdTextBox.KeyUp += (s, a) =>
            {
                if (!string.IsNullOrEmpty(ClientIdTextBox.Text))
                {
                    HideClientIdWarningLabel();
                }
                else
                {
                    ShowClientIdWarningLabel();
                }
            };
        }

        public Uri HelpUri { get; set; }

        public string ApplicationName
        {
            get => ApplicationNameLabel.Text;
            set => SetText(ApplicationNameLabel, value);
        }

        public string ClientId
        {
            get => ClientIdTextBox.Text;
            set => SetText(ClientIdTextBox, value);
        }

        public string ClientSecret
        {
            get => ClientSecretTextBox.Text;
            set => SetText(ClientSecretTextBox, value);
        }

        public void ShowLoading()
        {
            SetVisible(HelpPictureBox, false);
            SetVisible(LoadingPictureBox, true);
        }

        public void HideLoading()
        {
            SetVisible(HelpPictureBox, true);
            SetVisible(LoadingPictureBox, false);
        }

        public void HideClientSecretWarningLabel()
        {
            SetVisible(ClientSecretWarningLabel, false);
        }

        public void ShowClientIdWarningLabel()
        {
            SetVisible(ClientIdWarningLabel, true);
        }

        public void HideClientIdWarningLabel()
        {
            SetVisible(ClientIdWarningLabel, false);
        }

        public void HideClientSecret()
        {
            HideClientSecretWarningLabel();
            SetVisible(ClientSecretLabel, false);
            SetVisible(ClientSecretTextBox, false);
        }

        public void ShowClientSecret()
        {
            SetVisible(ClientSecretWarningLabel, true);
            SetVisible(ClientSecretLabel, true);
            SetVisible(ClientSecretTextBox, true);
        }

        public void SetOktaApplicationType(OktaApplicationType oktaApplicationType)
        {
            switch (oktaApplicationType)
            {
                case OktaApplicationType.None:
                    break;
                case OktaApplicationType.Native:
                    HideClientSecret();
                    break;
                case OktaApplicationType.SinglePageApplication:
                    HideClientSecret();
                    break;
                case OktaApplicationType.Web:
                    break;
                case OktaApplicationType.Service:
                    break;
                case OktaApplicationType.Repository:
                    break;
                default:
                    break;
            }
        }

        protected void OpenHelp()
        {
            Process.Start(HelpUri.ToString());
        }
    }
}
