using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace Okta.Wizard.Tests
{
    public class OktaWizardSettingsShould
    {
        private ITestOutputHelper output;
        public OktaWizardSettingsShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetDefaultSettings()
        {
            Environment.SetEnvironmentVariable(OktaWizardSettings.REGISTRATION_BASE_URL_ENVIRONMENT_VARIABLE, string.Empty);
            Environment.SetEnvironmentVariable(OktaWizardSettings.REGISTRATION_ID_ENVIRONMENT_VARIABLE, string.Empty);
            OktaWizardSettings oktaWizardSettings = new OktaWizardSettings();
            oktaWizardSettings.RegistrationBaseUrl.Should().Be(OktaWizardSettings.DEFAULT_REGISTRATION_BASE_URL);
            oktaWizardSettings.RegistrationId.Should().Be(OktaWizardSettings.DEFAULT_REGISTRATION_ID);
        }

        [Fact]
        public void GetEnvironmentVariableSettings()
        {
            string testBaseUrl = "this is a test base URL";
            string testRegistrationId = "this is a test registration id";
            Environment.SetEnvironmentVariable(OktaWizardSettings.REGISTRATION_BASE_URL_ENVIRONMENT_VARIABLE, testBaseUrl);
            Environment.SetEnvironmentVariable(OktaWizardSettings.REGISTRATION_ID_ENVIRONMENT_VARIABLE, testRegistrationId);

            OktaWizardSettings oktaWizardSettings = new OktaWizardSettings();
            oktaWizardSettings.RegistrationBaseUrl.Should().Be(testBaseUrl);
            oktaWizardSettings.RegistrationId.Should().Be(testRegistrationId);
        }

        [Fact]
        public void JsonWebKeyTest()
        {
            RSA rsa1 = RSA.Create(2048);
            RsaSecurityKey publicKey = new RsaSecurityKey(rsa1.ExportParameters(false));
            JsonWebKey jsonWebKey = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicKey);
            string json = JsonConvert.SerializeObject(jsonWebKey, Formatting.Indented);
            output.WriteLine(json);
        }
    }
}