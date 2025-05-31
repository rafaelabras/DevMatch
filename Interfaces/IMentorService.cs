using DevMatch.Models;

namespace DevMatch.Interfaces
{
    public interface IMentorService
    {
        public bool VerifyMentorExiste(User user);
    }
}
