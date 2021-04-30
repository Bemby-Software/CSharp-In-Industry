using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Site.Core.Entities;
using Site.Testing.Common.Models;

namespace Site.Testing.Common.Helpers
{
    public static class FlowHelpers
    {
        public static async Task<string> SignUpTeamWithUserAndGetTokensAsync(string userEmail)
        {
            var tokenValue = Guid.NewGuid().ToString();

            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            var participant = new TestParticipant(teamId) {Email = userEmail};
            var participantId = await DbHelper.TestConnection.InsertAsync(participant);

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now, 
                Value = tokenValue, 
                IsValid = true, 
                ParticipantId = participantId,
                TeamId = teamId
            });

            return tokenValue;
        }
    }
}