using Okta.Sdk.Client;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IProjectConfiguration
    {
        SdkConfig SdkConfig { get; set; }
        ProjectArguments ProjectArguments { get; set; }
    }
}
