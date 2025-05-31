using DevMatch.Dtos.MessageDto;
using DevMatch.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DevMatch.Interfaces
{
    public interface IMessageService
    {
        Task EnviarMensagem(ConnectionDto dto, IHubContext<ChatHubs> context);
    }
}
