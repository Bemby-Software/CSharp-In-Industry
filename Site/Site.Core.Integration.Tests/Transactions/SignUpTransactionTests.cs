using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Transactions;
using Site.Core.Entities;
using Site.Core.Integration.Tests.Helpers;

namespace Site.Core.Integration.Tests.Transactions
{
    public class SignUpTransactionTests : ServiceUnderTest<ISignUpTransaction, SignUpTransaction>
    {
        public override void Setup()
        {
            Mocker.Use<IDbConnection>(Startup.TestConnection);
        }

        [Test]
        public async Task SignUpAsync_ValidTeam_SignsUp()
        {
            //Arrange
            var sut = CreateSut();

            var newTeam = new Team()
            {
                CreatedAt = DateTime.Now,
                Name = "Test Team",
                Participants = new List<Participant>
                {
                    new Participant
                    {
                        Forename = "John", 
                        Surname = "Doe", 
                        Email = "john.doe@gmail.com", 
                        CreatedAt = DateTime.Now,
                        Token = new Token
                        {
                            Value = "342345hjk34hgtkj34h5kjh345",
                            CreatedAt = DateTime.Now,
                            IsValid = true,
                        }
                    },
                    new Participant
                    {
                        Forename = "Mary", 
                        Surname = "Doe", 
                        Email = "mary.doe@gmail.com", 
                        CreatedAt = DateTime.Now,
                        Token = new Token
                        {
                            Value = "lfkasdlfhjlkasdhf87",
                            CreatedAt = DateTime.Now,
                            IsValid = true,
                        }
                    }
                }
            };

            //Act
            await sut.SignUpAsync(newTeam);

            //Assert
            var teams = await TeamsHelper.GetTeamsAsync();
            
            var team = teams.FirstOrDefault(t => t.Name == newTeam.Name);
            
            Assert.IsNotNull(team);
            
            Assert.AreEqual(newTeam.Name, team.Name);
            Assert.AreEqual(newTeam.CreatedAt.ToString(), team.CreatedAt.ToString());

            var participants = await ParticipantsHelper.GetParticipantsInTeamAsync(team.Id);

            var participant = newTeam.Participants[0];
            
            var john = participants.First(p => p.Forename == participant.Forename);
            
            Assert.IsNotNull(john);
            Assert.AreEqual(participant.Surname, john.Surname);
            Assert.AreEqual(participant.Email, john.Email);
            Assert.AreEqual(participant.CreatedAt.ToString(), john.CreatedAt.ToString());
            Assert.AreEqual(participant.TeamId, team.Id);

            var johnsToken = await TokensHelper.GetTokenForParticipantAsync(john.Id);
            
            Assert.AreEqual(participant.Token.Value, johnsToken.Value);
            Assert.AreEqual(participant.Token.CreatedAt.ToString(), participant.Token.CreatedAt.ToString());
            Assert.AreEqual(participant.Token.IsValid, participant.Token.IsValid);
            Assert.AreEqual(participant.Token.TeamId, team.Id);
            
            participant = newTeam.Participants[1];
            
            var mary = participants.First(p => p.Forename == participant.Forename);
            
            Assert.IsNotNull(mary);
            Assert.AreEqual(participant.Surname, mary.Surname);
            Assert.AreEqual(participant.Email, mary.Email);
            Assert.AreEqual(participant.CreatedAt.ToString(), mary.CreatedAt.ToString());
            Assert.AreEqual(participant.TeamId, team.Id);

            var marysToken = await TokensHelper.GetTokenForParticipantAsync(mary.Id);
            
            Assert.AreEqual(participant.Token.Value, marysToken.Value);
            Assert.AreEqual(participant.Token.CreatedAt.ToString(), participant.Token.CreatedAt.ToString());
            Assert.AreEqual(participant.Token.IsValid, participant.Token.IsValid);
            Assert.AreEqual(participant.Token.TeamId, team.Id);
        }
    }
}