using DevMatch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMatch.EntitiesConfiguration
{
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasCheckConstraint("CK_1_5", "[Nota] >= 1 AND [Nota] <= 5");

            builder.Property(p => p.Comentario).HasMaxLength(500);
        }

        
    }
}
