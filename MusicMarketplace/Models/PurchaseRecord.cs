/// <summary>
/// Модель записи о совершенной покупке
/// </summary>
public class PurchaseRecord
{
    /// <summary> Уникальный идентификатор записи </summary>
    public int Id { get; set; }

    /// <summary> Идентификатор купленного товара </summary>
    public int ProductId { get; set; }

    /// <summary> Ссылка на объект товара (навигационное свойство) </summary>
    public Product Product { get; set; }

    /// <summary> Идентификатор покупателя </summary>
    public int BuyerId { get; set; }

    /// <summary> Ссылка на объект покупателя (навигационное свойство) </summary>
    public Buyer Buyer { get; set; }

    /// <summary> Дата и время покупки (UTC) </summary>
    public DateTime PurchaseDate { get; set; }

    /// <summary> Итоговая стоимость (цена товара + доставка) </summary>
    public decimal TotalPrice { get; set; }
}