using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Okta.Wizard.Tests
{
    public class CascadingOktaWizardPathResolverShould
    {
        ITestOutputHelper output;
        public CascadingOktaWizardPathResolverShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void NotResolvePathIfFileDoesntExists()
        {
            lock (EnvironmentLock.Object)
            {
                lock (FileLock.Object)
                {
                    Environment.SetEnvironmentVariable(EnvironmentOktaWizardPathResolver.OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME, "C:\\environment\\variable");

                    string homeWizardPath = new HomeOktaFolderOktaWizardPathResolver().ResolveOktaWizardPath(out _);
                    string defaultWizardPath = new DefaultOktaWizardPathResolver().ResolveOktaWizardPath(out _);
                    if (File.Exists(homeWizardPath))
                    {
                        File.Delete(homeWizardPath);
                    }
                    if (File.Exists(defaultWizardPath))
                    {
                        File.Delete(defaultWizardPath);
                    }

                    string testProjectDirectory = "C:\\test\\project";
                    CascadingOktaWizardPathResolver resolver = new CascadingOktaWizardPathResolver(testProjectDirectory);

                    string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);

                    checkedPaths.Length.Should().Be(4);
                    foreach (string path in checkedPaths)
                    {
                        output.WriteLine(path);
                    }
                    resolved.Should().BeNullOrEmpty();
                    checkedPaths[0].Should().Be("C:\\environment\\variable\\.okta\\wizard\\bin\\okta.exe");
                    checkedPaths[1].Should().Be("C:\\test\\project\\.okta\\wizard\\bin\\okta.exe");
                    checkedPaths[2].Should().Be(homeWizardPath);
                    checkedPaths[3].Should().Be(defaultWizardPath);
                    Environment.SetEnvironmentVariable(EnvironmentOktaWizardPathResolver.OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME, null);
                }
            }
        }

        [Fact]
        public void ResolvePathIfFileExists()
        {
            lock (EnvironmentLock.Object)
            {
                lock (FileLock.Object)
                {
                    DirectoryInfo dir = new DirectoryInfo($"./{nameof(ResolvePathIfFileExists)}");
                    FileInfo file = new FileInfo(Path.Combine(dir.FullName, OktaWizardPathResolver.RELATIVE_PATH));
                    if (!file.Directory.Exists)
                    {
                        file.Directory.Create();
                    }
                    file.Create().Close();
                    CascadingOktaWizardPathResolver resolver = new CascadingOktaWizardPathResolver(dir.FullName);
                    string resolved = resolver.ResolveOktaWizardPath(out _);
                    File.Exists(resolved).Should().Be(true);
                    output.WriteLine(resolved);
                }
            }
        }
    }
}
