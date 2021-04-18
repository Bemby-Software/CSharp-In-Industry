using System;
using Dapper.Contrib.Extensions;

namespace Site.Core.Entities
{
    [Table("Tokens")]
    public class Token
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsValid { get; set; }

        public string Value { get; set; }

        public int TeamId { get; set; }

        public int ParticipantId { get; set; }
    }
}   