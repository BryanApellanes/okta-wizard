using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Tests
{
    public class OrganizationRequestShould
    {
        [Fact]
        public void BeEqual()
        {
            string firstName = System.Guid.NewGuid().ToString();
            string lastName = System.Guid.NewGuid().ToString();
            string email = System.Guid.NewGuid().ToString();
            string country = System.Guid.NewGuid().ToString();
            OrganizationRequest requestOne = new OrganizationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Country = country
            };
            OrganizationRequest requestTwo = new OrganizationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Country = country
            };

            requestOne.Equals(requestTwo).Should().BeTrue();
        }

        [Fact]
        public void NotBeEqual()
        {
            string firstName = System.Guid.NewGuid().ToString();
            string lastName = System.Guid.NewGuid().ToString();
            string email = System.Guid.NewGuid().ToString();
            string country = System.Guid.NewGuid().ToString();
            OrganizationRequest requestOne = new OrganizationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Country = country
            };
            OrganizationRequest requestTwo = new OrganizationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = "different value",
                Country = country
            };

            requestOne.Equals(requestTwo).Should().BeFalse();
        }
    }
}
