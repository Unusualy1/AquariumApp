using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Model.DataAccess.Cfg;

public class FishCfg : IEntityTypeConfiguration<Fish>
{
    public void Configure(EntityTypeBuilder<Fish> builder)
    {
        throw new NotImplementedException();
    }
}
