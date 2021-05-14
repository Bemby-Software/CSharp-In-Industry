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
        }

        public async Task<IssueDto> GetIssue(int issueNumber, string repository, string key)
        {
            var response = await _httpClient.GetAsync($"/{repository}/issues/{issueNumber}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<IssueDto>(await response.Content.ReadAsStringAsync());
        }

        public Task CreateIssue(IssueDto issue, string repository, string key)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<List<IssueDto>> GetIssues(string repository, string key)
        {
            throw new System.NotImplementedException();
        }
    }
}