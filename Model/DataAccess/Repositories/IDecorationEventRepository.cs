namespace Model.DataAccess.Repositories;

public interface IDecorationEventRepository
{
    public List<DecorationEvent> GetAll();

    public List<DecorationEvent> GetAllByDecorationId(int decorationId);

    public Task Add(DecorationEvent decorationEvent);

    public Task Update(DecorationEvent decorationEvent);

    public Task Delete(long id);
}

