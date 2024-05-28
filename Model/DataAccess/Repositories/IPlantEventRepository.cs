namespace Model.DataAccess.Repositories;

public interface IPlantEventRepository
{
    public List<PlantEvent> GetAll();

    public List<PlantEvent> GetAllByPlantId(long plantId);

    public Task Add(PlantEvent plantEvent);

    public Task Update(PlantEvent plantEvent);

    public Task Delete(long id);
}
