using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    public class ApplicationDefinitionArguments
    {
        public const int SSL_PORT = 44300;
        public const int PORT = 8080;

        public ApplicationDefinitionArguments(SdkConfig sdkConfig, ProjectArguments projectArguments)
        {
            this.SdkConfig = sdkConfig;
            this.ProjectArguments = projectArguments;
        }
        
        protected SdkConfig SdkConfig { get; set; }

        public ProjectArguments ProjectArguments { get; set; }

        public string OktaDomain
        {
            get
            {
                return SdkConfig?.Okta?.Client?.OktaDomain;
            }
        }

        public Uri LocalHostUri
        {
            get
            {
                return new Uri($"http://localhost:{PORT}");
            }
        }

        public Uri SslLocalHostUri
        {
            get
            {
                return new Uri($"https://localhost:{SSL_PORT}");
            }
        }

        public string GetApplicationJsonFilePath()
        {
            return new FileInfo(Path.Combine(ProjectArguments.GetOktaWizardDirectoryPath(), "application.json")).FullName;
        }
    }
}
