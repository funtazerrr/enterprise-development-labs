/// <summary>
/// Интерфейс репозитория для работы с товарами
/// </summary>
public interface IProductRepository
{
    /// <summary> Получить все товары </summary>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary> Получить товары по идентификатору продавца </summary>
    Task<IEnumerable<Product>> GetBySellerAsync(int sellerId);

    /// <summary> Получить товар по ID </summary>
    Task<Product> GetByIdAsync(int id);

    /// <summary> Добавить новый товар </summary>
    Task AddAsync(Product product);

    /// <summary> Обновить информацию о товаре </summary>
    Task UpdateAsync(Product product);

    /// <summary> Удалить товар </summary>
    Task DeleteAsync(int id);
}