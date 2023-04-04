using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace Okta.Wizard.Config
{
    /// <summary>
    /// Represents the "client" node of the Sdk configuration.
    /// </summary>
    public class ClientConfig
    {
        /// <summary>
        /// Gets or sets the Okta domain.
        /// </summary>
        [YamlMember(Alias = "oktaDomain")]
        public string OktaDomain { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [YamlMember(Alias = "token")]
        public string Token { get; set; }
    }
}
