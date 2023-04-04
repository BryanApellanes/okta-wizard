using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class CreateApplicationRequest
    {
        public CreateApplicationRequest(ApplicationDefinitionArguments applicationDefinitionArguments, Application application)
        {
            this.ApplicationDefinitionArguments = applicationDefinitionArguments;
            this.Application = application;
        }

        public ApplicationDefinitionArguments ApplicationDefinitionArguments { get; set; }
        public Application Application { get; set; }
    }
}
