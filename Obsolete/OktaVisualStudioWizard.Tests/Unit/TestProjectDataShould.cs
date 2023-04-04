using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Okta.Wizard.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard.Tests.Unit
{
    [TestClass]
    public class TestProjectDataShould
    {
        [TestMethod]
        public void DeserializeSuccessfully()
        {
            TestProjectData testProjectData = Deserialize.FromJsonFile<TestProjectData>("./TestProjectData.json");
            testProjectData.Should().NotBeNull();
            testProjectData.Time.Should().Be("2/7/2021 9:34:34 AM");
            testProjectData.Year.Should().Be("2021");
            testProjectData.UserName.Should().Be("user.name");
            testProjectData.UserDomain.Should().Be("usermachinename");
            testProjectData.MachineName.Should().Be("usermachinename");
            testProjectData.ClrVersion.Should().Be("4.0.30319.42000");
            testProjectData.RegisteredOrganization.Should().Be("");
            testProjectData.RunSilent.Should().Be("False");
            testProjectData.SolutionDirectory.Should().Be("C:\\source\\repos\\OktaApplicationWizard28");
            testProjectData.ProjectName.Should().Be("OktaApplicationWizard28");
            testProjectData.SafeProjectName.Should().Be("OktaApplicationWizard28");
            testProjectData.CurrentUiCultureName.Should().Be("en-US");
            testProjectData.InstallPath.Should().Be("C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\Common7\\IDE\\");
            testProjectData.SpecifiedSolutionName.Should().Be("");
            testProjectData.ExclusiveProject.Should().Be("True");
            testProjectData.DestinationDirectory.Should().Be("C:\\source\\repos\\OktaApplicationWizard28");
            testProjectData.TargetFrameworkVersion.Should().Be("4.5");
        }
    }
}
