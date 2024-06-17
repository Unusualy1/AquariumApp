using Microsoft.EntityFrameworkCore;
using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class PlantRepository : IPlantRepository
{
    public async Task<Plant?> GetById(long id)
    {
        using Context context = new();
        return await context.Plants.FindAsync(id);
    }

    public List<Plant> GetAll()
    {
        using Context context = new();

        return [.. context.Plants.Include(p => p.PlantSpecies)];
    }

    public async Task Add(Plant plant)
    {
        using Context context = new();

        if (plant.PlantSpecies != null)
        {
            context.Attach(plant.PlantSpecies);
        }

        await context.Plants.AddAsync(plant);
        await context.SaveChangesAsync();
    }

    public async Task Update(Plant plant)
    {
        using Context context = new();

        context.Plants.Update(plant);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        Plant? findedPlant = await context.Plants
                                          .Include(p => p.PlantEvents)
                                          .FirstOrDefaultAsync(p => p.Id == id);

        if (findedPlant == null) return;

        context.PlantEvents.RemoveRange(findedPlant.PlantEvents);

        context.Plants.Remove(findedPlant);
        await context.SaveChangesAsync();
    }
}
