namespace Site.Core.Exceptions.Teams
{
    public class TeamNotFoundException : ExceptionBase
    {
        private readonly int _teamId;

        public TeamNotFoundException(int teamId)
        {
            _teamId = teamId;
        }

        public override string Code => "team_not_found";
        public override string Reason => $"The team with id {_teamId} was not found";
    }
}