using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard.VisualStudio
{
    /// <summary>
    /// Represents a project arguments.
    /// </summary>
    public class ProjectArgumentAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance of ProjectArgumentAttribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public ProjectArgumentAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
