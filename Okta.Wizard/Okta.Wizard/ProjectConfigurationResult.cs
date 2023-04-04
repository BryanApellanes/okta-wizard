using Newtonsoft.Json;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class ProjectConfigurationResult
    {
        public ProjectConfigurationResult(ProjectConfiguration projectConfiguration, SettingsWriterResult[] settingsWriterResults)
        {
            this.OperationSucceeded = true;
            this.ProjectConfiguration = projectConfiguration;
            SettingsWriterResults = settingsWriterResults;
        }

        public ProjectConfigurationResult(Exception ex)
        {
            this.OperationSucceeded = false;
            this.Message = $"{ex.Message}\r\n{ex.StackTrace}";
            this.Exception = ex;
        }

        public string Message 
        {
            get; 
            set; 
        }

        public ProjectConfiguration ProjectConfiguration
        {
            get;
            set;
        }

        public SettingsWriterResult[] SettingsWriterResults { get; private set; }

        public bool OperationSucceeded { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
