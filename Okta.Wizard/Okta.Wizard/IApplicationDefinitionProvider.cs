using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IApplicationDefinitionProvider
    {
        Task<OpenIdConnectApplication> GetApplicationDefinitionAsync(ApplicationDefinitionArguments applicationDefinitionParameters);
    }
}
