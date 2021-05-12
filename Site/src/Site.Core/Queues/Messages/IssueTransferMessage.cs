namespace Site.Core.Queues.Messages
{
    public class IssueTransferMessage : BaseQueueMessage
    {
        public int IssueId { get; set; }

        public int GitHubAccountId { get; set; }
    }
}