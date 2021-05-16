namespace Site.Core.Exceptions.GitHubAccount
{
    public class IssueCopyAlreadyCompleteException : ExceptionBase
    {
        public override string Code => "issue_copy_already_complete";
        public override string Reason => "The github accounts issue transfer has already been completed";
    }
}