// <copyright file="UserManagerShould.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Threading;
using DevEx.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;
using Okta.Wizard.Messages;

namespace OktaVisualStudioWizard.Tests.Integration
{
    [TestClass]
    public class UserManagerShould
    {
        [TestMethod]
        public void GetUser()
        {
            UserManager userManager = new UserManager();
            string userId = null;
            try
            {
                UserProfile testUserProfile = CreateTestUserProfile();
                string testPassword = UserProfile.CreateValidPassword();
                UserCreationResponse creationResponse = userManager.CreateUserAsync(testUserProfile, testPassword).Result;
                userId = creationResponse.Id;

                bool? gettingEventFired = false;
                bool? gotEventFired = false;
                userManager.GettingUser += (s, a) => gettingEventFired = true;
                userManager.GotUser += (s, a) => gotEventFired = true;
                GetUserResponse userResponse = userManager.GetUserAsync(userId).Result;
                userResponse.ApiException.Should().BeNull();
                userResponse.Profile.Should().Be(testUserProfile);
                gettingEventFired.Should().BeTrue();
                gotEventFired.Should().BeTrue();
            }
            finally
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    userManager.DeleteUserAsync(userId).Wait();
                    userManager.DeleteUserAsync(userId).Wait();
                }
            }
        }

        [TestMethod]
        public void CreateUser()
        {
            UserManager userManager = new UserManager();
            string userId = null;
            try
            {
                UserProfile testUserProfile = CreateTestUserProfile();
                bool? creatingEventFired = false;
                bool? createdEventFired = false;

                string testPassword = UserProfile.CreateValidPassword();
                userManager.CreatingUser += (s, a) => creatingEventFired = true;
                userManager.CreatedUser += (s, a) => createdEventFired = true;

                UserCreationResponse response = userManager.CreateUserAsync(testUserProfile, testPassword).Result;
                creatingEventFired.Should().BeTrue();
                createdEventFired.Should().BeTrue();
                response.Id.Should().NotBeNullOrEmpty();
                userId = response.Id;
                response.Status.Should().Be("ACTIVE");
            }
            finally
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    userManager.DeleteUserAsync(userId).Wait();
                    userManager.DeleteUserAsync(userId).Wait();
                }
            }
        }

        [TestMethod]
        public void AssignUserToApplication()
        {
            UserManager userManager = new UserManager(ApiCredentials.Default, new ClientApplicationRegistrationManager(ApiCredentials.Default));
            ApplicationRegistrationManager applicationRegistrationManager = new ClientApplicationRegistrationManager();
            string userId = null;
            string clientApplicationId = null;
            try
            {
                UserProfile testUserProfile = CreateTestUserProfile();
                string password = UserProfile.CreateValidPassword();
                UserCreationResponse userCreationResponse = userManager.CreateUserAsync(testUserProfile, password).Result;
                userId = userCreationResponse.Id;
                ApplicationRegistrationResponse applicationRegistrationResponse =  applicationRegistrationManager.RegisterApplicationAsync($"Test-App-{nameof(AssignUserToApplication)}").Result;
                clientApplicationId = applicationRegistrationResponse.ClientId;
                AssignUserToApplicationRequest assignUserToApplicationRequest = new AssignUserToApplicationRequest(userCreationResponse.Id)
                {
                    Credentials = new UserCredentials
                    {
                        UserName = testUserProfile.Login,
                        Password = new UserPassword
                        {
                            Value = password
                        }
                    }
                };
                AssignUserToApplicationResponse assignUserResponse = userManager.AssignUserToApplicationAsync(applicationRegistrationResponse.ClientId, assignUserToApplicationRequest).Result;

                assignUserResponse.Should().NotBeNull();
                assignUserResponse.Status.Should().Be("ACTIVE");
                assignUserResponse.SyncState.Should().Be("DISABLED");
            }
            finally
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    userManager.DeleteUserAsync(userId).Wait();
                    userManager.DeleteUserAsync(userId).Wait();
                }

                if (!string.IsNullOrEmpty(clientApplicationId))
                {
                    applicationRegistrationManager.DeleteClientApplicationAsync(clientApplicationId).Wait();
                }
                
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            UserManager userManager = new UserManager();
            string userId = null;
            try
            {
                UserProfile testUserProfile = CreateTestUserProfile();
                bool? deletingEventFired = false;
                bool? deletedEventFired = false;

                string testPassword = UserProfile.CreateValidPassword();
                userManager.DeletingUser += (s, a) => deletingEventFired = true;
                userManager.DeletedUser += (s, a) => deletedEventFired = true;

                UserCreationResponse response = userManager.CreateUserAsync(testUserProfile, testPassword).Result;
                response.Id.Should().NotBeNullOrEmpty();
                userId = response.Id;
                response.Status.Should().Be("ACTIVE");

                userManager.DeleteUserAsync(userId).Wait();
                deletingEventFired.Should().BeTrue();
                deletedEventFired.Should().BeTrue();
                Thread.Sleep(1500);
                GetUserResponse getUserResponse = userManager.GetUserAsync(userId).Result;
                getUserResponse.Status.Should().Be("DEPROVISIONED");
            }
            finally
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    userManager.DeleteUserAsync(userId).Wait();
                    Thread.Sleep(1500);
                    userManager.DeleteUserAsync(userId).Wait();
                }
            }
        }

        private static UserProfile CreateTestUserProfile()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string first = 4.RandomCharacters(alphabet);
            string last = 4.RandomCharacters(alphabet);
            UserProfile testUserProfile = new UserProfile
            {
                FirstName = first,
                LastName = last,
                Email = $"{first}.{last}@example.com",
                Login = $"{first}.{last}@example.com",
                MobilePhone = "555-123-4567"
            };
            return testUserProfile;
        }
    }
}
