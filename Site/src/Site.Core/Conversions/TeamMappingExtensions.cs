using System;
using System.Linq;
using Site.Core.DTO.Requests;
using Site.Core.Entities;

namespace Site.Core.Conversions
{
    public static class TeamMappingExtensions
    {
        public static Team AsEntity(this CreateTeamRequest request) => new Team()
        {
            Name = request.Name,
            CreatedAt = DateTime.Now,
            Participants = request.Participants?.Select(x => x.AsEntity()).ToList()
        };
    }
}