using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Testing.Common.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return SendAsync(request);
        }

        public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            throw new Exception("Ensure this is mocked");
        }
        
    }
}