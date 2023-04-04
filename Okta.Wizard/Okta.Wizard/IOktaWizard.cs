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
    public interface IOktaWizard
    {
        event EventHandler<OktaWizardStatusChangedEventArgs> OnStatusChanged;

        event EventHandler<OktaWizardStatusChangedEventArgs> OnIdle;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnRunStarted;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnRunComplete;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgRequired;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgStarted;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgComplete;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateNewOrgException;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationPending;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationComplete;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnNewOrgVerificationException;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationStarted;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationComplete;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnCreateApplicationException;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectStarted;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectComplete;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnConfigureProjectException;
        event EventHandler<OktaWizardStatusChangedEventArgs> OnError;

        IOktaWizardSettings Settings { get; }
        OktaWizardStatus Status { get; }
        ProjectArguments ProjectArguments { get; set; }
        SdkConfig SdkConfig { get; set; }
        Task<OktaWizardRunResult> RunAsync(OktaWizardRunArguments arguments);
        Task<bool> SdkConfigurationExistsAsync(string configPath = null);
        Task<SdkConfig> LoadSdkConfigAsync(string configPath = null);
        Task DeleteConfigAsync(string configPath = null);
        Task<SdkConfig> CreateSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null);

        Task<ApplicationApi> GetApplicationApiAsync(SdkConfig configuration);
        Task<CreateApplicationResult> CreateApplicationAsync();
        Task<CreateApplicationResult> CreateApplicationAsync(ApplicationApi api, ApplicationDefinitionArguments parameters);

        Task<ProjectConfigurationResult> ConfigureProjectAsync(ProjectConfiguration projectConfiguration);
    }
}
