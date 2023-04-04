// <copyright file="AssignUserToApplicationResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a response to a request to assign a user to an application.
    /// </summary>
    public class AssignUserToApplicationResponse : ApiResponse
    {
        /// <summary>
        /// Gets or sets the user's id.
        /// </summary>
        /// <value>
        /// The user's id.
        /// </value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        /// <value>
        /// The external id.
        /// </value>
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the date and time created.
        /// </summary>
        /// <value>
        /// The date and time created.
        /// </value>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the data and time of last update.
        /// </summary>
        /// <value>
        /// The data and time of last update.
        /// </value>
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the data and time of status change.
        /// </summary>
        /// <value>
        /// The data and time of status change.
        /// </value>
        [JsonProperty("statusChanged")]
        public DateTime StatusChanged { get; set; }

        /// <summary>
        /// Gets or sets the sync state.
        /// </summary>
        /// <value>
        /// The sync state.
        /// </value>
        [JsonProperty("syncState")]
        public string SyncState { get; set; }

        /// <summary>
        /// Gets or sets the last sync.
        /// </summary>
        /// <value>
        /// The last sync.
        /// </value>
        [JsonProperty("lastSync")]
        public string LastSync { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        [JsonProperty("credentials")]
        public UserCredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        [JsonProperty("profile")]
        public UserProfile Profile { get; set; }
    }
}
