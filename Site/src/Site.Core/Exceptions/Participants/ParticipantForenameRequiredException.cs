namespace Site.Core.Exceptions.Participants
{
    public class ParticipantForenameRequiredException : ExceptionBase
    {
        public override string Code => "forename_required";
        public override string Reason => "A participant must have a forename";
    }
}