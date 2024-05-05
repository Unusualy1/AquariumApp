using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class DecorationsCfg : IEntityTypeConfiguration<Decoration>
{
    public void Configure(EntityTypeBuilder<Decoration> builder)
    {
        throw new NotImplementedException();
    }
}

