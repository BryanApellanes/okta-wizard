// <copyright file="ClientApplicationRegistrationManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;

namespace Okta.Wizard
{
    /// <summary>
    /// Application registration manager that uses the clients API to register an application.
    /// </summary>
    public class ClientApplicationRegistrationManager : ApplicationRegistrationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplicationRegistrationManager"/> class.
        /// </summary>
        public ClientApplicationRegistrationManager()
        {
            ApiCredentials = ApiCredentials.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplicationRegistrationManager"/> class.
        /// </summary>
        /// <param name="oktaApiCredentials">The API credentials.</param>
        public ClientApplicationRegistrationManager(ApiCredentials oktaApiCredentials)
        {
            ApiCredentials = oktaApiCredentials ?? ApiCredentials.Default;
        }

        /// <summary>
        /// Gets the base uri to the clients api.
        /// </summary>
        /// <param name="queryString">The query string</param>
        /// <returns>string</returns>
        protected override string GetPath(string queryString = null)
        {
            Uri domain = GetDomainUri();
            string path = Path.Combine(domain.ToString(), "oauth2", "v1", "clients");
            if (!string.IsNullOrEmpty(queryString))
            {
                path += $"?{queryString}";
            }

            return path;
        }
    }
}