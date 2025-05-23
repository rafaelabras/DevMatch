using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class Session
    {
 
        public int MentorId { get; set; }
        public User Mentor { get; set; } = null!;

        public int MentoradoId { get; set; }
        public User Mentorado { get; set; } = null!;

        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Topico { get; set; } = null!;
        public string Status { get; set; } = null!;

        public ICollection<ChatMessage> Mensagens { get; set; } = [];
        public Rating? Rating { get; set; } = null!;
    }
}
