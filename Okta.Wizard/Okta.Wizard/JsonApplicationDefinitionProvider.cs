using Newtonsoft.Json;
using Okta.Sdk.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class JsonApplicationDefinitionProvider : LogEventWriter, IApplicationDefinitionProvider
    {
        ApplicationDefinitionProvider defaultApplicationDefinitionProvider;
        public JsonApplicationDefinitionProvider(IJsonWebKeyProvider jsonWebKeyProvider, ILogger logger) : base(logger)
        {
            this.defaultApplicationDefinitionProvider = new ApplicationDefinitionProvider(jsonWebKeyProvider);
        }

        public async Task<OpenIdConnectApplication> GetApplicationDefinitionAsync(ApplicationDefinitionArguments arguments)
        {
            OpenIdConnectApplication result = await defaultApplicationDefinitionProvider.GetApplicationDefinitionAsync(arguments);

            string applicationJsonFilePath = arguments.GetApplicationJsonFilePath();
            if (File.Exists(applicationJsonFilePath))
            {
                string json = File.ReadAllText(applicationJsonFilePath);
                OpenIdConnectApplication fromJson = JsonConvert.DeserializeObject<OpenIdConnectApplication>(json);
                if(fromJson.Settings?.OauthClient != null)
                {
                    result.Settings.OauthClient = await UseSourceProperties(result.Settings.OauthClient, fromJson.Settings.OauthClient);
                }
            }
            return result;
        }

        protected async Task<T> UseSourceProperties<T>(T destination, T source)
        {
            Type destinationType = destination.GetType();
            Type sourceType = source.GetType();

            foreach (PropertyInfo destinationProp in destinationType.GetProperties())
            {
                PropertyInfo sourceProp = TryGetPropertyNamed(sourceType, destinationProp.Name);
                if (sourceProp != null)
                {
                    if (AreCompatibleProperties(destinationProp, sourceProp))
                    {
                        ParameterInfo[] indexParameters = sourceProp.GetIndexParameters();
                        if (indexParameters == null || indexParameters.Length == 0)
                        {
                            object value = sourceProp.GetValue(source, null);
                            if (value != null)
                            {
                                destinationProp.SetValue(destination, value, null);
                            }
                        }
                    }
                }
            }

            return destination;
        }

        private bool AreCompatibleProperties(PropertyInfo destinationProp, PropertyInfo sourceProp)
        {
            return (sourceProp.PropertyType == destinationProp.PropertyType ||
                sourceProp.PropertyType == Nullable.GetUnderlyingType(destinationProp.PropertyType) ||
                Nullable.GetUnderlyingType(sourceProp.PropertyType) == destinationProp.PropertyType)
                && destinationProp.CanWrite;
        }

        private PropertyInfo TryGetPropertyNamed(Type sourceType, string propertyName)
        {
            try
            {
                return sourceType.GetProperty(propertyName);

            }
            catch (AmbiguousMatchException ame)
            {
                return sourceType.GetProperties().FirstOrDefault(p => p.DeclaringType == sourceType && p.Name == propertyName);
            }
        }
    }
}
