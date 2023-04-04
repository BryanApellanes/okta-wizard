// <copyright file="ProjectInitializationSettingsShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class ProjectInitializationSettingsShould
    {
        [TestMethod]
        public void ResolveValues()
        {
            string testAppProjectName = $"{nameof(ResolveValues)}_testProjectAppName";
            string testPath = "c:\\TestPath\\Template\\test.vstemplate";
            ProjectFinalizationSettings settings = new ProjectFinalizationSettings
            {
                OktaApplicationSettings = new OktaApplicationSettings
                {
                },
                ProjectData = new ProjectData
                {
                    ProjectName = testAppProjectName,
                    CustomParams = new object[] {testPath},
                },
            };
            Assert.IsNull(settings.OktaApplicationSettings.VsTemplateName);
            Assert.IsNull(settings.OktaApplicationSettings.ApplicationName);
            settings.ResolveValues();
            Assert.AreEqual("Template", settings.OktaApplicationSettings.VsTemplateName);
            Assert.AreEqual(testAppProjectName, settings.OktaApplicationSettings.ApplicationName);
        }
    }
}
