using System;
using System.Threading.Tasks;
using Site.Core.DAL.Transactions;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ISignUpTransaction _signUpTransaction;

        public TeamRepository(ISignUpTransaction signUpTransaction)
        {
            _signUpTransaction = signUpTransaction;
        }
        
        public Task CreateTeamsAsync(Team team) 
            => _signUpTransaction.SignUpAsync(team);
    }
}