using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    /// <summary>
    /// An attribute used to addorn properties that represent replacement dictionary keys.
    /// </summary>
    public class ReplaceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceAttribute"/> class.
        /// </summary>
        public ReplaceAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public ReplaceAttribute(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }
    }
}
