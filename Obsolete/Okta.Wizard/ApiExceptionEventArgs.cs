// <copyright file="ApiExceptionEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to api exception events.
    /// </summary>
    public class ApiExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="apiException">The API exception.</param>
        public ApiExceptionEventArgs(ApiException apiException)
        {
            ApiException = apiException;
        }

        /// <summary>
        /// Gets or sets the api exception.
        /// </summary>
        /// <value>
        /// The API exception.
        /// </value>
        public ApiException ApiException { get; set; }

        /// <summary>
        /// Gets the error response.
        /// </summary>
        /// <value>
        /// The error response.
        /// </value>
        public ErrorResponse ErrorResponse { get => ApiException?.ErrorResponse; }
    }
}
