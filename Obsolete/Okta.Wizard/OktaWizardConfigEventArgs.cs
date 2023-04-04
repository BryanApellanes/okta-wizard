// <copyright file="OktaWizardConfigEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to wizard configuration.
    /// </summary>
    public class OktaWizardConfigEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Gets or sets the Okta wizard.
        /// </summary>
        /// <value>
        /// The Okta wizard.
        /// </value>
        public OktaWizard OktaWizard { get; set; }

        /// <summary>
        /// Gets or sets the Okta wizard configuration.
        /// </summary>
        /// <value>
        /// The Okta wizard configuration.
        /// </value>
        public OktaWizardConfig OktaWizardConfig { get; set; }
    }
}
