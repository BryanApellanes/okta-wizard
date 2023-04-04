// <copyright file="OktaApplicationSettingsEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to events pertaining to Okta application settings.
    /// </summary>
    public class OktaApplicationSettingsEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the Okta wizard.
        /// </summary>
        /// <value>
        /// The Okta wizard.
        /// </value>
        public OktaWizard OktaWizard { get; set; }

        /// <summary>
        /// Gets or sets the Okta application settings.
        /// </summary>
        /// <value>
        /// The Okta application settings.
        /// </value>
        public OktaApplicationSettings OktaApplicationSettings { get; set; }

        /// <summary>
        /// Gets or sets the exception if any.
        /// </summary>
        /// <value>
        /// The exception if any.
        /// </value>
        public Exception Exception { get; set; }
    }
}
