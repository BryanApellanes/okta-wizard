// <copyright file="TelemetryEventReporter.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Reflection;
using System.Threading.Tasks;
using DevEx;
using DevEx.Shared;

namespace Okta.Wizard.Telemetry
{
    /// <summary>
    /// A component that reports telemetry data.
    /// </summary>
    public class TelemetryEventReporter : ITelemetryEventReporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryEventReporter"/> class.
        /// </summary>
        /// <param name="asyncTelemetryService">The async telementry service</param>
        public TelemetryEventReporter(IAsyncTelemetryService asyncTelemetryService)
        {
            TelemetryService = asyncTelemetryService;
        }

        /// <summary>
        /// Gets or sets the telemetry service.
        /// </summary>
        /// <value>
        /// The telemetry service.
        /// </value>
        protected IAsyncTelemetryService TelemetryService { get; set; }

        /// <summary>
        /// Tries to increment the event counter for the specified event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        public void TryRequestIncrement(string eventName)
        {
            try
            {
                RequestIncrement(eventName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception trying to increment event counter: {ex.Message}");
            }
        }

        /// <summary>
        /// Subscribe this reporter to the telemtry events of the specified event source.
        /// </summary>
        /// <param name="eventSource">The event source.</param>
        public void SubscribeToTelemetryEvents(ITelemetryEventSource eventSource)
        {
            Type type = eventSource.GetType();
            EventInfo[] events = type.GetEvents();
            foreach (EventInfo eventInfo in events)
            {
                TelemetryEventAttribute attr = eventInfo.GetCustomAttribute<TelemetryEventAttribute>();
                if (attr != null)
                {
                    eventInfo.AddEventHandler(eventSource, (EventHandler)((s, a) => TryRequestIncrement(attr.EventName)));
                }
            }
        }

        private void RequestIncrement(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException("eventName must be specified");
            }

            Task.Run(() =>
            {
                try
                {
                    TelemetryService.IncrementEventCounterAsync(eventName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in task incrementing event counter: {ex.Message}");
                }
            });
        }
    }
}
