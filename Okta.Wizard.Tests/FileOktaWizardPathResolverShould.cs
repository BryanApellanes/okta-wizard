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
    public class FileOktaWizardPathResolverShould
    {
        ITestOutputHelper output;
        public FileOktaWizardPathResolverShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ReadPathFromFileInCurrentDirectory()
        {
            lock (FileLock.Object)
            {
                string fileContent = $"C:\\temp\\{nameof(ReadPathFromFileInCurrentDirectory)}";
                string expectedExePath = $"{fileContent}\\.okta\\wizard\\bin\\okta.exe";
                FileInfo expectedFileInfo = new FileInfo(expectedExePath);
                expectedFileInfo.Directory.Create();
                File.Create(expectedExePath);
                FileInfo pathFileToRead = new FileInfo(Path.Combine(".", FileOktaWizardPathResolver.FILE));
                pathFileToRead.Directory.Create();
                File.WriteAllText(pathFileToRead.FullName, fileContent);


                FileOktaWizardPathResolver resolver = new FileOktaWizardPathResolver();
                string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
                resolved.Should().Be(expectedExePath);

                output.WriteLine($"{nameof(FileOktaWizardPathResolver)} checked {checkedPaths[0]}");
                pathFileToRead.Delete();
            }
        }

        [Fact]
        public void ReadPathFromFileInDotOktaDirectory()
        {
            lock (FileLock.Object)
            {
                string fileContent = $"C:\\temp\\{nameof(ReadPathFromFileInDotOktaDirectory)}";
                string expectedExePath = $"{fileContent}\\.okta\\wizard\\bin\\okta.exe";
                FileInfo expectedFileInfo = new FileInfo(expectedExePath);
                expectedFileInfo.Directory.Create();
                File.Create(expectedExePath);
                FileInfo pathFileToRead = new FileInfo(Path.Combine(".", ".okta", FileOktaWizardPathResolver.FILE));
                pathFileToRead.Directory.Create();
                File.WriteAllText(pathFileToRead.FullName, fileContent);


                FileOktaWizardPathResolver resolver = new FileOktaWizardPathResolver();
                string resolved = resolver.ResolveOktaWizardPath(out string[] checkedPaths);
                resolved.Should().Be(expectedExePath);

                output.WriteLine($"{nameof(FileOktaWizardPathResolver)} checked {checkedPaths[0]}");
                pathFileToRead.Delete();
            }
        }
    }
}
