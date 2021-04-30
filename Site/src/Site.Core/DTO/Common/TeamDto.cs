using System.Collections.Generic;

namespace Site.Core.DTO.Common
{
    public class TeamDto
    {
        public TeamDto()
        {
            Participants = new List<ParticipantDto>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ParticipantDto> Participants { get; set; }
    }
}