// <copyright file="WebWizardRunFinisher.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// A component for executing additional logic when the wizard finishes for a web application.
    /// </summary>
    public class WebWizardRunFinisher : WizardRunFinisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebWizardRunFinisher"/> class.
        /// </summary>
        /// <param name="userManager">The user manager</param>
        public WebWizardRunFinisher(IUserManager userManager)
            : base(userManager)
        {
        }
    }
}
