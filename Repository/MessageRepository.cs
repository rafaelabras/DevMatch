using DevMatch.Data;
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
