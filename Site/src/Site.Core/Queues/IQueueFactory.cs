using Azure.Storage.Queues;

namespace Site.Core.Queues
{
    public interface IQueueFactory
    {
        QueueClient CreateIssuesQueueClient();
    }
}