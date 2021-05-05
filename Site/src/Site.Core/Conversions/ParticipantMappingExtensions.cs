using System;
using Site.Core.DTO.Common;
using Site.Core.DTO.Responses;
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

        public static ParticipantDto AsParticipantDto(this Participant participant)
        {
            return new ParticipantDto()
            {
                Id = participant.Id,
                Forename = participant.Forename,
                Surname = participant.Surname,
                Email = participant.Email
            };
        }

        public static GetParticipantResponse AsGetParticipantResponse(this Participant participant)
        {
            return new GetParticipantResponse
            {
                Id = participant.Id,
                Forename = participant.Forename,
                Surname = participant.Surname,
                Email = participant.Email,
                Team = participant.Team?.AsTeamDto()
            };
        }
    }
}