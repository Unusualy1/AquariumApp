using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class FishCfg : IEntityTypeConfiguration<Fish>
{
    public void Configure(EntityTypeBuilder<Fish> builder)
    {
        builder.Property(f => f.Name)
           .IsRequired()
           .HasMaxLength(16);

        builder.Property(f => f.Width)
            .IsRequired()
            .HasAnnotation("Range", new { Min = 0, Max = 256 });

        builder.Property(f => f.Length)
            .IsRequired()
            .HasAnnotation("Range", new { Min = 0, Max = 256 });

        builder.Property(f => f.Color)
            .HasMaxLength(32);

        builder.Property(f => f.Diet)
            .HasMaxLength(32);

        builder.Property(f => f.Habitat)
            .HasMaxLength(64);

        builder.HasOne(f => f.FishSpecies)
               .WithMany()
               .HasForeignKey(f => f.FishSpeciesId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.FishEvents)
            .WithOne(e => e.Fish)
            .HasForeignKey(e => e.FishId);
    }
}
