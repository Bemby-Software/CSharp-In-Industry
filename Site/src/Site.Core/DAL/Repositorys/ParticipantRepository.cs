using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IDbConnection _dbConnection;
        private const string IsEmailInUseSql = "SELECT * FROM Participants WHERE Email = @Email";

        private const string IsSignInDetailsValidSql = @"SELECT COUNT(*) FROM Participants 
                                                        LEFT JOIN Tokens ON Participants.Id = ParticipantId
                                                        WHERE IsValid = 1
                                                        AND Participants.Email = @Email
                                                        AND [Value] = @Token";
        private const string GetByTokenSql =
            @"SELECT * FROM Participants LEFT JOIN Tokens ON Participants.Id = ParticipantId WHERE [Value] = @Token";

        private const string GetAllByTeamIdSql = "SELECT * FROM Participants WHERE TeamId = @TeamId";

        public ParticipantRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            var result = await _dbConnection.ExecuteScalarAsync(IsEmailInUseSql, new {Email = email});
            return result is not null;
        }

        public async Task<bool> AreSignInDetailsValidAsync(string email, string token)
        {
            var result = await _dbConnection.ExecuteScalarAsync<int>(IsSignInDetailsValidSql, new {Email = email, Token = token});
            return result != 0;
        }

        public Task<Participant> GetAsync(string token) 
            => _dbConnection.QuerySingleAsync<Participant>(GetByTokenSql, new {Token = token});

        public async Task<List<Participant>> GetAllAsync(int teamId)
        {
            var result = await _dbConnection.QueryAsync<Participant>(GetAllByTeamIdSql, new {TeamId = teamId});
            return result.ToList();
        }
    }
}