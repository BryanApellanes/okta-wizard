using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    public class PendingRegistration : Jsonable
    {
        public OrganizationRequest Request { get; set; }
        public OrganizationResponse Response { get; set;}

        public static bool Exists(string filePath = null)
        {
            return Exists(out PendingRegistration _, filePath);
        }

        public static bool Exists(out PendingRegistration pendingRegistration, string filePath = null)
        {
            pendingRegistration = Load(filePath);
            return pendingRegistration != null;
        }

        public static PendingRegistration Load(string filePath = null)
        {
            string fullPath = HomePath.Resolve(filePath ?? OktaWizardSettings.REGISTRATION_TEMP_FILE_PATH);
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                return JsonConvert.DeserializeObject<PendingRegistration>(json);
            }
            return null;
        }

        public static void Delete(string filePath = null)
        {
            string fullPath = HomePath.Resolve(filePath ?? OktaWizardSettings.REGISTRATION_TEMP_FILE_PATH);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
