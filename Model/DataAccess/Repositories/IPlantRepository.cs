namespace Model.DataAccess.Repositories;

public interface IPlantRepository
{
    public List<Plant> GetAll(); 

    public Task Add(Plant plant); 

    public Task Update(Plant plant); 

    public Task Delete(long id);
}
