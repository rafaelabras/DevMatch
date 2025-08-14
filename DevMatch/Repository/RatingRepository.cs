using DevMatch.Data;
using DevMatch.Dtos.Rating;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevMatch.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;
        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RatingResponseDto> AtualizarRating(RatingResponseDto rating, User user)
        {
            var usersession = user.SessionsComoMentorado.FirstOrDefault(x => x.Id == rating.SessionId);

            if (usersession == null || usersession.Rating == null)
                return null; 

            usersession.Rating.Nota = rating.Nota;
            usersession.Rating.Comentario = rating.Comentario;

            await _context.SaveChangesAsync();

            return rating;
        }

        public async Task<RatingResponseDto> DarNota(User user,int id, AtribuirRatingDto nota)
        {
            var usersession = user.SessionsComoMentorado.FirstOrDefault(x => x.Id == id);

            if (usersession.Rating != null)
                return null;

            var rating = new Rating
            {
                Session = usersession,
                SessionId = usersession.Id,
                Nota = nota.Nota,
                Comentario = nota.Comentario,
            };

            usersession.Rating = rating;

            var salvar = await _context.SaveChangesAsync();

            return new RatingResponseDto
            {
                SessionId = rating.SessionId,
                Nota = rating.Nota,
                Comentario = rating.Comentario
            };


        }

        public async Task<bool> DeletarRating(User user, int id)
        {
            var session = user.SessionsComoMentorado.FirstOrDefault(x => x.Id == id);

            if (session == null)
                return false;

            var rating = session.Rating;

            if (session.Rating == null)
                return false;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
