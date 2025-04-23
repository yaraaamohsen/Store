using System.Text.Json;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;

namespace Presistance
{
    public class DbIntializer : IDbIntializer
    {
        private readonly StoreDbContext _context;

        public DbIntializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task IntializeAsync()
        {
            try
            {
                // Create Database If It Doesn't Exists && Apply Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                // Data Seeding

                // seeding ProductTypes From Json Files
                if (!_context.ProductType.Any())
                {
                    // 1. Read All Data From Types Json File As String
                    var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\types.json");

                    // 2. Transform String To C# Object [List Of ProductTypes] [Deserialize => from string to C# Object]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                    // 3. Add List<ProductTypes> To Database    
                    if (types != null && types.Any())
                    {
                        await _context.ProductType.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                // seeding ProductBrands From Json Files
                if (!_context.ProductBrand.Any())
                {
                    var BrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    if (brands != null && brands.Any())
                    {
                        await _context.ProductBrand.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                // seeding Product From Json Files
                if (!_context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\Seeding\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
