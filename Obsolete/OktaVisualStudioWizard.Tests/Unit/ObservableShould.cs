// <copyright file="ObservableShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.VisualStudio.Wizard.Controls;
using Okta.Wizard.Binding;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class ObservableShould
    {
        [TestMethod]
        public void BindToControl()
        {
            string oktaDomain = "TestDomain";
            string apiToken = "TestApiToken";

            ApiCredentialsControl setupControl = new ApiCredentialsControl();
            ApiCredentialsObservable setupObservable = new ApiCredentialsObservable();
            setupControl.OktaDomain.Should().BeNullOrEmpty();
            setupControl.ApiToken.Should().BeNullOrEmpty();

            setupControl.BindModel(setupObservable);

            setupControl.OktaDomain.Should().BeNullOrEmpty();
            setupControl.ApiToken.Should().BeNullOrEmpty();

            setupObservable.OktaDomain = oktaDomain;
            setupObservable.ApiToken = apiToken;

            setupControl.OktaDomain.Should().Be(oktaDomain);
            setupControl.ApiToken.Should().Be(apiToken);
        }
    }
}
