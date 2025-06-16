/// <summary>
/// Модель продавца (магазина) на площадке
/// </summary>
public class Seller
{
    /// <summary> Уникальный идентификатор продавца </summary>
    public int Id { get; set; }

    /// <summary> Название магазина </summary>
    public string ShopName { get; set; }

    /// <summary> Страна расположения магазина </summary>
    public string Country { get; set; }

    /// <summary> Стоимость доставки одного товара (фиксированная для этого продавца) </summary>
    public decimal ShippingCostPerItem { get; set; }

    /// <summary> Список товаров продавца (навигационное свойство) </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}