using Microsoft.EntityFrameworkCore;
using Model.DataAccess.Cfg;

namespace Model.DataAccess;

public class Context : DbContext
{
    public DbSet<Fish> Fishes { get; set; }

    public DbSet<FishSpecies> FishSpecies { get; set; }

    public DbSet<Decoration> Decorations { get; set; }

    public DbSet<Plant> Plants { get; set; }

    public DbSet<HabitatConditions> HabitatConditions { get; set; }

    public Context()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=localhost;Port=5432;Database=aquarium_app;Username=postgres;Password=root;";
        optionsBuilder.UseNpgsql(connectionString)
                      .UseAllCheckConstraints();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HabitatConditions>().HasData(new HabitatConditions
        {
            Id = 1,
            WaterTemperature = 0.0,
            DegreeOfAcidity = 0,
            Lighting = 0.0,
            OxygenLevel = 0.0,
            Salinity = 0.0,
            Substrate = "Не указано"
        });
        //modelBuilder.ApplyConfiguration(new DecorationsCfg());
        //modelBuilder.ApplyConfiguration(new FishCfg());
        //modelBuilder.ApplyConfiguration(new FishSpeciesCfg());
        //modelBuilder.ApplyConfiguration(new HabitatConditionsCfg());
        //modelBuilder.ApplyConfiguration(new PlantCfg());
    }
    
}
