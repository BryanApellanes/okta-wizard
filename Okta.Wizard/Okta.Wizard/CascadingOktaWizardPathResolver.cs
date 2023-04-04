using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Wizard
{
    public class CascadingOktaWizardPathResolver : OktaWizardPathResolver
    {
        List<IOktaWizardPathResolver> resolvers;
        public CascadingOktaWizardPathResolver(string projectDirectory, ILogger logger = null)
        {
            this.resolvers = new List<IOktaWizardPathResolver>();
            this.Logger = logger ?? new FileLogger(typeof(CascadingOktaWizardPathResolver));
            this.SetResolvers(projectDirectory);
        }

        public IOktaWizardPathResolver[] OktaWizardPathResolvers 
        {
            get
            {
                return resolvers.ToArray();
            }
        }

        public void SetResolvers(string projectDirectory)
        {
            AddResolver(new EnvironmentOktaWizardPathResolver());
            AddResolver(new FileOktaWizardPathResolver());
            AddResolver(new OktaFolderOktaWizardPathResolver(projectDirectory));
            AddResolver(new HomeOktaFolderOktaWizardPathResolver());
            AddResolver(new DefaultOktaWizardPathResolver());
        }

        public void AddResolver(IOktaWizardPathResolver oktaWizardPathResolver)
        {
            this.resolvers.Add(oktaWizardPathResolver);
        }

        public ILogger Logger { get; private set; }

        public override bool OktaWizardExists(out string[] checkedPaths)
        {
            return base.OktaWizardExists(out checkedPaths);
        }

        public override string ResolveOktaWizardPath(out string[] checkedPaths)
        {
            List<string> paths = new List<string>();
            string result = string.Empty;
            foreach(IOktaWizardPathResolver resolver in this.OktaWizardPathResolvers)
            {
                if (resolver.OktaWizardExists(out string[] notExistsPaths))
                {
                    result = resolver.ResolveOktaWizardPath(out string[] tryResolvePaths);
                    Logger.Info($"Using Okta Wizard executable found at: {result}");
                    break;
                }

                paths.AddRange(notExistsPaths);
            }

            checkedPaths = paths.ToArray();
            return result;
        }
    }
}
