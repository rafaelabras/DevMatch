using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class MentorProfile
    {
        [Key]
        public string UserId { get; set; } = null;
        public User User { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string TechStack { get; set; } = null!;
        public string Disponibilidade { get; set; } = null!;
    }
}
