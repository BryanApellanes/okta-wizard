using Newtonsoft.Json;
using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class CreateApplicationResult
    {
        public CreateApplicationResult(ApplicationDefinitionArguments applicationDefinitionArguments)
        {
            this.ApplicationDefinitionArguments = applicationDefinitionArguments;
            this.OperationSucceeded = true;
        }

        public CreateApplicationResult(ApplicationDefinitionArguments applicationDefinitionArguments, Exception ex)
        {
            this.ApplicationDefinitionArguments = applicationDefinitionArguments;
            this.OperationSucceeded = false;
            this.Message = $"{ex.Message}\r\n{ex.StackTrace}";
        }

        public OpenIdConnectApplication CreatedApplication { get; set; }
        public ApplicationDefinitionArguments ApplicationDefinitionArguments { get; set; }
        public bool OperationSucceeded { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
