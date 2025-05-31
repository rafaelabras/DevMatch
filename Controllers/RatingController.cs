using DevMatch.Dtos.Rating;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/rating")]
    public class RatingController : Controller
    {

        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingService _ratingService;
        private readonly UserManager<User> _userManager;
        private readonly ISessionRepository _sessionRepository;
        public RatingController(IRatingRepository ratingRepository, IRatingService ratingService, UserManager<User> userManager, ISessionRepository sessionRepository)
        {   
            _ratingRepository = ratingRepository;
            _ratingService = ratingService;
            _userManager = userManager;
            _sessionRepository = sessionRepository;
        }


        [Authorize]
        [HttpPost("AvaliarSession{id:int}")]
        public async Task<IActionResult> AvaliarSession([FromRoute] int id,[FromBody] AtribuirRatingDto nota)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var carregarUser= await _sessionRepository.CarregarUser(user);
            if (carregarUser == null)
                return NotFound();

            var VerficarUserSession = _sessionRepository.VerificarUserSession(carregarUser, id);

            if (VerficarUserSession == null)
                return NotFound();

            var verificarnota = _ratingService.VerificarRating(nota);
            if (verificarnota == null)
                return BadRequest("Houve um erro na sintaxe da nota.");

            var AddNota = await _ratingRepository.DarNota(user, id, verificarnota);
            if (AddNota == null)
                return BadRequest("Vc ja deu uma nota para essa sessão.");

            return Ok(AddNota);
        }

        [Authorize]
        [HttpPatch("AtualizarNota")]
        public async Task<IActionResult> AtualizarNota([FromBody] RatingResponseDto rating)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            var carregarUser = await _sessionRepository.CarregarUser(user);
            if (carregarUser == null)
                return NotFound();

            var verificar = _ratingService.VerificarRatingNaSession(rating, carregarUser);

            if (verificar == null)
                return BadRequest();

            var atualizar = await _ratingRepository.AtualizarRating(rating, carregarUser);

            return Ok(atualizar);

        }

    }
}
