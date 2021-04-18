using System.ComponentModel;
using System.Net.Http;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;
using Xunit;

namespace Site.Web.Acceptance.Steps.SignUp
{
    [Binding]
    public class SignUpNamInUseStep : IClassFixture<WebApplicationFactory<ApiStartup>>
    {
        private readonly WebApplicationFactory<ApiStartup> _factory;
        private readonly HttpClient _client;

        public SignUpNamInUseStep(WebApplicationFactory<ApiStartup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();  
        }
        
        [Given(@"a team tries to sign up")]
        public void GivenATeamTriesToSignUp()
        {
            ScenarioContext.StepIsPending();
        }
        
        [When(@"the team name is in use")]
        public void WhenTheTeamNameIsInUse()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"an error should be returned")]
        public void ThenAnErrorShouldBeReturned()
        {
            ScenarioContext.StepIsPending();
        }

        
    }
}