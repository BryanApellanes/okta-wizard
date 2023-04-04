// <copyright file="ManagementApiApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a request to register a client application using the management API.
    /// </summary>
    public class ManagementApiApplicationRegistrationRequest : Serializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagementApiApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="message">The message.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public ManagementApiApplicationRegistrationRequest(string label, string message = null, string clientUri = null, string logoUri = null)
        {
            this.Name = "oidc_client";
            this.Label = label;
            this.SignOnMode = "OPENID_CONNECT";
            this.Credentials = new ManagementApiApplicationRegistrationRequestCredentials();
            this.Settings = new ManagementApiApplicationRegistrationRequestSettings(
                ApplicationRegistrationRequest.GetRedirectUrisFor(OktaApplicationType.Native),
                ApplicationRegistrationRequest.GetPostLogoutRedirectUrisFor(OktaApplicationType.Native),
                message,
                null,
                clientUri,
                logoUri);
        }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the application label.
        /// </summary>
        /// <value>
        /// The application label.
        /// </value>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the sign on mode.
        /// </summary>
        /// <value>
        /// The sign on mode.
        /// </value>
        [JsonProperty("signOnMode")]
        public string SignOnMode { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        [JsonProperty("credentials")]
        public ManagementApiApplicationRegistrationRequestCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        [JsonProperty("settings")]
        public ManagementApiApplicationRegistrationRequestSettings Settings { get; set; }
    }
}
