using DevMatch.Dtos.SessionDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using DevMatch.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/session")]
    public class SessionController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMentorRepository _mentorRepository;
        public SessionController(UserManager<User> usermanager, ISessionRepository sessionRepository, IMentorRepository mentorRepository)
        {
            _userManager = usermanager;
            _sessionRepository = sessionRepository;
            _mentorRepository = mentorRepository;
        }


        [Authorize(Roles = "Mentor")]
        [HttpPost("CreateSession")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionDto session)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var userCarregado = await _mentorRepository.CarregarMentorProfile(user);

            if (userCarregado == null)
                return NotFound();

            if (session.DataStart <= DateTime.Now)
                return BadRequest("A data não pode ser no passado.");

            var sessionCreate = new Session
            {
                DataStart = session.DataStart,
                DataEnd = session.DataEnd,
                Status = session.Status,
                Topico = session.Topico
            };

            user.SessionsComoMentor.Add(sessionCreate);

            var update = await _userManager.UpdateAsync(user);

            if (!update.Succeeded)
                return BadRequest(update.Errors);


            return StatusCode(201, session);
        }

        [Authorize]
        [HttpGet("GetMySessions")]

        public async Task<IActionResult> GetMySessions()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var userCarregado = await _sessionRepository.CarregarUser(user);
            if (userCarregado == null)
                return NotFound();

            var retornarListasDTO = _sessionRepository.ReturnSessionsDto(userCarregado);

            return Ok(retornarListasDTO.ToList());

        }


        [Authorize(Roles = "Mentor")]
        [HttpPatch("AtualizarSession/{id:int}")]
        public async Task<IActionResult> AtualizarSession([FromBody] CreateSessionDto session, [FromRoute] int id)

        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var userCarregado = await _sessionRepository.CarregarUser(user);
            if (userCarregado == null)
                return NotFound();

            var atualizarSession = await _sessionRepository.UpdateSession(userCarregado, id, session);

            if (atualizarSession == null)
                return NotFound();

            return StatusCode(200, atualizarSession);
        }


        [Authorize(Roles = "Mentor")]
        [HttpDelete("DeletarSession/{id:int}")]
        public async Task<IActionResult> DeletarSession([FromRoute] int id)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var userCarregado = await _sessionRepository.CarregarUser(user);
            if (userCarregado == null)
                return NotFound();

            var deletar = await _sessionRepository.DeleteSession(userCarregado, id);

            if (deletar == null)
                return NotFound();

            return Ok(deletar);
        }


        [Authorize]
        [HttpPost("ParticiparDeSession/{id:int}")]
        public async Task<IActionResult> ParticiparDeSesssion([FromRoute] int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var session = await _sessionRepository.GetSession(id);
            if (session == null)
                return NotFound();

            var inscreverNaSessao = await _sessionRepository.ParticiparDeUmaSession(user, session);
            if (inscreverNaSessao == null)
                return BadRequest("O usuario já esta inscrito na sessão.");

            return Ok(inscreverNaSessao);
        }


    }
}
