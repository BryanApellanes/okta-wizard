// <copyright file="GetUserResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a response to a get user request.
    /// </summary>
    public class GetUserResponse : ApiResponse
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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created.
        /// </summary>
        /// <value>
        /// The date and time of created.
        /// </value>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the date and time activated.
        /// </summary>
        /// <value>
        /// Or set the date and time activated.
        /// </value>
        [JsonProperty("activated")]
        public DateTime? Activated { get; set; }

        /// <summary>
        /// Gets or sets the date and time status changed.
        /// </summary>
        /// <value>
        /// The date and time status changed.
        /// </value>
        [JsonProperty("statusChanged")]
        public DateTime? StatusChanged { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login.
        /// </summary>
        /// <value>
        /// The date and time of last login.
        /// </value>
        [JsonProperty("lastLogin")]
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last update.
        /// </summary>
        /// <value>
        /// The date and time of last update.
        /// </value>
        [JsonProperty("lastUpdated")]
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the password changed.
        /// </summary>
        /// <value>
        /// The date and time the password changed.
        /// </value>
        [JsonProperty("passwordChanged")]
        public DateTime? PasswordChanged { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        [JsonProperty("profile")]
        public UserProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        [JsonProperty("credentials")]
        public UserCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}
