using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Тесты для сервиса музыкального маркетплейса
/// </summary>
public class MusicMarketplaceServiceTests
{
    /// <summary>
    /// Тестовые данные продуктов
    /// </summary>
    private readonly List<Product> _testProducts = new()
    {
        new Product { Id = 1, Format = AudioFormat.Vinyl, IsSold = true, Artist = "Pink Floyd", Price = 100 },
        new Product { Id = 2, Format = AudioFormat.Vinyl, IsSold = false, Artist = "The Beatles", Price = 150 },
        new Product { Id = 3, Format = AudioFormat.Disc, IsSold = true, Artist = "Queen", Price = 50 },
        new Product { Id = 4, Format = AudioFormat.Cassette, IsSold = true, Artist = "Nirvana", Price = 30 }
    };

    /// <summary>
    /// Тестовые данные продавцов
    /// </summary>
    private readonly List<Seller> _testSellers = new()
    {
        new Seller { Id = 1, ShopName = "Vinyl Shop" },
        new Seller { Id = 2, ShopName = "CD Paradise" }
    };

    /// <summary>
    /// Тестовые данные покупателей
    /// </summary>
    private readonly List<Buyer> _testBuyers = new()
    {
        new Buyer { Id = 1, FullName = "John Doe" },
        new Buyer { Id = 2, FullName = "Jane Smith" }
    };

    /// <summary>
    /// Тестовые данные о покупках
    /// </summary>
    private readonly List<PurchaseRecord> _testPurchases = new()
    {
        new PurchaseRecord { Id = 1, ProductId = 1, BuyerId = 1, PurchaseDate = DateTime.UtcNow.AddDays(-1), TotalPrice = 110 },
        new PurchaseRecord { Id = 2, ProductId = 3, BuyerId = 2, PurchaseDate = DateTime.UtcNow.AddDays(-10), TotalPrice = 60 }
    };

    /// <summary>
    /// Тест получения проданных виниловых пластинок
    /// </summary>
    [Fact]
    public async Task GetSoldVinylsAsync_ReturnsOnlySoldVinyls()
    {
        /// <summary>
        /// Подготовка тестовых данных
        /// </summary>
        var stubRepo = new StubProductRepository(_testProducts);
        var service = new MusicMarketplaceService(
            stubRepo,
            new StubSellerRepository(_testSellers),
            new StubBuyerRepository(_testBuyers),
            new StubPurchaseRepository(_testPurchases));

        /// <summary>
        /// Выполнение тестируемого метода
        /// </summary>
        var result = await service.GetSoldVinylsAsync();

        /// <summary>
        /// Проверка результатов
        /// </summary>
        Assert.Single(result);
        Assert.All(result, p => Assert.True(p.Format == AudioFormat.Vinyl && p.IsSold));
    }

    /// <summary>
    /// Тест получения товаров продавца, отсортированных по цене
    /// </summary>
    [Fact]
    public async Task GetProductsBySellerAsync_ReturnsOrderedByPrice()
    {
        /// <summary>
        /// Подготовка тестовых данных
        /// </summary>
        var testProducts = new List<Product>
        {
            new Product { Id = 1, SellerId = 1, Price = 100 },
            new Product { Id = 2, SellerId = 1, Price = 50 },
            new Product { Id = 3, SellerId = 2, Price = 75 }
        };

        var stubRepo = new StubProductRepository(testProducts);
        var service = new MusicMarketplaceService(
            stubRepo,
            new StubSellerRepository(_testSellers),
            new StubBuyerRepository(_testBuyers),
            new StubPurchaseRepository(_testPurchases));

        /// <summary>
        /// Выполнение тестируемого метода
        /// </summary>
        var result = (await service.GetProductsBySellerAsync(1)).ToList();

        /// <summary>
        /// Проверка результатов
        /// </summary>
        Assert.Equal(2, result.Count);
        Assert.Equal(50, result[0].Price);
        Assert.Equal(100, result[1].Price);
    }

    /// <summary>
    /// Тест получения топ-5 покупателей по средней стоимости покупок
    /// </summary>
    [Fact]
    public async Task GetTopBuyersByAveragePurchaseAsync_ReturnsOrderedResults()
    {
        /// <summary>
        /// Подготовка тестовых данных
        /// </summary>
        var testPurchases = new List<PurchaseRecord>
        {
            new PurchaseRecord { BuyerId = 1, TotalPrice = 100 },
            new PurchaseRecord { BuyerId = 1, TotalPrice = 200 },
            new PurchaseRecord { BuyerId = 2, TotalPrice = 50 }
        };

        var service = new MusicMarketplaceService(
            new StubProductRepository(_testProducts),
            new StubSellerRepository(_testSellers),
            new StubBuyerRepository(_testBuyers),
            new StubPurchaseRepository(testPurchases));

        /// <summary>
        /// Выполнение тестируемого метода
        /// </summary>
        var result = (await service.GetTopBuyersByAveragePurchaseAsync()).ToList();

        /// <summary>
        /// Проверка результатов
        /// </summary>
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id); // Средняя покупка 150
        Assert.Equal(2, result[1].Id);  // Средняя покупка 50
    }

    /// <summary>
    /// Тест получения статистики продаж за последние 2 недели
    /// </summary>
    [Fact]
    public async Task GetSellerSalesStatsLastTwoWeeksAsync_ReturnsCorrectCounts()
    {
        /// <summary>
        /// Подготовка тестовых данных
        /// </summary>
        var testPurchases = new List<PurchaseRecord>
        {
            new PurchaseRecord
            {
                Product = new Product { SellerId = 1 },
                PurchaseDate = DateTime.UtcNow.AddDays(-1)
            },
            new PurchaseRecord
            {
                Product = new Product { SellerId = 1 },
                PurchaseDate = DateTime.UtcNow.AddDays(-10)
            },
            new PurchaseRecord
            {
                Product = new Product { SellerId = 2 },
                PurchaseDate = DateTime.UtcNow.AddDays(-15) // Не должна учитываться
            }
        };

        var service = new MusicMarketplaceService(
            new StubProductRepository(_testProducts),
            new StubSellerRepository(_testSellers),
            new StubBuyerRepository(_testBuyers),
            new StubPurchaseRepository(testPurchases));

        /// <summary>
        /// Выполнение тестируемого метода
        /// </summary>
        var result = await service.GetSellerSalesStatsLastTwoWeeksAsync();

        /// <summary>
        /// Проверка результатов
        /// </summary>
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result[_testSellers[0]]); // 2 продажи
        Assert.Equal(0, result[_testSellers[1]]); // 0 продаж
    }
}

/// <summary>
/// Заглушка для репозитория продуктов
/// </summary>
public class StubProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public StubProductRepository(List<Product> products) => _products = products;

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult(_products.AsEnumerable());

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetBySellerAsync(int sellerId) =>
        Task.FromResult(_products.Where(p => p.SellerId == sellerId));

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Заглушка для репозитория продавцов
/// </summary>
public class StubSellerRepository : ISellerRepository
{
    private readonly List<Seller> _sellers;

    public StubSellerRepository(List<Seller> sellers) => _sellers = sellers;

    public Task AddAsync(Seller seller)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Seller>> GetAllAsync() => Task.FromResult(_sellers.AsEnumerable());

    public Task<Seller> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Seller seller)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Заглушка для репозитория покупателей
/// </summary>
public class StubBuyerRepository : IBuyerRepository
{
    private readonly List<Buyer> _buyers;

    public StubBuyerRepository(List<Buyer> buyers) => _buyers = buyers;

    public Task AddAsync(Buyer buyer)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Buyer>> GetAllAsync() => Task.FromResult(_buyers.AsEnumerable());

    public Task<Buyer> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Заглушка для репозитория записей о покупках
/// </summary>
public class StubPurchaseRepository : IPurchaseRecordRepository
{
    private readonly List<PurchaseRecord> _purchases;

    public StubPurchaseRepository(List<PurchaseRecord> purchases) => _purchases = purchases;

    public Task AddAsync(PurchaseRecord record)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PurchaseRecord>> GetAllAsync() => Task.FromResult(_purchases.AsEnumerable());

    public Task<IEnumerable<PurchaseRecord>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}