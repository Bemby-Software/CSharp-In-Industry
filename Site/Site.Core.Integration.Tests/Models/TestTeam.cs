using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using Site.Core.Entities;

namespace Site.Core.Integration.Tests.Models
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