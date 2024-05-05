namespace Model.DataAccess.Repositories;

public interface IHabitatConditionRepository
{
    HabitatConditions Get();

    Task Update(HabitatConditions habitatConditions);
}
