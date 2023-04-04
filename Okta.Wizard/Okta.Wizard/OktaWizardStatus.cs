using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public enum OktaWizardStatus
    {
        Idle,
        RunStarted,
        RunComplete,
        CreateNewOrgRequired,
        CreateNewOrgStarted,
        CreateNewOrgComplete,
        CreateNewOrgException,
        NewOrgVerificationPending,
        NewOrgVerificationComplete,
        NewOrgVerificationException,
        CreateApplicationStarted,
        CreateApplicationComplete,
        CreateApplicationException,
        ConfigureProjectStarted,
        ConfigureProjectComplete,
        ConfigureProjectException,
        Error
    }
}
