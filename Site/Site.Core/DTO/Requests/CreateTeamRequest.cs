using System.Collections.Generic;
using Site.Core.DTO.Common;

namespace Site.Core.DTO.Requests
{
    public class CreateTeamRequest
    {
        public string Name { get; set; }
        
        public List<CreateParticipantDto> Participants;
    }
}