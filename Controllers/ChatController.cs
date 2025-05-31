using DevMatch.Dtos.MessageDto;
using DevMatch.Hubs;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/chat")]
    public class ChatController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<ChatHubs> _hubContext;
 
        public ChatController(IMessageRepository messageRepository, UserManager<User> userManager, IHubContext<ChatHubs> hubContext)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("MensagensDaSession{id:int}")]
        public async Task <IActionResult> MensagensDaSession([FromRoute] int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);
            var mensagens = _messageRepository.CarregarMensagens(user, id);
            if (mensagens == null)
                return NotFound();

            return Ok(mensagens);
        }

        [Authorize(Roles = "Mentor,Admin")]
        [HttpPost("NotificarSession")]

        public async Task <IActionResult> NotificarSession ([FromBody] ConnectionDto connection)
        {
            if (connection == null || string.IsNullOrWhiteSpace(connection.Message))
                return BadRequest("Mensagem inválida.");


            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            var id = connection.SessionId;

            await _hubContext.Clients.Group($"chat-{id}").SendAsync("ReceiveSystemMessage", new MessageNotify
            {
                SenderId = user.Id,
                Conteudo = connection.Message,
                Timestamp = DateTime.UtcNow
            });

            return Ok($"Mensagem enviada para a sessão {connection.SessionId}.");

        }



         
        
    }
}
