using API.Helpers;
using API.Models.Users;
using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UpdatedUserAPI = API.Models.Users.UpdatedUser;
using UpdatedUserBAL = BusinessLayer.Models.UpdatedUser;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private IConfiguration _cnf;
        private ILogger<UserController> _logger;
        public UserController(IUserService userService, IMapper mapper, IWebHostEnvironment hostEnvironment,
            IConfiguration configuration, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _env = hostEnvironment;
            _cnf = configuration;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> User(int id)
        {
            try
            {
                var user = _mapper.Map<FullUser>(_userService.GetUserWithStatisticById(id));
                var resourceHelper = new LocalResourceHelper(_env, _cnf);
                var file = await resourceHelper.GetResource(user.LogoPath);
                var responseUser = new UserResponse
                {
                    Login = user.Login,
                    Email = user.Email,
                    Logo = file,
                    Battles = user.Battles,
                    WinBattles = user.WinBattles,
                    WinRate = user.WinRate
                };
                return Ok(new { user = responseUser });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = "User profile not found" });
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> User(int id, [FromBody] UpdatedUserAPI updatedUser)
        {
            try
            {
                ReducedUser user = _userService.GetUserById(id);
                
                if (user == null)
                {
                    return NotFound(new { message = "User profile not found" });
                }

                LocalResourceHelper resourceHelper = new LocalResourceHelper(_env, _cnf);

                if (!resourceHelper.IsResourceValid(updatedUser.Logo))
                {
                    return BadRequest(new { message = "The file exceeds the allowed size of 100 kilobytes" });
                }

                string path = await resourceHelper.SaveResource(updatedUser.Logo, updatedUser.Login);

                UpdatedUserBAL userBAL = new UpdatedUserBAL
                {
                    Id = id,
                    Login = updatedUser.Login,
                    Email = updatedUser.Email,
                    PathToLogo = path
                };

                _userService.UpdateUser(userBAL);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
