using Microsoft.EntityFrameworkCore;
using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class DecorationRepository : IDecorationRepository
{
    public List<Decoration> GetAll()
    {
        using Context context = new();

        return [.. context.Decorations];
    }

    public async Task Add(Decoration decoration)
    {
        using Context context = new();
        
        context.Decorations.Add(decoration);
        await context.SaveChangesAsync();
    }

    public async Task Update(Decoration decoration)
    {
        using Context context = new();

        context.Decorations.Update(decoration);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        Decoration? findedDecoration = await context.Decorations
                                                    .Include(d => d.DecorationEvents)
                                                    .FirstOrDefaultAsync(d => d.Id == id);

        if (findedDecoration == null) return;

        context.DecorationsEvents.RemoveRange(findedDecoration.DecorationEvents);

        context.Decorations.Remove(findedDecoration);
        await context.SaveChangesAsync();
    }
}
