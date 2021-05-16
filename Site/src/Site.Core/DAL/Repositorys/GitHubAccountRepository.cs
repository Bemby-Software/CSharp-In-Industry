using System.Data;
using System.Threading.Tasks;
using Dapper;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public class GitHubAccountRepository : IGitHubAccountRepository
    {
        private readonly IDbConnection _dbConnection;
        private const string CreateSql = @"INSERT INTO GitHubAccounts (Repository) VALUES (@Repository); SELECT SCOPE_IDENTITY();";
        private const string GetByIdSql = "SELECT * FROM GitHubAccounts WHERE Id = @Id";
        private const string UpdateSql = "UPDATE GitHubAccounts SET IssuesCopied = @IssuesCopied, IsIssueCopyComplete = @IsIssueCopyComplete WHERE Id = @Id";

        public GitHubAccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public Task<GitHubAccount> GetAsync(int id)
        {
            return _dbConnection.QuerySingleAsync<GitHubAccount>(GetByIdSql, new {Id = id});
        }

        public Task UpdateAsync(GitHubAccount account)
        {
            return _dbConnection.ExecuteAsync(UpdateSql, account);
        }

        public Task<int> CreateAsync(GitHubAccount account)
        {
            return _dbConnection.QuerySingleAsync<int>(CreateSql, account);
        }
    }
}