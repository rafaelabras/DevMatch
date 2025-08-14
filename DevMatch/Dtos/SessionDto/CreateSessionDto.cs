using DevMatch.Models;

namespace DevMatch.Dtos.SessionDto
{
    public class CreateSessionDto
    {
        public DateTime DataStart { get; set; } = DateTime.Now;
        public DateTime DataEnd { get; set; } = DateTime.Now;
        public string Topico { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
