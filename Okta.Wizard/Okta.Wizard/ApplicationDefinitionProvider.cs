using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class ApplicationDefinitionProvider : LogEventWriter, IApplicationDefinitionProvider
    {
        public ApplicationDefinitionProvider(IJsonWebKeyProvider jsonWebKeyProvider)
        {
            this.JsonWebKeyProvider = jsonWebKeyProvider;
        }

        protected IJsonWebKeyProvider JsonWebKeyProvider { get; set; }

        public async Task<OpenIdConnectApplication> GetApplicationDefinitionAsync(ApplicationDefinitionArguments applicationDefinitionArguments)
        {
            if (applicationDefinitionArguments == null)
            {
                throw new ArgumentNullException(nameof(applicationDefinitionArguments));
            }

            ProjectArguments projectArguments = applicationDefinitionArguments.ProjectArguments;
            if (projectArguments == null)
            {
                throw new ArgumentNullException($"{nameof(applicationDefinitionArguments.ProjectArguments)}");
            }

            string projectName = projectArguments.ProjectName;
            if (string.IsNullOrEmpty(projectName))
            {
                throw new ArgumentNullException("ProjectName");
            }

            string launchUrl = applicationDefinitionArguments.SslLocalHostUri.ToString();
            if (string.IsNullOrEmpty(launchUrl))
            {
                throw new ArgumentNullException($"{nameof(applicationDefinitionArguments.SslLocalHostUri)}");
            }

            OpenIdConnectApplication application = new OpenIdConnectApplication
            {
                Name = "oidc_client",
                SignOnMode = "OPENID_CONNECT",
                Label = projectName,
                Credentials = new OAuthApplicationCredentials()
                {
                    OauthClient = new ApplicationCredentialsOAuthClient()
                    {
                        ClientId = System.Guid.NewGuid().ToString(),
                        TokenEndpointAuthMethod = "client_secret_post",
                        AutoKeyRotation = true,
                    },
                },
                Settings = new OpenIdConnectApplicationSettings
                {
                    OauthClient = new OpenIdConnectApplicationSettingsClient()
                    {
                        ClientUri = $"{launchUrl}",
                        ResponseTypes = new List<OAuthResponseType>
                        {
                            "token",
                            "id_token",
                            "code",
                        },
                        RedirectUris = new List<string>
                        {
                            $"{launchUrl}/oauth2/callback",
                        },
                        PostLogoutRedirectUris = new List<string>
                        {
                            $"{launchUrl}/postlogout",
                        },
                        GrantTypes = new List<OAuthGrantType>
                        {
                            "implicit",
                            "authorization_code",
                        },
                        ApplicationType = "native",
                    },
                }
            };
            JsonWebKey jsonWebKey = JsonWebKeyProvider.CreateJsonWebKey(projectName);
            if (jsonWebKey != null)
            {
                application.Settings.OauthClient.Jwks = new OpenIdConnectApplicationSettingsClientKeys
                {
                    Keys = new List<JsonWebKey> { JsonWebKeyProvider.CreateJsonWebKey(projectName) }
                };
            }
            return application;
        }
    }
}
