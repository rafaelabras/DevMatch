﻿using DevMatch.Dtos.MessageDto;
using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync (ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetAllMessagesAsync(string userId);
        Task<ICollection<MessageResponseDto>> CarregarMensagens(User user, int SessionId);
    }
}
