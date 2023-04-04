using System.IO;

namespace Okta.Wizard
{
    public abstract class OktaWizardPathResolver : LogEventWriter, IOktaWizardPathResolver
    {
        public const string DEFAULT_PATH = "~/.okta/wizard/bin/okta.exe";
        public const string RELATIVE_PATH = ".okta/wizard/bin/okta.exe";
        public OktaWizardPathResolver()
        {
            this.Logger = new FileLogger(GetType());
        }

        public virtual bool OktaWizardExists(out string[] checkedPaths)
        {
            string path = ResolveOktaWizardPath(out checkedPaths);
            bool result = File.Exists(path);
            if (result)
            {
                Info($"Found okta.exe at {path}");
            }
            return result;
        }

        public abstract string ResolveOktaWizardPath(out string[] checkedPaths);
    }
}
