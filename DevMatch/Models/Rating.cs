using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class Rating
    {
        [Key]
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public int Nota { get; set; }
        public string? Comentario { get; set; } 
    }
}
