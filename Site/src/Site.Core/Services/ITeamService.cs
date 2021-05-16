using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Services
{
    public interface ITeamService
    {
        Task CreateAsync(Team team);
        Task<bool> IsTeamNameInUseAsync(string name);
        Task<bool> IsValid(int teamId);
    }
}   