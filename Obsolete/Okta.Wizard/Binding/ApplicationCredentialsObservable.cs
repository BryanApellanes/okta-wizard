// <copyright file="ApplicationCredentialsObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents application credentials for presentation.
    /// </summary>
    public class ApplicationCredentialsObservable : Observable
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        public string ApplicationName
        {
            get => GetProperty<string>("ApplicationName");
            set => SetProperty("ApplicationName", value);
        }

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public string ClientId
        {
            get => GetProperty<string>("ClientId");
            set => SetProperty("ClientId", value);
        }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret
        {
            get => GetProperty<string>("ClientSecret");
            set => SetProperty("ClientSecret", value);
        }

        /// <summary>
        /// Copies the application name, client id and client secret to a new ApplicationCredentials instance.
        /// </summary>
        /// <returns>ApplicationCredentials</returns>
        public ApplicationCredentials ToApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationName = ApplicationName,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
            };
        }
    }
}
