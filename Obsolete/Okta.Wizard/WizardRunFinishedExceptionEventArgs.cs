// <copyright file="WizardRunFinishedExceptionEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents event data relevant to wizard run finished exception events.
    /// </summary>
    public class WizardRunFinishedExceptionEventArgs : ExceptionEventArgs
    {
        /// <summary>
        /// Gets or sets the wizard run finisher.
        /// </summary>
        /// <value>
        /// The wizard run finisher.
        /// </value>
        public IWizardRunFinisher WizardRunFinisher { get; set; }
    }
}
