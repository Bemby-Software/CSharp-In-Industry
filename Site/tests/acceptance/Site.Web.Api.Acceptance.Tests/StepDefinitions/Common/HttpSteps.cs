using System.Net;
using System.Net.Http;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.StepDefinitions.Common
{
    [Binding]
    public class HttpSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public HttpSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"the api returns a (.*) status code")]
        public void ThenTheApiReturnsAStatusCode(int status)
        {
            _scenarioContext.Get<HttpResponseMessage>().StatusCode.Should().Be((HttpStatusCode)status);
        }
    }
}