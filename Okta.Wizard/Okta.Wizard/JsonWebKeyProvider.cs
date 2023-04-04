using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Okta.Wizard
{
    public class JsonWebKeyProvider : IJsonWebKeyProvider
    {
        public Sdk.Model.JsonWebKey CreateJsonWebKey(string kid)
        {
            RSA rsa = RSA.Create();
            rsa.KeySize = 2048;
            RsaSecurityKey publicKey = new RsaSecurityKey(rsa.ExportParameters(false));
            Microsoft.IdentityModel.Tokens.JsonWebKey jsonWebKey = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicKey);
            jsonWebKey.Kid = kid;
            string json = JsonConvert.SerializeObject(jsonWebKey);
            return JsonConvert.DeserializeObject<Sdk.Model.JsonWebKey>(json);
        }
    }
}
