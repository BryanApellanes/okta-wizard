// <copyright file="ApplicationFindResponse.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a response to an application search request.
    /// </summary>
    public class ApplicationFindResponse : ApplicationMessage
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
