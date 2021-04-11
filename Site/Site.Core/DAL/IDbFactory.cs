using System.Data;

namespace Site.Core.DAL
{
    public interface IDbFactory
    {
        IDbConnection CreateDbConnection();
    }
}