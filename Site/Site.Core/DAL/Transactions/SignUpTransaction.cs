using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Site.Core.Entities;

namespace Site.Core.DAL.Transactions
{
    public class SignUpTransaction : ISignUpTransaction
    {
        private readonly IDbConnection _dbConnection;

        private const string InsertTeamSql = @"INSERT INTO Teams (Name, CreatedAt) VALUES (@Name, @CreatedAt); 
                                                SELECT SCOPE_IDENTITY();";
        
        private const string InsertParticipantsSql = @"INSERT INTO Participants (Forename, Surname, Email, TeamId, CreatedAt)
                                                        VALUES (@Forename, @Surname, @Email, @TeamId, @CreatedAt);
                                                        SELECT SCOPE_IDENTITY();";
        
        private const string InsertTokensSql = @"INSERT INTO Tokens (CreatedAt, IsValid, [Value], TeamId, ParticipantId)
                                                 VALUES (@CreatedAt, @IsValid, @Value, @TeamId, @ParticipantId)";

        public SignUpTransaction(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public async Task SignUpAsync(Team team)
        {
            _dbConnection.Open();
            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                var teamIds = await _dbConnection.QueryAsync<int>(InsertTeamSql, team, transaction);

                var teamId = teamIds.First();
                
                team.Participants.ForEach(p => p.TeamId = teamId);

                foreach (var participant in team.Participants)
                {
                    var ids = await _dbConnection.QueryAsync<int>(InsertParticipantsSql, participant, transaction);
                    participant.Token.ParticipantId = ids.First();
                    participant.Token.TeamId = teamId;

                    await _dbConnection.ExecuteAsync(InsertTokensSql, participant.Token, transaction);
                }
                
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}