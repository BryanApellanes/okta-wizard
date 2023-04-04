// <copyright file="ManagementApiApplicationRegistrationRequestSettings.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents the settings property of the management API application registration request.
    /// </summary>
    public class ManagementApiApplicationRegistrationRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagementApiApplicationRegistrationRequestSettings"/> class.
        /// </summary>
        /// <param name="redirectUris">The redirect URIs.</param>
        /// <param name="postLogoutRedirectUris">The post logout redirect URIs.</param>
        /// <param name="message">The message.</param>
        /// <param name="helpUrl">The help URL.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public ManagementApiApplicationRegistrationRequestSettings(string[] redirectUris, string[] postLogoutRedirectUris, string message = null, string helpUrl = null, string clientUri = null, string logoUri = null)
        {
            this.App = new object();
            this.Notifications = new
            {
                vpn = new
                {
                    network = new
                    {
                        connection = "DISABLED",
                    },
                    message,
                    helpUrl,
                },
            };
            this.OAuthClient = new
            {
                client_uri = clientUri,
                logo_uri = logoUri,
                redirect_uris = redirectUris ?? new string[] { },
                post_logout_redirect_uris = postLogoutRedirectUris ?? new string[] { },
                response_types = new string[] { "code" },
                grant_types = new string[] { "authorization_code" },
                application_type = "native",
                consent_method = "REQUIRED",
                issuer_mode = "ORG_URL",
                idp_initiated_login = new
                {
                    mode = "DISABLED",
                    default_scope = new object[] { },
                },
            };
        }

        /// <summary>
        /// Gets or sets the app.
        /// </summary>
        /// <value>
        /// The app.
        /// </value>
        [JsonProperty("app")]
        public object App { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        [JsonProperty("notifications")]
        public object Notifications { get; set; }

        /// <summary>
        /// Gets or sets the oauth client.
        /// </summary>
        /// <value>
        /// The oauth client.
        /// </value>
        [JsonProperty("oauthClient")]
        public object OAuthClient { get; set; }
    }
}
