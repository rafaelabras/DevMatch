using DevMatch.Dtos.MentorDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using DevMatch.Repository;
using DevMatch.Interfaces;
using Microsoft.Win32;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/mentor")]
    public class MentorController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMentorRepository _mentorRepository;
        private readonly IMentorService _mentorService;
        public MentorController(UserManager<User> usermanager, IMentorRepository mentorRepository, IMentorService mentorservice)
        {
            _userManager = usermanager;
            _mentorRepository = mentorRepository;
            _mentorService = mentorservice;
        }
      

        [Authorize(Roles = "Mentor")]
        [HttpPost("ProfileCreate")]
        public async Task<IActionResult> ProfileCreate([FromBody] RegisterProfileDto register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);
            
            var verificarMentor = _mentorService.VerifyMentorExiste(user);

            if (verificarMentor == true)
                return BadRequest("O perfil de mentor já existe.");

            var Profile = new MentorProfile
            {
                Bio = register.Bio,
                Disponibilidade = register.Disponibilidade,
                TechStack = register.TechStack,
            };

            user.MentorProfile = Profile;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return StatusCode(201, new RegisterProfileResponseDto
            {
                MentorName = user.Name,
                Bio = register.Bio,
                TechStack = register.TechStack,
                Disponibilidade=register.Disponibilidade
            });

        }

        [Authorize(Roles = "Mentor")]
        [HttpPatch("UpdateProfile")]

        public async Task<IActionResult> UpdateProfile([FromBody] RegisterProfileDto update)
        {
         
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return NotFound();

            var user = await _userManager.FindByEmailAsync(email);

            // Devido ao entityframework não navegar entre propriedades de navegação e o lazy loading estar desativado, basta usar o eager loading com .include.
            var userCarregado = await _mentorRepository.CarregarMentorProfile(user);
            
            var atualizar =  _mentorRepository.UpdateMentor(update, userCarregado);

            var updateuser = await _userManager.UpdateAsync(user);

            if (!updateuser.Succeeded)
                return BadRequest(updateuser.Errors);

            return StatusCode(202, new RegisterProfileResponseDto
            {
                MentorName = user.Name,
                Bio = update.Bio,
                TechStack = update.TechStack,
                Disponibilidade = update.Disponibilidade
            });

        }

        [Authorize(Roles = "Mentor")]
        [HttpDelete("DeletarProfileMentor")]
        public async Task<IActionResult> DeletarProfileMentor()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var verificarMentor = _mentorService.VerifyMentorExiste(user);

            if (verificarMentor == false)
                return BadRequest("O perfil de mentor não existe.");

            var deletar = await _mentorRepository.DeletarMentor(user);

            if (deletar == true)
                return Ok();

            return BadRequest();
        }

    }
}
