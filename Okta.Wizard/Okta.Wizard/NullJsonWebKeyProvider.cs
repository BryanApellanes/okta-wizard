using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class NullJsonWebKeyProvider : IJsonWebKeyProvider
    {
        public JsonWebKey CreateJsonWebKey(string keyId)
        {
            return null;
        }
    }
}
