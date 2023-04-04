// <copyright file="VisualStudioWizardRunFinisher.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Threading.Tasks;
using Okta.Wizard;

namespace Okta.VisualStudio.Wizard
{
    /// <summary>
    /// A component for executing additional logic when the wizard finishes.
    /// </summary>
    public class VisualStudioWizardRunFinisher : WizardRunFinisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioWizardRunFinisher"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public VisualStudioWizardRunFinisher(IUserManager userManager)
            : base(userManager)
        {
        }

        /// <inheritdoc/>
        public override async Task<WizardRunFinishedResult> RunFinishedAsync(OktaWizardResult oktaWizardResult)
        {
            WizardRunFinishedResult result = await base.RunFinishedAsync(oktaWizardResult); // creates test user and writes credentials to a file.
            ProjectTemplateParameters projectTemplateParameters = oktaWizardResult.GetProjectTemplateParameters(oktaWizardResult.OktaApplicationSettings.OktaApplicationType);
            result.ProjectTemplateParameters = projectTemplateParameters;

            return result;
        }
    }
}
