namespace Site.Core.Exceptions.Participants
{
    public class ParticipantsRequiredException : ExceptionBase
    {
        public override string Code => "participants_required";
        public override string Reason => "A team cannot be created without any participants";
    }
}