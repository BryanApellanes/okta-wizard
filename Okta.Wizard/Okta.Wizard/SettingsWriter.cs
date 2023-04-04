using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public abstract class SettingsWriter : ISettingsWriter
    {
        public abstract Task<string[]> GetTargetFilesAsync(ProjectConfiguration projectConfiguration);

        public virtual async Task<SettingsWriterResult> WriteSettingsAsync(ProjectConfiguration projectConfiguration)
        {
            try
            {
                SettingsWriterResult result = new SettingsWriterResult(this.GetType());
                string[] filePaths = await GetTargetFilesAsync(projectConfiguration);
                foreach (string filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        content = projectConfiguration.DoStringReplacements(content);
                        File.WriteAllText(filePath, content);
                        result.Messages.Add(filePath, "Wrote file");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return GetResult(ex);
            }
        }

        protected SettingsWriterResult GetResult(string message, params FileInfo[] files)
        {
            return new SettingsWriterResult(this.GetType(), message, files);
        }

        protected SettingsWriterResult GetResult(Exception ex)
        {
            return new SettingsWriterResult(this.GetType(), ex);
        }
    }
}
