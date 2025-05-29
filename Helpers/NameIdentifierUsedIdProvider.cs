using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevMatch.Helpers
{
    public class NameIdentifierUsedIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
