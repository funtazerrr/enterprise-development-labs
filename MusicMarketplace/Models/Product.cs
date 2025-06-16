/// <summary>
/// Модель музыкального товара в магазине
/// </summary>
public class Product
{
    /// <summary> Уникальный идентификатор товара </summary>
    public int Id { get; set; }

    /// <summary> Тип носителя (винил, кассета и т.д.) </summary>
    public AudioFormat Format { get; set; }

    /// <summary> Тип издания (альбом/сингл) </summary>
    public ReleaseType ReleaseType { get; set; }

    /// <summary> Исполнитель или группа </summary>
    public string Artist { get; set; }

    /// <summary> Название релиза </summary>
    public string Title { get; set; }

    /// <summary> Страна производства </summary>
    public string Country { get; set; }

    /// <summary> Состояние аудионосителя </summary>
    public Condition MediaCondition { get; set; }

    /// <summary> Состояние упаковки (конверта, коробки) </summary>
    public Condition PackagingCondition { get; set; }

    /// <summary> Цена в валюте магазина </summary>
    public decimal Price { get; set; }

    /// <summary> Флаг, указывающий продан товар или нет </summary>
    public bool IsSold { get; set; }

    /// <summary> Идентификатор продавца </summary>
    public int SellerId { get; set; }

    /// <summary> Объект продавца (навигационное свойство) </summary>
    public Seller Seller { get; set; }
}