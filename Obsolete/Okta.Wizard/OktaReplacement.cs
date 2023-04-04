// <copyright file="OktaReplacement.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a value replacement.
    /// </summary>
    public class OktaReplacement
    {
        /// <summary>
        /// Gets or sets the value to replace.
        /// </summary>
        /// <value>
        /// The value to replace.
        /// </value>
        public string ValueToReplace { get; set; }

        /// <summary>
        /// Gets or sets the value to use when performing replacements.  If
        /// this value is present, as a key, in the replacements dictionary of OktaWizardResult,
        /// the value from the dictionary is used, otherwise the raw value is used.
        /// </summary>
        /// <value>
        /// The value to use when performing replacements.  If
        /// this value is present, as a key, in the replacements dictionary of OktaWizardResult,
        /// the value from the dictionary is used, otherwise the raw value is used.
        /// </value>
        public string ValueToUse { get; set; }

        /// <summary>
        /// Apply variable replacements to the provided input.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <param name="input">The input.</param>
        /// <returns>The result of variable replacement.</returns>
        public string Apply(OktaWizardResult oktaWizardResult, string input)
        {
            string replaceWith = ValueToUse;
            if (oktaWizardResult.Replacements.ContainsKey(replaceWith))
            {
                replaceWith = oktaWizardResult.Replacements[replaceWith];
            }

            return input.Replace(ValueToReplace, replaceWith);
        }
    }
}
