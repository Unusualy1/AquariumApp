using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class PlantCfg : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> builder)
    {

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Name)
                   .HasMaxLength(16)
                   .IsRequired()
                   .HasAnnotation("ErrorMessage", "Названия растения не может превышать {1} символов.");

        builder.Property(p => p.Count)
               .IsRequired()
               .HasAnnotation("Range", new { Min = 0, Max = int.MaxValue })
               .HasAnnotation("ErrorMessage", "Значения для количества растений должно быть между {1} и {2}");

        builder.HasOne(p => p.PlantSpecies)
               .WithMany()
               .HasForeignKey(p => p.PlantSpeciesId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(p => p.PlantEvents)
               .WithOne(e => e.Plant)
               .HasForeignKey(e => e.PlantId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}