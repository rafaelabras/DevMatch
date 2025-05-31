using DevMatch.Dtos.Rating;
using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface IRatingService
    {
        public AtribuirRatingDto VerificarRating(AtribuirRatingDto rating);

        public RatingResponseDto VerificarRatingNaSession(RatingResponseDto rating, User user);

        public bool VerificarExistenciaRating();
    }
}
