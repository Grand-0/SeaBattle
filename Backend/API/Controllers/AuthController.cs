using API.Identity;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtGenerator _jwtGenerator { get; }
        public AuthController(IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("registeration")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var token = _jwtGenerator.CreateToken(user);
            return Ok(token);
        }
    }
}
