using System;

namespace Site.Core.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public abstract string Code { get; }
        
        public abstract string Reason { get; }
    }
}