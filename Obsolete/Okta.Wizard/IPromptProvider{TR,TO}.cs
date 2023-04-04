// <copyright file="IPromptProvider{R,O}.cs" company="Okta, Inc">
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
    /// <typeparam name="TR">The return type of the prompt.</typeparam>
    /// <typeparam name="TO">The input type of the observable.</typeparam>
    public interface IPromptProvider<TR, TO> : IPromptProvider<TR>
        where TO : Observable
    {
        /// <summary>
        /// Prompts the user for input asynchronously.
        /// </summary>
        /// <param name="observable">The observable used for databinding.</param>
        /// <returns>Task{TR}</returns>
        Task<TR> PromptAsync(TO observable);
    }
}
