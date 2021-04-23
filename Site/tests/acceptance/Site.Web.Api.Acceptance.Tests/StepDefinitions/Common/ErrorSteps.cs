using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Site.Core.DTO.Common;
using Site.Web.Acceptance.Helpers;
using TechTalk.SpecFlow;

namespace Site.Web.Acceptance.StepDefinitions.Common
{
    [Binding]
    public class ErrorSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ErrorSteps(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
        }
        
        [Then(@"a bad request response should be returned")]
        public async Task ThenABadRequestResponseShouldBeReturned()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _scenarioContext.Set(await ErrorsHelper.GetError(response));
        }

        [Then(@"it should have a code '(.*)'")]
        public void ThenItShouldHaveACode(string code)
        {
            _scenarioContext.Get<ErrorDto>().Code.Should().Be(code);
        }

        [Then(@"the reason should be '(.*)'")]
        public void TheReasonShouldBe(string reason)
        {
            _scenarioContext.Get<ErrorDto>().Reason.Should().Be(reason);
        }

        [Then(@"it should be friendly to the user")]
        public void ThenItShouldBeFriendlyToTheUser()
        {
            _scenarioContext.Get<ErrorDto>().IsUserFriendly.Should().BeTrue();
        }
    }
}