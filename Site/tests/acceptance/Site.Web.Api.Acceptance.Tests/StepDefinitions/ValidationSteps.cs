using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Site.Core.Entities;
using Site.Testing.Common;
using TechTalk.SpecFlow;
using Xunit;

namespace Site.Web.Acceptance.StepDefinitions
{
    [Binding]
    public class ValidationSteps : IClassFixture<SiteWebApplicationFactory>
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;

        public ValidationSteps(SiteWebApplicationFactory factory, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _client = factory.CreateClient();
        }
        
        [Given(@"a participant is signed up with the email '(.*)'")]
        public async Task GivenAParticipantIsSignedUpWithTheEmail(string email)
        {
            var team = new Team
            {
                Name = "Test Team",
                Participants = new List<Participant>
                {
                    new() {Forename = "Joe", Surname = "Bloggs", Email = email}
                }
            };

            var response = await _client.PostAsJsonAsync("api/team", team);
            response.EnsureSuccessStatusCode();
        }

        [When(@"a participant tries to use email '(.*)'")]
        public async Task WhenAParticipantTriesToUseEmail(string email)
        {
            var response = await _client.GetAsync($"api/validation/isEmailInUse/{email}");
            _scenarioContext.Set(response);
        }

        [Then(@"the api returns true with the (.*) status code")]
        public void ThenTheApiReturnsTrueWithTheStatusCode(int code)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>();
            response.StatusCode.Should().Be((HttpStatusCode) code);
            response.ReadContentAsBoolean().Should().Be(true);
        }

        [Then(@"the api returns false with the (.*) status code")]
        public void ThenTheApiReturnsFalseWithTheStatusCode(int code)
        {
            var response = _scenarioContext.Get<HttpResponseMessage>();
            response.StatusCode.Should().Be((HttpStatusCode) code);
            response.ReadContentAsBoolean().Should().Be(false);
        }

        [Given(@"a team signs up with the name '(.*)'")]
        public async Task GivenATeamSignsUpWithTheName(string name)
        {
            var team = new Team {Name = name, Participants = new List<Participant>
            {
                new(){Forename = "Joe", Surname = "Bloggs", Email = "joe.bloggs@gmail.com"}
            }};

            await _client.PostAsJsonAsync("api/team", team);
        }

        [When(@"a team tries to sign up with the name '(.*)'")]
        public async Task WhenATeamTriesToSignUpWithTheName(string name)
        {
            var response = await _client.GetAsync($"api/validation/isTeamNameInUse/{name}");
            _scenarioContext.Set(response);
        }
    }
}