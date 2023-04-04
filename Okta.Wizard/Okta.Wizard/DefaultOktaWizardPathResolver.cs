using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    public class DefaultOktaWizardPathResolver : OktaWizardPathResolver
    {
        public override string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            string fullPath = new FileInfo(HomePath.Resolve(DEFAULT_PATH)).FullName;
            checkedPaths = new string[1] { fullPath };
            return fullPath;
        }
    }
}
