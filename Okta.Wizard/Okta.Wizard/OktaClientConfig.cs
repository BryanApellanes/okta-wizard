using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class OktaClientConfig : Jsonable
    {
        public string OktaDomain { get; set; }
        public string Token { get; set; }
    }
}
