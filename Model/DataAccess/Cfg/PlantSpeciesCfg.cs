using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class PlantSpeciesCfg : IEntityTypeConfiguration<PlantSpecies>
{
    public void Configure(EntityTypeBuilder<PlantSpecies> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(256);

        builder.Property(p => p.Type)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(p => p.Origin)
            .HasMaxLength(32);
    }
}