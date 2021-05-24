using System.Threading.Tasks;
using Site.Testing.Common.Helpers.Docker;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.Hooks
{
    public class QueuesHooks
    {
        [BeforeTestRun]
        public async Task StartQueueService()
        {
            await new DockerContainerBuilder()
                .WithName("azurite-services")
                .WithImage("mcr.microsoft.com/azure-storage/azurite")
                .WithPortMapping(10000, 10000)
                .WithPortMapping(10001, 10001)
                .WithPortMapping(10002, 10002)
                .Start();
        }
    }
}