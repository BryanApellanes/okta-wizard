using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class OrganizationRequest : Jsonable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }
            if (obj is OrganizationRequest other)
            {
                if (this.FirstName != null)
                {
                    if (!this.FirstName.Equals(other.FirstName))
                    {
                        return false;
                    }
                }
                else if (other.FirstName != null)
                {
                    return false;
                }
                if (this.LastName != null)
                {
                    if (!this.LastName.Equals(other.LastName))
                    {
                        return false;
                    }
                }
                else if (other.LastName != null)
                {
                    return false;
                }
                if (this.Email != null)
                {
                    if (!this.Email.Equals(other.Email))
                    {
                        return false;
                    }
                }
                else if (other.Email != null)
                {
                    return false;
                }
                if (this.Country != null)
                {
                    if (!this.Country.Equals(other.Country))
                    {
                        return false;
                    }
                }
                else if (other.Country != null)
                {
                    return false;
                }
                return true;
            }

            return base.Equals(obj);
        }
    }
}
