namespace Site.Core.Exceptions.GitHubAccount
{
    public class WriteAccessNeededException : ExceptionBase
    {
        public override string Code => "no_write_access";
        public override string Reason => "Write access needed for all accounts listed in instructions";
    }
}