using BoDi;
using TechTalk.SpecFlow;
using WireMock.Server;

namespace Site.Functions.Acceptance.Tests.Hooks
{
    [Binding]
    public class GitHubApiHooks
    {
        private readonly IObjectContainer _objectContainer;

        public const string GitHubApiWireMockServer = "github-api";

        public GitHubApiHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public void StartGitHubApiServer()
        {
            var server = WireMockServer.Start(9000);
            _objectContainer.RegisterInstanceAs(server, GitHubApiWireMockServer);
        }

        [BeforeScenario]
        public void ResetServer()
        {
            var server = _objectContainer.Resolve<WireMockServer>(GitHubApiWireMockServer);
            server.Reset();
        }
    }
}