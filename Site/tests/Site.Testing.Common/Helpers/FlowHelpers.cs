using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Site.Core;
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

        public static async Task<Tuple<int, int>> SignUpParticipantAsyncAndGetTeamIdAndParticipantIdAsync(string token,
            Participant participant)
        {
            var teamId = await DbHelper.TestConnection.InsertAsync(new TestTeam());

            participant.TeamId = teamId;

            var participantId = await DbHelper.TestConnection.InsertAsync(participant.AsTest());

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now,
                Value = token,
                IsValid = true,
                ParticipantId = participantId,
                TeamId = teamId
            });

            return new(teamId, participantId);
        }

        public static async Task<Tuple<int, int>> SignUpParticipantAsyncWithTeamAndGetTeamIdAndParticipantIdAsync(
            string token, Participant participant, Team team)
        {
            var teamId = await DbHelper.TestConnection.InsertAsync(team.AsTest());

            var participants = team.Participants.SelectOrEmpty(p => p.AsTest(teamId));

            await DbHelper.TestConnection.InsertAsync(participants);

            var participantId = await DbHelper.TestConnection.InsertAsync(participant.AsTest(teamId));

            await DbHelper.TestConnection.InsertAsync(new Token
            {
                CreatedAt = DateTime.Now,
                Value = token,
                IsValid = true,
                ParticipantId = participantId,
                TeamId = teamId
            });

            return new(teamId, participantId);
        }
    }
}