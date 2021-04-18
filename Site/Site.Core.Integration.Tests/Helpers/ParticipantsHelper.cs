using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using Site.Core.Entities;
using Site.Core.Integration.Tests.Models;

namespace Site.Core.Integration.Tests.Helpers
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
            => await TestStartup.TestConnection.InsertAsync(participant);
    }
}