// <copyright file="ClientApplicationRegistrationManagerShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;
using Okta.Wizard.Messages;

namespace OktaVisualStudioWizard.Tests.Integration
{
    [TestClass]
    public class ClientApplicationRegistrationManagerShould
    {
        [TestMethod]
        public void ListApplications()
        {
            ApplicationRegistrationManager registration = new ClientApplicationRegistrationManager();
            ApplicationListResponse[] response = registration.ListApplicationsAsync().Result;
            response.Should().NotBeNull();
            response.Length.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void RegisterClientApplication()
        {

            ApplicationRegistrationManager registrationManager = new ClientApplicationRegistrationManager();
            string clientId = string.Empty;
            try
            {
                string testApplicationName = $"Visual Studio Test App - ({nameof(RegisterClientApplication)} Test)";
                ApplicationRegistrationResponse response = registrationManager.RegisterApplicationAsync(testApplicationName).Result;
                response.ClientId.Should().NotBeNullOrEmpty();
                clientId = response.ClientId;
                response.ClientSecret.Should().NotBeNullOrEmpty();
                response.ClientName.Should().Be(testApplicationName);
            }
            finally
            {
                if (!string.IsNullOrEmpty(clientId))
                {
                    registrationManager.DeleteClientApplicationAsync(clientId).Wait();
                }
            }
        }

        [TestMethod]
        public void FindClientApplicationByName()
        {
            ApplicationRegistrationManager registrationManager = new ClientApplicationRegistrationManager();
            string clientId = string.Empty;
            try
            {
                string testApplicationName = $"Visual Studio Test App - ({nameof(FindClientApplicationByName)} Test)";
                ApplicationRegistrationResponse registrationResponse = registrationManager.RegisterApplicationAsync(testApplicationName).Result;
                clientId = registrationResponse.ClientId;
                ApplicationFindResponse[] findResponse = registrationManager.FindApplicationsAsync(testApplicationName, 1).Result;
                findResponse.Length.Should().Be(1);
            }
            finally
            {
                if (!string.IsNullOrEmpty(clientId))
                {
                    registrationManager.DeleteClientApplicationAsync(clientId).Wait();
                }
            }
        }

        [TestMethod]
        public void DeleteClientApplication()
        {
            ApplicationRegistrationManager registration = new ClientApplicationRegistrationManager();
            ApplicationListResponse[] listResponse = registration.ListApplicationsAsync().Result;
            listResponse.Should().NotBeNull();
            int initialCount = listResponse.Length;
            initialCount.Should().BeGreaterThan(0);

            string testApplicationName = "Visual Studio Test App";
            ApplicationRegistrationResponse response = registration.RegisterApplicationAsync(testApplicationName).Result;
            response.ClientId.Should().NotBeNullOrEmpty();
            string clientId = response.ClientId;
            response.ClientSecret.Should().NotBeNullOrEmpty();
            response.ClientName.Should().Be(testApplicationName);

            listResponse = registration.ListApplicationsAsync().Result;
            int newCount = listResponse.Length;
            newCount.Should().Be(initialCount + 1);

            ApiStatusResponse status = registration.DeleteClientApplicationAsync(clientId).Result;
            status.HttpStatusCode.Should().Be(HttpStatusCode.NoContent);
            status.Error.Should().BeNullOrEmpty();

            listResponse = registration.ListApplicationsAsync().Result;
            int finalCount = listResponse.Length;
            finalCount.Should().Be(initialCount);
        }
    }
}
