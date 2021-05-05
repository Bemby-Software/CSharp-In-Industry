using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Site.Core.Conversions;
using Site.Core.DTO.Common;
using Site.Core.DTO.Requests;
using Site.Core.Entities;

namespace Site.Core.Unit.Tests.Conversions
{
    public class TeamMappingExtensionsTests
    {
        [Test]
        public void CreateTeamRequest_AsEntity_NoParticipants_ReturnsEmptyList()
        {
            //Arrange
            var request = new CreateTeamRequest
            {
                Name = "Test",
                Participants = null
            };
            
            //Act
            var team = request.AsEntity();
            
            //Assert
            Assert.IsEmpty(team.Participants);
        }
        
        [Test]
        public void CreateTeamRequest_AsEntity_MapsCorrectly()
        {
            //Arrange
            var request = new CreateTeamRequest
            {
                Name = "Test",
                Participants = new ()
                {
                    new()
                    {
                        Email = "test-email", 
                        Forename = "test-forename", 
                        Surname = "test-surname"
                    }
                }
            };
            
            //Act
            var team = request.AsEntity();
            
            //Assert
            Assert.AreEqual(request.Name, team.Name);
            Assert.AreEqual(request.Participants.First().Email, team.Participants.First().Email);
            Assert.AreEqual(request.Participants.First().Forename, team.Participants.First().Forename);
            Assert.AreEqual(request.Participants.First().Surname, team.Participants.First().Surname);
        }
        
        [Test]
        public void Team_AsTeamDto_NoParticipants_ReturnsEmptyList()
        {
            //Arrange
            var team = new Team()
            {
                Name = "Test",
                Participants = null
            };
            
            //Act
            var dto = team.AsTeamDto();
            
            //Assert
            Assert.IsEmpty(dto.Participants);
        }
        
        [Test]
        public void Team_AsTeamDto_MapsCorrectly()
        {
            //Arrange
            var request = new Team()
            {
                Name = "Test",
                Participants = new ()
                {
                    new()
                    {
                        Id = 1,
                        Email = "test-email", 
                        Forename = "test-forename", 
                        Surname = "test-surname"
                    }
                }
            };
            
            //Act
            var teamDto = request.AsTeamDto();
            
            //Assert
            Assert.AreEqual(request.Name, teamDto.Name);
            Assert.AreEqual(request.Participants.First().Email, teamDto.Participants.First().Email);
            Assert.AreEqual(request.Participants.First().Forename, teamDto.Participants.First().Forename);
            Assert.AreEqual(request.Participants.First().Surname, teamDto.Participants.First().Surname);
            Assert.AreEqual(request.Participants.First().Id, teamDto.Participants.First().Id);
        }
        
    }
}