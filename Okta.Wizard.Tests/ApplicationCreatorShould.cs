using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Tests
{
    public class ApplicationCreatorShould
    {
        [Fact]
        public void CallJsonWebKeyProvider()
        {
            IJsonWebKeyProvider mockJsonWebKeyProvider = new JsonWebKeyProvider();
            //ApplicationCreator applicationCreator = new ApplicationCreator()
        }
    }
}
