namespace Model.DataAccess.Repositories;

public interface IDecorationEventRepository
{
    public List<DecorationEvent> GetAll();

    public Task Add(DecorationEvent decorationEvent);

    public Task Update(DecorationEvent decorationEvent);

    public Task Delete(DecorationEvent decorationEvent);
}

