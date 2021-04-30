using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Site.Core.DAL.Repositorys
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IDbConnection _dbConnection;
        public const string IsEmailInUseSql = "SELECT * FROM Participants WHERE Email = @Email";

        public const string IsSignInDetailsValid = @"SELECT COUNT(*) FROM Participants 
                                                        LEFT JOIN Tokens ON Participants.Id = ParticipantId
                                                        WHERE IsValid = 1
                                                        AND Participants.Email = @Email
                                                        AND [Value] = @Token";

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
            var result = await _dbConnection.ExecuteScalarAsync<int>(IsSignInDetailsValid, new {Email = email, Token = token});
            return result != 0;
        }
    }
}