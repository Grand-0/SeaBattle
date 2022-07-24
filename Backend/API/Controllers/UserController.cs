using API.Helpers;
using API.Models.Users;
using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
                var file = resourceHelper.GetResource(user.LogoPath);
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
        public async Task<IActionResult> User(int id, UpdateUserModel updateData)
        {
            try
            {
                if (updateData.Login != null)
                {
                    if (updateData.Email != null)
                    {
                        _userService.UpdateUserLoginAndEmail(id, updateData.Login, updateData.Email);
                    }
                    else
                    {
                        _userService.UpdateUserLogin(id, updateData.Login);
                    }
                }
                else
                {
                    if (updateData.Email != null)
                    {
                        _userService.UpdateUserEmail(id, updateData.Email);
                    }
                    else
                    {
                        throw new Exception("All fields has value null!");
                    }
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost("{id}/img")]
        public async Task<IActionResult> User(int id, [FromBody]IFormFile file)
        {
            try
            {
                var identity = HttpContext.User.Identity;
                string userLogin = TokenHelper.GetClaim(identity, "Name");

                LocalResourceHelper helper = new LocalResourceHelper(_env, _cnf);
                var path = await helper.SaveResource(file, userLogin);
                _userService.UpdateUserLogo(id, path.LocalPath);

                return Ok(new { pathToLogo = path.FullPath });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
