using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Conversions;
using Site.Core.DTO.Requests;
using Site.Core.DTO.Responses;
using Site.Core.Services;

namespace Site.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            await _participantService.SignInAsync(signInRequest.Email, signInRequest.Token);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetParticipantResponse>> Get([FromQuery] string token, [FromQuery] bool includeTeam = false)
        {
            var participant = await _participantService.GetByTokenAsync(token, includeTeam);
            return Ok(participant.AsGetParticipantResponse());
        }
    }
}