using FluentAssertions;
using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Okta.Wizard.Tests
{
    public class EnvironmentOktaWizardPathResolverShould
    {
        ITestOutputHelper output;
        public EnvironmentOktaWizardPathResolverShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CheckDefaultPathIfEnvironmentVariableNotSet()
        {
            lock (EnvironmentLock.Object)
            {
                Environment.SetEnvironmentVariable(EnvironmentOktaWizardPathResolver.OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME, null);

                string home = HomePath.Resolve("~");
                EnvironmentOktaWizardPathResolver resolver = new EnvironmentOktaWizardPathResolver();
                string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
                checkedPaths.Length.Should().Be(1);
                checkedPaths[0].Should().StartWith(home);
                checkedPaths[0].Should().Be(resolved);
                output.WriteLine($"{nameof(EnvironmentOktaWizardPathResolver)} checked {checkedPaths[0]}");
            }
        }

        [Fact]
        public void CheckEnvironmentVariableValue()
        {
            lock (EnvironmentLock.Object)
            {
                string testPath = "C:\\test\\path";
                string testFullPath = new FileInfo(Path.Combine(testPath, OktaWizardPathResolver.RELATIVE_PATH)).FullName;
                Environment.SetEnvironmentVariable(EnvironmentOktaWizardPathResolver.OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME, testPath);

                EnvironmentOktaWizardPathResolver resolver = new EnvironmentOktaWizardPathResolver();
                string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
                checkedPaths.Length.Should().Be(1);
                checkedPaths[0].Should().Be(resolved);
                resolved.Should().Be(testFullPath);
                output.WriteLine($"{nameof(EnvironmentOktaWizardPathResolver)} checked {checkedPaths[0]}");
                Environment.SetEnvironmentVariable(EnvironmentOktaWizardPathResolver.OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME, string.Empty);
            }
        }
    }
}
