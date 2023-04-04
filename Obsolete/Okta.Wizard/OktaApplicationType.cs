// <copyright file="OktaApplicationType.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the types of Okta applications.
    /// </summary>
    public enum OktaApplicationType
    {
        /// <summary>
        /// No value specified.
        /// </summary>
        None,

        /// <summary>
        /// Native application.
        /// </summary>
        Native,

        /// <summary>
        /// Single page application.
        /// </summary>
        SinglePageApplication,

        /// <summary>
        /// Web.
        /// </summary>
        Web,

        /// <summary>
        /// Service.
        /// </summary>
        Service,

        /// <summary>
        /// Repository.
        /// </summary>
        Repository,
    }
}
