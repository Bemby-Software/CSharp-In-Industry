using System;
using Dapper.Contrib.Extensions;

namespace Site.Core.Entities
{
    [Table("Participants")]
    public class Participant
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int TeamId { get; set; }
        
        public Team Team { get; set; }

        public Token Token { get; set; }
    }
}