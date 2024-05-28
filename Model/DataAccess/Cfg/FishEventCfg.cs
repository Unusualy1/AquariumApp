using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model.Events;

namespace Model.DataAccess.Cfg;

public class FishEventCfg : IEntityTypeConfiguration<FishEvent>
{
    public void Configure(EntityTypeBuilder<FishEvent> builder)
    {
        builder.HasKey(fe => fe.Id);

        builder.Property(fe => fe.Type)
            .IsRequired();

        builder.Property(fe => fe.Description)
            .IsRequired();

        builder.HasOne(fe => fe.Fish)
            .WithMany(f => f.FishEvents)
            .HasForeignKey(fe => fe.FishId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
