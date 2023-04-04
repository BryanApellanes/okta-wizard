// <copyright file="Links.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents links.
    /// </summary>
    public class Links : Serializable
    {
        /// <summary>
        /// Gets or sets the activate link.
        /// </summary>
        /// <value>
        /// The activate link.
        /// </value>
        [JsonProperty("activate")]
        public Link Activate { get; set; }

        /// <summary>
        /// Gets or sets the self link.
        /// </summary>
        /// <value>
        /// The self link.
        /// </value>
        [JsonProperty("self")]
        public Link Self { get; set; }
    }
}
