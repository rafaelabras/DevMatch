using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace DevMatch.Dtos.UserDto
{
    public class UserResponseDto
    {
        public string Name { get; set; } = null!;
        
        public string Email { get; set; } = null!;
        
        public string Token { get; set; } = null!;
    }
}
