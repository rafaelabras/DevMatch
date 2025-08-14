using DevMatch.Models;

namespace DevMatch.Dtos.SessionDto
{
    public class SessionResponseDto
    {
        public int Id { get; set; }
        public DateTime DataStart { get; set; } = DateTime.Now;
        public DateTime DataEnd { get; set; } = DateTime.Now.AddMinutes(30);
        public string Topico { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
