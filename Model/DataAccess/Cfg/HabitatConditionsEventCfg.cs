using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class HabitatConditionsEventCfg : IEntityTypeConfiguration<HabitatConditionsEvent>
{
    public void Configure(EntityTypeBuilder<HabitatConditionsEvent> builder)
    {
        builder.Property(hce => hce.Type)
            .IsRequired();

        builder.Property(hce => hce.Description)
            .IsRequired();

    }
}

