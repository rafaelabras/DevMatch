using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DevMatch.Hubs
{
    public sealed class ChatHubs : Hub
    {
        private readonly IMessageRepository _messageRepository;
        public ChatHubs(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public override async Task OnConnectedAsync()
        {
            var user = Context.UserIdentifier;
            var sessionId = Context.GetHttpContext()?.Request.Query["sessionId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{user}");

            if (!string.IsNullOrEmpty(sessionId))
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{sessionId}");

            await Clients.Caller.SendAsync("ReceiveMessage", $"Usuario: {user} conectado!");

        }

        public async Task SendMessageToSession(string message, int sessionId)
        {
            var senderId = Context.UserIdentifier;

            var chatMessage = new ChatMessage
            {
                SessionId = sessionId,
                SenderId = senderId,
                Conteudo = message
            };

            await Clients.Group($"chat-{sessionId}").
                SendAsync("ReceiveMessage", chatMessage);

            await _messageRepository.SaveMessageAsync(chatMessage);
        }

    }
}
