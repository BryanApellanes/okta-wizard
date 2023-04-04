using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.VisualStudio
{
    public class VisualStudioWizardEventHandlers
    {
        public VisualStudioWizardEventHandlers() 
        {
            this.BeforeOpeningFile = (o) => { };
            this.ProjectFinishedGenerating = (o) => { };
            this.ProjectItemFinishedGenerating = (o) => { };
            this.RunFinished = () => { };
            this.RunStarted = (o, d, s, oa) => { };
            this.ShouldAddProjectItem = (s) => true;
        }

        public VisualStudioWizardContext Context { get; set; }

        public Action<object> BeforeOpeningFile { get; set; }

        public Action<object> ProjectFinishedGenerating { get; set; }

        public Action<object> ProjectItemFinishedGenerating { get; set; }

        public Action RunFinished { get; set; }

        public Action<object, Dictionary<string, string>, string, object[]> RunStarted { get; set; }

        public Func<string, bool> ShouldAddProjectItem { get; set; }
    }
}
