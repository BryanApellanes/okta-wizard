using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class LaunchSettingsWriter : SettingsWriter
    {
        public const string LAUNCH_SETTINGS_PATH = "$ProjectDirectory$/Properties/launchSettings.json";

        public override async Task<string[]> GetTargetFilesAsync(ProjectConfiguration projectConfiguration)
        {
            return new string[] { new FileInfo(LAUNCH_SETTINGS_PATH.Replace("$ProjectDirectory$", projectConfiguration.ProjectDirectory)).FullName };
        }
    }
}
