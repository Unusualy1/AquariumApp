using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishEventRepository : IFishEventRepository
{
    public List<FishEvent> GetAll()
    {
        using Context context = new();

        return [.. context.FishEvents];
    }

    public List<FishEvent> GetAllByFishId(long fishId)
    {
        using Context context = new();

        return [.. context.FishEvents.Where(fe => fe.FishId == fishId)];
    }

    public async Task Add(FishEvent fishEvent)
    {
        using Context context = new();

        context.FishEvents.Add(fishEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(FishEvent fishEvent)
    {
        using Context context = new();

        context.FishEvents.Update(fishEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        FishEvent? findedFishEvent = await context.FishEvents.FindAsync(id);

        if (findedFishEvent == null) return;

        context.FishEvents.Remove(findedFishEvent);
        await context.SaveChangesAsync();
    } 
}
