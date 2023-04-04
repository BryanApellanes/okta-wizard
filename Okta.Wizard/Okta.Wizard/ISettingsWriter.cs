using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface ISettingsWriter
    {
        Task<string[]> GetTargetFilesAsync(ProjectConfiguration projectConfiguration);
        Task<SettingsWriterResult> WriteSettingsAsync(ProjectConfiguration projectConfiguration);
    }
}
