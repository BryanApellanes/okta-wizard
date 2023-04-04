using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class OktaWizardSettings : IOktaWizardSettings
    {
        public const string DEFAULT_REGISTRATION_BASE_URL = "https://okta-devok12.okta.com";
        public const string DEFAULT_REGISTRATION_ID = "reg405abrRAkn0TRf5d6";

        public const string REGISTRATION_BASE_URL_ENVIRONMENT_VARIABLE = "OKTA_WIZARD_REGISTRATION_BASE_URL";
        public const string REGISTRATION_ID_ENVIRONMENT_VARIABLE = "OKTA_WIZARD_REGISTRATION_ID";

        public const string REGISTRATION_TEMP_FILE_PATH = "~/.okta/wizard/PendingRegistration.json";

        public const string WIZARD_CONFIGURATION_FILE_PATH = "~/.okta/wizard/wizard.yaml";
        public const string WIZARD_LOG_FILE_PATH = "~/.okta/wizard/logs/";

        public const int POLL_INTERVAL = 4;

        public string RegistrationBaseUrl 
        {
            get => GetRegistrationBaseUrl();
        }

        public string RegistrationId 
        {
            get => GetRegistrationId();
        }

        public int PollingIntervalSeconds
        {
            get => POLL_INTERVAL;
        }

        protected virtual string GetRegistrationBaseUrl()
        {
            return Environment.GetEnvironmentVariable(REGISTRATION_BASE_URL_ENVIRONMENT_VARIABLE) ?? DEFAULT_REGISTRATION_BASE_URL;
        }

        protected virtual string GetRegistrationId()
        {
            return Environment.GetEnvironmentVariable(REGISTRATION_ID_ENVIRONMENT_VARIABLE) ?? DEFAULT_REGISTRATION_ID;
        }
    }
}
