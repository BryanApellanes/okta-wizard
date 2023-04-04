// <copyright file="UserSignInCredentials.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx;
using DevEx.Internal;
using Microsoft.AspNetCore.DataProtection;
using Okta.Wizard.Internal;
using System.IO;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using System;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents user inputs for signing in to Okta.
    /// </summary>
    public class UserSignInCredentials : SecureSerializable
    {
        public const string DefaultFileName = "usic.ejson";
        public static readonly string DefaultFilePath = Path.Combine(OktaWizardConfig.OktaWizardHome, DefaultFileName);

        /// <summary>
        /// Gets or sets the user's sign in url, also known as their Okta domain.
        /// </summary>
        /// <value>
        /// The user's sign in url, also known as their Okta domain.
        /// </value>
        [EnvironmentVariable("OktaDomain")]
        public string SignInUrl{ get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        [EnvironmentVariable("OktaUserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [EnvironmentVariable("OktaPassword")]
        public string Password{ get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [YamlIgnore]
        public bool IsValid => !string.IsNullOrEmpty(SignInUrl) && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

        public override FileInfo ToJsonFile(string filePath)
        {
            return ToJsonFile(filePath, true);
        }

        public override FileInfo ToYamlFile(string filePath)
        {
            return ToYamlFile(filePath, true);
        }

        public static UserSignInCredentials FromEnvironmentVariables()
        {
            return Deserialize.FromEnvironmentVariables<UserSignInCredentials>();
        }

        public static UserSignInCredentials FromEncrypedJsonFile(string filePath)
        {
            string encrypted = File.ReadAllText(filePath);

            return Deserialize.FromJson<UserSignInCredentials>(GetDefaultDataProtector().Unprotect(encrypted));
        }

        public FileInfo Save(string filePath = null)
        {
            FileInfo file = new FileInfo(filePath ?? DefaultFilePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            return ToJsonFile(file.FullName);
        }

        /// <summary>
        /// Try to load user credentials from the specified encrypted file or UserSignInCredentials.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing user sign in credentials.</param>
        /// <returns>UserSignInCredentials or null</returns>
        public static UserSignInCredentials TryLoad(string filePath = null)
        {
            return TryLoad(out _, filePath);
        }

        /// <summary>
        /// Try to load user credentials from the specified encrypted file or UserSignInCredentials.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="exception">The exception that occurred if any, will be null on success.</param>
        /// <param name="filePath">The path to the encrypted file containing user sign in credentials.</param>
        /// <returns>UserSignInCredentials or null</returns>
        public static UserSignInCredentials TryLoad(out Exception exception, string filePath = null)
        {
            try
            {
                exception = null;
                return Load(filePath);
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        /// <summary>
        /// Load user credentials from the specified encrypted file or UserSignInCredentials.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing user sign in credentials.</param>
        /// <returns>UserSignInCredentials or null</returns>
        public static UserSignInCredentials Load(string filePath = null)
        {
            filePath = filePath ?? DefaultFilePath;
            SecureSerializable secureSerializable = new SecureSerializable();
            return secureSerializable.FromEncryptedJsonFile<UserSignInCredentials>(filePath);
        }
    }
}
