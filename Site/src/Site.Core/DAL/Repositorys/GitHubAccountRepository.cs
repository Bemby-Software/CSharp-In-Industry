using System.Data;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public class GitHubAccountRepository : IGitHubAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public GitHubAccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public Task<GitHubAccount> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(GitHubAccount account)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateAsync(GitHubAccount account)
        {
            throw new System.NotImplementedException();
        }
    }
}