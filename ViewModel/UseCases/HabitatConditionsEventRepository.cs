using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class HabitatConditionsEventRepository : IHabitatConditionsEventRepository
{
    public List<HabitatConditionsEvent> GetAll()
    {
        using Context context = new();

        return [.. context.HabitatConditionsEvents];
    }

    public async Task Add(HabitatConditionsEvent habitatConditionsEvent)
    {
        using Context context = new();

        context.HabitatConditionsEvents.Add(habitatConditionsEvent);
        await context.SaveChangesAsync();
    }

    public async Task Update(HabitatConditionsEvent habitatConditionsEvent)
    {

        using Context context = new();

        context.HabitatConditionsEvents.Update(habitatConditionsEvent);
        await context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        using Context context = new();

        HabitatConditionsEvent? findedHabitatConditionsEvent = await context.HabitatConditionsEvents.FindAsync(id);

        if (findedHabitatConditionsEvent == null) return;

        context.HabitatConditionsEvents.Remove(findedHabitatConditionsEvent);
        await context.SaveChangesAsync();
    }
}
