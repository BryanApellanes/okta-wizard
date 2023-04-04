// <copyright file="OperationDescriptor.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// A class that describes an operation.
    /// </summary>
    public class OperationDescriptor
    {
        /// <summary>
        /// Gets or sets the kind of operation.
        /// </summary>
        /// <value>
        /// The kind of operation.
        /// </value>
        public OperationKind OperationKind { get; set; }

        /// <summary>
        /// Gets or sets the name of the property the operation occurred on.
        /// </summary>
        /// <value>
        /// The name of the property the operation occurred on.
        /// </value>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the value of the property the operation occurred on.
        /// </summary>
        /// <value>
        /// The value of the property the operation occurred on.
        /// </value>
        public object PropertyValue { get; set; }
    }
}
