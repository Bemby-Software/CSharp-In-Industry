using System;
using Dapper.Contrib.Extensions;
using Site.Core.Entities;

namespace Site.Core.Integration.Tests.Models
{
    [Table("Participants")]
    public class TestParticipant
    {
        public TestParticipant(int teamId)
        {
            Forename = "Joe";
            Surname = "Bloggs";
            Email = "joe.bloggs@gmail.com";
            CreatedAt = DateTime.Now;
            TeamId = teamId;
        }
        
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int TeamId { get; set; }
        
    }
}