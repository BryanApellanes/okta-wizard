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
    public class HomeOktaFolderOktaWizardPathResolverShould
    {
        ITestOutputHelper output;
        public HomeOktaFolderOktaWizardPathResolverShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CheckHomePath()
        {
            string home = HomePath.Resolve("~");
            HomeOktaFolderOktaWizardPathResolver resolver = new HomeOktaFolderOktaWizardPathResolver();
            string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
            checkedPaths.Length.Should().Be(1);
            checkedPaths[0].Should().StartWith(home);
            checkedPaths[0].Should().Be(resolved);
            output.WriteLine($"{nameof(HomeOktaFolderOktaWizardPathResolver)} checked {checkedPaths[0]}");
        }
    }
}
