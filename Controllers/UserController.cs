using DevMatch.Dtos.User;
using DevMatch.Dtos.UserDto;
using DevMatch.Enums;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/account")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(ITokenService tokenservice, UserManager<User> usermanager, SignInManager<User> signinmanager)
        {
            _tokenService = tokenservice;
            _userManager = usermanager;
            _signInManager = signinmanager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User
                {
                    UserName = register.Name,
                    Name = register.Name,
                    Email = register.Email
                };


                var createUser = await _userManager.CreateAsync(user, register.Password);
                if (createUser.Succeeded) {

                    var roleResult = await _userManager.AddToRoleAsync(user, "Mentorado");
                    if (roleResult.Succeeded)
                    {
                        var refreshToken = _tokenService.GenerateRereshToken(user);
                        await _tokenService.ValidateToken(refreshToken);

                        return Ok(new UserResponseDto

                        {
                            Name = user.UserName,
                            Email = user.Email,
                            Token = await _tokenService.GenerateToken(user),
                            RefreshToken = refreshToken
                        });
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }



                }
                else
                {
                    return BadRequest(createUser.Errors);
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                    return BadRequest("Email ou senha incorretos.");


                var result = await _signInManager.CheckPasswordSignInAsync(user, login.PasswordHash, false);
                if (!result.Succeeded)
                    return BadRequest("Email ou senha incorretos. ");

                return Ok(new UserResponseDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Token = await _tokenService.GenerateToken(user),
                    RefreshToken = _tokenService.GenerateRereshToken(user)
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [Authorize(Roles = "Mentorado")]
        [HttpPost("ChangeRole")]
        public async Task<IActionResult> ChangeRole()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                // if (email == null)
                //   return BadRequest("Token JWT não contém claim de email.");

                if (email == null)
                    return BadRequest();

                var usuario = await _userManager.FindByEmailAsync(email);

                await _userManager.RemoveFromRoleAsync(usuario, "Mentorado");
                var mudarRole = await _userManager.AddToRoleAsync(usuario, "Mentor");

                var refreshToken = _tokenService.GenerateRereshToken(usuario);
                if (mudarRole.Succeeded)
                {
                    await _tokenService.ValidateToken(refreshToken);
                    return StatusCode(200, new UserResponseDto
                    {
                        Name = usuario.Name,
                        Email = usuario.Email,
                        Token = await _tokenService.GenerateToken(usuario),
                        RefreshToken = refreshToken
                    });

                }
                else
                    return BadRequest("Nao foi possivel alterar sua role.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("ValidateToken")]
        public async Task<IActionResult> ValidateToken()
        {
            return null;
        }





    }
}
