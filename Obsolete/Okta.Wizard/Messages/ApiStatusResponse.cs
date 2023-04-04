// <copyright file="ApiStatusResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Net;
using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents an api status response.
    /// </summary>
    public class ApiStatusResponse : Serializable
    {
        /// <summary>
        /// Gets or sets the HttpStatusCode.
        /// </summary>
        /// <value>
        /// The HttpStatusCode.
        /// </value>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        /// <value>
        /// The error text.
        /// </value>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
