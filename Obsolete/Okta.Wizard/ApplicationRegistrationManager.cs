// <copyright file="ApplicationRegistrationManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Okta.Wizard.Internal;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// A class used to register client applications.
    /// </summary>
    public abstract class ApplicationRegistrationManager : ServiceClient, IApplicationRegistrationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistrationManager"/> class.
        /// </summary>
        public ApplicationRegistrationManager()
        {
            ApiCredentials = ApiCredentials.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRegistrationManager"/> class.
        /// </summary>
        /// <param name="oktaApiCredentials">Credentials to use.</param>
        public ApplicationRegistrationManager(ApiCredentials oktaApiCredentials)
        {
            ApiCredentials = oktaApiCredentials ?? ApiCredentials.Default;
        }

        /// <summary>
        /// The event raised before an application is registered.
        /// </summary>
        public event EventHandler RegisteringApplication;

        /// <summary>
        /// The event raised after an application is registered.
        /// </summary>
        public event EventHandler RegisteredApplication;

        /// <summary>
        /// The event raised before an application is deleted.
        /// </summary>
        public event EventHandler DeletingApplication;

        /// <summary>
        /// The event raised after an application is deleted.
        /// </summary>
        public event EventHandler DeletedApplication;

        /// <summary>
        /// The event rasied before an application is retrieved.
        /// </summary>
        public event EventHandler RetrievingApplication;

        /// <summary>
        /// The event raised after an application is retrieved.
        /// </summary>
        public event EventHandler RetrievedApplication;

        /// <summary>
        /// The event raised before a user is assigned to an application.
        /// </summary>
        public event EventHandler AssigningUserToApplication;

        /// <summary>
        /// The event raised after a user is assigned to an application.
        /// </summary>
        public event EventHandler AssignedUserToApplication;

        /// <summary>
        /// The event raised when multiple applications with the same name are found.
        /// </summary>
        public event EventHandler MultipleApplicationsFoundWarning;

        private static readonly Lazy<ApplicationRegistrationManager> _default = new Lazy<ApplicationRegistrationManager>(() => new ClientApplicationRegistrationManager());

        /// <summary>
        /// Gets the default client application registration manager
        /// </summary>
        /// <value>
        /// The default client application registration manager
        /// </value>
        public static ApplicationRegistrationManager Default
        {
            get => _default.Value;
        }

        /// <summary>
        /// Assigns a user to the specified application.
        /// </summary>
        /// <param name="applicationId">The application to assign the user to.</param>
        /// <param name="request">The request</param>
        /// <returns>Task{AssignUserToApplicationResponse}</returns>
        public async Task<AssignUserToApplicationResponse> AssignUserToApplicationAsync(string applicationId, AssignUserToApplicationRequest request)
        {
            AssigningUserToApplication?.Invoke(this, new AssignUserToApplicationEventArgs(applicationId, request));
            HttpClient client = new HttpClient();

            Uri domain = GetDomainUri();
            string path = Path.Combine(domain.ToString(), "api", "v1", "apps", applicationId, "users");
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Post, path);
            requestMessage.Content = GetStringContent(request.ToJson());
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            ApiException apiException = null;
            if (!responseMessage.IsSuccessStatusCode)
            {
                apiException = HandleError(responseMessage);
            }

            string responseJson = await responseMessage.Content.ReadAsStringAsync();
            AssignedUserToApplication?.Invoke(this, new AssignUserToApplicationEventArgs(applicationId, request));
            AssignUserToApplicationResponse response = Deserialize.FromJson<AssignUserToApplicationResponse>(responseJson);
            response.ApiException = apiException;
            return response;
        }

        /// <summary>
        /// Lists client applications.
        /// </summary>
        /// <returns>Task{ApplicationListResponse[]}</returns>
        public async Task<ApplicationListResponse[]> ListApplicationsAsync()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Get);
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            string json = await responseMessage.Content.ReadAsStringAsync();
            ApiException apiException = null;
            if (!responseMessage.IsSuccessStatusCode)
            {
                apiException = HandleError(responseMessage);
            }

            ApplicationListResponse[] result = Deserialize.FromJson<ApplicationListResponse[]>(json);
            if (apiException != null)
            {
                foreach (ApplicationListResponse clientApplicationListResponse in result)
                {
                    clientApplicationListResponse.ApiException = apiException;
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves the specified application.
        /// </summary>
        /// <param name="applicationName">The name of the application to retrieve.</param>
        /// <returns>Task{ApplicationFindResponse}</returns>
        public async Task<ApplicationFindResponse> RetrieveApplicationAsync(string applicationName)
        {
            RetrievingApplication?.Invoke(this, new ClientApplicationRetrievalEventArgs { ApplicationName = applicationName });
            ApplicationFindResponse[] apps = await FindApplicationsAsync(applicationName);
            ApplicationFindResponse app = null;
            if (apps.Length > 1)
            {
                MultipleApplicationsFoundWarning?.Invoke(this, new ClientApplicationRetrievalEventArgs { ApplicationName = applicationName });
            }

            if (apps.Length > 0)
            {
                app = apps[0];
            }

            RetrievedApplication?.Invoke(this, new ClientApplicationRetrievalEventArgs { ApplicationName = applicationName, Response = app });
            return app;
        }

        /// <summary>
        /// Searches applications.
        /// </summary>
        /// <param name="searchTerm">The search term/</param>
        /// <param name="limit">The count per page.</param>
        /// <param name="after">The ID after which results are returned.</param>
        /// <returns>Task{ApplicationFindResponse[]}</returns>
        public async Task<ApplicationFindResponse[]> FindApplicationsAsync(string searchTerm, int limit = 20, string after = null)
        {
            HttpClient client = new HttpClient();
            string query = $"q={searchTerm}&limit={limit}";
            if (!string.IsNullOrEmpty(after))
            {
                query += $"&after={after}";
            }

            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Get, GetPath(query));
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            ApiException apiException = null;
            if (!responseMessage.IsSuccessStatusCode)
            {
                apiException = HandleError(responseMessage);
            }

            string json = await responseMessage.Content.ReadAsStringAsync();
            ApplicationFindResponse[] result = Deserialize.FromJson<ApplicationFindResponse[]>(json);
            if (apiException != null)
            {
                foreach (ApplicationFindResponse response in result)
                {
                    response.ApiException = apiException;
                }
            }

            return result;
        }

        /// <summary>
        /// Registers the specified client application.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <returns>Task{ApplicationRegistrationResponse}</returns>
        public virtual async Task<ApplicationRegistrationResponse> RegisterApplicationAsync(string clientName, string clientUri = null, string logoUri = null)
        {
            return await RegisterApplicationAsync(OktaApplicationType.None, clientName, clientUri, logoUri);
        }

        /// <summary>
        /// Registers the specified client application.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <param name="initiateLoginUri">The initiate login URI.</param>
        /// <returns>Task{ApplicationRegistrationResponse}</returns>
        public virtual async Task<ApplicationRegistrationResponse> RegisterApplicationAsync(OktaApplicationType oktaApplicationType, string clientName, string clientUri = null, string logoUri = null, string initiateLoginUri = null)
        {
            InvokeRegisteringApplicationEvent(clientName, clientUri, logoUri);
            ApplicationRegistrationRequest registrationRequest = ApplicationRegistrationRequest.For(oktaApplicationType, clientName, clientUri, logoUri);
            registrationRequest.InitiateLoginUri = initiateLoginUri;
            HttpResponseMessage response = await RequestRegisterClientApplication(registrationRequest);
            ApiException apiException = null;
            if (!response.IsSuccessStatusCode)
            {
                apiException = HandleError(response);
            }

            string json = await response.Content.ReadAsStringAsync();
            ApplicationRegistrationResponse applicationRegistrationResponse = Deserialize.FromJson<ApplicationRegistrationResponse>(json);
            applicationRegistrationResponse.ApiException = apiException;
            InvokeRegisteredApplicationEvent(clientName, clientUri, logoUri, applicationRegistrationResponse);
            return applicationRegistrationResponse;
        }

        /// <summary>
        /// Invokes the RegisteredApplication event.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        /// <param name="applicationRegistrationResponse">The application registration response.</param>
        protected void InvokeRegisteredApplicationEvent(string clientName, string clientUri, string logoUri, ApplicationRegistrationResponse applicationRegistrationResponse)
        {
            RegisteredApplication?.Invoke(this, new ApplicationRegistrationEventArgs { ClientName = clientName, ClientUri = clientUri, LogoUri = logoUri, Response = applicationRegistrationResponse });
        }

        /// <summary>
        /// Invokes the RegisteringApplication event.
        /// </summary>
        /// <param name="clientName">The client name.</param>
        /// <param name="clientUri">The client URI.</param>
        /// <param name="logoUri">The logo URI.</param>
        protected void InvokeRegisteringApplicationEvent(string clientName, string clientUri, string logoUri)
        {
            RegisteringApplication?.Invoke(this, new ApplicationRegistrationEventArgs { ClientName = clientName, ClientUri = clientUri, LogoUri = logoUri });
        }

        /// <summary>
        /// Deletes the client application with the specified id.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>Task{ApiStatusResponse}</returns>
        public virtual async Task<ApiStatusResponse> DeleteClientApplicationAsync(string clientId)
        {
            InvokeDeletingApplicationEvent(clientId);
            HttpClient client = new HttpClient();
            ApiStatusResponse result = new ApiStatusResponse();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Delete, GetClientsPath());
            requestMessage.RequestUri = new Uri(Path.Combine(requestMessage.RequestUri.ToString(), clientId));
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
            if (!((int)responseMessage.StatusCode >= 200) && !((int)responseMessage.StatusCode <= 299))
            {
                string json = await responseMessage.Content.ReadAsStringAsync();
                result = Deserialize.FromJson<ApiStatusResponse>(json);
            }

            result.HttpStatusCode = responseMessage.StatusCode;
            InvokeDeletedApplicationEvent(clientId);
            return result;
        }

        /// <summary>
        /// Gets the clients path.
        /// </summary>
        /// <returns>string</returns>
        protected string GetClientsPath()
        {
            Uri domain = GetDomainUri();
            return Path.Combine(domain.ToString(), "oauth2", "v1", "clients");
        }

        /// <summary>
        /// Invokes the DeletedApplication event.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        protected void InvokeDeletedApplicationEvent(string clientId)
        {
            DeletedApplication?.Invoke(this, new ClientApplicationDeletionEventArgs { ClientId = clientId });
        }

        /// <summary>
        /// Invokes the DeletingApplication event.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        protected void InvokeDeletingApplicationEvent(string clientId)
        {
            DeletingApplication?.Invoke(this, new ClientApplicationDeletionEventArgs { ClientId = clientId });
        }

        /// <summary>
        /// Sends the specified client application registration request.
        /// </summary>
        /// <param name="registrationRequest">The registration request.</param>
        /// <returns>Task{HttpResponseMessage}</returns>
        protected async Task<HttpResponseMessage> RequestRegisterClientApplication(ApplicationRegistrationRequest registrationRequest)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Post);
            string requestJson = registrationRequest.ToJson();
            requestMessage.Content = GetStringContent(requestJson);
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            return response;
        }
    }
}
