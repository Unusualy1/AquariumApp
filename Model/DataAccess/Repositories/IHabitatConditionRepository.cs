namespace Model.DataAccess.Repositories;

public interface IHabitatConditionRepository
{
    public HabitatConditions Get();

    public Task Update(HabitatConditions habitatConditions);
}
