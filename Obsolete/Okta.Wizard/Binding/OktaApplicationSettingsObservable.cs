// <copyright file="OktaApplicationSettingsObservable.cs" company="Okta, Inc">
// Copyright (c) 2020 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

namespace Okta.Wizard.Binding
{
    /// <summary>
    /// A data binding class that represents application settings.
    /// </summary>
    public class OktaApplicationSettingsObservable : Observable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OktaApplicationSettingsObservable"/> class.
        /// </summary>
        public OktaApplicationSettingsObservable()
        {
            this.UserSignInCredentials = new UserSignInCredentials();
        }

        private bool? showUserSignInCredentialsControl;

        /// <summary>
        /// Gets or sets a value indicating whether to show the user sign in credentials control.
        /// </summary>
        public bool ShowUserSignInCredentialsControl 
        {
            get
            {
                if (showUserSignInCredentialsControl != null)
                {
                    return (bool)showUserSignInCredentialsControl;
                }

                return string.IsNullOrEmpty(SignInUrl) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password);
            }

            set
            {
                showUserSignInCredentialsControl = value;
            }
        }

        /// <summary>
        /// Gets or sets the ApiToken.
        /// </summary>
        /// <value>
        /// The ApiToken.
        /// </value>
        public string ApiToken
        {
            get
            {
                return GetProperty<string>(nameof(ApiToken));
            }

            set
            {
                SetProperty(nameof(ApiToken), value);
            }
        }

        /// <summary>
        /// Gets or sets the users sign in credentials.
        /// </summary>
        /// <value>
        /// The users sign in credentials.
        /// </value>
        public UserSignInCredentials UserSignInCredentials { get; set; }

        /// <summary>
        /// Gets or sets the sign in url.
        /// </summary>
        /// <value>
        /// The sign in url.
        /// </value>
        public string SignInUrl
        {
            get
            {
                return UserSignInCredentials.SignInUrl;
            }

            set
            {
                UserSignInCredentials.SignInUrl = value;
            }
        }

        public string UserName
        {
            get
            {
                return UserSignInCredentials.UserName;
            }

            set
            {
                UserSignInCredentials.UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return UserSignInCredentials.Password;
            }

            set
            {
                UserSignInCredentials.Password = value;
            }
        }

        /// <summary>
        /// Gets or sets the application type.
        /// </summary>
        /// <value>
        /// The application type.
        /// </value>
        public OktaApplicationType OktaApplicationType
        {
            get
            {
                return GetProperty<OktaApplicationType>(nameof(OktaApplicationType));
            }

            set
            {
                SetProperty(nameof(OktaApplicationType), value);
            }
        }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <value>
        /// The application name.
        /// </value>
        public string ApplicationName
        {
            get
            {
                return GetProperty<string>(nameof(ApplicationName));
            }

            set
            {
                SetProperty(nameof(ApplicationName), value);
            }
        }

        /// <summary>
        /// Gets or Sets the name of the Visual Studio template.
        /// </summary>
        /// <value>
        /// The name of the Visual Studio template.
        /// </value>
        public string VsTemplateName
        {
            get
            {
                return GetProperty<string>(nameof(VsTemplateName));
            }

            set
            {
                SetProperty(nameof(VsTemplateName), value);
            }
        }

        /// <summary>
        /// Gets API credentials.
        /// </summary>
        /// <returns>ApiCredentials</returns>
        public ApiCredentials ToApiCredentials()
        {
            return ToOktaApplicationSettings().ApiCredentials;
        }

        /// <summary>
        /// Copy the OktaDomain and ApiToken to a new instance of ApiCredentials.
        /// </summary>
        /// <returns>OktaApplicationSettings</returns>
        public OktaApplicationSettings ToOktaApplicationSettings()
        {
            return new OktaApplicationSettings
            {
                OktaApplicationType = OktaApplicationType,
                ApiCredentials = new ApiCredentials
                {
                    Domain = SignInUrl,
                    Token = ApiToken,
                },
            };
        }

        public UserSignInCredentials ToUserSignInCredentials()
        {
            return new UserSignInCredentials
            {
                SignInUrl = SignInUrl,
                UserName = UserName,
                Password = Password,
            };
        }
    }
}
