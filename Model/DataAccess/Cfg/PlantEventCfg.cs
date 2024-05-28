using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model.Events;

namespace Model.DataAccess.Cfg;

public class PlantEventCfg : IEntityTypeConfiguration<PlantEvent>
{
    public void Configure(EntityTypeBuilder<PlantEvent> builder)
    {
        builder.Property(pe => pe.Type)
             .IsRequired();

        builder.Property(pe => pe.Description)
            .IsRequired();

        builder.HasOne(pe => pe.Plant)
            .WithMany()
            .HasForeignKey(pe => pe.PlantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
