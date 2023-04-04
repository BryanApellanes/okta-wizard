using Okta.Sdk.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    /// <summary>
    /// This class exists so the type can be used as a generic argument.
    /// ApplicationApi does not have a parameterles constructor.
    /// </summary>
    public class OktaWizardApplicationApi : ApplicationApi
    {
    }
}
