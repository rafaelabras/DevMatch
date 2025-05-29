using Microsoft.AspNetCore.SignalR;

namespace DevMatch.Hubs
{
    public sealed class ChatHubs : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

    }
}
