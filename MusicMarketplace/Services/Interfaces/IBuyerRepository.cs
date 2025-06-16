/// <summary>
/// Интерфейс репозитория для работы с покупателями
/// </summary>
public interface IBuyerRepository
{
    /// <summary> Получить всех покупателей </summary>
    Task<IEnumerable<Buyer>> GetAllAsync();

    /// <summary> Получить покупателя по ID </summary>
    Task<Buyer> GetByIdAsync(int id);

    /// <summary> Добавить нового покупателя </summary>
    Task AddAsync(Buyer buyer);
}