namespace Model.DataAccess.Repositories;

public interface IPlantSpeciesEventRepository
{
    public List<PlantSpeciesEvent> GetAll();

    public List<PlantSpeciesEvent> GetAllByPlantSpeciesId(long plantSpeciesId);

    public Task Add(PlantSpeciesEvent plantSpeciesEvent);

    public Task Update(PlantSpeciesEvent plantSpeciesEvent);

    public Task Delete(long id);
    
}
