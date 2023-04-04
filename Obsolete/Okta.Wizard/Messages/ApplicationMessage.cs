// <copyright file="ApplicationMessage.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a base class for client application messages.
    /// </summary>
    public class ApplicationMessage : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMessage"/> class.
        /// </summary>
        public ApplicationMessage()
        {
            ApplicationType = "web";
            ResponseTypes = new string[] { "code", "id_token" };
            GrantTypes = new string[] { "implicit", "authorization_code", "refresh_token" };
            TokenEndpointAuthMethod = "client_secret_post";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMessage"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public ApplicationMessage(string clientName, string clientUri = null, string logoUri = null)
            : this()
        {
            ClientName = clientName;
            ClientUri = clientUri ?? GetClientUri() ?? "https://localhost:8080";
            if (!ClientUri.EndsWith("/"))
            {
                ClientUri += "/";
            }

            LogoUri = logoUri ?? $"{ClientUri}logo.png";
            Uri uri = new Uri(ClientUri);
            RedirectUris = new string[]
            {
                $"http://{uri.Host}:5000/authorization-code/callback",
                $"https://{uri.Host}:5001/authorization-code/callback",
                $"https://{uri.Host}:8081/authorization-code/callback",
                $"https://{uri.Host}:44314/authorization-code/callback",
            };
            PostLogoutRedirectUris = new string[]
            {
                $"http://{uri.Host}:5000/SignOut",
                $"https://{uri.Host}:5001/SignOut",
                $"https://{uri.Host}:5001/signout/callback",
                $"https://{uri.Host}:44314/signout/callback",
            };
            InitiateLoginUri = $"{ClientUri}login";
        }

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
        /// Gets or sets the application type.
        /// </summary>
        /// <value>
        /// The application type.
        /// </value>
        [JsonProperty("application_type")]
        public string ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets the redirect uris.
        /// </summary>
        /// <value>
        /// The redirect uris.
        /// </value>
        [JsonProperty("redirect_uris")]
        public string[] RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the post logout redirect uris.
        /// </summary>
        /// <value>
        /// The post logout redirect uris.
        /// </value>
        [JsonProperty("post_logout_redirect_uris")]
        public string[] PostLogoutRedirectUris { get; set; }

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
        /// Gets or sets the initiate login uri.
        /// </summary>
        /// <value>
        /// The initiate login uri.
        /// </value>
        [JsonProperty("initiate_login_uri")]
        public string InitiateLoginUri { get; set; }

        /// <summary>
        /// Gets or sets the token endpoing method.
        /// </summary>
        /// <value>
        /// The token endpoing method.
        /// </value>
        [JsonProperty("token_endpoint_auth_method")]
        public string TokenEndpointAuthMethod { get; set; }

        /// <summary>
        /// Gets the client uri making sure that it uses https.
        /// </summary>
        /// <param name="clientUri">The client URI.</param>
        /// <returns>string or null if an invalid client URI is specified.</returns>
        protected string GetClientUri(string clientUri = null)
        {
            try
            {
                clientUri = clientUri ?? ClientUri;
                if (string.IsNullOrEmpty(clientUri))
                {
                    return null;
                }

                Uri clientUrl = new Uri(clientUri);
                if (!clientUrl.Scheme.Equals("https"))
                {
                    clientUrl = new Uri($"https://{clientUrl.Authority}");
                }

                return clientUrl.ToString();
            }
            catch
            {
            }

            return null;
        }
    }
}
