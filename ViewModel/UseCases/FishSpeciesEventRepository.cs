using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishSpeciesEventRepository : IFishSpeciesEventRepository
{
    public List<FishSpeciesEvent> GetAll()
    {
        using Context context = new();

        return [.. context.FishSpeciesEvents];
    }

    public List<FishSpeciesEvent> GetAllByFishSpeciesId(long fishSpeciesId)
    {
        using Context context = new();

        return [.. context.FishSpeciesEvents.Where(fse => fse.FishSpeciesId == fishSpeciesId)];
    }

    public async Task Add(FishSpeciesEvent fishSpeciesEvent)
    {
        using Context context = new();

        context.FishSpeciesEvents.Add(fishSpeciesEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(FishSpeciesEvent fishSpeciesEvent)
    {
        using Context context = new();

        context.FishSpeciesEvents.Update(fishSpeciesEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        FishSpeciesEvent? findedFishSpeciesEvent = await context.FishSpeciesEvents.FindAsync(id);

        if (findedFishSpeciesEvent == null) return;

        context.FishSpeciesEvents.Remove(findedFishSpeciesEvent);
        await context.SaveChangesAsync();

    }
}
