using System;
using System.Reflection;
using System.Security.Authentication;
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
        private readonly ITeamRepository _teamRepository;

        public ParticipantService(IEmailHelper emailHelper, IParticipantRepository participantRepository, ITeamRepository teamRepository)
        {
            _emailHelper = emailHelper;
            _participantRepository = participantRepository;
            _teamRepository = teamRepository;
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

        public async Task SignInAsync(string email, string token)
        {
            if (!await _participantRepository.AreSignInDetailsValidAsync(email, token))
                throw new ParticipantsSignInDetailInvalidException();
        }

        public async Task<Participant> GetByTokenAsync(string token, bool includeTeam)
        {
            var participant = await _participantRepository.GetAsync(token);

            if (participant is null)
                throw new InvalidCredentialException();

            if (includeTeam)
            {
                var team = await _teamRepository.GetAsync(participant.TeamId);
                team.Participants = await _participantRepository.GetAllAsync(team.Id);
                participant.Team = team;
            }

            return participant;
        }
    }
}