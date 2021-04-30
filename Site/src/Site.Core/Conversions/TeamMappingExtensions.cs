using System;
using System.Collections.Generic;
using System.Linq;
using Site.Core.DTO.Common;
using Site.Core.DTO.Requests;
using Site.Core.Entities;

namespace Site.Core.Conversions
{
    public static class TeamMappingExtensions
    {
        public static Team AsEntity(this CreateTeamRequest request) => 
            new()
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                Participants = request.Participants.SelectOrEmpty(x => x.AsEntity()).ToList()
            };

        public static TeamDto AsTeamDto(this Team team) =>
            new()
            {
                Id = team.Id,
                Name = team.Name,
                Participants = team.Participants.SelectOrEmpty(p => p.AsParticipantDto())
            };
    }
}