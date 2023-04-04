// <copyright file="ApiKeyToolEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data relevant to an API key tool event.
    /// </summary>
    public class ApiKeyToolEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the project data.
        /// </summary>
        public ProjectData ProjectData { get; set; }

        /// <summary>
        /// Gets or sets the user sign in credentials.
        /// </summary>
        public UserSignInCredentials UserSignInCredentials { get; set; }

        /// <summary>
        /// Gets or sets the api key tool file.
        /// </summary>
        public FileInfo ApiKeyToolFile { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception{ get; set; }
    }
}
