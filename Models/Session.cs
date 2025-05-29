
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DevMatch.Models
{
    public class Session
    {
 
        public string MentorId { get; set; }
        public User Mentor { get; set; } = null!;

        [AllowNull]
        public string? MentoradoId { get; set; }
        [AllowNull]
        public User? Mentorado { get; set; } = null!;

        public int Id { get; set; }
        public DateTime DataStart { get; set; } = DateTime.Now;
        public DateTime DataEnd {  get; set; } = DateTime.Now.AddMinutes(30);
        public string Topico { get; set; } = null!;
        public string Status { get; set; } = null!;

        public ICollection<ChatMessage> Mensagens { get; set; } = [];
        public Rating? Rating { get; set; } = null!;
    }
}
