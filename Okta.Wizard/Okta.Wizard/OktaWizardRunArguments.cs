using Newtonsoft.Json;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaWizardRunArguments
    {
        public OktaWizardRunArguments()
        {
            if (SdkConfig.Exists())
            {
                SdkConfig = SdkConfig.Load();
            }
        }

        public OktaWizardRunArguments(ProjectArguments projectArguments) : this()
        {
            this.ProjectArguments = projectArguments;
        }

        [JsonIgnore]
        public Func<OrganizationRequest> GetOrganizationRequest { get; set; }

        public ProjectArguments ProjectArguments { get; set; }

        public SdkConfig SdkConfig { get; set; }

        [JsonIgnore]
        public bool ShouldCreateSdkConfig
        {
            get
            {
                return SdkConfig == null;
            }
        }
    }
}
