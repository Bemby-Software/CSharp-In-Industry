using System.Threading.Tasks;

namespace Site.Core.DAL.Repositorys
{
    public interface IParticipantRepository
    {
        Task<bool> IsEmailInUseAsync(string email);
    }
}