using System;

namespace Site.Core.DAL.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException(Exception inner) : base("Failed making database query", inner) {}
    }
}