using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    public class OktaFolderOktaWizardPathResolver : OktaWizardPathResolver
    {
        public OktaFolderOktaWizardPathResolver(string directoryContainingDotOkta = ".")
        {
            this.DirectoryPath = directoryContainingDotOkta;
        }

        public string DirectoryPath { get; set; }
        public override string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            string path = Path.Combine(DirectoryPath, RELATIVE_PATH);
            FileInfo okwExe = new FileInfo(path);
            checkedPaths = new string[1] { okwExe.FullName };
            return okwExe.FullName;
        }
    }
}
