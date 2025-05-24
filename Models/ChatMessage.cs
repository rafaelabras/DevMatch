using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int SessionId { get; set; } 
        public int SenderId { get; set; } 
        public Session Session { get; set; } = null!;
        public User Sender { get; set; } = null!;
        public string Conteudo { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
