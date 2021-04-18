using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Integration.Tests.Helpers;
using Site.Core.Integration.Tests.Models;

namespace Site.Core.Integration.Tests.Repositories
{
    public class ParticipantsRepositoryTests : ServiceUnderTest<IParticipantRepository, ParticipantRepository>
    {
        public override void Setup()
        {
            Mocker.Use<IDbConnection>(TestStartup.TestConnection);
        }

        [Test]
        public async Task IsEmailInUseAsync_ParticipantWithEmail_ReturnsTrue()
        {
            //Arrange
            var sut = CreateSut();

            var teamId = await TestStartup.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId);
            await ParticipantsHelper.AddParticipantAsync(participant);

            //Act
            var result = await sut.IsEmailInUseAsync(participant.Email);

            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public async Task IsEmailInUseAsync_ParticipantWithoutEmail_ReturnsFalse()
        {
            //Arrange
            var sut = CreateSut();

            var teamId = await TestStartup.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId);
            await ParticipantsHelper.AddParticipantAsync(participant);

            //Act
            var result = await sut.IsEmailInUseAsync("test@test.com");

            //Assert
            Assert.IsFalse(result);
        }
    }
}