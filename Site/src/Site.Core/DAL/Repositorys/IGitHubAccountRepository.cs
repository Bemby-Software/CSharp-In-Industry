using System.Threading.Tasks;
using Site.Core.Apis;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public interface IGitHubAccountRepository
    {
        Task<GitHubAccount> GetAsync(int id);

        Task UpdateAsync(GitHubAccount account);

        Task<int> CreateAsync(GitHubAccount account);
    }
}