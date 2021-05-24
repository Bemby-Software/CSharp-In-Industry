namespace Site.Core.Configuration
{
    public interface ISiteConfiguration
    {
        public string DbConnectionString { get; }
        public string[] GithubApiKeys { get; set; }
        string StorageAccountConnectionString { get; set; }
        public string MasterRepository { get; set; }

        public string GitHubApiUrl { get; set; }

        public string IssueTransferQueueName { get; set; } 
    }
}