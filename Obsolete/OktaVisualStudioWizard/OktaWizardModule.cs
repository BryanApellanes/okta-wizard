// <copyright file="OktaWizardModule.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using DevEx;
using Microsoft.AspNetCore.DataProtection;
using Ninject;
using Ninject.Modules;
using Okta.VisualStudio.Wizard.Forms;
using Okta.Wizard;
using Okta.Wizard.Binding;
using Okta.Wizard.Telemetry;

namespace Okta.VisualStudio.Wizard
{
    /// <summary>
    /// A dependency injection container for Okta wizard
    /// </summary>
    public class OktaWizardModule : NinjectModule
    {
        private readonly OktaWizardConfig oktaWizardConfig = OktaWizardConfig.Default;
        private CreateNewApplicationForm createNewApplicationForm;
        private ApplicationRegistrationManager applicationRegistrationManager;
        private AutoRegisterApplicationForm autoRegisterApplicationForm;

        /// <inheritdoc/>
        public override void Load()
        {
            IDataProtectionProvider dataProtectionProvider = OktaWizardConfig.GetDefaultDataProtectionProvider();
            this.Bind<IDataProtectionProvider>().ToConstant(dataProtectionProvider);

            createNewApplicationForm = new CreateNewApplicationForm();

            applicationRegistrationManager = new OktaApplicationTypeApplicationRegistrationManager();

            autoRegisterApplicationForm = new AutoRegisterApplicationForm(applicationRegistrationManager);

            this.Bind<IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable>>().ToConstant(createNewApplicationForm);
            this.Bind<IPromptProvider<ApplicationCredentials>>().ToConstant(autoRegisterApplicationForm);
            this.Bind<IAsyncTelemetryService>().ToConstant(new TelemetryClient(oktaWizardConfig.TelemetryServiceRoot));
            this.Bind<ITelemetryEventReporter>().To<TelemetryEventReporter>();
            this.Bind<IApplicationRegistrationManager>().ToConstant(applicationRegistrationManager);
            this.Bind<IUserManager>().ToConstant(new UserManager(applicationRegistrationManager));
            this.Bind<IWizardRunFinisherResolver>().To<WizardRunFinisherResolver>();
        }

        /// <summary>
        /// Tries to get an OktaWizard instance.
        /// </summary>
        /// <returns>OktaWizard</returns>
        public OktaWizard TryGetOktaWizard()
        {
            try
            {
                return GetOktaWizard();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string stackTrace = ex.StackTrace;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                if (!ex.Message.Equals(message))
                {
                    message += $"\r\nInner Exception: {ex.Message}";
                }

                if (!ex.StackTrace.Equals(stackTrace))
                {
                    stackTrace += $"\r\nInner Exception Stack Trace: {ex.StackTrace}";
                }

                OktaWizard.Log($"{message}\r\n\r\n{stackTrace}");
                return null;
            }
        }

        /// <summary>
        /// Gets an OktaWizard instance.
        /// </summary>
        /// <returns>OktaWizard</returns>
        public OktaWizard GetOktaWizard()
        {
            OktaWizardModule oktaWizardModule = new OktaWizardModule();
            IKernel kernel = new StandardKernel(oktaWizardModule);
            OktaWizard oktaWizard = kernel.Get<OktaWizard>();
            oktaWizard = kernel.Get<OktaWizard>();
            oktaWizardModule.autoRegisterApplicationForm.SetupClick = () => oktaWizard.PromptForOktaApplicationSettings(oktaWizard.GetOktaApplicationSettingsObservable());  // TODO: review this to determine a better place for this code
            return oktaWizard;
        }
    }
}
