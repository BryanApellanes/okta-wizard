// <copyright file="OktaApiToken.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents an Okta API token.
    /// </summary>
    public class OktaApiToken : ApiToken
    {
        public new const string DefaultFileName = "oat.ejson";

        public static readonly string DefaultFilePath = Path.Combine(OktaWizardConfig.OktaWizardHome, DefaultFileName);

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaApiToken"/> class.
        /// </summary>
        /// <param name="name">The name to give to this instance.</param>
        public OktaApiToken(string name)
        {
            FileName = DefaultFileName;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the sign in url.
        /// </summary>
        public string SignInUrl { get; set; }

        /// <summary>
        /// Save this token as encrypted json to the specified file path or OktaApiToken.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The file path to save to</param>
        /// <returns>FileInfo</returns>
        public FileInfo Save(string filePath = null)
        {
            FileInfo file = new FileInfo(filePath ?? DefaultFilePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            return ToJsonFile(file.FullName);
        }

        public ApiCredentials ToApiCredentials()
        {
            return new ApiCredentials { Domain = SignInUrl, Token = Value };
        }

        /// <summary>
        /// Try to load Okta API token from the specified encrypted file or OktaApiToken.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing Okta API token sign in credentials.</param>
        /// <returns>OktaApiToken or null</returns>
        public static OktaApiToken TryLoad(string filePath = null)
        {
            return TryLoad(out _, filePath);
        }

        /// <summary>
        /// Try to load Okta API token from the specified encrypted file or OktaApiToken.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="exception">The exception that occurred if any, will be null on success.</param>
        /// <param name="filePath">The path to the encrypted file containing Okta API token sign in credentials.</param>
        /// <returns>OktaApiToken or null</returns>
        public static OktaApiToken TryLoad(out Exception exception, string filePath = null)
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
        /// Load Okta API token from the specified encrypted file or OktaApiToken.DefaultFilePath if no file path is specified.
        /// </summary>
        /// <param name="filePath">The path to the encrypted file containing Okta API token sign in credentials.</param>
        /// <returns>OktaApiToken or null</returns>
        public static OktaApiToken Load(string filePath = null)
        {
            filePath = filePath ?? DefaultFilePath;
            SecureSerializable secureSerializable = new SecureSerializable();
            return secureSerializable.FromEncryptedJsonFile<OktaApiToken>(filePath ?? DefaultFilePath);
        }
    }
}
