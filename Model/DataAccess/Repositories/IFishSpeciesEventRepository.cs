namespace Model.DataAccess.Repositories;

public interface IFishSpeciesEventRepository
{
    public List<FishSpeciesEvent> GetAll();

    public Task Add(FishSpeciesEvent fishSpeciesEvent);

    public Task Update(FishSpeciesEvent fishSpeciesEvent);

    public Task Delete(FishSpeciesEvent fishSpeciesEvent);
}
