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

        public override void Setup()
        {
            _teamRepository = Mocker.GetMock<ITeamRepository>();
            _tokenFactory = Mocker.GetMock<ITokenFactory>();
            _emailHelper = Mocker.GetMock<IEmailHelper>();
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

            var team = new Team();
            team.Participants = new List<Participant>();

            //Act

            //Assert
        }
    }
}