using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard;
using Okta.Wizard.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class OktaApplicationSettingsObservableShould
    {
        [TestMethod]
        public void ReturnApiToken()
        {
            string testDomain = $"{nameof(ReturnApiToken)}_TestDomain";
            string testApiToken = $"{nameof(ReturnApiToken)}_TestApiToken";
            OktaApplicationSettingsObservable oktaAppSettingsObservable = new OktaApplicationSettingsObservable
            {
                ApiToken = testApiToken,
            };

            ApiCredentials apiCreds = oktaAppSettingsObservable.ToApiCredentials();
            apiCreds.Should().NotBeNull();
            apiCreds.Domain.Should().Be(testDomain);
            apiCreds.Token.Should().Be(testApiToken);
        }
    }
}
