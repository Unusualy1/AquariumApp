using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishRepository : IFishRepository
{
    public async Task Add(Fish fish)
    {
        using Context context = new();

        await context.Fishes.AddAsync(fish);
        context.SaveChanges();
    }
    public async Task Update(Fish fish)
    {
        using Context context = new();

        context.Fishes.Update(fish);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        Fish? findedFish = await context.Fishes.FindAsync(id);

        if (findedFish == null) return;

        context.Fishes.Remove(findedFish);
        context.SaveChanges();
    }

    public List<Fish> GetAll()
    {
        using Context context = new();

        return [.. context.Fishes];
    }
}
