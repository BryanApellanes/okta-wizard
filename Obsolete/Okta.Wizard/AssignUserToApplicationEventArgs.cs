// <copyright file="AssignUserToApplicationEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data related to the assignment of a user to an application.
    /// </summary>
    public class AssignUserToApplicationEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignUserToApplicationEventArgs"/> class.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="request">The request.</param>
        public AssignUserToApplicationEventArgs(string applicationId, AssignUserToApplicationRequest request)
        {
            ApplicationId = applicationId;
            Request = request;
        }

        /// <summary>
        /// Gets or sets the application id.
        /// </summary>
        /// <value>
        /// The application ID.
        /// </value>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public AssignUserToApplicationRequest Request { get; set; }
    }
}
