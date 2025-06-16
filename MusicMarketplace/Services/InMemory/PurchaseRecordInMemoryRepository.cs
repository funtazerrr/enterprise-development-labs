/// <summary>
/// In-memory реализация репозитория записей о покупках
/// </summary>
public class PurchaseRecordInMemoryRepository : IPurchaseRecordRepository
{
    private readonly List<PurchaseRecord> _records = new();
    private int _nextId = 1;

    public Task<IEnumerable<PurchaseRecord>> GetAllAsync() =>
        Task.FromResult(_records.AsEnumerable());

    public Task AddAsync(PurchaseRecord record)
    {
        record.Id = _nextId++;
        _records.Add(record);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<PurchaseRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate) =>
        Task.FromResult(_records.Where(r => r.PurchaseDate >= startDate && r.PurchaseDate <= endDate));
}