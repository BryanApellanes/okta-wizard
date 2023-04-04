// <copyright file="ServiceApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Reprents an application registration request for a service application.
    /// </summary>
    public class ServiceApplicationRegistrationRequest : ApplicationRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public ServiceApplicationRegistrationRequest(string clientName, string clientUri, string logoUri = null)
            : base(clientName, clientUri, logoUri)
        {
            ClientName = clientName;
            ApplicationType = "service";
            RedirectUris = new string[] { };
            PostLogoutRedirectUris = new string[] { };
            ResponseTypes = new string[] { "token" };
            GrantTypes = new string[] { "client_credentials" };
            TokenEndpointAuthMethod = "client_secret_basic";
        }
    }
}
