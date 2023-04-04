// <copyright file="CreateUserEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to creating a user.
    /// </summary>
    public class CreateUserEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        /// <value>
        /// The user profile.
        /// </value>
        public UserProfile UserProfile { get; set; }
    }
}
