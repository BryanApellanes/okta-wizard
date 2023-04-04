// <copyright file="OktaTemplateNames.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the names of the templates.
    /// </summary>
    public enum OktaTemplateNames
    {
        /// <summary>
        /// No template name specified.
        /// </summary>
        None,

        /// <summary>
        /// Xamarin.
        /// </summary>
        OktaXamarin,

        /// <summary>
        /// Blazor web assembly.
        /// </summary>
        OktaBlazorWebAssembly,

        /// <summary>
        /// Aps .Net Core Mvc.
        /// </summary>
        OktaAspNetCoreMvc,

        /// <summary>
        /// Asp .Net Core Web API.
        /// </summary>
        OktaAspNetCoreWebApi,

        /// <summary>
        /// The parent template, allows the selection of one of the other templates when selected.
        /// </summary>
        OktaApplicationWizard,
    }
}
