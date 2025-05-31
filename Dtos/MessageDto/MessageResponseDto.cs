using DevMatch.Models;

namespace DevMatch.Dtos.MessageDto
{
    public class MessageResponseDto
    {
        public int Id { get; set; }
        public int SessionId { get; set;  }
        public string SenderId { get; set; }
        public string Conteudo { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
