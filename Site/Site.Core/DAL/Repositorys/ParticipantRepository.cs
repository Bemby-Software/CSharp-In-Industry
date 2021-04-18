using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Site.Core.DAL.Repositorys
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly IDbConnection _dbConnection;
        public const string IsEmailInUseSql = "SELECT * FROM Participants WHERE Email = @Email";

        public ParticipantRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            var result = await _dbConnection.ExecuteScalarAsync(IsEmailInUseSql, new {Email = email});
            return result is not null;
        }
    }
}