using DevMatch.Models;

namespace DevMatch.Dtos.Rating
{
    public class RatingResponseDto
    {
        public int SessionId { get; set; }
        public int Nota { get; set; }
        public string? Comentario { get; set; }
    }
}
