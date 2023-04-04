using System;
using System.IO;
using YamlDotNet.Serialization;

namespace Okta.Wizard.Config
{
    /// <summary>
    /// Represents the minimal configuration to be used by the management Sdk during wizard operations.
    /// </summary>
    public class SdkConfig
    {
        public SdkConfig() { }
        public SdkConfig(string domain, string token) 
        {
            this.Okta = new OktaConfig
            {
                Client = new ClientConfig
                {
                    OktaDomain = domain,
                    Token = token
                }
            };
        }

        /// <summary>
        /// Gets or sets the Okta config.
        /// </summary>
        [YamlMember(Alias = "okta")]
        public OktaConfig Okta { get; set; }

        /// <summary>
        /// Determines if the configuration file exists.
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static bool Exists(string configPath = null)
        {
            configPath = configPath ?? OktaWizardSettings.WIZARD_CONFIGURATION_FILE_PATH;
            string filePath = new FileInfo(HomePath.Resolve(configPath)).FullName;
            return File.Exists(filePath);
        }

        /// <summary>
        /// Deletes the configuration file.
        /// </summary>
        /// <param name="configPath"></param>
        public static void Delete(string configPath = null)
        {
            configPath = configPath ?? OktaWizardSettings.WIZARD_CONFIGURATION_FILE_PATH;
            string filePath = new FileInfo(HomePath.Resolve(configPath)).FullName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Loads the configuration file.
        /// </summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <returns>SdkConfig</returns>
        public static SdkConfig Load(string configPath = null)
        {
            configPath = configPath ?? OktaWizardSettings.WIZARD_CONFIGURATION_FILE_PATH;
            string filePath = new FileInfo(HomePath.Resolve(configPath)).FullName;
            if (!File.Exists(filePath))
            {
                return null;
            }
            Deserializer deserializer = new Deserializer();
            return deserializer.Deserialize<SdkConfig>(File.ReadAllText(filePath));
        }

        /// <summary>
        /// Tries to save the configuration file.
        /// </summary>
        /// <param name="ex">The exception that occurred, if any.</param>
        /// <returns>True if the file was saved.</returns>
        public bool TrySave(out Exception ex)
        {
            return TrySave(OktaWizardSettings.WIZARD_CONFIGURATION_FILE_PATH, out ex);
        }

        /// <summary>
        /// Tries to save the configuration file.
        /// </summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <param name="ex">The exception that occurred, if any. </param>
        /// <returns>True if the file was save.</returns>
        public bool TrySave(string configPath, out Exception ex)
        {
            ex = null;
            try
            {
                Save(configPath);
                return true;
            }
            catch (Exception e)
            {
                ex = e;
            }
            return false;
        }

        /// <summary>
        /// Saves the configuration file.
        /// </summary>
        /// <param name="configPath">The path to save the configuration to.</param>
        public void Save(string configPath = null)
        {
            configPath = configPath ?? OktaWizardSettings.WIZARD_CONFIGURATION_FILE_PATH;
            string filePath = HomePath.Resolve(configPath);
            Serializer serializer = new Serializer();
            File.WriteAllText(filePath, serializer.Serialize(this));
        }
    }
}
