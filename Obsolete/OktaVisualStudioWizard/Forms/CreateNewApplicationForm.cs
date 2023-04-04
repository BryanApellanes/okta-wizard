// <copyright file="CreateNewApplicationForm.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TemplateWizard;
using Okta.VisualStudio.Wizard.Controls;
using Okta.Wizard;
using Okta.Wizard.Binding;

namespace Okta.VisualStudio.Wizard.Forms
{
    public partial class CreateNewApplicationForm : BindableForm, IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable>
    {
        public CreateNewApplicationForm()
        {
            InitializeComponent();
            UserSignInCredentialsControl.SignUpUrl = "https://developer.okta.com/signup/";
            UserSignInCredentialsControl.SignUpUriHelpUrl = "https://support.okta.com/help/s/sign-in-url";
            UserSignInCredentialsControl.SignUpLinkLabel.Click += (sender, args) => Process.Start(UserSignInCredentialsControl.SignUpUrl);
            UserSignInCredentialsControl.DontKnowYourSignInUrlLinkLabel.Click += (sender, args) => Process.Start(UserSignInCredentialsControl.SignUpUriHelpUrl);
            UserSignInCredentialsControl.ForgotPasswordLinkLabel.Click += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(UserSignInCredentialsControl.SignInUrl))
                {
                    try
                    {
                        string signInUrl = UserSignInCredentialsControl.SignInUrl;
                        if (!signInUrl.EndsWith("/"))
                        {
                            signInUrl += "/";
                        }

                        Uri uri = new Uri($"{signInUrl}signin/forgot-password");
                        Process.Start(uri.ToString());
                    }
                    catch
                    {
                        // don't crash
                    }
                }
            };
            UserSignInCredentialsControl.SignInUrlTextBox.Leave += (sender, args) => SetForgotPasswordLinkVisibility();
            UserSignInCredentialsControl.ModelBound += (sender, args) => SetForgotPasswordLinkVisibility();
        }

        public OktaWizard OktaWizard { get; set; }

        public string ApplicationName
        {
            get => ApplicationNameLabel.Text.Substring(1, ApplicationNameLabel.Text.Length - 2);
            set => SetControlProperty(ApplicationNameLabel, "Text", $"({value})");
        }

        public async Task<OktaApplicationSettings> PromptAsync(OktaApplicationSettingsObservable setupObservable)
        {
            return await PromptAsync((Observable)setupObservable);
        }

        public async Task<OktaApplicationSettings> PromptAsync(Observable observable)
        {
            if (!(observable is OktaApplicationSettingsObservable appSettingsObservable))
            {
                throw new ArgumentException($"Specified observable must be of type {nameof(ApiCredentialsObservable)}");
            }

            // - write usercredentials if they had to be specified or were updated

            ApplicationName = appSettingsObservable.ApplicationName;
            UserSignInCredentialsControl.BindModel(appSettingsObservable);

            if (!appSettingsObservable.ShowUserSignInCredentialsControl)
            {
                HideUserSignInCredentialsControl();
            }

            if (appSettingsObservable.OktaApplicationType != OktaApplicationType.None)
            {
                oktaApplicationTypeControl.Select(appSettingsObservable.OktaApplicationType);
                oktaApplicationTypeControl.Hide();
            }

            DialogResult dialogResult = ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                while (oktaApplicationTypeControl.SelectedOktaApplicationType == null)
                {
                    OktaApplicationTypeWarningLabel.Show();
                    dialogResult = ShowDialog();
                }

                OktaApplicationTypeWarningLabel.Hide();
                appSettingsObservable.ReadInTargetValues();

                OktaApplicationSettings settings = appSettingsObservable.ToOktaApplicationSettings();
                settings.ApplicationName = appSettingsObservable.ApplicationName;
                settings.OktaApplicationType = (OktaApplicationType)oktaApplicationTypeControl.SelectedOktaApplicationType?.OktaApplicationType;
                settings.UserSignInCredentials = UserSignInCredentialsControl.GetUserSignInCredentials();
                return settings;
            }

            if (dialogResult == DialogResult.Cancel)
            {
                throw new WizardCancelledException();
            }

            return new OktaApplicationSettings();
        }

        public void ShowUserSignInCredentialsControl()
        {
            SetUserSignInCredentialsControlVisibility(true);
        }

        public void HideUserSignInCredentialsControl()
        {
            SetUserSignInCredentialsControlVisibility(false);
        }

        protected virtual void SetUserSignInCredentialsControlVisibility(bool visible)
        {
            SetControlProperty(UserSignInCredentialsControl, nameof(UserSignInCredentialsControl.Visible), visible);
        }

        public void SetForgotPasswordLinkVisibility()
        {
            try
            {
                UserSignInCredentialsControl.CheckForgotPasswordLinkEnabled();
            }
            catch
            {
                // don't crash
            }
        }
    }
}
