// <copyright file="IWizardRunFinisherResolver.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    /// <summary>
    /// A component used to resolve wizard run finishers.
    /// </summary>
    public interface IWizardRunFinisherResolver
    {
        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        IUserManager UserManager { get; set; }

        /// <summary>
        /// Gets the wizard run finisher for the specified result.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <param name="useExisting">Value indicating whether to reuse previously resolved finisher.</param>
        /// <returns>IWizardRunFinisher</returns>
        IWizardRunFinisher GetWizardRunFinisher(OktaWizardResult oktaWizardResult, bool useExisting = false);

        /// <summary>
        /// Gets the wizard run finisher for the specified okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="useExisting">Value indicating whether to reuse previously resolved finisher.</param>
        /// <returns>IWizardRunFinisher</returns>
        IWizardRunFinisher GetWizardRunFinisher(OktaApplicationType oktaApplicationType, bool useExisting = false);

        /// <summary>
        /// Sets the wizard run finisher for the specified Okta application type.
        /// </summary>
        /// <typeparam name="T">The type of the wizard run finisher.</typeparam>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        void SetWizardRunFinisher<T>(OktaApplicationType oktaApplicationType)
            where T : IWizardRunFinisher;

        /// <summary>
        /// Sets the wizard run finisher for the specified Okta application type.
        /// </summary>
        /// <param name="type">The type of the wizard run finisher.</param>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        void SetWizardRunFinisher(Type type, OktaApplicationType oktaApplicationType);

        /// <summary>
        /// Sets the wizard run finisher for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="instance">The instance.</param>
        void SetWizardRunFinisher(OktaApplicationType oktaApplicationType, IWizardRunFinisher instance);
    }
}
