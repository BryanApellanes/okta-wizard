using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using Okta.Wizard;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaVisualStudioWizard
{
    public class OktaVisualStudioWizard : IWizard
    {
        private VisualStudioWizardEventHandlers visualStudioWizardEventHandlers;
        private VisualStudioWizardContext visualStudioWizardContext;
        private CascadingOktaWizardPathResolver oktaWizardPathResolver;

        public OktaVisualStudioWizard()
        {
            this.visualStudioWizardEventHandlers = new VisualStudioWizardEventHandlers()
            {
                RunFinished = () =>
                {
                    visualStudioWizardContext.ProjectArguments.Save();
                    FileLogger logger = new FileLogger(typeof(OktaVisualStudioWizard));
                    string wizardExePath = ResolveOktaWizardPath(out string[] checkedPaths);
                    if (!string.IsNullOrEmpty(wizardExePath) && File.Exists(wizardExePath))
                    {
                        logger.Info($"Executing okta.exe found at {wizardExePath}");
                        wizardExePath.Run();
                    }
                    else
                    {
                        logger.Warn($"okta.exe was not found, checked paths: {string.Join("\r\n", checkedPaths)}");
                    }
                }
            };
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            this.visualStudioWizardEventHandlers.BeforeOpeningFile(projectItem);
        }

        public void ProjectFinishedGenerating(Project project)
        {
            this.visualStudioWizardEventHandlers.ProjectFinishedGenerating(project);
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            this.visualStudioWizardEventHandlers.ProjectItemFinishedGenerating(projectItem);
        }

        public void RunFinished()
        {
            this.visualStudioWizardEventHandlers.RunFinished();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            ProjectArguments projectArguments = ProjectArguments.FromDictionary(replacementsDictionary);
            this.visualStudioWizardContext = new VisualStudioWizardContext()
            {
                AutomationObject = automationObject,
                ReplacementsDictionary = replacementsDictionary,
                WizardRunKind = runKind.ToString(),
                CustomParams = customParams,
                ProjectArguments = projectArguments
            };
            this.visualStudioWizardEventHandlers.Context = this.visualStudioWizardContext;
            this.oktaWizardPathResolver = new CascadingOktaWizardPathResolver(projectArguments.DestinationDirectory, new FileLogger(GetType()));
            this.visualStudioWizardEventHandlers.RunStarted(automationObject, replacementsDictionary, runKind.ToString(), customParams);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return this.visualStudioWizardEventHandlers.ShouldAddProjectItem(filePath);
        }

        public string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            this.oktaWizardPathResolver = new CascadingOktaWizardPathResolver(this.visualStudioWizardContext.ProjectArguments.DestinationDirectory, new FileLogger(GetType()));
            return oktaWizardPathResolver.ResolveOktaWizardPath(out checkedPaths);
        }
    }
}
