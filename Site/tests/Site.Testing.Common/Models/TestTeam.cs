using System;
using Dapper.Contrib.Extensions;

namespace Site.Testing.Common.Models
{
    [Table("Teams")]
    public class TestTeam
    {
        public TestTeam()
        {
            Name = "Test Team";
            CreatedAt = DateTime.Now;
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        
    }
}