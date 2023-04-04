using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class ApiException : Exception
    {
        public ApiException(OrganizationErrorResponse errorResponse) : base(GetMessage(errorResponse))
        { }

        private static string GetMessage(OrganizationErrorResponse errorResponse)
        {
            StringBuilder messageBuilder = new StringBuilder();
            foreach(ErrorCause cause in errorResponse.ErrorCauses)
            {
                messageBuilder.AppendLine(cause.ErrorSummary);
            }
            return messageBuilder.ToString();
        }
    }
}
