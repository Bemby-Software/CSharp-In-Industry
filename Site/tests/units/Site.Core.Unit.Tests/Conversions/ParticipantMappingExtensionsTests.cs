using System;
using System.Linq;
using NUnit.Framework;
using Site.Core.Conversions;
using Site.Core.DTO.Common;
using Site.Core.Entities;

namespace Site.Core.Unit.Tests.Conversions
{
    public class ParticipantMappingExtensionsTests
    {
        [Test]
        public void CreateParticipantDto_AsEntity_MapsCorrectly()
        {
            //Arrange
            var createParticipantDto = new CreateParticipantDto()
            {
                Email = "test-email",
                Forename = "test-forename",
                Surname = "test-surname"
            };
            
            //Act
            var participant = createParticipantDto.AsEntity();
            
            //Assert
            Assert.AreEqual(createParticipantDto.Email, participant.Email);
            Assert.AreEqual(createParticipantDto.Forename, participant.Forename);
            Assert.AreEqual(createParticipantDto.Surname, participant.Surname);
            Assert.AreEqual(DateTime.Now.ToSecondsTolerance(), participant.CreatedAt.ToSecondsTolerance());
        }
        
        [Test]
        public void Participant_AsParticipantDto_MapsCorrectly()
        {
            //Arrange
            var participant = new Participant()
            {
                Id = 1,
                Email = "test-email",
                Forename = "test-forename",
                Surname = "test-surname"
            };
            
            //Act
            var dto = participant.AsParticipantDto();
            
            //Assert
            Assert.AreEqual(participant.Email, dto.Email);
            Assert.AreEqual(participant.Forename, dto.Forename);
            Assert.AreEqual(participant.Surname, dto.Surname);
            Assert.AreEqual(participant.Id, dto.Id);
        }

        [Test]
        public void Participant_AsGetParticipantResponse_NoTeam_TeamIsNull()
        {
            //Arrange
            var participant = new Participant()
            {
                Id = 1,
                Email = "test-email",
                Forename = "test-forename",
                Surname = "test-surname"
            };
            
            //Act
            var response = participant.AsGetParticipantResponse();

            //Assert
            Assert.AreEqual(participant.Email, response.Email);
            Assert.AreEqual(participant.Forename, response.Forename);
            Assert.AreEqual(participant.Surname, response.Surname);
            Assert.AreEqual(participant.Id, response.Id);
            Assert.IsNull(response.Team);
        }
        
        [Test]
        public void Participant_AsGetParticipantResponse_Team_TeamIsNull()
        {
            //Arrange
            var participant = new Participant()
            {
                Id = 1,
                Email = "test-email",
                Forename = "test-forename",
                Surname = "test-surname",
                Team = new Team()
                {
                    Id = 1,
                    Name = "test-team",
                    Participants = new ()
                    {
                        new()
                        {
                            Id = 2,
                            Email = "test-email1", 
                            Forename = "test-forename1", 
                            Surname = "test-surname1"
                        }
                    }
                }
            };
            
            //Act
            var response = participant.AsGetParticipantResponse();

            //Assert
            Assert.AreEqual(participant.Email, response.Email);
            Assert.AreEqual(participant.Forename, response.Forename);
            Assert.AreEqual(participant.Surname, response.Surname);
            Assert.AreEqual(participant.Id, response.Id);
            Assert.AreEqual(participant.Team.Id, response.Team.Id);
            Assert.AreEqual(participant.Team.Name, response.Team.Name);
            Assert.AreEqual(participant.Team.Participants.First().Email, response.Team.Participants.First().Email);
            Assert.AreEqual(participant.Team.Participants.First().Forename, response.Team.Participants.First().Forename);
            Assert.AreEqual(participant.Team.Participants.First().Surname, response.Team.Participants.First().Surname);
            Assert.AreEqual(participant.Team.Participants.First().Id, response.Team.Participants.First().Id);
        }
    }
}