namespace Site.Core.Exceptions.GitHubAccount
{
    public class GitHubAccountNotFoundException : ExceptionBase
    {
        private readonly int _id;

        public GitHubAccountNotFoundException(int id)
        {
            _id = id;
        }
        public override string Code => "github_account_not_found";
        public override string Reason => $"{_id} is not a valid id for a github account in this system";
    }
}