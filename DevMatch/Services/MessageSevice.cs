using DevMatch.Dtos.MessageDto;
using DevMatch.Hubs;
using DevMatch.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DevMatch.Services
{
    public class MessageSevice : IMessageService
    {
        public async Task EnviarMensagem(ConnectionDto dto, IHubContext<ChatHubs> context)
        {
            await context.Clients.All.SendAsync("Receive", dto.Message);
        }
    }
}
