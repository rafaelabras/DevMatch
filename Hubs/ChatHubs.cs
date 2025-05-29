using DevMatch.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DevMatch.Hubs
{
    public sealed class ChatHubs : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync( $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync($"{Context.ConnectionId}: {message}");
        }

    }
}
