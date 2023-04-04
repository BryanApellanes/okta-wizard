// <copyright file="ManagementApiApplicationRegistrationRequestShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard.Messages;

namespace OktaVisualStudioWizard.Tests.Integration
{
    [TestClass]
    public class ManagementApiApplicationRegistrationRequestShould
    {
        [TestMethod]
        public void SerializeCorrectly()
        {
            ManagementApiApplicationRegistrationRequest request = new ManagementApiApplicationRegistrationRequest($"{nameof(SerializeCorrectly)}_test", "My Native Test App");

            string actual = request.ToJson(true);
            string expected = File.ReadAllText("./Integration/test-application-registration-request.json");
            Assert.AreEqual(expected, actual);
        }
    }
}
