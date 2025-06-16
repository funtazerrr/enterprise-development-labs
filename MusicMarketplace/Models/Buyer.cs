/// <summary>
/// Модель покупателя на площадке
/// </summary>
public class Buyer
{
    /// <summary> Уникальный идентификатор покупателя </summary>
    public int Id { get; set; }

    /// <summary> Полное имя покупателя </summary>
    public string FullName { get; set; }

    /// <summary> Страна доставки </summary>
    public string Country { get; set; }

    /// <summary> Полный почтовый адрес </summary>
    public string Address { get; set; }

    /// <summary> Список покупок (навигационное свойство) </summary>
    public ICollection<PurchaseRecord> Purchases { get; set; } = new List<PurchaseRecord>();
}