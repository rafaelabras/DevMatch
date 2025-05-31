using DevMatch.Dtos.Rating;
using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface IRatingRepository
    {
        public Task<RatingResponseDto> DarNota(User user, int id,AtribuirRatingDto nota);
        public Task<RatingResponseDto> AtualizarRating(RatingResponseDto rating, User user);
    }
}
