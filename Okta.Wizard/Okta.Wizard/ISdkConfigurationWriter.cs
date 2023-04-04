using Okta.Sdk.Api;
using Okta.Sdk.Client;
using Okta.Wizard.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface ISdkConfigurationWriter
    {
        event EventHandler<SdkConfigurationWriterEventArgs> StatusChanged;
        SdkConfigurationWriterStatus Status { get; }
        Task<ApplicationApi> GetApplicationApiClientAsync(SdkConfig configuration);
        Task<bool> SdkConfigurationExistsAsync(string configPath = null);
        Task<SdkConfig> GetSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null);
        Task<SdkConfig> LoadSdkConfigurationAsync(string configPath = null);
        Task<SdkConfig> CreateSdkConfigurationAsync(OrganizationRequest organizationRequest, string configPath = null);
        Task<OrganizationResponse> CreateNewOrganizationAsync(OrganizationRequest registrationRequest);
    }
}
