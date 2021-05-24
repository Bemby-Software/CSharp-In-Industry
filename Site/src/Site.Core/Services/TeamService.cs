using System.Linq;
using System.Threading.Tasks;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Core.Exceptions;
using Site.Core.Exceptions.Participants;
using Site.Core.Exceptions.Teams;
using Site.Core.Factories;
using Site.Core.Helpers;

namespace Site.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly ITokenFactory _tokenFactory;
        private readonly IEmailHelper _emailHelper;
        private readonly IParticipantService _participantService;

        public TeamService(ITeamRepository repository, ITokenFactory tokenFactory, 
            IEmailHelper emailHelper, IParticipantService participantService)
        {
            _repository = repository;
            _tokenFactory = tokenFactory;
            _emailHelper = emailHelper;
            _participantService = participantService;
        }
        
        public async Task CreateAsync(Team team)
        {
            if (team.Name.IsEmpty())
                throw new TeamNameRequiredException();

            if (await _repository.IsTeamNameInUseAsync(team.Name))
                throw new TeamNameInUseException();
            
            if (team.Participants.IsNullOrEmpty())
                throw new ParticipantsRequiredException();

            foreach (var participant in team.Participants)
                await _participantService.ValidateParticipant(participant);
            
            team.Participants.ForEach(x => x.Token = _tokenFactory.Create());

            await _repository.CreateTeamsAsync(team);

            await _emailHelper.SendSignedUpEmailsAsync(team.Participants);
        }

        public Task<bool> IsTeamNameInUseAsync(string name) 
            => _repository.IsTeamNameInUseAsync(name);

        public async Task<bool> IsValid(int teamId)
        {
            var team = await _repository.GetAsync(teamId);
            return team is not null;
        }
    }
}