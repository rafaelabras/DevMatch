namespace DevMatch.Dtos.MessageDto
{
    public class MessageNotify
    {
        public int SessionId { get; set; }
        public string SenderId { get; set; }
        public string Conteudo { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
