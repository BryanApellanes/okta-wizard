// <copyright file="OktaApplicationSettings.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents settings used to create an Okta application.
    /// </summary>
    public class OktaApplicationSettings
    {
        private ApiCredentials apiCredentials;

        /// <summary>
        /// Gets or sets the API credentials.
        /// </summary>
        /// <value>
        /// The API credentials.
        /// </value>
        public ApiCredentials ApiCredentials
        {
            get
            {
                if (apiCredentials == null)
                {
                    apiCredentials = Deserialize.FromEnvironmentVariables<ApiCredentials>();
                }

                return apiCredentials;
            }

            set
            {
                apiCredentials = value;
            }
        }

        /// <summary>
        /// Gets or sets the user sign in credentials.
        /// </summary>
        /// <value>
        /// The user sign in credentials.
        /// </value>
        public UserSignInCredentials UserSignInCredentials { get; set; }

        /// <summary>
        /// Gets or sets the application credentials.
        /// </summary>
        /// <value>
        /// The application credentials.
        /// </value>
        public ApplicationCredentials ApplicationCredentials { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the name of the Visual Studio template.
        /// </summary>
        /// <value>
        /// The name of the Visual Studio template.
        /// </value>
        public string VsTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the path in the file system where the Visual Studio extension is installed.
        /// </summary>
        /// <value>
        /// The path in the file system where the Visual Studio extension is installed.
        /// </value>
        public string ExtensionTemplatesPath { get; set; }

        /// <summary>
        /// Gets or sets the Okta application type.
        /// </summary>
        /// <value>
        /// The Okta application type.
        /// </value>
        public OktaApplicationType OktaApplicationType { get; set; }

        /// <summary>
        /// Gets or sets information about the git repository, may be null.
        /// </summary>
        /// <value>
        /// Information about the git repository, may be null.
        /// </value>
        public ProjectFinalizationRepositoryDescriptor ProjectInitializationRepository { get; set; } // TODO: review for removal
    }
}
