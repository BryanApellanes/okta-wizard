using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public interface IJsonWebKeyProvider
    {
        JsonWebKey CreateJsonWebKey(string keyId);
    }
}
