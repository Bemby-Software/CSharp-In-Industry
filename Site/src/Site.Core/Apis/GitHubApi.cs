using System.Net.Http;

namespace Site.Core.Apis
{
    public class GitHubApi : IGitHubApi
    {
        private readonly HttpClient _httpClient;

        public GitHubApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}