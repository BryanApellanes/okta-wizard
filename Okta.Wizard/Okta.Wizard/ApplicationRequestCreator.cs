using Newtonsoft.Json;
using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class ApplicationRequestCreator : LogEventWriter, IApplicationRequestCreator
    {
        public ApplicationRequestCreator(IApplicationDefinitionProvider applicationDefinitionProvider, ILogger logger) : base(logger)
        {
            this.ApplicationDefinitionProvider = applicationDefinitionProvider;
        }

        protected IApplicationDefinitionProvider ApplicationDefinitionProvider { get; private set; }

        public async Task<CreateApplicationRequest> CreateApplicationRequestAsync(ApplicationDefinitionArguments applicationDefinitionArguments)
        {
            return new CreateApplicationRequest(applicationDefinitionArguments, await GetApplicationDefinitionAsync(applicationDefinitionArguments));
        }

        public async Task<Application> GetApplicationDefinitionAsync(ApplicationDefinitionArguments applicationDefinitionArguments)
        {
            return await ApplicationDefinitionProvider.GetApplicationDefinitionAsync(applicationDefinitionArguments);
        }
    }
}
