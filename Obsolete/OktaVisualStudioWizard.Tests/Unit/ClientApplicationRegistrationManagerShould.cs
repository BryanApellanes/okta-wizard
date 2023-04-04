// <copyright file="ClientApplicationRegistrationManagerShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;

namespace OktaVisualStudioWizard.Tests.Unit
{
    public class TestApplicationRegistrationManager: ClientApplicationRegistrationManager
    {
        public TestApplicationRegistrationManager(ApiCredentials creds) : base(creds) { }

        public string CallGetPath()
        {
            return base.GetPath();
        }
    }

    [TestClass]
    public class ClientApplicationRegistrationManagerShould
    {
        [TestMethod]
        public void GetSecurePath()
        {
            TestApplicationRegistrationManager mgr = new TestApplicationRegistrationManager(new ApiCredentials { Domain = "http://dev-xxx.domain.com" });
            Uri path = new Uri(mgr.CallGetPath());
            path.Scheme.Should().Be("https");
            path.ToString().Should().Be("https://dev-xxx.domain.com/oauth2/v1/clients");
        }
    }
}
