using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Specialized;
using Site.Testing.Common.Helpers;
using Site.Testing.Common.Helpers.Docker;
using Site.Testing.Common.Helpers.Functions;
using Site.Web.Acceptance.Functions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using ContainerBuilder = Site.Testing.Common.Helpers.Docker.ContainerBuilder;

namespace Site.Web.Acceptance.Hooks
{
    [Binding]
    public class FunctionsHooks
    {
        public static AzureFunctionsHelper FunctionsHelper;
        
        [BeforeTestRun]
        public static async Task Start(ITestRunnerManager testRunnerManager, ITestRunner testRunner)
        {
            await new ContainerBuilder()
                .WithName("azurite-services")
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                .WithPortMapping(10000, 10000)
                .WithPortMapping(10001, 10001)
                .WithPortMapping(10002, 10002)
                .Start();
            
            FunctionsHelper = new AzureFunctionsHelper(new FunctionsAppLogger());
            FunctionsHelper.Start("src/Site.Functions", "site.functions");
        }

        [AfterTestRun]
        public static void Stop()
        {
            if(FunctionsHelper is not null)
                FunctionsHelper.Stop("site.functions");
        }
    }
}