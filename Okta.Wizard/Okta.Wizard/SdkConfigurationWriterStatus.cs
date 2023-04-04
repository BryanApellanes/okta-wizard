using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public enum SdkConfigurationWriterStatus
    {
        Invalid,
        Idle,
        CreatingNewOrgStarted,
        CreatingNewOrgComplete,
        CreatingNewOrgException,
        NewOrgVerificationPending,
        NewOrgVerificationComplete,
        NewOrgVerificationException,
    }
}
