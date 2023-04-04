// <copyright file="ClientApplicationRetrievalEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to client application retrieval.
    /// </summary>
    public class ClientApplicationRetrievalEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ApplicationFindResponse Response { get; set; }

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
    }
}
