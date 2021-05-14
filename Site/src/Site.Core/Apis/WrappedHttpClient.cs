using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Site.Core.Apis
{
    public class WrappedHttpClient : IHttpClient
    {
        private readonly HttpClient _client;

        public WrappedHttpClient(Uri baseAddress)
        {
            _client = new HttpClient() {BaseAddress = baseAddress};
        }
        public Task<HttpResponseMessage> GetAsync(string uri) 
            => _client.GetAsync(uri);

        public HttpRequestHeaders DefaultRequestHeaders => _client.DefaultRequestHeaders;
    }
}