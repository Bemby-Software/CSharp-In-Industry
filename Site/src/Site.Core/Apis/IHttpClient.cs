using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Site.Core.Apis
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);

        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

        HttpRequestHeaders DefaultRequestHeaders { get; }
        
    }
}