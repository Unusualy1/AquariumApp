using System.Diagnostics.Contracts;

namespace Model.DataAccess.Repositories;

public interface IFishRepository
{
    public Task<Fish?> GetById(long id);
    public List<Fish> GetAll(); 
    public Task Add(Fish fish); 
    public Task Update(Fish fish); 
    public Task Delete(long id);
}