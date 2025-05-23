using DevMatch.Models;
using Microsoft.EntityFrameworkCore;

namespace DevMatch.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<MentorProfile> MentorProfile { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ChatMessage> ChatMensagens { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
