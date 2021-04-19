namespace Site.Core.Exceptions.Teams
{
    public class TeamNameRequiredException : ExceptionBase
    {
        public override string Code => "team_name_required";
        public override string Reason => "A team name is required";
    }
}