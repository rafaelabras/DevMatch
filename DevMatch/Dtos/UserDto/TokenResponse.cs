using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DevMatch.Dtos.UserDto
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
