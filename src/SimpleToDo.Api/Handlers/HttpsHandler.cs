using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApi.Api.Handlers
{
    public class HttpsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //bool webConfigRequireHttps = SettingsLoader.TryGetSetting<bool>("RequireHttps", true);

            //if (webConfigRequireHttps &&
            //    request.RequestUri.Scheme != Uri.UriSchemeHttps)
            //{
            //    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Forbidden);

            //    response.ReasonPhrase = "HTTPS required.";

            //    return Task.FromResult(response);
            //}

            return base.SendAsync(request, cancellationToken);
        }
    }
}