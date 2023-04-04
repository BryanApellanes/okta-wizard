using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class OrganizationCreator : LogEventWriter, IOrganizationCreator
    {
        public OrganizationCreator(IHttpClient httpClient, IHttpContentBuilder httpContentBuilder = null, ILogger logger = null) 
        {
            this.HttpClient = httpClient;
            this.HttpContentBuilder = httpContentBuilder;
        }

        protected IHttpClient HttpClient { get; set; }
        protected IHttpContentBuilder HttpContentBuilder { get; set; }

        public async Task<OrganizationResponse> CreateNewOrganizationAsync(OrganizationRequest createOrganizationRequest)
        {
            HttpRequestMessage requestMessage = CreateOrganizationRequestMessage(createOrganizationRequest);

            HttpResponseMessage response = await HttpClient.SendAsync(requestMessage);
            if (response != null && response.IsSuccessStatusCode && response.Content != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                OrganizationResponse orgResponse =  JsonConvert.DeserializeObject<OrganizationResponse>(json);
                orgResponse.OperationSucceeded = true;
                return orgResponse;
            }
            if(response != null && !response.IsSuccessStatusCode && response.Content != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrganizationErrorResponse>(json);
            }
            return new OrganizationResponse();
        }

        public async Task<OrganizationResponse> VerifyNewOrganizationAsync(string identifier)
        {
            string url = $"{OktaWizardSettings.DEFAULT_REGISTRATION_BASE_URL}/api/internal/v1/developer/redeem/{identifier}";

            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if(response != null && response.IsSuccessStatusCode && response.Content != null)
            {
                string json = await response.Content.ReadAsStringAsync() ;
                return JsonConvert.DeserializeObject<OrganizationResponse>(json);
            }

            return new OrganizationResponse();
        }

        private HttpRequestMessage CreateOrganizationRequestMessage(OrganizationRequest createOrganizationRequest)
        {
            string url = $"{OktaWizardSettings.DEFAULT_REGISTRATION_BASE_URL}/api/v1/registration/{OktaWizardSettings.DEFAULT_REGISTRATION_ID}/register";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = HttpContentBuilder.GetHttpContent<OrganizationRequestContent>(new OrganizationRequestContent()
            {
                UserProfile = createOrganizationRequest
            });
            request.Headers.Add("User-Agent", "OktaWizard/1.0");

            return request;
        }
    }
}
