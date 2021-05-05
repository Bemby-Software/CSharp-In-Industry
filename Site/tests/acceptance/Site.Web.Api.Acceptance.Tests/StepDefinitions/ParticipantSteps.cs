using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Site.Core.DTO.Responses;
using Site.Core.Entities;
using Site.Testing.Common.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Utf8Json;
using Xunit;

namespace Site.Web.Acceptance.StepDefinitions
{
    [Binding]
    public class ParticipantSteps : IClassFixture<SiteWebApplicationFactory>
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient _client;


        public ParticipantSteps(ScenarioContext scenarioContext, SiteWebApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _client = factory.CreateClient();
        }
        
        [Given(@"a participant with the token '(.*)'")]
        public void GivenAParticipantWithTheToken(string token)
        {
            _scenarioContext.Set(token, "token value");
        }

        [Given(@"the details is signed up")]
        public async Task GivenTheDetailsIsSignedUp(Table table)
        {
            var participants = table.CreateSet<Participant>();
            var participant = participants.First();
            participant.CreatedAt = DateTime.Now;

            var team = new Team()
            {
                Name = "Test Team 1",
                CreatedAt = DateTime.Now,
                Participants = new List<Participant>()
                {
                    new Participant
                    {
                        Forename = "Fred",
                        Surname = "Richards",
                        Email = "fred.richards@test.com",
                        CreatedAt = DateTime.Now
                    }
                }
            };

            var (teamId, participantId) = await FlowHelpers.SignUpParticipantAsyncWithTeamAndGetTeamIdAndParticipantIdAsync(
                _scenarioContext.Get<string>("token value"), participant, team);
            
            _scenarioContext.Set(team);
            _scenarioContext.Set(teamId, "teamId");
            _scenarioContext.Set(participantId, "participantId");
            _scenarioContext.Set(participant);
        }

        [When(@"that participant tries to get there details")]
        public async Task WhenThatParticipantTriesToGetThereDetails()
        {
            var response = await _client.GetParticipantAsync(_scenarioContext.Get<string>("token value"), false);
            _scenarioContext.Set(response);
        }


        [Then(@"the correct participant details")]
        public async Task ThenTheCorrectParticipantDetails()
        {
            var response = _scenarioContext.Get<HttpResponseMessage>();
            var content = await response.Content.ReadAsStringAsync();
            var participantResponse = await response.Content.ReadFromJsonAsync<GetParticipantResponse>();
            var participant = _scenarioContext.Get<Participant>();
            

            participantResponse.Should().NotBeNull();
            participantResponse?.Id.Should().Be(_scenarioContext.Get<int>("participantId"));
            participantResponse?.Forename.Should().Be(participant.Forename);
            participantResponse?.Surname.Should().Be(participant.Surname);
            participantResponse?.Email.Should().Be(participant.Email);
            
            _scenarioContext.Set(participantResponse);
        }

        [When(@"that participant tries to get there details including the team")]
        public async Task WhenThatParticipantTriesToGetThereDetailsIncludingTheTeam()
        {
            var response = await _client.GetParticipantAsync(_scenarioContext.Get<string>("token value"), true);
            _scenarioContext.Set(response);
        }

        [Then(@"the team information is returned")]
        public void ThenTheTeamInformationIsReturned()
        {
            var response = _scenarioContext.Get<GetParticipantResponse>();
            var team = _scenarioContext.Get<Team>();

            response.Team.Should().NotBeNull();
            response.Team.Name.Should().Be(team.Name);
            response.Team.Participants.Count().Should().Be(team.Participants.Count + 1);
        }
    }
}