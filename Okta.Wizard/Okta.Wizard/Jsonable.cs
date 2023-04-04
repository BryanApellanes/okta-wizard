using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public abstract class Jsonable : IJsonable
    {
        public string ToJson(Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(this, formatting);
        }

        public virtual void Save(string filePath = null)
        {
            string fullPath = HomePath.Resolve(filePath ?? OktaWizardSettings.REGISTRATION_TEMP_FILE_PATH);
            FileInfo fileInfo = new FileInfo(fullPath);
            if(!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            File.WriteAllText(fullPath, this.ToJson());
        }
    }
}
