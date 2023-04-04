using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IOktaWizardSettings
    {
        string RegistrationBaseUrl { get; }
        string RegistrationId { get; }

        int PollingIntervalSeconds { get; }
    }
}
