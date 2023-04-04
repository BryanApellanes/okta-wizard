using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class CreateNewOrganizationFailedOrganizationResponse : OrganizationResponse
    {
        public CreateNewOrganizationFailedOrganizationResponse(OrganizationResponse actual, Exception ex) 
        {
            this.Exception = ex;

            this.Actual = actual;
            this.OrgUrl = actual?.OrgUrl;
            this.ApiToken = actual?.ApiToken;
            this.Identifier = actual?.Identifier;
            this.OrgStatus = (OrgStatus)actual?.OrgStatus;
            this.OperationSucceeded = false;
        }

        protected OrganizationResponse Actual { get; set; }

        public Exception Exception { get; set; }
    }
}
