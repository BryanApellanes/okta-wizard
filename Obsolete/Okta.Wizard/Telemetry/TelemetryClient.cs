// <copyright file="TelemetryClient.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevEx;
using DevEx.Internal;
using DevEx.Telemetry.Messages;
using Okta.Wizard.Internal;
using Polly;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a client used to report telemetry data.
    /// </summary>
    public class TelemetryClient : IAsyncTelemetryService, ISecretProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryClient"/> class.
        /// </summary>
        /// <param name="telemetryServiceRoot">The root of the telemetry service.</param>
        /// <param name="preRetryHandler">The pre retry handler.</param>
        public TelemetryClient(string telemetryServiceRoot, Action<Exception, TimeSpan> preRetryHandler = null)
        {
            TelemetryServiceRoot = telemetryServiceRoot ?? OktaWizardConfig.DefaultTelemetryServiceRoot;
            if (TelemetryServiceRoot.EndsWith("/"))
            {
                TelemetryServiceRoot.Substring(0, TelemetryServiceRoot.Length - 1);
            }

            PreRetryHandler = preRetryHandler ?? ((e, t) => { }); // this may be useful for logging when implemented
            CreatedUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the base url of the telemetry service this client interacts with.
        /// </summary>
        /// <value>
        /// The base url of the telemetry service this client interacts with.
        /// </value>
        protected string TelemetryServiceRoot
        {
            get;
        }

        /// <summary>
        /// Gets or sets the action to execute prior to retries.
        /// </summary>
        /// <value>
        /// The action to execute prior to retries.
        /// </value>
        protected Action<Exception, TimeSpan> PreRetryHandler { get; set; }

        /// <summary>
        /// The event that is raised when an api exception occurs.
        /// </summary>
        public event EventHandler ApiException;

        /// <summary>
        /// Gets the date and time this client was created.
        /// </summary>
        /// <value>
        /// The date and time this client was created.
        /// </value>
        public DateTime CreatedUtc { get; }

        /// <summary>
        /// Gets the last api exception that occurred.  May be null.
        /// </summary>
        /// <value>
        /// The last api exception that occurred.  May be null.
        /// </value>
        public ApiException LastException { get; private set; }

        /// <summary>
        /// Increments the counter for the specified event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <returns>Task{IncrementResponse}</returns>
        public Task<IncrementResponse> IncrementEventCounterAsync(string eventName)
        {
            return IncrementEventCounterAsync(new IncrementRequest { EventName = eventName, TelemetrySessionId = TelemetrySessionData.TelemetrySessionId });
        }

        /// <summary>
        /// Increments the counter for the specified event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <returns>IncrementResponse</returns>
        public IncrementResponse IncrementEventCounter(string eventName)
        {
            return IncrementEventCounter(new IncrementRequest { EventName = eventName, TelemetrySessionId = TelemetrySessionData.TelemetrySessionId });
        }

        /// <summary>
        /// Increments the counter for the specified request.
        /// </summary>
        /// <param name="incrementRequest">The increment request.</param>
        /// <returns>IncrementResponse</returns>
        public IncrementResponse IncrementEventCounter(IncrementRequest incrementRequest)
        {
            if (incrementRequest == null)
            {
                throw new ArgumentNullException("incrementRequest cannot be null");
            }

            if (string.IsNullOrEmpty(incrementRequest.TelemetrySessionId))
            {
                incrementRequest.TelemetrySessionId = TelemetrySessionData.TelemetrySessionId;
            }

            return Policy
                   .Handle<Exception>()
                   .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), PreRetryHandler)
                   .Execute(() =>
                   {
                       HttpClient httpClient = GetHttpClient(TelemetryServiceRoot);

                       Uri endPoint = new Uri($"{TelemetryServiceRoot}/api/increment");
                       HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, endPoint)
                       {
                           Content = new StringContent(incrementRequest.ToJson(), Encoding.UTF8, "application/json"),
                       };
                       AddSignatureHeader(requestMessage, incrementRequest, endPoint);
                       HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                       if (ExceptionOccurred(responseMessage))
                       {
                           return new IncrementResponse { ApiException = new ApiException(responseMessage) };
                       }

                       string responseJson = responseMessage.Content.ReadAsStringAsync().Result;
                       return Deserialize.FromJson<IncrementResponse>(responseJson);
                   });
        }

        /// <summary>
        /// Increments the counter for the specified request.
        /// </summary>
        /// <param name="incrementRequest">The increment request.</param>
        /// <returns>Task{IncrementResponse}</returns>
        public async Task<IncrementResponse> IncrementEventCounterAsync(IncrementRequest incrementRequest)
        {
            return await Task.Run(() => IncrementEventCounter(incrementRequest));
        }

        /// <summary>
        /// Gets a summary of all events that have occurred.
        /// </summary>
        /// <returns>Summary</returns>
        public Summary Summary()
        {
            return SummaryAsync().Result;
        }

        /// <summary>
        /// Gets a summary of all events that have occurred.
        /// </summary>
        /// <returns>Summary</returns>
        public async Task<Summary> SummaryAsync()
        {
            using (HttpClient httpClient = GetHttpClient(TelemetryServiceRoot))
            {
                Uri endPoint = new Uri($"{TelemetryServiceRoot}/api/summary");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, endPoint);
                AddSignatureHeader(requestMessage, endPoint);
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                if (ExceptionOccurred(responseMessage))
                {
                    return new Summary { ApiException = new ApiException(responseMessage) };
                }

                string responseJson = await responseMessage.Content.ReadAsStringAsync();
                return Deserialize.FromJson<Summary>(responseJson);
            }
        }

        /// <summary>
        /// Gets the signing secret for this client.  Implements ISecretProvider.GetSecret().
        /// </summary>
        /// <returns>string</returns>
        public string GetSecret()
        {
            return TelemetrySessionData.SessionSecret;
        }

        private readonly object startSessionLock = new object();
        private TelemetrySessionData telemetrySessionData;

        /// <summary>
        /// Gets data representing a telemetry session.
        /// </summary>
        /// <value>
        /// Data representing a telemetry session.
        /// </value>
        protected TelemetrySessionData TelemetrySessionData
        {
            get
            {
                lock (startSessionLock)
                {
                    if (telemetrySessionData == null)
                    {
                        telemetrySessionData = new TelemetrySessionData(StartSession(Environment.MachineName));
                    }
                }

                return telemetrySessionData;
            }

            private set
            {
                telemetrySessionData = value;
            }
        }

        private readonly Dictionary<string, StartSessionResponse> telemetrySessions = new Dictionary<string, StartSessionResponse>();
        private readonly object telemetrySessionLock = new object();

        /// <summary>
        /// Starts a telemetry session.
        /// </summary>
        /// <param name="machineName">The maching name.</param>
        /// <returns>StartSessionResponse</returns>
        public StartSessionResponse StartSession(string machineName)
        {
            lock (telemetrySessionLock)
            {
                if (!telemetrySessions.ContainsKey(machineName))
                {
                    telemetrySessions.Add(
                        machineName,
                        Policy
                           .Handle<Exception>()
                           .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), PreRetryHandler)
                           .Execute(() =>
                           {
                               HttpClient httpClient = new HttpClient();
                               Uri endpoint = new Uri($"{TelemetryServiceRoot}/api/sessionid?machineName={machineName}");
                               HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
                               HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                               if (ExceptionOccurred(responseMessage))
                               {
                                   return new StartSessionResponse(LastException);
                               }

                               string responseJson = responseMessage.Content.ReadAsStringAsync().Result;
                               StartSessionResponse startSessionResponse = Deserialize.FromJson<StartSessionResponse>(responseJson);
                               TelemetrySessionData = new TelemetrySessionData(startSessionResponse);
                               return startSessionResponse;
                           }));
                }
            }

            return telemetrySessions[machineName];
        }

        /// <summary>
        /// Ends a telemetry session.
        /// </summary>
        public void EndSession()
        {
            EndSession(TelemetrySessionData.TelemetrySessionId);
        }

        /// <summary>
        /// Ends the specified telemetry session.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        public void EndSession(string sessionId)
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), PreRetryHandler)
                .Execute(() =>
                {
                    HttpClient httpClient = new HttpClient();
                    Uri endpoint = new Uri($"{TelemetryServiceRoot}/api/{sessionId}");
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, endpoint);
                    HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                    FireExceptionEventIfExceptionOccurred(responseMessage);
                });
        }

        private void FireExceptionEventIfExceptionOccurred(HttpResponseMessage responseMessage)
        {
            ExceptionOccurred(responseMessage);
        }

        private bool ExceptionOccurred(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                LastException = new ApiException(responseMessage);
                ApiException?.Invoke(this, new ApiExceptionEventArgs(LastException));
                return true;
            }

            return false;
        }

        private void AddSignatureHeader(HttpRequestMessage requestMessage, Uri uri)
        {
            AddSignatureHeader<IncrementRequest>(requestMessage, null, uri);
        }

        private void AddSignatureHeader<T>(HttpRequestMessage requestMessage, T data, Uri uri)
            where T : Serializable, new()
        {
            SignatureCalculator signatureCalculator = new SignatureCalculator(this);
            string signature = signatureCalculator.CalculateSignature(data, uri);
            requestMessage.Headers.Add(TelemetryConstants.SignatureHeader, signature);
        }

        private HttpClient GetHttpClient(string baseAddress)
        {
            return GetHttpClient(baseAddress, new Cookie(TelemetryConstants.SessionIdCookie, TelemetrySessionData.TelemetrySessionId));
        }

        private HttpClient GetHttpClient(string baseAddress, params Cookie[] cookies)
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpClientHandler httpClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };
            HttpClient httpClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(baseAddress) };
            foreach (Cookie cookie in cookies)
            {
                cookieContainer.Add(new Uri(baseAddress), cookie);
            }

            return httpClient;
        }
    }
}
