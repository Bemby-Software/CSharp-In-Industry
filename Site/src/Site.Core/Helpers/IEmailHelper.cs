using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Helpers
{
    public interface IEmailHelper
    {
        Task SendSignedUpEmailsAsync(List<Participant> participants);

        bool IsValidEmail(string email);
    }
}