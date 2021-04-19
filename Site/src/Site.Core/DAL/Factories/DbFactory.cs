using System.Data;
using System.Data.SqlClient;
using Site.Core.Configuration;

namespace Site.Core.DAL.Factories
{
    public class DbFactory : IDbFactory
    {
        private readonly ISiteConfiguration _siteConfiguration;

        public DbFactory(ISiteConfiguration siteConfiguration)
        {
            _siteConfiguration = siteConfiguration;
        }
        
        public IDbConnection CreateDbConnection() => new SqlConnection(_siteConfiguration.DbConnectionString);
    }
}