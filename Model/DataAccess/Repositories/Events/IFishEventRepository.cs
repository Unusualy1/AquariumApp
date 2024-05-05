using Model.Events;

namespace Model.DataAccess.Repositories.Events;

public interface IFishEventRepostiry
{
    public List<FishEvent> GetAll();

    public Task Add(FishEvent fishEvent);

    public Task Update(FishEvent fishEvent);

    public Task Delete(FishEvent fishEvent);
}
