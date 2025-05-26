using DevMatch.Models;
using Microsoft.EntityFrameworkCore;

namespace DevMatch.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<MentorProfile> MentorProfile { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ChatMessage> ChatMensagens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MentorProfile>().
                HasOne(p => p.User)
                .WithOne(u => u.MentorProfile)
                .HasForeignKey<MentorProfile>(i => i.UserId);


            modelBuilder.Entity<Session>()
                .HasOne(mentor => mentor.Mentor)
                .WithMany(u => u.SessionsComoMentor)
                .HasForeignKey(id => id.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(mentorado => mentorado.Mentorado)
                .WithMany(u => u.SessionsComoMentorado)
                .HasForeignKey(fk => fk.MentoradoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(r => r.Rating)
                .WithOne(r => r.Session)
                .HasForeignKey<Rating>(fk => fk.SessionId);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(u => u.Session)
                .WithMany(u => u.Mensagens)
                .HasForeignKey(fk => fk.SessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(u => u.Sender)
                .WithMany(u => u.MensagensEnviadas)
                .HasForeignKey(fk => fk.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }

    }
}
