// <copyright file="ApplicationCredentials.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents application credentials.
    /// </summary>
    public class ApplicationCredentials
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the id of the application.
        /// </summary>
        /// <value>
        /// The id of the application.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets a value indicating whether application name, client id and client secret have values.
        /// </summary>
        /// <value>
        /// Value indicating whether application name, client id and client secret have values.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public bool IsValid => !string.IsNullOrEmpty(ApplicationName) && !string.IsNullOrEmpty(ClientId) && !string.IsNullOrEmpty(ClientSecret);

        /// <summary>
        /// Gets or sets a value indicating whether the specified application already exists.
        /// </summary>
        /// <value>
        /// A value indicating if the specified application already exists.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public bool ApplicationExists { get; set; }
    }
}
