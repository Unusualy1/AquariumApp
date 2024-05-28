using Microsoft.EntityFrameworkCore;
using Model.Abstactions;
using Model.DataAccess.Cfg;
using Model.Events;
using Model.Factories;

namespace Model.DataAccess;

public class Context : DbContext
{
    public DbSet<Fish> Fishes { get; set; }

    public DbSet<FishSpecies> FishSpecies { get; set; }

    public DbSet<Decoration> Decorations { get; set; }

    public DbSet<Plant> Plants { get; set; }

    public DbSet<HabitatConditions> HabitatConditions { get; set; }

    public DbSet<DecorationEvent> DecorationsEvents { get; set; }

    public DbSet<FishEvent> FishEvents { get; set; }

    public DbSet<FishSpeciesEvent> FishSpeciesEvents { get; set; }

    public DbSet<HabitatConditionsEvent> HabitatConditionsEvents { get; set; }

    public DbSet<PlantEvent> PlantEvents { get; set; }
    


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
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new FishCfg());
        modelBuilder.ApplyConfiguration(new FishSpeciesCfg());
        modelBuilder.ApplyConfiguration(new FishEventCfg());
        modelBuilder.ApplyConfiguration(new DecorationCfg());
        modelBuilder.ApplyConfiguration(new DecorationEventCfg());
        modelBuilder.ApplyConfiguration(new HabitatConditionsCfg());
        modelBuilder.ApplyConfiguration(new HabitatConditionsEventCfg());
        modelBuilder.ApplyConfiguration(new PlantCfg());
        modelBuilder.ApplyConfiguration(new PlantEventCfg());

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

        modelBuilder.Entity<HabitatConditionsEvent>().HasData(new HabitatConditionsEvent
        {
            Id = 1,
            Type = EventType.Создание,
            Description = "Созданы начальные условия обитания",
            HabitatConditionsId = 1,
        });

        modelBuilder.Entity<FishSpecies>().HasData(new FishSpecies
        {
            Id = 1,
            Name = "Рыба-клоун",
            Description = "Род морских лучепёрых рыб из семейства помацентровых. Чаще всего под этим названием фигурирует аквариумная рыбка оранжевый амфиприон (Amphiprion percula).",
            Origin = "Япония",
            Type = "Морская мирная"
        });

        modelBuilder.Entity<FishSpeciesEvent>().HasData(FishSpeciesEventFactory.CreateStandartFishSpeciesEvent(1, EventType.Создание, 1));

        modelBuilder.Entity<Fish>().HasData(new Fish
        {
            Id = 1,
            Name = "Немо",
            Diet = "Водросли",
            Color = "Огненно-оранжевый",
            Habitat = "Море",
            Length = 12,
            Width = 14,
            FishSpeciesId = 1
        });

        modelBuilder.Entity<FishEvent>().HasData(FishEventFactory.CreateStandartFishEvent(1, EventType.Создание, 1));
    }
    
}
