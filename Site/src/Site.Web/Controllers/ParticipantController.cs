using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Core.DTO.Requests;
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
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest)
        {
            await _participantService.SignInAsync(signInRequest.Email, signInRequest.Token);
            return Ok();
        }
    }
}