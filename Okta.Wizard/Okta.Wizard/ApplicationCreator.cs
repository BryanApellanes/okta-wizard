using Newtonsoft.Json;
using Okta.Sdk.Api;
using Okta.Sdk.Client;
using Okta.Sdk.Model;
using Okta.Wizard.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class ApplicationCreator : LogEventWriter, IApplicationCreator
    {
        public ApplicationCreator(IApplicationRequestCreator applicationRequestCreator, IApplicationApi applicationApi)
        {
            this.ApplicationRequestCreator = applicationRequestCreator;
            this.ApplicationApi = applicationApi;
        }

        protected IApplicationRequestCreator ApplicationRequestCreator { get; private set; }

        protected IApplicationApi ApplicationApi { get; set; }

        public Task<CreateApplicationRequest> CreateApplicationRequestAsync(ApplicationDefinitionArguments arguments)
        {
            return ApplicationRequestCreator.CreateApplicationRequestAsync(arguments);
        }

        public async Task<CreateApplicationResult> CreateApplicationAsync(ApplicationDefinitionArguments arguments)
        {
            if (ApplicationApi == null)
            {
                throw new ArgumentNullException(nameof(ApplicationApi));
            }
            CreateApplicationRequest createApplicationRequest = await CreateApplicationRequestAsync(arguments);

            Info($"Creating application: {createApplicationRequest?.Application.ToJson()}");
            CreateApplicationResult createApplicationResult = new CreateApplicationResult(arguments);
            try
            {
                Application application = await ApplicationApi.CreateApplicationAsync(createApplicationRequest.Application);
                string json = application.ToJson();
                createApplicationResult.CreatedApplication = JsonConvert.DeserializeObject<OpenIdConnectApplication>(json);
                createApplicationResult.OperationSucceeded = true;
                Info($"Created application: {createApplicationResult.CreatedApplication.ToJson()}");
            }
            catch (Exception e)
            {
                Error($"Exception creating application: {e.Message}", e);
                createApplicationResult = new CreateApplicationResult(arguments, e);
            }
            return createApplicationResult;
        }
    }
}
