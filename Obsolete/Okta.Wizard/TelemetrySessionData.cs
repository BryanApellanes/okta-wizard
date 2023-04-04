// <copyright file="TelemetrySessionData.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using DevEx.Telemetry.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data used to identify a telemetry session.
    /// </summary>
    public class TelemetrySessionData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetrySessionData"/> class.
        /// </summary>
        public TelemetrySessionData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetrySessionData"/> class.
        /// </summary>
        /// <param name="response">The start session response</param>
        public TelemetrySessionData(StartSessionResponse response)
        {
            StartSessionResponse = response;
            Created = response.Created;
            MachineName = response.MachineName;
            SessionSecret = response.SessionSecret;
            TelemetrySessionId = response.TelemetrySessionId;
        }

        internal StartSessionResponse StartSessionResponse { get; set; }

        /// <summary>
        /// Gets the status of starting the telemetry session.
        /// </summary>
        /// <value>
        /// The status of starting the telemetry session.
        /// </value>
        public StartSessionStatus Status { get => StartSessionResponse?.Status ?? StartSessionStatus.Invalid; }

        /// <summary>
        /// Gets or sets the date and time created.
        /// </summary>
        /// <value>
        /// The date and time created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the machine name.
        /// </summary>
        /// <value>
        /// The machine name.
        /// </value>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the session secret.
        /// </summary>
        /// <value>
        /// The session secret.
        /// </value>
        public string SessionSecret { get; set; }

        /// <summary>
        /// Gets or sets the telemetry session id.
        /// </summary>
        /// <value>
        /// The telemetry session id.
        /// </value>
        public string TelemetrySessionId { get; set; }
    }
}
