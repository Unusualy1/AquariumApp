using Microsoft.EntityFrameworkCore;
using Model.DataAccess;
using Model.DataAccess.Repositories.Events;
using Model.Events;
using System.ComponentModel;

namespace ViewModel.UseCases.Events;

public class FishEventRepository : IFishEventRepository
{
    public List<FishEvent> GetAll()
    {
        using Context context = new();

        return [.. context.FishEvents];
    }

    public async Task Add(FishEvent fishEvent)
    {
        using Context context = new();

        context.FishEvents.Add(fishEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(FishEvent fishEvent)
    {
        using Context context = new();

        context.FishEvents.Remove(fishEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(FishEvent fishEvent)
    {
        using Context context = new();

        context.Entry(fishEvent).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public List<FishEvent> GetAllByFishId(long fishId)
    {
        using Context context = new();

        return [.. context.FishEvents.Where(fe => fe.FishId == fishId)];
    }
}
