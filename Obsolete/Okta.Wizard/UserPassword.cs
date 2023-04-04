// <copyright file="UserPassword.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a users password.
    /// </summary>
    public class UserPassword : Serializable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
