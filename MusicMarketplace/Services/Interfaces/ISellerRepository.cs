/// <summary>
/// Интерфейс репозитория для работы с продавцами
/// </summary>
public interface ISellerRepository
{
    /// <summary> Получить всех продавцов </summary>
    Task<IEnumerable<Seller>> GetAllAsync();

    /// <summary> Получить продавца по ID </summary>
    Task<Seller> GetByIdAsync(int id);

    /// <summary> Добавить нового продавца </summary>
    Task AddAsync(Seller seller);

    /// <summary> Обновить информацию о продавце </summary>
    Task UpdateAsync(Seller seller);
}