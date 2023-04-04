using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class VerifyNewOrganizationFailedException : Exception
    {
        public VerifyNewOrganizationFailedException(Exception innerException): base($"Failed to verify new organization: ({innerException?.Message})", innerException)
        { 
        }
    }
}
