// <copyright file="UserSignInCredentialsControl.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Wizard;

namespace Okta.VisualStudio.Wizard.Controls
{
    public partial class UserSignInCredentialsControl : OktaUserControl
    {
        public UserSignInCredentialsControl()
        {
            InitializeComponent();
            ForgotPasswordToolTip.SetToolTip(HelpPictureBox, "To reset your password, your Sign-in URL is required.");
        }

        public string SignUpUrl{ get; set; }

        public string SignUpUriHelpUrl{ get; set; }

        public string ForgotPasswordUrl { get; set; }

        public string SignInUrl
        {
            get => SignInUrlTextBox.Text;
            set => SetProperty(SignInUrlTextBox, "Text", value);
        }

        public string UserName
        {
            get => UserNameTextBox.Text;
            set => SetProperty(UserNameTextBox, "Text", value);
        }

        public string Password
        {
            get => PasswordTextBox.Text;
            set => SetProperty(PasswordTextBox, "Text", value);
        }

        public void CheckForgotPasswordLinkEnabled()
        {
            if (!string.IsNullOrEmpty(SignInUrl))
            {
                SetProperty(ForgotPasswordLinkLabel, nameof(ForgotPasswordLinkLabel.Enabled), true);
            }
            else
            {
                SetProperty(ForgotPasswordLinkLabel, nameof(ForgotPasswordLinkLabel.Enabled), false);
            }
        }

        public UserSignInCredentials GetUserSignInCredentials()
        {
            return new UserSignInCredentials
            {
                SignInUrl = SignInUrl,
                UserName = UserName,
                Password = Password,
            };
        }
    }
}
