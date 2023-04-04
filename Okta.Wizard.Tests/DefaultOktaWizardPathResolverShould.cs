using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Okta.Wizard.Tests
{
    public class DefaultOktaWizardPathResolverShould
    {
        ITestOutputHelper output;
        public DefaultOktaWizardPathResolverShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CheckDefaultPath()
        {
            DefaultOktaWizardPathResolver resolver = new DefaultOktaWizardPathResolver();
            string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
            checkedPaths.Length.Should().Be(1);
            checkedPaths[0].Should().Be(resolved);
            output.WriteLine($"{nameof(DefaultOktaWizardPathResolver)} checked {checkedPaths[0]}");
        }
    }
}
