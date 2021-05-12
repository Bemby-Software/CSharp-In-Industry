using System;

namespace Site.Core.Entities
{
    public class GitHubAccount
    {
        public int Id { get; set; }

        public string Repository { get; set; }

        public int IssuesCopied { get; set; }
        
        public bool IsIssueCopyComplete { get; set; }

        public DateTime LinkedAt { get; set; }
    }
}