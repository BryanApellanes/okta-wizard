// <copyright file="Link.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a link.
    /// </summary>
    public class Link : Serializable // TODO: consider moving this and other similar clases to the Okta.Wizard.Messages namespace
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
