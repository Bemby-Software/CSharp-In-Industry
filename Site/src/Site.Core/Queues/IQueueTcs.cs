using System.Threading.Tasks;
using Site.Core.Queues.Messages;

namespace Site.Core.Queues
{
    public interface IQueue<T> where T : BaseQueueMessage
    {
        Task<T> Get();

        Task Remove(T message);

        Task Add(T message);
    }
}