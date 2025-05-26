using System.ComponentModel.DataAnnotations;

namespace DevMatch.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; }

    }
}
