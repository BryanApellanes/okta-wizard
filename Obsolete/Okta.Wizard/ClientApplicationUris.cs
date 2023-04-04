// <copyright file="ClientApplicationUris.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents the redirect and post logout uris for Okta application types.
    /// </summary>
    public class ClientApplicationUris
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplicationUris"/> class.
        /// </summary>
        public ClientApplicationUris()
        {
            RedirectUris = new Dictionary<OktaApplicationType, List<string>>();
            PostLogoutUris = new Dictionary<OktaApplicationType, List<string>>();
            SetDefaults();
        }

        /// <summary>
        /// Gets or sets the redirect URIs.
        /// </summary>
        /// <value>
        /// The redirect URIs.
        /// </value>
        public Dictionary<OktaApplicationType, List<string>> RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the post logout URIs.
        /// </summary>
        /// <value>
        /// The post logout URIs.
        /// </value>
        public Dictionary<OktaApplicationType, List<string>> PostLogoutUris { get; set; }

        /// <summary>
        /// Clears the redirect and post logout URIs.
        /// </summary>
        public void Clear()
        {
            RedirectUris.Clear();
            PostLogoutUris.Clear();
        }

        /// <summary>
        /// Clears the redirect and post logout URIs and sets them to default values.
        /// </summary>
        public void SetDefaults()
        {
            Clear();
            RedirectUris.Add(OktaApplicationType.Native, new List<string>
            {
                "my.app.login:/callback",
                "com.okta.xamarin.ios.login:/callback",
            });
            PostLogoutUris.Add(OktaApplicationType.Native, new List<string>
            {
                "my.app.logout:/callback",
                "com.okta.xamarin.ios.logout:/callback",
            });
            RedirectUris.Add(OktaApplicationType.SinglePageApplication, new List<string>
            {
                "http://localhost:8080/implicit/callback",
                "https://localhost:8080/implicit/callback",
            });
            PostLogoutUris.Add(OktaApplicationType.SinglePageApplication, new List<string>
            {
                "http://localhost:8080",
                "https://localhost:8080",
            });
            RedirectUris.Add(OktaApplicationType.Web, new List<string>
            {
                "http://localhost:8080/authorization-code/callback",
                "https://localhost:8080/authorization-code/callback",
            });
            PostLogoutUris.Add(OktaApplicationType.Web, new List<string>
            {
                "http://localhost:8080",
                "https://localhost:8080",
            });
            RedirectUris.Add(OktaApplicationType.Service, new List<string>
            {
            });
        }
    }
}
