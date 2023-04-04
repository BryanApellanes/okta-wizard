// <copyright file="UserSignInCredentialsEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to a user sign in credentials event.
    /// </summary>
    public class UserSignInCredentialsEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the user sign in credentials.
        /// </summary>
        public UserSignInCredentials UserSignInCredentials{ get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception{ get; set; }
    }
}
