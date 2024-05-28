using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class DecorationCfg : IEntityTypeConfiguration<Decoration>
{
    public void Configure(EntityTypeBuilder<Decoration> builder)
    {
        builder.Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Description)
            .HasMaxLength(200);

        builder.Property(d => d.Count)
            .IsRequired()
            .HasAnnotation("Range", new { Min = 0, Max = int.MaxValue });
    }
}

