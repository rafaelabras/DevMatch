using DevMatch.Data;
using DevMatch.Dtos.Rating;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.EntityFrameworkCore;

namespace DevMatch.Services
{
    public class RatingService : IRatingService
    {
        private readonly ISessionRepository _sessionRepository;
        public RatingService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public bool VerificarExistenciaRating()
        {
            throw new NotImplementedException();
        }

        public AtribuirRatingDto VerificarRating(AtribuirRatingDto rating)
        {
            if (rating.Nota > 6 || rating.Nota < 0)
                return null;

            var comentario = rating.Comentario;

            if (String.IsNullOrWhiteSpace(comentario) == true)
                return null;

            return rating;
        }

        public RatingResponseDto VerificarRatingNaSession(RatingResponseDto rating, User user)
        {
            var verificar = VerificarRating(new AtribuirRatingDto 
            { Nota = rating.Nota, Comentario = rating.Comentario });

            if (verificar == null)
                return null;

            var usersession = user.SessionsComoMentorado.FirstOrDefault(x => x.Id == rating.SessionId);
            if (usersession == null)
                return null;

            if (usersession.Rating == null)
                return null;

            return rating;
        }
    }
}
