using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class ProjectConfigurationWriter : IProjectConfigurationWriter
    {
        private List<ISettingsWriter> settingsWriters = new List<ISettingsWriter>();
        public ProjectConfigurationWriter() 
        {
            this.AddSettingsWriterAsync(new LaunchSettingsWriter()).Wait();
            this.AddSettingsWriterAsync(new AppSettingsWriter()).Wait();
        }

        public ISettingsWriter[] SettingsWriters
        {
            get
            {
                return settingsWriters.ToArray();
            }
        }

        public async Task AddSettingsWriterAsync(ISettingsWriter writer)
        {
            this.settingsWriters.Add(writer);
        }

        public async Task AddSettingsWritersAsync(params ISettingsWriter[] writers)
        {
            this.settingsWriters.AddRange(writers);
        }

        public async Task ClearSettingsWritersAsync()
        {
            this.settingsWriters.Clear();
        }

        public async Task<ProjectConfigurationResult> ConfigureProjectAsync(ProjectConfiguration projectConfiguration)
        {
            List<SettingsWriterResult> writeResults = new List<SettingsWriterResult>();
            foreach(ISettingsWriter settingsWriter in this.settingsWriters)
            {
                writeResults.Add(await settingsWriter.WriteSettingsAsync(projectConfiguration));
            }
            return new ProjectConfigurationResult(projectConfiguration, writeResults.ToArray());
        }
    }
}
