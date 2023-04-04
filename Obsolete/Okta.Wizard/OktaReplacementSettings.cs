// <copyright file="OktaReplacementSettings.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using DevEx.Internal;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a set of variable replacements and the file names they are applied to.
    /// </summary>
    public class OktaReplacementSettings : Serializable
    {
        /// <summary>
        /// Gets or sets the names of files to perform string replacements on.  Include
        /// only the name of the file with extension excluding the path.
        /// </summary>
        /// <value>
        /// The names of files to perform string replacements on.  Include
        /// only the name of the file with extension excluding the path.
        /// </value>
        public string[] FileNames { get; set; }

        /// <summary>
        /// Gets or sets the replacements to make.
        /// </summary>
        /// <value>
        /// The replacements to make.
        /// </value>
        public IEnumerable<OktaReplacement> OktaReplacements { get; set; }
    }
}
