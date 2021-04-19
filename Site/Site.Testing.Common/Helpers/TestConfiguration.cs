using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Site.Testing.Common.Helpers
{
    public class TestConfiguration
    {

        public static TestConfiguration GetConfiguration()
        {
            var current = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(current)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var testConfiguration = new TestConfiguration();
            
            config.GetSection("Settings").Bind(testConfiguration);
            
            return testConfiguration;
        }

        #region Properties

        public string SqlServerImage { get; set; }

        public int SqlServerPort { get; set; }

        public string SqlServerContainerName { get; set; }
        
        public string SqlServerPassword { get; set; }

        public string SqlServerUser { get; set; }

        public string SqlServer { get; set; }

        public string DbServerConnectionString => $@"Server={SqlServer};User Id={SqlServerUser};Password={SqlServerPassword}";
        
        public string DbConnectionString => $@"Server={SqlServer};Database={SqlServerDatabase};User Id={SqlServerUser};Password={SqlServerPassword}";
        public string SqlServerDatabase { get; set; }

        #endregion

    }
}