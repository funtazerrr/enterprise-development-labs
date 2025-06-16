/// <summary>
/// In-memory реализация репозитория товаров
/// </summary>
public class ProductInMemoryRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public Task<IEnumerable<Product>> GetAllAsync() =>
        Task.FromResult(_products.AsEnumerable());

    public Task<IEnumerable<Product>> GetBySellerAsync(int sellerId) =>
        Task.FromResult(_products.Where(p => p.SellerId == sellerId));

    public Task<Product> GetByIdAsync(int id) =>
        Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task AddAsync(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index >= 0) _products[index] = product;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _products.RemoveAll(p => p.Id == id);
        return Task.CompletedTask;
    }
}