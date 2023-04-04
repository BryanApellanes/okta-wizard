// <copyright file="ProjectFinalizationRepositoryDescriptor.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard
{
    /// <summary>
    /// Represents information about a repository used as a template.
    /// </summary>
    public class ProjectFinalizationRepositoryDescriptor // TODO: revisit this concept, review for potential removal.
    {
        /// <summary>
        /// Gets or sets the root git repository path.
        /// </summary>
        /// <value>
        /// The root git repository path.
        /// </value>
        public string RepositoryRoot { get; set; }

        /// <summary>
        /// Gets or sets the directory path within the repository that represents the
        /// project initialization template content.
        /// </summary>
        /// <value>
        /// The directory path within the repository that represents the
        /// project initialization template content.
        /// </value>
        public string RepositoryPath { get; set; }
    }
}
