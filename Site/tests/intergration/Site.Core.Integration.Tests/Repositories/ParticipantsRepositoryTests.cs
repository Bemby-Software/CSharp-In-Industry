using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
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

        [Test]
        public async Task AreSignInDetailsValidAsync_ValidDetails_ReturnsTrue()
        {
            //Arrange
            var sut = CreateSut();

            var tokenValue = "some-token";

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId) {Email = "test-sign-in@test.com"};
            var participantId = await DbHelper.TestConnection.InsertAsync(participant);

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now, 
                Value = tokenValue, 
                IsValid = true, 
                ParticipantId = participantId,
                TeamId = teamId
            });
            
            //Act
            var result = await sut.AreSignInDetailsValidAsync(participant.Email, tokenValue);
            
            //Assert
            Assert.IsTrue(result);
        }
        
        [Test]
        public async Task AreSignInDetailsValidAsync_InValidEmail_ReturnsFalse()
        {
            //Arrange
            var sut = CreateSut();

            var tokenValue = "some-token";

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId) {Email = "test-sign-in@test.com"};
            var participantId = await DbHelper.TestConnection.InsertAsync(participant);

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now, 
                Value = tokenValue, 
                IsValid = true, 
                ParticipantId = participantId,
                TeamId = teamId
            });
            
            //Act
            var result = await sut.AreSignInDetailsValidAsync("test@test.com", tokenValue);
            
            //Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public async Task AreSignInDetailsValidAsync_InValidToken_ReturnsFalse()
        {
            //Arrange
            var sut = CreateSut();

            var tokenValue = "some-token";

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId) {Email = "test-sign-in@test.com"};
            var participantId = await DbHelper.TestConnection.InsertAsync(participant);

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now, 
                Value = tokenValue, 
                IsValid = true, 
                ParticipantId = participantId,
                TeamId = teamId
            });
            
            //Act
            var result = await sut.AreSignInDetailsValidAsync(participant.Email, " bad-token");
            
            //Assert
            Assert.IsFalse(result);
        }
    }
}