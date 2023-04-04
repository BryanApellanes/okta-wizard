using Ninject;
using Okta.Wizard.Config;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Okta.Wizard.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string ArgNamePrefix = "-";

        public App()
        {
            StartupUri = StartupAsync().Result;
        }

        public static void InitializeDi(StandardKernel kernel)
        {            
            kernel.Bind<IOktaWizardSettings>().To<OktaWizardSettings>();
            kernel.Bind<ILogger>().To<FileLogger>();
            kernel.Bind<IHttpClient>().To<HttpClientWrapper>();
            kernel.Bind<IHttpContentBuilder>().To<HttpContentBuilder>();
            kernel.Bind<IOrganizationCreator>().To<OrganizationCreator>();
            kernel.Bind<ISdkConfigurationWriter>().To<SdkConfigurationWriter>();
            kernel.Bind<IJsonWebKeyProvider>().To<JsonWebKeyProvider>();
            kernel.Bind<IApplicationDefinitionProvider>().To<JsonApplicationDefinitionProvider>();
            kernel.Bind<IApplicationRequestCreator>().To<ApplicationRequestCreator>();
            kernel.Bind<IProjectConfigurationWriter>().To<ProjectConfigurationWriter>();
            
            kernel.Bind<IOktaWizard>().To<OktaWizard>();        
        }

        public static AppArguments Arguments
        {
            get;
            private set;
        }

        public static ILogger Logger
        {
            get
            {
                return Di.Get<ILogger>(InitializeDi);
            }
        }

        public static ProjectArguments? ProjectArguments
        {
            get
            {
                try
                {
                    return ProjectArguments.Read(Arguments.ProjectDirectory);
                }
                catch (Exception ex)
                {
                    Logger.Error("Failed to get project arguments", ex);
                    return null;
                }
            }
        }


        public IOktaWizard OktaWizard
        {
            get => Di.Get<IOktaWizard>(InitializeDi);
        }

        protected async Task<Uri> StartupAsync()
        {
            Dictionary<string, string> namedArguments = GetNamedCommandLineArgs();
            Arguments = new AppArguments(namedArguments);
            
            
            if (Arguments.ResetOrg)
            {
                PendingRegistration.Delete();
                if (await OktaWizard.SdkConfigurationExistsAsync())
                {
                    await OktaWizard.DeleteConfigAsync();
                }
            }

            return new Uri("Windows/OktaWizardWindow.xaml", UriKind.Relative);
        }

        protected Dictionary<string, string> GetNamedCommandLineArgs()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string[] args = Environment.GetCommandLineArgs();
            for(int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith(ArgNamePrefix))
                {
                    if(i+1 < args.Length)
                    {
                        keyValuePairs[args[i]] = args[i + 1];
                    }
                    else
                    {
                        keyValuePairs[args[i]] = string.Empty;
                    }
                }
                else
                {
                    keyValuePairs[args[i]] = string.Empty;
                }
            }
            return keyValuePairs;
        }
    }
}
