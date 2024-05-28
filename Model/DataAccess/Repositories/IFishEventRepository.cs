namespace Model.DataAccess.Repositories;

public interface IFishEventRepository
{
    public List<FishEvent> GetAll();

    public Task Add(FishEvent fishEvent);

    public Task Update(FishEvent fishEvent);

    public Task Delete(FishEvent fishEvent);

    public List<FishEvent> GetAllByFishId(long fishId);

}
