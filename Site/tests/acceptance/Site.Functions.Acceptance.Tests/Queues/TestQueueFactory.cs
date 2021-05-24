using Azure.Storage.Queues;
using Site.Core.Queues;

namespace Site.Functions.Acceptance.Tests.Queues
{
    public class TestQueueFactory : IQueueFactory
    {
        public QueueClient CreateIssuesQueueClient()
        {
            return new QueueClient("UseDevelopmentStorage=true", "issue-transfer-queue-acceptance-testing");
        }
    }
}