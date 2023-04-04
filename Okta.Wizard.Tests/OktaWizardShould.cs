using NSubstitute;
using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Tests
{
    public class OktaWizardShould
    {
        [Fact]
        public async void CallApplicationCreator()
        {
            IOrganizationCreator mockOrgCreator = Substitute.For<IOrganizationCreator>();
            IApplicationRequestCreator mockApplicationRequestCreator = Substitute.For<IApplicationRequestCreator>();
            IOktaWizardSettings mockOktaWizardSettings = Substitute.For<IOktaWizardSettings>();
            ISdkConfigurationWriter mockSdkConfigurer = Substitute.For<ISdkConfigurationWriter>();
            IProjectConfigurationWriter mockProjectConfigurationWriter = Substitute.For<IProjectConfigurationWriter>();
            
            OktaWizard oktaWizard = new OktaWizard(mockOktaWizardSettings, mockSdkConfigurer, mockApplicationRequestCreator, mockProjectConfigurationWriter);

            ApplicationDefinitionArguments arguments = new ApplicationDefinitionArguments(new Config.SdkConfig(), new ProjectArguments());
            await oktaWizard.CreateApplicationAsync(new Sdk.Api.ApplicationApi(), arguments);

            mockApplicationRequestCreator.Received().CreateApplicationRequestAsync(arguments);
        }
    }
}
