using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Testing.Common.Helpers;
using Site.Testing.Common.Models;

namespace Site.Core.Integration.Tests.Repositories
{
    public class ParticipantsRepositoryTests : ServiceUnderTest<IParticipantRepository, ParticipantRepository>
    {
        public override void Setup()
        {
            Mocker.Use<IDbConnection>(DbHelper.TestConnection);
        }

        [Test]
        public async Task IsEmailInUseAsync_ParticipantWithEmail_ReturnsTrue()
        {
            //Arrange
            var sut = CreateSut();

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

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

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId);
            await ParticipantsHelper.AddParticipantAsync(participant);

            //Act
            var result = await sut.IsEmailInUseAsync("test@test.com");

            //Assert
            Assert.IsFalse(result);
        }
    }
}