using System.Data;

namespace Site.Core.DAL.Factories
{
    public interface IDbFactory
    {
        IDbConnection CreateDbConnection();
    }
}