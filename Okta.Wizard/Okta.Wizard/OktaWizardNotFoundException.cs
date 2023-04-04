using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaWizardNotFoundException : Exception
    {
        public OktaWizardNotFoundException(params string[] checkedPaths) : base($"Okta Wizard not found, checked paths: {string.Join(", ", checkedPaths)}")
        { }
    }
}
