using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using TechTalk.SpecFlow.Assist;
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

        [Given(@"a team with the name '(.*)'")]
        public void GivenATeamWithTheName(string teamName)
        {
            var team = new Team() {Name = teamName};
            _scenarioContext.Set(team);
        }

        [Given(@"No Participants")]
        public void GivenNoParticipants()
        {
            _scenarioContext.Get<Team>().Participants = new List<Participant>();
        }

        [When(@"the team tries to sign up")]
        public async Task WhenTheTeamTriesToSignUp()
        { 
            var result = await _client.PostAsJsonAsync("api/team", _scenarioContext.Get<Team>());
            _scenarioContext.Set(result);
        }

        [Given(@"participants with the details")]
        public void GivenParticipantsWithTheDetails(Table table)
        {
            var participants = table.CreateSet<Participant>().ToList();
            _scenarioContext.Get<Team>().Participants = participants;
        }

        [Given(@"participants with the details are already signed up")]
        public async Task GivenParticipantsWithTheDetailsAreAlreadySignedUp(Table table)
        {
            var participants = table.CreateSet<Participant>().ToList();
            var team = _scenarioContext.Get<Team>();
            team.Participants = participants;
            var response = await _client.PostAsJsonAsync("api/team", team);
            response.EnsureSuccessStatusCode();
        }

        [Given(@"another team with the name '(.*)'")]
        public void GivenAnotherTeamWithTheName(string teamName)
        {
            var team = new Team() {Name = teamName};
            _scenarioContext.Set(team);
        }

        [Then(@"a ok response is returned")]
        public void ThenAOkResponseIsReturned()
        {
            _scenarioContext.Get<HttpResponseMessage>().StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the tokens should have been sent to the participants")]
        public void ThenTheTokensShouldHaveBeenSentToTheParticipants()
        {
            //TODO: How to check emails sent? Mock out the service and check the calls? Or just accept this would be covered by UT. This could also be covered by a fake email service that actually gathers the emails but just does not send them.
        }

        [Then(@"the records should reside in the database")]
        public void ThenTheRecordsShouldResideInTheDatabase()
        {
            //TODO: Get full team from database including participants and tokens and verify them? Do we do this or just make an API call to get these back later on?
        }
    }   
}