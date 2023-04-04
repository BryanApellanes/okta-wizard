using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Okta.Wizard.VisualStudio
{
    /// <summary>
    /// Represents project arguments provided by a Visual Studio wizard run.
    /// </summary>
    public class ProjectArguments: Jsonable
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        [ProjectArgument("$time$")]        
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        [ProjectArgument("$year$")]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [ProjectArgument("$username$")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the userdomain.
        /// </summary>
        [ProjectArgument("$userdomain$")]
        public string UserDomain { get; set; }

        [ProjectArgument("$machinename$")]
        public string MachineName { get; set; }

        [ProjectArgument("$clrversion$")]
        public string ClrVersion { get; set; }

        [ProjectArgument("$registeredorganization$")]
        public string RegisteredOrganization { get; set; }

        [ProjectArgument("$runsilent$")]
        public string RunSilent { get; set; }

        [ProjectArgument("$solutiondirectory$")]
        public string SolutionDirectory { get; set; }

        [ProjectArgument("$projectname$")]
        public string ProjectName { get; set; }

        [ProjectArgument("$safeprojectname$")]
        public string SafeProjectName { get; set; }

        [ProjectArgument("$currentuiculturename$")]
        public string CurrentUiCultureName { get; set; }

        [ProjectArgument("$installpath$")]
        public string InstallPath { get; set; }

        [ProjectArgument("$specifiedsolutionname$")]
        public string SpecifiedSolutionName { get; set; }

        [ProjectArgument("$exclusiveproject$")]
        public string ExclusiveProject { get; set; }

        [ProjectArgument("$destinationdirectory$")]
        public string DestinationDirectory { get; set; }

        [ProjectArgument("$targetframeworkversion$")]
        public string TargetFrameworkVersion { get; set; }

        public string GetOktaWizardDirectoryPath()
        {
            return GetOktaWizardDirectoryPath(DestinationDirectory);
        }

        public override void Save(string filePath = null)
        {
            base.Save(filePath ?? GetJsonFilePath(DestinationDirectory));
        }

        public static ProjectArguments Read(string projectDirectory)
        {
            string jsonFilePath = GetJsonFilePath(projectDirectory);
            string json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<ProjectArguments>(json);
        }

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            PropertyInfo[] properties = typeof(ProjectArguments).GetProperties();
            foreach(PropertyInfo property in properties)
            {
                string value = property.GetValue(this) as string;
                ProjectArgumentAttribute projectParameter = property.GetCustomAttribute<ProjectArgumentAttribute>();
                if (projectParameter != null)
                {
                    result.Add(projectParameter.Name, value);
                }
                if (!result.ContainsKey($"${property.Name}$"))
                {
                    result.Add($"${property.Name}$", value);
                }
            }
            return result;
        }

        public static ProjectArguments FromDictionary(Dictionary<string, string> keyValuePairs)
        {
            ProjectArguments result = new ProjectArguments();
            PropertyInfo[] properties = typeof(ProjectArguments).GetProperties();
            foreach(PropertyInfo property in properties)
            {
                ProjectArgumentAttribute projectParameter =  property.GetCustomAttribute<ProjectArgumentAttribute>();
                if (keyValuePairs.ContainsKey(projectParameter.Name))
                {
                    property.SetValue(result, keyValuePairs[projectParameter.Name]);
                }
            }
            return result;
        }

        private static string GetOktaWizardDirectoryPath(string projectDirectory)
        {
            return Path.Combine(projectDirectory, ".okta", "wizard");
        }

        private static string GetJsonFilePath(string projectDirectory)
        {
            return Path.Combine(GetOktaWizardDirectoryPath(projectDirectory), $"{nameof(ProjectArguments)}.json");
        }
    }
}
