using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class OrganizationResponse : Jsonable
    {
        public string OrgUrl { get; set; }
        public string ApiToken { get; set; }

        [JsonProperty("developerOrgCliToken")]
        public string Identifier { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrgStatus OrgStatus { get; set; }

        public bool IsActive
        {
            get => OrgStatus == OrgStatus.Active;
        }

        public bool OperationSucceeded { get; set; }
    }
}
