using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaWizardStatusTransition
    {
        public OktaWizardStatus PreviousStatus { get; set; }
        public OktaWizardStatus CurrentStatus { get; set; }
    }
}
