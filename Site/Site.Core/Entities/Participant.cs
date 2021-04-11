using System;

namespace Site.Core.Entities
{
    public class Participant
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int TeamId { get; set; }

        public Token Token { get; set; }
    }
}