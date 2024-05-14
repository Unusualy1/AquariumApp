using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishSpeciesRepository : IFishSpeciesRepository
{
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

    public async Task Delete(FishSpecies fishSpecies)
    {
        using Context context = new();

        context.FishSpecies.Remove(fishSpecies);
        await context.SaveChangesAsync();
    }

    public List<FishSpecies> GetAll()
    {
        using Context context = new();

        return [.. context.FishSpecies];
    }
}
