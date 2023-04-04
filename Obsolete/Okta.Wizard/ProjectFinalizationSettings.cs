// <copyright file="ProjectFinalizationSettings.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to finalizing project initialization.
    /// </summary>
    public class ProjectFinalizationSettings : Serializable
    {
        /// <summary>
        /// Gets or sets project data.
        /// </summary>
        /// <value>
        /// Project data.
        /// </value>
        public ProjectData ProjectData { get; set; }

        /// <summary>
        /// Gets or sets Okta application settings.
        /// </summary>
        /// <value>
        /// Okta application settings.
        /// </value>
        public OktaApplicationSettings OktaApplicationSettings { get; set; }

        /// <summary>
        /// Ensures that property values are consistent.
        /// </summary>
        public void ResolveValues()
        {
            OktaApplicationSettings.VsTemplateName = ProjectData.GetSelectedVsTemplateName();
            OktaApplicationSettings.ApplicationName = ProjectData.ProjectName;
        }
    }
}
