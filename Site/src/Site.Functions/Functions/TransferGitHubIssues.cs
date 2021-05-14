using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Site.Core.Apis;
using Site.Core.Apis.GitHub;
using Site.Core.Configuration;
using Site.Core.Queues;
using Site.Core.Queues.Messages;

namespace Site.Functions
{
    public class TransferGitHubIssues
    {
        private readonly ISiteConfiguration _siteConfiguration;
        private readonly IQueue<IssueTransferMessage> _queue;
        private readonly IGitHubApi _gitHubApi;


        public TransferGitHubIssues(ISiteConfiguration siteConfiguration, IQueue<IssueTransferMessage> queue, IGitHubApi gitHubApi)
        {
            _siteConfiguration = siteConfiguration;
            _queue = queue;
            _gitHubApi = gitHubApi;
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

                await _queue.Remove(message);
            }
        }
        
    }
}
