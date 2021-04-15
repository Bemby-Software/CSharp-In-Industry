using System.Threading.Tasks;

namespace Site.Core.DAL.Repositorys
{
    public class ParticipantRepository : IParticipantRepository
    {
        public Task<bool> IsEmailInUseAsync(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}