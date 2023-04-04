// <copyright file="SinglePageApplicationWizardRunFinisher.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// A component for executing additional logic when the wizard finishes for a single page application.
    /// </summary>
    public class SinglePageApplicationWizardRunFinisher : WizardRunFinisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePageApplicationWizardRunFinisher"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        public SinglePageApplicationWizardRunFinisher(IUserManager userManager)
            : base(userManager)
        {
        }
    }
}
