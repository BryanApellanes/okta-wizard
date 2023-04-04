// <copyright file="ManagementApiApplicationRegistrationRequestCredentials.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents the credentials property of the management API application registration request.
    /// </summary>
    public class ManagementApiApplicationRegistrationRequestCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagementApiApplicationRegistrationRequestCredentials"/> class.
        /// </summary>
        public ManagementApiApplicationRegistrationRequestCredentials()
        {
            this.OauthClient = new ManagementApiApplicationRegistrationRequestCredentialsOauthClient();
        }

        /// <summary>
        /// Gets or sets the oauth client.
        /// </summary>
        /// <value>
        /// The oauth client.
        /// </value>
        [JsonProperty("oauthClient")]
        public ManagementApiApplicationRegistrationRequestCredentialsOauthClient OauthClient { get; set; }
    }
}
