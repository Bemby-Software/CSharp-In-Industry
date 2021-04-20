using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Services
{
    public interface IParticipantService
    {
        Task ValidateParticipant(Participant participant);
        Task<bool> IsEmailInUseAsync(string email);
    }
}