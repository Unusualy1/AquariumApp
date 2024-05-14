using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class DecorationRepository : IDecorationRepository
{
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

    public async Task Delete(Decoration decoration)
    {
        using Context context = new();

        context.Decorations.Remove(decoration);
        await context.SaveChangesAsync();
    }

    public List<Decoration> GetAll()
    {
        using Context context = new();

        return [.. context.Decorations];
    }

    
}
