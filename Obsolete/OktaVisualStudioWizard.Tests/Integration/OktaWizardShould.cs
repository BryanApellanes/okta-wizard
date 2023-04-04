// <copyright file="OktaWizardShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Okta.VisualStudio.Wizard;
using Okta.Wizard;
using NSubstitute;
using Okta.Wizard.Binding;
using DevEx;
using Microsoft.AspNetCore.DataProtection;
using Okta.Wizard.Internal;

namespace OktaVisualStudioWizard.Tests.Integration
{
    [TestClass]
    public class OktaWizardShould
    {
        public const string TestProjectDataFile = "./TestProjectData.json";

        [TestMethod]
        public void NotPromptForInputIfOktaApiTokenExistsAndApplicationTypeIsKnown()
        {
            // if we have an OktaApiToken that works
            // and we know the OktaApplicationType we can skip the OktaApplicationSettings prompt
            TestOktaWizard oktaWizard = GetOktaWizard();
            oktaWizard.ApiCredentialsWorksReturnValue = true;
            oktaWizard.OktaApplicationSettingsToUse = new OktaApplicationSettings { ApiCredentials = new ApiCredentials { Domain = "test.domain", Token = "test.token" } };
            TestProjectData testProjectData = Deserialize.FromJsonFile<TestProjectData>(TestProjectDataFile);
            testProjectData.OktaApplicationType = OktaApplicationType.Native;

            oktaWizard.RunAsync(testProjectData).Wait();

            oktaWizard.PromptForOktaApplicationSettingsAsyncWasCalled.Should().BeFalse();
        }

        [TestMethod]
        public void RunApiKeyToolIfUserCredentialsExistsAndApplicationTypeIsKnown()
        {
            TestOktaWizard oktaWizard = GetOktaWizard();
            oktaWizard.ApiCredentialsWorksReturnValue = true;
            oktaWizard.OktaApplicationSettingsToUse = new OktaApplicationSettings { ApiCredentials = new ApiCredentials { Domain = "test.domain", Token = "test.token" } };
            TestProjectData testProjectData = Deserialize.FromJsonFile<TestProjectData>(TestProjectDataFile);
            testProjectData.OktaApplicationType = OktaApplicationType.Native;

            oktaWizard.RunAsync(testProjectData).Wait();
            oktaWizard.ExecuteApiKeyToolWasCalled.Should().BeTrue();
        }

        [TestMethod]
        public void TryToLoadApiTokenTwiceIfUserCredentialsExistsWithoutApiToken()
        {
            TestOktaWizard oktaWizard = GetOktaWizard();
            oktaWizard.ApiCredentialsWorksReturnValue = true;
            oktaWizard.OktaApplicationSettingsToUse = new OktaApplicationSettings { ApiCredentials = new ApiCredentials { Domain = "test.domain", Token = "test.token" } };
            TestProjectData testProjectData = Deserialize.FromJsonFile<TestProjectData>(TestProjectDataFile);
            testProjectData.OktaApplicationType = OktaApplicationType.Native;

            oktaWizard.RunAsync(testProjectData).Wait();
            oktaWizard.TryLoadOktaApiTokenCallCount.Should().Be(2);
        }

        [TestMethod]
        public void ExecuteApiKeyTool()
        {
            TestOktaWizard oktaWizard = GetOktaWizard();
            TestProjectData testProjectData = Deserialize.FromJsonFile<TestProjectData>(TestProjectDataFile);
            oktaWizard.ExecuteApiKeyTool(testProjectData);
        }

        private TestOktaWizard GetOktaWizard()
        {
            IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable> oktaApplicationSettingsPromptProvider = Substitute.For<IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable>>();
            IPromptProvider<ApplicationCredentials> applicationCredentialsPromptProvider = Substitute.For<IPromptProvider<ApplicationCredentials>>();
            IWizardRunFinisherResolver wizardRunFinisherResolver = Substitute.For<IWizardRunFinisherResolver>();
            ITelemetryEventReporter telemetryEventReporter = Substitute.For<ITelemetryEventReporter>();
            IDataProtectionProvider dataProtectionProvider = Substitute.For<IDataProtectionProvider>();
            IApplicationRegistrationManager applicationRegistrationManager = Substitute.For<IApplicationRegistrationManager>();

            TestOktaWizard oktaWizard = new TestOktaWizard(oktaApplicationSettingsPromptProvider, applicationCredentialsPromptProvider, applicationRegistrationManager, wizardRunFinisherResolver, telemetryEventReporter, dataProtectionProvider)
            {
                Notify = (msg, s) => Console.WriteLine(msg),
            };
            return oktaWizard;
        }
    }
}
