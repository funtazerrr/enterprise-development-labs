using System;
using System.Threading.Tasks;

/// <summary>
/// Класс для заполнения репозиториев начальными тестовыми данными
/// </summary>
public class DataSeeder
{
    private readonly IProductRepository _productRepository;
    private readonly ISellerRepository _sellerRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IPurchaseRecordRepository _purchaseRepository;

    public DataSeeder(
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

    /// <summary>
    /// Заполняет все репозитории тестовыми данными
    /// </summary>
    public async Task SeedAsync()
    {
        await SeedSellersAsync();
        await SeedBuyersAsync();
        await SeedProductsAsync();
        await SeedPurchaseRecordsAsync();
    }

    private async Task SeedSellersAsync()
    {
        var sellers = new[]
        {
            new Seller { ShopName = "Vinyl Masters", Country = "USA", ShippingCostPerItem = 10.0m },
            new Seller { ShopName = "CD Empire", Country = "UK", ShippingCostPerItem = 5.0m },
            new Seller { ShopName = "Retro Sounds", Country = "Germany", ShippingCostPerItem = 8.0m }
        };

        foreach (var seller in sellers)
        {
            await _sellerRepository.AddAsync(seller);
        }
    }

    private async Task SeedBuyersAsync()
    {
        var buyers = new[]
        {
            new Buyer { FullName = "John Smith", Country = "Canada", Address = "123 Maple St" },
            new Buyer { FullName = "Maria Garcia", Country = "Spain", Address = "456 Oak Ave" },
            new Buyer { FullName = "Ivan Petrov", Country = "Russia", Address = "789 Birch Rd" }
        };

        foreach (var buyer in buyers)
        {
            await _buyerRepository.AddAsync(buyer);
        }
    }

    private async Task SeedProductsAsync()
    {
        var products = new[]
        {
            // Виниловые пластинки
            new Product {
                Format = AudioFormat.Vinyl,
                ReleaseType = ReleaseType.Album,
                Artist = "Pink Floyd",
                Title = "The Dark Side of the Moon",
                Country = "UK",
                MediaCondition = Condition.Excellent,
                PackagingCondition = Condition.Good,
                Price = 99.99m,
                IsSold = true,
                SellerId = 1
            },
            new Product {
                Format = AudioFormat.Vinyl,
                ReleaseType = ReleaseType.Album,
                Artist = "The Beatles",
                Title = "Abbey Road",
                Country = "UK",
                MediaCondition = Condition.Good,
                PackagingCondition = Condition.Good,
                Price = 89.99m,
                IsSold = false,
                SellerId = 1
            },

            // CD-диски
            new Product {
                Format = AudioFormat.Disc,
                ReleaseType = ReleaseType.Album,
                Artist = "Queen",
                Title = "A Night at the Opera",
                Country = "USA",
                MediaCondition = Condition.New,
                PackagingCondition = Condition.New,
                Price = 29.99m,
                IsSold = true,
                SellerId = 2
            },
            new Product {
                Format = AudioFormat.Disc,
                ReleaseType = ReleaseType.Single,
                Artist = "Michael Jackson",
                Title = "Thriller",
                Country = "USA",
                MediaCondition = Condition.Excellent,
                PackagingCondition = Condition.Excellent,
                Price = 19.99m,
                IsSold = false,
                SellerId = 2
            },

            // Кассеты
            new Product {
                Format = AudioFormat.Cassette,
                ReleaseType = ReleaseType.Album,
                Artist = "Nirvana",
                Title = "Nevermind",
                Country = "USA",
                MediaCondition = Condition.Satisfactory,
                PackagingCondition = Condition.Good,
                Price = 24.99m,
                IsSold = true,
                SellerId = 3
            }
        };

        foreach (var product in products)
        {
            await _productRepository.AddAsync(product);
        }
    }

    private async Task SeedPurchaseRecordsAsync()
    {
        var now = DateTime.UtcNow;
        var purchases = new[]
        {
            new PurchaseRecord {
                ProductId = 1,
                BuyerId = 1,
                PurchaseDate = now.AddDays(-3),
                TotalPrice = 109.99m // цена + доставка
            },
            new PurchaseRecord {
                ProductId = 3,
                BuyerId = 2,
                PurchaseDate = now.AddDays(-10),
                TotalPrice = 34.99m
            },
            new PurchaseRecord {
                ProductId = 5,
                BuyerId = 3,
                PurchaseDate = now.AddDays(-15),
                TotalPrice = 32.99m
            }
        };

        foreach (var purchase in purchases)
        {
            await _purchaseRepository.AddAsync(purchase);
        }
    }
}