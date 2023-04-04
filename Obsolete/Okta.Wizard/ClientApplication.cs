// <copyright file="ClientApplication.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;
using Okta.Wizard.Messages;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a client application.
    /// </summary>
    public class ClientApplication : Serializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplication"/> class.
        /// </summary>
        /// <param name="applicationRegistrationResponse">The application registration response</param>
        public ClientApplication(ApplicationRegistrationResponse applicationRegistrationResponse)
        {
            Name = applicationRegistrationResponse.ClientName;
            ClientId = applicationRegistrationResponse.ClientId;
            ClientSecret = applicationRegistrationResponse.ClientSecret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplication"/> class.
        /// </summary>
        /// <param name="applicationRetrieveResponse">The application find response.</param>
        public ClientApplication(ApplicationFindResponse applicationRetrieveResponse)
        {
            Name = applicationRetrieveResponse.ClientName;
            ClientId = applicationRetrieveResponse.ClientId;
            ClientSecret = "UNAVAILABLE";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplication"/> class.
        /// Create a new instance.
        /// </summary>
        /// <param name="applicationListResponse">The list response.</param>
        public ClientApplication(ApplicationListResponse applicationListResponse)
        {
            Name = applicationListResponse.ClientName;
            ClientId = applicationListResponse.ClientId;
            ClientSecret = "UNAVAILABLE";
        }

        /// <summary>
        /// Gets or sets a value indicating whether the application exists.
        /// </summary>
        /// <value>
        /// A value indicating whether the application exists.
        /// </value>
        public bool Exists { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public string ProjectName => Name.Replace(" ", ".").Replace("-", ".").Replace("\\", ".").Replace("/", ".");

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client ID.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
