// <copyright file="ServiceClient.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Net.Http;
using System.Text;

namespace Okta.Wizard
{
    /// <summary>
    /// A base service client.
    /// </summary>
    public abstract class ServiceClient
    {
        /// <summary>
        /// Gets or sets the API credentials.
        /// </summary>
        /// <value>
        /// The API credentials.
        /// </value>
        public ApiCredentials ApiCredentials { get; set; }

        /// <summary>
        /// Gets or sets the error response.
        /// </summary>
        /// <value>
        /// The error response.
        /// </value>
        public ErrorResponse ErrorResponse { get; set; }

        /// <summary>
        /// The event that is raised when an exception occurs.
        /// </summary>
        public event EventHandler ApiException;

        /// <summary>
        /// Handle any errors that occurred for the specified response.
        /// </summary>
        /// <param name="responseMessage">The response to handle.</param>
        /// <returns>ApiException</returns>
        protected ApiException HandleError(HttpResponseMessage responseMessage)
        {
            ApiException apiException = null;
            if (!responseMessage.IsSuccessStatusCode)
            {
                apiException = new ApiException(responseMessage);
                ErrorResponse = apiException.ErrorResponse;
                ApiException?.Invoke(this, new ApiExceptionEventArgs(apiException));
            }

            return apiException;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns>string</returns>
        protected abstract string GetPath(string queryString = null);

        /// <summary>
        /// Gets the domain URI.
        /// </summary>
        /// <returns>URI</returns>
        protected virtual Uri GetDomainUri()
        {
            return OktaWizardConfig.GetDomainUri(ApiCredentials?.Domain);
        }

        /// <summary>
        /// Gets the http content for the specified content.
        /// </summary>
        /// <param name="content">The body content.</param>
        /// <returns>HttpContent</returns>
        protected HttpContent GetStringContent(string content)
        {
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            return stringContent;
        }

        protected HttpRequestMessage GetHttpRequestMessage(HttpMethod method, string path = null)
        {
            return GetHttpRequestMessage(ApiCredentials.Token, method, path);
        }

        /// <summary>
        /// Gets a request message.
        /// </summary>
        /// <param name="apiToken">The API token.</param>
        /// <param name="method">The http method.</param>
        /// <param name="path">The path.</param>
        /// <returns>HttpRequestMessage</returns>
        protected HttpRequestMessage GetHttpRequestMessage(string apiToken, HttpMethod method, string path = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, path ?? GetPath());
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", $"SSWS {apiToken}");
            request.Headers.Add("User-Agent", OktaWizardUserAgent.Value);
            return request;
        }
    }
}
