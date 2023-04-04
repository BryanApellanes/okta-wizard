// <copyright file="TestOktaWizard.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx;
using Microsoft.AspNetCore.DataProtection;
using Okta.Wizard;
using Okta.Wizard.Binding;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard.Tests.Integration
{
    public class TestOktaWizard: OktaWizard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestOktaWizard"/> class.
        /// </summary>
        /// <param name="oktaApplicationSettingsPromptProvider"></param>
        /// <param name="applicationCredentialsPromptProvider"></param>
        /// <param name="applicationRegistrationManager"></param>
        /// <param name="wizardRunFinisherResolver"></param>
        /// <param name="telemetryEventReporter"></param>
        /// <param name="dataProtectionProvider"></param>
        public TestOktaWizard(IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable> oktaApplicationSettingsPromptProvider, IPromptProvider<ApplicationCredentials> applicationCredentialsPromptProvider, IApplicationRegistrationManager applicationRegistrationManager, IWizardRunFinisherResolver wizardRunFinisherResolver, ITelemetryEventReporter telemetryEventReporter, IDataProtectionProvider dataProtectionProvider)
            : base(oktaApplicationSettingsPromptProvider, applicationCredentialsPromptProvider, applicationRegistrationManager, wizardRunFinisherResolver, telemetryEventReporter, dataProtectionProvider)
        {
        }

        public bool ApiCredentialsWorksReturnValue{ get; set; }

        public override bool ApiCredentialsWorks(ApiCredentials apiCredentials)
        {
            return ApiCredentialsWorksReturnValue;
        }

        public bool PromptForApplicationCredentialsAsyncWasCalled { get; set; }

        public ApplicationCredentials ApplicationCredentialsToUse{ get; set; }

        public override Task<ApplicationCredentials> PromptForApplicationCredentialsAsync(AutoRegisterApplicationFormObservable autoRegisterApplicationFormObservable)
        {
            PromptForApplicationCredentialsAsyncWasCalled = true;
            return Task.FromResult(ApplicationCredentialsToUse);
        }

        public bool PromptForOktaApplicationSettingsAsyncWasCalled { get; set; }

        public OktaApplicationSettings OktaApplicationSettingsToUse { get; set; }

        public override Task<OktaApplicationSettings> PromptForOktaApplicationSettingsAsync(OktaApplicationSettingsObservable oktaApplicationSettingsObservable)
        {
            PromptForOktaApplicationSettingsAsyncWasCalled = true;
            return Task.FromResult(OktaApplicationSettingsToUse);
        }

        public ExecuteApiKeyToolResult ExecuteApiKeyToolResultToUse { get; set; }

        public bool ExecuteApiKeyToolWasCalled { get; set; }

        /// <summary>
        /// Executes base.ExecuteApiKeyTool unless ApiKeyToUse has a value.
        /// </summary>
        /// <param name="projectData"></param>
        /// <returns></returns>
        public override ExecuteApiKeyToolResult ExecuteApiKeyTool(ProjectData projectData)
        {
            ExecuteApiKeyToolWasCalled = true;
            if (ExecuteApiKeyToolResultToUse != null)
            {
                return ExecuteApiKeyToolResultToUse;
            }

            return base.ExecuteApiKeyTool(projectData);
        }

        public UserSignInCredentials UserSignInCredentialsToUse { get; set; }

        public override UserSignInCredentials TryLoadLocalUserSignInCredentials(string filePath = null)
        {
            if(UserSignInCredentialsToUse != null)
            {
                return UserSignInCredentialsToUse;
            }

            return base.TryLoadLocalUserSignInCredentials();
        }

        public OktaApiToken OktaApiTokenToUse { get; set; }
        
        public bool TryLoadOktaApiTokenWasCalled { get; set; }
        
        public int TryLoadOktaApiTokenCallCount{ get; set; }

        public override OktaApiToken TryLoadOktaApiToken(string filePath = null)
        {
            ++TryLoadOktaApiTokenCallCount;
            if(OktaApiTokenToUse != null)
            {
                return OktaApiTokenToUse;
            }

            return base.TryLoadOktaApiToken();
        }
    }
}
