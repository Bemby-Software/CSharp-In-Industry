using System.Threading.Tasks;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions.General;
using Site.Core.Exceptions.Participants;
using Site.Core.Helpers;

namespace Site.Core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IEmailHelper _emailHelper;
        private readonly IParticipantRepository _participantRepository;

        public ParticipantService(IEmailHelper emailHelper, IParticipantRepository participantRepository)
        {
            _emailHelper = emailHelper;
            _participantRepository = participantRepository;
        }
        
        public async Task ValidateParticipant(Participant participant)
        {
            if (participant.Forename.IsEmpty())
                throw new ParticipantForenameRequiredException();

            if (participant.Surname.IsEmpty())
                throw new ParticipantSurnameRequiredException();

            if (!_emailHelper.IsValidEmail(participant.Email))
                throw new InvalidEmailException();

            if (await _participantRepository.IsEmailInUseAsync(participant.Email))
                throw new EmailInUseException();

        }

        public async Task IsEmailInOkAsync(string email)
        {
            if (!_emailHelper.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }
            
            if (await _participantRepository.IsEmailInUseAsync(email))
            {
                throw new EmailInUseException();
            }
        }
    }
}