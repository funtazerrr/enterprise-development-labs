/// <summary>
/// Интерфейс репозитория для работы с записями о покупках
/// </summary>
public interface IPurchaseRecordRepository
{
    /// <summary> Получить все записи о покупках </summary>
    Task<IEnumerable<PurchaseRecord>> GetAllAsync();

    /// <summary> Добавить новую запись о покупке </summary>
    Task AddAsync(PurchaseRecord record);

    /// <summary> Получить записи за указанный период </summary>
    Task<IEnumerable<PurchaseRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
}