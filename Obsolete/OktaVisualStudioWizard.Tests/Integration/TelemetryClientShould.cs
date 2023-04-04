// <copyright file="TelemetryClientShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Threading;
using DevEx.Telemetry.Messages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;

namespace OktaVisualStudioWizard.Tests.Integration
{
    [TestClass]
    public class TelemetryClientShould
    {
        [TestMethod]
        public void IncrementEventCounter()
        {
            TelemetryClient client = new TelemetryClient(OktaWizardConfig.DefaultTelemetryServiceRoot);
            string testEventName = "TEST_EVENT_NAME";
            IncrementResponse response = client.IncrementEventCounterAsync(testEventName).Result;
            response.EventName.Should().Be(testEventName);
            response.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void GetSummary()
        {
            TelemetryClient client = new TelemetryClient(OktaWizardConfig.DefaultTelemetryServiceRoot);
            string testEventName = "TEST_SUMMARY_EVENT_NAME";
            client.IncrementEventCounterAsync(testEventName).Wait();
            Thread.Sleep(1000);
            Summary summary = client.Summary();
            summary.SessionCount.Should().BeGreaterThan(0);
            summary.EventSummaries.Should().NotBeNull();
            summary.EventSummaries.Count.Should().BeGreaterThan(0);
            Console.WriteLine(summary.ToJson());
        }
    }
}
