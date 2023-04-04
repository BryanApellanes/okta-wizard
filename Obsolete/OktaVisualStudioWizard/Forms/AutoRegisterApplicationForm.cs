// <copyright file="AutoRegisterApplicationForm.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Wizard;
using Okta.Wizard.Binding;
using Okta.Wizard.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okta.VisualStudio.Wizard.Forms
{
    public partial class AutoRegisterApplicationForm : FlatBindableForm, IPromptProvider<ApplicationCredentials>
    {
        private readonly IApplicationRegistrationManager applicationRegistrationManager;
        public AutoRegisterApplicationForm()
        {
            InitializeComponent();
            SetupPictureBox.Click += (s, a) => SetupClick?.Invoke();
            this.applicationRegistrationManager = new ClientApplicationRegistrationManager();
            WireRegistrationListeners();
        }

        public AutoRegisterApplicationForm(IApplicationRegistrationManager applicationRegistrationManager)
        {
            InitializeComponent();
            SetupPictureBox.Click += (s, a) => SetupClick?.Invoke();
            this.applicationRegistrationManager = applicationRegistrationManager;
            WireRegistrationListeners();
        }

        /// <summary>
        /// The action executed when the configure setup icon is clicked.
        /// </summary>
        public Action SetupClick { get; set; }

        public OktaWizard OktaWizard{ get; set; }

        public void SetOktaWizard(OktaWizard oktaWizard)
        {
            OktaWizard = oktaWizard;
        }

        public void Bind(AutoRegisterApplicationFormObservable observable)
        {
            BindObservable(observable);
            BindTaggedControl(observable.ApplicationCredentials, "ApplicationCredentials");
        }

        public string OktaDomain
        {
            get => AutoRegisterApplicationOktaDomainToolStripStatusLabel.Text;
            set => SetObjectProperty(AutoRegisterApplicationOktaDomainToolStripStatusLabel, "Text", value);
        }

        public string ApiToken
        {
            get => AutoRegisterApplicationApiTokenToolStripStatusLabel.Text;
            set => SetObjectProperty(AutoRegisterApplicationApiTokenToolStripStatusLabel, "Text", value);
        }

        public string ApplicationName
        {
            get => ApplicationCredentialsControl.ApplicationName;
            set => SetControlProperty(ApplicationCredentialsControl, "ApplicationName", value);
        }

        public string ButtonText
        {
            get => OkButton.Text;
            set => SetControlProperty(OkButton, "Text", value);
        }

        public void EnableButton()
        {
            SetControlProperty(OkButton, "Enabled", true);
        }

        public void DisableButton()
        {
            SetControlProperty(OkButton, "Enabled", false);
        }

        public async Task<ApplicationCredentials> PromptAsync(ApplicationCredentialsObservable observable)
        {
            return await PromptAsync((Observable)observable);
        }

        private AutoRegisterApplicationFormObservable formObservable;

        public async Task ShowAsync()
        {
            await Task.Run(() => Show());
        }

        public async Task<ApplicationCredentials> PromptAsync(Observable observable)
        {
            if (!(observable is AutoRegisterApplicationFormObservable autoRegisterApplicationFormObservable))
            {
                throw new ArgumentException($"Specified observable must be of type {nameof(AutoRegisterApplicationFormObservable)}");
            }
            if (string.IsNullOrEmpty(autoRegisterApplicationFormObservable.OktaDomain))
            {
                throw new ArgumentNullException($"The {nameof(AutoRegisterApplicationFormObservable.OktaDomain)} property of the specified AutoRegisterApplicationFormObservable must be set.");
            }
            if (string.IsNullOrEmpty(autoRegisterApplicationFormObservable.ApiToken))
            {
                throw new ArgumentNullException($"The {nameof(AutoRegisterApplicationFormObservable.OktaDomain)} property of the specified AutoRegisterApplicationFormObservable must be set.");
            }
            applicationRegistrationManager.ApiCredentials = new ApiCredentials { Domain = autoRegisterApplicationFormObservable.OktaDomain, Token = autoRegisterApplicationFormObservable.ApiToken };
            this.formObservable = autoRegisterApplicationFormObservable;
            Bind(autoRegisterApplicationFormObservable);
            
            SetObjectProperty(this, "OktaDomain", autoRegisterApplicationFormObservable.OktaDomain);
            SetObjectProperty(this, "ApiToken", autoRegisterApplicationFormObservable.ApiToken);            

            ApplicationCredentialsControl.BindModel(autoRegisterApplicationFormObservable);
            ApplicationCredentialsControl.AddBinding(autoRegisterApplicationFormObservable, "ClientId");
            ApplicationCredentialsControl.AddBinding(autoRegisterApplicationFormObservable, "ClientSecret");
            string applicationName = autoRegisterApplicationFormObservable.ApplicationName;
            OktaApplicationType oktaApplicationType = autoRegisterApplicationFormObservable.OktaApplicationType;

            ButtonText = "Registering ...";
            Thread.Sleep(300);

            Task<ClientApplication> autoRegisterTask = AutoRegisterApplicationAsync(autoRegisterApplicationFormObservable);
            ShowDialog();
            autoRegisterApplicationFormObservable.ReadInTargetValues();
            autoRegisterApplicationFormObservable.ApplicationExists = (bool)(await autoRegisterTask)?.Exists;
            return autoRegisterApplicationFormObservable.ApplicationCredentials?.ToApplicationCredentials();
        }

        protected async Task<ClientApplication> AutoRegisterApplicationAsync(AutoRegisterApplicationFormObservable settings)
        {
            oktaApplicationType = settings.OktaApplicationType;
            ApplicationCredentialsControl.SetOktaApplicationType(oktaApplicationType);
            string applicationName = settings.ApplicationName;
            ApplicationCredentialsControl.HideClientSecretWarningLabel();
            ApplicationCredentialsControl.ShowLoading();
            if (await OktaWizard.ApplicationExistsAsync(applicationName))
            {
                ClientApplication clientApplication = await OktaWizard.GetClientApplicationAsync(applicationName);
                clientApplication.Exists = true;
                if (oktaApplicationType != OktaApplicationType.Native && oktaApplicationType != OktaApplicationType.SinglePageApplication)
                {
                    OktaWizard.Notify($"{applicationName}\r\n\r\nThe specified application already exists, please manually enter the 'Client Secret'.  Click the help icon for additional assistance.", Severity.Warning);
                    FocusOnControlTaggedWith("ClientSecret");
                }

                Thread.Sleep(300);
                return clientApplication;
            }

            string initiateLoginUri = ApplicationRegistrationRequest.GetInitiateLoginUri(oktaApplicationType, settings.OktaDomain);
            ApplicationRegistrationResponse response = await applicationRegistrationManager.RegisterApplicationAsync(oktaApplicationType, applicationName, null, null, initiateLoginUri);
            return new ClientApplication(response);
        }

        private OktaApplicationType oktaApplicationType;

        protected void WireRegistrationListeners()
        {
            OkButton.Click += (s, a) =>
            {
                DialogResult = DialogResult.OK;
                formObservable.ReadInTargetValues();
                Close();
            };
            applicationRegistrationManager.RetrievedApplication += (s, a) =>
            {
                ClientApplicationRetrievalEventArgs care = (ClientApplicationRetrievalEventArgs)a;
                ApplicationCredentialsControl.ClientId = care.ClientApplication.ClientId;
                ApplicationCredentialsControl.ClientSecret = care.ClientApplication.ClientSecret;
                if (oktaApplicationType != OktaApplicationType.Native && oktaApplicationType != OktaApplicationType.SinglePageApplication)
                {
                    ApplicationCredentialsControl.ShowClientSecret();
                    ApplicationCredentialsControl.HideLoading();
                    ButtonText = "OK";
                    EnableButton();
                    FocusOnControlTaggedWith("ClientSecret");
                    Thread.Sleep(300);
                    Update();
                    Thread.Sleep(1500);
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    formObservable.ReadInTargetValues();
                    Thread.Sleep(300);
                    Update();
                    Thread.Sleep(1500);
                    Close();
                }
            };
            applicationRegistrationManager.RegisteredApplication += (s, a) =>
            {
                ButtonText = "OK";
                ApplicationCredentialsControl.HideLoading();
                ApplicationCredentialsControl.HideClientSecretWarningLabel();
                ClientApplication clientApplication = ((ApplicationRegistrationEventArgs)a).ClientApplication;
                SetControlProperty(ApplicationCredentialsControl, "ClientId", clientApplication.ClientId);
                SetControlProperty(ApplicationCredentialsControl, "ClientSecret", clientApplication.ClientSecret);
                DialogResult = DialogResult.OK;
                formObservable.ReadInTargetValues();
                Thread.Sleep(300);
                Update();
                Thread.Sleep(1500);
                Close();
            };
        }
    }
}
