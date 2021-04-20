using System;
using System.Threading.Tasks;
using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.Hooks
{
    [Binding]
    public class DatabaseHooks
    {
        [BeforeTestRun]
        public static async Task DatabaseUp()
        {
            await SqlServerContainer.StartAsync();
            var settings = TestConfiguration.GetConfiguration();

            await DbHelper.EnsureStarted(settings.DbServerConnectionString, TimeSpan.FromSeconds(60));
            
            await DbHelper.CreateTestDatabase(settings);
        }

        [BeforeScenario]
        public static async Task ResetDatabase(ScenarioContext scenarioContext)
        {
            await DbHelper.ReCreateDatabase();
        }
        
    }
}