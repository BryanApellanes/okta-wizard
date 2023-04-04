// <copyright file="SinglePageApplicationApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Reprents an application registration request for a single page application.
    /// </summary>
    public class SinglePageApplicationApplicationRegistrationRequest : ApplicationRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePageApplicationApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public SinglePageApplicationApplicationRegistrationRequest(string clientName, string clientUri, string logoUri = null)
            : base(clientName, clientUri, logoUri)
        {
            ClientName = clientName;
            ApplicationType = "browser";
            RedirectUris = new string[]
            {
                "http://localhost:8080/implicit/callback",
                "https://localhost:8080/implicit/callback",
            };
            PostLogoutRedirectUris = new string[]
            {
                "http://localhost:8080",
                "https://localhost:8080",
            };
            ResponseTypes = new string[] { "code" };
            GrantTypes = new string[] { "authorization_code" };
            TokenEndpointAuthMethod = "none";
        }
    }
}
