// <copyright file="ManagementApiApplicationRegistrationResponse.cs" company="Okta, Inc">
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
    /// Represents a response to a management API application registration request.
    /// </summary>
    public class ManagementApiApplicationRegistrationResponse
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets when the application was last updated.
        /// </summary>
        /// <value>
        /// When the application was last updated.
        /// </value>
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets when the application was created.
        /// </summary>
        /// <value>
        /// When the application was created.
        /// </value>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// Or set the credentials.
        /// </value>
        [JsonProperty("credentials")]
        public ManagementApiApplicationRegistrationRequestCredentials Credentials { get; set; }

        // Other response fields omitted as unnecessary

        /// <summary>
        /// Convert this instance into an application registration response.
        /// </summary>
        /// <returns>ApplicationRegistrationRespsone</returns>
        public ApplicationRegistrationResponse Convert()
        {
            return new ApplicationRegistrationResponse
            {
                ClientId = Id,
                ClientName = Label,
            };
        }
    }
}
