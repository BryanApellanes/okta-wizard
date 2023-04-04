// <copyright file="ApplicationRegistrationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// Represents a request to register a client application.
    /// </summary>
    public class ApplicationRegistrationRequest : ApplicationMessage
    {
        /// <summary>
        /// Get the initiate login url appropriate for the specified okta application type and domain.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="domain">The Okta domain.</param>
        /// <returns>string</returns>
        public static string GetInitiateLoginUri(OktaApplicationType oktaApplicationType, string domain)
        {
            switch (oktaApplicationType)
            {
                case OktaApplicationType.None:
                    break;
                case OktaApplicationType.Native:
                    string[] parts = domain.Split('.');
                    StringBuilder result = new StringBuilder();
                    bool first = true;
                    for (int i = parts.Length - 1; i >= 0; i--)
                    {
                        if (!first)
                        {
                            result.Append(".");
                        }

                        first = false;
                        result.Append(parts[i]);
                    }

                    result.Append(":/callback");
                    return result.ToString();
                case OktaApplicationType.SinglePageApplication:
                    return $"https://{domain}:8080/implicit/callback";
                case OktaApplicationType.Web:
                    return $"https://{domain}/authorization-code/callback";
                case OktaApplicationType.Service:
                case OktaApplicationType.Repository:
                default:
                    return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets or sets the client application URIs.
        /// </summary>
        /// <value>
        /// The client application URIs.
        /// </value>
        public static ClientApplicationUris ClientApplicationUris
        {
            get;
            set;
        }

        static ApplicationRegistrationRequest()
        {
            ClientApplicationUris = new ClientApplicationUris();
            ApplicationRegistrationRequestFactories = new Dictionary<OktaApplicationType, Func<string, string, string, ApplicationRegistrationRequest>>
            {
                { OktaApplicationType.None, (clientName, clientUri, logoUri) => new ApplicationRegistrationRequest(clientName, clientUri, logoUri) },
                { OktaApplicationType.Native, (clientName, clientUri, logoUri) => new NativeApplicationRegistrationRequest(clientName, clientUri, logoUri) },
                { OktaApplicationType.SinglePageApplication, (clientName, clientUri, logoUri) => new SinglePageApplicationApplicationRegistrationRequest(clientName, clientUri, logoUri) },
                { OktaApplicationType.Web, (clientName, clientUri, logoUri) => new WebApplicationRegistrationRequest(clientName, clientUri, logoUri) },
                { OktaApplicationType.Service, (clientName, clientUri, logoUri) => new ServiceApplicationRegistrationRequest(clientName, clientUri, logoUri) },
                { OktaApplicationType.Repository, (clientName, clientUri, logoUri) => new ApplicationRegistrationRequest(clientName, clientUri, logoUri) },
            };
        }

        /// <summary>
        /// Gets the application registration request factories.
        /// </summary>
        /// <value>
        /// The application registration request factories.
        /// </value>
        public static Dictionary<OktaApplicationType, Func<string, string, string, ApplicationRegistrationRequest>> ApplicationRegistrationRequestFactories
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistrationRequest"/> class.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        public ApplicationRegistrationRequest(string clientName, string clientUri = null, string logoUri = null)
            : base(clientName, clientUri, logoUri)
        {
        }

        /// <summary>
        /// Gets an application registration request for the specified arguments.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <returns>ApplicationRegistrationRequest</returns>
        public static ApplicationRegistrationRequest For(string templateName, string clientName, string clientUri, string logoUri = null)
        {
            if (!string.IsNullOrEmpty(templateName))
            {
                if (Enum.TryParse(templateName, out OktaApplicationType oktaTemplateName))
                {
                    return For(oktaTemplateName, clientName, clientUri, logoUri);
                }
            }

            return new ApplicationRegistrationRequest(clientName, clientUri, logoUri);
        }

        /// <summary>
        /// Gets the redirect URIs for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <returns>array of strings.</returns>
        public static string[] GetRedirectUrisFor(OktaApplicationType oktaApplicationType)
        {
            return ClientApplicationUris.RedirectUris[oktaApplicationType].ToArray();
        }

        /// <summary>
        /// Gets the post logout redirect URIs for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <returns>array of strings.</returns>
        public static string[] GetPostLogoutRedirectUrisFor(OktaApplicationType oktaApplicationType)
        {
            return ClientApplicationUris.PostLogoutUris[oktaApplicationType].ToArray();
        }

        /// <summary>
        /// Gets an application registration request for the speicified arguments.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <returns>ApplicationRegistrationRequest</returns>
        public static ApplicationRegistrationRequest For(OktaApplicationType oktaApplicationType, string clientName, string clientUri, string logoUri = null)
        {
            ApplicationRegistrationRequest registrationRequest = ApplicationRegistrationRequestFactories[oktaApplicationType](clientName, clientUri, logoUri);
            return registrationRequest;
        }
    }
}
