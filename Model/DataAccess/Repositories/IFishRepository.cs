namespace Model.DataAccess.Repositories;

public interface IFishRepository
{
    public List<Fish> GetAll(); 
    public Task Add(Fish fish); 
    public Task Update(Fish fish); 
    public Task Delete(long id);
}