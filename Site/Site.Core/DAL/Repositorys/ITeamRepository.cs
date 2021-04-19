using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public interface ITeamRepository
    {
        Task CreateTeamsAsync(Team team);

        Task<bool> IsTeamNameInUseAsync(string teamName);
    }
}