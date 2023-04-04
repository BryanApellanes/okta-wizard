using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public interface IOktaWizardPathResolver
    {
        string ResolveOktaWizardPath(out string[] checkedPaths);

        bool OktaWizardExists(out string[] checkedPaths);
    }
}
