using Okta.Sdk.Api;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IApplicationCreator
    {
        Task<CreateApplicationRequest> CreateApplicationRequestAsync(ApplicationDefinitionArguments parameters);
        Task<CreateApplicationResult> CreateApplicationAsync(ApplicationDefinitionArguments parameters);
    }
}
