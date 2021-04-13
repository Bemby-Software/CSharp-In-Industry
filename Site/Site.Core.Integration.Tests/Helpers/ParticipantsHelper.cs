using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Integration.Tests.Helpers
{
    public class ParticipantsHelper
    {
        public static async Task<List<Participant>> GetParticipantsInTeamAsync(int teamId)
        {
            var participants = await DbHelper.Query<Participant>(@"SELECT * FROM Participants WHERE TeamId = @TeamId",
                new {TeamId = teamId});

            return participants.ToList();
        }
    }
}