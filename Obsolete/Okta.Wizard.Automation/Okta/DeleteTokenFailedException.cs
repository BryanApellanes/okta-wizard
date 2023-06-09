﻿// <copyright file="DeleteTokenFailedException.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Wizard.Automation.Okta
{
    public class DeleteTokenFailedException : Exception
    {
        public DeleteTokenFailedException(string tokenName, string additionalInfo = null) : base($"Failed to delete token ({additionalInfo ?? string.Empty}): {tokenName}") { }
    }
}
