using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class PlantSpeciesEventCfg : IEntityTypeConfiguration<PlantSpeciesEvent>
{
    public void Configure(EntityTypeBuilder<PlantSpeciesEvent> builder)
    {
        builder.Property(pse => pse.Type)
           .IsRequired();

        builder.Property(pse => pse.Description)
            .IsRequired();

        builder.HasOne(pse => pse.PlantSpecies)
            .WithMany(ps => ps.PlantSpeciesEvents)
            .HasForeignKey(pse => pse.PlantSpeciesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}