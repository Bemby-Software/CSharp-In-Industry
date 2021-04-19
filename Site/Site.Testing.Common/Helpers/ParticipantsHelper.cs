using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Site.Core.DAL.Health;
using Site.Core.Entities;
using Site.Testing.Common.Models;

namespace Site.Testing.Common.Helpers
{
    public static class ParticipantsHelper
    {
        public static async Task<List<Participant>> GetParticipantsInTeamAsync(int teamId)
        {
            var participants = await DbHelper.Query<Participant>(@"SELECT * FROM Participants WHERE TeamId = @TeamId",
                new {TeamId = teamId});

            return participants.ToList();
        }

        public static async Task AddParticipantAsync(TestParticipant participant) 
            => await DbHelper.TestConnection.InsertAsync(participant);
    }
}