using DevMatch.Data;
using DevMatch.Dtos.MentorDto;
using DevMatch.Interfaces;
using DevMatch.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevMatch.Repository
{
    public class MentorRepository : IMentorRepository
    {
        private readonly ApplicationDbContext _context;
        public MentorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CarregarMentorProfile(User user)
        {
            var mentorprofile = await _context.MentorProfile.FirstOrDefaultAsync(x => x.UserId == user.Id);
            user.MentorProfile = mentorprofile;
            return user;
        }

        public async Task<bool> DeletarMentor(User user)
        {
            var mentor = await _context.MentorProfile.FirstOrDefaultAsync(x => x == user.MentorProfile);

            var deletar = _context.MentorProfile.Remove(mentor);

            await _context.SaveChangesAsync();

            return true;
        }

        public User UpdateMentor(RegisterProfileDto update, User user)
        {

            user.MentorProfile.TechStack = update.TechStack;
            user.MentorProfile.Bio = update.Bio;    
            user.MentorProfile.Disponibilidade = update.Disponibilidade;

            return user;
        }




    }
}
