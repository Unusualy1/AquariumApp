namespace Model.DataAccess.Repositories;

public interface IFishSpeciesEventRepository
{
    public List<FishSpeciesEvent> GetAll();

    public List<FishSpeciesEvent> GetAllByFishSpeciesId(long fishSpeciesId);

    public Task Add(FishSpeciesEvent fishSpeciesEvent);

    public Task Update(FishSpeciesEvent fishSpeciesEvent);

    public Task Delete(long id);
}
