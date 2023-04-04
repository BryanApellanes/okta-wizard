using FluentAssertions;
using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Okta.Wizard.Tests
{
    public class JsonWebKeyProviderShould
    {
        private ITestOutputHelper output;
        public JsonWebKeyProviderShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CreateJsonWebKey()
        {
            string testKid = "This is a test web key";

            JsonWebKeyProvider jsonWebKeyProvider = new JsonWebKeyProvider();
            JsonWebKey jsonWebKey = jsonWebKeyProvider.CreateJsonWebKey(testKid);
            
            jsonWebKey.Should().NotBeNull();
            jsonWebKey.E.Should().NotBeNull();
            jsonWebKey.E.Should().Be("AQAB");
            jsonWebKey.Kid.Should().NotBeNull();
            jsonWebKey.Kid.Should().Be(testKid);
            jsonWebKey.Kty.Should().NotBeNull();
            jsonWebKey.Kty.Should().Be("RSA");
        }
    }
}
