// <copyright file="ApiResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents an api response.
    /// </summary>
    public abstract class ApiResponse : Serializable
    {
        /// <summary>
        /// Gets or sets the api exception that occurred, if any.
        /// </summary>
        /// <value>
        /// The api exception that occurred, if any.
        /// </value>
        [JsonIgnore]
        public ApiException ApiException { get; set; }

        /// <summary>
        /// Gets a value indicating whether ApiException is null.
        /// </summary>
        /// <value>
        /// A boolean value indicating if ApiException is null.
        /// </value>
        [JsonIgnore]
        public bool Success { get => ApiException == null; }
    }
}
