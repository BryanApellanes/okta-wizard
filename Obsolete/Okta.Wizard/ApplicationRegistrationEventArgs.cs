// <copyright file="ApplicationRegistrationEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to a client application registration.
    /// </summary>
    public class ApplicationRegistrationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>
        /// The client name.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client URI.
        /// </summary>
        /// <value>
        /// The client URI.
        /// </value>
        public string ClientUri { get; set; }

        /// <summary>
        /// Gets or sets the logo uri.
        /// </summary>
        /// <value>
        /// The logo URI.
        /// </value>
        public string LogoUri { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ApplicationRegistrationResponse Response { get; set; }

        /// <summary>
        /// Gets the client application.
        /// </summary>
        /// <value>
        /// The client application.
        /// </value>
        public ClientApplication ClientApplication
        {
            get => new ClientApplication(Response);
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }
    }
}
