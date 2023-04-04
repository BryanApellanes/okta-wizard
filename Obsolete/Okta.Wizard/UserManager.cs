// <copyright file="UserManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Okta.Wizard.Internal;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// A component used to manage users.
    /// </summary>
    public class UserManager : ServiceClient, IUserManager
    {
        private readonly IApplicationRegistrationManager applicationRegistrationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        public UserManager()
        {
            ApiCredentials = ApiCredentials.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="apiCredentials">The API credentials.</param>
        /// <param name="applicationRegistrationManager">The application registration manager.</param>
        public UserManager(ApiCredentials apiCredentials, IApplicationRegistrationManager applicationRegistrationManager)
        {
            this.ApiCredentials = apiCredentials ?? ApiCredentials.Default;
            this.applicationRegistrationManager = applicationRegistrationManager;
            this.applicationRegistrationManager.AssigningUserToApplication += (s, a) => AssigningUserToApplication?.Invoke(this, a);
            this.applicationRegistrationManager.AssignedUserToApplication += (s, a) => AssignedUserToApplication?.Invoke(this, a);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="applicationRegistrationManager">The application registration manager.</param>
        public UserManager(IApplicationRegistrationManager applicationRegistrationManager)
            : this(ApiCredentials.Default, applicationRegistrationManager)
        {
        }

        /// <summary>
        /// The event that is raised before getting a user.
        /// </summary>
        public event EventHandler GettingUser;

        /// <summary>
        /// The event that is raised after getting a user.
        /// </summary>
        public event EventHandler GotUser;

        /// <summary>
        /// The event that is raised before creating a user.
        /// </summary>
        public event EventHandler CreatingUser;

        /// <summary>
        /// The event that is raised after creating a user.
        /// </summary>
        public event EventHandler CreatedUser;

        /// <summary>
        /// The event that is raised before activating a user.
        /// </summary>
        public event EventHandler ActivatingUser;

        /// <summary>
        /// The event that is raised after activiating a user.
        /// </summary>
        public event EventHandler ActivatedUser;

        /// <summary>
        /// The event that is raised before deleting a user.
        /// </summary>
        public event EventHandler DeletingUser;

        /// <summary>
        /// The event that is raised after deleting a user.
        /// </summary>
        public event EventHandler DeletedUser;

        /// <summary>
        /// The event that is raised before assigning a user to an application.
        /// </summary>
        public event EventHandler AssigningUserToApplication;

        /// <summary>
        /// The event that is raised after assigning a user to an application.
        /// </summary>
        public event EventHandler AssignedUserToApplication;

        /// <summary>
        /// Gets a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Task{GetUserResponse}</returns>
        public async Task<GetUserResponse> GetUserAsync(string userId)
        {
            GettingUser?.Invoke(this, new UserEventArgs { UserId = userId });
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Get, $"{GetPath()}/{userId}");
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new GetUserResponse
                {
                    ApiException = HandleError(responseMessage),
                };
            }

            GetUserResponse response = Deserialize.FromJson<GetUserResponse>(await responseMessage.Content.ReadAsStringAsync());
            GotUser?.Invoke(this, new UserEventArgs { UserId = userId, Profile = response.Profile });
            return response;
        }

        /// <summary>
        /// Activates a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Task{HttpStatusCode}</returns>
        public async Task<HttpStatusCode> ActivateUserAsync(string userId)
        {
            ActivatingUser?.Invoke(this, new UserEventArgs { UserId = userId });
            HttpClient client = new HttpClient();
            string activationPath = Path.Combine(GetPath(), "lifecycle", "activate");
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Post, activationPath);
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            HttpStatusCode statusCode = responseMessage.StatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                ActivatedUser?.Invoke(this, new UserEventArgs { UserId = userId });
            }

            return statusCode;
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task{UserCreationResponse}</returns>
        public async Task<UserCreationResponse> CreateUserAsync(UserProfile userProfile, string password)
        {
            CreatingUser?.Invoke(this, new CreateUserEventArgs { UserProfile = userProfile });
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Post, GetPath("activate=true"));
            string requestJson = new UserCreationRequest
            {
                Profile = userProfile,
                Credentials = new UserCredentials
                {
                    Password = new UserPassword
                    {
                        Value = password,
                    },
                    RecoveryQuestion = new UserRecoveryQuestion
                    {
                        Question = "Who created this user account?",
                        Answer = "Okta Visual Studio Template Wizard",
                    },
                },
            }.ToJson();
            requestMessage.Content = GetStringContent(requestJson);
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);

            UserCreationResponse response = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                string responseJson = await responseMessage.Content.ReadAsStringAsync();
                CreatedUser?.Invoke(this, new CreateUserEventArgs { UserProfile = userProfile });
                response = Deserialize.FromJson<UserCreationResponse>(responseJson);
            }
            else
            {
                response.ApiException = HandleError(responseMessage);
            }
            response.HttpStatusCode = responseMessage.StatusCode;
            return response;
        }

        /// <summary>
        /// Assigns a user to an application.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="request">The request.</param>
        /// <returns>Task{AssignUserToApplicationResponse}</returns>
        public Task<AssignUserToApplicationResponse> AssignUserToApplicationAsync(string applicationId, AssignUserToApplicationRequest request)
        {
            return applicationRegistrationManager.AssignUserToApplicationAsync(applicationId, request);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <returns>Task{HttpStatusCode}</returns>
        public async Task<HttpStatusCode> DeleteUserAsync(string userId)
        {
            DeletingUser?.Invoke(this, new UserEventArgs { UserId = userId });
            HttpClient client = new HttpClient();
            string deletePath = Path.Combine(GetPath(), userId);
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Delete, deletePath);
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            if (responseMessage.IsSuccessStatusCode)
            {
                DeletedUser?.Invoke(this, new UserEventArgs { UserId = userId });
            }
            else
            {
                HandleError(responseMessage);
            }

            return responseMessage.StatusCode;
        }

        /// <summary>
        /// Gets the users API path.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns>string</returns>
        protected override string GetPath(string queryString = null)
        {
            Uri domain = GetDomainUri();

            string path = Path.Combine(domain.ToString(), "api", "v1", "users");
            if (!string.IsNullOrEmpty(queryString))
            {
                path += $"?{queryString}";
            }

            return path;
        }
    }
}
