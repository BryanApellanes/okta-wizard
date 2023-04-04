// <copyright file="ObservableEventArgs.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using Okta.Wizard.Binding;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents relevant data when an observable event occurs.
    /// </summary>
    public class ObservableEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the observable.
        /// </summary>
        /// <value>
        /// The observable.
        /// </value>
        public Observable Observable { get; set; }

        /// <summary>
        /// Gets or sets the operation descriptor.
        /// </summary>
        /// <value>
        /// The operation descriptor.
        /// </value>
        public OperationDescriptor OperationDescriptor { get; set; }
    }
}
