using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Site.Core.Apis.GitHub;
using Site.Core.Configuration;
using Site.Core.DAL.Repositorys;
using Site.Core.Exceptions.GitHubAccount;

namespace Site.Core.Services
{
    public class GitHubAccountService : IGitHubAccountService
    {
        private readonly IGitHubAccountRepository _gitHubAccountRepository;
        private readonly IGitHubApi _gitHubApi;
        private readonly ISiteConfiguration _siteConfiguration;
        private readonly ILogger<GitHubAccountService> _logger;

        public GitHubAccountService(IGitHubAccountRepository gitHubAccountRepository, 
            IGitHubApi gitHubApi, 
            ISiteConfiguration siteConfiguration,
            ILogger<GitHubAccountService> logger)
        {
            _gitHubAccountRepository = gitHubAccountRepository;
            _gitHubApi = gitHubApi;
            _siteConfiguration = siteConfiguration;
            _logger = logger;
        }
        
        public async Task IncrementIssueTransferCount(int id)
        {
            var issues = await _gitHubApi.GetIssues(_siteConfiguration.MasterRepository,
                _siteConfiguration.GithubApiKeys.First());
            
            var account = await _gitHubAccountRepository.GetAsync(id);

            if (account is null)
                throw new GitHubAccountNotFoundException(id);

            if (account.IsIssueCopyComplete)
                throw new IssueCopyAlreadyCompleteException();

            account.IssuesCopied++;

            if (account.IssuesCopied >= issues.Count)
            {
                account.IsIssueCopyComplete = true;
                _logger.LogInformation($"Completing github account: {account.Repository} issue copy with a total copied of: {account.IssuesCopied}");
            }

            await _gitHubAccountRepository.UpdateAsync(account);
        }
    }
}