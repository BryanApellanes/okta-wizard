using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    /// <summary>
    /// Resolves the okta.exe path relative to the path specified in the environment variable OKTA_WIZARD_PATH. 
    /// For example, if the value of the environment variable OKTA_WIZARD_PATH is set to "C:\\test\\bin", the
    /// resolved path for okta.exe would be "C:\\test\\bin\\.okta\\wizard\\bin\\okta.exe".
    /// </summary>
    public class EnvironmentOktaWizardPathResolver : OktaWizardPathResolver
    {
        public const string OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME = "OKTA_WIZARD_PATH";

        public override string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            List<string> paths = new List<string>();
            string environmentVariableValue = Environment.GetEnvironmentVariable(OKTAWIZARDPATH_ENVIRONMENT_VARIABLE_NAME);

            FileInfo pathInfo;
            if (string.IsNullOrEmpty(environmentVariableValue))
            {
                pathInfo = new FileInfo(HomePath.Resolve(DEFAULT_PATH));
                paths.Add(pathInfo.FullName);
                checkedPaths = paths.ToArray();
                return pathInfo.FullName;
            }
            pathInfo = new FileInfo(Path.Combine(HomePath.Resolve(environmentVariableValue), RELATIVE_PATH));
            paths.Add(pathInfo.FullName);
            checkedPaths = paths.ToArray();
            return pathInfo.FullName;
        }
    }
}
