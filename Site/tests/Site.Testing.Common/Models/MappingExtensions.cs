using Site.Core.Entities;

namespace Site.Testing.Common.Models
{
    public static class MappingExtensions
    {
        public static TestParticipant AsTest(this Participant participant, int teamId = 0)
        {
            return new(teamId)
            {
                Id = participant.Id,
                Forename = participant.Forename,
                Surname = participant.Surname,
                Email = participant.Email,
                CreatedAt = participant.CreatedAt
            };
        }

        public static TestTeam AsTest(this Team team) => new()
        {
            Id = team.Id,
            Name = team.Name,
            CreatedAt = team.CreatedAt
        };
    }
}