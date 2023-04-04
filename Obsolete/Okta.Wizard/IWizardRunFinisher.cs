// <copyright file="IWizardRunFinisher.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    /// <summary>
    /// A component encapsulating logic to run when the wizard finishes.
    /// </summary>
    public interface IWizardRunFinisher
    {
        /// <summary>
        /// The event that is raised when an exception occurs creating a user.
        /// </summary>
        event EventHandler CreateUserExceptionOccurred;

        /// <summary>
        /// Gets or sets the exception that occurred creating a user.
        /// </summary>
        /// <value>
        /// The exception that occurred creating a user.
        /// </value>
        Exception CreateUserException { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred assigning a user to an application.
        /// </summary>
        /// <value>
        /// The exception that occurred assigning a user to an application.
        /// </value>
        Exception AssigningUserToApplicationException { get; set; }

        /// <summary>
        /// The event that is raised when an exception occurs assigning a user to an application.
        /// </summary>
        event EventHandler AssigningUserToApplicationExceptionOccurred;

        /// <summary>
        /// Gets or sets the custom delegate executed when events occur.
        /// </summary>
        /// <value>
        /// Custom delegate executed when events of interest occur.
        /// </value>
        Action<string, Severity> Notify { get; set; }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        IUserManager UserManager { get; set; }

        /// <summary>
        /// Executes final logic.
        /// </summary>
        /// <param name="oktaWizardResult">The okta wizard result.</param>
        /// <returns>WizardRunFinishedResult</returns>
        WizardRunFinishedResult RunFinished(OktaWizardResult oktaWizardResult);

        /// <summary>
        /// Executes final logic.
        /// </summary>
        /// <param name="oktaWizardResult">The okta wizard result.</param>
        /// <returns>Task{WizardRunFinishedResult}</returns>
        Task<WizardRunFinishedResult> RunFinishedAsync(OktaWizardResult oktaWizardResult);

        /// <summary>
        /// Does appropriate text replacements.
        /// </summary>
        /// <param name="oktaWizardResult">The okta wizard result.</param>
        /// <returns>Task{FileInfo[]}</returns>
        Task<FileInfo[]> DoOktaReplacementsAsync(OktaWizardResult oktaWizardResult);
    }
}
