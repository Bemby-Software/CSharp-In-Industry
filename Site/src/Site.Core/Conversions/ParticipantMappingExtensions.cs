using System;
using Site.Core.DTO.Common;
using Site.Core.Entities;

namespace Site.Core.Conversions
{
    public static class ParticipantMappingExtensions
    {
        public static Participant AsEntity(this CreateParticipantDto dto) => new Participant
        {
            Email = dto.Email,
            Forename = dto.Forename,
            Surname = dto.Surname,
            CreatedAt = DateTime.Now
        };
    }
}