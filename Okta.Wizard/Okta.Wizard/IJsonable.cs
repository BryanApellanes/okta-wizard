using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public interface IJsonable
    {
        string ToJson(Formatting formatting = Formatting.Indented);        
    }
}
