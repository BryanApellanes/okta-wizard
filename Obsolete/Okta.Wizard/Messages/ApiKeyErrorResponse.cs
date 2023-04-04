using DevEx.Internal;
using Okta.Wizard.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard.Messages
{
    /// <summary>
    /// A deserialization target for Okta.Wizard.Automation.ApiKeyErrorResponse.
    /// </summary>
    public class ApiKeyErrorResponse : Serializable
    {
        static ApiKeyErrorResponse()
        {
            FilePath = Path.Combine(OktaWizardConfig.OktaWizardHome, "aker.json");
        }

        public static string FilePath { get; set; }

        public Dictionary<string, object> OktaSignInFailedEventArgs { get; set; }

        public List<object> PageActionResults { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public bool FailureOccurred { get; set; }

        /// <summary>
        /// Loads the ApiKeyErrorResponse from the default path.
        /// </summary>
        /// <returns>ApiKeyErrorResponse</returns>
        public static ApiKeyErrorResponse Load()
        {
            return Deserialize.FromJsonFile<ApiKeyErrorResponse>(FilePath);
        }
    }
}
