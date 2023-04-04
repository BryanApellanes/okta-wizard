// <copyright file="OktaVisualStudioWizard.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Threading;
using Okta.VisualStudio.Wizard.Forms;
using Okta.Wizard;
using Okta.Wizard.Internal;

namespace Okta.VisualStudio.Wizard
{
    /// <summary>
    /// Okta specified implementation of IWizard.
    /// </summary>
    public class OktaVisualStudioWizard : IWizard
    {
        private readonly OktaWizard oktaWizard;
        private OktaWizardResult oktaWizardResult;
        private ProjectData projectData;
        private DTE dte;
        private static readonly Dictionary<Severity, MessageBoxIcon> MessageBoxIconsBySeverity = new Dictionary<Severity, MessageBoxIcon>
        {
            { Severity.Information, MessageBoxIcon.Information },
            { Severity.Warning, MessageBoxIcon.Warning },
            { Severity.Error, MessageBoxIcon.Error },
            { Severity.Fatal, MessageBoxIcon.Stop },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaVisualStudioWizard"/> class.
        /// </summary>
        public OktaVisualStudioWizard()
        {
            oktaWizard = new OktaWizardModule().TryGetOktaWizard();
            oktaWizard.Notify = (msg, sev) => MessageBox.Show(msg, "Okta Wizard", MessageBoxButtons.OK, MessageBoxIconsBySeverity[sev]);
        }

        // This method is called before opening any item that
        // has the OpenInEditor attribute.

        /// <inheritdoc/>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

#pragma warning disable VSTHRD100 // Avoid async void methods
        /// <inheritdoc/>
        public async void ProjectFinishedGenerating(Project project)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                if (projectData.GetSelectedVsTemplateName().Equals("OktaApplicationWizard"))  // TODO: stop using magic strings
                {
                    // because we are processing the template for the wizard itself
                    // the solution must be cleared for the projects of the actual templates to
                    // be included without naming collisions.
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    foreach (Project solutionProject in dte.Solution.Projects)
                    {
                        try
                        {
                            dte.Solution.Remove(solutionProject);
                            solutionProject.Delete();
                        }
                        catch
                        {
                            // avoid disposed object access exception, project is a COM object
                        }
                    }

                    ProjectTemplateParameters projectTemplateParameters = oktaWizardResult.GetProjectTemplateParameters();
                    FileInfo visualStudioTemplate = new FileInfo(projectTemplateParameters.VsTemplateFilePath);
                    dte.Solution.AddFromTemplate(projectTemplateParameters.VsTemplateFilePath, projectTemplateParameters.DestinationFolder, projectTemplateParameters.DestinationProjectName);
                }
            }
            catch (Exception ex)
            {
                oktaWizard.Notify($"An error occurred in ProjectFinishedGenerating:\r\n\r\n{ex}\r\n\r\n{ex.StackTrace}", Severity.Error);
            }
        }

        // This method is only called for item templates,
        // not for project templates.

        /// <inheritdoc/>
        public void ProjectItemFinishedGenerating(ProjectItem
            projectItem)
        {
        }

        // This method is called after the project is created.
#pragma warning disable VSTHRD100 // Avoid async void methods. Allow await in method body.
        /// <inheritdoc/>
        public async void RunFinished()
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                WizardRunFinishedResult runFinishedResult = await oktaWizard.RunFinished(oktaWizardResult);
                if (runFinishedResult?.TestUser != null)
                {
                    TestUserForm.ShowUserCredentials(runFinishedResult.TestUser, runFinishedResult.TestUserFile.FullName);
                }

                string resultYamlPath = Path.Combine(projectData.DestinationDirectory, $"{nameof(OktaWizardResult)}.yaml");
                if (File.Exists(resultYamlPath))
                {
                    File.Delete(resultYamlPath);
                }

                string welcomeFile = Path.Combine(projectData.DestinationDirectory, "OktaWelcome.md");
                if (!File.Exists(welcomeFile))
                {
                    welcomeFile = Path.Combine(projectData.DestinationDirectory, "Common", "OktaWelcome.md");
                }

                if (File.Exists(welcomeFile))
                {
                    try
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        dte.ItemOperations.OpenFile(welcomeFile);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                oktaWizard.Notify($"An error occurred in RunFinished:\r\n\r\n{ex}\r\n\r\n{ex.StackTrace}", Severity.Error);
            }
        }

#pragma warning disable VSTHRD100 // Avoid async void methods. Allow await in method body.
        /// <inheritdoc/>
        public async void RunStarted(
            object automationObject,
#pragma warning restore VSTHRD100 // Avoid async void methods
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                dte = automationObject as DTE;
                AttachCancelHandler();

                projectData = ProjectData.FromDictionary(replacementsDictionary);
                projectData.CustomParams = customParams.Select(cp => cp.ToString()).ToArray();

                oktaWizard.SetFinalizer(OktaApplicationType.None, new VisualStudioWizardRunFinisher(oktaWizard.UserManager));

                string resultYamlPath = Path.Combine(projectData.DestinationDirectory, $"{nameof(OktaWizardResult)}.yaml");

                // We're re-entering the wizard from the parent template
                if (File.Exists(resultYamlPath))
                {
                    OktaWizardResult result = Deserialize.FromYamlFile<OktaWizardResult>(resultYamlPath);
                    projectData.AddWizardResult(result);
                    projectData.UpdateReplacementsDicstionary(replacementsDictionary);
                    result.SetOktaReplacements(replacementsDictionary);
                    result.ProjectData = projectData;
                    oktaWizardResult = result;
                }
                else
                {
                    OktaWizardResult result = await oktaWizard.RunAsync(projectData);
                    if (result.Status != WizardStatus.Success)
                    {
                        throw result.Exception ?? new Exception("Okta Visual Studio Wizard experienced an unexpected error");
                    }

                    projectData.AddWizardResult(result);
                    projectData.UpdateReplacementsDicstionary(replacementsDictionary);

                    result.ProjectData = projectData;
                    result.Replacements = replacementsDictionary;
                    result.ShouldCreateUser = false;
                    result.ToYamlFile(resultYamlPath); // the serialized copy of the result is read by calls to dte.Solution.AddProjectFromTemplate
                    result.ShouldCreateUser = true;

                    oktaWizardResult = result;
                }
            }
            catch (WizardCancelledException wce)
            {
                throw wce;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}\r\n\r\n{ex.StackTrace}", "Okta Wizard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private readonly bool addFiles = true;

        /// <inheritdoc/>
        public bool ShouldAddProjectItem(string filePath)
        {
            return addFiles;
        }

        private void AttachCancelHandler()
        {
            void CancelHandler(object s, EventArgs a)
            {
                if (a is ApiCredentialsEventArgs apiCredArgs && apiCredArgs.Exception is WizardCancelledException wce1)
                {
                    throw wce1;
                }

                if (a is ApplicationCredentialsEventArgs appCredArgs && appCredArgs.Exception is WizardCancelledException wce2)
                {
                    throw wce2;
                }
            }

            oktaWizard.ApplicationCredentialsPromptExceptionOccurred += CancelHandler;
        }
    }
}
