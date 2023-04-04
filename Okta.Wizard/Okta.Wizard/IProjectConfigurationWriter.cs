using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IProjectConfigurationWriter
    {
        ISettingsWriter[] SettingsWriters { get; }
        Task AddSettingsWriterAsync(ISettingsWriter writer);
        Task AddSettingsWritersAsync(params ISettingsWriter[] writers);
        Task ClearSettingsWritersAsync();
        Task<ProjectConfigurationResult> ConfigureProjectAsync(ProjectConfiguration projectConfiguration);
    }
}
