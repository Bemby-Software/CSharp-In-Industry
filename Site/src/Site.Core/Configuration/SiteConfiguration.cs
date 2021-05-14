namespace Site.Core.Configuration
{
    public class SiteConfiguration : ISiteConfiguration
    {
        public string DbConnectionString { get; set; }
        
        public string[] GithubApiKeys { get; set; }
        public string StorageAccountConnectionString { get; set; }
        public string MasterRepository { get; set; }
    }
}