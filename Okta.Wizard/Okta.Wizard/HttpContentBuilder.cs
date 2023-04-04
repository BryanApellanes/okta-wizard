using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Okta.Wizard
{
    public class HttpContentBuilder : IHttpContentBuilder
    {
        public HttpContent GetHttpContent<T>(object content)
        {
            JsonContent jsonContent = JsonContent.Create(content, typeof(T));

            return jsonContent;
        }
    }
}
