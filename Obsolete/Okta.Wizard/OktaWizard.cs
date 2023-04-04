// <copyright file="OktaWizard.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DevEx;
using DevEx.Shared;
using Microsoft.AspNetCore.DataProtection;
using Okta.Wizard.Binding;
using Okta.Wizard.Messages;

namespace Okta.Wizard
{
    /// <summary>
    /// Component used to initialize a Visual Studio project.
    /// </summary>
    public class OktaWizard : ServiceClient, ITelemetryEventSource
    {
        public const string ApiKeyToolOutputTextFile = "./apikeytooloutput.txt";
        private readonly IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable> oktaApplicationSettingsPromptProvider;
        private readonly IPromptProvider<ApplicationCredentials> applicationCredentialsPromptProvider;
        private readonly IWizardRunFinisherResolver wizardRunFinisherResolver;
        private readonly ITelemetryEventReporter telemetryEventReporter;
        private readonly IDataProtectionProvider dataProtectionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaWizard"/> class.
        /// </summary>
        protected OktaWizard()
        {
            this.WizardConfig = OktaWizardConfig.Default;
            this.ApplicationRegistrationManager = new ClientApplicationRegistrationManager(WizardConfig?.GetApiCredentials());
            this.Notify = (msg, sev) => Console.WriteLine(msg);
            this.TelemetrySessionId = $"{nameof(OktaWizard)}-{Environment.MachineName}-{Guid.NewGuid()}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaWizard"/> class.
        /// </summary>
        /// <param name="oktaApplicationSettingsPromptProvider">The Okta application settings prompt provider.</param>
        /// <param name="applicationCredentialsPromptProvider">The application Credentials prompt provider.</param>
        /// <param name="applicationRegistrationManager">The application registration manager.</param>
        /// <param name="wizardRunFinisherResolver">The wizard run finisher resolver.</param>
        /// <param name="telemetryEventReporter">The telemetry event reporter.</param>
        /// <param name="dataProtectionProvider">The data protection provider.</param>
        public OktaWizard(IPromptProvider<OktaApplicationSettings, OktaApplicationSettingsObservable> oktaApplicationSettingsPromptProvider, IPromptProvider<ApplicationCredentials> applicationCredentialsPromptProvider, IApplicationRegistrationManager applicationRegistrationManager, IWizardRunFinisherResolver wizardRunFinisherResolver, ITelemetryEventReporter telemetryEventReporter, IDataProtectionProvider dataProtectionProvider)
            : this()
        {
            this.oktaApplicationSettingsPromptProvider = oktaApplicationSettingsPromptProvider;
            this.applicationCredentialsPromptProvider = applicationCredentialsPromptProvider;
            this.wizardRunFinisherResolver = wizardRunFinisherResolver;
            this.telemetryEventReporter = telemetryEventReporter;
            this.dataProtectionProvider = dataProtectionProvider;

            this.ApplicationRegistrationManager = applicationRegistrationManager;
            this.ApplicationRegistrationManager.RegisteringApplication += (s, a) => RegisteringApplication?.Invoke(this, a);
            this.ApplicationRegistrationManager.RegisteredApplication += (s, a) => RegisteredApplication?.Invoke(this, a);
            this.ApplicationRegistrationManager.DeletingApplication += (s, a) => DeletingApplication?.Invoke(this, a);
            this.ApplicationRegistrationManager.DeletedApplication += (s, a) => DeletedApplication?.Invoke(this, a);

            this.CreateUserExceptionOccurred += (sender, args) => CreateUserException = ((CreateUserEventArgs)args).Exception;

            this.UserManager = wizardRunFinisherResolver.UserManager;
            this.UserManager.GettingUser += (s, a) => GettingUser?.Invoke(this, a);
            this.UserManager.GotUser += (s, a) => GotUser?.Invoke(this, a);
            this.UserManager.CreatingUser += (s, a) => CreatingUser?.Invoke(this, a);
            this.UserManager.CreatedUser += (s, a) => CreatedUser?.Invoke(this, a);
            this.UserManager.ActivatingUser += (s, a) => ActivatingUser?.Invoke(this, a);
            this.UserManager.ActivatedUser += (s, a) => ActivatedUser?.Invoke(this, a);
            this.UserManager.DeletingUser += (s, a) => DeletingUser?.Invoke(this, a);
            this.UserManager.DeletedUser += (s, a) => DeletedUser?.Invoke(this, a);
            this.UserManager.AssigningUserToApplication += (s, a) => AssigningUserToApplication(this, a);
            this.UserManager.AssignedUserToApplication += (s, a) => AssignedUserToApplication(this, a);
            this.UserManager.ApiException += (s, a) =>
            {
                ApiExceptionEventArgs apiExceptionEventArgs = (ApiExceptionEventArgs)a;
                CreateUserExceptionOccurred?.Invoke(this, new CreateUserEventArgs { Exception = apiExceptionEventArgs.ApiException });
            };

            SetOktaWizardOnPrompters();

            this.telemetryEventReporter.SubscribeToTelemetryEvents(this);
        }

        private static readonly Lazy<OktaWizard> _default = new Lazy<OktaWizard>(() => new OktaWizard());

        /// <summary>
        /// Gets the default wizard.
        /// </summary>
        /// <value>
        /// The default wizard.
        /// </value>
        public static OktaWizard Default
        {
            get => _default.Value;
        }

        /// <summary>
        /// Gets the telemetry session id.
        /// </summary>
        /// <value>
        /// The telemetry session id.
        /// </value>
        public string TelemetrySessionId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred loading the wizard config if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred loading the wizard config if any.  May be null.
        /// </value>
        public Exception OktaWizardConfigLoadException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred loading the config if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred loading the config if any.  May be null.
        /// </value>
        public Exception OktaConfigLoadException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred saving the config if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred saving the config if any.  May be null.
        /// </value>
        public Exception OktaConfigSaveException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred prompting for API credentials if any. May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred prompting for API credentials if any. May be null.
        /// </value>
        public Exception ApiCredentialsPromptException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred prompting for application settings if any. May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred prompting for application settings if any. May be null.
        /// </value>
        public Exception OktaApplicationSettingsPromptException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred prompting for application credentials if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred prompting for application credentials if any.  May be null.
        /// </value>
        public Exception ApplicationCredentialsPromptException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred assignint a user to an application if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred assignint a user to an application if any.  May be null.
        /// </value>
        public Exception AssigningUserToApplicationException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception that occurred creating a user if any.  May be null.
        /// </summary>
        /// <value>
        /// The exception that occurred creating a user if any.  May be null.
        /// </value>
        public Exception CreateUserException
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last result of running the wizard.
        /// </summary>
        /// <value>
        /// The last result of running the wizard.
        /// </value>
        public OktaWizardResult LastResult { get; private set; }

        /// <summary>
        /// The telemetry event that is raised before assigning a user to an application.
        /// </summary>
        [TelemetryEvent("OKTA_ASSIGNING_USER_TO_APPLICATION")]
        public event EventHandler AssigningUserToApplication;

        /// <summary>
        /// The telemetry event that is raised when a user is assigned to an application.
        /// </summary>
        [TelemetryEvent("OKTA_ASSIGNED_USER_TO_APPLICATION")]
        public event EventHandler AssignedUserToApplication;

        /// <summary>
        /// The telemetry event that is raised when an exception occurs assigning a user to an application.
        /// </summary>
        [TelemetryEvent("OKTA_ASSIGNING_USER_TO_APPLICATION_EXCEPTION")]
        public event EventHandler AssigningUserToApplicationExceptionOccurred;

        /// <summary>
        /// The telemetry event that is raised when the Okta config is updated.
        /// </summary>
        [TelemetryEvent("OKTA_CONFIG_UPDATED")]
        public event EventHandler OktaConfigUpdated; // TODO: review this for removal

        /// <summary>
        /// The telemetry event that is raised before the Okta wizard config is loaded.
        /// </summary>
        [TelemetryEvent("OKTA_WIZARD_CONFIG_LOADING")]
        public event EventHandler OktaWizardConfigLoading;

        /// <summary>
        /// The telemetry event that is raised when the Okta wizard config is loaded.
        /// </summary>
        [TelemetryEvent("OKTA_WIZARD_CONFIG_LOADED")]
        public event EventHandler OktaWizardConfigLoaded;

        /// <summary>
        /// The telemetry event that is raised when an exception occurs loading the Okta wizard config.
        /// </summary>
        [TelemetryEvent("OKTA_WIZARD_CONFIG_LOAD_EXCEPTION")]
        public event EventHandler OktaWizardConfigLoadExceptionOccurred;

        /// <summary>
        /// The telemetry event that is raised when the Okta wizard config is saved.
        /// </summary>
        [TelemetryEvent("OKTA_WIZARD_CONFIG_SAVED")]
        public event EventHandler OktaWizardConfigSaved;

        /// <summary>
        /// The telemetry event that is raised before prompting for application settings.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_SETTINGS_PROMPTING")]
        public event EventHandler OktaApplicationSettingsPrompting;

        /// <summary>
        /// The telemetery event that is raised after prompting for application settings.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_SETTINGS_PROMPTED")]
        public event EventHandler OktaApplicationSettingsPrompted;

        /// <summary>
        /// The telemetry event that is raised when an exception occurs prompting for application settings.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_SETTINGS_PROMPT_EXCEPTION")]
        public event EventHandler OktaApplicationSettingsPromptExceptionOccurred;

        /// <summary>
        /// The telemetry event that is raised before prompting for application credentials.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_CREDENTIALS_PROMPTING")]
        public event EventHandler ApplicationCredentialsPrompting;

        /// <summary>
        /// The telemetry event that is raised after prompting for application credentials.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_CREDENTIALS_PROMPTED")]
        public event EventHandler ApplicationCredentialsPrompted;

        /// <summary>
        /// The telemetry event that is raised when an exception occurs prompting for application credentials.
        /// </summary>
        [TelemetryEvent("OKTA_APPLICATION_CREDENTIALS_PROMPT_EXCEPTION")]
        public event EventHandler ApplicationCredentialsPromptExceptionOccurred;

        /// <summary>
        /// The telemetry event that is raised before registering an applicatoin.
        /// </summary>
        [TelemetryEvent("OKTA_REGISTERING_APPLICATION")]
        public event EventHandler RegisteringApplication;

        /// <summary>
        /// The telemetry event that is raised after registering an application.
        /// </summary>
        [TelemetryEvent("OKTA_REGISTERED_APPLICATION")]
        public event EventHandler RegisteredApplication;

        /// <summary>
        /// The telemetry event that is raised before deleting an application.
        /// </summary>
        [TelemetryEvent("OKTA_DELETING_APPLICATION")]
        public event EventHandler DeletingApplication;

        /// <summary>
        /// The telemetry event that is raised after deleting an application.
        /// </summary>
        [TelemetryEvent("OKTA_DELETED_APPLICATION")]
        public event EventHandler DeletedApplication;

        /// <summary>
        /// The telemetry event that is raised before getting a user.
        /// </summary>
        [TelemetryEvent("OKTA_GETTING_USER")]
        public event EventHandler GettingUser;

        /// <summary>
        /// The telemetry event that is raised after getting a user.
        /// </summary>
        [TelemetryEvent("OKTA_GOT_USER")]
        public event EventHandler GotUser;

        /// <summary>
        /// The telemetry event that is raised before creating a user.
        /// </summary>
        [TelemetryEvent("OKTA_CREATING_USER")]
        public event EventHandler CreatingUser;

        /// <summary>
        /// The telemetry event that is raised after creating a user.
        /// </summary>
        [TelemetryEvent("OKTA_CREATED_USER")]
        public event EventHandler CreatedUser;

        /// <summary>
        /// The telemetry event that is raised before activating a user.
        /// </summary>
        [TelemetryEvent("OKTA_ACTIVATING_USER")]
        public event EventHandler ActivatingUser;

        /// <summary>
        /// The telemetry event that is rased after activating a user.
        /// </summary>
        [TelemetryEvent("OKTA_ACTIVATED_USER")]
        public event EventHandler ActivatedUser;

        /// <summary>
        /// The telemetry event that is raised before deleting a user.
        /// </summary>
        [TelemetryEvent("OKTA_DELETING_USER")]
        public event EventHandler DeletingUser;

        /// <summary>
        /// The telemetry event that is raisd after deleting a user.
        /// </summary>
        [TelemetryEvent("OKTA_DELETED_USER")]
        public event EventHandler DeletedUser;

        /// <summary>
        /// The telemetry event that is raised when an excpetion occurs creating a user.
        /// </summary>
        [TelemetryEvent("OKTA_CREATE_USER_EXCEPTION")]
        public event EventHandler CreateUserExceptionOccurred;

        [TelemetryEvent("OKTA_EXECUTING_API_KEY_TOOL")]
        public event EventHandler ExecutingApiKeyTool;

        [TelemetryEvent("OKTA_EXECUTED_API_KEY_TOOL")]
        public event EventHandler ExecutedApiKeyTool;

        /// <summary>
        /// The event that is raised when an exception occurrs running the api key tool.
        /// </summary>
        [TelemetryEvent("OKTA_EXECUTE_API_KEY_TOOL_EXCEPTION")]
        public event EventHandler ExecuteApiKeyToolException;

        /// <summary>
        /// The event that is raised when the api key tool executes to completion but exits with a value of 1.
        /// </summary>
        [TelemetryEvent("OKTA_EXECUTE_API_KEY_TOOL_FAILED")]
        public event EventHandler ExecuteApiKeyToolFailed;

        [TelemetryEvent("OKTA_SAVED_USERSIGNINCREDENTIALS")]
        public event EventHandler SavedUserSignInCredentials;

        [TelemetryEvent("OKTA_SAVE_USERSIGNINCREDENTIALS_EXCEPTION")]
        public event EventHandler SaveUserSignInCredentialsException;

        [TelemetryEvent("OKTA_SAVED_OKTAAPITOKEN")]
        public event EventHandler SavedOktaApiToken;

        [TelemetryEvent("OKTA_SAVE_OKTAAPITOKEN_EXCEPTION")]
        public event EventHandler SaveOktaApiTokenException;

        /// <summary>
        /// Gets the Okta wizard config.
        /// </summary>
        /// <value>
        /// The Okta wizard config.
        /// </value>
        public OktaWizardConfig WizardConfig { get; private set; } // TODO: deprecate this property and related class

        public string OktaDomain
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the application registration manager.
        /// </summary>
        /// <value>
        /// The application registration manager.
        /// </value>
        public IApplicationRegistrationManager ApplicationRegistrationManager { get; protected set; }

        /// <summary>
        /// Gets or sets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public IUserManager UserManager { get; protected set; }

        /// <summary>
        /// Gets or sets the action executed when log events occur.
        /// </summary>
        /// <value>
        /// The action executed when log events occur.
        /// </value>
        public Action<string, Severity> Notify { get; set; }

        /// <summary>
        /// Runs the wizard asynchronously.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Task{OktaWizardResult}</returns>
        public async Task<OktaWizardResult> RunAsync(string applicationName)
        {
            return await RunAsync(new ProjectData { ProjectName = applicationName });
        }

        /// <summary>
        /// Runs the wizard asynchronously
        /// </summary>
        /// <param name="projectData">The project data.</param>
        /// <returns>Task{OktaWizardResult}</returns>
        public async Task<OktaWizardResult> RunAsync(ProjectData projectData)
        {
            if (projectData == null)
            {
                throw new ArgumentNullException(nameof(projectData), "ProjectData must be specified.");
            }

            string applicationName = projectData.ProjectName;

            OktaWizardResult result = new OktaWizardResult
            {
                ProjectData = projectData,
            };
            try
            {
                OktaApplicationSettingsObservable oktaApplicationSettingsObservable = new OktaApplicationSettingsObservable();
                // - preload usercredentials if they exist locally
                UserSignInCredentials userSignInCredentials = TryLoadLocalUserSignInCredentials();
                // - preload okta api token if it exists locally
                OktaApiToken oktaApiToken = TryLoadOktaApiToken();
                Task<OktaApiToken> oktaApiTokenGenerationTask = Task.FromResult(oktaApiToken);

                // if we have user credentials but no token
                // we should start the token generation process now so it has time to execute
                if (userSignInCredentials != null && oktaApiToken == null)
                {
                    oktaApiTokenGenerationTask = Task.Run(() => GetOktaApiToken(projectData, userSignInCredentials));
                }

                oktaApplicationSettingsObservable.ApiToken = oktaApiToken?.Value;
                oktaApplicationSettingsObservable.SignInUrl = userSignInCredentials?.SignInUrl;
                oktaApplicationSettingsObservable.UserName = userSignInCredentials?.UserName;
                oktaApplicationSettingsObservable.Password = userSignInCredentials?.Password;
                oktaApplicationSettingsObservable.ApplicationName = projectData.ProjectName;
                oktaApplicationSettingsObservable.VsTemplateName = projectData.GetSelectedVsTemplateName();
                oktaApplicationSettingsObservable.OktaApplicationType = projectData.GetOktaApplicationType();


                OktaApplicationSettings oktaApplicationSettings = null;
                // if we have an OktaApiToken that works
                // we can skip prompting for it
                if (oktaApiToken != null)
                {
                    OktaDomain = oktaApiToken.SignInUrl;
                    oktaApplicationSettingsObservable.ApiToken = oktaApiToken?.Value;
                    oktaApplicationSettingsObservable.SignInUrl = oktaApiToken.SignInUrl;
                    if (!ApiCredentialsWorks(oktaApiToken.ToApiCredentials()))
                    {
                        oktaApplicationSettings = await PromptForOktaApplicationSettingsAsync(oktaApplicationSettingsObservable);
                    }
                    else
                    {
                        //oktaApplicationSettingsObservable.ShowUserSignInCredentialsControl = false; // the token works, we don't need their credentials
                        oktaApplicationSettings = oktaApplicationSettingsObservable.ToOktaApplicationSettings();
                    }
                }

                if (oktaApplicationSettings == null && userSignInCredentials != null)
                {
                    // if we have user credentials
                    //      get the api key from the task that should be running
                    oktaApiToken = oktaApiTokenGenerationTask.Result;
                }

                // if we don't have okta application type show the OktaApplicationSettings prompt
                if (oktaApplicationSettingsObservable.OktaApplicationType == OktaApplicationType.None)
                {
                    oktaApplicationSettings = await PromptForOktaApplicationSettingsAsync(oktaApplicationSettingsObservable);
                }

                // if none of the previous conditions resulted in valid oktaApplicationSettings show the OktaApplicationSettings prompt
                if (oktaApplicationSettings == null)
                {
                    oktaApplicationSettings = await PromptForOktaApplicationSettingsAsync(oktaApplicationSettingsObservable);
                }

                if (oktaApiToken == null)
                {
                    SaveUserSignInCredentials(oktaApplicationSettings.UserSignInCredentials);
                    oktaApiToken = GetOktaApiToken(projectData, oktaApplicationSettings.UserSignInCredentials);
                    oktaApplicationSettings.ApiCredentials = new ApiCredentials { Domain = oktaApiToken.SignInUrl, Token = oktaApiToken.Value };
                }

                // if oktaApplicationSettings.ApiCredentials still doesn't work
                while (!ApiCredentialsWorks(oktaApplicationSettings.ApiCredentials))
                {
                    Thread.Sleep(1500);
                    oktaApplicationSettingsObservable.ShowUserSignInCredentialsControl = true;
                    Notify("That didn't seem to work.  Click the help links if you need assistance.", Severity.Warning);
                    oktaApplicationSettings = await PromptForOktaApplicationSettingsAsync(oktaApplicationSettingsObservable);

                    oktaApiToken = GetOktaApiToken(projectData, oktaApplicationSettings.UserSignInCredentials);
                    oktaApplicationSettings.ApiCredentials = new ApiCredentials { Domain = oktaApiToken.SignInUrl, Token = oktaApiToken.Value };
                }

                // save settings once we're here
                SaveUserSignInCredentials(oktaApplicationSettings.UserSignInCredentials);
                SaveOktaApiToken(oktaApiToken);

                // we need to have an api key/token by the time we get here
                ApplicationCredentials applicationCredentials = await GetApplicationCredentials(oktaApplicationSettings);

                result.OktaApplicationSettings = oktaApplicationSettings;
                result.ApplicationCredentials = applicationCredentials;
                result.ApplicationName = applicationName;
                result.Status = WizardStatus.Success;
                result.OktaWizardConfig = WizardConfig;
            }
            catch (Exception ex)
            {
                result.Status = WizardStatus.Error;
                result.Exception = ex;
            }

            LastResult = result;
            return result;
        }

        private OktaApiToken GetOktaApiToken(ProjectData projectData, UserSignInCredentials userSignInCredentials)
        {
            ExecuteApiKeyToolResult executeApiKeyToolResult = ExecuteApiKeyToolAsync(projectData, userSignInCredentials).Result;
            if (!executeApiKeyToolResult.Success)
            {
                ExecuteApiKeyToolFailed?.Invoke(this, new ApiKeyToolEventArgs { ApiKeyToolFile = executeApiKeyToolResult.ApiKeyToolFile, Exception = executeApiKeyToolResult.Exception, ProjectData = projectData, UserSignInCredentials = userSignInCredentials });
                Log($"An error occurred creating an api key: {executeApiKeyToolResult.Exception?.Message}\r\n{executeApiKeyToolResult.Exception?.StackTrace}");
            }

            return executeApiKeyToolResult.OktaApiToken;
        }

        private async Task<ApplicationCredentials> GetApplicationCredentials(OktaApplicationSettings oktaApplicationSettings)
        {
            AutoRegisterApplicationFormObservable autoRegisterApplicationFormObservable = new AutoRegisterApplicationFormObservable
            {
                ApplicationName = oktaApplicationSettings.ApplicationName,
                OktaDomain = oktaApplicationSettings.ApiCredentials.Domain,
                ApiToken = oktaApplicationSettings.ApiCredentials.Token,
                OktaApplicationType = oktaApplicationSettings.OktaApplicationType,
                ApplicationCredentials = new ApplicationCredentialsObservable()
                {
                    ApplicationName = oktaApplicationSettings.ApplicationName,
                },
            };
            ApplicationCredentials applicationCredentials = await PromptForApplicationCredentialsAsync(autoRegisterApplicationFormObservable);
            return applicationCredentials;
        }

        public virtual void SaveUserSignInCredentials(UserSignInCredentials userSignInCredentials)
        {
            try
            {
                userSignInCredentials.Save(UserSignInCredentials.DefaultFilePath);
                SavedUserSignInCredentials?.Invoke(this, new UserSignInCredentialsEventArgs { UserSignInCredentials = userSignInCredentials });
            }
            catch (Exception ex)
            {
                SaveUserSignInCredentialsException?.Invoke(this, new UserSignInCredentialsEventArgs { Exception = ex });
            }
        }

        public virtual void SaveOktaApiToken(OktaApiToken oktaApiToken)
        {
            try
            {
                oktaApiToken.Save(OktaApiToken.DefaultFilePath);
                SavedOktaApiToken?.Invoke(this, new OktaApiTokenEventArgs { OktaApiToken = oktaApiToken });
            }
            catch (Exception ex)
            {
                SaveOktaApiTokenException?.Invoke(this, new OktaApiTokenEventArgs { OktaApiToken = oktaApiToken, Exception = ex });
            }
        }

        /// <summary>
        /// Try to load user credentials from the specified encrypted file or UserSignInCredentials.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing user sign in credentials.</param>
        /// <returns>UserSignInCredentials or null</returns>
        public virtual UserSignInCredentials TryLoadLocalUserSignInCredentials(string filePath = null)
        {
            return UserSignInCredentials.TryLoad(filePath);
        }

        /// <summary>
        /// Try to load Okta API token from the specified encrypted file or OktaApiToken.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing Okta API token sign in credentials.</param>
        /// <returns>OktaApiToken or null</returns>
        public virtual OktaApiToken TryLoadOktaApiToken(string filePath = null)
        {
            return OktaApiToken.TryLoad(filePath);
        }

        public virtual ExecuteApiKeyToolResult ExecuteApiKeyTool(ProjectData projectData)
        {
            return Task.Run(() => ExecuteApiKeyToolAsync(projectData)).Result;
        }

        public virtual async Task<ExecuteApiKeyToolResult> ExecuteApiKeyToolAsync(ProjectData projectData, UserSignInCredentials userSignInCredentials = null)
        {
            FileInfo apiKeyTool = projectData.GetApiKeyToolFile();
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = apiKeyTool.FullName,
                UseShellExecute = false,
                CreateNoWindow = true,
                ErrorDialog = false,
            };
            ExecuteApiKeyToolResult result = new ExecuteApiKeyToolResult { ApiKeyToolFile = apiKeyTool };
            try
            {
                userSignInCredentials.Save();
                ExecutingApiKeyTool?.Invoke(this, new ApiKeyToolEventArgs { ProjectData = projectData, UserSignInCredentials = userSignInCredentials, ApiKeyToolFile = apiKeyTool });

                Process apiKeyToolProcess = Process.Start(processStartInfo); // this file takes 2 arguments: input and output.  Input is the Usercredentials file and output is the apitoken file.  If not specified it will use ~/.okta/vs_wizard/usic.ejson as input and ~/.okta/vs_wizard/oat.ejson as output.  Contents of these files is encrypted.
                apiKeyToolProcess.WaitForExit();

                result.Success = apiKeyToolProcess.ExitCode == 0;
                result.OktaApiToken = TryLoadOktaApiToken();
                if (!result.Success)
                {
                    result.ApiKeyErrorResponse = ApiKeyErrorResponse.Load();
                }

                ExecutedApiKeyTool?.Invoke(this, new ApiKeyToolEventArgs { ProjectData = projectData, UserSignInCredentials = userSignInCredentials, ApiKeyToolFile = apiKeyTool });
            }
            catch (Exception ex)
            {
                ExecuteApiKeyToolException?.Invoke(this, new ApiKeyToolEventArgs { ProjectData = projectData, UserSignInCredentials = userSignInCredentials, ApiKeyToolFile = apiKeyTool, Exception = ex });
            }

            return result;
        }

        /// <summary>
        /// Sets the finalizer for the speicfied Okta application type.
        /// </summary>
        /// <typeparam name="T">The finalizer type.</typeparam>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        public void SetFinalizer<T>(OktaApplicationType oktaApplicationType)
            where T : IWizardRunFinisher
        {
            wizardRunFinisherResolver.SetWizardRunFinisher<T>(oktaApplicationType);
        }

        /// <summary>
        /// Sets the finalizer for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <param name="instance">The finalizer instance.</param>
        public void SetFinalizer(OktaApplicationType oktaApplicationType, IWizardRunFinisher instance)
        {
            wizardRunFinisherResolver.SetWizardRunFinisher(oktaApplicationType, instance);
        }

        /// <summary>
        /// Runs the finalizer for the specified Okta wizard result.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        /// <returns>Task{WizardRunFinishedResult}</returns>
        public async Task<WizardRunFinishedResult> RunFinished(OktaWizardResult oktaWizardResult)
        {
            return await Task.Run(async () =>
            {
                IWizardRunFinisher wizardRunFinisher = wizardRunFinisherResolver.GetWizardRunFinisher(oktaWizardResult);
                return await wizardRunFinisher.RunFinishedAsync(oktaWizardResult);
            });
        }

        /// <summary>
        /// Gets an Okta application settings observable.
        /// </summary>
        /// <returns>OktaApplicationSettingsObservable</returns>
        public OktaApplicationSettingsObservable GetOktaApplicationSettingsObservable()
        {
            return new OktaApplicationSettingsObservable
            {
                ApiToken = WizardConfig.OktaApiToken,
            };
        }

        /// <summary>
        /// Returns true if the specified api credentials work to retrieve Okta applications.
        /// </summary>
        /// <param name="apiCredentials">The API credentials.</param>
        /// <returns>bool</returns>
        public virtual bool ApiCredentialsWorks(ApiCredentials apiCredentials)
        {
            ApiCredentials = apiCredentials;
            OktaDomain = apiCredentials.Domain;
            if (!ApiCredentials.IsValid)
            {
                return false;
            }

            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage requestMessage = GetHttpRequestMessage(apiCredentials.Token, HttpMethod.Get, GetPath(new Uri(apiCredentials.Domain)));
                HttpResponseMessage responseMessage = client.SendAsync(requestMessage).Result;
                responseMessage.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                ApiCredentialsPromptException = ex;
                return false;
            }
        }

        protected override Uri GetDomainUri()
        {
            return new Uri(OktaDomain);
        }

        /// <inheritdoc/>
        protected override string GetPath(string queryString = null)
        {
            return GetPath(null, queryString);
        }

        protected string GetPath(Uri uri, string queryString = null)
        {
            Uri domain = uri ?? GetDomainUri();

            string path = Path.Combine(domain.ToString(), "api", "v1", "apps");
            if (!string.IsNullOrEmpty(queryString))
            {
                path += $"?{queryString}";
            }

            return path;
        }

        /// <summary>
        /// Prompts for Okta application settings.
        /// </summary>
        /// <param name="oktaApplicationSettingsObservable">The observable to databind.</param>
        /// <returns>OktaApplicationSettings</returns>
        public OktaApplicationSettings PromptForOktaApplicationSettings(OktaApplicationSettingsObservable oktaApplicationSettingsObservable)
        {
            return PromptForOktaApplicationSettingsAsync(oktaApplicationSettingsObservable).Result;
        }

        /// <summary>
        /// Prompts for the Okta application settings asynchronously.
        /// </summary>
        /// <param name="oktaApplicationSettingsObservable">The observable to databind.</param>
        /// <returns>Task{OktaApplicationSettings}</returns>
        public virtual async Task<OktaApplicationSettings> PromptForOktaApplicationSettingsAsync(OktaApplicationSettingsObservable oktaApplicationSettingsObservable)
        {
            try
            {
                OktaApplicationSettingsPrompting?.Invoke(this, new OktaApplicationSettingsEventArgs { OktaWizard = this, OktaApplicationSettings = oktaApplicationSettingsObservable.ToOktaApplicationSettings() });
                OktaApplicationSettings result = await oktaApplicationSettingsPromptProvider.PromptAsync(oktaApplicationSettingsObservable);

                OktaApplicationSettingsPrompted?.Invoke(this, new OktaApplicationSettingsEventArgs { OktaWizard = this, OktaApplicationSettings = oktaApplicationSettingsObservable.ToOktaApplicationSettings() });

                oktaApplicationSettingsObservable.ReadInTargetValues();
                WizardConfig.OktaApiToken = oktaApplicationSettingsObservable.ApiToken;
                SaveWizardConfig();
                return result;
            }
            catch (Exception ex)
            {
                OktaApplicationSettingsPromptException = ex;
                OktaApplicationSettingsPromptExceptionOccurred?.Invoke(this, new OktaApplicationSettingsEventArgs { OktaWizard = this, Exception = ex });
                return new OktaApplicationSettings();
            }
        }

        private void SaveWizardConfig()
        {
            WizardConfig.Save();
            OktaWizardConfigSaved?.Invoke(this, new OktaWizardConfigEventArgs { OktaWizard = this, OktaWizardConfig = WizardConfig });
        }

        /// <summary>
        /// Promps for applicaiton credentials.
        /// </summary>
        /// <param name="autoRegisterApplicationFormObservable">The observable to databind.</param>
        /// <returns>Task{ApplicationCredentials}</returns>
        public virtual async Task<ApplicationCredentials> PromptForApplicationCredentialsAsync(AutoRegisterApplicationFormObservable autoRegisterApplicationFormObservable)
        {
            try
            {
                ApplicationCredentialsPrompting?.Invoke(this, new ApplicationCredentialsEventArgs { OktaWizard = this, ApplicationCredentials = autoRegisterApplicationFormObservable.ToApplicationCredentials() });
                ApplicationCredentials result = await applicationCredentialsPromptProvider.PromptAsync(autoRegisterApplicationFormObservable);
                result.ApplicationExists = autoRegisterApplicationFormObservable.ApplicationExists;
                ApplicationCredentialsPrompted?.Invoke(this, new ApplicationCredentialsEventArgs { OktaWizard = this, ApplicationCredentials = result });
                return result;
            }
            catch (Exception ex)
            {
                ApplicationCredentialsPromptException = ex;
                ApplicationCredentialsPromptExceptionOccurred?.Invoke(this, new ApplicationCredentialsEventArgs { OktaWizard = this, Exception = ex });
                return new ApplicationCredentials();
            }
        }

        /// <summary>
        /// Try to load the wizard config.
        /// </summary>
        /// <param name="filePath">The path to the configuration file.</param>
        /// <returns>bool indicating if the configuration was loaded without exceptions.</returns>
        public bool TryLoadWizardConfig(string filePath = null)
        {
            return TryLoadWizardConfig(out _, filePath);
        }

        /// <summary>
        /// Try to load the wizard config.
        /// </summary>
        /// <param name="wizardConfig">The loaded wizard configuration.</param>
        /// <param name="filePath">The path to the configuration file.</param>
        /// <returns>bool indicating if the configuration was loaded without exceptions.</returns>
        public bool TryLoadWizardConfig(out OktaWizardConfig wizardConfig, string filePath = null)
        {
            try
            {
                wizardConfig = LoadWizardConfig(filePath);
                return true;
            }
            catch (Exception ex)
            {
                OktaWizardConfigLoadException = ex;
                OktaWizardConfigLoadExceptionOccurred?.Invoke(this, new OktaWizardConfigEventArgs { OktaWizard = this, Exception = ex });
                wizardConfig = OktaWizardConfig.Default;
                return false;
            }
        }

        /// <summary>
        /// Loads the OktaWizardConfig from the specified file and sets it as the WizardConfig property.
        /// </summary>
        /// <param name="filePath">The path to the OktaWizardConfig yaml file; not to be confused with OktaConfig.</param>
        /// <returns>OktaWizardConfig</returns>
        public OktaWizardConfig LoadWizardConfig(string filePath = null)
        {
            OktaWizardConfigLoading?.Invoke(this, new OktaWizardConfigEventArgs { OktaWizard = this });
            WizardConfig = OktaWizardConfig.Load(filePath);
            OktaWizardConfigLoaded?.Invoke(this, new OktaWizardConfigEventArgs { OktaWizard = this, OktaWizardConfig = WizardConfig });
            return WizardConfig;
        }

        /// <summary>
        /// Asynchronously determine if the specified application exists.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Task{bool}</returns>
        public async Task<bool> ApplicationExistsAsync(string applicationName)
        {
            if (await ExistsLocallyAsync(applicationName))
            {
                return true;
            }

            ApplicationFindResponse[] response = await ApplicationRegistrationManager.FindApplicationsAsync(applicationName, 20);
            return response.Any(ca => ca.ClientName.Equals(applicationName));
        }

        /// <summary>
        /// Asynchronously gets the spcified client application.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Task{ClientApplication}</returns>
        public async Task<ClientApplication> GetClientApplicationAsync(string applicationName)
        {
            return new ClientApplication(await ApplicationRegistrationManager.RetrieveApplicationAsync(applicationName));
        }

        private List<ClientApplication> clientApplications;

        /// <summary>
        /// Asynchronously gets client applications.
        /// </summary>
        /// <param name="reload">If true, retrieve the applications again even if they have previously been retrieved.</param>
        /// <returns>Task{List{ClientApplication}}</returns>
        public async Task<List<ClientApplication>> GetClientApplicationsAsync(bool reload = false)
        {
            if (clientApplications == null || clientApplications.Count == 0 || reload)
            {
                clientApplications = await RetrieveClientApplicationsAsync();
            }

            return clientApplications;
        }

        /// <summary>
        /// Sets the OktaWizard property on the credential prompter components.
        /// </summary>
        protected void SetOktaWizardOnPrompters()
        {
            oktaApplicationSettingsPromptProvider.OktaWizard = this;
            applicationCredentialsPromptProvider.OktaWizard = this;
        }

        /// <summary>
        /// Asynchronously determine if the specified application name exists.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Task{bool}</returns>
        protected async Task<bool> ExistsLocallyAsync(string applicationName)
        {
            return (await GetClientApplicationsAsync()).Any(ca => !string.IsNullOrEmpty(ca.Name) && ca.Name.Equals(applicationName));
        }

        /// <summary>
        /// Asynchronously retrieve client applications.
        /// </summary>
        /// <returns>Task{List{ClientApplication}}</returns>
        protected async Task<List<ClientApplication>> RetrieveClientApplicationsAsync()
        {
            return (await ApplicationRegistrationManager.ListApplicationsAsync())
                .Select(calr => new ClientApplication(calr)).ToList();
        }

        private string GetHomePath()
        {
            string homePath = Environment.GetEnvironmentVariable("USERPROFILE") ??
                    Path.Combine(Environment.GetEnvironmentVariable("HOMEDRIVE"), Environment.GetEnvironmentVariable("HOMEPATH"));
            return homePath ?? ".";
        }

        /// <summary>
        /// Gets the telemetry session id.
        /// </summary>
        /// <returns>string</returns>
        public string GetTelemetrySessionId()
        {
            return TelemetrySessionId;
        }

        private static readonly object LogLock = new object();

        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message)
        {
            lock (LogLock)
            {
                string logFilePath = Path.Combine(OktaWizardConfig.OktaWizardHome, "log.txt");
                FileInfo logFile = new FileInfo(logFilePath);
                if (!logFile.Directory.Exists)
                {
                    logFile.Directory.Create();
                }

                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
