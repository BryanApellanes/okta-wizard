// <copyright file="OktaWizardResult.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using DevEx.Internal;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the result of running the wizard.
    /// </summary>
    public class OktaWizardResult : Serializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OktaWizardResult"/> class.
        /// </summary>
        public OktaWizardResult()
        {
            OktaApplicationSettings = new OktaApplicationSettings();
            ShouldCreateUser = true;
        }

        /// <summary>
        /// Gets or sets the anme of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the api credentials.
        /// </summary>
        /// <value>
        /// The api credentials.
        /// </value>
        public ApiCredentials ApiCredentials
        {
            get
            {
                return OktaApplicationSettings?.ApiCredentials;
            }

            set
            {
                OktaApplicationSettings.ApiCredentials = value;
            }
        }

        /// <summary>
        /// Gets or sets the Okta application settings.
        /// </summary>
        /// <value>
        /// The Okta application settings.
        /// </value>
        public OktaApplicationSettings OktaApplicationSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the application credentials.
        /// </summary>
        /// <value>
        /// The application credentials.
        /// </value>
        public ApplicationCredentials ApplicationCredentials
        {
            get
            {
                return OktaApplicationSettings.ApplicationCredentials;
            }

            set
            {
                OktaApplicationSettings.ApplicationCredentials = value;
            }
        }

        /// <summary>
        /// Gets or sets the exception if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception if any.  May be null.
        /// </value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public WizardStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the wizard configuration.
        /// </summary>
        /// <value>
        /// The wizard configuration.
        /// </value>
        public OktaWizardConfig OktaWizardConfig { get; set; }

        /// <summary>
        /// Gets or sets the project data.
        /// </summary>
        /// <value>
        /// The project data.
        /// </value>
        public ProjectData ProjectData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a user should be created.
        /// </summary>
        /// <value>
        /// A value indicating whether a user should be created.
        /// </value>
        public bool ShouldCreateUser { get; set; }

        /// <summary>
        /// Gets or sets the replacements dictionary.
        /// </summary>
        /// <value>
        /// The replacements dictionary.
        /// </value>
        public Dictionary<string, string> Replacements { get; set; }

        /// <summary>
        /// Sets replacement values into the specified replacements.
        /// </summary>
        /// <param name="replacements">The replacements.</param>
        public void SetReplacements(Dictionary<string, string> replacements)
        {
            foreach (string key in Replacements?.Keys)
            {
                if (replacements.ContainsKey(key))
                {
                    replacements[key] = Replacements[key];
                }
                else
                {
                    replacements.Add(key, Replacements[key]);
                }
            }
        }

        /// <summary>
        /// Add Okta specific keys and values to the specified dictionary.
        /// </summary>
        /// <param name="replacements">The replacements.</param>
        public void SetOktaReplacements(Dictionary<string, string> replacements)
        {
            Uri oktaUri = OktaWizardConfig.GetDomainUri();
            string oktaDomain = oktaUri.Authority;
            AddKeyValue("{yourOktaDomain}", oktaDomain, replacements);
            AddKeyValue("{ClientId}", ApplicationCredentials.ClientId, replacements);
            AddKeyValue("{ClientSecret}", ApplicationCredentials.ClientSecret, replacements);
            AddKeyValue("{ApplicationName}", ApplicationName, replacements);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the specified application already exists.  If the application already
        /// exists then a test user is not created for the application.
        /// </summary>
        /// <value>
        /// A value that indicates whether the specified application already exists.  If the application already
        /// exists then a test user is not created for the application.
        /// </value>
        public bool ApplicationExists { get => ApplicationCredentials.ApplicationExists; set => ApplicationCredentials.ApplicationExists = value; }

        /// <summary>
        /// Gets the Okta application type.
        /// </summary>
        /// <returns>OktaApplicationType</returns>
        public OktaApplicationType GetOktaApplicationType()
        {
            return ProjectData.GetOktaApplicationType();
        }

        /// <summary>
        /// Gets project template parameters.
        /// </summary>
        /// <returns>ProjectTemplateParameters</returns>
        public ProjectTemplateParameters GetProjectTemplateParameters()
        {
            return GetProjectTemplateParameters(OktaApplicationSettings.OktaApplicationType);
        }

        /// <summary>
        /// Gets project template parameters.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <returns>ProjectTemplateParameters</returns>
        public ProjectTemplateParameters GetProjectTemplateParameters(OktaApplicationType oktaApplicationType)
        {
            return ProjectTemplateParameters.GetProjectTemplateParameters(ProjectData, oktaApplicationType);
        }

        private void AddKeyValue(string key, string value, Dictionary<string, string> replacements)
        {
            if (replacements.ContainsKey(key))
            {
                replacements[key] = value;
            }
            else
            {
                replacements.Add(key, value);
            }
        }
    }
}
