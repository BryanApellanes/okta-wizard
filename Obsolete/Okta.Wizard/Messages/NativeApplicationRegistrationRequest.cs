// <copyright file="NativeApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Reprents an application registration request for a native application.
    /// </summary>
    public class NativeApplicationRegistrationRequest : ApplicationRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public NativeApplicationRegistrationRequest(string clientName, string clientUri, string logoUri = null)
            : base(clientName, clientUri, logoUri)
        {
            ClientName = clientName;
            ApplicationType = "native";
            RedirectUris = new string[]
            {
                "my.app.login:/callback",
                "com.okta.xamarin.ios.login:/callback",
            };
            PostLogoutRedirectUris = new string[]
            {
                "my.app.logout:/callback",
                "com.okta.xamarin.ios.logout:/callback",
            };
            ResponseTypes = new string[] { "code" };
            GrantTypes = new string[] { "authorization_code" };
            TokenEndpointAuthMethod = "none";
        }
    }
}
