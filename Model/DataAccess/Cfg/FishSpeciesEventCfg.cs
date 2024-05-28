using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class FishSpeciesEventCfg : IEntityTypeConfiguration<FishSpeciesEvent>
{
    public void Configure(EntityTypeBuilder<FishSpeciesEvent> builder)
    {
        builder.Property(fse => fse.Type)
             .IsRequired();

        builder.Property(fse => fse.Description)
            .IsRequired();

        builder.HasOne(fse => fse.FishSpecies)
            .WithMany()
            .HasForeignKey(fse => fse.FishSpeciesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
