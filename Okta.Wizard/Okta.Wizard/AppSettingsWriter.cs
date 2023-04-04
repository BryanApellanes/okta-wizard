using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class AppSettingsWriter : SettingsWriter
    {
        public const string APP_SETTINGS_FILE = "$ProjectDirectory$/appsettings.json";

        public override async Task<string[]> GetTargetFilesAsync(ProjectConfiguration projectConfiguration)
        {
            return new string[] { new FileInfo(APP_SETTINGS_FILE.Replace("$ProjectDirectory$", projectConfiguration.ProjectDirectory)).FullName };
        }
    }
}
