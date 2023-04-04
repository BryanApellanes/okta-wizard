// <copyright file="ApiToken.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents an API token.
    /// </summary>
    public class ApiToken : SecureSerializable
    {
        /// <summary>
        /// Default file name to use when this token is saved.
        /// </summary>
        public const string DefaultFileName = "apitoken";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiToken"/> class.
        /// </summary>
        public ApiToken()
        {
            FileName = DefaultFileName;
        }

        [JsonIgnore]
        public string FileName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        [JsonIgnore]
        [YamlIgnore]
        public bool HasValue => !string.IsNullOrEmpty(Value);

        /// <summary>
        /// Serialize this API token to encrypted json in the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>FileInfo</returns>
        public override FileInfo ToJsonFile(string filePath)
        {
            return ToJsonFile(filePath, true);
        }

        /// <summary>
        /// Save this API token to encrypted json in the spcified location.
        /// </summary>
        /// <param name="directoryPath">The directory to save the file in.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>FileInfo</returns>
        public FileInfo Save(string directoryPath, string fileName = null)
        {
            string path = Path.Combine(directoryPath, fileName ?? FileName ?? DefaultFileName);
            return ToJsonFile(path);
        }

        /// <summary>
        /// Load the API token represented by the specified location.
        /// </summary>
        /// <param name="directoryPath">The directory to load from.</param>
        /// <param name="fileName">The file name to load from.</param>
        /// <returns>ApiToken</returns>
        public static ApiToken Load(string directoryPath, string fileName = null)
        {
            string path = Path.Combine(directoryPath, fileName ?? DefaultFileName);
            SecureSerializable secureSerializable = new SecureSerializable();
            return secureSerializable.FromEncryptedJsonFile<ApiToken>(path);
        }
    }
}
