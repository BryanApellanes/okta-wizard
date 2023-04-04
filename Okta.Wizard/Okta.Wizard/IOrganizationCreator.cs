using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IOrganizationCreator
    {
        Task<OrganizationResponse> CreateNewOrganizationAsync(OrganizationRequest createOrganizationRequest);

        Task<OrganizationResponse> VerifyNewOrganizationAsync(string identifier);
    }
}
