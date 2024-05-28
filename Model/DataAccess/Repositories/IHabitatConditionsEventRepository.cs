namespace Model.DataAccess.Repositories;

public interface IHabitatConditionsEventRepository
{
    public List<HabitatConditionsEvent> GetAll();

    public Task Add(HabitatConditionsEvent habitatConditionsEvent);

    public Task Update(HabitatConditionsEvent habitatConditionsEvent);

    public Task Delete(long id);
}
