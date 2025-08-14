using DevMatch.Data;
using DevMatch.Dtos.MessageDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace DevMatch.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<MessageResponseDto>> CarregarMensagens(User user, int SessionId)
        {
            var encontrar = await _context.Sessions.Where(x => x.Id == SessionId).FirstOrDefaultAsync();

            if (encontrar == null)
                return null;

            var mensagens = encontrar.Mensagens;

            if (mensagens == null)
                return null;
            
            ICollection<MessageResponseDto> messageDto = new List<MessageResponseDto>();

            foreach (var m in mensagens)
            {
                if (m.SenderId == user.Id){
                    messageDto.Add(new MessageResponseDto
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        Conteudo = m.Conteudo,
                        SessionId = m.SessionId,
                        Timestamp = m.Timestamp
                    });

                }
            }

            return messageDto;

        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessagesAsync(string userId)
        {
            var userMensagens = await _context.ChatMensagens.Where(x =>
            x.SenderId == userId
            ).ToListAsync();

            return userMensagens;
        }

        public async Task SaveMessageAsync(ChatMessage message)
        {
            var sessionId = message.SessionId;

            var session = await _context.Sessions.FirstOrDefaultAsync(x => x.Id == sessionId);

            if (session == null)
                throw new Exception("Sessão não encontrada.");

            session.Mensagens.Add(message);

            await _context.SaveChangesAsync();
        }
    }
}
