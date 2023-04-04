// <copyright file="NotificationObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents notification information for presentation.
    /// </summary>
    public class NotificationObservable : Observable
    {
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <value>
        /// The message text.
        /// </value>
        public string MessageText
        {
            get => GetProperty<string>("MessageText");
            set => SetProperty("MessageText", value);
        }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>
        /// The stack trace.
        /// </value>
        public string StackTrace
        {
            get => GetProperty<string>("StackTrace");
            set => SetProperty("StackTrace", value);
        }
    }
}
