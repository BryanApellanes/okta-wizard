// <copyright file="ReplaceAttribute.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// An attribute used to addorn properties that represent replacement dictionary keys.
    /// </summary>
    public class ReplaceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceAttribute"/> class.
        /// </summary>
        public ReplaceAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public ReplaceAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }
    }
}
