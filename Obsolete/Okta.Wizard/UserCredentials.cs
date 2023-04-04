// <copyright file="UserCredentials.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a users credentials.
    /// </summary>
    public class UserCredentials : Serializable
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [JsonProperty("password")]
        public UserPassword Password { get; set; }

        /// <summary>
        /// Gets or sets the recovery question.
        /// </summary>
        /// <value>
        /// The recovery question.
        /// </value>
        [JsonProperty("recovery_question")]
        public UserRecoveryQuestion RecoveryQuestion { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [JsonProperty("provider")]
        public UserCredentialsProvider Provider { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
