using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class FishSpeciesCfg : IEntityTypeConfiguration<FishSpecies>
{
    public void Configure(EntityTypeBuilder<FishSpecies> builder)
    {
        builder.Property(fs => fs.Name)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(fs => fs.Description)
            .HasMaxLength(256);

        builder.Property(fs => fs.Type)
            .HasMaxLength(16);

        builder.Property(fs => fs.Origin)
            .HasMaxLength(32);
    }
}
