using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Site.Core.Apis.GitHub;
using Site.Core.Apis.GitHub.DTO;
using Site.Core.Configuration;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions;
using Site.Core.Exceptions.GitHubAccount;
using Site.Core.Exceptions.Teams;

namespace Site.Core.Services
{
    public class GitHubAccountService : IGitHubAccountService
    {
        private readonly IGitHubAccountRepository _gitHubAccountRepository;
        private readonly IGitHubApi _gitHubApi;
        private readonly ISiteConfiguration _siteConfiguration;
        private readonly ILogger<GitHubAccountService> _logger;
        private readonly ITeamService _teamService;

        public GitHubAccountService(IGitHubAccountRepository gitHubAccountRepository, 
            IGitHubApi gitHubApi, 
            ISiteConfiguration siteConfiguration,
            ILogger<GitHubAccountService> logger,
            ITeamService teamService)
        {
            _gitHubAccountRepository = gitHubAccountRepository;
            _gitHubApi = gitHubApi;
            _siteConfiguration = siteConfiguration;
            _logger = logger;
            _teamService = teamService;
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

        public async Task Assign(string repository, int teamId)
        {
            if (!await _teamService.IsValid(teamId))
                throw new TeamNotFoundException(teamId);

            await VerifyApiKeysAccess(repository);

            await _gitHubAccountRepository.CreateAsync(new() {Repository = repository, TeamId = teamId});
        }

        private async Task VerifyApiKeysAccess(string repository)
        {
            var i = 1;
            foreach (var key in _siteConfiguration.GithubApiKeys)
            {
                try
                {
                    await _gitHubApi.CreateIssue(new() {Title = $"Test Issue {i}", Body = "This is a test issue please feel free to delete."}, repository, key);
                }
                catch (Exception)
                {
                    throw new WriteAccessNeededException();
                }
                
                i++;
            }
        }
    }
}