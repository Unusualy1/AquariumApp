using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class PlantEventRepository : IPlantEventRepository
{
    public List<PlantEvent> GetAll()
    {
        using Context context = new();

        return [..context.PlantEvents];
    }

    public List<PlantEvent> GetAllByPlantId(long plantId)
    {
        using Context context = new();

        return [..context.PlantEvents.Where(pe => pe.PlantId == plantId)];
    }

    public async Task Add(PlantEvent plantEvent)
    {
        using Context context = new();

        context.PlantEvents.Add(plantEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(PlantEvent plantEvent)
    {
        using Context context = new();

        context.PlantEvents.Update(plantEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        PlantEvent? findedPlantEvent = await context.PlantEvents.FindAsync(id);

        if (findedPlantEvent == null) return;

        context.PlantEvents.Remove(findedPlantEvent);
        await context.SaveChangesAsync();
    }
}
