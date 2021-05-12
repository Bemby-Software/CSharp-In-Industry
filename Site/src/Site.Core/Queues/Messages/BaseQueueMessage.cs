using System.Text.Json.Serialization;

namespace Site.Core.Queues.Messages
{
    public class BaseQueueMessage
    {
        [JsonIgnore]
        public string MessageId { get; set; }

        [JsonIgnore]
        public string PopReceipt { get; set; }
    }
}