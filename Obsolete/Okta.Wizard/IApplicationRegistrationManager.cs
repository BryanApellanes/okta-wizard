// <copyright file="IApplicationRegistrationManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading.Tasks;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a component used to register client applications.
    /// </summary>
    public interface IApplicationRegistrationManager
    {
        /// <summary>
        /// Gets or sets the API credentials.
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
        /// Lists client applications.
        /// </summary>
        /// <returns>Task{ApplicationListResponse[]}</returns>
        Task<ApplicationListResponse[]> ListApplicationsAsync();

        /// <summary>
        /// Retrieves the specified application.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Task{ApplicationFindResponse}</returns>
        Task<ApplicationFindResponse> RetrieveApplicationAsync(string applicationName);

        /// <summary>
        /// When impelemented in a derived class, searches applications.
        /// </summary>
        /// <param name="startsWith">The text to search.</param>
        /// <param name="limit">The count per page.</param>
        /// <param name="after">The ID after which to return results.</param>
        /// <returns>Task{ApplicationFindResponse[]}</returns>
        Task<ApplicationFindResponse[]> FindApplicationsAsync(string startsWith, int limit = 20, string after = null);

        /// <summary>
        /// Registers the specified client application.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <returns>Task{ApplicationRegistrationResponse}</returns>
        Task<ApplicationRegistrationResponse> RegisterApplicationAsync(string clientName, string clientUri = null, string logoUri = null);

        /// <summary>
        /// Registers the specified client application.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <param name="initiateLoginUri">The initiate login URI.</param>
        /// <returns>Task{ApplicationRegistrationResponse}</returns>
        Task<ApplicationRegistrationResponse> RegisterApplicationAsync(OktaApplicationType oktaApplicationType, string clientName, string clientUri = null, string logoUri = null, string initiateLoginUri = null);

        /// <summary>
        /// When impelemented in a derived class, assigns a user to the specified application.
        /// </summary>
        /// <param name="applicationId">The application ID</param>
        /// <param name="request">Thre request</param>
        /// <returns>Task{AssignUserToApplicationResponse}</returns>
        Task<AssignUserToApplicationResponse> AssignUserToApplicationAsync(string applicationId, AssignUserToApplicationRequest request);

        /// <summary>
        /// When impelemented in a derived class, deletes the specified client application.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <returns>Task{ApiStatusResponse}</returns>
        Task<ApiStatusResponse> DeleteClientApplicationAsync(string clientId);

        /// <summary>
        /// The event that is raised before retrieving an application.
        /// </summary>
        event EventHandler RetrievingApplication;

        /// <summary>
        /// The event that is raised when an application is retreived.
        /// </summary>
        event EventHandler RetrievedApplication;

        /// <summary>
        /// The event that is raised before registering an application.
        /// </summary>
        event EventHandler RegisteringApplication;

        /// <summary>
        /// The event that is raised when an application is registered.
        /// </summary>
        event EventHandler RegisteredApplication;

        /// <summary>
        /// The event that is raised befpre deleting an application.
        /// </summary>
        event EventHandler DeletingApplication;

        /// <summary>
        /// The event that is raised when an application is deleted.
        /// </summary>
        event EventHandler DeletedApplication;

        /// <summary>
        /// The event that is raised before assigning a user to an application.
        /// </summary>
        event EventHandler AssigningUserToApplication;

        /// <summary>
        /// The event that is raised when a user is assigned to an application.
        /// </summary>
        event EventHandler AssignedUserToApplication;

        /// <summary>
        /// The event that is raised when more than on application is found.
        /// </summary>
        event EventHandler MultipleApplicationsFoundWarning;

        /// <summary>
        /// The evnet that is raised when an API exception occurs.
        /// </summary>
        event EventHandler ApiException;
    }
}
