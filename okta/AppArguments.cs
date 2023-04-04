using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard.Wpf
{
    public class AppArguments
    {
        public AppArguments(Dictionary<string, string> namedArguments)
        {
            this.NamedArguments = namedArguments;
        }

        public bool ResetOrg
        {
            get
            {
                return NamedArguments.ContainsKey("-resetOrg");
            }
        }

        public string ProjectDirectory
        {
            get
            {
                return NamedArguments.ContainsKey("-projectDirectory") ? new FileInfo(HomePath.Resolve(NamedArguments["-projectDirectory"])).FullName : ".";
            }
        }

        public string ApplicationJsonPath
        {
            get
            {
                if (NamedArguments.ContainsKey("-appJson"))
                {
                    return NamedArguments["-appJson"];
                }
                if (NamedArguments.ContainsKey("-applicationJson"))
                {
                    return NamedArguments["-applicationJson"];
                }
                return new FileInfo(Path.Combine(ProjectDirectory, ".okta", "wizard", "application.json")).FullName;
            }
        }

        public bool EnableDiagnostics
        {
            get
            {
                return NamedArguments.ContainsKey("-diag") || NamedArguments.ContainsKey("-diagnostics");
            }
        }

        private Dictionary<string, string> NamedArguments { get; set; }
    }
}
