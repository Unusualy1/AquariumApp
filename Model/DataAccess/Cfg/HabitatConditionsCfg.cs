using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Events;

namespace Model.DataAccess.Cfg;

public class HabitatConditionsCfg : IEntityTypeConfiguration<HabitatConditions>
{
    public void Configure(EntityTypeBuilder<HabitatConditions> builder)
    {

        builder.Property(hc => hc.WaterTemperature)
            .HasAnnotation("Range", new { Min = -273.15, Max = 1000 })
            .IsRequired();

        builder.Property(hc => hc.DegreeOfAcidity)
            .HasAnnotation("Range", new { Min = 0, Max = 14 })
            .IsRequired();

        builder.Property(hc => hc.Lighting)
            .HasAnnotation("Range", new { Min = 0.0, Max = 1000.0 })
            .IsRequired();

        builder.Property(hc => hc.Substrate)
            .HasMaxLength(100);

        builder.Property(hc => hc.OxygenLevel)
            .HasAnnotation("Range", new { Min = 0.0, Max = 1000.0 })
            .IsRequired();

        builder.Property(hc => hc.Salinity)
            .HasAnnotation("Range", new { Min = 0.0, Max = 1000.0 })
            .IsRequired();

        builder.HasMany(hc => hc.HabitatConditionsEvents)
            .WithOne(e => e.HabitatConditions)
            .HasForeignKey(e => e.HabitatConditionsId);
    }
}

