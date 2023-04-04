// <copyright file="OktaWizardConfig.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using DevEx.Internal;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Okta.Wizard.Internal;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Configuration for the Okta Wizard.
    /// </summary>
    public class OktaWizardConfig : Serializable
    {
        /// <summary>
        /// Gets the path to the default okta wizard configuration file.
        /// </summary>
        /// <value>
        /// The path to the default okta wizard configuration file.
        /// </value>
        public static string DefaultOktaWizardConfigPath => HomePath.Expand("~\\.okta\\vs_wizard\\vs_wizard.yaml");

        /// <summary>
        /// Gets the path to the Okta wizard home.
        /// </summary>
        /// <value>
        /// The path to the Okta wizard home.
        /// </value>
        public static string OktaWizardHome => HomePath.Expand("~\\.okta\\vs_wizard");

        public static string ScreenShotsDirectory => Path.Combine(OktaWizardConfig.OktaWizardHome, "screenshots");

        /// <summary>
        /// Gets the default telemetry service root.
        /// </summary>
        public const string DefaultTelemetryServiceRoot = "https://devextelemetryappservice.azurewebsites.net";

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaWizardConfig"/> class.
        /// </summary>
        public OktaWizardConfig()
        {
            ClientApplications = new List<ClientApplication>();
            Debug = false;
            TelemetryServiceRoot = DefaultTelemetryServiceRoot;
            OktaReplacementFiles = new string[]
            {
                "OktaConfig.xml",
                "appsettings.json",
                "OktaConfig.plist",
                "OktaWelcome.md",
            };
        }

        /// <summary>
        /// Gets or sets the root of the telemetry service.
        /// </summary>
        /// <value>
        /// The root of the telemetry service.
        /// </value>
        public string TelemetryServiceRoot { get; set; }

        /// <summary>
        /// Gets or sets the Okta domain.
        /// </summary>
        /// <value>
        /// The Okta domain.
        /// </value>
        public string OktaDomain { get; set; }

        /// <summary>
        /// Gets or sets the Okta API token.
        /// </summary>
        /// <value>
        /// The Okta API token.
        /// </value>
        public string OktaApiToken { get; set; }

        /// <summary>
        /// Gets or sets the Okta replacement files.
        /// </summary>
        /// <value>
        /// The Okta replacement files.
        /// </value>
        public string[] OktaReplacementFiles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to always prompt for API credentials regardless of the value of DontShowThisAgainSetupWindowChecked.
        /// </summary>
        /// <value>
        /// A value indicating whether to always prompt for API credentials regardless of the value of DontShowThisAgainSetupWindowChecked.
        /// </value>
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the client applications.
        /// </summary>
        /// <value>
        /// The client applications.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public List<ClientApplication> ClientApplications { get; set; }

        /// <summary>
        /// Gets a value indicating whether API credentials are set.
        /// </summary>
        /// <value>
        /// A value indicating whether API credentials are set.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public bool HasApiCredentials
        {
            get => GetApiCredentials().IsValid;
        }

        /// <summary>
        /// Gets the domain URI.
        /// </summary>
        /// <returns>URI</returns>
        public Uri GetDomainUri()
        {
            return GetDomainUri(OktaDomain);
        }

        /// <summary>
        /// Gets API credentials.
        /// </summary>
        /// <returns>ApiCredentials</returns>
        public ApiCredentials GetApiCredentials()
        {
            if (!string.IsNullOrEmpty(OktaDomain) && !string.IsNullOrEmpty(OktaApiToken))
            {
                return new ApiCredentials
                {
                    Domain = GetDomainUri(OktaDomain).ToString(),
                    Token = OktaApiToken,
                };
            }

            return new ApiCredentials();
        }

        /// <summary>
        /// Saves the current configuration.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Save(string filePath = null)
        {
            filePath = filePath ?? DefaultOktaWizardConfigPath;
            string yaml = ToYaml();
            string encrypted = GetDefaultDataProtector().Protect(yaml);
            File.WriteAllText(filePath, encrypted);
            FileInfo configFile = new FileInfo(filePath);
            DirectoryInfo configDirectory = configFile.Directory;
            foreach (ClientApplication clientApplication in ClientApplications)
            {
                string clientAppConfigPath = Path.Combine(configDirectory.FullName, $"{nameof(ClientApplication)}s", $"{clientApplication.ProjectName}.yaml");
                clientApplication.ToYamlFile(clientAppConfigPath);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance equals the specified object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is OktaWizardConfig oktaWizardConfig))
            {
                return false;
            }

            return (bool)oktaWizardConfig?.ToJson().Equals(this.ToJson());
        }

        /// <summary>
        /// Gets a hash code for this instance.
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return ToJson().GetHashCode();
        }

        private static readonly Lazy<OktaWizardConfig> _default = new Lazy<OktaWizardConfig>(() => Load(DefaultOktaWizardConfigPath));

        /// <summary>
        /// Gets the default Okta wizard configuration.
        /// </summary>
        /// <value>
        /// The default Okta wizard configuration.
        /// </value>
        public static OktaWizardConfig Default
        {
            get => _default.Value;
        }

        /// <summary>
        /// Gets the default data protection provider.
        /// </summary>
        /// <returns>IDataProtectionProvider</returns>
        public static IDataProtectionProvider GetDefaultDataProtectionProvider()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IDataProtectionProvider dataProtectionProvider = serviceProvider.GetDataProtectionProvider();
            return dataProtectionProvider;
        }

        /// <summary>
        /// Gets the default data protector.
        /// </summary>
        /// <param name="dataProtectionProvider">The provider used to get the protector.</param>
        /// <returns>IDataProtector</returns>
        public static IDataProtector GetDefaultDataProtector(IDataProtectionProvider dataProtectionProvider = null)
        {
            return (dataProtectionProvider ?? GetDefaultDataProtectionProvider()).CreateProtector(OktaWizardUserAgent.Value);
        }

        /// <summary>
        /// Load Okta wizard configuration.
        /// </summary>
        /// <param name="filePath">The path to load from</param>
        /// <returns>OktaWizardConfig</returns>
        public static OktaWizardConfig Load(string filePath = null)
        {
            return Load(GetDefaultDataProtectionProvider(), filePath);
        }

        /// <summary>
        /// Load Okta wizard configuration.
        /// </summary>
        /// <param name="dataProtectionProvider">The data protection provider.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>OktaWizardConfig</returns>
        public static OktaWizardConfig Load(IDataProtectionProvider dataProtectionProvider, string filePath = null)
        {
            OktaWizardConfig result = EnsureDefault(dataProtectionProvider, ref filePath);
            DirectoryInfo clientApplicationConfigDirectory = EnsureClientApplicationsConfigDirectory(filePath);

            FileInfo[] clientAppConfigFiles = clientApplicationConfigDirectory.GetFiles("*.yaml", SearchOption.TopDirectoryOnly);
            foreach (FileInfo clientAppConfig in clientAppConfigFiles)
            {
                result.ClientApplications.Add(Deserialize.FromYamlFile<ClientApplication>(clientAppConfig.FullName));
            }

            return result;
        }

        /// <summary>
        /// Get domain uri.
        /// </summary>
        /// <param name="domain">The domain</param>
        /// <returns>URI</returns>
        public static Uri GetDomainUri(string domain)
        {
            if (string.IsNullOrEmpty(domain))
            {
                throw new ArgumentNullException("domain");
            }

            Uri domainUri;
            if (domain.StartsWith("http"))
            {
                domainUri = new Uri(domain);
            }
            else
            {
                domainUri = new Uri($"https://{domain}");
            }

            if (!domainUri.Scheme.Equals("https"))
            {
                domainUri = new Uri($"https://{domainUri.Authority}");
            }

            return domainUri;
        }

        private static DirectoryInfo EnsureClientApplicationsConfigDirectory(string filePath)
        {
            filePath = filePath ?? DefaultOktaWizardConfigPath;
            DirectoryInfo configDirectory = new FileInfo(filePath).Directory;
            DirectoryInfo clientApplicationConfigDirectory = new DirectoryInfo(Path.Combine(configDirectory.FullName, $"{nameof(ClientApplication)}s"));
            if (!clientApplicationConfigDirectory.Exists)
            {
                clientApplicationConfigDirectory.Create();
            }

            return clientApplicationConfigDirectory;
        }

        private static OktaWizardConfig defaultOktaWizardConfig;
        private static object defaultOktaWizardConfigLock = new object();

        private static OktaWizardConfig EnsureDefault(IDataProtectionProvider dataProtectionProvider, ref string filePath)
        {
            if (defaultOktaWizardConfig == null)
            {
                lock (defaultOktaWizardConfigLock)
                {
                    defaultOktaWizardConfig = ReadOktaWizardConfig(GetDefaultDataProtector(dataProtectionProvider), filePath);
                }
            }

            return defaultOktaWizardConfig;
        }

        private static OktaWizardConfig ReadOktaWizardConfig(IDataProtector dataProtector = null, string filePath = null)
        {
            dataProtector = dataProtector ?? GetDefaultDataProtector();
            filePath = filePath ?? DefaultOktaWizardConfigPath;
            if (!File.Exists(filePath))
            {
                return WriteDefault(dataProtector, filePath);
            }

            string fileContent = File.ReadAllText(filePath);
            try
            {
                string yaml = dataProtector.Unprotect(fileContent);
                return Deserialize.FromYaml<OktaWizardConfig>(yaml);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception reading OktaWizardConfig, resetting to default values: {0}", ex.Message);
                BackUpFile(filePath);
                return WriteDefault(dataProtector, filePath);
            }
        }

        private static OktaWizardConfig WriteDefault(IDataProtector dataProtector, string filePath)
        {
            OktaWizardConfig oktaWizardConfig = new OktaWizardConfig();
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            string yaml = oktaWizardConfig.ToYaml();
            string encryptedYaml = dataProtector.Protect(yaml);
            File.WriteAllText(filePath, encryptedYaml);
            return oktaWizardConfig;
        }

        private static void BackUpFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string baseFileName = Path.GetFileNameWithoutExtension(filePath);
            int number = 0;
            while (fileInfo.Exists)
            {
                string fileName = $"{baseFileName}_{++number}.yaml";
                fileInfo = new FileInfo(Path.Combine(fileInfo.DirectoryName, fileName));
            }

            File.Move(filePath, fileInfo.FullName);
        }
    }
}
