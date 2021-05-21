using System;
using System.Threading.Tasks;
using Site.Testing.Common.Helpers;
using Site.Testing.Common.Helpers.Docker;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using ContainerBuilder = Site.Testing.Common.Helpers.Docker.ContainerBuilder;

namespace Site.Web.Acceptance.Hooks
{
    [Binding]
    public class DatabaseHooks
    {
        [BeforeTestRun]
        public static async Task DatabaseUp()
        {
            var settings = TestConfiguration.GetConfiguration();

            await new ContainerBuilder()
                .WithName(settings.SqlServerContainerName)
                .WithImage(settings.SqlServerImage)
                .WithPortMapping(settings.SqlServerPort, settings.SqlServerPort)
                .WithEnvironmentVariables("ACCEPT_EULA", "Y")
                .WithEnvironmentVariables("SA_PASSWORD", settings.SqlServerPassword)
                .Start();
            
            await DbHelper.EnsureStarted(settings.DbServerConnectionString, TimeSpan.FromSeconds(60));
            
            await DbHelper.ReCreateDatabase();
        }

        [BeforeScenario]
        public static async Task ResetDatabase(ScenarioContext scenarioContext)
        {
            await DbHelper.RespawnDb();
        }
        
    }
}