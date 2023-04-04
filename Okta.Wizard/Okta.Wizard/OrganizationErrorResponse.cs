using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OrganizationErrorResponse : OrganizationResponse
    {
        public OrganizationErrorResponse()
        {
            this.OperationSucceeded = false;
        }

        public string ErrorCode { get; set; }
        public string ErrorSummary { get; set; }
        public string ErrorLink { get; set; }
        public string ErrorId { get; set; }
        public ErrorCause[] ErrorCauses { get; set; }
    }
}
