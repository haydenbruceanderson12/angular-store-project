using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class SeedStoreDbContext
{
    public static async Task SeedAsync(StoreDbContext context)
    {
        if (context.Products.Any() is true) return;

        var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

        if (products is null) return;

        await context.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
