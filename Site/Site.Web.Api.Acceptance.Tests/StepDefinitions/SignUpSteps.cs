using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Site.Core.DTO.Common;
using Site.Core.Entities;
using Site.Core.Exceptions.Teams;
using Site.Web.Acceptance.Helpers;
using TechTalk.SpecFlow;
using Xunit;

namespace Site.Web.Acceptance.StepDefinitions
{
    [Binding]
    public class SignUpSteps : IClassFixture<SiteWebApplicationFactory>
    {
        private readonly SiteWebApplicationFactory _factory;
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;
        private const string TeamName = "Mighty Coders";

        public SignUpSteps(SiteWebApplicationFactory factory, ScenarioContext scenarioContext)
        {
            _factory = factory;
            _scenarioContext = scenarioContext;
            _client = _factory.CreateClient();
        }
        
        [Given(@"a team exists with the name '(.*)'")]
        public async Task GivenATeamExistsWithTheName(string teamName)
        {
            var team = new Team()
            {
                Name = teamName,
                Participants = new List<Participant>
                {
                    new Participant
                    {
                        Forename = "Joe",
                        Surname = "Bloggs",
                        Email = "test@test.com",
                    }
                }
            };
            var result = await _client.PostAsJsonAsync("api/team", team);
            result.EnsureSuccessStatusCode();
            _scenarioContext.Add("team_name", teamName);
        }

        [When(@"a team tries to use the same name again")]
        public async Task WhenATeamTriesToUseTheSameNameAgain()
        {
            var team = new Team()
            {
                Name = _scenarioContext.Get<string>("team_name"),
                Participants = new List<Participant>
                {
                    new Participant
                    {
                        Forename = "Joe",
                        Surname = "Bloggs",
                        Email = "test@test.com",
                    }
                }
            };
            var result = await _client.PostAsJsonAsync("api/team", team);
            _scenarioContext.Set(result);
        }
        
        [Then(@"an bad request response should be returned")]
        public async Task ThenAnBadRequestResponseShouldBeReturned()
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

        [Then(@"a the reason '(.*)'")]
        public void ThenATheReason(string reason)
        {
            _scenarioContext.Get<ErrorDto>().Reason.Should().Be(reason);
        }

        [Then(@"should be friendly to the user")]
        public void ThenShouldBeFriendlyToTheUser()
        {
            _scenarioContext.Get<ErrorDto>().IsUserFriendly.Should().BeTrue();
        }
    }   
}