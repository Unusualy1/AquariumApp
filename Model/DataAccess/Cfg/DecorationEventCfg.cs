using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model.Events;

namespace Model.DataAccess.Cfg;

public class DecorationEventCfg : IEntityTypeConfiguration<DecorationEvent>
{
    public void Configure(EntityTypeBuilder<DecorationEvent> builder)
    {
        builder.Property(de => de.Type)
           .IsRequired();

        builder.Property(de => de.Description)
            .IsRequired();

        builder.HasOne(de => de.Decoration)
            .WithMany()
            .HasForeignKey(de => de.DecorationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

