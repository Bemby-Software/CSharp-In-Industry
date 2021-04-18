using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions;
using Site.Core.Exceptions.Participants;
using Site.Core.Factories;
using Site.Core.Helpers;
using Site.Core.Services;

namespace Site.Core.Unit.Tests.Services
{
    public class TeamServiceTests : ServiceUnderTest<ITeamService, TeamService>
    {
        private Mock<ITeamRepository> _teamRepository;
        private Mock<ITokenFactory> _tokenFactory;
        private Mock<IEmailHelper> _emailHelper;
        private Mock<IParticipantService> _participantsService;

        public override void Setup()
        {
            _teamRepository = Mocker.GetMock<ITeamRepository>();
            _tokenFactory = Mocker.GetMock<ITokenFactory>();
            _emailHelper = Mocker.GetMock<IEmailHelper>();
            _participantsService = Mocker.GetMock<IParticipantService>();
        }

        [Test]
        public void CreateAsync_NoParticipants_ThrowsParticipantsRequired()
        {
            //Arrange
            var sut = CreateSut();

            var team = new Team();

            //Act
            //Assert
            Assert.ThrowsAsync<ParticipantsRequiredException>(() => sut.CreateAsync(team));
        }

        [Test]
        public async Task CreateAsync_ParticipantsInTeam_CreatesTokensForAll()
        {
            //Arrange
            var sut = CreateSut();

            var team = new Team
            {
                Participants = new List<Participant>
                {
                    new Participant(),
                    new Participant(),
                    new Participant()
                }
            };

            _tokenFactory.Setup(o => o.Create()).Returns(new Token());

            //Act
            await sut.CreateAsync(team);

            //Assert
            foreach (var teamParticipant in team.Participants) 
                Assert.IsNotNull(teamParticipant.Token);
        }
        
        [Test]
        public async Task CreateAsync_ParticipantsInTeam_ValidatesAllParticipants()
        {
            //Arrange
            var sut = CreateSut();

            var team = new Team
            {
                Participants = new List<Participant>
                {
                    new Participant(),
                    new Participant(),
                    new Participant()
                }
            };

            //Act
            await sut.CreateAsync(team);

            //Assert
            foreach (var participant in team.Participants)
            {
                _participantsService.Verify(o => o.ValidateParticipant(participant));
            }
        }
        
        [Test]
        public async Task CreateAsync_ParticipantsInTeam_CreatesTeam()
        {
            //Arrange
            var sut = CreateSut();

            var team = new Team
            {
                Participants = new List<Participant>
                {
                    new Participant(),
                    new Participant(),
                    new Participant()
                }
            };

            //Act
            await sut.CreateAsync(team);

            //Assert
            _teamRepository.Verify(o => o.CreateTeamsAsync(team));
        }
        
        [Test]
        public async Task CreateAsync_ParticipantsInTeam_SendsEmailsToParticipants()
        {
            //Arrange
            var sut = CreateSut();

            var team = new Team
            {
                Participants = new List<Participant>
                {
                    new Participant(),
                    new Participant(),
                    new Participant()
                }
            };

            //Act
            await sut.CreateAsync(team);

            //Assert
            _emailHelper.Verify(o => o.SendSignedUpEmailsAsync(team.Participants));
        }
        
        
    }
}