namespace Site.Core.Exceptions.General
{
    public class EmailInUseException : ExceptionBase
    {
        public override string Code => "email_in_use";
        public override string Reason => "The email provided is already in use.";
    }
}