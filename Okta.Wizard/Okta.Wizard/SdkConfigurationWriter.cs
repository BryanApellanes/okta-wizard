using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Okta.Sdk.Api;
using Okta.Sdk.Client;
using Okta.Wizard.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    public class SdkConfigurationWriter : ISdkConfigurationWriter
    {
        public SdkConfigurationWriter(IOktaWizardSettings settings, IOrganizationCreator organizationCreator, ILogger logger = null) 
        {
            this.Status = SdkConfigurationWriterStatus.Idle;
            this.Settings = settings;
            this.OrganizationCreator = organizationCreator;
            this.Logger = logger ?? FileLogger.Create();
        }
        public SdkConfigurationWriterStatus Status { get; private set; }
        protected IOktaWizardSettings Settings { get; set; }
        protected IOrganizationCreator OrganizationCreator { get; set; }
        protected ILogger Logger { get; set; }

        public event EventHandler<SdkConfigurationWriterEventArgs> StatusChanged;

        public async Task<ApplicationApi> GetApplicationApiClientAsync(SdkConfig config)
        {
            return new ApplicationApi(new  Configuration { OktaDomain = config.Okta.Client.OktaDomain, Token = config.Okta.Client.Token });
        }

        public async Task<SdkConfig> GetSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null)
        {
            SdkConfig configuration = await LoadSdkConfigurationAsync(configPath);
            if (configuration == null)
            {
                configuration = await CreateSdkConfigurationAsync(organizationRequest, configPath);
            }
            return configuration;
        }

        public async Task<bool> SdkConfigurationExistsAsync(string configPath = null)
        {
            return SdkConfig.Exists(configPath);
        }

        public virtual async Task<SdkConfig> LoadSdkConfigurationAsync(string configPath = null)
        {
            return SdkConfig.Load(configPath);
        }

        public async Task<SdkConfig> CreateSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null)
        {

            if (!PendingRegistration.Exists(out PendingRegistration pendingRegistration))
            {
                pendingRegistration = new PendingRegistration()
                {
                    Request = organizationRequest,
                    Response = await CreateNewOrganizationAsync(organizationRequest)
                };
                pendingRegistration.Save();
            }
            else // pending registration is found on disk
            {
                if (!pendingRegistration.Request.Equals(organizationRequest)) // if the requests arent the same create a new org
                {
                    pendingRegistration.Response = await CreateNewOrganizationAsync(organizationRequest);
                }
            }
            OrganizationResponse activationResponse = pendingRegistration.Response;
            if (pendingRegistration.Response != null && !string.IsNullOrWhiteSpace(pendingRegistration.Response.Identifier))
            {
                OnStatusChanged(SdkConfigurationWriterStatus.NewOrgVerificationPending);
                activationResponse = await VerifyNewOrganizationAsync(pendingRegistration.Response.Identifier);
                OnStatusChanged(SdkConfigurationWriterStatus.NewOrgVerificationComplete);
            }

            if (pendingRegistration.Response is CreateNewOrganizationFailedOrganizationResponse errorResponse)
            {
                throw errorResponse.Exception;
            }

            SdkConfig configuration = new SdkConfig(activationResponse.OrgUrl, activationResponse.ApiToken);

            if (!configuration.TrySave(out Exception saveException))
            {
                Warn($"Failed to save wizard configuration: ({saveException?.Message})");
            }
            return configuration;
        }

        public async Task<OrganizationResponse> CreateNewOrganizationAsync(OrganizationRequest organizationRequest)
        {
            OnStatusChanged(SdkConfigurationWriterStatus.CreatingNewOrgStarted);
            Info($"Creating new organization: {organizationRequest.ToJson(Formatting.Indented)}");
            OrganizationResponse response = null;
            try
            {
                response = await this.OrganizationCreator.CreateNewOrganizationAsync(organizationRequest);
                if (response == null)
                {
                    string message = "No response received creating new organization";
                    Warn(message);
                    OnStatusChanged(SdkConfigurationWriterStatus.CreatingNewOrgException);
                    throw new InvalidOperationException(message);
                }
                if (response is OrganizationErrorResponse errorResponse)
                {
                    OnStatusChanged(SdkConfigurationWriterStatus.CreatingNewOrgException);
                    throw new ApiException(errorResponse);
                }
                OnStatusChanged(SdkConfigurationWriterStatus.CreatingNewOrgComplete);
                response.OperationSucceeded = true;
                return response;
            }
            catch (Exception ex)
            {
                Error("Exception occurred creating new organization", ex);
                return new CreateNewOrganizationFailedOrganizationResponse(response, ex);
            }
        }

        protected void OnStatusChanged(SdkConfigurationWriterStatus status, Exception ex = null)
        {
            this.Status = status;
            this.StatusChanged?.Invoke(this, new SdkConfigurationWriterEventArgs(this, status, ex));
        }

        protected async Task<OrganizationResponse> VerifyNewOrganizationAsync(string identifier)
        {
            Info($"Verifying new organization: {identifier}");
            OrganizationResponse response = null;
            while (response == null || !response.IsActive)
            {
                try
                {
                    Thread.Sleep(Settings.PollingIntervalSeconds);
                    response = await OrganizationCreator.VerifyNewOrganizationAsync(identifier);
                    string message = "Waiting for new organization verification";
                    if (response.IsActive)
                    {
                        OnStatusChanged(SdkConfigurationWriterStatus.NewOrgVerificationComplete);
                        message = "Verified new organization";
                    }

                    Info(message);
                }
                catch (Exception ex)
                {
                    Exception toThrow = new VerifyNewOrganizationFailedException(ex);
                    Warn(toThrow.Message);
                    OnStatusChanged(SdkConfigurationWriterStatus.NewOrgVerificationException, toThrow);
                    throw toThrow;
                }
            }
            return response;
        }

        private void Info(string message)
        {
            Task.Run(() => Logger.Info(message));
        }

        private void Warn(string message)
        {
            Task.Run(() => Logger.Warn(message));
        }

        private void Error(string message, Exception ex)
        {
            Task.Run(() => Logger.Error(message, ex));
        }
    }
}
