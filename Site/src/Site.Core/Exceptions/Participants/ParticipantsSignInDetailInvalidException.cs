namespace Site.Core.Exceptions.Participants
{
    public class ParticipantsSignInDetailInvalidException : ExceptionBase
    {
        public override string Code => "invalid_credentials";
        public override string Reason => "The credentials provided do not match our records.";
    }
}