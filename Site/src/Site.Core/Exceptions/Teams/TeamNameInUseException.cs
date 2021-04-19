namespace Site.Core.Exceptions.Teams
{
    public class TeamNameInUseException : ExceptionBase
    {
        public override string Code => "team_name_in_use";
        public override string Reason => "The team name is already in use";
    }
}