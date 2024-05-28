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

        FishSpecies? findedFishSpecies = await context.FishSpecies.FindAsync(id);

        if (findedFishSpecies == null) return;

        context.FishSpecies.Remove(findedFishSpecies);
        await context.SaveChangesAsync();
    }
}
