using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Core.DTO.Requests;
using Site.Core.Services;

namespace Site.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubAccountService _gitHubAccountService;

        public GitHubController(IGitHubAccountService gitHubAccountService)
        {
            _gitHubAccountService = gitHubAccountService;
        }

        [HttpPut]
        public async Task<IActionResult> Assign([FromBody] AssignTeamRepositoryRequest request)
        {
            await _gitHubAccountService.Assign(request.Repository, request.TeamId);
            return Ok();
        }
    }
}