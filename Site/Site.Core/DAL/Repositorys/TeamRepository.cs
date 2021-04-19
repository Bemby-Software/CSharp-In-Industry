using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Site.Core.DAL.Transactions;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ISignUpTransaction _signUpTransaction;
        private readonly IDbConnection _dbConnection;

        public TeamRepository(ISignUpTransaction signUpTransaction, IDbConnection dbConnection)
        {
            _signUpTransaction = signUpTransaction;
            _dbConnection = dbConnection;
        }
        
        public Task CreateTeamsAsync(Team team) 
            => _signUpTransaction.SignUpAsync(team);

        public async Task<bool> IsTeamNameInUseAsync(string teamName)
        {
            var team = await _dbConnection.QuerySingleOrDefaultAsync<Team>("SELECT * FROM Teams WHERE Name = @Name", new {Name = teamName});
            return team is not null;
        }
    }
}