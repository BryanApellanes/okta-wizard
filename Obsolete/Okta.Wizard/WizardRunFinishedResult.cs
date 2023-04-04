// <copyright file="WizardRunFinishedResult.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the result of finishing a wizard run.
    /// </summary>
    public class WizardRunFinishedResult : Serializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardRunFinishedResult"/> class.
        /// </summary>
        /// <param name="message">A message.</param>
        public WizardRunFinishedResult(string message = null)
        {
            Status = WizardRunFinishedStatus.Success;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardRunFinishedResult"/> class.
        /// </summary>
        /// <param name="ex">An exception</param>
        public WizardRunFinishedResult(Exception ex)
        {
            Status = WizardRunFinishedStatus.Error;
            Message = ex.Message;
            Exception = ex;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public WizardRunFinishedStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the test user.
        /// </summary>
        /// <value>
        /// The test user.
        /// </value>
        public TestUser TestUser { get; set; }

        /// <summary>
        /// Gets or sets the file test user information is saved to.
        /// </summary>
        /// <value>
        /// The file test user information is saved to.
        /// </value>
        public FileInfo TestUserFile { get; set; }

        /// <summary>
        /// Gets or sets the project template parameters.
        /// </summary>
        /// <value>
        /// The project template parameters.
        /// </value>
        public ProjectTemplateParameters ProjectTemplateParameters { get; set; }

        /// <summary>
        /// Gets or sets the exception if any. May be null.
        /// </summary>
        /// <value>
        /// The exception if any. May be null.
        /// </value>
        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
