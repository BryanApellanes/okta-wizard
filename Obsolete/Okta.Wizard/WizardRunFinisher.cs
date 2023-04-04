// <copyright file="WizardRunFinisher.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DevEx.Extensions;
using Okta.Wizard.Internal;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// A component for executing additional logic when the wizard finishes.
    /// </summary>
    public class WizardRunFinisher : IWizardRunFinisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardRunFinisher"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public WizardRunFinisher(IUserManager userManager)
        {
            this.Notify = (msg, sev) => { };
            this.CustomFinalizer = (s) => { };
            this.UserManager = userManager;
        }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public IUserManager UserManager { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred creating a user if any. May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred creating a user if any. May be null.
        /// </value>
        public Exception CreateUserException { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred assigning a user to an application if any. May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred assigning a user to an application if any. May be null.
        /// </value>
        public Exception AssigningUserToApplicationException { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred when wizard run finished.
        /// </summary>
        /// <value>
        /// The exception that occurred when wizard run finished.
        /// </value>
        public Exception FinishException { get; set; }

        /// <summary>
        /// Gets or sets the exception that occurred performin variable replacements.
        /// </summary>
        /// <value>
        /// The exception that occurred performin variable replacements.
        /// </value>
        public Exception OktaReplacementException { get; set; }

        /// <summary>
        /// Gets or sets a delegate used to emit notifications of interest.
        /// </summary>
        /// <value>
        /// A delegate used to emit notifications of interest.
        /// </value>
        public Action<string, Severity> Notify { get; set; }

        /// <summary>
        /// Gets or sets the final operation that is executed on Finalize.  May be null.
        /// </summary>
        /// <value>
        /// The final operation that is executed on Finalize.  May be null.
        /// </value>
        public Action<OktaWizardResult> CustomFinalizer { get; set; }

        /// <summary>
        /// The event that is raised when an exception occurs creating a user.
        /// </summary>
        public event EventHandler CreateUserExceptionOccurred;

        /// <summary>
        /// The event that is raised when an exception occurs assigning a user to an application.
        /// </summary>
        public event EventHandler AssigningUserToApplicationExceptionOccurred;

        /// <summary>
        /// The event that is raised when an exception occurs finishing the wizard run.
        /// </summary>
        public event EventHandler FinishExceptionOccurred;

        /// <summary>
        /// Finishes the wizard run by creating a test user if appropriate and any final variable replacements.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <returns>WizardRunFinishedResult</returns>
        public virtual WizardRunFinishedResult RunFinished(OktaWizardResult oktaWizardResult)
        {
            return RunFinishedAsync(oktaWizardResult).Result;
        }

        /// <summary>
        /// Finishes the wizard run by creating a test user if appropriate and any final variable replacements.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <returns>Task{WizardRunFinishedResult}</returns>
        public virtual async Task<WizardRunFinishedResult> RunFinishedAsync(OktaWizardResult oktaWizardResult)
        {
            WizardRunFinishedResult result = new WizardRunFinishedResult
            {
                Status = WizardRunFinishedStatus.Success,
            };
            try
            {
                if (oktaWizardResult != null && oktaWizardResult.Status == WizardStatus.Success && !oktaWizardResult.ApplicationExists && oktaWizardResult.ShouldCreateUser)
                {
                    await WriteTestUserFile(oktaWizardResult, result);
                }

                await this.DoOktaReplacementsAsync(oktaWizardResult);
                CustomFinalizer?.Invoke(oktaWizardResult);
            }
            catch (Exception ex)
            {
                FinishException = ex;
                FinishExceptionOccurred?.Invoke(this, new WizardRunFinishedExceptionEventArgs { Exception = ex, WizardRunFinisher = this });
                result = new WizardRunFinishedResult(ex);
            }

            return result;
        }

        /// <summary>
        /// Does final Okta specific variable replacements.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <returns>Task{FileInfo[]}</returns>
        public virtual async Task<FileInfo[]> DoOktaReplacementsAsync(OktaWizardResult oktaWizardResult)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return DoOktaReplacements(oktaWizardResult);
                }
                catch (Exception ex)
                {
                    OktaReplacementException = ex;
                    return new FileInfo[] { };
                }
            });
        }

        /// <summary>
        /// Perform final Okta specific variable replacements.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <returns>FileInfo[]</returns>
        protected virtual FileInfo[] DoOktaReplacements(OktaWizardResult oktaWizardResult)
        {
            DirectoryInfo projectRoot = new DirectoryInfo(oktaWizardResult.ProjectData.DestinationDirectory);
            List<FileInfo> results = new List<FileInfo>();
            foreach (string file in oktaWizardResult.OktaWizardConfig.OktaReplacementFiles)
            {
                foreach (FileInfo fi in projectRoot.GetFiles(file, SearchOption.AllDirectories))
                {
                    string content = File.ReadAllText(fi.FullName);
                    string replacedContent = GetOktaReplacedString(oktaWizardResult, content);
                    File.WriteAllText(fi.FullName, replacedContent);
                    results.Add(fi);
                }
            }

            string oktaReplacementsFile = Path.Combine(projectRoot.FullName, $"{nameof(OktaReplacementSettings)}.yaml");
            if (File.Exists(oktaReplacementsFile))
            {
                try
                {
                    OktaReplacementSettings oktaReplacementSettings = Deserialize.FromYamlFile<OktaReplacementSettings>(oktaReplacementsFile);
                    foreach (string file in oktaReplacementSettings.FileNames)
                    {
                        foreach (FileInfo fi in projectRoot.GetFiles(file, SearchOption.AllDirectories))
                        {
                            string content = File.ReadAllText(fi.FullName);
                            string replacedContent = content;
                            foreach (OktaReplacement oktaReplacement in oktaReplacementSettings.OktaReplacements)
                            {
                                replacedContent = oktaReplacement.Apply(oktaWizardResult, replacedContent);
                            }

                            File.WriteAllText(fi.FullName, replacedContent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OktaReplacementException = ex;
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// Gets a string representing the specified input with Okta variable replacements applied.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <param name="input">input</param>
        /// <returns>string</returns>
        protected string GetOktaReplacedString(OktaWizardResult oktaWizardResult, string input)
        {
            Uri oktaUri = oktaWizardResult.OktaWizardConfig.GetDomainUri();
            string oktaDomain = oktaUri.Authority;
            return input
                .Replace("{yourOktaDomain}", oktaDomain)
                .Replace("{ClientId}", oktaWizardResult.ApplicationCredentials.ClientId)
                .Replace("{ClientSecret}", oktaWizardResult.ApplicationCredentials.ClientSecret)
                .Replace("{ApplicationName}", oktaWizardResult.ApplicationName);
        }

        /// <summary>
        /// Creates a user and assigns them to the application.
        /// </summary>
        /// <param name="oktaApplicationSettings">The Okta application settings.</param>
        /// <returns>Task{TestUser}</returns>
        protected async Task<TestUser> CreateAndAssignTestUserToApplicationAsync(OktaApplicationSettings oktaApplicationSettings)
        {
            AssignUserToApplicationRequest request = null;
            TestUser result = null;
            try
            {
                TestUser testUser = await CreateTestUserAsync(oktaApplicationSettings);
                if (testUser != null)
                {
                    AssignUserToApplicationRequest assignUserToApplicationRequest = new AssignUserToApplicationRequest(testUser.Id)
                    {
                        Credentials = new UserCredentials
                        {
                            UserName = testUser.UserProfile.Login,
                            Password = new UserPassword
                            {
                                Value = testUser.Password,
                            },
                        },
                    };
                    AssignUserToApplicationResponse assignUserResponse = await UserManager.AssignUserToApplicationAsync(oktaApplicationSettings.ApplicationCredentials.ClientId, assignUserToApplicationRequest);
                    if (!assignUserResponse.Success)
                    {
                        throw assignUserResponse.ApiException;
                    }

                    result = testUser;
                }
            }
            catch (Exception ex)
            {
                AssigningUserToApplicationException = ex;
                AssigningUserToApplicationExceptionOccurred?.Invoke(this, new AssignUserToApplicationEventArgs(oktaApplicationSettings?.ApplicationCredentials?.ClientId, request) { Exception = ex });
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Creates a test user.
        /// </summary>
        /// <param name="oktaApplicationSettings">The Okta application settings.</param>
        /// <returns>Task{TestUser}</returns>
        protected async Task<TestUser> CreateTestUserAsync(OktaApplicationSettings oktaApplicationSettings)
        {
            UserProfile testUserProfile = null;
            try
            {
                UserManager.ApiCredentials = oktaApplicationSettings.ApiCredentials;
                string safeApplicationName = oktaApplicationSettings.ApplicationName.Replace(" ", "_").ToLowerInvariant();
                string first = $"Test-User-{safeApplicationName}";
                string last = $"{6.RandomCharacters("abcdefghijklmnopqrstuvwxyz")}{3.RandomCharacters("1234567890")}";
                string email = $"{first}.{last}@example.com";
                testUserProfile = new UserProfile
                {
                    FirstName = first,
                    LastName = last,
                    Email = email,
                    Login = email,
                    MobilePhone = "555-123-4567",
                };
                string testPassword = UserProfile.CreateValidPassword();
                UserCreationResponse response = await UserManager.CreateUserAsync(testUserProfile, testPassword);
                return new TestUser { Id = response.Id, Password = testPassword, UserProfile = response.Profile };
            }
            catch (Exception ex)
            {
                CreateUserException = ex;
                CreateUserExceptionOccurred?.Invoke(this, new CreateUserEventArgs { UserProfile = testUserProfile, Exception = ex });
                Notify($"An exception occurred creating a test user: {ex.Message}\r\n\r\n{ex.StackTrace}", Severity.Error);
            }

            return null;
        }

        private async Task WriteTestUserFile(OktaWizardResult oktaWizardResult, WizardRunFinishedResult result)
        {
            TestUser testUser = await CreateAndAssignTestUserToApplicationAsync(oktaWizardResult.OktaApplicationSettings);
            if (testUser != null)
            {
                FileInfo testUserFile = new FileInfo(Path.Combine(oktaWizardResult.ProjectData.DestinationDirectory, "test-user.yaml"));
                testUser.ToYamlFile(testUserFile.FullName);
                result.TestUser = testUser;
                result.TestUserFile = testUserFile;
            }
        }
    }
}
