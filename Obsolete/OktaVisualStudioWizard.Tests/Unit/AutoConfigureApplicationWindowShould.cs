// <copyright file="AutoConfigureApplicationWindowShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.VisualStudio.Wizard.Forms;
using Okta.Wizard.Binding;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class AutoConfigureApplicationWindowShould
    {
        [TestMethod]
        public void BindToTarget()
        {
            string testApplication = "TestApplicationName";
            AutoRegisterApplicationForm autoConfigureApplicationWindow = new AutoRegisterApplicationForm();
            autoConfigureApplicationWindow.ApplicationName.Should().Be("APPLICATION_NAME"); // The default value set by the constructor
            autoConfigureApplicationWindow.ApplicationCredentialsControl.Should().NotBeNull();

            autoConfigureApplicationWindow.ApplicationCredentialsControl.ClientId.Should().BeNullOrEmpty();
            autoConfigureApplicationWindow.ApplicationCredentialsControl.ClientSecret.Should().BeNullOrEmpty();

            ApplicationCredentialsObservable applicationCredentialsObservable = new ApplicationCredentialsObservable();
            applicationCredentialsObservable.ApplicationName.Should().BeNullOrEmpty();

            applicationCredentialsObservable.AddBindings(autoConfigureApplicationWindow);
            autoConfigureApplicationWindow.ApplicationName = testApplication;
            applicationCredentialsObservable.ReadInTargetValues();

            applicationCredentialsObservable.ApplicationName.Should().Be(testApplication);
        }
    }
}
