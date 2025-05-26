using DevMatch.Dtos.User;
using DevMatch.Dtos.UserDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/account")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;

        public UserController(ITokenService tokenservice)
        {
            _tokenService = tokenservice;
        }


        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDto register)
        {
            var user = new User
            {
                Name = register.Name,
                Email = register.Email,
                PasswordHash = register.Password
            };

            return Ok(new UserResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            });
        }


        [HttpPost("Refresh-Token")]



    }
}
