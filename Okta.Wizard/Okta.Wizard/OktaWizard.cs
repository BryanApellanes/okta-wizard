using Newtonsoft.Json;
using Okta.Sdk.Api;
using Okta.Sdk.Client;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class OktaWizard : LogEventWriter, IOktaWizard
    {
        public OktaWizard(IOktaWizardSettings settings, ISdkConfigurationWriter sdkConfigurer, IApplicationRequestCreator applicationRequestCreator, IProjectConfigurationWriter projectConfigurationWriter, ILogger logger = null)
        {
            this.Settings = settings;
            this.SdkConfigurer = sdkConfigurer;
            this.ApplicationRequestCreator = applicationRequestCreator;
            this.ProjectConfigurationWriter = projectConfigurationWriter;
            this.Logger = logger ?? FileLogger.Create();
            this.Status = OktaWizardStatus.Idle;
            this.SdkConfigurer.StatusChanged += SdkConfgurerStatusChanged;
        }

        public event EventHandler<OktaWizardStatusChangedEventArgs> OnStatusChanged;

        public event EventHandler<OktaWizardStatusChangedEventArgs> OnIdle;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnRunStarted;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnRunComplete;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgRequired;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgStarted;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgComplete;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgException;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationPending;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationComplete;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationException;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationStarted;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationComplete;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationException;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectStarted;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectComplete;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectException;
        public event EventHandler<OktaWizardStatusChangedEventArgs> OnError;

        public IOktaWizardSettings Settings
        {
            get;
            private set;
        }

        public Exception Exception { get; set; }
        public ProjectArguments ProjectArguments { get; set; }
        public SdkConfig SdkConfig { get; set; }

        public OktaWizardStatus Status { get; private set; }

        public ISdkConfigurationWriter SdkConfigurer { get; private set; }

        public IApplicationRequestCreator ApplicationRequestCreator { get; private set; }

        protected IProjectConfigurationWriter ProjectConfigurationWriter { get; private set; }

        public async Task<OktaWizardRunResult> RunAsync(OktaWizardRunArguments arguments)
        {
            OktaWizardRunResult result = new OktaWizardRunResult(arguments);
            try
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.RunStarted, result);
                if (arguments.ShouldCreateSdkConfig)
                {
                    if (arguments.GetOrganizationRequest != null)
                    {
                        OrganizationRequest organizationRequest = arguments.GetOrganizationRequest();
                        if (organizationRequest == null)
                        {
                            throw new ArgumentNullException("organizationRequest");
                        }
                        arguments.SdkConfig = await CreateSdkConfigurationAsync(organizationRequest);
                        arguments.SdkConfig.Save();
                    }
                    else
                    {
                        return await SetCreateNewOrgRequiredStatusAsync(result);
                    }
                }
                ApplicationApi api = await GetApplicationApiAsync(arguments.SdkConfig);
                if (api == null)
                {
                    throw new ArgumentNullException("Failed to get ApplicationApi");
                }

                CreateApplicationResult appResult = await CreateApplicationAsync(api, new ApplicationDefinitionArguments(arguments.SdkConfig, arguments.ProjectArguments));
                if (!appResult.OperationSucceeded)
                {
                    throw appResult.Exception;
                }
                result.CreateApplicationResult = appResult;

                ProjectConfigurationResult projectConfigurationResult = await ConfigureProjectAsync(new ProjectConfiguration(arguments.SdkConfig, appResult.CreatedApplication, appResult.ApplicationDefinitionArguments));
                if (!projectConfigurationResult.OperationSucceeded)
                {
                    throw projectConfigurationResult.Exception;
                }

                result.ProjectConfigurationResult = projectConfigurationResult;
                result.Success = true;
                result.OktaWizardStatus = OktaWizardStatus.RunComplete;
                await this.InfoAsync($"Okta Wizard Run Complete: \r\n{result.ToJson()}");
                await this.TriggerStatusChangedAsync(OktaWizardStatus.RunComplete, result);                
            }
            catch (Exception ex)
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.Error, result, ex);
            }
            return result;
        }

        public Task<bool> SdkConfigurationExistsAsync(string configPath = null)
        {
            return Task.FromResult(SdkConfig.Exists(configPath));
        }

        public Task<SdkConfig> LoadSdkConfigAsync(string configPath = null)
        {
            return Task.FromResult(SdkConfig.Load(configPath));
        }

        public Task DeleteConfigAsync(string configPath = null)
        {
            return Task.Run(() => SdkConfig.Delete(configPath));
        }

        public async Task<SdkConfig> CreateSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null)
        {
            try
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateNewOrgStarted);

                SdkConfig = await SdkConfigurer.CreateSdkConfigurationAsync(organizationRequest, configPath);

                await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateNewOrgComplete);
                return SdkConfig;
            }
            catch (Exception ex)
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.Error, ex);
                Fatal("Failed to create configuration", ex);
                throw;
            }
        }

        public async Task<ApplicationApi> GetApplicationApiAsync(SdkConfig sdkConfig)
        {
            try
            {
                return await SdkConfigurer.GetApplicationApiClientAsync(sdkConfig);
            }
            catch (Exception ex)
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.Error, ex);
                Fatal("Failed to get ApplicationApi client.", ex);
                return null;
            }
        }

        public async Task<CreateApplicationResult> CreateApplicationAsync()
        {
            ApplicationApi api = await GetApplicationApiAsync(await LoadSdkConfigAsync());
            return await CreateApplicationAsync(api, new ApplicationDefinitionArguments(SdkConfig, ProjectArguments));
        }

        public async Task<CreateApplicationResult> CreateApplicationAsync(ApplicationApi api, ApplicationDefinitionArguments arguments)
        {
            try
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateApplicationStarted);

                ApplicationCreator applicationCreator = new ApplicationCreator(ApplicationRequestCreator, api);
                CreateApplicationResult response = await applicationCreator.CreateApplicationAsync(arguments);

                await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateApplicationComplete);

                return response;
            }
            catch (Exception ex)
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateApplicationException, ex);
                Error($"Exception creating application: {ex.Message}", ex);
                return new CreateApplicationResult(arguments, ex);
            }
        }

        public async Task<ProjectConfigurationResult> ConfigureProjectAsync(ProjectConfiguration projectConfiguration)
        {
            try
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.ConfigureProjectStarted);

                ProjectConfigurationResult result = await ProjectConfigurationWriter.ConfigureProjectAsync(projectConfiguration);

                await this.TriggerStatusChangedAsync(OktaWizardStatus.ConfigureProjectComplete);
                return result;
            }
            catch (Exception ex)
            {
                await this.TriggerStatusChangedAsync(OktaWizardStatus.ConfigureProjectException, ex);
                Error($"Exception configuring project: {ex.Message}", ex);
                return new ProjectConfigurationResult(ex);
            }
        }

        protected async Task TriggerStatusChangedAsync(OktaWizardStatus status, Exception ex = null)
        {
            await TriggerStatusChangedAsync(status, null, null);
        }

        protected async Task TriggerStatusChangedAsync(OktaWizardStatus status, OktaWizardRunResult runResult, Exception ex = null)
        {
            if (this.Status != status)
            {
                if (runResult != null)
                {
                    runResult.OktaWizardStatus = status;
                    if (ex != null)
                    {
                        runResult.Exception = ex;
                    }
                }
                OktaWizardStatusChangedEventArgs args = new OktaWizardStatusChangedEventArgs
                {
                    Transition = new OktaWizardStatusTransition
                    {
                        PreviousStatus = this.Status,
                        CurrentStatus = status,
                    },
                    RunResult = runResult,
                    Status = status,
                    Exception = ex
                };
                if (ex != null)
                {
                    this.Exception = ex;
                }
                this.Status = status;
                this.OnStatusChanged?.Invoke(this, args);
                await this.TriggerStatusEventAsync(args);
            }
        }

        protected async Task TriggerStatusEventAsync(OktaWizardStatusChangedEventArgs args)
        {
            switch(args.Status)
            {
                case OktaWizardStatus.Idle:
                    this.OnIdle?.Invoke(this, args);
                    break;
                case OktaWizardStatus.RunStarted:
                    this.OnRunStarted?.Invoke(this, args);
                    break;
                case OktaWizardStatus.RunComplete:
                    this.OnRunComplete?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateNewOrgRequired:
                    this.OnCreateNewOrgRequired?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateNewOrgStarted:
                    this.OnCreateNewOrgStarted?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateNewOrgComplete:
                    this.OnCreateNewOrgComplete?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateNewOrgException:
                    this.OnCreateNewOrgException?.Invoke(this, args);
                    break;
                case OktaWizardStatus.NewOrgVerificationPending:
                    this.OnNewOrgVerificationPending?.Invoke(this, args);
                    break;
                case OktaWizardStatus.NewOrgVerificationComplete:
                    this.OnNewOrgVerificationComplete?.Invoke(this, args);
                    break;
                case OktaWizardStatus.NewOrgVerificationException:
                    this.OnNewOrgVerificationException?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateApplicationStarted:
                    this.OnCreateApplicationStarted?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateApplicationComplete:
                    this.OnCreateApplicationComplete?.Invoke(this, args);
                    break;
                case OktaWizardStatus.CreateApplicationException:
                    this.OnCreateApplicationException?.Invoke(this, args);
                    break;
                case OktaWizardStatus.ConfigureProjectStarted:
                    this.OnConfigureProjectStarted?.Invoke(this, args);
                    break;
                case OktaWizardStatus.ConfigureProjectComplete:
                    this.OnConfigureProjectComplete?.Invoke(this, args);
                    break;
                case OktaWizardStatus.ConfigureProjectException:
                    this.OnConfigureProjectException?.Invoke(this, args);
                    break;
                case OktaWizardStatus.Error:
                    this.OnError?.Invoke(this, args);
                    break;
            }
        }

        protected void SdkConfgurerStatusChanged(object sender, SdkConfigurationWriterEventArgs eventArgs)
        {

            OktaWizardStatus status = this.Status;
            switch (eventArgs.Status)
            {
                case SdkConfigurationWriterStatus.Idle:
                    status = OktaWizardStatus.Idle;
                    break;
                case SdkConfigurationWriterStatus.CreatingNewOrgStarted:
                    status = OktaWizardStatus.CreateNewOrgStarted;
                    break;
                case SdkConfigurationWriterStatus.CreatingNewOrgComplete:
                    status = OktaWizardStatus.CreateNewOrgComplete;
                    break;
                case SdkConfigurationWriterStatus.CreatingNewOrgException:
                    status = OktaWizardStatus.CreateNewOrgException;
                    this.Exception = eventArgs.Exception;
                    break;
                case SdkConfigurationWriterStatus.NewOrgVerificationPending:
                    status = OktaWizardStatus.NewOrgVerificationPending;
                    break;
                case SdkConfigurationWriterStatus.NewOrgVerificationComplete:
                    status = OktaWizardStatus.NewOrgVerificationComplete;
                    break;
                case SdkConfigurationWriterStatus.NewOrgVerificationException:
                    status = OktaWizardStatus.NewOrgVerificationException;
                    this.Exception = eventArgs.Exception;
                    break;
            }
            this.TriggerStatusChangedAsync(status).Wait();
        }

        private async Task<OktaWizardRunResult> SetCreateNewOrgRequiredStatusAsync(OktaWizardRunResult result)
        {
            result.Success = false;
            result.OktaWizardStatus = OktaWizardStatus.CreateNewOrgRequired;
            await this.TriggerStatusChangedAsync(OktaWizardStatus.CreateNewOrgRequired, result);
            
            return result;
        }
    }
}
