using Model.Events;

namespace Model.DataAccess.Repositories.Events;

public interface IHabitatConditionsEventRepository
{
    public List<HabitatConditionsEvent> GetAll();

    public Task Add(HabitatConditionsEvent habitatConditionsEvent);

    public Task Update(HabitatConditionsEvent habitatConditionsEvent);

    public Task Delete(HabitatConditionsEvent habitatConditionsEvent);
}
