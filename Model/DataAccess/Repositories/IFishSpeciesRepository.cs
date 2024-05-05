namespace Model.DataAccess.Repositories;

public interface IFishSpeciesRepository
{
    public List<FishSpecies> GetAll(); 
    public Task Add(FishSpecies fishSpecies); 
    public Task Update(FishSpecies fishSpecies); 
    public Task Delete(FishSpecies fishSpecies);
}
