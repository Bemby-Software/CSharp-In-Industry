using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Services;

namespace Site.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IParticipantService _participantService;

        public ValidationController(ITeamService teamService, IParticipantService participantService)
        {
            _teamService = teamService;
            _participantService = participantService;
        }

        [HttpGet("IsEmailInUse/{email}")]
        public async Task<IActionResult> IsEmailInUse([FromRoute] string email)
        {
            return Ok(await _participantService.IsEmailInUseAsync(email));
        }
        
        [HttpGet("IsTeamNameInUse/{name}")]
        public async Task<IActionResult> IsTeamNameInUse([FromRoute] string name)
        {
            return Ok(await _teamService.IsTeamNameInUseAsync(name));
        }
    }
}