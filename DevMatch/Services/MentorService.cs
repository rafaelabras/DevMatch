using DevMatch.Interfaces;
using DevMatch.Models;

namespace DevMatch.Services
{
    public class MentorService : IMentorService
    {
        public bool VerifyMentorExiste(User user)
        {
            if (user.MentorProfile != null)
                return true;

            return false;
        }
    }
}
