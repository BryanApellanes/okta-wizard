// <copyright file="ExecuteApiKeyToolResult.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Okta.Wizard.Messages;
using System;
using System.IO;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the result of running the API key tool.
    /// </summary>
    public class ExecuteApiKeyToolResult : Serializable
    {
        /// <summary>
        /// Gets or sets the Okta API Token.
        /// </summary>
        public OktaApiToken OktaApiToken { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the key tool ran successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the API key tool file.
        /// </summary>
        public FileInfo ApiKeyToolFile{ get; set; }

        /// <summary>
        /// Gets or sets the API key error response.
        /// </summary>
        public ApiKeyErrorResponse ApiKeyErrorResponse { get; set; }
    }
}
