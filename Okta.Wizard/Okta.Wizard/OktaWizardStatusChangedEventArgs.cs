using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaWizardStatusChangedEventArgs : EventArgs
    {
        public OktaWizardStatus Status { get; set; }
        public OktaWizardStatusTransition Transition { get; set; }
        public Exception Exception { get; set; }

        public OktaWizardRunResult RunResult { get; set; }
    }
}
