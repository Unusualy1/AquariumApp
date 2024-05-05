namespace Model.DataAccess.Repositories;

public interface IPlantRepository
{
    List<Plant> GetAll();

    Task Add(Plant plant);

    Task Update(Plant plant);

    Task Delete(Plant plant);
}
