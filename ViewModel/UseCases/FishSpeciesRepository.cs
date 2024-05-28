using Microsoft.EntityFrameworkCore;
using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishSpeciesRepository : IFishSpeciesRepository
{
    public List<FishSpecies> GetAll()
    {
        using Context context = new();

        return [.. context.FishSpecies];
    }
    
    public async Task<FishSpecies?> GetById(long id)
    {
        using Context context = new();
        return await context.FishSpecies.FindAsync(id);
    }

    public async Task Add(FishSpecies fishSpecies)
    {
        using Context context = new();

        context.FishSpecies.Add(fishSpecies);
        await context.SaveChangesAsync();
    }

    public async Task Update(FishSpecies fishSpecies)
    {
        using Context context = new();

        context.FishSpecies.Update(fishSpecies);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        FishSpecies? findedFishSpecies = await context.FishSpecies
                                                       .Include(fs => fs.FishSpeciesEvents)
                                                       .FirstOrDefaultAsync(f => f.Id == id);

        if (findedFishSpecies == null) return;

        context.FishSpeciesEvents.RemoveRange(findedFishSpecies.FishSpeciesEvents);

        context.FishSpecies.Remove(findedFishSpecies);
        await context.SaveChangesAsync();
    }
}
