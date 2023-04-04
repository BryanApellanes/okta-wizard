// <copyright file="ApiCredentialsObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents API Credentials for presentation.
    /// </summary>
    public class ApiCredentialsObservable : Observable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCredentialsObservable"/> class.
        /// </summary>
        public ApiCredentialsObservable()
        {
        }

        /// <summary>
        /// Gets or sets the OktaDomain.
        /// </summary>
        /// <value>
        /// The OktaDomain.
        /// </value>
        public string OktaDomain
        {
            get
            {
                return GetProperty<string>("OktaDomain");
            }

            set
            {
                SetProperty("OktaDomain", value);
            }
        }

        /// <summary>
        /// Gets or sets the ApiToken.
        /// </summary>
        /// <value>
        /// The ApiToken.
        /// </value>
        public string ApiToken
        {
            get
            {
                return GetProperty<string>("ApiToken");
            }

            set
            {
                SetProperty("ApiToken", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to prompt for API credentials.
        /// </summary>
        /// <value>
        /// The value indicating whether to prompt for API credentials.
        /// </value>
        public bool DontShowThisAgainSetupWindowChecked
        {
            get
            {
                return GetProperty<bool>("DontShowThisAgainSetupWindowChecked");
            }

            set
            {
                SetProperty("DontShowThisAgainSetupWindowChecked", value);
            }
        }

        /// <summary>
        /// Copy the OktaDomain and ApiToken to a new instance of ApiCredentials.
        /// </summary>
        /// <returns>ApiCredentials</returns>
        public ApiCredentials ToApiCredentials()
        {
            return new ApiCredentials
            {
                Domain = OktaDomain,
                Token = ApiToken,
            };
        }
    }
}
