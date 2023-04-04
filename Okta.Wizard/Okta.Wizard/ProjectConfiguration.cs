using Newtonsoft.Json;
using Okta.Sdk.Client;
using Okta.Sdk.Model;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class ProjectConfiguration
    {
        public ProjectConfiguration(SdkConfig sdkConfig, OpenIdConnectApplication application, ApplicationDefinitionArguments applicationDefinitionArguments)
        {
            this.SdkConfig = sdkConfig;
            this.Application = application;
            this.ApplicationDefinitionArguments = applicationDefinitionArguments;
        }

        public SdkConfig SdkConfig 
        {
            get;
            set;
        }

        public ProjectArguments ProjectArguments
        {
            get
            {
                return ApplicationDefinitionArguments?.ProjectArguments;
            }
        }

        [JsonIgnore]
        public OpenIdConnectApplication Application { get; set; }


        public ApplicationDefinitionArguments ApplicationDefinitionArguments
        {
            get;
            set;
        }

        public string SolutionDirectory
        {
            get
            {
                return ApplicationDefinitionArguments?.ProjectArguments?.SolutionDirectory ?? string.Empty;
            }
        }

        public string ProjectDirectory
        {
            get
            {
                return ApplicationDefinitionArguments?.ProjectArguments?.DestinationDirectory ?? string.Empty;
            }
        }

        public string OktaDomain
        {
            get
            {
                return ApplicationDefinitionArguments?.OktaDomain;
            }
        }

        public string ClientId
        {
            get
            {
                return Application?.Credentials?.OauthClient?.ClientId;
            }
        }

        public string ClientSecret
        {
            get
            {
                return Application?.Credentials?.OauthClient?.ClientSecret;
            }
        }

        public string ApplicationUrl
        {
            get
            {
                return ApplicationDefinitionArguments.LocalHostUri.ToString();
            }
        }

        public string SslApplicationUrl
        {
            get
            {
                return ApplicationDefinitionArguments.SslLocalHostUri.ToString();
            }
        }

        public string Port
        {
            get
            {
                return ApplicationDefinitionArguments.PORT.ToString();
            }
        }

        public string SslPort
        {
            get
            {
                return ApplicationDefinitionArguments.SSL_PORT.ToString();
            }
        }

        public virtual string DoStringReplacements(string input)
        {
            Dictionary<string, string> replacements = ProjectArguments.ToDictionary();
            PropertyInfo[] properties = typeof(ProjectConfiguration).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string) && !replacements.ContainsKey($"${property.Name}$"))
                {
                    replacements.Add($"${property.Name}$", property.GetValue(this) as string);
                }
            }
            foreach (string key in replacements.Keys)
            {
                input = input.Replace(key, replacements[key]);
            }
            return input;
        }
    }
}
