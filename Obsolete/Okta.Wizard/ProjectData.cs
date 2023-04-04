// <copyright file="ProjectData.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a strongly typed container for data used by Visual Studio in
    /// project initialization.
    /// </summary>
    public class ProjectData : Serializable
    {
        private readonly Dictionary<string, string> addedReplacements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectData"/> class.
        /// </summary>
        public ProjectData()
        {
            addedReplacements = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [Replace("$time$")]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [Replace("$year$")]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Replace("$username$")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user domain.
        /// </summary>
        /// <value>
        /// The user domain.
        /// </value>
        [Replace("$userdomain$")]
        public string UserDomain { get; set; }

        /// <summary>
        /// Gets or sets the machine name.
        /// </summary>
        /// <value>
        /// The machine name.
        /// </value>
        [Replace("$machinename$")]
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the CLR version.
        /// </summary>
        /// <value>
        /// The CLR version.
        /// </value>
        [Replace("$clrversion$")]
        public string ClrVersion { get; set; }

        /// <summary>
        /// Gets or sets the registered organization.
        /// </summary>
        /// <value>
        /// The registered organization.
        /// </value>
        [Replace("$registeredorganization$")]
        public string RegisteredOrganization { get; set; }

        /// <summary>
        /// Gets or sets runsilent.
        /// </summary>
        /// <value>
        /// Runsilent.
        /// </value>
        [Replace("$runsilent$")]
        public string RunSilent { get; set; }

        /// <summary>
        /// Gets or sets the solution directory.
        /// </summary>
        /// <value>
        /// The solution directory.
        /// </value>
        [Replace("$solutiondirectory$")]
        public string SolutionDirectory { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        /// <value>
        /// The project name.
        /// </value>
        [Replace("$projectname$")]
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the safe project name.
        /// </summary>
        /// <value>
        /// The safe project name.
        /// </value>
        [Replace("$safeprojectname$")]
        public string SafeProjectName { get; set; }

        /// <summary>
        /// Gets or sets the current UI culture name.
        /// </summary>
        /// <value>
        /// The current UI culture name.
        /// </value>
        [Replace("$currentuiculturename$")]
        public string CurrentUiCultureName { get; set; }

        /// <summary>
        /// Gets or sets the install path.
        /// </summary>
        /// <value>
        /// The install path.
        /// </value>
        [Replace("$installpath$")]
        public string InstallPath { get; set; }

        /// <summary>
        /// Gets or sets the specified solution name.
        /// </summary>
        /// <value>
        /// The specified solution name.
        /// </value>
        [Replace("$specifiedsolutionname$")]
        public string SpecifiedSolutionName { get; set; }

        /// <summary>
        /// Gets or sets the exclusive project.
        /// </summary>
        /// <value>
        /// The exclusive project.
        /// </value>
        [Replace("$exclusiveproject$")]
        public string ExclusiveProject { get; set; }

        /// <summary>
        /// Gets or sets the destination directory.
        /// </summary>
        /// <value>
        /// The destination directory.
        /// </value>
        [Replace("$destinationdirectory$")]
        public string DestinationDirectory { get; set; }

        /// <summary>
        /// Gets or sets the target framework version.
        /// </summary>
        /// <value>
        /// The target framework version.
        /// </value>
        [Replace("$targetframeworkversion$")]
        public string TargetFrameworkVersion { get; set; }

        /// <summary>
        /// Gets or sets the custom parameters passed to the wizard by Visual Studio.  Typically
        /// contains a single string whose value is the full path to the .vstemplate file in the
        /// Visual Studio extensions folder.
        /// </summary>
        /// <value>
        /// The custom parameters passed to the wizard by Visual Studio.  Typically
        /// contains a single string whose value is the full path to the .vstemplate file in the
        /// Visual Studio extensions folder.
        /// </value>
        [JsonIgnore]
        [YamlIgnore]
        public object[] CustomParams { get; set; }

        /// <summary>
        /// Gets the selected template name.
        /// </summary>
        /// <returns>string</returns>
        public string GetSelectedVsTemplateName()
        {
            if (CustomParams?.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(CustomParams[0].ToString());
                return fileInfo.Directory.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the okta-apikey.exe file.
        /// </summary>
        /// <returns>FileInfo</returns>
        public FileInfo GetApiKeyToolFile()
        {
            string path = Path.Combine(GetToolsDirectory().FullName, "okta-apikey.exe");
            return new FileInfo(path);
        }

        /// <summary>
        /// Gets the tools/net5.0 directory in the project templates directory.
        /// </summary>
        /// <returns>DirectoryInfo</returns>
        public DirectoryInfo GetToolsDirectory()
        {
            return new DirectoryInfo(Path.Combine(GetProjectTemplatesDirectory().FullName, OktaTemplateNames.OktaApplicationWizard.ToString(), "tools", "net5.0"));
        }

        /// <summary>
        /// Gets the project template directory.
        /// </summary>
        /// <returns>DirectoryInfo</returns>
        public DirectoryInfo GetProjectTemplatesDirectory()
        {
            if (CustomParams?.Length > 0)
            {
                FileInfo fileInfo = new FileInfo(CustomParams[0].ToString());
                DirectoryInfo directoryInfo = fileInfo.Directory;
                while (!directoryInfo.Name.Equals("ProjectTemplates"))
                {
                    if (directoryInfo == null)
                    {
                        break;
                    }

                    directoryInfo = directoryInfo.Parent;
                }

                return directoryInfo;
            }

            return null;
        }

        /// <summary>
        /// Gets the template file for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">Okta application type.</param>
        /// <returns>FileInfo</returns>
        public FileInfo GetTemplateFile(OktaApplicationType oktaApplicationType)
        {
            string templateName = GetTemplateName(oktaApplicationType);
            return GetTemplateFile(templateName);
        }

        /// <summary>
        /// Gets the specified template.
        /// </summary>
        /// <param name="templateName">The name of the template.</param>
        /// <returns>FileInfo</returns>
        public FileInfo GetTemplateFile(string templateName)
        {
            DirectoryInfo templateDirectory = GetTemplateDirectory(templateName);
            return templateDirectory.GetFiles("*.vstemplate").FirstOrDefault();
        }

        /// <summary>
        /// Gets the template directory.
        /// </summary>
        /// <returns>DirectoryInfo</returns>
        public DirectoryInfo GetTemplateDirectory()
        {
            return GetTemplateDirectory(GetSelectedVsTemplateName());
        }

        /// <summary>
        /// Gets the template directory for the specified template.
        /// </summary>
        /// <param name="templateName">The name of the template</param>
        /// <returns>DirectoryInfo</returns>
        public DirectoryInfo GetTemplateDirectory(string templateName)
        {
            return GetTemplateDirectories()[templateName];
        }

        /// <summary>
        /// Gets template directories.
        /// </summary>
        /// <returns>Dictionary{string, DirectoryInfo}</returns>
        public Dictionary<string, DirectoryInfo> GetTemplateDirectories()
        {
            Dictionary<string, DirectoryInfo> result = new Dictionary<string, DirectoryInfo>();
            DirectoryInfo projectTemplateDirectory = GetProjectTemplatesDirectory();
            if (projectTemplateDirectory != null)
            {
                foreach (DirectoryInfo templateDirectory in projectTemplateDirectory.GetDirectories())
                {
                    result.Add(templateDirectory.Name, templateDirectory);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the template for the specified Okta application type.
        /// </summary>
        /// <param name="oktaApplicationType">The Okta application type.</param>
        /// <returns>string</returns>
        public static string GetTemplateName(OktaApplicationType oktaApplicationType)
        {
            switch (oktaApplicationType)
            {
                case OktaApplicationType.None:
                    return "OktaApplicationWizard";
                case OktaApplicationType.Native:
                    return "OktaXamarin";
                case OktaApplicationType.SinglePageApplication:
                    return "OktaBlazorWebAssembly";
                case OktaApplicationType.Web:
                    return "OktaAspNetCoreMvc";
                case OktaApplicationType.Service:
                    return "OktaAspNetCoreWebApi";
                case OktaApplicationType.Repository:
                default:
                    return "OktaApplicationWizard";
            }
        }

        /// <summary>
        /// Gets the Okta application type.
        /// </summary>
        /// <returns>OktaApplicationType</returns>
        public virtual OktaApplicationType GetOktaApplicationType()
        {
            string visualStudioTemplate = GetSelectedVsTemplateName();
            OktaApplicationType result = OktaApplicationType.None;
            if (!string.IsNullOrEmpty(visualStudioTemplate))
            {
                switch (visualStudioTemplate)
                {
                    case "OktaApplicationWizard":
                        result = OktaApplicationType.None;
                        break;
                    case "OktaXamarin":
                        result = OktaApplicationType.Native;
                        break;
                    case "OktaBlazorWebAssembly":
                        result = OktaApplicationType.SinglePageApplication;
                        break;
                    case "OktaAspNetCoreMvc":
                        result = OktaApplicationType.Web;
                        break;
                    case "OktaAspNetCoreWebApi":
                        result = OktaApplicationType.Service;
                        break;
                    default:
                        result = OktaApplicationType.None;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Add data from the specified wizard result to this instance.
        /// </summary>
        /// <param name="oktaWizardResult">The Okta wizard result.</param>
        public void AddWizardResult(OktaWizardResult oktaWizardResult)
        {
            SafeProjectName = oktaWizardResult.ApplicationName.Replace(" ", ".").Replace("-", ".").Replace("\\", ".").Replace("/", ".");
            OktaWizardConfig oktaWizardConfig = oktaWizardResult.OktaWizardConfig;
            ApplicationCredentials applicationCredentials = oktaWizardResult.ApplicationCredentials;
            Uri oktaUri = oktaWizardConfig.GetDomainUri();
            string oktaDomain = oktaUri.Authority;
            SetReplacement("{yourOktaDomain}", oktaDomain);
            SetReplacement("{ClientId}", applicationCredentials.ClientId);
            SetReplacement("{ClientSecret}", applicationCredentials.ClientSecret);
            SetReplacement("{ApplicationName}", applicationCredentials.ApplicationName);
        }

        /// <summary>
        /// Add a replacement for the specified key and value pair.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void SetReplacement(string key, string value)
        {
            if (addedReplacements.ContainsKey(key))
            {
                addedReplacements[key] = value;
            }
            else
            {
                addedReplacements.Add(key, value);
            }
        }

        /// <summary>
        /// Update the specified dictionary with replacement values from this instance.
        /// </summary>
        /// <param name="replacementsDictionary">The replacements dictionary.</param>
        public void UpdateReplacementsDicstionary(Dictionary<string, string> replacementsDictionary)
        {
            Type type = typeof(ProjectData);
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                object attribute = property.GetCustomAttributes(typeof(ReplaceAttribute), true).FirstOrDefault();
                if (attribute is ReplaceAttribute replaceAttribute)
                {
                    if (replacementsDictionary.ContainsKey(replaceAttribute.Key))
                    {
                        replacementsDictionary[replaceAttribute.Key] = property.GetValue(this) as string;
                    }
                }
            }

            foreach (string key in addedReplacements.Keys)
            {
                if (replacementsDictionary.ContainsKey(key))
                {
                    replacementsDictionary[key] = addedReplacements[key];
                }
                else
                {
                    replacementsDictionary.Add(key, addedReplacements[key]);
                }
            }
        }

        /// <summary>
        /// Create a project data instance from the specified dictionary.
        /// </summary>
        /// <param name="replacementsDictionary">The dictionary.</param>
        /// <returns>ProjectData</returns>
        public static ProjectData FromDictionary(Dictionary<string, string> replacementsDictionary)
        {
            ProjectData result = new ProjectData();
            Type type = typeof(ProjectData);
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                object attribute = property.GetCustomAttributes(typeof(ReplaceAttribute), true).FirstOrDefault();
                if (attribute is ReplaceAttribute replaceAttribute)
                {
                    if (replacementsDictionary.ContainsKey(replaceAttribute.Key))
                    {
                        property.SetValue(result, replacementsDictionary[replaceAttribute.Key]);
                    }
                }
            }

            return result;
        }
    }
}
