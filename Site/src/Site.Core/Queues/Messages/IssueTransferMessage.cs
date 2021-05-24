namespace Site.Core.Queues.Messages
{
    public class IssueTransferMessage : BaseQueueMessage
    {
        public int IssueNumber { get; set; }

        public string TransferRepository { get; set; }

        public int GitHubAccountId { get; set; }
    }
}   