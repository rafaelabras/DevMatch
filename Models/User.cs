using DevMatch.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevMatch.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRolesEnum Role { get; set; }
        public MentorProfile? MentorProfile { get; set; }

        public ICollection<Session> SessionsComoMentor { get; set; } = [];
        public ICollection<Session> SessionsComoMentorado { get; set; } = [];
        public ICollection<ChatMessage> MensagensEnviadas { get; set; } = [];
    }
}
