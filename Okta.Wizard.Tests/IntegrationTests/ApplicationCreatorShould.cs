using FluentAssertions;
using Okta.Sdk.Api;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Tests.IntegrationTests
{
    public class ApplicationCreatorShould
    {
        [Fact]
        public async void CreateApplication()
        {
            ApplicationDefinitionProvider applicationDefinitionProvider = new ApplicationDefinitionProvider(new NullJsonWebKeyProvider());
            ApplicationRequestCreator applicationRequestCreator = new ApplicationRequestCreator(applicationDefinitionProvider, new FileLogger());
            SdkConfig sdkConfig = new SdkConfig("https://dev-04778726.okta.com", "00vckjPFOj-BDiWwIeokpkoAF4zhc3Jk0yClsApzJ6");
            ProjectArguments projectArguments = new ProjectArguments
            {
                ProjectName = "TestProject"
            };
            ApplicationApi api = new ApplicationApi(new Sdk.Client.Configuration { OktaDomain = sdkConfig.Okta.Client.OktaDomain, Token = sdkConfig.Okta.Client.Token });
            ApplicationCreator applicationCreator = new ApplicationCreator(applicationRequestCreator, api);
            CreateApplicationResult applicationResponse = await applicationCreator.CreateApplicationAsync(new ApplicationDefinitionArguments(sdkConfig, projectArguments));
            applicationResponse.Should().NotBeNull();
            applicationResponse.OperationSucceeded.Should().BeTrue(); 
        }
    }
}
