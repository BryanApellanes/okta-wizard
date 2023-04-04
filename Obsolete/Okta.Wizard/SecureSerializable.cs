// <copyright file="SecureSerializable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Internal;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Okta.Wizard.Internal;
using System;
using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents data that can serialize to encrypted text.
    /// </summary>
    public class SecureSerializable : Serializable
    {
        public SecureSerializable()
        {
            DataProtector = GetDefaultDataProtector();
        }

        [JsonIgnore]
        [YamlIgnore]
        public IDataProtector DataProtector { get; set; }

        /// <summary>
        /// Serialize this instance to json and encrypt the result.
        /// </summary>
        /// <returns>encrypted json</returns>
        public string ToEncryptedJson()
        {
            EnsureDataProtector();

            return DataProtector.Protect(ToJson());
        }

        /// <summary>
        /// Serialize this instance to yaml and encrypt the reuslt.
        /// </summary>
        /// <returns>Encrypted yaml.</returns>
        public string ToEncryptedYaml()
        {
            EnsureDataProtector();

            return DataProtector.Protect(ToYaml());
        }

        /// <summary>
        /// Deserialize the specified encrypted json as the specified generic type T.
        /// </summary>
        /// <typeparam name="T">The type of the return object.</typeparam>
        /// <param name="encryptedJson">Encrypted json</param>
        /// <returns>T</returns>
        public T ReadEncryptedJson<T>(string encryptedJson)
        {
            return Deserialize.FromJson<T>(DataProtector.Unprotect(encryptedJson));
        }

        /// <summary>
        /// Deserialize the specified encrypted yaml as the specified generic type T.
        /// </summary>
        /// <typeparam name="T">The type of the return object.</typeparam>
        /// <param name="encryptedYaml">Encrypted yaml</param>
        /// <returns>T</returns>
        public T ReadEncryptedYaml<T>(string encryptedYaml)
        {
            return Deserialize.FromYaml<T>(DataProtector.Unprotect(encryptedYaml));
        }

        /// <summary>
        /// Serialize to yaml in the specified file path.  Will not encrypt.
        /// </summary>
        /// <param name="filePath">The file path/</param>
        /// <returns>FileInfo</returns>
        public override FileInfo ToYamlFile(string filePath)
        {
            return ToYamlFile(filePath, false);
        }

        /// <summary>
        /// Serialize to yaml in the specified path, optionally encrypting.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="secure">A value indicating whether to encrypt the yaml.</param>
        /// <returns>FileInfo</returns>
        public FileInfo ToYamlFile(string filePath, bool secure)
        {
            if (!secure)
            {
                return base.ToYamlFile(filePath);
            }

            return ToEncryptedYamlFile(filePath);
        }

        /// <summary>
        /// Deserialize from json in the specified path.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="filePath">The file path.</param>
        /// <returns>T</returns>
        public T FromEncryptedJsonFile<T>(string filePath)
        {
            string json = DataProtector.Unprotect(File.ReadAllText(filePath));
            return Deserialize.FromJson<T>(json);
        }

        /// <summary>
        /// Serialize to json in the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>FileInfo</returns>
        public override FileInfo ToJsonFile(string filePath)
        {
            return ToJsonFile(filePath, false);
        }

        /// <summary>
        /// Serialize to json in the specified file path, optionally encrypting.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="secure">A value indicating whether to encrypt the output.</param>
        /// <returns>FileInfo</returns>
        public FileInfo ToJsonFile(string filePath, bool secure)
        {
            if (!secure)
            {
                return base.ToJsonFile(filePath);
            }

            return ToEncryptedJsonFile(filePath);
        }

        protected FileInfo ToEncryptedYamlFile(string filePath)
        {
            File.WriteAllText(filePath, ToEncryptedYaml());
            return new FileInfo(filePath);
        }

        protected FileInfo ToEncryptedJsonFile(string filePath)
        {
            File.WriteAllText(filePath, ToEncryptedJson());
            return new FileInfo(filePath);
        }

        /// <summary>
        /// Gets the default data protection provider.
        /// </summary>
        /// <returns>IDataProtectionProvider</returns>
        public static IDataProtectionProvider GetDefaultDataProtectionProvider()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IDataProtectionProvider dataProtectionProvider = serviceProvider.GetDataProtectionProvider();
            return dataProtectionProvider;
        }

        /// <summary>
        /// Gets the default data protector.
        /// </summary>
        /// <param name="dataProtectionProvider">The provider used to get the protector.</param>
        /// <returns>IDataProtector</returns>
        public static IDataProtector GetDefaultDataProtector(IDataProtectionProvider dataProtectionProvider = null)
        {
            return (dataProtectionProvider ?? GetDefaultDataProtectionProvider()).CreateProtector(OktaWizardUserAgent.Value);
        }

        private void EnsureDataProtector()
        {
            if (DataProtector == null)
            {
                throw new InvalidOperationException("DataProtector not specified");
            }
        }
    }
}
