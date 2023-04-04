using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.VisualStudio
{
    public class VisualStudioWizardContext
    {
        public object AutomationObject { get; set; }
        public Dictionary<string, string> ReplacementsDictionary { get; set; }

        public string WizardRunKind { get; set; }

        public object[] CustomParams { get; set; }

        public ProjectArguments ProjectArguments { get; set; }
    }
}
