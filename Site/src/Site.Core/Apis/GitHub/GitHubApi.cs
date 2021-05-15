using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Site.Core.Apis.GitHub.DTO;

namespace Site.Core.Apis.GitHub
{
    public class GitHubApi : IGitHubApi
    {
        private readonly IHttpClient _httpClient;

        public GitHubApi(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            SetUserAgent();
        }

        public async Task<IssueDto> GetIssue(int issueNumber, string repository, string key)
        {
            SetToken(key);
            var response = await _httpClient.GetAsync($"/repos/{repository}/issues/{issueNumber}");
            response.EnsureSuccessStatusCode();
            return await Deserialize<IssueDto>(response.Content);
        }

        public Task CreateIssue(IssueDto issue, string repository, string key)
        {
            SetToken(key);
            throw new System.NotImplementedException();
        }
        
        public Task<List<IssueDto>> GetIssues(string repository, string key)
        {
            SetToken(key);
            throw new System.NotImplementedException();
        }

        private void SetUserAgent()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "comp-site-application");
        }

        private void SetToken(string key)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"token {key}");
        }

        private async Task<T> Deserialize<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
            });
        }
    }
}