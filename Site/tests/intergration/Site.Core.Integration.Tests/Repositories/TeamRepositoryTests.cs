using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Testing.Common.Helpers;
using Site.Testing.Common.Models;

namespace Site.Core.Integration.Tests.Repositories
{
    public class TeamRepositoryTests : ServiceUnderTest<ITeamRepository, TeamRepository>
    {
        public override void Setup()
        {
            Mocker.Use<IDbConnection>(DbHelper.TestConnection);
        }

        [Test]
        public async Task IsTeamNameInUseAsync_TeamNameInDb_ReturnsTrue()
        {
            //Arrange
            var sut = CreateSut();

            var team = new TestTeam();
            await DbHelper.TestConnection.InsertAsync(team);
            
            //Act
            var result = await sut.IsTeamNameInUseAsync(team.Name);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public async Task IsTeamNameInUseAsync_NoTeamWithNameInDb_ReturnsFalse()
        {
            //Arrange
            var sut = CreateSut();
            
            
            //Act
            var result = await sut.IsTeamNameInUseAsync("Not In Db Team Name");
            
            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAsync_TeamId_GetsTeam()
        {
            //Arrange
            var sut = CreateSut();

            var team = new TestTeam();

            var teamId = await DbHelper.TestConnection.InsertAsync(team);

            //Act
            var got = await sut.GetAsync(teamId);

            //Assert
            Assert.AreEqual(teamId, got.Id);
            Assert.AreEqual(team.Name, got.Name);
        }
        
    }
}