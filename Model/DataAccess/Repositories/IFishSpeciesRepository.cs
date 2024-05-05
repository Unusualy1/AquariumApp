namespace Model.DataAccess.Repositories;

public interface IFishSpeciesRepository
{
    List<FishSpecies> GetAll();

    Task Add(FishSpecies fishSpecies);

    Task Update(FishSpecies fishSpecies);

    Task Delete(FishSpecies fishSpecies);
}
