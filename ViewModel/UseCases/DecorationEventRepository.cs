using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;
using System.Diagnostics;

namespace ViewModel.UseCases;

public class DecorationEventRepository : IDecorationEventRepository
{
    public List<DecorationEvent> GetAll()
    {
        using Context context = new();

        return [.. context.DecorationsEvents];
    }

    public List<DecorationEvent> GetAllByDecorationId(int decorationId)
    {
        using Context context = new();

        return [.. context.DecorationsEvents.Where(fe => fe.DecorationId == decorationId)];
    }

    public async Task Add(DecorationEvent decorationEvent)
    {
        using Context context = new();

        context.DecorationsEvents.Add(decorationEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(DecorationEvent decorationEvent)
    {
        using Context context = new();

        context.DecorationsEvents.Update(decorationEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();
            
        DecorationEvent? findedDecorationEvent = await context.DecorationsEvents.FindAsync(id);

        if (findedDecorationEvent == null) return; 

        context.DecorationsEvents.Remove(findedDecorationEvent);
        await context.SaveChangesAsync();
    }
}
