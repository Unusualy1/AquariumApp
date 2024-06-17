
using Model.DataAccess;
using Model;
using Model.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ViewModel.UseCases;

public class PlantSpeciesRepository : IPlantSpeciesRepository
{
    public List<PlantSpecies> GetAll()
    {
        using Context context = new();

        return [.. context.PlantSpecies];
    }

    public async Task<PlantSpecies?> GetById(long id)
    {
        using Context context = new();
        return await context.PlantSpecies.FindAsync(id);
    }

    public async Task Add(PlantSpecies plantSpecies)
    {
        using Context context = new();

        context.PlantSpecies.Add(plantSpecies);
        await context.SaveChangesAsync();
    }

    public async Task Update(PlantSpecies plantSpecies)
    {
        using Context context = new();

        context.PlantSpecies.Update(plantSpecies);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        PlantSpecies? findedPlantSpecies = await context.PlantSpecies
                                                        .Include(ps => ps.PlantSpeciesEvents)
                                                        .FirstOrDefaultAsync(ps => ps.Id == id);

        if (findedPlantSpecies == null) return;

        context.PlantSpeciesEvents.RemoveRange(findedPlantSpecies.PlantSpeciesEvents);

        context.PlantSpecies.Remove(findedPlantSpecies);
        await context.SaveChangesAsync();
    }
}
