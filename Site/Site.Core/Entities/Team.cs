using System;
using System.Collections.Generic;

namespace Site.Core.Entities
{
    public class Team
    {
        public Team()
        {
            Participants = new List<Participant>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Participant> Participants { get; set; }
    }
}