// <copyright file="UserSignInCredentialsObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents user sign in credentials.
    /// </summary>
    public class UserSignInCredentialsObservable : Observable
    {
        /// <summary>
        /// Gets or sets the url used to sign in.
        /// </summary>
        /// <value>
        /// The url used to sign in.
        /// </value>
        public string SignInUrl
        {
            get
            {
                return GetProperty<string>(nameof(SignInUrl));
            }

            set
            {
                SetProperty(nameof(SignInUrl), value);
            }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName
        {
            get
            {
                return GetProperty<string>(nameof(UserName));
            }

            set
            {
                SetProperty(nameof(UserName), value);
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return GetProperty<string>(nameof(Password));
            }

            set
            {
                SetProperty(nameof(Password), value);
            }
        }
    }
}
