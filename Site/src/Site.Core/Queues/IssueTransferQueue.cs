using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Site.Core.Queues.Messages;

namespace Site.Core.Queues
{
    public class IssueTransferQueue : IQueue<IssueTransferMessage>
    {
        private readonly QueueClient _client;

        public IssueTransferQueue(IQueueFactory queueFactory)
        {
            _client = queueFactory.CreateIssuesQueueClient();
        }
        
        public async Task<IssueTransferMessage> Get()
        {
            var message = await _client.ReceiveMessageAsync();
            
            if (message is null)
                return null;

            var data = JsonSerializer.Deserialize<IssueTransferMessage>(message.Value.MessageText);

            data.MessageId = message.Value.MessageId;
            data.PopReceipt = message.Value.PopReceipt;
            
            return data;
        }

        public async Task Remove(IssueTransferMessage message) 
            => await _client.DeleteMessageAsync(message.MessageId, message.PopReceipt);

        public async Task Add(IssueTransferMessage message)
        {
            await _client.SendMessageAsync(JsonSerializer.Serialize(message));
        }
    }
}