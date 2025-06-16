/// <summary>
/// In-memory реализация репозитория продавцов
/// </summary>
public class SellerInMemoryRepository : ISellerRepository
{
    private readonly List<Seller> _sellers = new();
    private int _nextId = 1;

    public Task<IEnumerable<Seller>> GetAllAsync() =>
        Task.FromResult(_sellers.AsEnumerable());

    public Task<Seller> GetByIdAsync(int id) =>
        Task.FromResult(_sellers.FirstOrDefault(s => s.Id == id));

    public Task AddAsync(Seller seller)
    {
        seller.Id = _nextId++;
        _sellers.Add(seller);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Seller seller)
    {
        var index = _sellers.FindIndex(s => s.Id == seller.Id);
        if (index >= 0) _sellers[index] = seller;
        return Task.CompletedTask;
    }
}