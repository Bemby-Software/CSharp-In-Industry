using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        
        public async Task<List<IssueDto>> GetIssues(string repository, string key)
        {
            SetToken(key);
            var response = await _httpClient.GetAsync($"/repos/{repository}/issues");
            response.EnsureSuccessStatusCode();
            return await Deserialize<List<IssueDto>>(response.Content);
        }

        public async Task CreateIssue(IssueDto issue, string repository, string key)
        {
            SetToken(key);
            var response = await _httpClient.PostAsync($"/repos/{repository}/issues", new StringContent(Serialize(issue), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
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
        
        private string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, new()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
            });
        }
    }
}