using DevMatch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMatch.EntitiesConfiguration
{
    public class MentorProfileConfiguration : IEntityTypeConfiguration<MentorProfile>
    {
        public void Configure(EntityTypeBuilder<MentorProfile> builder)
        {
            builder.Property(p => p.Bio).HasMaxLength(500);
            builder.Property(p => p.Disponibilidade).HasMaxLength(100);
            builder.Property(p => p.TechStack).HasMaxLength(300);

        }
    }
}
