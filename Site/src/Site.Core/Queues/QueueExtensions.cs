using Microsoft.Extensions.DependencyInjection;
using Site.Core.Queues.Messages;

namespace Site.Core.Queues
{
    public static class QueueExtensions
    {
        public static IServiceCollection AddQueueServices(this IServiceCollection services)
        {
            services.AddSingleton<IQueueFactory, QueueFactory>();
            services.AddScoped<IQueue<IssueTransferMessage>, IssueTransferQueue>();
            return services;
        }
    }
}