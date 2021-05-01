using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.DAL.Repositorys
{
    public interface IParticipantRepository
    {
        Task<bool> IsEmailInUseAsync(string email);
        Task<bool> AreSignInDetailsValidAsync(string email, string token);
        Task<Participant> GetAsync(string token);
        Task<List<Participant>> GetAllAsync(int teamId);
    }
}