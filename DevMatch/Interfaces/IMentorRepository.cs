using DevMatch.Dtos.MentorDto;
using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface IMentorRepository
    {
        public User UpdateMentor (RegisterProfileDto update, User user);
        public Task<User> CarregarMentorProfile (User user);
        public Task<bool> DeletarMentor(User user);
    }
}
