using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Core.Conversions;
using Site.Core.DTO.Requests;
using Site.Core.Services;

namespace Site.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Post([FromBody] CreateTeamRequest request)
        {
            await _teamService.CreateAsync(request.AsEntity());
            return Ok();
        }
    }
}