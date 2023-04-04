// <copyright file="OktaApplicationTypeApplicationRegistrationManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// A class used to register client applications based on Okta application type.
    /// </summary>
    public class OktaApplicationTypeApplicationRegistrationManager : ApplicationRegistrationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OktaApplicationTypeApplicationRegistrationManager"/> class.
        /// </summary>
        public OktaApplicationTypeApplicationRegistrationManager()
        {
            ApplicationRegistrationManagers =
                new Dictionary<OktaApplicationType, Func<ApplicationRegistrationManager>>()
                {
                    { OktaApplicationType.None, () => new ClientApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                    { OktaApplicationType.Native, () => new ManagementApiApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                    { OktaApplicationType.SinglePageApplication, () => new ClientApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                    { OktaApplicationType.Web, () => new ClientApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                    { OktaApplicationType.Service, () => new ClientApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                    { OktaApplicationType.Repository, () => new ClientApplicationRegistrationManager { ApiCredentials = ApiCredentials } },
                };
        }

        /// <inheritdoc/>
        public override async Task<ApplicationRegistrationResponse> RegisterApplicationAsync(OktaApplicationType oktaApplicationType, string clientName, string clientUri = null, string logoUri = null, string initiateLoginUri = null)
        {
            ApplicationRegistrationManager applicationRegistrationManager = ApplicationRegistrationManagers[oktaApplicationType]();
            InvokeRegisteringApplicationEvent(clientName, clientUri, logoUri);
            ApplicationRegistrationResponse response = await applicationRegistrationManager.RegisterApplicationAsync(oktaApplicationType, clientName, clientUri, logoUri, initiateLoginUri);
            InvokeRegisteredApplicationEvent(clientName, clientUri, logoUri, response);
            return response;
        }

        /// <summary>
        /// Gets or sets the application registration managers keyed by Okta application type.
        /// </summary>
        protected Dictionary<OktaApplicationType, Func<ApplicationRegistrationManager>> ApplicationRegistrationManagers
        {
            get;
            set;
        }

        /// <inheritdoc/>
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
