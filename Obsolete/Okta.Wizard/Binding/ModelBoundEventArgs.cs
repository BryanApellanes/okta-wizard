// <copyright file="ModelBoundEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// Represents data relevant to a ModelBound event.
    /// </summary>
    public class ModelBoundEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        public Control Control { get; set; }

        /// <summary>
        /// Gets or sets the observable.
        /// </summary>
        public Observable Observable { get; set; }
    }
}
