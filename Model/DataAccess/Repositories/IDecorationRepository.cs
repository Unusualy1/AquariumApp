namespace Model.DataAccess.Repositories;

public interface IDecorationRepository
{
    public List<Decoration> GetAll(); 

    public Task Add(Decoration decoration); 

    public Task Update(Decoration decoration);
    
    public Task Delete(long id);
}
