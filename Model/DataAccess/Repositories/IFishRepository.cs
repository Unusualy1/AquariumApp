namespace Model.DataAccess.Repositories;

public interface IFishRepository
{
    List<Fish> GetAll();

    Task Add(Fish fish);
    
    Task Update(Fish fish);
    
    Task Delete(Fish fish);
}