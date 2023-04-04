// <copyright file="IPromptProvider{T}.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Threading.Tasks;
using Okta.Wizard.Binding;

namespace Okta.Wizard
{

    /// <summary>
    /// Represents a component that prompts for user input.
    /// </summary>
    /// <typeparam name="T">The return type of the prompt.</typeparam>
    public interface IPromptProvider<T>
    {
        /// <summary>
        /// Gets or sets the Okta wizard.
        /// </summary>
        /// <value>
        /// The Okta wizard.
        /// </value>
        OktaWizard OktaWizard { get; set; }

        /// <summary>
        /// Prompts the user for input.
        /// </summary>
        /// <param name="observable">The observable used for databinding.</param>
        /// <returns>Task{T}</returns>
        Task<T> PromptAsync(Observable observable);
    }
}
