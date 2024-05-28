using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class PlantRepository : IPlantRepository
{
    public List<Plant> GetAll()
    {
        using Context context = new();

        return [.. context.Plants];
    }

    public async Task Add(Plant plant)
    {
        using Context context = new();

        context.Plants.Add(plant);
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

        Plant? findedPlant = await context.Plants.FindAsync(id);

        if (findedPlant == null) return;

        context.Plants.Remove(findedPlant);
        await context.SaveChangesAsync();
    }
}
