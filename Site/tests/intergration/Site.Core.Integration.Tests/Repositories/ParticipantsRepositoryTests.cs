using System;
using System.Data;
using System.Linq;
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
            DbHelper.ReCreateDatabase();
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

        [Test]
        public async Task GetAsync_TokenForParticipant_GetsParticipant()
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
            var got = await sut.GetAsync(tokenValue);

            //Assert
            Assert.AreEqual(participantId, got.Id);
            Assert.AreEqual(participant.Forename, got.Forename);
            Assert.AreEqual(participant.Surname, got.Surname);
            Assert.AreEqual(participant.Email, got.Email);
            Assert.AreEqual(teamId, got.TeamId);
        }

        [Test]
        public async Task GetAllAsync_TeamId_GetsAllParticipants()
        {
            //Arrange
            var sut = CreateSut();
            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());


            var sample = new TestParticipant(teamId) {Email = "test-sign-in@test.com"};
            await DbHelper.TestConnection.InsertAsync(sample);
            await DbHelper.TestConnection.InsertAsync(sample);
            await DbHelper.TestConnection.InsertAsync(sample);
            await DbHelper.TestConnection.InsertAsync(sample);
            
            //Act
            var participants = await sut.GetAllAsync(teamId);
            
            //Assert
            Assert.AreEqual(4, participants.Count);
            var participant = participants.First();
            Assert.AreEqual(participant.Forename, sample.Forename);
            Assert.AreEqual(participant.Surname, sample.Surname);
            Assert.AreEqual(participant.Email, sample.Email);
            Assert.AreEqual(teamId, sample.TeamId);
            Assert.AreNotEqual(0, participant.Id);
        }
    }
}