/// <summary>
/// Интерфейс сервиса для работы с музыкальной торговой площадкой
/// </summary>
public interface IMusicMarketplaceService
{
    /// <summary> Получить все проданные виниловые пластинки </summary>
    Task<IEnumerable<Product>> GetSoldVinylsAsync();

    /// <summary> Получить товары продавца, отсортированные по цене </summary>
    /// <param name="sellerId">Идентификатор продавца</param>
    Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId);

    /// <summary> Получить продаваемые альбомы исполнителя в хорошем состоянии </summary>
    /// <param name="artist">Имя исполнителя</param>
    Task<IEnumerable<Product>> GetAvailableAlbumsByArtistAsync(string artist);

    /// <summary> Получить статистику по проданным товарам по типам носителей </summary>
    Task<Dictionary<AudioFormat, int>> GetSoldProductsStatsByFormatAsync();

    /// <summary> Получить топ-5 покупателей по средней стоимости покупок </summary>
    Task<IEnumerable<Buyer>> GetTopBuyersByAveragePurchaseAsync();

    /// <summary> Получить статистику продаж по продавцам за последние 2 недели </summary>
    Task<Dictionary<Seller, int>> GetSellerSalesStatsLastTwoWeeksAsync();
}