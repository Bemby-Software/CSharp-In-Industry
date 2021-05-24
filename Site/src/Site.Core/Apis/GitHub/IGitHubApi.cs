using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Services;

namespace Site.Core.Apis.GitHub
{
    public interface IGitHubApi
    {
        Task<IssueDto> GetIssue(int issueNumber, string repository, string key);
        Task CreateIssue(IssueDto issue, string repository, string key);

        Task<List<IssueDto>> GetIssues(string repository, string key);
    }
}   