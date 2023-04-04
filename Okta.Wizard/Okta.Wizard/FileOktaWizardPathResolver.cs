using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    public class FileOktaWizardPathResolver : OktaWizardPathResolver
    {
        public const string FILE = "OKTA_WIZARD_PATH";
        public const string DOT_OKTA_DIR = ".okta";

        public override string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            List<string> paths = new List<string>();
            FileInfo fileInCurrentDirectory = new FileInfo(Path.Combine(".", FILE));
            if (fileInCurrentDirectory.Exists)
            {
                string fileContent = File.ReadAllText(fileInCurrentDirectory.FullName);
                FileInfo pathToCheck = new FileInfo(Path.Combine(fileContent, RELATIVE_PATH));
                paths.Add(pathToCheck.FullName);
                if (pathToCheck.Exists)
                {
                    checkedPaths = paths.ToArray();
                    return pathToCheck.FullName;
                }
            }

            FileInfo fileInDotOktaDir = new FileInfo(Path.Combine(".", DOT_OKTA_DIR, FILE));
            if (fileInDotOktaDir.Exists)
            {
                string fileContent = File.ReadAllText(fileInDotOktaDir.FullName);
                FileInfo pathToCheck = new FileInfo(Path.Combine(fileContent, RELATIVE_PATH));
                paths.Add(pathToCheck.FullName);
                if (pathToCheck.Exists)
                {
                    checkedPaths = paths.ToArray();
                    return pathToCheck.FullName;
                }
            }
            checkedPaths = paths.ToArray();
            return string.Empty;
        }
    }
}
