// <copyright file="Program.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using Okta.Wizard;
using Okta.Wizard.Automation;
using Okta.Wizard.Automation.Okta;
using System;
using System.IO;

namespace Okta
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ApiKeyErrorResponse errorResponse = new ApiKeyErrorResponse();
                DataArguments dataArguments = GetDataArguments(args);
                UserSignInCredentials userSignInCredentials = GetUserSignInCredentials(dataArguments);
                if (!userSignInCredentials.IsValid)
                {
                    throw new InvalidOperationException("Failed to get user sign in credentials.");
                }
                string orgName = GetOrgName(userSignInCredentials.SignInUrl);

                string tokenName = $"{orgName}__{userSignInCredentials.UserName}__{Environment.MachineName}"; // this is the name displayed in the Okta dashboard
                OktaApiToken oktaApiToken = new OktaApiToken(tokenName);
                EnsureOneApiTokenPageActionSequence ensureOneApiTokenPageActionSequence = new EnsureOneApiTokenPageActionSequence(userSignInCredentials, tokenName);
                ensureOneApiTokenPageActionSequence.EnableDebug(OktaWizardConfig.ScreenShotsDirectory);
                ensureOneApiTokenPageActionSequence.SignInFailed += (sender, args) =>
                {
                    OktaSignInFailedEventArgs oktaSignInFailedEventArgs = (OktaSignInFailedEventArgs)args;
                    errorResponse.OktaSignInFailedEventArgs = oktaSignInFailedEventArgs;
                    Console.WriteLine("Sign In Failed: {0}", oktaSignInFailedEventArgs.Message);
                };
                ensureOneApiTokenPageActionSequence.TokenCreated += (sender, args) =>
                {
                    TokenCreatedEventArgs tokenCreatedEventArgs = (TokenCreatedEventArgs)args;
                    oktaApiToken = new OktaApiToken(tokenCreatedEventArgs.TokenName) { SignInUrl = userSignInCredentials.SignInUrl, Value = tokenCreatedEventArgs.TokenValue };
                    FileInfo oktaApiTokenFile = oktaApiToken.Save(dataArguments.OutputFile);
                    Console.WriteLine("Okta API token written to: ({0})", oktaApiTokenFile.FullName);
                };
                ensureOneApiTokenPageActionSequence.Failure += (sender, args) =>
                {
                    PageActionSequenceEventArgs pageActionSequenceEventArgs = (PageActionSequenceEventArgs)args;
                    errorResponse.PageActionResults = pageActionSequenceEventArgs.Results;
                    errorResponse.Message = pageActionSequenceEventArgs.Exception?.Message;
                    errorResponse.StackTrace = pageActionSequenceEventArgs.Exception?.StackTrace;
                };
                ensureOneApiTokenPageActionSequence.ExecuteAsync().Wait();
                Console.WriteLine();
                if (errorResponse.FailureOccurred)
                {
                    errorResponse.Save();
                    Console.WriteLine(errorResponse.ToJson(true));
                }
                else
                {
                    Console.WriteLine(oktaApiToken.ToJson(true));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return 1;
            }
            finally
            {
                AutomationPage.TryCloseBrowser();
            }

            return 0;
        }

        private static DataArguments GetDataArguments(string[] args)
        {
            FileInfo userCredentialsFile = new FileInfo(UserSignInCredentials.DefaultFilePath);
            FileInfo apiTokenFile = new FileInfo(OktaApiToken.DefaultFilePath);
            if(args != null && args.Length == 2)
            {
                userCredentialsFile = new FileInfo(args[0]);
                apiTokenFile = new FileInfo(args[1]);
            }
            if (args == null || args.Length == 1)
            {
                userCredentialsFile = new FileInfo(args[0]);
            }

            return new DataArguments
            {
                InputFile = userCredentialsFile.FullName,
                OutputFile = apiTokenFile.FullName
            };
        }

        private static UserSignInCredentials GetUserSignInCredentials(DataArguments dataArguments)
        {
            string userSignInCredentialsFilePath = dataArguments.InputFile;
            UserSignInCredentials result = new UserSignInCredentials();
            if(File.Exists(userSignInCredentialsFilePath))
            {
                result = UserSignInCredentials.FromEncrypedJsonFile(userSignInCredentialsFilePath);
                if(result.IsValid)
                {
                    Console.WriteLine("using credentials from: ({0})", userSignInCredentialsFilePath);
                    return result;
                }
            }

            return result;
        }

        private static string GetOrgName(string url)
        {
            Uri uri = new Uri(url);
            string host = uri.Host;
            string[] segments = host.Split(".", StringSplitOptions.RemoveEmptyEntries);
            if(segments.Length == 3)
            {
                return segments[0];
            }
            return host.Replace(".", "_");
        }
    }
}
