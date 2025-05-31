using DevMatch.Data;
using DevMatch.Dtos.SessionDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Threading.Tasks;

namespace DevMatch.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;
        
        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> CarregarUser(User user)
        {
            if (user.SessionsComoMentor == null)
                return null;

            var mentorprofile = await _context.MentorProfile.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (mentorprofile == null)
                return null;

            var session = await _context.Sessions.Where(x => x.MentorId == user.Id).ToListAsync();

            if (session == null)
                return null;

            user.MentorProfile = mentorprofile;
            user.SessionsComoMentor = session;

            return user;
        }


        public ICollection<SessionResponseDto> ReturnSessionsDto(User user)
        {

            var sessoes = user.SessionsComoMentor.ToList();
            ICollection<SessionResponseDto> sessionsDeResposta = new List<SessionResponseDto>();

            foreach (var s in sessoes)
            {
                sessionsDeResposta.Add(new SessionResponseDto
                {
                    Id = s.Id,
                    Status = s.Status,
                    Topico = s.Topico,
                    DataStart = s.DataStart,
                    DataEnd = s.DataEnd
                });
            }

            return sessionsDeResposta;
        }

        public async Task<SessionResponseDto> UpdateSession(User user, int id, CreateSessionDto session)
        {

            var encontrar = await _context.Sessions.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (encontrar == null || encontrar.MentorId != user.Id)
                return null;


                encontrar.Status = session.Status;
                encontrar.Topico = session.Topico;
                encontrar.DataStart = session.DataStart;
                encontrar.DataEnd = session.DataEnd;

                await _context.SaveChangesAsync();

                return new SessionResponseDto
                {
                    Id = encontrar.Id,
                    Status = encontrar.Status,
                    Topico = encontrar.Topico,
                    DataStart = encontrar.DataStart,
                    DataEnd = encontrar.DataEnd
                };

        }

        public async Task<SessionResponseDto> DeleteSession(User user, int id)
        {
            var encontrar = await _context.Sessions.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (encontrar == null || encontrar.MentorId != user.Id)
                return null;

            var remove = _context.Sessions.Remove(encontrar);

            await _context.SaveChangesAsync();

            return new SessionResponseDto
            {
                Id = id,
                Status = encontrar.Status,
                Topico = encontrar.Topico,
                DataStart = encontrar.DataStart,
                DataEnd = encontrar.DataEnd
            };

        }

        public User VerificarUserSession(User user, int sessionId)
        {

            var userEstarNaSession = user.SessionsComoMentorado.FirstOrDefault(x => x.Id == sessionId);
            if (userEstarNaSession == null)
                return null;

            return user;

        }

        public async Task<Session> GetSession(int sessionId)
        {
            var acharSession = await _context.Sessions.FirstOrDefaultAsync(x => x.Id == sessionId);

            if (acharSession == null)
                return null;

            return acharSession;
        }

        public async Task<SessionResponseDto> ParticiparDeUmaSession(User user, Session session)
        {
            if (user.SessionsComoMentorado.Contains(session))
                return null;

            user.SessionsComoMentorado.Add(session);
            await _context.SaveChangesAsync();

            return new SessionResponseDto
            {
                Id = session.Id,
                Status = session.Status,
                Topico = session.Topico,
                DataStart = session.DataStart,
                DataEnd = session.DataEnd
            };
        }
    }
}
