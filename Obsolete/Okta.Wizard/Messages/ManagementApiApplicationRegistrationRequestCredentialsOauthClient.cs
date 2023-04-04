// <copyright file="ManagementApiApplicationRegistrationRequestCredentialsOauthClient.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents the oauth client property of the management API application registration request.
    /// </summary>
    public class ManagementApiApplicationRegistrationRequestCredentialsOauthClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagementApiApplicationRegistrationRequestCredentialsOauthClient"/> class.
        /// </summary>
        public ManagementApiApplicationRegistrationRequestCredentialsOauthClient()
        {
            AutoKeyRotation = true;
            TokenEndpointAuthMethod = "none";
        }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically rotate keys.
        /// </summary>
        /// <value>
        /// A value indication whether to automatically rotate keys.
        /// </value>
        [JsonProperty("autoKeyRotation")]
        public bool AutoKeyRotation { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        [JsonProperty("client_id")] // for deserialization
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint auth method.
        /// </summary>
        /// <value>
        /// The token endpoint auth method.
        /// </value>
        [JsonProperty("token_endpoint_auth_method")]
        public string TokenEndpointAuthMethod { get; set; }
    }
}
