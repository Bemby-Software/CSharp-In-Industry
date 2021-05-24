using BoDi;
using TechTalk.SpecFlow;
using WireMock.Server;

namespace Site.Functions.Acceptance.Tests.Hooks
{
    [Binding]
    public class GitHubApiHooks
    {
        public const string GitHubApiWireMockServer = "github-api";

        public static WireMockServer GitHubServer;
        

        [BeforeTestRun]
        public static void StartGitHubApiServer(IObjectContainer container)
        {
            var server = WireMockServer.Start(9000);
            GitHubServer = server;
        }

        [BeforeScenario]
        public static void ResetServer(IObjectContainer container)
        {
            // if(GitHubServer is null)
            //     return;
            //
            // GitHubServer.Reset();
        }
    }
}