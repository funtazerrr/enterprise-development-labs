/// <summary>
/// In-memory реализация репозитория покупателей
/// </summary>
public class BuyerInMemoryRepository : IBuyerRepository
{
    private readonly List<Buyer> _buyers = new();
    private int _nextId = 1;

    public Task<IEnumerable<Buyer>> GetAllAsync() =>
        Task.FromResult(_buyers.AsEnumerable());

    public Task<Buyer> GetByIdAsync(int id) =>
        Task.FromResult(_buyers.FirstOrDefault(b => b.Id == id));

    public Task AddAsync(Buyer buyer)
    {
        buyer.Id = _nextId++;
        _buyers.Add(buyer);
        return Task.CompletedTask;
    }
}