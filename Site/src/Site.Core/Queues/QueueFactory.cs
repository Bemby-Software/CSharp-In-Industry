using Azure.Storage.Queues;
using Site.Core.Configuration;

namespace Site.Core.Queues
{
    public class QueueFactory : IQueueFactory
    {
        private readonly IEnvironment _environment;
        private readonly ISiteConfiguration _siteConfiguration;

        public QueueFactory(IEnvironment environment, ISiteConfiguration siteConfiguration)
        {
            _environment = environment;
            _siteConfiguration = siteConfiguration;
        }
        
        public QueueClient CreateIssuesQueueClient()
        {
            return new(_siteConfiguration.StorageAccountConnectionString, "issue-transfer-queue");
        }
    }
}