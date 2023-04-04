using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaWizardRunResult : Jsonable
    {
        public OktaWizardRunResult(OktaWizardRunArguments arguments)
        {
            this.Arguments = arguments;
        }

        public OktaWizardRunArguments Arguments { get; private set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        public OktaWizardStatus OktaWizardStatus { get; set; }

        public CreateApplicationResult CreateApplicationResult { get; set; }

        public ProjectConfigurationResult ProjectConfigurationResult { get; set; }
    }
}
