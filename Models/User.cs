using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Session> SessionsComoMentor { get; set; } = [];
        public ICollection<Session> SessionsComoMentorado { get; set; } = [];
    }
}
