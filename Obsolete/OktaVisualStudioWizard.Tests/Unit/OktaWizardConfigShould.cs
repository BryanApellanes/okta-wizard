// <copyright file="OktaWizardConfigShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using DevEx.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;
using Okta.Wizard.Internal;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class OktaWizardConfigShould
    {
        [TestMethod]
        public void HashCorrectly()
        {
            OktaWizardConfig configOne = new OktaWizardConfig
            {
                TelemetryServiceRoot = 6.RandomCharacters(),
                OktaDomain = 8.RandomCharacters(),
                OktaApiToken = 16.RandomCharacters()
            };

            OktaWizardConfig configTwo = Deserialize.FromJson<OktaWizardConfig>(configOne.ToJson());

            HashSet<OktaWizardConfig> configs = new HashSet<OktaWizardConfig>
            {
                configOne
            };

            Assert.IsFalse(configOne == configTwo);
            Assert.IsTrue(configs.Contains(configTwo));
            Assert.IsTrue(configOne.GetHashCode() == configTwo.GetHashCode());
        }

        [TestMethod]
        public void BeEqual()
        {
            OktaWizardConfig configOne = new OktaWizardConfig
            {
                TelemetryServiceRoot = 6.RandomCharacters(),
                OktaDomain = 8.RandomCharacters(),
                OktaApiToken = 16.RandomCharacters()
            };

            OktaWizardConfig configTwo = Deserialize.FromJson<OktaWizardConfig>(configOne.ToJson());

            Assert.IsTrue(configOne.Equals(configTwo));
        }

        [TestMethod]
        public void NotBeEqual()
        {
            OktaWizardConfig configOne = new OktaWizardConfig
            {
                TelemetryServiceRoot = 6.RandomCharacters(),
                OktaDomain = 8.RandomCharacters(),
                OktaApiToken = 16.RandomCharacters()
            };

            OktaWizardConfig configTwo = new OktaWizardConfig
            {
                TelemetryServiceRoot = 6.RandomCharacters(),
                OktaDomain = 8.RandomCharacters(),
                OktaApiToken = 16.RandomCharacters()
            };

            Assert.IsFalse(configOne.Equals(configTwo));
        }
    }
}
