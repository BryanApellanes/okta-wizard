// <copyright file="AutoRegisterApplicationFormObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents application information for presentation.
    /// </summary>
    public class AutoRegisterApplicationFormObservable : Observable
    {
        /// <summary>
        /// Gets or sets the name of the selected Visual Studio template.
        /// </summary>
        /// <value>
        /// The name of the selected Visual Studio template.
        /// </value>
        public OktaApplicationType OktaApplicationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets whether the application is pre-existing.
        /// </summary>
        /// <value>
        /// Whether the application is pre-existing.
        /// </value>
        public bool ApplicationExists
        {
            get => GetProperty<bool>("ApplicationExists");
            set => SetProperty("ApplicationExists", value);
        }

        /// <summary>
        /// Gets or sets the organization Okta domain.
        /// </summary>
        /// <value>
        /// The organization Okta domain.
        /// </value>
        public string OktaDomain
        {
            get => GetProperty<string>("OktaDomain");
            set => SetProperty("OktaDomain", value);
        }

        /// <summary>
        /// Gets or sets the organization api token.
        /// </summary>
        /// <value>
        /// The organization API token.
        /// </value>
        public string ApiToken
        {
            get => GetProperty<string>("ApiToken");
            set => SetProperty("ApiToken", value);
        }

        /// <summary>
        /// Gets or sets the name of the application specified by the user.
        /// </summary>
        /// <value>
        /// The name of the application specified by the user.
        /// </value>
        public string ApplicationName
        {
            get => GetProperty<string>("ApplicationName");
            set => SetProperty("ApplicationName", value);
        }

        /// <summary>
        /// Gets or sets the text to display on the button.
        /// </summary>
        /// <value>
        /// The text to display on the button.
        /// </value>
        public string ButtonText
        {
            get => GetProperty<string>("ButtonText");
            set => SetProperty("ButtonText", value);
        }

        /// <summary>
        /// Gets or sets the observable for application credentials.
        /// </summary>
        /// <value>
        /// The observable for application credentials.
        /// </value>
        public ApplicationCredentialsObservable ApplicationCredentials
        {
            get;
            set;
        }

        /// <summary>
        /// Updates this Observable with the current values of all target bindings.
        /// </summary>
        public override void ReadInTargetValues()
        {
            base.ReadInTargetValues();
            ApplicationCredentials.ReadInTargetValues();
        }

        /// <summary>
        /// Copies the application name, client id and client secret to a new ApplicationCredentials instance.
        /// </summary>
        /// <returns>ApplicationCredentials</returns>
        public ApplicationCredentials ToApplicationCredentials()
        {
            return ApplicationCredentials.ToApplicationCredentials();
        }
    }
}
