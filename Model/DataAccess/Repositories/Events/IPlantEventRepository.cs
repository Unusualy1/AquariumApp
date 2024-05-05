using Model.Events;

namespace Model.DataAccess.Repositories.Events;

public interface IPlantEventRepository
{
    public List<PlantEvent> GetAll(); 
    public Task Add(PlantEvent plantEvent); 
    public Task Update(PlantEvent plantEvent); 
    public Task Delete(PlantEvent plantEvent);
}
