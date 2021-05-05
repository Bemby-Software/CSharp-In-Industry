using Site.Core.DTO.Common;

namespace Site.Core.DTO.Responses
{
    public class GetParticipantResponse
    {
        public int Id { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        
        public TeamDto Team { get; set; }
    }
}