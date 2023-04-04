// <copyright file="ApplicationCredentialsEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to application credential events.
    /// </summary>
    public class ApplicationCredentialsEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Gets or sets the Okta wizard.
        /// </summary>
        /// <value>
        /// The Okta wizard.
        /// </value>
        public OktaWizard OktaWizard { get; set; }

        /// <summary>
        /// Gets or sets the application credentials.
        /// </summary>
        /// <value>
        /// The application credentials.
        /// </value>
        public ApplicationCredentials ApplicationCredentials { get; set; }
    }
}
