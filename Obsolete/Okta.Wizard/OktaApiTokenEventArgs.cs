﻿// <copyright file="OktaApiTokenEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard
{
    public class OktaApiTokenEventArgs : EventArgs
    {
        public OktaApiToken OktaApiToken { get; set; }

        public Exception Exception { get; set; }
    }
}
