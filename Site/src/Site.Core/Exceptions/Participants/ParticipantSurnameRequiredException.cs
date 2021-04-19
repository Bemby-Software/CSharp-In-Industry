namespace Site.Core.Exceptions.Participants
{
    public class ParticipantSurnameRequiredException : ExceptionBase
    {    
        public override string Code => "surname_required";
        public override string Reason => "A participant must have a surname";
    }
}