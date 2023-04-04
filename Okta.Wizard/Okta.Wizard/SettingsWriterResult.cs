using Newtonsoft.Json;
using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Okta.Wizard
{
    public class SettingsWriterResult
    {
        public SettingsWriterResult(Type type, string message = null)
        {
            this.SettingsWriterTypeName = type.FullName;
            this.Messages = new Dictionary<string, string>();
           // this.Message = message;
            this.OperationSucceeded = true;
        }

        public SettingsWriterResult(Type type, string message, params FileInfo[] files): this(type, message)
        {
            //this.Files = files.Select(fileInfo => fileInfo.FullName).ToArray();
        }

        public SettingsWriterResult(Type type, Exception ex)
        {
            this.SettingsWriterTypeName = type.FullName;
            this.Exception = ex;
            //this.Message = $"{ex.Message}\r\n{ex.StackTrace}";
            this.OperationSucceeded = false;
        }

        public Dictionary<string, string> Messages { get; }
/*        public string Message { get; set; }
        public string[] Files { get; set; }*/

        public bool OperationSucceeded { get; private set; }

        [JsonIgnore]
        public Exception Exception { get; private set; }

        public string SettingsWriterTypeName { get; private set; }
    }
}
