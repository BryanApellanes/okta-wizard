// <copyright file="IUserManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a component used to manage users in the context of the Okta wizard.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// The event that is raised before getting a user.
        /// </summary>
        event EventHandler GettingUser;

        /// <summary>
        /// The event that is raised when a user is gotten.
        /// </summary>
        event EventHandler GotUser;

        /// <summary>
        /// The event that is raised before creating a user.
        /// </summary>
        event EventHandler CreatingUser;

        /// <summary>
        /// The event that is raised after a user is created.
        /// </summary>
        event EventHandler CreatedUser;

        /// <summary>
        /// The event that is raised before activating a user.
        /// </summary>
        event EventHandler ActivatingUser;

        /// <summary>
        /// The event that is raised after activating a user.
        /// </summary>
        event EventHandler ActivatedUser;

        /// <summary>
        /// The event that is raised before deleting a user.
        /// </summary>
        event EventHandler DeletingUser;

        /// <summary>
        /// The event that is raised after deleting a user.
        /// </summary>
        event EventHandler DeletedUser;

        /// <summary>
        /// The event that is raised before assigning a user to an application.
        /// </summary>
        event EventHandler AssigningUserToApplication;

        /// <summary>
        /// The event that is raised after assigning a user to an application.
        /// </summary>
        event EventHandler AssignedUserToApplication;

        /// <summary>
        /// The event that is raised when an api excption occurs.
        /// </summary>
        event EventHandler ApiException;

        /// <summary>
        /// Gets or sets the api credentials.
        /// </summary>
        /// <value>
        /// The API credentials.
        /// </value>
        ApiCredentials ApiCredentials { get; set; }

        /// <summary>
        /// Gets or sets the error response.
        /// </summary>
        /// <value>
        /// The error response.
        /// </value>
        ErrorResponse ErrorResponse { get; set; }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        /// <param name="userId">The id of the user to get.</param>
        /// <returns>Task{GetUserResponse}</returns>
        Task<GetUserResponse> GetUserAsync(string userId);

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userProfile">The profile of the user to create.</param>
        /// <param name="password">The password of the new user.</param>
        /// <returns>UserCreationResponse</returns>
        Task<UserCreationResponse> CreateUserAsync(UserProfile userProfile, string password);

        /// <summary>
        /// Activates a user.
        /// </summary>
        /// <param name="userId">The id of the user to activate.</param>
        /// <returns>Task{HttpStatusCode}</returns>
        Task<HttpStatusCode> ActivateUserAsync(string userId);

        /// <summary>
        /// Assigns a user to an application.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="request">The request.</param>
        /// <returns>Task{AssignUserToApplicationResponse}</returns>
        Task<AssignUserToApplicationResponse> AssignUserToApplicationAsync(string applicationId, AssignUserToApplicationRequest request);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Task{HttpStatusCode}</returns>
        Task<HttpStatusCode> DeleteUserAsync(string userId);
    }
}
