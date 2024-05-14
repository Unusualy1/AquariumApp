using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishRepository : IFishRepository
{
    public async Task Add(Fish fish)
    {
        using Context context = new();

        context.Fishes.Add(fish);
        await context.SaveChangesAsync();
    }
    public async Task Update(Fish fish)
    {
        using Context context = new();

        context.Fishes.Update(fish);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Fish fish)
    {
        using Context context = new();

        context.Fishes.Remove(fish);
        await context.SaveChangesAsync();
    }

    public List<Fish> GetAll()
    {
        using Context context = new();

        return [.. context.Fishes];
    }
}
