// <copyright file="ApplicationRegistrationResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a response to a request to register a client application.
    /// </summary>
    public class ApplicationRegistrationResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the client id issued at.
        /// </summary>
        /// <value>
        /// The client id issued at.
        /// </value>
        [JsonProperty("client_id_issued_at")]
        public ulong ClientIdIssuedAt { get; set; }

        /// <summary>
        /// Gets or sets the client secret expires at.
        /// </summary>
        /// <value>
        /// The client secret expires at.
        /// </value>
        [JsonProperty("client_secret_expires_at")]
        public ulong ClientSecretExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>
        /// The client name.
        /// </value>
        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client uri.
        /// </summary>
        /// <value>
        /// The client uri.
        /// </value>
        [JsonProperty("client_uri")]
        public string ClientUri { get; set; }

        /// <summary>
        /// Gets or sets the logo uri.
        /// </summary>
        /// <value>
        /// The logo uri.
        /// </value>
        [JsonProperty("logo_uri")]
        public string LogoUri { get; set; }

        /// <summary>
        /// Gets or sets the redirect uris.
        /// </summary>
        /// <value>
        /// The redirect uris.
        /// </value>
        [JsonProperty("redirect_uris")]
        public string[] RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the response types.
        /// </summary>
        /// <value>
        /// The response types.
        /// </value>
        [JsonProperty("response_types")]
        public string[] ResponseTypes { get; set; }

        /// <summary>
        /// Gets or sets the grant types.
        /// </summary>
        /// <value>
        /// The grant types.
        /// </value>
        [JsonProperty("grant_types")]
        public string[] GrantTypes { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint auth method.
        /// </summary>
        /// <value>
        /// The token endpoint auth method.
        /// </value>
        [JsonProperty("token_endpoint_auth_method")]
        public string TokenEndpointAuthMethod { get; set; }

        /// <summary>
        /// Gets or sets the application type.
        /// </summary>
        /// <value>
        /// The application type.
        /// </value>
        [JsonProperty("application_type")]
        public string ApplicationType { get; set; }
    }
}
