namespace Model.DataAccess.Repositories;

public interface IDecorationRepository
{
    List<Decoration> GetAll();

    Task Add(Decoration decoration);

    Task Update(Decoration decoration);

    Task Delete(Decoration decoration);
}
