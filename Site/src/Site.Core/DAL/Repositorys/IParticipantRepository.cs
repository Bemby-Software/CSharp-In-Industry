using System.Threading.Tasks;

namespace Site.Core.DAL.Repositorys
{
    public interface IParticipantRepository
    {
        Task<bool> IsEmailInUseAsync(string email);
        Task<bool> AreSignInDetailsValidAsync(string email, string token);
    }
}