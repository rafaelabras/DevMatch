using DevMatch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMatch.EntitiesConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(p => p.Email).IsUnique();

        }
    }
}
