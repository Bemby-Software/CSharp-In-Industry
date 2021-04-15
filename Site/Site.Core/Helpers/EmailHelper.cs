using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Site.Core.Entities;

namespace Site.Core.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        public Task SendSignedUpEmailsAsync(List<Participant> participants)
        {
            return Task.CompletedTask;
        }

        public bool IsValidEmail(string email)
        {
            var validator = new EmailAddressAttribute();
            return validator.IsValid(email);
        }
    }
}