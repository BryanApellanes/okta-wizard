// <copyright file="AssignUserToApplicationRequest.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// A request to assign a user to an application.
    /// </summary>
    public class AssignUserToApplicationRequest : Serializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignUserToApplicationRequest"/> class.
        /// </summary>
        public AssignUserToApplicationRequest()
        {
            Scope = "USER";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignUserToApplicationRequest"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public AssignUserToApplicationRequest(string userId)
            : this()
        {
            Id = userId;
        }

        /// <summary>
        /// Gets or sets the user's id.
        /// </summary>
        /// <value>
        /// The user's id.
        /// </value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the scope.  The default value is "USER".
        /// </summary>
        /// <value>
        /// The scope.  The default value is "USER".
        /// </value>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the user credentials.
        /// </summary>
        /// <value>
        /// The user credentials.
        /// </value>
        [JsonProperty("credentials")]
        public UserCredentials Credentials { get; set; }
    }
}
