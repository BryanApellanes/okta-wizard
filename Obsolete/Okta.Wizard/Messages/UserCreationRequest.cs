// <copyright file="UserCreationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a request to create a user.
    /// </summary>
    public class UserCreationRequest : Serializable
    {
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
    }
}
