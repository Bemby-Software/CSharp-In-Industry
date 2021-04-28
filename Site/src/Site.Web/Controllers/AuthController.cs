using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Core.DTO.Requests;
using Site.Core.Entities;
using Site.Core.Exceptions.General;

namespace Site.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            await Task.CompletedTask;
            _logger.LogInformation($"{request.Email} and {request.Token}");
            throw new EmailInUseException();
            return Ok();
        }
    }
}