using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class MentorProfile
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string TechStack { get; set; } = null!;
        public string Disponibilidade { get; set; } = null!;
    }
}
