namespace Site.Core.Exceptions.General
{
    public class InvalidEmailException : ExceptionBase
    {
        public override string Code => "invalid_email";
        public override string Reason => "The email provided is invalid";
    }
}