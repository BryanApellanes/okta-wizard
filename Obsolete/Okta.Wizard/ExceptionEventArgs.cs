// <copyright file="ExceptionEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to exception events.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the exception that occurred, if any, may be null.
        /// </summary>
        /// <value>
        /// The exception that occurred, if any, may be null.
        /// </value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets a value indicating whether an exception occurred.
        /// </summary>
        /// <value>
        /// A value indicating if an exception occurred.
        /// </value>
        public bool ExceptionOccurred { get => Exception != null; }
    }
}
