namespace Model.DataAccess.Repositories;

public interface IFishSpeciesRepository
{
    public List<FishSpecies> GetAll();

    public Task<FishSpecies?> GetById(long id);

    public Task Add(FishSpecies fishSpecies); 

    public Task Update(FishSpecies fishSpecies); 

    public Task Delete(long id);
}
