using Model.DataAccess;
using Model;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class PlantSpeciesEventRepository : IPlantSpeciesEventRepository
{
    public List<PlantSpeciesEvent> GetAll()
    {
        using Context context = new();

        return [.. context.PlantSpeciesEvents];
    }

    public List<PlantSpeciesEvent> GetAllByPlantSpeciesId(long plantSpeciesId)
    {
        using Context context = new();

        return [.. context.PlantSpeciesEvents.Where(fse => fse.PlantSpeciesId == plantSpeciesId)];
    }

    public async Task Add(PlantSpeciesEvent plantSpeciesEvent)
    {
        using Context context = new();

        context.PlantSpeciesEvents.Add(plantSpeciesEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(PlantSpeciesEvent plantSpeciesEvent)
    {
        using Context context = new();

        context.PlantSpeciesEvents.Update(plantSpeciesEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        PlantSpeciesEvent? findedPlantSpeciesEvent = await context.PlantSpeciesEvents.FindAsync(id);

        if (findedPlantSpeciesEvent == null) return;

        context.PlantSpeciesEvents.Remove(findedPlantSpeciesEvent);
        await context.SaveChangesAsync();

    }
}
