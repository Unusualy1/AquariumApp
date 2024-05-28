using Microsoft.EntityFrameworkCore;
using Model;
using Model.DataAccess;
using Model.DataAccess.Repositories;

namespace ViewModel.UseCases;

public class HabitatConditionsRepository : IHabitatConditionRepository
{
    public HabitatConditions Get()
    {
        using Context context = new();

        return context.HabitatConditions.Include(hc => hc.HabitatConditionsEvents).First() ?? throw new ArgumentNullException();
    }

    public async Task Update(HabitatConditions habitatConditions)
    {
        using Context context = new();

        context.HabitatConditions.Update(habitatConditions);
        await context.SaveChangesAsync();
    }
}
