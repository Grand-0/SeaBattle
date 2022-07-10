using API.HashProtection;
using API.Identity;
using API.Models.Users;
using BusinessLayer.Models;
using BusinessLayer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtGenerator _jwtGenerator;
        private IHashService _hashService;
        private IUserService _userService;
        public AuthController(IJwtGenerator jwtGenerator, IHashService hashService, IUserService userService)
        {
            _jwtGenerator = jwtGenerator;
            _hashService = hashService;
            _userService = userService;
        }

        [HttpPost("registeration")]
        public async Task<IActionResult> Registeration([FromForm] UserRegistration user)
        {
            string token = "";

            try
            {
                bool isUserExist = _userService.IsUserExist(user.Login, user.Email);

                if (!isUserExist)
                {
                    var hash = _hashService.HashPassword(user.Password);

                    try
                    {
                        _userService.CreateUser(new ReducedUser
                        {
                            Id = null,
                            Login = user.Login,
                            Email = user.Email,
                            PasswordHash = hash.HashResult,
                            IndividualSalt = hash.UniqueGuid,
                            Logo = null
                        });

                        token = _jwtGenerator.CreateToken(user);
                    }
                    catch(Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                int userId =_userService.GetUserIdByLogin(user.Login);

                return Ok(new { 
                    access_token = token,
                    user_id = userId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password)
        {
            try
            {
                ReducedUser user = _userService.GetUserByLogin(login);

                if (_hashService.isHashEquals(password, user.IndividualSalt, user.PasswordHash))
                {
                    string token = _jwtGenerator.CreateToken(new UserBase { Login = user.Login, Email = user.Email });
                    return Ok(new {
                        access_token = token,
                        user_id = user.Id
                    });
                }
                else
                {
                    return Unauthorized("Неверный логин или пароль!");
                }
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            _userService.Test();

            return Ok();
        }
    }
}
