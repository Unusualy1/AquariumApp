using Microsoft.EntityFrameworkCore;
using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class FishRepository : IFishRepository
{
    public List<Fish> GetAll()
    {
        using Context context = new();

        return [.. context.Fishes.Include(f => f.FishSpecies)];
    }

    public async Task<Fish?> GetById(long id)
    {
        using Context context = new();
        return await context.Fishes.FindAsync(id);
    }

    public async Task Add(Fish fish)
    {
        using Context context = new();

        if (fish.FishSpecies != null)
        {
            context.Attach(fish.FishSpecies);
        }

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

        Fish? findedFish = await context.Fishes
                                        .Include(f => f.FishEvents)
                                        .FirstOrDefaultAsync(f => f.Id == id);
        if (findedFish == null) return;

        context.FishEvents.RemoveRange(findedFish.FishEvents);

        context.Fishes.Remove(findedFish);
        await context.SaveChangesAsync();
    }
}
