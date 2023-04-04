using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class HttpClientWrapper : IHttpClient
    {
        public HttpClientWrapper(HttpClient client)
        {
            this.Wrapped = client;
        }

        protected HttpClient Wrapped { get; private set; }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return Wrapped.SendAsync(request);
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Wrapped.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return Wrapped.PostAsync(url, content);
        }
    }
}
