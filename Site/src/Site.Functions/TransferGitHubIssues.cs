using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Site.Core.Apis.GitHub;
using Site.Core.Configuration;
using Site.Core.Queues;
using Site.Core.Queues.Messages;
using Site.Core.Services;

namespace Site.Functions
{
    public class TransferGitHubIssues
    {
        private readonly ISiteConfiguration _siteConfiguration;
        private readonly IQueue<IssueTransferMessage> _queue;
        private readonly IGitHubApi _gitHubApi;
        private readonly IGitHubAccountService _gitHubAccountService;


        public TransferGitHubIssues(
            ISiteConfiguration siteConfiguration, 
            IQueue<IssueTransferMessage> queue, 
            IGitHubApi gitHubApi, 
            IGitHubAccountService gitHubAccountService
            )
        {
            _siteConfiguration = siteConfiguration;
            _queue = queue;
            _gitHubApi = gitHubApi;
            _gitHubAccountService = gitHubAccountService;
        }
        
        [FunctionName("TransferGitHubIssues")]
        public async Task Run([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, ILogger logger)
        {
            foreach (var key in _siteConfiguration.GithubApiKeys)
            {
                var message = await _queue.Get();

                if (message is null)
                {
                    logger.LogInformation("No messages on the queue");
                    continue;
                }

                try
                {
                    var issue = await _gitHubApi.GetIssue(message.IssueNumber, _siteConfiguration.MasterRepository, key);
                    await _gitHubApi.CreateIssue(issue, message.TransferRepository, key);
                }
                catch (Exception e)
                {
                    logger.LogError($"Failed to process message with id: {message.MessageId} with error: {e}");
                    return;
                }
                
                logger.LogInformation($"GitHub issue number: {message.IssueNumber} transferred to repository {message.TransferRepository}");

                await _gitHubAccountService.IncrementIssueTransferCount(message.GitHubAccountId);

                await _queue.Remove(message);
            }
        }

    }
}
