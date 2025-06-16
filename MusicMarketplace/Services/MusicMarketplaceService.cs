/// <summary>
/// Сервис для работы с музыкальной торговой площадкой
/// </summary>
public class MusicMarketplaceService : IMusicMarketplaceService
{
    private readonly IProductRepository _productRepository;
    private readonly ISellerRepository _sellerRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IPurchaseRecordRepository _purchaseRepository;

    /// <summary>
    /// Конструктор сервиса
    /// </summary>
    public MusicMarketplaceService(
        IProductRepository productRepository,
        ISellerRepository sellerRepository,
        IBuyerRepository buyerRepository,
        IPurchaseRecordRepository purchaseRepository)
    {
        _productRepository = productRepository;
        _sellerRepository = sellerRepository;
        _buyerRepository = buyerRepository;
        _purchaseRepository = purchaseRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetSoldVinylsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Where(p =>
            p.Format == AudioFormat.Vinyl &&
            p.IsSold);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId)
    {
        var products = await _productRepository.GetBySellerAsync(sellerId);
        return products.OrderBy(p => p.Price);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetAvailableAlbumsByArtistAsync(string artist)
    {
        var products = await _productRepository.GetAllAsync();
        return products.Where(p =>
            p.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase) &&
            p.Format == AudioFormat.Disc &&
            p.ReleaseType == ReleaseType.Album &&
            !p.IsSold &&
            p.MediaCondition >= Condition.Good &&
            p.PackagingCondition >= Condition.Good);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<AudioFormat, int>> GetSoldProductsStatsByFormatAsync()
    {
        var soldProducts = (await _productRepository.GetAllAsync())
            .Where(p => p.IsSold);

        return soldProducts
            .GroupBy(p => p.Format)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Buyer>> GetTopBuyersByAveragePurchaseAsync()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        var buyers = await _buyerRepository.GetAllAsync();

        return buyers
            .Join(purchases,
                b => b.Id,
                pr => pr.BuyerId,
                (b, pr) => new { Buyer = b, Purchase = pr })
            .GroupBy(x => x.Buyer)
            .OrderByDescending(g => g.Average(x => x.Purchase.TotalPrice))
            .Take(5)
            .Select(g => g.Key);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<Seller, int>> GetSellerSalesStatsLastTwoWeeksAsync()
    {
        var twoWeeksAgo = DateTime.UtcNow.AddDays(-14);
        var recentPurchases = (await _purchaseRepository.GetAllAsync())
            .Where(pr => pr.PurchaseDate >= twoWeeksAgo);

        var sellers = await _sellerRepository.GetAllAsync();

        return sellers
            .GroupJoin(recentPurchases,
                s => s.Id,
                pr => pr.Product.SellerId,
                (s, prs) => new { Seller = s, SalesCount = prs.Count() })
            .ToDictionary(x => x.Seller, x => x.SalesCount);
    }
}