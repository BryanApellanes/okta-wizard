// <copyright file="ManagementApiApplicationRegistrationManager.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Okta.Wizard.Internal;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Application registration manager that uses the management api to register an application.
    /// </summary>
    public class ManagementApiApplicationRegistrationManager : ApplicationRegistrationManager
    {
        /// <inheritdoc/>
        public override async Task<ApplicationRegistrationResponse> RegisterApplicationAsync(OktaApplicationType oktaApplicationType, string clientName, string clientUri = null, string logoUri = null, string initiateLoginUri = null)
        {
            InvokeRegisteringApplicationEvent(clientName, clientUri, logoUri);
            ManagementApiApplicationRegistrationRequest request = new ManagementApiApplicationRegistrationRequest(clientName, clientUri, logoUri);
            HttpResponseMessage response = await RequestRegisterApplication(request);
            ApiException apiException = null;
            if (!response.IsSuccessStatusCode)
            {
                apiException = HandleError(response);
            }

            string json = await response.Content.ReadAsStringAsync();
            ManagementApiApplicationRegistrationResponse managementApiApplicationRegistrationResponse = Deserialize.FromJson<ManagementApiApplicationRegistrationResponse>(json);
            ApplicationRegistrationResponse applicationRegistrationResponse = managementApiApplicationRegistrationResponse.Convert();
            applicationRegistrationResponse.ApiException = apiException;
            InvokeRegisteredApplicationEvent(clientName, clientUri, logoUri, applicationRegistrationResponse);
            return applicationRegistrationResponse;
        }

        /// <inheritdoc/>
        public override async Task<ApiStatusResponse> DeleteClientApplicationAsync(string clientId)
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
        /// Request an application registration.
        /// </summary>
        /// <param name="registrationRequest">The registration request.</param>
        /// <returns>Task{HttpResponseMessage}</returns>
        protected async Task<HttpResponseMessage> RequestRegisterApplication(ManagementApiApplicationRegistrationRequest registrationRequest)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = GetHttpRequestMessage(HttpMethod.Post);
            string requestJson = registrationRequest.ToJson();
            requestMessage.Content = GetStringContent(requestJson);
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            return response;
        }

        /// <inheritdoc/>
        protected override string GetPath(string queryString = null)
        {
            Uri domain = GetDomainUri();
            string path = Path.Combine(domain.ToString(), "api", "v1", "apps");
            if (!string.IsNullOrEmpty(queryString))
            {
                path += $"?{queryString}";
            }

            return path;
        }
    }
}
