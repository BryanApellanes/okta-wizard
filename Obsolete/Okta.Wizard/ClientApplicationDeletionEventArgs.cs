// <copyright file="ClientApplicationDeletionEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to a client application deletion event.
    /// </summary>
    public class ClientApplicationDeletionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public string ClientId { get; set; }
    }
}
