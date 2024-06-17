namespace Model.DataAccess.Repositories;

public interface IPlantSpeciesRepository
{
    public List<PlantSpecies> GetAll();

    public Task<PlantSpecies?> GetById(long id);

    public Task Add(PlantSpecies plantSpecies);

    public Task Update(PlantSpecies plantSpecies);

    public Task Delete(long id);
}
