// <copyright file="ApiCredentials.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using DevEx;
using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents credentials used to interact with the Okta api.
    /// </summary>
    public class ApiCredentials : Serializable
    {
        /// <summary>
        /// Gets or sets the okta domain.
        /// </summary>
        /// <value>
        /// The okta domain.
        /// </value>
        [EnvironmentVariable("OktaDomain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the API token.
        /// </summary>
        /// <value>
        /// The API token.
        /// </value>
        [EnvironmentVariable("OktaOrgToken")]
        public string Token { get; set; }

        /// <summary>
        /// Gets a value indicating whether both domain and token have values.  Will not test those values against the API.
        /// </summary>
        /// <value>
        /// A value indicating whether both domain and token have values.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public bool IsValid { get => !string.IsNullOrEmpty(Domain) && !string.IsNullOrEmpty(Token); }

        private static readonly Lazy<ApiCredentials> _default =
            new Lazy<ApiCredentials>(Deserialize.FromEnvironmentVariables<ApiCredentials>);

        /// <summary>
        /// Gets the system default API credentials.
        /// </summary>
        /// <value>
        /// The system default API credentials.
        /// </value>
        public static ApiCredentials Default => _default.Value;
    }
}
