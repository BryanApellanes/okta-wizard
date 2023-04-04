// <copyright file="ApiCredentialsEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to API credentials events.
    /// </summary>
    public class ApiCredentialsEventArgs : ExceptionEventArgs // TODO: review for removal
    {
        /// <summary>
        /// Gets or sets the Okta wizard.
        /// </summary>
        /// <value>
        /// The Okta wizard.
        /// </value>
        public OktaWizard OktaWizard { get; set; }

        /// <summary>
        /// Gets or sets the API credentials.
        /// </summary>
        /// <value>
        /// The API credentials.
        /// </value>
        public ApiCredentials ApiCredentials { get; set; }
    }
}
