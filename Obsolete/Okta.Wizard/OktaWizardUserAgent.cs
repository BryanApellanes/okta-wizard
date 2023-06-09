﻿// <copyright file="OktaWizardUserAgent.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Reflection;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents Okta wizard user agent.
    /// </summary>
    public class OktaWizardUserAgent
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public static string Value => new Lazy<string>(() => $"Okta-Visual-Studio-Wizard/{Assembly.GetExecutingAssembly().GetName().Version}").Value;
    }
}
