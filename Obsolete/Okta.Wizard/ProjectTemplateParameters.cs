// <copyright file="ProjectTemplateParameters.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data required by Visual Studio to add a project from a named template.
    /// </summary>
    public class ProjectTemplateParameters
    {
        /// <summary>
        /// Gets or sets the path to the visual studio template file (*.vstemplate).
        /// </summary>
        /// <value>
        /// The path to the visual studio template file (*.vstemplate).
        /// </value>
        public string VsTemplateFilePath { get; set; }

        /// <summary>
        /// Gets or sets the destination folder.
        /// </summary>
        /// <value>
        /// The destination folder.
        /// </value>
        public string DestinationFolder { get; set; }

        /// <summary>
        /// Gets or sets the name of the destination project.
        /// </summary>
        /// <value>
        /// The name of the destination project.
        /// </value>
        public string DestinationProjectName { get; set; }

        public static ProjectTemplateParameters GetProjectTemplateParameters(ProjectData projectData, OktaApplicationType oktaApplicationType)
        {
            return new ProjectTemplateParameters
            {
                VsTemplateFilePath = projectData.GetTemplateFile(oktaApplicationType).FullName,
                DestinationFolder = projectData.DestinationDirectory,
                DestinationProjectName = projectData.ProjectName,
            };
        }
    }
}
