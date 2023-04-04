// <copyright file="UserProfile.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using DevEx.Extensions;
using DevEx.Internal;
using Newtonsoft.Json;
using Okta.Wizard.Internal;

namespace Okta.Wizard
{
    /// <summary>
    /// Represents a user profile.
    /// </summary>
    public class UserProfile : Serializable
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone.
        /// </summary>
        /// <value>
        /// The mobile phone.
        /// </value>
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is UserProfile userProfile))
            {
                return false;
            }

            return userProfile.FirstName.Equals(FirstName) && userProfile.LastName.Equals(LastName) && userProfile.Email.Equals(Email) && userProfile.Login.Equals(Login) && userProfile.MobilePhone.Equals(MobilePhone);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return $"{FirstName}{LastName}{Email}{Login}{MobilePhone}".GetHashCode();
        }

        /// <summary>
        /// Creates a valid password.
        /// </summary>
        /// <returns>string</returns>
        public static string CreateValidPassword()
        {
            string firstFour = 4.RandomCharacters("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            string nextFour = 4.RandomCharacters("abcdefghijklmnopqrstuvwxyz");
            string number = 1.RandomCharacters("1234567890");
            return $"{firstFour}{nextFour}{number}";
        }
    }
}
