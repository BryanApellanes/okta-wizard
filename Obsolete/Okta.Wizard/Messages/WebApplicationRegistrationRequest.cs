// <copyright file="WebApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Reprents an application registration request for a web application.
    /// </summary>
    public class WebApplicationRegistrationRequest : ApplicationRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public WebApplicationRegistrationRequest(string clientName, string clientUri, string logoUri = null)
            : base(clientName, clientUri, logoUri)
        {
            ClientName = clientName;
            ApplicationType = "web";
            RedirectUris = new string[]
            {
                "http://localhost:8080/authorization-code/callback",
                "https://localhost:8080/authorization-code/callback",
            };
            PostLogoutRedirectUris = new string[]
            {
                "http://localhost:8080",
                "https://localhost:8080",
            };
            ResponseTypes = new string[] { "code" };
            GrantTypes = new string[] { "authorization_code" };
            TokenEndpointAuthMethod = "client_secret_basic";
        }
    }
}
