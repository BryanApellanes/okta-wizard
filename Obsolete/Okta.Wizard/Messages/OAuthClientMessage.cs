// <copyright file="OAuthClientMessage.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents data passed to the api to update application settings.
    /// </summary>
    public class OAuthClientMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthClientMessage"/> class.
        /// </summary>
        public OAuthClientMessage()
        {
            AutoKeyRotation = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to auto rotate keys.
        /// </summary>
        /// <value>
        /// The auto key rotation setting.
        /// </value>
        [JsonProperty("autoKeyRotation")]
        public bool AutoKeyRotation { get; set; }

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
