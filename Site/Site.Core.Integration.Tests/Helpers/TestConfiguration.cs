using System.IO;
using Microsoft.Extensions.Configuration;

namespace Site.Core.Integration.Tests.Helpers
{
    public class TestConfiguration
    {

        public static TestConfiguration GetConfiguration()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
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

        #endregion
           
    }
}