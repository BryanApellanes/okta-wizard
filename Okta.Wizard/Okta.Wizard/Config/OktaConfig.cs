using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace Okta.Wizard.Config
{
    /// <summary>
    /// Represents the "okta" node of the Sdk configuration.
    /// </summary>
    public class OktaConfig
    {
        /// <summary>
        /// Gets or sets the client config.
        /// </summary>
        [YamlMember(Alias = "client")]
        public ClientConfig Client { get; set; }
    }
}
